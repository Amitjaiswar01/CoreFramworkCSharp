using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7985_T7986_VerifyTheLayoutOfInHomeConsultationsPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7985_Windows_VerifyLayoutOfInHomeConsultationsPage : T7985_DesktopBase
    {
        public T7985_Windows_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7985_Mac_VerifyLayoutOfInHomeConsultationsPage : T7985_DesktopBase
    {
        public T7985_Mac_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7985_iPad_VerifyLayoutOfInHomeConsultationsPage : T7985_DesktopBase
    {
        public T7985_iPad_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7985_TabletEmulator_VerifyLayoutOfInHomeConsultationsPage : T7985_DesktopBase
    {
        public T7985_TabletEmulator_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of In-Home Consultations Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10810
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7985
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10810"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7985")]
    public abstract class T7985_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7985_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            // Arrange: Navigate to the LP Site
            InitializeVisualTest(config);

            // Act : Navigate  to the In-Home Consultations Page
            Browser.Navigate(Urls.LightingDesignServicesPageUrl);

            // Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            // Act : Open the Make an Appointment modal
            ProductDetail.OpenMakeAnAppointmentModal();

            // Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
