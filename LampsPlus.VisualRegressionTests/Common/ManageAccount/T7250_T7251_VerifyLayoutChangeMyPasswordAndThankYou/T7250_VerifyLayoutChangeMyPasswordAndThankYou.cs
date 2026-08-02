using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7250_T7251_VerifyLayoutChangeMyPasswordAndThankYou
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7250_Windows_VerifyLayoutChangeMyPasswordAndThankYou : T7250_DesktopBase
    {
        public T7250_Windows_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7250_Mac_VerifyLayoutChangeMyPasswordAndThankYou : T7250_DesktopBase
    {
        public T7250_Mac_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7250_iPad_VerifyLayoutChangeMyPasswordAndThankYou : T7250_DesktopBase
    {
        public T7250_iPad_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7250_TabletEmulator_VerifyLayoutChangeMyPasswordAndThankYou : T7250_DesktopBase
    {
        public T7250_TabletEmulator_VerifyLayoutChangeMyPasswordAndThankYou(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutChangePasswordAndThankYou(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout Change My Password And ThankYou Message
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9777
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7250
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9777"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7250")]
    public abstract class T7250_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7250_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

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

            if (isAccountSignedIn)
            {
                try
                {
                    /*Act:
                    Navigate to Manage Account Page
                    Click on the Change Password Link 
                    Capture the screenshot of the modal
                    */
                    HeaderFooter.NavigateToManageAccount();
                    ManageAccount.NavigateToChangePasswordLink();
                    ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

                    /*Act:
                    Change the original password with the new password
                    Capture screenshot of Thank You modal
                    Close Thank You Modal
                    */
                    ManageAccount.SetNewPassword(newPassword);
                    ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
                    ManageAccount.CloseChangePasswordThankYouModal();

                    //Change the password back to the original.
                    ManageAccount.NavigateToChangePasswordLink();
                    ManageAccount.SetOriginalPassword(originalPassword);
                    HeaderFooter.SignOut();
                }

                //Act: If any step fails in above execution, Change the password back to the original.
                catch (Exception ex)
                {
                    Log.Message($"Changing password failed: {ex.Message}");

                    bool checkModal = Modal.IsModalWindowInitialized();
                    if (checkModal)
                    {
                        Modal.CloseLpModal();
                    }

                    ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                    HeaderFooter.SignOut();
                }

                //Assert the user is able to access the account with the original password
                finally
                {
                    var isAccountSignedInAfterOriginalPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                    Assert.True(isAccountSignedInAfterOriginalPasswordChange, "Account was not able to sign in with the original password ");
                    HeaderFooter.SignOut();
                }
            }
            //Changing password back to original if sign in failed for dedicated account
            else
            {
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                HeaderFooter.SignOut();
                var isAccountSignedInAfterPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterPasswordChange, "Account was not able to sign in with the original password ");
                HeaderFooter.SignOut();
            }
        }
    }
}
