using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email
{
    public class EmailDesktop : IEmailDesktop
    {
        //Class members
        private string _subscribeBtnId  = "subscribeBtn";
        private string _emailRemoveId = "EmailRemove";
        private string _changePreferencesBtnId  = "changePreferencesBtn";
        private string _emailSaveSettingsButtonXpath  = "//div[contains(@class,'emailSaveSettingsButton')]";
        private string _prefConfirmationMessageClass = "prefConfirmationMessage";
        private string _firstNameId  = "FirstName";
        private string _lastNameId  = "LastName";
        private string _zipcodeId  = "Zipcode";
        private string _subscribeNowThankYou = "//*[@id='subscribeNowThankYou']";
        private string _emailUtagClass = "emailUtag";
        private string _unsubscribeLpRadioId = "radio90032";
        private string _subscribeLpRadioId = "radio90031";
        private string _subscribeObRadioId = "radio90091";
        private string _unsubscribeObRadioId = "radio90092";
        private string _continueShoppingBtnClass = "btnDark";
        private string _subscribeString = "Subscribe";
        private string _unsubscribeString = "Unsubscribe";
        private string _accountTitleClass = "accountTitle";

        protected string EmailAddressId => "EmailAddress";

        private IElement ContinueShoppingBtn => Browser.Locate.ElementByClassName(_continueShoppingBtnClass);
        private IElement PrefConfirmationMessageElement => Browser.Locate.ElementByClassName(_prefConfirmationMessageClass);
        private ReadOnlyCollection<IElement> ListOfSubscribeButtons => GetSubscriptionLinks(true);
        private ReadOnlyCollection<IElement> ListOfUnsubscribeButtons => GetSubscriptionLinks(false);

        protected IElement EmailRemoveField => Browser.Locate.ElementById(_emailRemoveId);
        protected IElement FirstNameField => Browser.Locate.ElementById(_firstNameId);
        protected IElement LastNameField => Browser.Locate.ElementById(_lastNameId);
        protected IElement ZipcodeField => Browser.Locate.ElementById(_zipcodeId);
        protected IElement EmailAddressField => Browser.Locate.ElementBySelector(EmailAddressId.ToCssIdSelector());
        protected IElement SubscribeNowThankYouElement => Browser.Locate.ElementByXpath(_subscribeNowThankYou);
        protected IElement EmailUtagElement => Browser.Locate.ElementByClassName(_emailUtagClass);
        protected IElement SubscribeBtn => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, _subscribeBtnId);
        protected IElement ChangePreferencesBtn => Browser.Locate.ElementById(_changePreferencesBtnId);
        protected virtual IElement EmailSaveSettingsBtn => Browser.Locate.ElementByXpath(_emailSaveSettingsButtonXpath);
        protected virtual IElement UnsubscribeObRadioBtn => Browser.Locate.ElementById(_unsubscribeObRadioId);
        protected virtual IElement SubscribeObRadioBtn => Browser.Locate.ElementById(_subscribeObRadioId);
        protected virtual IElement SubscribeLpRadioBtn => Browser.Locate.ElementById(_subscribeLpRadioId);
        protected virtual IElement UnsubscribeLpRatioBtn => Browser.Locate.ElementById(_unsubscribeLpRadioId);
        protected virtual IElement AccountTitle => Browser.Locate.ElementByClassName(_accountTitleClass);

        /// <summary>
        /// Get the list of subscribe or unsubscribe buttons.
        /// </summary>
        /// <param name="subscribe"></param>
        /// <returns></returns>
        private ReadOnlyCollection<IElement> GetSubscriptionLinks(bool subscribe)
        {
            var elementsList = new List<IElement>();
            var subscriptionButtons = subscribe ? _subscribeString : _unsubscribeString;
            var subscriptionButtonsList = Browser.Locate.ElementsByTagNameAndAttribute(HtmlTextWriterTag.Label, AttributeSelectorType.Contains, "for", "radio");

            foreach (var element in subscriptionButtonsList)
            {
                if (element.InternalElement.Text == subscriptionButtons)
                {
                    elementsList.Add(element);
                }
            }
            return new ReadOnlyCollection<IElement>(elementsList);
        }

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;

        public EmailDesktop(IBrowser browser, IAssert assert)
        {
            Browser = browser;
            Assert = assert;
        }

        //Interface implementation
        public virtual bool IsEmailPreferencesPage => Browser.Wait.ForClickableElement(EmailSaveSettingsBtn).Displayed;
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/account/email/?isFromFooter=true";
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_subscribeBtnId.ToCssIdSelector()));

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public virtual void GoToEmailPreferencesByEmail(string email)
        {
            EmailRemoveField.Clear();
            EmailRemoveField.SendKeys(email);
            ChangePreferencesBtn.Click();
            Browser.Wait.ForDomReady();
        }

        public virtual void FillOutSubscribeNow(Account account)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(EmailAddressId.ToCssIdSelector()));
            Assert.Equals(account.EmailAddress, EmailAddressField.GetAttribute("value"), "Email address is not pre-populated");
            FirstNameField.SendKeys(account.FirstName);
            LastNameField.SendKeys(account.LastName);
            ZipcodeField.SendKeys(account.ZipCode);
            SubscribeBtn.Click();

            Browser.Wait.ForDomReady();
            Assert.True(SubscribeNowThankYouElement.Displayed, "Thank you header is not displayed.");
        }

        public bool VerifyUserEmailNotDisplayedInUrl(string pageUrl, string userEmailInPageUrl)
        {
            if (pageUrl.Contains("email=" + userEmailInPageUrl.Replace("@lampsplus.com", String.Empty)))
            {
                return true;
            }
            return false;
        }

        public void ChangeEmailPreferencesSubscribe()
        {
            foreach (var button in ListOfSubscribeButtons) { button.Click(); }
        }

        public void ChangeEmailPreferencesUnsubscribe()
        {
            foreach (var button in ListOfUnsubscribeButtons) { button.Click(); }
        }

        public void SaveSettings()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_emailSaveSettingsButtonXpath));

            EmailSaveSettingsBtn.Click();

            Browser.Wait.ForDisplayedElement(PrefConfirmationMessageElement);
        }

        public void UpdateEmailPreferences()
        {

            if ((bool) Browser.ExecuteJs("return document.querySelector('#radio90031').checked").Equals(true))
            {
                UnsubscribeLpRatioBtn.Click();
            }
            else
            {
                SubscribeLpRadioBtn.Click();
            }

            if ((bool) Browser.ExecuteJs("return document.querySelector('#radio90091').checked").Equals(true))
            { 
                UnsubscribeObRadioBtn.Click();
            }
            else
            {
                SubscribeObRadioBtn.Click();
            }

            EmailSaveSettingsBtn.Click();
        }

        public virtual string GetSaveSettingsThankYouMessage()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_prefConfirmationMessageClass));
            return TextActions.RegexNoTabsAndNewLines(PrefConfirmationMessageElement.Text).Trim();
        }

        public string GetContinueShoppingMessage()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_continueShoppingBtnClass));
            return ContinueShoppingBtn.Text;
        }

        public virtual string GetEmailFromHeader()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_accountTitleClass));
            return AccountTitle.Text;
        }

        public string GetEmailFromEmailAddressField()
        { 
            Browser.Wait.IsVisibleElement(By.Id(EmailAddressId));
            return EmailAddressField.GetAttribute("value");
	   }

        public virtual string GetThankYouMessageAfterSubscribing()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_subscribeNowThankYou));
            return SubscribeNowThankYouElement.Text;
        }
    }
}