using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7250_T7251_VerifyLayoutChangeMyPasswordAndThankYou
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7251_iPhone_VerifyLayoutChangeMyPasswordAndThankYou : T7251_MobileBase
    {
        public T7251_iPhone_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7251_Android_VerifyLayoutChangeMyPasswordAndThankYou : T7251_MobileBase
    {
        public T7251_Android_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7251_Emulator_VerifyLayoutChangeMyPasswordAndThankYou : T7251_MobileBase
    {
        public T7251_Emulator_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout Change My Password And ThankYou Message
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9777
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7251
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9777"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7251")]
    public abstract class T7251_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7251_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            /*Arrange: 
            Get the Username, new password, original password
            Sign in using the Username & original password
            */
            InitializeVisualTest(config);

            // We use the dedicated account for changing password scenario so we don't need to release the user account.
            var newPassword = LampsPlusAccounts.CustomerChangePasswordLoginAccount.TempPassword;
            var originalPassword = LampsPlusAccounts.CustomerChangePasswordLoginAccount.OriginalPassword;
            var accountUserName = LampsPlusAccounts.CustomerChangePasswordLoginAccount.UserName;

            var isAccountSignedIn = SignIn.SignIn(accountUserName, originalPassword);

            /*Act:
            Navigate to Manage Account Page
            Click on the Change Password Link 
            Capture the screenshot of the modal
            */
            if (isAccountSignedIn)
            {
                HeaderFooter.NavigateToManageAccount();
                ManageAccount.NavigateToChangePasswordLink();
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

                //Change the original password with the new password
                ManageAccount.SetNewPassword(newPassword);
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

                //Change the password back to the original.
                ManageAccount.SetOriginalPassword(originalPassword);
                HeaderFooter.SignOut();
                var isAccountSignedInAfterOriginalPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterOriginalPasswordChange, "Account was not able to sign in with the original password ");
            }
            else
            {
                //Assert the user is able to access the account with the original password
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                HeaderFooter.SignOut();
                var isAccountSignedInAfterPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterPasswordChange, "Account was not able to sign in with the original password ");
                HeaderFooter.SignOut();
            }
        }
    }
}
