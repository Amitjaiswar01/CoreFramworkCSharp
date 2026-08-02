using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HomePage.T7694_T7695_VerifyLayoutOfInstagramWidgetOnHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7694_Windows_VerifyLayoutOfInstagramWidgetOnHomepage : T7694_DesktopBase
    {
        public T7694_Windows_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7694_Mac_VerifyLayoutOfInstagramWidgetOnHomepage : T7694_DesktopBase
    {
        public T7694_Mac_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7694_iPad_VerifyLayoutOfInstagramWidgetOnHomepage : T7694_DesktopBase
    {
        public T7694_iPad_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7694_TabletEmulator_VerifyLayoutOfInstagramWidgetOnHomepage : T7694_DesktopBase
    {
        public T7694_TabletEmulator_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout Of Instagram Widget On LP Homepage
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9802
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7694
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9802"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7694")]
    public abstract class T7694_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7694_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            /* Act:
            Scrolling the page upto Pixlee Modal section
            Click on the first pixel image
            */
            Home.OpenInstagramWidget();

            // Act: Capture of the screenshot of the Instagram Overlay
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Home.GetInstagramOverlayContent());
        }
    }
}