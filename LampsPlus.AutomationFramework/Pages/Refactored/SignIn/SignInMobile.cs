using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn
{
    public class SignInMobile : SignInDesktop, ISignInMobile
    {
        //Class members
        private string _subtextXpath = "//*[@id='signIn']/form/p";
        private string _profileIconClass = "accountDropdownLink";
        private string _signInBtnId = "submitFormBtn";
        private string _emailId = "UserName";
        private string _passwordId = "Password";
        private string _signoutId = "ctDropdownSignOut";
        private string _professionalPageSignUpClass = "professionalPage__signUp";

        private IElement ProfessionalSignUpButton => Browser.Locate.ElementByClassName(_professionalPageSignUpClass);
        protected IElement ProfileIcon => Browser.Locate.ElementByClassName(_profileIconClass);
        protected IElement signInBtn => Browser.Locate.ElementById(_signInBtnId);
        protected IElement SignInEmailField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _emailId);
        protected IElement SignInPasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _passwordId);
        protected override IElement PasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "Password", SignInContainer);

        public SignInMobile(IBrowser browser, SessionSettings settings, IAssert assert, IModalDesktop modal) : base(browser, settings, assert, modal)
        {
        }
        public bool IsSignOutBtn => Browser.Wait.IsVisibleElement(By.Id(_signoutId));

        //Interface implementation
        public override IElement MessageElement => Browser.Locate.ElementByXpath(_subtextXpath);

        public override IElement GetSignInMessage()
        {
            return MessageElement;
        }

        public override string GetSignInMessageText()
        {
            return MessageElement.Text;
        }

        public override void SignInFromHeader(LampsPlusAccount loginAccount)
        {
            ProfileIcon.Click();
            Browser.Wait.IsVisibleElement(By.Id(_signInBtnId));
            SignInEmailField.SendKeys(loginAccount.UserName);
            SignInPasswordField.SendKeys(loginAccount.Password);
            signInBtn.Click();
        }

        public bool CheckSignOutIcon()
        {
            ProfileIcon.Click();
            return IsSignOutBtn;
        }

        public override void NavigateToProSignInPage()
        {
            Browser.Navigate(ProPageUrl);
            Browser.Wait.IsVisibleElement(By.ClassName(_professionalPageSignUpClass));
            ProfessionalSignUpButton.Click();
        }
    }
}
