using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7234_T7322_VerifyTheLayoutOfCategoryMenus
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7234_Windows_VerifyTheLayoutOfCategoryMenus : T7234_DesktopBase
    {
        public T7234_Windows_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7234_Mac_VerifyTheLayoutOfCategoryMenus : T7234_DesktopBase
    {
        public T7234_Mac_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7234_iPad_VerifyTheLayoutOfCategoryMenus : T7234_DesktopBase
    {
        public T7234_iPad_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7234_TabletEmulator_VerifyTheLayoutOfCategoryMenus : T7234_DesktopBase
    {
        public T7234_TabletEmulator_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Category menus appears correctly when the user interacts with them.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7424
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7234
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7424"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7234")]
    public abstract class T7234_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7234_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            /*Arrange: 
            User is on the Lamps Plus Homepage.
            */
            InitializeVisualTest(config, Urls.HomePageUrl);
            Browser.Wait.ForDomReady();

            /* Act:
            On the Homepage, hover over the Chandeliers menu and capture a screenshot of the menu element.
            */
            TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            foreach (var navElement in HeaderFooter.GetNavElements())
            {
                Browser.MouseOverOnElement(navElement);
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            }
        }
    }
}
