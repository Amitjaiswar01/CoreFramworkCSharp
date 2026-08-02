using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T219_T453_VerifyPlaFunctionality
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T453_iPhone_VerifyPlaFunctionality : T453_MobileBase
    {
        public T453_iPhone_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T453_Android_VerifyPlaFunctionality : T453_MobileBase
    {
        public T453_Android_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T453_Emulator_VerifyPlaFunctionality : T453_MobileBase
    {
        public T453_Emulator_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    /// <sumamry>
    ///Verify the functionality of PLAs on the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10079
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T453
    ///</sumamry>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10079"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T453")]
    public abstract class T453_MobileBase : TestsBaseMobile
    {
        protected T453_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to a PLA page with reviews
            var expectedSku = SortPla.GetPlaSkuWithReviews();
            var expectedUrl = Urls.CrystalChandeliersUrl;

            SortPla.NavigateToPlaWithReviews(expectedUrl, expectedSku);
            Assert.True(SortPla.IsCurrentPage, "PLA page is not displayed");

            //Act : Scroll to the bottom of the page
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            //Assert : Verify a sticky header appears
            Assert.True(SortPla.IsStickyHeaderVisible, "Sticky filter header not displayed after scrolling to the bottom of the page.");

            //Act : Scroll to the top of the page and tap on the product name
            Browser.ScrollToTopOfWindow();
            SortPla.NavigateToPdpThroughPlaProductName();

            //Assert : Verify after clicking on the product name takes the user to the product's PDP
            Assert.True(ProductDetail.IsCurrentPage, "Clicking on PLA does not navigate to PDP");

            //Act : Navigate back to Pla page
            Browser.GoBack();

            //Act : Click on More Details link
            SortPla.NavigateToPdpThroughMoreDetails();

            //Assert : Verify after clicking on the 'View Details' link, the user is brought to the corresponding PDP
            Assert.True(ProductDetail.IsCurrentPage, "Product Detail Page is not loaded");
            Assert.Equals(expectedSku, ProductDetail.GetProductSku(), $"PLA SKU: {expectedSku} not same on PLA and PDP.");

            //Act : Navigate back to Pla page and Add product to Cart
            Browser.GoBack();
            SortPla.PlaAddToCart();

            //Assert : Verify the product is added to Cart
            Assert.Equals(expectedSku, Cart.GetListOfCartSkus(Browser.PageUrl, 1)[0], $"PLA SKU: {expectedSku} was not added to the cart.");
        }
    }
}