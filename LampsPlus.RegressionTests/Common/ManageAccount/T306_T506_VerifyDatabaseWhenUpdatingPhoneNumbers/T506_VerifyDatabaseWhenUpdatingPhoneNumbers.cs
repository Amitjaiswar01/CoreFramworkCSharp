using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T306_T506_VerifyDatabaseWhenUpdatingPhoneNumbers
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T506_iPhone_VerifyDatabaseWhenUpdatingPhoneNumbers : T506_MobileBase
    {
        public T506_iPhone_VerifyDatabaseWhenUpdatingPhoneNumbers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void DatabaseWhenUpdatingPhoneNumbers(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T506_Emulator_VerifyDatabaseWhenUpdatingPhoneNumbers : T506_MobileBase
    {
        public T506_Emulator_VerifyDatabaseWhenUpdatingPhoneNumbers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void DatabaseWhenUpdatingPhoneNumbers(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the correct columns get updated in correct database table when updating phone numbers.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9904
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T506
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9904"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T506")]
    public abstract class T506_MobileBase : TestsBaseMobile
    {
        protected T506_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page: https://www.lampsplus.com/account/profile/.
            InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
            Assert.True(ManageAccount.IsCurrentPage, "User is not on the Manage Account page.");

            /*Act:
            Click the 'Edit' link in the 'Your Information' section.
            Enter a phone number for Phone, Fax, and Cell and save the record.
            */
            var phoneNumber = "3233332233";
            var faxNumber = "8181234567";
            var cellPhoneNumber = "5141234567";

            ManageAccount.EditAccountContactNumbers(phoneNumber, faxNumber, cellPhoneNumber);

            //Assert: The phone numbers from the tblUserProfile table associated with the email are updated.
            var userName = TestSetup.AccountConfig.UserName;
            var userPhoneInfo = ProductActions.GetUserPhoneInfo(userName);

            Assert.DatabaseObject(userPhoneInfo, $"ProductActions.GetUserPhoneInfo(\"${userName}\"");
            Assert.Equals(cellPhoneNumber, userPhoneInfo.CellPhoneNumber, "Cell phone number do not match.");
            Assert.Equals(faxNumber, userPhoneInfo.Fax, "Fax number do not match.");
            Assert.Equals(phoneNumber, userPhoneInfo.PhoneNumber, "Phone number do not match.");
        }
    }
}
