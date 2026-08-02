using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public class ProductDetailDimmersLocatorDesktopTests : ProductDetailDimmersLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        public ProductDetailDimmersLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements can be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailDimmersElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctDimmerElements()
        {
            VerifyElementDisplayed(() => ProductDetailDimmers.BuildFullSystemOptions);
            VerifyElementDisplayed(() => ProductDetailDimmers.ListOfFullSystemSkus);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public class ProductDetailDimmersLocatorMobileTests : ProductDetailDimmersLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        public ProductDetailDimmersLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements can be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailDimmersElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctDimmerElements()
        {
            VerifyElementNotImplemented(() => ProductDetailDimmers.BuildFullSystemOptions);
            VerifyElementNotImplemented(() => ProductDetailDimmers.ListOfFullSystemSkus);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailDimmersLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailDimmersLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Color Plus on Product Detail Page
            BuildElementsList(ProductDetailDimmers);

            var byoDimmerItemWithOptionsDb = ProductActions.GetByoDimmerWithItemOptions();

            ConditionalVerify.DatabaseObject(byoDimmerItemWithOptionsDb, "ProductActions.GetByoDimmerWithItemOptions()");

            ProductDetail.NavigateToProductDetailByShortSku(byoDimmerItemWithOptionsDb.PrimarySku);

            VerifyElementDisplayed(() => ProductDetailDimmers.SelectedMultiProductDropdownOption);

            VerifyDistinctDimmerElements();
        }

        protected abstract void VerifyDistinctDimmerElements();
    }
}
