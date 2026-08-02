using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T7637_T7638_VerifyUserCanAddOpenBoxItemToTheCart
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7637_Windows_VerifyUserCanAddOpenBoxItemToTheCart : T7637_DesktopBase
    {
        public T7637_Windows_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanAddOpenBoxItemToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7637_Mac_VerifyUserCanAddOpenBoxItemToTheCart : T7637_DesktopBase
    {
        public T7637_Mac_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanAddOpenBoxItemToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7637_iPad_VerifyUserCanAddOpenBoxItemToTheCart : T7637_DesktopBase
    {
        public T7637_iPad_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7637_TabletEmulator_VerifyUserCanAddOpenBoxItemToTheCart : T7637_DesktopBase
    {
        public T7637_TabletEmulator_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a User Can add an Open Box Item to the Cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10099
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7637
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10099"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7637")]
    public abstract class T7637_DesktopBase : TestsBaseDesktop
    {
        protected T7637_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange: User has identified an Open Box item using the query.
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetOpenBoxShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetOpenBoxShortSku()");

            //Act : User has navigated to Product Detail Page of Open Box SKU
            ProductDetail.NavigateToOpenBoxProductDetailByShortSku(sku);

            //Assert : Verify that "OPEN BOX OUTLET PRICE" Text is displayed on PDP
            Assert.Equals(ProductDetail.GetOpenBoxCallout(), "OPEN BOX OUTLET PRICE", "Open Box Outlet text is not displayed");

            //Assert : Verify that Quantity Box is Displayed
            Assert.True(ProductDetail.IsQuantityBoxDisplayed(), "Quantity Box is not displayed");

            //Assert : Verify that "Buy It New" Link is Displayed
            Assert.Equals(ProductDetail.BuyItNewLinkText, ProductDetail.GetBuyItNewText(),  "Buy It New Link is not displayed");

            //Act : User has Clicked on "Buy It New" Link 
            ProductDetail.SwitchToNewProduct();

            //Assert : Verify that the page Url does not contain the word "open-box"
            Assert.False(Browser.PageUrl.Contains("open-box"), "open-box is not in the URL");

            //Assert : Verify that the "Open Box Available" Link is Displayed
            Assert.Equals(ProductDetail.GetOpenBoxAvailableLinkText(), "Open Box Available", "Open Box Available Link is not displayed");

            //Act : User has Clicked on "Open Box Available" Link 
            ProductDetail.SwitchToTheOpenBoxProduct();

            //Assert : Verify that the page Url Contains the word "open-box"
            Assert.True(Browser.PageUrl.Contains("open-box"), "open-box is not in the URL");

            //Act : User has added the product to Cart 
            ProductDetail.AddToCart();

            //Assert : Verify "Almost Sold Out!" Callout is displayed on Cart 
            Assert.Equals( "Almost Sold Out!", Cart.GetLimitedQuantityCallout(), "Limited Quantity Callout is not displayed");
        }
    }
}