using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T305_T505_VerifyUserCanAccessChangeEmailPreferencesPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T505_iPhone_VerifyChangeEmailPreferenceModal : T505_MobileBase
    {
        public T505_iPhone_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T505_Emulator_VerifyChangeEmailPreferenceModal : T505_MobileBase
    {
        public T505_Emulator_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can access the secured Change Email Preferences page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9901
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T505
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9901"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T505")]
    public abstract class T505_MobileBase : TestsBaseMobile
    {
        protected T505_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the 'Manage Account' info page: https://www.lampsplus.com/account/profile/ 
            InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
            Assert.True(ManageAccount.IsCurrentPage, "User is not on Manage Account page.");

            /*Act:
            Under the Manage Account section, click on the 'Email Preferences' link.
            In the popup that is displayed, change one of the options and click 'Save'.
             */
            ManageAccount.OpenEmailPreferencesModal();
            ManageAccount.SelectNewOptionAndSave();

            //Assert: A Thank You message is displayed.
            Assert.True(ManageAccount.IsModalThankYouMessageVisible(), "Thank You message not displayed after clicking Save Settings");

            /*Act:
            Click the Unsubscribe button.
            Click 'Close' on the popup.
            */
            ManageAccount.Unsubscribe();
            ManageAccount.CloseEmailPreferencesModal();

            //Assert: The user is on the Manage Account Profile page.
            var browserUrl = Browser.PageUrl;
            Assert.Equals(Urls.ManageAccountPageUrl, browserUrl, "User is not on Manage Account Profile page.");
        }
    }
}
