using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Email
{
    public class EmailLocatorDesktopTest : EmailLocatorTests
    {
        public EmailLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Email")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateEmailElementsTest(string config) => Locate(config);

        protected override void VerifyEmailPreferences()
        {
            VerifyElementDisplayed(() => Email.ConfirmEmailAddressField);
            VerifyElementDisplayed(() => Email.ShowCountryLink);
            VerifyElementDisplayed(() => Email.SubscribeNowElement);

            Email.ShowCountryLink.Click();
            VerifyElementDisplayed(() => Email.CountryField);

            VerifyElementNotImplemented(() => Email.EmailTabs);
            VerifyElementNotImplemented(() => Email.EmailPreferencesTab);
            VerifyElementNotImplemented(() => Email.SubTextElement);
        }

        protected override void SubscribeToEmails(Account account)
        {
            SubscribeToEmailsWorkflow.SubscribeToEmailsFromFooter(account);

            VerifyElementDisplayed(() => Email.SubscribeNowThankYouElement);
            VerifyElementDisplayed(() => Email.EmailUtagElement);
            VerifyElementDisplayed(() => Email.ChangePreferencesBtn);
            VerifyElementDisplayed(() => Email.EmailRemoveField);

            SubscribeToEmailsWorkflow.GoToEmailPreferencesByEmail(account.EmailAddress);

            VerifyElementDisplayed(() => Email.LpEmailSubscribeRadio);
            VerifyElementDisplayed(() => Email.LpEmailUnsubscribeRadio);
            VerifyElementDisplayed(() => Email.LpObEmailUnsubscribeRadio);
            VerifyElementDisplayed(() => Email.EmailSaveSettingsBtn);
            VerifyElementDisplayed(() => Email.ListOfUnsubscribeButtons);
            VerifyElementDisplayed(() => Email.ListOfSubscribeButtons);

            VerifyElementNotImplemented(() => Email.ListOfFieldGroups);
        }
    }


    public class EmailLocatorMobileTest : EmailLocatorTests
    {
        public EmailLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Email")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateEmailElementsTest(string config) => Locate(config);

        protected override void VerifyEmailPreferences()
        {
            VerifyElementDisplayed(() => Email.EmailTabs);
            VerifyElementDisplayed(() => Email.EmailPreferencesTab);
            VerifyElementDisplayed(() => Email.SubscribeNowElement);

            VerifyElementNotImplemented(() => Email.ConfirmEmailAddressField);
            VerifyElementNotImplemented(() => Email.ShowCountryLink);
            VerifyElementNotImplemented(() => Email.CountryField);
        }

        protected override void SubscribeToEmails(Account account)
        {
            SubscribeToEmailsWorkflow.SubscribeToEmailsFromFooter(account);

            VerifyElementDisplayed(() => Email.SubscribeNowThankYouElement);
            VerifyElementDisplayed(() => Email.EmailUtagElement);
            Email.EmailPreferencesTab.Click();
            VerifyElementDisplayed(() => Email.ChangePreferencesBtn);
            VerifyElementDisplayed(() => Email.EmailRemoveField);

            SubscribeToEmailsWorkflow.GoToEmailPreferencesByEmail(account.EmailAddress);

            VerifyElementDisplayed(() => Email.LpEmailSubscribeRadio);
            VerifyElementDisplayed(() => Email.LpEmailUnsubscribeRadio);
            VerifyElementDisplayed(() => Email.LpObEmailUnsubscribeRadio);
            VerifyElementDisplayed(() => Email.EmailSaveSettingsBtn);
            VerifyElementDisplayed(() => Email.ListOfFieldGroups);
            VerifyElementDisplayed(() => Email.ListOfUnsubscribeButtons);
            VerifyElementDisplayed(() => Email.ListOfSubscribeButtons);
            VerifyElementDisplayed(() => Email.SubTextElement);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Email Subscribe page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "Email")]
    public abstract class EmailLocatorTests : PageObjectTestsBase
    {
        protected EmailLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            var account = new Account();
            InitializeFramework(config, Urls.EmailSubscribeChangeEmailPreferencesUrl);
            BuildElementsList(Email);
            
            VerifyElementDisplayed(() => Email.EmailAddressField);
            VerifyElementDisplayed(() => Email.FirstNameField);
            VerifyElementDisplayed(() => Email.LastNameField);
            VerifyElementDisplayed(() => Email.ZipcodeField);
            VerifyElementDisplayed(() => Email.SubscribeBtn);

            VerifyEmailPreferences();
           
            SubscribeToEmails(account);

            UpdateEmailPreferences();

            Browser.Wait.ForDisplayedElement(Email.PrefConfirmationMessageElement);
            VerifyElementDisplayed(() => Email.PrefConfirmationMessageElement);
        }

        protected abstract void VerifyEmailPreferences();

        protected abstract void SubscribeToEmails(Account account);

        /// <summary>
        /// Updates and saves the selected email preference.
        /// </summary>
        private void UpdateEmailPreferences()
        {
            Email.LpEmailUnsubscribeRadio.Click();
            Email.LpObEmailUnsubscribeRadio.Click();
            Email.EmailSaveSettingsBtn.Click();
        }
    }
}
