using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailMcpDesktopLocatorTests : ProductDetailMcpLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        public ProductDetailMcpDesktopLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailMcpElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctMcpElements()
        {
            VerifyElementDisplayed(() => ProductDetailMcp.PdpInterestElement);
            VerifyElementDisplayed(() => ProductDetailMcp.CustomerReviewsElement);

            VerifyElementDisplayed(() => ProductDetailMcp.MorePatternsLink);
            VerifyElementDisplayed(() => ProductDetailMcp.AllArtShadesLink);
            VerifyElementDisplayed(() => ProductDetailMcp.ArtShadeLink);
            VerifyElementDisplayed(() => ProductDetailMcp.ArtShadeLinks);
            VerifyElementDisplayed(() => ProductDetailMcp.ColorCustomizerToggleVisibilityLink);
            VerifyElementDisplayed(() => ProductDetailMcp.MorePatterns);
            VerifyElementDisplayed(() => ProductDetailMcp.OtherPatternsTop);
            VerifyElementDisplayed(() => ProductDetailMcp.OtherPatterns);
            VerifyElementDisplayed(() => ProductDetailMcp.PopularColorsTop);

            ProductDetailMcp.CustomizeColorsTop.Click();

            VerifyElementDisplayed(() => ProductDetailMcp.ListOfCustomizePatternColors);
            VerifyElementDisplayed(() => ProductDetailMcp.ListOfCustomizeSelectColors);
            VerifyElementDisplayed(() => ProductDetailMcp.ListOfTrimColors);
            VerifyElementDisplayed(() => ProductDetailMcp.CustomizeColors);
            VerifyElementDisplayed(() => ProductDetailMcp.CustomizeColorsTop);
            VerifyElementDisplayed(() => ProductDetailMcp.CustomizeColorsContent);
            VerifyElementDisplayed(() => ProductDetailMcp.SelectColors);
            VerifyElementDisplayed(() => ProductDetailMcp.TrimColors);

            ProductDetailMcp.OtherPatternsTop.Click();

            VerifyElementDisplayed(() => ProductDetailMcp.ListOfOtherPatterns);
            VerifyElementDisplayed(() => ProductDetailMcp.OtherPatternsContent);

            ProductDetailMcp.PopularColorsTop.Click();

            VerifyElementDisplayed(() => ProductDetailMcp.ListOfPopularColors);
            VerifyElementDisplayed(() => ProductDetailMcp.PopularColors);
            VerifyElementDisplayed(() => ProductDetailMcp.PopularColorsContent);

            Browser.Wait.ForClickableElement(ProductDetailMcp.ListOfPopularColors[1]).Click();
            VerifyElementDisplayed(() => ProductDetailMcp.CaretIcon);
        }
    }


    public class ProductDetailMcpLocatorMobileTests : ProductDetailMcpLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailMcpLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Color Plus elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailColorPlusElementsTest(string config) => Locate(config);

        protected override void VerifyDistinctMcpElements()
        {
            VerifyElementNotImplemented(() => ProductDetailMcp.AllArtShadesLink);
            VerifyElementNotImplemented(() => ProductDetailMcp.ArtShadeLinks);
            VerifyElementNotImplemented(() => ProductDetailMcp.ArtShadeLink);
            VerifyElementNotImplemented(() => ProductDetailMcp.ColorCustomizerToggleVisibilityLink);
            VerifyElementNotImplemented(() => ProductDetailMcp.MorePatterns);
            VerifyElementNotImplemented(() => ProductDetailMcp.OtherPatternsTop);
            VerifyElementNotImplemented(() => ProductDetailMcp.OtherPatterns);
            VerifyElementNotImplemented(() => ProductDetailMcp.PopularColorsTop);

            VerifyElementsNotImplemented(() => ProductDetailMcp.ListOfCustomizePatternColors);
            VerifyElementsNotImplemented(() => ProductDetailMcp.ListOfCustomizeSelectColors);
            VerifyElementsNotImplemented(() => ProductDetailMcp.ListOfTrimColors);
            VerifyElementNotImplemented(() => ProductDetailMcp.CustomizeColors);
            VerifyElementNotImplemented(() => ProductDetailMcp.CustomizeColorsTop);
            VerifyElementNotImplemented(() => ProductDetailMcp.CustomizeColorsContent);
            VerifyElementNotImplemented(() => ProductDetailMcp.SelectColors);
            VerifyElementNotImplemented(() => ProductDetailMcp.TrimColors);

            VerifyElementsNotImplemented(() => ProductDetailMcp.ListOfOtherPatterns);
            VerifyElementNotImplemented(() => ProductDetailMcp.OtherPatternsContent);

            VerifyElementsNotImplemented(() => ProductDetailMcp.ListOfPopularColors);
            VerifyElementNotImplemented(() => ProductDetailMcp.PopularColors);
            VerifyElementNotImplemented(() => ProductDetailMcp.PopularColorsContent);
            VerifyElementNotImplemented(() => ProductDetailMcp.MorePatternsLink);
            VerifyElementNotImplemented(() => ProductDetailMcp.CaretIcon);

            VerifyElementNotImplemented(() => ProductDetailMcp.CustomerReviewsElement);
            VerifyElementNotImplemented(() => ProductDetailMcp.PdpInterestElement);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailMcpLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output"></param>
        protected ProductDetailMcpLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given product detail page.
        /// </summary>        
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements for the Color Plus on Product Detail Page
            BuildElementsList(ProductDetailMcp);

            var mcpItemEntity = ProductActions.GetMpcItemSkus();
            ConditionalVerify.DatabaseObject(mcpItemEntity, "ProductActions.GetMpcItemSkus()");
            Search.ExecuteSearch(mcpItemEntity.ShortSku);

            VerifyElementDisplayed(() => ProductDetailMcp.PdpMoreYouMayLikeElement);

            VerifyDistinctMcpElements();
        }

        protected abstract void VerifyDistinctMcpElements();
    }
}
