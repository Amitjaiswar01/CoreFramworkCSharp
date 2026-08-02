using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T305_T505_VerifyUserCanAccessChangeEmailPreferencesPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T305_Windows_VerifyChangeEmailPreferenceModal : T305_DesktopBase
    {
        public T305_Windows_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T305_Mac_VerifyChangeEmailPreferenceModal : T305_DesktopBase
    {
        public T305_Mac_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T305_iPad_VerifyChangeEmailPreferenceModal : T305_DesktopBase
    {
        public T305_iPad_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T305_TabletEmulator_VerifyChangeEmailPreferenceModal : T305_DesktopBase
    {
        public T305_TabletEmulator_VerifyChangeEmailPreferenceModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyChangeEmailPreferenceModal(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can access the secured Change Email Preferences page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9901
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T305
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9901"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T305")]
    public abstract class T305_DesktopBase : TestsBaseDesktop
    {
        protected T305_DesktopBase(ITestOutputHelper output) : base(output) { }

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
            Modal.CloseLpModal();
            ManageAccount.OpenEmailPreferencesModal();
            ManageAccount.Unsubscribe();
            Modal.CloseLpModal();

            //Assert: Clicking 'Close' on the popup closes it.
            Assert.False(Modal.IsModalWindowInitialized(), "Modal still displaying on screen after closing");
        }
    }
}
