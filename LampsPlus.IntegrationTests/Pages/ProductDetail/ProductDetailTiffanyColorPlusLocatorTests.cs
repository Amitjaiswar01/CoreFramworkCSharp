using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailTiffanyColorPlusLocatorDesktopTests : ProductDetailTiffanyColorPlusLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailTiffanyColorPlusLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Tiffany Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailTiffanyColorPlusElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctTiffanyColorPlusElements()
        {
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyViewAllColorsLink);
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyListAllBaseSectionAnchors);
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyListBaseOptionsWidgetAnchors);
        }
    }


    public class ProductDetailTiffanyColorPlusLocatorMobileTests : ProductDetailTiffanyColorPlusLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailTiffanyColorPlusLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Tiffany Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailTrackLightingElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctTiffanyColorPlusElements()
        {
            VerifyElementNotImplemented(() => ProductDetailTiffanyColorPlus.TiffanyViewAllColorsLink);
            VerifyElementsNotImplemented(() => ProductDetailTiffanyColorPlus.TiffanyListAllBaseSectionAnchors);
            VerifyElementsNotImplemented(() => ProductDetailTiffanyColorPlus.TiffanyListBaseOptionsWidgetAnchors);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailTiffanyColorPlusLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailTiffanyColorPlusLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Tiffany Color Plus on Product Detail Page.
            BuildElementsList(ProductDetailTiffanyColorPlus);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetTiffanyColorPlusShortSku);
           
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyColorPlusSlider);
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyShadeOptionsLabel);
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyBaseOptionsLabel);
            VerifyElementDisplayed(() => ProductDetailTiffanyColorPlus.TiffanyAllBaseColorsSection);

            VerifyDistinctTiffanyColorPlusElements();
        }

        protected abstract void VerifyDistinctTiffanyColorPlusElements();
    }
}
