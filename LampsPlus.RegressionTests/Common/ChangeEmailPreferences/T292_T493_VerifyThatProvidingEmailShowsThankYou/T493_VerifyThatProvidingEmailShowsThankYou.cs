using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T292_T493_VerifyThatProvidingEmailShowsThankYou
{
    //[Collection(LpTraits.BatchGroup.Mobile.ChangeEmailPreferences)]
    public class T493_iPhone_VerifyThatProvidingEmailShowsThankYou : T493_MobileBase
    {
        public T493_iPhone_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T493_Emulator_VerifyThatProvidingEmailShowsThankYou : T493_MobileBase
    {
        public T493_Emulator_VerifyThatProvidingEmailShowsThankYou(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ChangePreferencesThankYou(string config) => Validate(config);
    }


    // <summary>
    // Verify that providing an email in the Change Email Preferences section shows thank you message
    // Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9939
    // Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T493
    // </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9939"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T493")]
    public class T493_MobileBase : TestsBaseMobile
    {
        protected T493_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange : User has Navigated to Email Page.
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            Email.Navigate();

            // Act : Fill "Subscribe to Lamps Plus Email" form and tap on "Subscribe".
            var account = new Account();
            Email.FillOutSubscribeNow(account);

            // Act : Select "Preferences" tab, Enter the Email
            Email.GoToEmailPreferencesByEmail(account.EmailAddress);

            // Assert : Verify user is on the Email Preference Page
            Assert.True(Email.IsEmailPreferencesPage, "User is not on Email Preference Page");

            // Assert : Verify Email is Displayed at the Header
            Assert.StringContains(account.EmailAddress, Email.GetEmailFromHeader(), "Email Address is Incorrect");

            int maxIteration = 10; int iterationsCount = 0;
            bool emailPreferenceUpdateCheck = false;
            while (iterationsCount < maxIteration && !emailPreferenceUpdateCheck)
            {
                // Act : Update Email Preferences
                Email.UpdateEmailPreferences();

                try
                {
                    // Assert : Verify that the "Thank You" message is displayed correctly.
                    Assert.Equals("Your preference changes have been successfully updated and saved.", Email.GetSaveSettingsThankYouMessage(), "Thank You message is Incorrect");

                    emailPreferenceUpdateCheck = true;
                }
                catch
                {
                    emailPreferenceUpdateCheck = false;
                }
                iterationsCount++;
            }

            if (emailPreferenceUpdateCheck == false)
            {
                // Act : Update Email Preferences
                Email.UpdateEmailPreferences();

                // Assert : Verify that the "Thank You" message is displayed correctly.
                Assert.Equals("Your preference changes have been successfully updated and saved.", Email.GetSaveSettingsThankYouMessage(), "Thank You message is Incorrect");
            }
        }
    }
}
