using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailColorPlusLocatorDesktopTests : ProductDetailColorPlusLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailColorPlusLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailColorPlusElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctColorPlusElements()
        {
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusListAllBaseSectionAnchors);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusListBaseOptionsWidgetAnchors);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ViewAllColorsLink);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ProductSliders);
        }

        protected override void VerifyManufacturerLinks() { }
    }


    public class ProductDetailColorPlusLocatorMobileTests : ProductDetailColorPlusLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailColorPlusLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailColorPlusElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctColorPlusElements()
        {
            VerifyElementsNotImplemented(() => ProductDetailColorPlus.ColorPlusListAllBaseSectionAnchors);
            VerifyElementsNotImplemented(() => ProductDetailColorPlus.ColorPlusListBaseOptionsWidgetAnchors);
            VerifyElementNotImplemented(() => ProductDetailColorPlus.ViewAllColorsLink);
            VerifyElementNotImplemented(() => ProductDetailColorPlus.ProductSliders);
        }

        protected override void VerifyManufacturerLinks()
        {
            ProductDetail.ProductDescriptionAccordion.Click();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.ProductDescriptionAccordion);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailColorPlusLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailColorPlusLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            // Obtain a list of elements for the Color Plus on Product Detail Page
            BuildElementsList(ProductDetailColorPlus);

            // Verify Color Plus elements common to Desktop and Mobile
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetColorPlusSku);

            VerifyElementDisplayed(() => ProductDetailColorPlus.PdpMoreYouMayLikeElement);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusSlider);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusShadeOptionsLabel);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusBaseColorOptionsLabel);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ColorPlusAllBaseColorsSection);

            VerifyManufacturerLinks();

            VerifyElementDisplayed(() => ProductDetailColorPlus.ManufacturerLink);
            VerifyElementDisplayed(() => ProductDetailColorPlus.ManufacturerLinkAnchor);

            // Verify Color Plus elements that are different on Desktop and Mobile
            VerifyDistinctColorPlusElements();
        }

        protected abstract void VerifyDistinctColorPlusElements();

        protected abstract void VerifyManufacturerLinks();
    }
}
