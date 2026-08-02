using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using OpenQA.Selenium;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7289_Windows_VerifyLayoutOfHousingOptionsItem : T7289_DesktopBase
    {
        public T7289_Windows_VerifyLayoutOfHousingOptionsItem(ITestOutputHelper output, T7289_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfHousingOptionsItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7289_Mac_VerifyLayoutOfHousingOptionsItem : T7289_DesktopBase
    {
        public T7289_Mac_VerifyLayoutOfHousingOptionsItem(ITestOutputHelper output, T7289_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfHousingOptionsItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7289_iPad_VerifyLayoutOfHousingOptionsItem : T7289_DesktopBase
    {
        public T7289_iPad_VerifyLayoutOfHousingOptionsItem(ITestOutputHelper output, T7289_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfHousingOptionsItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7289_TabletEmulator_VerifyLayoutOfHousingOptionsItem : T7289_DesktopBase
    {
        public T7289_TabletEmulator_VerifyLayoutOfHousingOptionsItem(ITestOutputHelper output, T7289_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfHousingOptionsItem(string config) => Validate(Validate, config);
    }


    public class T7289_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7289_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatHasHousingOptions;
        }
    }


    /// <summary>
    /// Verify the layout for an item with Housing Options.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7387
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7289
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7387"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7289")]
    public abstract class T7289_DesktopBase : VisualTestsBase, IClassFixture<T7289_SharedSku_Fixture>
    {
        protected readonly T7289_SharedSku_Fixture Fixture;

        protected T7289_DesktopBase(ITestOutputHelper output, T7289_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
      
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasHousingOptions()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.XPath(GlobalLocators.PdAddToCartXpath));

            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper});
        }
    }
}