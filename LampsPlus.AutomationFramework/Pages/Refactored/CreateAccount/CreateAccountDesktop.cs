using System;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.External.Nada;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CreateAccount
{
    public class CreateAccountDesktop : ICreateAccountDesktop
    {
        //Class members
        private string _createAccountBtnId = "createAccountBtn";
        private string _txtEmailId = "txtEmail";
        private string _txtPasswordId = "txtPassword";
        private string _createAccountButtonWrapperClass = "createAccount__button-wrapper";
        private string _customerServiceEmail = "LampsPlusAccountVerification@LampsPlus.com";
        private string _lampsPlusAccountVerificationSubject = "LampsPlus.com Account Verification";
        private string _createAccountFacebookButtonClass = "createAccount__facebook-button";
        private string _loginButtonId = "loginbutton";
        private string _gaAccountSignInBtnClass = "gaAccountSignInBtn";
        private string _createAccountId = "createAccount";

        private IElement EmailField => Browser.Locate.ElementById(_txtEmailId);
        private IElement PasswordField => Browser.Locate.ElementById(_txtPasswordId);

        protected virtual IElement FacebookConnectButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, _createAccountFacebookButtonClass);
        protected virtual IElement CreateAccountBtn => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Button, Browser.Locate.ElementByClassName(_createAccountButtonWrapperClass));
        
        protected virtual void WaitForFacebookLoginPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_loginButtonId.ToCssIdSelector()));
        }

        //Instances
        protected IBrowser Browser;

        public CreateAccountDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/account/create/";
        public virtual string FacebookLoginUrl => "https://www.facebook.com/login.php";
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_createAccountBtnId.ToCssIdSelector()));

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public void ClearEmailAndPasswordFields()
        {
            EmailField.Clear();
            PasswordField.Clear();
        }

        public void AddEmailAndPasswordToForm(Account account)
        {
            EmailField.SendKeys(account.EmailAddress);
            PasswordField.SendKeys(account.Password);

            CreateAccountBtn.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_gaAccountSignInBtnClass.ToCssClassSelector()));
        }

        public bool IsAccountVerificationEmailReceived(EmailMessageModel email, string toEmailAddress)
        {
            return email.From.Equals(_customerServiceEmail, StringComparison.InvariantCultureIgnoreCase) && email.Subject.Equals(_lampsPlusAccountVerificationSubject, StringComparison.InvariantCultureIgnoreCase);
        }

        public void OpenFacebookLoginPage()
        {
            FacebookConnectButton.Click();
            WaitForFacebookLoginPage();
        }

        public bool IsProfessionalCreateAccountPageLoaded()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_createAccountId.ToCssIdSelector()));
        }
    }
}