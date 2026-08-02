using System.Threading;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailMultiProductDesktopLocatorTests : ProductDetailMultiProductLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailMultiProductDesktopLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Multi Product elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailMultiProductElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctMultiProductElements()
        {
            VerifyElementDisplayed(() => ProductDetailMultiProduct.MultiProdSizeOptions);
            VerifyElementNotImplemented(() => ProductDetailMultiProduct.MultiProductRadioOptions);
        }
    }


    public class ProductDetailMultiProductLocatorMobileTests : ProductDetailMultiProductLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailMultiProductLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Track Lighting elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailMultiProductElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctMultiProductElements()
        {
            VerifyElementNotImplemented(() => ProductDetailMultiProduct.MultiProdSizeOptions);
            VerifyElementExists(() => ProductDetailMultiProduct.MultiProductRadioOptions);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailMultiProductLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailMultiProductLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Multi Product PDP.
            BuildElementsList(ProductDetailMultiProduct);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSkuForMultiProduct);
          
            VerifyElementDisplayed(() => ProductDetailMultiProduct.AvailableOptionsSectionTitle);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.SelectedMultiProductDropdownOption);

            ProductDetailMultiProduct.SelectedMultiProductDropdownOption.Click();

            Thread.Sleep(5000);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.ShipsFreeWithOrdersOver49CallOutForMultiProduct);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.MultiProductPrices);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.UnselectedMultiProductDropdownOption);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.MultiProdSizeOptionsElement);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.MultiProductOptionNames);
            VerifyElementDisplayed(() => ProductDetailMultiProduct.MultiProductDropdownOptions);

            VerifyDistinctMultiProductElements();
        }

        protected abstract void VerifyDistinctMultiProductElements();
    }
}
