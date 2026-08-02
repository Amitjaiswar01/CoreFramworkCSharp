using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7985_T7986_VerifyTheLayoutOfInHomeConsultationsPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7986_iPhone_VerifyLayoutOfInHomeConsultationsPage : T7986_MobileBase
    {
        public T7986_iPhone_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7986_AndroidPhone_VerifyLayoutOfInHomeConsultationsPage : T7986_MobileBase
    {
        public T7986_AndroidPhone_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7986_Emulator_VerifyLayoutOfInHomeConsultationsPage : T7986_MobileBase
    {
        public T7986_Emulator_VerifyLayoutOfInHomeConsultationsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutofInHomeConsultations(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of In-Home Consultations Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10810
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7986
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10810"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7986")]
    public abstract class T7986_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7986_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            // Arrange: Navigate to the LP Site
            InitializeVisualTest(config);

            // Act : Navigate to the  In-Home Consultations Page
            Browser.Navigate(Urls.LightingDesignServicesPageUrl);

            // Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            // Act : Open the Make an Appointment modal
            ProductDetail.OpenMakeAnAppointmentModal();

            // Act: Capture a screenshot of the visible area screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
