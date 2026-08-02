using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.External.Nada;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CreateAccount.T294_T495_VerifyUserCanCreateNewAccount
{
    public class T495_VerifyUserCanCreateNewAccount
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
        public class T495_IPhone_VerifyUserCanCreateNewAccount : T495_MobileBase
        {
            public T495_IPhone_VerifyUserCanCreateNewAccount(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T495. Rework - ACD-10643")]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void VerifyUserCanCreateNewAccount(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
        public class T495_Emulator_VerifyUserCanCreateNewAccount : T495_MobileBase
        {
            public T495_Emulator_VerifyUserCanCreateNewAccount(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void VerifyUserCanCreateNewAccount(string config) => Validate(config);
        }


        /// <summary>
        /// Verify that creating a regular new account, the information gets added into the database.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9895
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T495
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9895"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T495")]
        public abstract class T495_MobileBase : TestsBaseMobile
        {
            protected T495_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User clicked on the 'Create Account' link in the header area.
                */
                InitializeFunctionalTest(config, Urls.CreateAccountPageUrl);
                Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
                Assert.True(CreateAccount.IsCurrentPage, "Current page is not the Create Account page");
                CreateAccount.ClearEmailAndPasswordFields();

                /*Act
                Fill out the 'Create Account form completely.
                Click the 'Create Account' button'
                */
                var account = new Account();
                CreateAccount.AddEmailAndPasswordToForm(account);

                /*Assert
                Verify there is a record for the new account in all three tables: UserProfile.dbo.tblUserProfile, UserProfile.dbo.aspnet_Membership, UserProfile.dbo.aspnet_Users
                Verify the IsApproved value is false (0) in UserProfile.dbo.aspnet_Membership
                Verify the user receives an account confirmation email.
                */
                var email = NadaClient.GetAccountVerificationEmail(account.EmailAddress);
                var newAccount = AccountActions.GetUserByEmail(account.EmailAddress);
                var isConfirmationEmailReceived = CreateAccount.IsAccountVerificationEmailReceived(email, account.EmailAddress);

                Assert.Equals(account.EmailAddress, newAccount.Email, "Account not properly created in all three required database tables.");
                Assert.Equals(0, newAccount.IsApproved, "Account created successfully in aspnet_Membership table and is not activated yet(IsApproved=0)");
                Assert.Equals(true, isConfirmationEmailReceived, "Confirmation email received.");
            }
        }
    }
}
