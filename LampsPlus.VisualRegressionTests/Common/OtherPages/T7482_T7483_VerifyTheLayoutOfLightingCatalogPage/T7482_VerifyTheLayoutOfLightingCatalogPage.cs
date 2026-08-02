using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages.T7482_T7483_VerifyTheLayoutOfLightingCatalogPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7482_Windows_VerifyTheLayoutOfLightingCatalogPage : T7482_DesktopBase
    {
        public T7482_Windows_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7482_Mac_VerifyTheLayoutOfLightingCatalogPage : T7482_DesktopBase
    {
        public T7482_Mac_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7482_iPad_VerifyTheLayoutOfLightingCatalogPage : T7482_DesktopBase
    {
        public T7482_iPad_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7482_TabletEmulator_VerifyTheLayoutOfLightingCatalogPage : T7482_DesktopBase
    {
        public T7482_TabletEmulator_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Lighting Catalog Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7482
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7482")]
    public abstract class T7482_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7482_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            /*Arrange:
            User is on the homepage(https://www.lampsplus.com/)
            Scroll down to footer  
            */
            InitializeVisualTest(config, Urls.HomePageUrl);
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            //Act: On footer, click on catalog link https://www.lampsplus.com/lighting-catalog/
            HeaderFooter.LoadLightingCatalog();

            //Act: Once the page loads, Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true, true);
        }
    }
}
