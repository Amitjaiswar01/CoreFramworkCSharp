using Xunit;
using Xunit.Abstractions;
using System;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T299_T499_VerifyUserCanEditAccountInfo
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T499_iPhone_VerifyUserCanEditAndSaveAccountInfo : T499_MobileBase
    {
        public T499_iPhone_VerifyUserCanEditAndSaveAccountInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanEditAndSaveAccountInfo(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T499_Emulator_VerifyUserCanEditAndSaveAccountInfo : T499_MobileBase
    {
        public T499_Emulator_VerifyUserCanEditAndSaveAccountInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanEditAndSaveAccountInfo(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can edit their account info and save it.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9902
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T499
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9902"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T499")]
    public abstract class T499_MobileBase : TestsBaseMobile
    {
        protected T499_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the 'Manage Account' info page: https://www.lampsplus.com/account/profile/.
            InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
            Assert.True(ManageAccount.IsCurrentPage, "User is not on Manage Account page.");
            ManageAccount.ResetAccountPhoneNumber();

            /*Act:
            Click the 'Edit' link in the 'Your Information' section.
            Once the account information overlay appears, edit the Name and Phone.
            Click the 'Save' button.
             */
            ManageAccount.OpenYourInformationModal();

            var previousPhoneNumber = ManageAccount.GetProfilePhoneNumber();
            var firstName = "Noho";
            var lastName = "Dimittis";
            var phoneNumber = (Convert.ToInt64(previousPhoneNumber) + 1).ToString();

            ManageAccount.UpdateAccountProfile(firstName, lastName, phoneNumber, previousPhoneNumber);

            //Assert: The newly provided information is saved and appears in the 'Your Information' section.
            Assert.Equals($"{firstName} {lastName}".Trim().ToLower(), ManageAccount.GetAccountProfileFullName(), $"Name does not match. ( {ManageAccount.GetAccountProfileFullName()} )");
            Assert.Equals(phoneNumber, ManageAccount.GetProfilePhoneNumber(), $"Phone number does not match. /  { ManageAccount.GetProfilePhoneNumber() }");

            //Data Cleanup
            ManageAccount.ResetAccountPhoneNumber();
        }
    }
}
