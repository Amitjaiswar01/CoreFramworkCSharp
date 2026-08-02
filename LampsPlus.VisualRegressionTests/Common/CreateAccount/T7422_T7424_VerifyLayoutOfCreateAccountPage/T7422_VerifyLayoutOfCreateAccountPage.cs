using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CreateAccount.T7422_T7424_VerifyLayoutOfCreateAccountPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7422_Windows_VerifyLayoutOfTheCreateAccountPage : T7422_DesktopBase
    {
        public T7422_Windows_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7422_Mac_VerifyLayoutOfTheCreateAccountPage : T7422_DesktopBase
    {
        public T7422_Mac_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7422_iPad_VerifyLayoutOfTheCreateAccountPage : T7422_DesktopBase
    {
        public T7422_iPad_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7422_TabletEmulator_VerifyLayoutOfTheCreateAccountPage : T7422_DesktopBase
    {
        public T7422_TabletEmulator_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Create Account page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9779
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7422
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9779"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7422")]
    public abstract class T7422_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7422_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: User is on the Create Account page: https://www.lampsplus.com/account/create/
            InitializeVisualTest(config);
            CreateAccount.Navigate();
            Assert.True(CreateAccount.IsCurrentPage, "Current page is not Create Account page");

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            /*Act:
            Fill out the Create a Lamps Plus Account form.
            Click on the CREATE ACCOUNT button.
             */
            var account = new Account();
            CreateAccount.AddEmailAndPasswordToForm(account);

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}
