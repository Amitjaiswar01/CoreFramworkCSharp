using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CreateAccount.T7904_T7905_VerifyLayoutOfSignInPageAndModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7905_iPhone_VerifyLayoutOfTheCreateAccountPage : T7905_MobileBase
    {
        public T7905_iPhone_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7905_Android_VerifyLayoutOfTheCreateAccountPage : T7905_MobileBase
    {
        public T7905_Android_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7905_Emulator_VerifyLayoutOfTheCreateAccountPage : T7905_MobileBase
    {
        public T7905_Emulator_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout Of SignIn Page and Modal.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10444
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7905
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10444"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7905")]
    public abstract class T7905_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7905_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config,Urls.HomePageUrl);

            /*Act:
             Open the hamburger menu
             Click on Sign In Button
             Capture a screenshot of the entire page
            */
            HeaderFooter.OpenLpMenu();
            HeaderFooter.SelectSignInButton();
            Assert.True(SignIn.IsCurrentPage, "User is not at the sign in page");

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

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
            Assert.True(SignIn.IsCurrentPage, "User is not at the sign in page");

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}