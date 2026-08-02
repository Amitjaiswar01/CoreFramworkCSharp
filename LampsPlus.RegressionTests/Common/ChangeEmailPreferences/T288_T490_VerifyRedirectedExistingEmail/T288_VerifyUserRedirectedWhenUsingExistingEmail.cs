using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T288_T490_VerifyRedirectedExistingEmail
{
    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T288_Windows_VerifyRedirectedExistingEmail : T288_DesktopBase
    {
        public T288_Windows_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T288_Mac_VerifyRedirectedExistingEmail : T288_DesktopBase
    {
        public T288_Mac_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T288_iPad_VerifyRedirectedExistingEmail : T288_DesktopBase
    {
        public T288_iPad_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T288_TabletEmulator_VerifyRedirectedExistingEmail : T288_DesktopBase
    {
        public T288_TabletEmulator_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user is re-directed to Sign In page when using an existing email and the Change Email Preferences page after signing in.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5191
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T288
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5191"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T288")]
    public abstract class T288_DesktopBase : TestsBaseDesktop
    {
        protected T288_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            InitializeFunctionalTest(config);

            //Go to the subscribe page: https://www.lampsplus.com/account/email/?isFromFooter=true
            var expectedLandingPage = Email.PageUrl;
            var browser = Email.Navigate();
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            /*In the Change Your E-mail Preferences section, enter the email address for the consumer account in the Email Address field.
            Click the CHANGE PREFERENCES button.*/
            var expectedUserAccount = LampsPlusAccounts.CustomerLoginAccount;
            Email.GoToEmailPreferencesByEmail(expectedUserAccount.UserName);

            /* Assert:
             *1. User is redirected to Sign In page. Verify that the message 'For your security, please sign in to continue.' is displayed
             *2. The email address should be pre-filled and the password field should be blank.
             *3. No part of the user's email should exist as part of the URL.*/
            var expectedSignInMessage = "For your security, please sign in to continue.";
            Assert.Displayed(SignIn.GetSignInMessage(), "The Sign In Message is not displayed.");
            Assert.Equals(expectedSignInMessage, SignIn.GetSignInMessageText(), $"Expected the message: {expectedSignInMessage} but found {SignIn.GetSignInMessageText()}.");
            Assert.Equals(expectedUserAccount.UserName, SignIn.GetEmailFieldValue(), $"Expected user {expectedUserAccount.UserName} but found {SignIn.GetEmailFieldValue()}.");
            Assert.False(Email.VerifyUserEmailNotDisplayedInUrl(Browser.PageUrl, expectedUserAccount.UserName), "Personal email address displayed in page url.");

            /* On the Sign In page,
             * Enter the password in the Password field.
             * Click the SIGN IN button.*/
            SignIn.SignInWithPrefilledEmail(expectedUserAccount.Password);

            //Assert User is directed to the 'Change Email' Preferences page (URL includes https://www.lampsplus.com/account/email-preferences/ ).
            Assert.True(Email.IsEmailPreferencesPage, "Current page is not 'Change Email' Preferences page page");

            /* Assert:
             * The heading of the page reads as: Change Email Preferences for <EMAIL ADDRESS>.
             * No part of the user's email should exist as part of the URL.*/
            Assert.StringContains(Browser.PageUrl, Urls.ChangeEmailPreferencePageUrl, "Did not redirect to email preferences page after signing in.");
            Assert.StringContains(ManageAccount.GetEmailPreferenceHeaderText(), expectedUserAccount.UserName, "Email Preferences page header doesn't contain users email address.");
            Assert.False(Email.VerifyUserEmailNotDisplayedInUrl(Browser.PageUrl, expectedUserAccount.UserName), "Personal email address displayed in page url.");
        }
    }
}