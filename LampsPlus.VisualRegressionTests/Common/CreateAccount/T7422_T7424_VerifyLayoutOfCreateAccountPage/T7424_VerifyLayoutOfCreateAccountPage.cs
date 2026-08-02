using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.CreateAccount.T7422_T7424_VerifyLayoutOfCreateAccountPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7424_iPhone_VerifyLayoutOfTheCreateAccountPage : T7424_MobileBase
    {
        public T7424_iPhone_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7424_Android_VerifyLayoutOfTheCreateAccountPage : T7424_MobileBase
    {
        public T7424_Android_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7424_Emulator_VerifyLayoutOfTheCreateAccountPage : T7424_MobileBase
    {
        public T7424_Emulator_VerifyLayoutOfTheCreateAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfTheCreateAccountPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Create Account page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9779
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7424
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9779"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7424")]
    public abstract class T7424_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7424_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
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
