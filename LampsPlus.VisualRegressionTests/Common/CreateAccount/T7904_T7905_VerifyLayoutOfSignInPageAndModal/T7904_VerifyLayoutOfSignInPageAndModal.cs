using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CreateAccount.T7904_T7905_VerifyLayoutOfSignInPageAndModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7904_Windows_VerifyLayoutOfSignInPageAndModal : T7904_DesktopBase
    {
        public T7904_Windows_VerifyLayoutOfSignInPageAndModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSignInPageAndModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7904_Windows_Kiosk_VerifyLayoutOfSignInPageAndModal : T7904_DesktopBase
    {
        public T7904_Windows_Kiosk_VerifyLayoutOfSignInPageAndModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void LayoutOfSignInPageAndModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7904_Mac_VerifyLayoutOfSignInPageAndModal : T7904_DesktopBase
    {
        public T7904_Mac_VerifyLayoutOfSignInPageAndModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSignInPageAndModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7904_iPad_VerifyLayoutOfSignInPageAndModal : T7904_DesktopBase
    {
        public T7904_iPad_VerifyLayoutOfSignInPageAndModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSignInPageAndModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7904_TabletEmulator_VerifyLayoutOfSignInPageAndModal : T7904_DesktopBase
    {
        public T7904_TabletEmulator_VerifyLayoutOfSignInPageAndModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSignInPageAndModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout Of SignIn Page and Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10444
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7904
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10444"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7904")]
    public abstract class T7904_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7904_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            /*Act:
             On the Homepage, Hover the mouse over Sign In
             Click on Sign In
             Capture a screenshot of the Sign In Modal
            */
            SignIn.OpenSignInModal();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal(), true);

            /*Act:
             Navigate to sign in page
             Capture a screenshot of the entire page
             */
            SignIn.Navigate();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            /*Act:
             Navigate to Pros Page
             Click on the Sign In link
             Capture a screenshot of the entire page.
            */
            SignIn.NavigateToProSignInPage();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}