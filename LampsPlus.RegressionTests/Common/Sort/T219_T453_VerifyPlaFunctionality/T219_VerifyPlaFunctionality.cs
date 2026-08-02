using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T219_T453_VerifyPlaFunctionality
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T219_Windows_VerifyPlaFunctionality : T219_DesktopBase
    {
        public T219_Windows_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T219_Mac_VerifyPlaFunctionality : T219_DesktopBase
    {
        public T219_Mac_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T219_iPad_VerifyPlaFunctionality : T219_DesktopBase
    {
        public T219_iPad_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T219_TabletEmulator_VerifyPlaFunctionality : T219_DesktopBase
    {
        public T219_TabletEmulator_VerifyPlaFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PlaFunctionality(string config) => Validate(config);
    }


    /// <sumamry>
    ///Verify the functionality of PLAs on the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10079
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T219
    ///</sumamry>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10079"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T219")]
    public abstract class T219_DesktopBase : TestsBaseDesktop
    {
        protected T219_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to a PLA page with reviews
            var expectedSku = SortPla.GetPlaSkuWithReviews();
            var expectedUrl = Urls.CrystalChandeliersUrl;

            SortPla.NavigateToPlaWithReviews(expectedUrl, expectedSku);
            Assert.True(SortPla.IsCurrentPage, "PLA page is not displayed");

            //Assert : Verify Review stars and Text with the number of reviews id displayed
            Assert.True(!SortPla.DoesPlaRatingStarsDisplay(), "The rating stars are not displayed");
            Assert.True(SortPla.DoesReviewSummaryContainReviewsText(), "The count of reviews is not displayed");

            //Act : Navigate to review stars section
            SortPla.RedirectToCustomerReviewsSection();

            //Assert : Verify user is brought to the Reviews section of the PDP
            Assert.True(ProductDetail.IsCurrentPage, "Clicking on PLA does not navigate to PDP"); ;
            Assert.True(SortPla.IsReviewsSectionDisplayed, "Reviews section is not displayed");
            
            //Act : Navigate back to Pla page
            Browser.GoBack();

            //Assert : Verify Pla page displays
            Assert.True(SortPla.IsCurrentPage, "PLA page is not loaded");

            //Act : Click on View Details link
            SortPla.NavigateToPdpThroughMoreDetails();

            //Assert : Verify after clicking on the 'View Details' link, the user is brought to the corresponding PDP
            Assert.True(ProductDetail.IsCurrentPage, "Product Detail Page is not loaded");
            Assert.Equals(expectedSku, ProductDetail.GetProductSku(), $"PLA SKU: { expectedSku} not same on PLA and PDP.");

            //Act : Navigate back to Pla page and Add product to Cart
            Browser.GoBack();
            SortPla.PlaAddToCart();

            //Assert : Verify the product is added to Cart
            Assert.Equals(expectedSku, Cart.GetListOfCartSkus(Browser.PageUrl, 1)[0], $"PLA SKU: {expectedSku} was not added to the cart.");
        }
    }
}