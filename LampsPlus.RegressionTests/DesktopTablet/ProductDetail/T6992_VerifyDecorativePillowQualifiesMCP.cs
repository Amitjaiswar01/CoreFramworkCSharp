using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T6992_Windows_VerifyDecorativePillowQualifiesMcp : T6992_DesktopBase
	{
		public T6992_Windows_VerifyDecorativePillowQualifiesMcp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void DecorativePillowQualifiesMcp(string config) => Validate(config);
	}


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T6992_Mac_VerifyDecorativePillowQualifiesMcp : T6992_DesktopBase
    {
        public T6992_Mac_VerifyDecorativePillowQualifiesMcp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void DecorativePillowQualifiesMcp(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T6992_iPad_VerifyDecorativePillowQualifiesMcp : T6992_DesktopBase
    {
        public T6992_iPad_VerifyDecorativePillowQualifiesMcp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void DecorativePillowQualifiesMcp(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T6992_TabletEmulator_VerifyDecorativePillowQualifiesMcp : T6992_DesktopBase
    {
        public T6992_TabletEmulator_VerifyDecorativePillowQualifiesMcp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void DecorativePillowQualifiesMcp(string config) => Validate(config);
    }


    /// <summary>
	/// Verify that a Decorative Pillow item qualifies to be an MCP Item.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6077
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6992
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6077"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6992")]
	public abstract class T6992_DesktopBase : ProductDetailTestsBase
	{
		protected T6992_DesktopBase(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
		{
			InitializeFramework(config, Urls.HomePageUrl);

			var product = ProductActions.GetMcpPillowItemSkus();
			Assert.DatabaseObject(product, "ProductActions.GetMcpPillowItemSkus()");

            Search.SearchField.Click();
			Search.ExecuteSearch(product.ShortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

			// Step 1
			VerifyBaseUrlString(product.BaseSku);
			VerifyMorePatternsLink(product.BaseSku);

			// Step 2 and 3
			PopularColorSteps();

			// Step 4
			ProductDetailMcp.CustomizeColors.Click();
			CustomizeColorsSteps();

			// Step 5
			ProductDetailMcp.PopularColors.Click();
			Browser.Wait.ForElementToStopAnimating(ProductDetailMcp.CaretIcon);
			ProductDetailMcp.OtherPatterns.Click();
			OtherPatternSteps();
		}

		private void PopularColorSteps()
		{
			var popularColors = ProductDetailMcp.ListOfPopularColors;
			Browser.Wait.ForClickableElement(popularColors[1]).Click();

			// Wait for popular color slider to load
			Browser.Wait.UntilElementUnloads(Browser.Locate.ElementById(ProductDetailMcp.LoadingWrapperId));
			Browser.Wait.ForElementToStopAnimating(ProductDetailMcp.CaretIcon);
			VerifyUiElementsAreHidden();
			Browser.Wait.ForClickableElement(popularColors[0]).Click();

            // Wait for popular color slider to load
            Browser.Wait.UntilElementUnloads(Browser.Locate.ElementById(ProductDetailMcp.LoadingWrapperId));
            VerifyUiElementsAreShown();
		}

		private void CustomizeColorsSteps()
		{
			var patternColor = ProductDetailMcp.ListOfCustomizePatternColors[1];
			var selectPattern = ProductDetailMcp.ListOfCustomizeSelectColors[1];
			Browser.Wait.ForClickableElement(patternColor).Click();
			Browser.Wait.ForClickableElement(selectPattern).Click();

			VerifyUiElementsAreHidden();
		}

		private void OtherPatternSteps()
		{
			var otherPattern = ProductDetailMcp.ListOfOtherPatterns[1];
			Browser.Wait.ForClickableElement(otherPattern).Click();

			// Wait for other options slider to load
			Browser.Wait.ForDomReady(2000);
			VerifyUiElementsAreHidden();
		}

		private void VerifyBaseUrlString(string baseSku)
		{
			var morePatternsLink = ProductDetailMcp.MorePatternsLink.GetAttribute("href").ToLower();
			Assert.StringContains(morePatternsLink, $"products/s_{baseSku.ToLower()}", "More Patterns Url Doesn't Match");
		}

		private void VerifyMorePatternsLink(string baseSku)
		{
			var sortUrl = $"{Urls.LampsPlusProductsUrl}s_{baseSku}/"; // "/" is needed because the 301 redirect rule always append one if url does not already end with it.
			ProductDetailMcp.MorePatternsLink.Click();
			Browser.Wait.ForDomReady();
			Assert.True(Browser.PageUrl.Equals(sortUrl, StringComparison.OrdinalIgnoreCase), "URLs don't match for correct sort page.");
			Browser.GoBack();
		}

		private void VerifyUiElementsAreHidden()
		{
			Browser.Wait.ForCondition(() => !ProductDetailMcp.MorePatternsLink.Displayed);

			Assert.False(ProductDetailMcp.MorePatternsLink.Displayed, "More Patterns element should not be Visible");
			Assert.False(ProductDetailMcp.CustomerReviewsElement.Displayed, "Customer Reviews element should not be Visible");
			Assert.False(ProductDetailMcp.PdpInterestElement.Displayed, "Pinterest icon element should not be Visible");
			Assert.False(ProductDetailMcp.PdpMoreYouMayLikeElement.Displayed, "More You May Like element should not be Visible");
		}

		private void VerifyUiElementsAreShown()
		{
			Browser.Wait.ForCondition(() => ProductDetailMcp.MorePatternsLink.Displayed);

			Assert.True(ProductDetailMcp.MorePatternsLink.Displayed, "More Patterns element should be Visible");
			Assert.True(ProductDetailMcp.CustomerReviewsElement.Displayed, "Customer Reviews element should be Visible");
			Assert.True(ProductDetailMcp.PdpInterestElement.Displayed, "Pinterest icon element should be Visible");
			Assert.True(ProductDetailMcp.PdpMoreYouMayLikeElement.Displayed, "More You May Like element should be Visible");
		}
	}
}
