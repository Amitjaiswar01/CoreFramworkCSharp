using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email
{
    public class EmailMobile : EmailDesktop, IEmailMobile
    {
        //Class members
        private string _switchButtonsClass = "switchButtons";
        private string _jsUpdateEmailPrefCallClass = "jsUpdateEmailPref";
        private string _thankYouMessageContainerClass = "drawerMessageContainer";
        private string _subscribeHeaderId = "subscribeHeader";
        private string _forEmailClass = "forEmail";

        private IElement SubscribeHeaderElement => Browser.Locate.ElementById(_subscribeHeaderId);
        private IElement EmailTabs => Browser.Locate.ElementByClassName(_switchButtonsClass);
        private IElement EmailPreferencesTab => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.A, EmailTabs).ElementAt(1);
        private IElement ThankYouMessageContainer => Browser.Locate.ElementByClassName(_thankYouMessageContainerClass);

        protected override IElement SubscribeLpRadioBtn => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "radio90031");
        protected override IElement UnsubscribeLpRatioBtn => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "radio90032");
        protected override IElement SubscribeObRadioBtn => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "radio90091");
        protected override IElement UnsubscribeObRadioBtn => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "radio90092");
        protected override IElement EmailSaveSettingsBtn => Browser.Locate.ElementBySelector(_jsUpdateEmailPrefCallClass.ToCssClassSelector());
        protected virtual IElement AccountTitle => Browser.Locate.ElementByClassName(_forEmailClass);

        public EmailMobile(IBrowser browser, IAssert assert) : base(browser, assert)
        {
        }

        //Interface implementation
        public override bool IsEmailPreferencesPage => Browser.Wait.IsVisibleElement(By.ClassName(_jsUpdateEmailPrefCallClass));

        public override void GoToEmailPreferencesByEmail(string email)
        {
            EmailPreferencesTab.Click();
            EmailRemoveField.Clear();
            EmailRemoveField.SendKeys(email);
            ChangePreferencesBtn.Click();
            Browser.Wait.ForDomReady();
        }

        public override void FillOutSubscribeNow(Account account)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(EmailAddressId.ToCssIdSelector()));
            EmailAddressField.SendKeys(account.EmailAddress);
            FirstNameField.SendKeys(account.FirstName);
            LastNameField.SendKeys(account.LastName);
            ZipcodeField.SendKeys(account.ZipCode);
            Browser.ClickByJs(SubscribeBtn);

            Browser.Wait.ForDomReady();
            Assert.True(SubscribeNowThankYouElement.Displayed, "Thank you header is not displayed.");
        }

        public void SelectPreferenceTab()
        {
            EmailPreferencesTab.Click();
        }

        public void UpdatePreference()
        {
            EmailSaveSettingsBtn.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_thankYouMessageContainerClass));
        }

        public override string GetSaveSettingsThankYouMessage()
        {
            return  ThankYouMessageContainer.Text;
        }

        public override string GetThankYouMessageAfterSubscribing()
        {
            Browser.Wait.IsVisibleElement(By.Id(_subscribeHeaderId));
            return SubscribeHeaderElement.Text;
        }

        public override string GetEmailFromHeader()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_forEmailClass));
            return AccountTitle.Text;
        }
    }
}