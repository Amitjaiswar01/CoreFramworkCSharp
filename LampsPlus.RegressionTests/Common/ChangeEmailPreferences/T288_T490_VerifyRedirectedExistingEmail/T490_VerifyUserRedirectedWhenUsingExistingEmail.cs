using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T288_T490_VerifyRedirectedExistingEmail
{
    //[Collection(LpTraits.BatchGroup.Mobile.ChangeEmailPreferences)]
    public class T490_iPhone_VerifyRedirectedExistingEmail : MobileBase
    {
        public T490_iPhone_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T490_Emulator_VerifyRedirectedExistingEmail : MobileBase
    {
        public T490_Emulator_VerifyRedirectedExistingEmail(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    // <summary>
    // Verify the user is re-directed to Sign In page when using an existing email and the Change Email Preferences page after signing in.
    // Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5430
    // Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T490
    // </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5430"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T490")]
    public class MobileBase : TestsBaseMobile
    {
        protected MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFunctionalTest(config);

            //Arrange : Go to the subscribe page: https://www.lampsplus.com/account/email/?isFromFooter=true
            var expectedLandingPage = Email.PageUrl;
            var browser = Email.Navigate();
            Assert.True(Email.IsCurrentPage, "Current page is not Subscribe page");
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            /* Act :
             On the Subscribe to Lamps Plus Email page, tap the Preferences tab at the top of the page.
             Enter the email address for the consumer account in the Email field.
             Tap the CHANGE PREFERENCES button.
            */
            var expectedUserAccount = LampsPlusAccounts.CustomerLoginAccount;
            Email.GoToEmailPreferencesByEmail(expectedUserAccount.UserName);

            /* Assert :
             User is redirected to Sign In page. Verify that the message 'For your security, please sign in to continue.' is displayed
             Verify that the email address should be pre-filled and the password field should be blank.
             Verify no part of the user's email should exist as part of the URL.
            */
            var expectedSignInMessage = "For your security, please sign in to continue.";
            Assert.Displayed(SignIn.GetSignInMessage(), "The Sign In Message is not displayed.");
            Assert.Equals(expectedSignInMessage, SignIn.GetSignInMessageText(), $"Expected the message: {expectedSignInMessage} but found {SignIn.GetSignInMessageText()}.");
            Assert.Equals(expectedUserAccount.UserName, SignIn.GetEmailFieldValue(), $"Expected user {expectedUserAccount.UserName} but found {SignIn.GetEmailFieldValue()}.");
            Assert.False(Email.VerifyUserEmailNotDisplayedInUrl(Browser.PageUrl, expectedUserAccount.UserName), "Personal email address displayed in page url.");

            /* Act : 
              On the Sign In page,
              Enter the password in the Password field.
              Click the SIGN IN button.
            */
            SignIn.SignInWithPrefilledEmail(expectedUserAccount.Password);

            //Assert User is directed to the 'Change Email' Preferences page (URL includes https://www.lampsplus.com/account/email-preferences/ ).
            Assert.True(Email.IsEmailPreferencesPage, "Current page is not 'Change Email' Preferences page");

            /* Assert :
             Verify that the heading of the page reads as: Change Email Preferences for <EMAIL ADDRESS>.
             Verify that no part of the user's email should exist as part of the URL.
            */
            Assert.StringContains(Browser.PageUrl, Urls.ChangeEmailPreferencePageUrl, "Did not redirect to email preferences page after signing in.");
            Assert.StringContains(ManageAccount.GetEmailPreferenceHeaderText(), expectedUserAccount.UserName, "Email Preferences page header doesn't contain users email address.");
            Assert.False(Email.VerifyUserEmailNotDisplayedInUrl(Browser.PageUrl, expectedUserAccount.UserName), "Personal email address displayed in page url.");
        }
    }
}
