using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.AddingToCartAndWishList.T344_VerifyUserCanAddItemWishlist
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_Windows_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_Windows_VerifyUserCanAddItemWishlist (ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_Windows_Kiosk_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_Windows_Kiosk_VerifyUserCanAddItemWishlist(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_Windows_Employee_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_Windows_Employee_VerifyUserCanAddItemWishlist(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_Mac_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_Mac_VerifyUserCanAddItemWishlist(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_iPad_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_iPad_VerifyUserCanAddItemWishlist(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AddingToCartAndWishList)]
    public class T344_TabletEmulator_VerifyUserCanAddItemWishlist : T344_DesktopBase
    {
        public T344_TabletEmulator_VerifyUserCanAddItemWishlist(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyUserCanAddItemToWishlist(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can add an item to the Wish List.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9936
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T344
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9936"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T344")]
    public abstract class T344_DesktopBase : TestsBaseDesktop
    {
        protected T344_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST."); //Wishlist icon is not appearing for CSI account types.
            // Arrange : Empty the already created wishlist and navigate to product detail page for product which have free shipping and free return callout
            var shortSku = ProductActions.GetFreeShippingAndReturnShortSkus;
            Assert.DatabaseObject(shortSku, "ProductActions.GetFreeShippingAndReturnShortSkus()");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage,"Current page is not the product detail page");
            var productQuantity = ProductDetail.GetProductQuantity();
           
            // Act : Add item to wish list and get information for Product Sku and Quantity 
            WishListWorkflow.AddToWishlistAndVerifyCount();

            HeaderFooter.NavigateToWishListThroughHeaderLink();
            Assert.True(WishList.IsCurrentPage, "Current page is not the wish list page");
            var wishListSku = WishList.GetWishListItemSku();
            var wishListQty = WishList.GetWishListProductQty(0);

            // Assert : Check weather correct product and shortSku added to wish list or not and free shipping call out is displayed or not
            Assert.Equals(shortSku, wishListSku, "Sku does not match");
            Assert.Equals(productQuantity, wishListQty, "Quantity does not match");
            Assert.Displayed(WishList.GetFreeShippingCallout(), "The free shipping call out was not displayed on the page.");

            if (TestSetup.TestConfiguration.Browser != WebBrowser.Safari)
            {
                WishList.EmptyWishList();
            }
        }
    }
}