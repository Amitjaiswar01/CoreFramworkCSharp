using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T354_T539_VerifyCorrectQtyAddedToWishListAddingItemPdp
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T354_Windows_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_Windows_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T354_Windows_Kiosk_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_Windows_Kiosk_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T354_Windows_Employee_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_Windows_Employee_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T354_Mac_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_Mac_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T354_iPad_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_iPad_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T354_TabletEmulator_VerifyCorrectQtyAddedToWishListAddingItemPdp : T354_DesktopBase
    {
        public T354_TabletEmulator_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify That The Correct QTY Is Added To The Wish List When Adding An Item From The PDP Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10104
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T354
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10104"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T354")]
    public abstract class T354_DesktopBase : TestsBaseDesktop
    {
        protected T354_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange :
            User is on the Product Detail Page for the SKU with Inventory > 20
            User has no items in the Wish List
            */
            InitializeFunctionalTest(config);
            WishList.EmptyWishList();
            var sku = ProductActions.GetSkuThatHasQuantityGreaterThanTwenty;
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP");

            //Act : Type in any digit > 1 in the Quantity Field
            var quantity = MathHelper.GetRandomNumber(2, 20);
            ProductDetail.ChangeProductQuantity(quantity.ToString());

            //Assert : Verify the WishList button has "SAVE" Text
            Assert.Equals(ProductDetail.GetSavedButtonCallout(), ProductDetail.SavedWishListBeforeText, "Add To Wishlist Button does not have correct Text");

            //Act : Add the product to WishList
            WishListWorkflow.AddToWishlistAndVerifyCount();

            //Assert : Verify the 'SAVE' button changes to 'SAVED'
            Assert.Equals(ProductDetail.GetSavedButtonCallout(), ProductDetail.SavedWishListAfterText, "The 'SAVE' button did not change to 'SAVED");

            //Assert : Verify the quantity shown against SAVED drop-down in the header is '1'.
            Assert.Equals(HeaderFooter.GetNumberOfWishListItems(), 1, "The Quantity shown against SAVED drop-down in the header is Incorrect");

            //Assert : Verify user does not navigate directly to WishList page
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP");

            //Act : Navigate to WishList Page
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "User is not on the Wish List Page");
            var wishListSku = WishList.GetWishListItemSku();
            var wishListQty = WishList.GetWishListItemQty();

            //Assert : Verify the quantity entered on the PDP for the SKU is the quantity on the Wish List
            Assert.Equals(sku, wishListSku, "Item Added in the Wish List is Incorrect");
            Assert.Equals(quantity, wishListQty, "Quantity Added in the Wish List is Incorrect");
        }
    }
}
