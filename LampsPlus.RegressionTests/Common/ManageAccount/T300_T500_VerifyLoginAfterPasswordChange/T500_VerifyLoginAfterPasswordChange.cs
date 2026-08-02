using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T300_T500_VerifyLoginAfterPasswordChange
{
    //[Collection(LpTraits.BatchGroup.Mobile.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T500_iPhone_VerifyLoginAfterPasswordChange : MobileBase
    {
        public T500_iPhone_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T500_Emulator_VerifyLoginAfterPasswordChange : MobileBase
    {
        public T500_Emulator_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can log in to their account after updating their password.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5444
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T500
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5444"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T500")]
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class MobileBase : TestsBaseMobile
    {
        protected MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config, string.Empty, true, true);

            // We use the dedicated account for changing password scenario so we don't need to release the user account.
            var newPassword = LampsPlusAccounts.CustomerChangePasswordLoginAccount.TempPassword;
            var originalPassword = LampsPlusAccounts.CustomerChangePasswordLoginAccount.OriginalPassword;
            var accountUserName = LampsPlusAccounts.CustomerChangePasswordLoginAccount.UserName;

            //Verify if dedicated account can sign in
            var isAccountSignedIn = SignIn.SignIn(accountUserName, originalPassword);

            if (isAccountSignedIn)
            {
                //Assert the user is able to access the account with the new password
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, originalPassword, newPassword);
                HeaderFooter.SignOut();
                var isAccountSignedInAfterPasswordChange = SignIn.SignIn(accountUserName, newPassword);
                Assert.True(isAccountSignedInAfterPasswordChange, "Account was not able to sign in with the new password ");

                //Change the password back to the original.
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                HeaderFooter.SignOut();
                var isAccountSignedInAfterOriginalPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterOriginalPasswordChange, "Account was not able to sign in with the original password ");
                HeaderFooter.SignOut();
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
