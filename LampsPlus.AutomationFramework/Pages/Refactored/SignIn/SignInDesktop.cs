using System;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn
{
    public class SignInDesktop : ISignInDesktop
    {
        //Class members
        private string _messageClass = "message";
        private string _accountSignInId = "accountSignIn";
        private string _accountSignInXpath = "//*[@id='submitFormBtn']";
        private string _userNameModalId = "UserNameModal";
        private string _passwordModalId = "PasswordModal";
        private string _signInBtnCLass = "signInBtn";
        private string _accountLinkXpath = "//*[@id='userName']";
        private string _signInHeaderLinkId = "hdrSignIn";
        private string _dropDownSignInClass = "calloutBtn";
        private string _signInContainerClass = "signInContainer";
        private string _signInButtonId = "signInButton";
        private string _proSignInId = "memberSignIn";
        private string _rememberMeCheckboxXpath = "//div[contains(@class, 'fieldInline')]/label";
        private string _userNameFieldId = "UserName";

        protected IElement UserNameField => Browser.Locate.ElementById(_userNameFieldId);
        protected IElement SignInContainer => Browser.Locate.ElementById(_accountSignInId);
        protected virtual IElement PasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "txtPassword", SignInContainer);
        protected IElement SignInButton => Browser.Locate.ElementByXpath(_accountSignInXpath);
        protected IElement SignInHeaderLink => Browser.Locate.ElementById(_signInHeaderLinkId);
        protected IElement EmailFieldModal => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _userNameModalId);
        protected IElement PasswordFieldModal => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _passwordModalId);
        protected IElement SignInButtonModal => Browser.Locate.ElementByClassName(_signInBtnCLass);
        protected IElement SignInBtnDropDown => Browser.Locate.ElementByClassName(_dropDownSignInClass);
        private IElement ShippingSignInButton => Browser.Locate.ElementById(_signInButtonId);
        private IElement ProSignInLink => Browser.Locate.ElementById(_proSignInId);

        //Instances 
        protected IBrowser Browser;
        protected readonly SessionSettings Settings;
        protected readonly IAssert Assert;
        protected readonly IModalDesktop Modal;

        public SignInDesktop(IBrowser browser, SessionSettings settings, IAssert assert, IModalDesktop modal)
        {
            Browser = browser;
            Settings = settings;
            Assert = assert;
            Modal = modal;
        }

        //Interface implementation
        public virtual IElement MessageElement => Browser.Locate.ElementByXpath("//*[@id=\"Signin\"]/div[1]");
        public virtual IElement EmailField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "UserName", SignInContainer);
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/account/sign-in/";
        public string ProPageUrl => "https://www.lampsplus.com/pros/";

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.XPath(_accountSignInXpath), 30);
        public bool IsMyAccountLink => Browser.Wait.IsVisibleElement(By.XPath(_accountLinkXpath));
        public bool IsRememberMeCheckboxVisible => Browser.Wait.IsInvisibleElement(By.XPath(_rememberMeCheckboxXpath));

        public void SignInWithPrefilledEmail(string password)
        {
            PasswordField.SendKeys(password);
            SignInButton.Click();
        }

        public bool SignIn(string userName, string password)
        {
            //Navigate to SignIn page.
            Browser.Navigate(PageUrl);
            Assert.True(IsCurrentPage, "Current page is not SignIn page");

            //Sign in
            EmailField.SendKeys(userName);
            PasswordField.SendKeys(password);
            SignInButton.Click();

            //Verify If User successfully signed in
            var tempImplicitWait = -50;
            var desktopHomePageValidationLocator = "#userName";
            var desktopSignInErrorValidationLocator = "#accountSignIn > div.server-error";
            var mobileHomePageValidationLocator = ".hpsplash__img";
            var mobileSignInErrorValidationLocator = "#signIn > form > p";
            try
            {
                Browser.Wait.IsVisibleElement(!Settings.IsMobileView ? By.CssSelector(desktopHomePageValidationLocator)
                    : By.CssSelector(mobileHomePageValidationLocator), tempImplicitWait);
            }
            catch (Exception ex)
            {
                Browser.Log.Message($"Error message: {ex.Message}");
                Browser.Wait.ForClickableElement(!Settings.IsMobileView ? Browser.Locate.ElementBySelector(desktopSignInErrorValidationLocator)
                    : Browser.Locate.ElementBySelector(mobileSignInErrorValidationLocator), tempImplicitWait);
                return false;
            }

            return true;
        }

        public virtual IElement GetSignInMessage()
        {
            return MessageElement;
        }

        public virtual string GetSignInMessageText()
        {
            return MessageElement.Text;
        }

        public string GetEmailFieldValue()
        {
            return EmailField.GetAttribute("value");
        }

        public virtual void SignInFromHeader(LampsPlusAccount loginAccount)
        {
            Browser.MouseOverOnElement(SignInHeaderLink);
            Browser.Wait.IsVisibleElement(By.ClassName(_signInContainerClass));

            SignInBtnDropDown.Click();

            Browser.Wait.ForElementToStopAnimating(Modal.GetLpModal());
            Browser.Wait.IsVisibleElement(By.ClassName(_signInBtnCLass));
            Browser.SwitchFocusToIframe(Modal.GetLpModal());

            EmailFieldModal.SendKeys(loginAccount.UserName);
            PasswordFieldModal.SendKeys(loginAccount.Password);
            SignInButtonModal.Click();
        }

        public void SignInFromShippingHeader(LampsPlusAccount loginAccount)
        {
            Browser.Wait.IsVisibleElement(By.Id(_signInButtonId));

            ShippingSignInButton.Click();

            Browser.Wait.ForElementToStopAnimating(Modal.GetLpModal());
            Browser.Wait.IsVisibleElement(By.ClassName(_signInBtnCLass));
            Browser.SwitchFocusToIframe(Modal.GetLpModal());

            EmailFieldModal.SendKeys(loginAccount.UserName);
            PasswordFieldModal.SendKeys(loginAccount.Password);
            SignInButtonModal.Click();
        }

        public bool IsMyAccountLinkVisible()
        {
            return IsMyAccountLink;
        }

        public void OpenSignInModal()
        {
            Browser.MouseOverOnElement(SignInHeaderLink);
            Browser.Wait.IsVisibleElement(By.ClassName(_signInContainerClass));

            SignInBtnDropDown.Click();
            Browser.Wait.ForElementToStopAnimating(Modal.GetLpModal());
            Browser.Wait.IsVisibleElement(By.ClassName(_signInBtnCLass));
            Browser.SwitchFocusToIframe(Modal.GetLpModal());
        }

        public virtual void NavigateToProSignInPage()
        {
           Browser.Navigate(ProPageUrl);
           Browser.Wait.ForClickableElement(ProSignInLink);
           ProSignInLink.Click();
           Browser.Wait.IsVisibleElement(By.XPath(_accountSignInXpath));
        }
    }
}
