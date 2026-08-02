using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T292_T493_VerifyThatProvidingEmailShowsThankYou
{
    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T292_Windows_VerifyThatProvidingEmailShowsThankYou : T292_DesktopBase
    {
        public T292_Windows_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T292_Mac_VerifyThatProvidingEmailShowsThankYou : T292_DesktopBase
    {
        public T292_Mac_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T292_iPad_VerifyThatProvidingEmailShowsThankYou : T292_DesktopBase
    {
        public T292_iPad_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T292_TabletEmulator_VerifyThatProvidingEmailShowsThankYou : T292_DesktopBase
    {
        public T292_TabletEmulator_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that providing an email in the Change Email Preferences section shows thank you message
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9939
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T292
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9939"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T292")]
    public abstract class T292_DesktopBase : TestsBaseDesktop
    {
        protected T292_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            // Arrange : Go to footer, Enter email in Stay Connected field, click on Arrow button and Fill Subscribe Now Form 
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            var account= new Account();
            HeaderFooter.NavigateToEmailPageFromFooter(account.EmailAddress);
            Assert.True(Email.IsCurrentPage, "User is Not on Email Page");
            Email.FillOutSubscribeNow(account);

            // Act : Enter Email address and click on Update Preferences
            Email.GoToEmailPreferencesByEmail(account.EmailAddress);

            // Assert : Verify User is on Email Preference Page
            Assert.True(Email.IsEmailPreferencesPage, "User is Not on Email Preference Page");

            // Assert : Verify Email is Displayed at the Header
            Assert.StringContains(account.EmailAddress, Email.GetEmailFromHeader(), "Email Address is Incorrect");

            // Act : Update preferences
            Email.UpdateEmailPreferences();

            // Assert : Verify Thank you Message and Continue Shopping is displayed correctly 
            Assert.Equals( "Thank You! Your preference changes have been successfully updated and saved.", Email.GetSaveSettingsThankYouMessage(), "Thank You Message is Incorrect");
            Assert.Equals("Continue Shopping", Email.GetContinueShoppingMessage(), "Continue Shopping Message is Incorrect");
        }
    }
}