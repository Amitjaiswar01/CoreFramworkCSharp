using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailTrackLightingLocatorDesktopTests : ProductDetailTrackLightingLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailTrackLightingLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Track Lighting elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailTrackLightingElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctTrackLightingElements()
        {
            VerifyElementDisplayed(() => ProductDetailTrackLighting.BuildFullSystemAddToWishListButton);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.BuildFullSystemAddToCartButton);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.BuildFullSystemContainer);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.BuildFullSystemOptions);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.DesignYourOwnTrackLightingSystemBanner);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.ListOfFullSystemProductNames);
            VerifyElementDisplayed(() => ProductDetailTrackLighting.ListOfFullSystemSkus);

            Browser.Navigate(Urls.DesignYourOwnTrackLightingSystemPageUrl);

            VerifyElementDisplayed(() => ProductDetailTrackLighting.DyotsSelectRoom);
        }
    }


    public class ProductDetailTrackLightingLocatorMobileTests : ProductDetailTrackLightingLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailTrackLightingLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Track Lighting elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailTrackLightingElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctTrackLightingElements()
        {
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.BuildFullSystemAddToWishListButton);
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.BuildFullSystemAddToCartButton);
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.BuildFullSystemContainer);
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.BuildFullSystemOptions);
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.DesignYourOwnTrackLightingSystemBanner);
            VerifyElementNotImplemented(() => ProductDetailTrackLighting.DyotsSelectRoom);
            VerifyElementsNotImplemented(() => ProductDetailTrackLighting.ListOfFullSystemProductNames);
            VerifyElementsNotImplemented(() => ProductDetailTrackLighting.ListOfFullSystemSkus);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailTrackLightingLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailTrackLightingLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Track Lighting on Product Detail Page.
            BuildElementsList(ProductDetailTrackLighting);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetTrackLightingProductCase1().PrimarySku);

            VerifyDistinctTrackLightingElements();
        }

        protected abstract void VerifyDistinctTrackLightingElements();
    }
}
