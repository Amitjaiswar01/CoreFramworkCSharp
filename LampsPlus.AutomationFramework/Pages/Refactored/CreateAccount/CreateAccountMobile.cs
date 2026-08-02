using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CreateAccount
{
    public class CreateAccountMobile : CreateAccountDesktop, ICreateAccountMobile
    {
        //Class members
        private string _createAccountBtnId = "createAccountBtn";
        private string _userNameId = "UserName";
        private string _accountFacebookConnectBtnId = "accountFacebookConnectBtn";
        private string _loginPasswordStepElementId = "login_password_step_element";

        private IElement AccountVerificationUserNameField => Browser.Locate.ElementBySelector(_userNameId.ToCssIdSelector());

        protected override IElement CreateAccountBtn => Browser.Locate.ElementById(_createAccountBtnId);
        protected override IElement FacebookConnectButton => Browser.Locate.ElementBySelector(_accountFacebookConnectBtnId.ToCssIdSelector());

        protected override void WaitForFacebookLoginPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_loginPasswordStepElementId.ToCssIdSelector()));
        }

        //Instances
        public CreateAccountMobile(IBrowser browser) : base(browser) { }

        //Interface Implementation
        public override string FacebookLoginUrl => "https://m.facebook.com/login.php";

        public void RemoveUsernamePasswordModal()
        {
            AccountVerificationUserNameField.Click();
            AccountVerificationUserNameField.SendKeys("a");
        }
    }
}
