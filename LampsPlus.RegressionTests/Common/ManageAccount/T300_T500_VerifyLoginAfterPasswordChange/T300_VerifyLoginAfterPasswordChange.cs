using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T300_T500_VerifyLoginAfterPasswordChange
{
    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T300_Windows_VerifyLoginAfterPasswordChange : T300_DesktopBase
    {
        public T300_Windows_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T300_Mac_VerifyLoginAfterPasswordChange : T300_DesktopBase
    {
        public T300_Mac_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T300. Rework - ACD-10769")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T300_iPad_VerifyLoginAfterPasswordChange : T300_DesktopBase
    {
        public T300_iPad_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T300_TabletEmulator_VerifyLoginAfterPasswordChange : T300_DesktopBase
    {
        public T300_TabletEmulator_VerifyLoginAfterPasswordChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can log in to their account after updating their password.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5067
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T300
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5067"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T300")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T300_DesktopBase : TestsBaseDesktop
    {
        protected T300_DesktopBase(ITestOutputHelper output) : base(output) { }

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
                Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
                var isAccountSignedInAfterPasswordChange = SignIn.SignIn(accountUserName, newPassword);
                Assert.True(isAccountSignedInAfterPasswordChange, "Account was not able to sign in with the new password ");

                //Change the password back to the original.
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                HeaderFooter.SignOut();
                Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
                var isAccountSignedInAfterOriginalPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterOriginalPasswordChange, "Account was not able to sign in with the original password ");
                HeaderFooter.SignOut();
            }
            else
            {
                //Assert the user is able to access the account with the original password
                ManageAccountWorkflow.ChangeAccountPassword(accountUserName, newPassword, originalPassword);
                HeaderFooter.SignOut();
                Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
                var isAccountSignedInAfterPasswordChange = SignIn.SignIn(accountUserName, originalPassword);
                Assert.True(isAccountSignedInAfterPasswordChange, "Account was not able to sign in with the original password ");
                HeaderFooter.SignOut();
            }
        }
    }
}