using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T7637_T7638_VerifyUserCanAddOpenBoxItemToTheCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7638_iPhone_VerifyUserCanAddOpenBoxItemToTheCart : T7638_MobileBase
    {
        public T7638_iPhone_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanAddOpenBoxItemToCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7638_Emulator_VerifyUserCanAddOpenBoxItemToTheCart : T7638_MobileBase
    {
        public T7638_Emulator_VerifyUserCanAddOpenBoxItemToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanAddOpenBoxItemToCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a User Can Add an Open Box item to the Cart.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10099
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7638
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10099"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7638")]
    public abstract class T7638_MobileBase : TestsBaseMobile
    {
        protected T7638_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
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
            Assert.Equals(ProductDetail.BuyItNewLinkText, ProductDetail.GetBuyItNewText(), "Buy It New Link is not displayed");

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