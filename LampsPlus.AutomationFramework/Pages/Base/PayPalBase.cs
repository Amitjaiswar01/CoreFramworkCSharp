using System.Web.UI;
using Automation.Framework;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class PayPalBase : Page, IPayPal
    {
        protected PayPalBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string BaslLoginButtonContainerClass { get; } = "baslLoginButtonContainer";
        private string BtnLoginId { get; } = "btnLogin";
        private string BtnNextId { get; } = "btnNext";
        private string ConfirmButtonTopId { get; } = "confirmButtonTop";
        private string LoginSectionId { get; } = "loginSection";
        private string PasswordId { get; } = "password";
        private string PreloaderSpinnerId { get; } = "preloaderSpinner";
        private string ContinueButtonId { get; } = "confirmButtonTop";
        private string EmailLoginId { get; } = "email";
        private string LoginEndButtonId { get; } = "btnLogin";
        private string NextButtonLoginId { get; } = "btnNext";
        private string PasswordLoginId { get; } = "password";
        #endregion

        #region Page Elements
        public IElement ContinueButton => Browser.Locate.ElementById(ContinueButtonId);
        public IElement EmailLoginInput => Browser.Locate.ElementById(EmailLoginId);
        public IElement LoginEndButton => Browser.Locate.ElementById(LoginEndButtonId);
        public IElement LoginStartButton => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(BaslLoginButtonContainerClass));
        public IElement NextButton => Browser.Locate.ElementById(NextButtonLoginId);
        public IElement PayPalContinue => Browser.Locate.ElementById(ConfirmButtonTopId);
        public IElement PayPalNextButton => Browser.Locate.ElementById(BtnNextId);
        public IElement PayPalLogin => Browser.Locate.ElementById(BtnLoginId);
        public IElement PasswordLoginInput => Browser.Locate.ElementById(PasswordLoginId);
        public IElement PayPalLogInButton => Browser.Locate.ElementByLinkText("Log In", Browser.Locate.ElementById(LoginSectionId));
        public IElement PayPalPassword => Browser.Locate.ElementById(PasswordId);
        public IElement PayPalSpinner => Browser.Locate.ElementById(PreloaderSpinnerId);
        #endregion
        
	    public bool IsPayPalSpinnerActive => PayPalSpinner == null || !PayPalSpinner.GetAttribute(HtmlTextWriterAttribute.Style.ToString()).Contains("none");
	}
}
