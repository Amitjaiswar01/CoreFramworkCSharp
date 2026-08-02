using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.RoomViewer.T7980_T7979_VerifyTheLayoutOfEmptyRoomViewerPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7979_iPhone_VerifyLayoutOfEmptyRoom : T7979_MobileBase
    {
        public T7979_iPhone_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7979_AndroidPhone_VerifyLayoutOfEmptyRoom : T7979_MobileBase
    {
        public T7979_AndroidPhone_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7979_Emulator_VerifyLayoutOfEmptyRoom : T7979_MobileBase
    {
        public T7979_Emulator_VerifyLayoutOfEmptyRoom(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfEmptyRoom(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the  Layout of Empty Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10791
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7979
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10791"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7979")]
    public abstract class T7979_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7979_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

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
