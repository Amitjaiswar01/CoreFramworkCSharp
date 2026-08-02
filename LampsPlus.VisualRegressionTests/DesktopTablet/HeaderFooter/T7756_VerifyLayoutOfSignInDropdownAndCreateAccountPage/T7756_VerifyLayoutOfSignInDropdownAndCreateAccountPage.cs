using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.HeaderFooter.T7756_VerifyLayoutOfSignInDropdownAndCreateAccountPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7756_Windows_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk : T7756_DesktopBase
    {
        public T7756_Windows_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyLayoutOfSignInDropDownAndCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7756_Mac_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk : T7756_DesktopBase
    {
        public T7756_Mac_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_UNSI)]
        public void VerifyLayoutOfSignInDropDownAndCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7756_iPad_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk : T7756_DesktopBase
    {
        public T7756_iPad_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI)]
        public void VerifyLayoutOfSignInDropDownAndCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7756_TabletEmulator_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk : T7756_DesktopBase
    {
        public T7756_TabletEmulator_VerifyLayoutOfTheSignInDropdownAndCreateAccountPageForKioskModeForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void VerifyLayoutOfSignInDropDownAndCreateAccountPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sign In dropdown and Create Account page for Kiosk mode.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9801
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7756
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9801"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7756")]
    public abstract class T7756_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7756_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is signed into Kiosk mode and on the Home page.
            InitializeVisualTest(config);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act: Hover over the Sign In link in the header.
            HeaderFooter.OpenSignInMenu();

            //Act: Capture a screenshot of the visible page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Navigate to the Professional Create Account page.
            Browser.Navigate(Urls.ProfessionalsPageUrl);
            Assert.True(CreateAccount.IsProfessionalCreateAccountPageLoaded(), "User is not on the Professional Create Account page.");

            //Act: Capture a screenshot of the visible page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
