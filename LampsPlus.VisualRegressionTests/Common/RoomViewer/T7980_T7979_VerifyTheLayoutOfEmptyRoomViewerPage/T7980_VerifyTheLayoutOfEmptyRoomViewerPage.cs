using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.RoomViewer.T7980_T7979_VerifyTheLayoutOfEmptyRoomViewerPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7980_Windows_VerifyLayoutOfEmptyRoom : T7980_DesktopBase
    {
        public T7980_Windows_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7980_Windows_VerifyLayoutOfEmptyRoomKiosk : T7980_DesktopBase
    {
        public T7980_Windows_VerifyLayoutOfEmptyRoomKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7980_Mac_VerifyLayoutOfEmptyRoom : T7980_DesktopBase
    {
        public T7980_Mac_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7980_iPad_VerifyLayoutOfEmptyRoom : T7980_DesktopBase
    {
        public T7980_iPad_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7980_TabletEmulator_VerifyLayoutOfEmptyRoom : T7980_DesktopBase
    {
        public T7980_TabletEmulator_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Empty Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10791
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7980
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10791"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7980")]
    public abstract class T7980_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7980_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            // Arrange: Navigate to the LP Site
            InitializeVisualTest(config);

            // Act : Navigate  to the Viewer Page
            Browser.Navigate(Urls.AugmentedRealityUrl);

            // Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
