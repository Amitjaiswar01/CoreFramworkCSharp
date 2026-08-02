using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailFinishFamilyLocatorDesktopTests : ProductDetailFinishFamilyLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailFinishFamilyLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Finish Family elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailFinishFamilyElementsTest(string config) => Locate(config);
    }


    public class ProductDetailFinishFamilyLocatorMobileTests : ProductDetailFinishFamilyLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailFinishFamilyLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Finish Family elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailFinishFamilyElementsTest(string config) => Locate(config);
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailFinishFamilyLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailFinishFamilyLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Finish Family on Product Detail Page.
            BuildElementsList(ProductDetailFinishFamily);

            // Verify Finish Family elements common to Desktop and Mobile.
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuThatHasFinishFamily());

            VerifyElementDisplayed(() => ProductDetailFinishFamily.MoreOptionsCollapsableSectionHeader);
            VerifyElementDisplayed(() => ProductDetailFinishFamily.MoreOptionsCollapsableSlider);
            VerifyElementDisplayed(() => ProductDetailFinishFamily.ItemsList);
            VerifyElementDisplayed(() => ProductDetailFinishFamily.OtherOptionsAccordion);
        }
    }
}
