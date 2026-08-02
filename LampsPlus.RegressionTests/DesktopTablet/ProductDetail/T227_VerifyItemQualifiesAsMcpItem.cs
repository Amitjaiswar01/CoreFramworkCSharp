using System.Web.UI;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T227_Windows_VerifyItemQualifiesAsMcpItem : T227_DesktopBase
    {
        public T227_Windows_VerifyItemQualifiesAsMcpItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ItemQualifiesAsMcp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T227_Mac_VerifyItemQualifiesAsMcpItem : T227_DesktopBase
    {
        public T227_Mac_VerifyItemQualifiesAsMcpItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ItemQualifiesAsMcp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T227_iPad_VerifyItemQualifiesAsMcpItem : T227_DesktopBase
    {
        public T227_iPad_VerifyItemQualifiesAsMcpItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ItemQualifiesAsMcp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T227_TabletEmulator_VerifyItemQualifiesAsMcpItem : T227_DesktopBase
    {
        public T227_TabletEmulator_VerifyItemQualifiesAsMcpItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ItemQualifiesAsMcp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an item qualifies to be an MCP Item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5165
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T227
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5165"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T227")]
    public abstract class T227_DesktopBase : ProductDetailTestsBase
    {
        protected T227_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Initialize test
            InitializeFramework(config);

            // Step 1
            var mcpItemEntity = ProductActions.GetMpcItemSkus();
            Assert.DatabaseObject(mcpItemEntity, "ProductActions.GetMpcItemSkus()");

            Search.ExecuteSearch(mcpItemEntity.ShortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            VerifyCustomSlidersWork();

            VerifyMorePatternsLink(mcpItemEntity.BaseSku);
            Browser.GoBack(); 

            VerifyAllArtShadesLink();
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreFiltersBtnClass.ToCssClassSelector()));
            Browser.Wait.WaitForAjaxComplete();

            Browser.GoBack();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.Wait.WaitForAjaxComplete();

            // Step 2 & 3
            SelectPopularColor();
            VerifyUiElementsAreHidden();

            DeselectPopularColor();
            VerifyUiElementsAreShown();

            // Step 4
            SelectCustomColor();

            Browser.Wait.IsInvisibleElement(By.Id(ProductDetailMcp.PdMoreYouMayLikeId));
            VerifyUiElementsAreHidden();
        }

        private void VerifyCustomSlidersWork()
        {
            // Only one slider's content can show at one time
            Assert.True(IsPopularColorsContentVisible(), "Popular colors should be displayed");
            Browser.Wait.ForClickableElement(ProductDetailMcp.PopularColorsTop).Click();
            Assert.False(IsPopularColorsContentVisible(), "Popular colors should not be displayed");

            Browser.Wait.ForClickableElement(ProductDetailMcp.CustomizeColorsTop).Click();
            Assert.True(IsCustomizeColorsContentVisible(), "Customize colors should be displayed");
        }

        private void VerifyMorePatternsLink(string baseSku)
        {
            var link = $"https://www.lampsplus.com/products/s_{baseSku}/".ToLower();
            ProductDetailMcp.ViewAllShadePatternsBtn.Click();
            Browser.Wait.AreAllElementsVisible(By.ClassName(Sort.SortResultImgContainerClass));
            Assert.Equals(link, Browser.PageUrl, "More patterns redirect url does not match the correct link.");
        }

        private void VerifyAllArtShadesLink()
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollToTopOfWindow();
            var link = $"{ProductDetail.ListOfBreadCrumbLink()[1].GetAttribute(HtmlTextWriterAttribute.Href.ToString())}type_art-shade/";

            Browser.ScrollToElement(ProductDetailMcp.AllArtShadesLink);
            Browser.ClickByJs(ProductDetailMcp.AllArtShadesLink);
            Browser.Wait.ForDomReady();
            Assert.Equals(link, Browser.PageUrl, $"{RecurringDataIssue}All art shades redirect url does not match the correct link.");
        }

        private void VerifyUiElementsAreShown()
        {
            //Browser.Wait.ForElement(ProductDetail.ColorCustomizerToggleVisibilityLink);
            Browser.Wait.ForDisplayedElement(ProductDetailMcp.ColorCustomizerToggleVisibilityLink);
            Assert.True(ProductDetailMcp.ColorCustomizerToggleVisibilityLink.Displayed, "More Patterns link should be displayed.");


            Browser.Wait.ForDisplayedElement(ProductDetailMcp.CustomerReviews);
            Assert.True(ProductDetailMcp.CustomerReviews.Displayed, "Customer Reviews element should be visible");

            Browser.Wait.ForDisplayedElement(ProductDetailMcp.PdpInterestElement);
            Assert.True(ProductDetailMcp.PdpInterestElement.Displayed, "Social Elements should be visible.");

            Browser.Wait.ForDisplayedElement(ProductDetailMcp.PdpMoreYouMayLikeElement);
            Assert.True(ProductDetailMcp.PdpMoreYouMayLikeElement.Displayed, "More Patterns element should be visible");

            Browser.Wait.ForDisplayedElement(ProductDetailMcp.AllArtShadesLink);
            Assert.True(ProductDetailMcp.AllArtShadesLink.Displayed, "All Art Shades link should be displayed.");
        }

        private void VerifyUiElementsAreHidden()
        {
            Assert.False(ProductDetailMcp.ColorCustomizerToggleVisibilityLink.Displayed, "More Patterns link should not be displayed.");
            Assert.False(ProductDetailMcp.CustomerReviews.Displayed, "Customer Reviews element should not be visible");
            Assert.False(ProductDetailMcp.PdpInterestElement.Displayed, "Social Elements should not be visible.");
            Assert.False(ProductDetailMcp.PdpMoreYouMayLikeElement.Displayed, "More You May Like element should not be visible");
            Assert.True(ProductDetailMcp.AllArtShadesLink.Displayed, "All Art Shades link should be displayed.");
        }

        private void SelectPopularColor()
        {
            // Select a popular color by random
            Browser.Wait.ForDomReady();
            var popularColors = Browser.Locate.DisplayedElements(ProductDetailMcp.ListOfPopularColors);
            var randomIndex = MathHelper.GetRandomNumber(popularColors.Count);

            Browser.Wait.ForClickableElement(popularColors[randomIndex == 0 ? 1 : randomIndex]).Click();
            Browser.Wait.UntilElementUnloads(Browser.Locate.ElementById(ProductDetailMcp.LoadingWrapperId));
            Browser.Wait.ForElement(ProductDetailMcp.PopularColorsTop);
        }

        private void DeselectPopularColor()
        {
            // Click original popular color from page load
            Browser.Wait.ForClickableElement(ProductDetailMcp.ListOfPopularColors[0]).Click();
            Browser.Wait.UntilElementUnloads(Browser.Locate.ElementById(ProductDetailMcp.LoadingWrapperId));
        }

        private void SelectCustomColor()
        {
            ProductDetailMcp.CustomizeColorsTop.Click();

            // Select a custom color pattern by random
            var patternColors = Browser.Locate.DisplayedElements(ProductDetailMcp.ListOfCustomizePatternColors);
            var randomIndex = MathHelper.GetRandomNumber(patternColors.Count);
            var patternColor = patternColors[randomIndex < 4 ? 4 : randomIndex]; // Prevent from selecting the small thumbnails

            // Select a custom color trim by random
            var trimColors = Browser.Locate.DisplayedElements(ProductDetailMcp.ListOfTrimColors);
            randomIndex = MathHelper.GetRandomNumber(trimColors.Count);
            var trimColor = trimColors[randomIndex == 0 ? 1 : randomIndex]; // Prevent from selecting the first option

            Browser.Wait.ForClickableElement(patternColor).Click();
            Browser.Wait.ForClickableElement(trimColor).Click();
        }

        private bool IsPopularColorsContentVisible()
        {
            Browser.Wait.ForElementToStopAnimating(ProductDetailMcp.CaretIcon);
            Browser.Wait.ForDomReady();
            return ProductDetailMcp.PopularColorsContent.Displayed && (!ProductDetailMcp.CustomizeColorsContent.Displayed);
        }

        private bool IsCustomizeColorsContentVisible()
        {
            Browser.Wait.ForElementToStopAnimating(ProductDetailMcp.CaretIcon);
            return ProductDetailMcp.CustomizeColorsContent.Displayed && (!ProductDetailMcp.PopularColorsContent.Displayed);
        }

        private bool IsOtherPatternsContentVisible()
        {
            Browser.Wait.ForElementToStopAnimating(ProductDetailMcp.CaretIcon);
            return ProductDetailMcp.OtherPatternsContent.Displayed && (!ProductDetailMcp.PopularColorsContent.Displayed && !ProductDetailMcp.CustomizeColorsContent.Displayed);
        }
    }
}
