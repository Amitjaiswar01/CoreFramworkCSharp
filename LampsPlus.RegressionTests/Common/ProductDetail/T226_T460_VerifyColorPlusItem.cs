using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using xRetry;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T226_Windows_VerifyColorPlusItem : T226_DesktopBase
	{
        public T226_Windows_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ColorPlusItem(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T226_Mac_VerifyColorPlusItem : T226_DesktopBase
    {
        public T226_Mac_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T226. Rework - ACD-10294")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ColorPlusItem(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T226_iPad_VerifyColorPlusItem : T226_DesktopBase
    {
        public T226_iPad_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T226. Rework - ACD-10294")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ColorPlusItem(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T226_TabletEmulator_VerifyColorPlusItem : T226_DesktopBase
    {
        public T226_TabletEmulator_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T226. Rework - ACD-10294")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ColorPlusItem(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T460_iPhone_VerifyColorPlusItem : T460_MobileBase
	{
		public T460_iPhone_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void ColorPlusItem(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T460_Emulator_VerifyColorPlusItem : T460_MobileBase
	{
        public T460_Emulator_VerifyColorPlusItem(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void ColorPlusItem(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that an item qualifies to be a Color Plus item.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5508
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T226
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5508"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T226")]
	public abstract class T226_DesktopBase : T226_T460_Base
	{
		protected T226_DesktopBase(ITestOutputHelper output) : base(output) { }

    }


	/// <summary>
	/// Verify that an item qualifies to be a Color Plus item.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5086
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T460
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5086"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T460")]
	public abstract class T460_MobileBase : T226_T460_Base
	{
		protected T460_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            var shortSku = ProductActions.GetColorPlusSku;

            Assert.DatabaseObject(shortSku, "ProductActions.GetColorPlusSku()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            // Need to force hide the sticky header
            ProductDetail.ForceHideStickyHeader();

            ProductDetail.WaitForPdpToLoad();
            Browser.Wait.ForDisplayedElement(ProductDetailColorPlus.ColorPlusShadeOptionsLabel);

            Assert.Displayed(ProductDetailColorPlus.ColorPlusShadeOptionsLabel, "Shade Options label not displayed.");
            Assert.Displayed(ProductDetailColorPlus.ColorPlusBaseColorOptionsLabel, "Base Color options not displayed.");
        }
	}


	public abstract class T226_T460_Base : ProductDetailTestsBase
    {
        protected T226_T460_Base(ITestOutputHelper output) : base(output) { }
        
        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetColorPlusSku;

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            Assert.DatabaseObject(shortSku, "ProductActions.GetColorPlusSku()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            // Need to force hide the sticky header
            ProductDetail.ForceHideStickyHeader();

            ProductDetail.WaitForPdpToLoad();
            Browser.Wait.ForDisplayedElement(ProductDetailColorPlus.ColorPlusShadeOptionsLabel);

            Assert.Displayed(ProductDetailColorPlus.ColorPlusShadeOptionsLabel, "Shade Options label not displayed.");
            Assert.Displayed(ProductDetailColorPlus.ColorPlusBaseColorOptionsLabel, "Base Color options not displayed.");
            Assert.Displayed(ProductDetailColorPlus.ViewAllColorsLink, "View all colors link not displayed.");

            ProductDetailColorPlus.ViewAllColorsLink.Click();

            Assert.Displayed(ProductDetailColorPlus.ColorPlusAllBaseColorsSection, "All Base Color accordion not displayed.");
            Assert.Displayed(ProductDetail.BrandLogo, "Color Plus Logo Image not displayed.");

            // Bring element that is above the desired link into view because the sticky dropdown blocks the desired link from being clicked
            Browser.MouseOverOnElement(ProductDetailMcp.PdpMoreYouMayLikeElement);

            if (ProductDetail.IsReplacementPartLinkVisible == false)
            {
                var colorPlusLink =  ProductDetail.GetAllColorPlusElement.GetAttribute("href");

                Assert.Equals(Urls.ColorPlusPageUrl, colorPlusLink, "Color Plus Link Is Not Correct");
            }
            else
            {
                var replacementPartLinkText = ProductDetail.ReplacementPartLink.Text;

                if (replacementPartLinkText.Contains(shortSku))
                {
                    Assert.Displayed(ProductDetail.ReplacementPartLink, "Replacement Part Link not displayed.");
                    ProductDetail.ReplacementPartLink.Click();

                    Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.ReplacementPartSkuXpath));

                    Assert.True(ProductDetail.IsReplacementPartModalVisible, "Replacement Part modal is not displayed");
                }
                else
                {
                    Assert.True(ProductDetail.IsShopAllLinkVisible, "Shop All link should not be displayed.");
                }
            }
        }
    }
}
