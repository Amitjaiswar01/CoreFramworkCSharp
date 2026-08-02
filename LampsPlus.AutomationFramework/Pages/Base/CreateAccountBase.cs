using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium.Support.UI;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class CreateAccountBase : Page, ICreateAccount
    {
        /// <inheritdoc />
        protected CreateAccountBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }

        public string Question { get; } = "What is your favorite food?";
        public string VerificationRegEx { get; } = "(.*?)";

        /// <summary>
        /// End of Account Verification Url.
        /// </summary>
        public string LampsPlusAccountVerificationUrlEnd { get; } = "\">please click";

        /// <summary>
        /// Account Activation key.
        /// </summary>
        public string LampsPlusAccountActivationKey => "activationkey";
        #endregion

        #region CSS Selector Strings
        private string DdlSecurityQuestionId { get; } = "ddlSecurityQuestion";
        private string TxtEmailId { get; } = "txtEmail";
        private string TxtFirstNameId { get; } = "txtFirstName";
        private string TxtLastNameId { get; } = "txtLastName";
        private string TxtPasswordId { get; } = "txtPassword";
        private string TxtSecurityAnswerId { get; } = "txtSecurityAnswer";
        private string TxtZipCodeId { get; } = "txtZipCode";
        private string UserNameId { get; } = "UserName";

        public string CreateAccountBtnId { get; } = "createAccountBtn";
        public string CreateAccountButtonWrapperClass { get; } = "createAccount__button-wrapper";
        public string CreateAccountId { get; } = "createAccount";
        #endregion

        #region Page Elements
        //Elements that are located the same way in both Desktop and Mobile views.
        public IElement AccountVerificationUserNameField => Browser.Locate.ElementBySelector(UserNameId.ToCssIdSelector());
        public IElement EmailField => Browser.Locate.ElementById(TxtEmailId);
        public IElement FirstNameField => Browser.Locate.ElementById(TxtFirstNameId);
	    public IElement LastNameField => Browser.Locate.ElementById(TxtLastNameId);
        public IElement PasswordField => Browser.Locate.ElementById(TxtPasswordId);
        public IElement SecurityAnswerField => Browser.Locate.ElementById(TxtSecurityAnswerId);
	    public IElement ZipCodeField => Browser.Locate.ElementById(TxtZipCodeId);

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public abstract IElement CreateAccountBtn { get; }
        public abstract IElement ProCreateAccountTitle { get; }
        public abstract IElement TogglePasswordVisibility { get; }
        #endregion

        /// <summary>
        /// Select the security question based on the value defined in the Question property of this class.
        /// </summary>
        public void SelectSecurityQuestion() { new SelectElement(Browser.Locate.ElementById(DdlSecurityQuestionId).InternalElement).SelectByText(Question); }

        /// <summary>
        /// Get the customer service email address.
        /// </summary>
        public string CustomerServiceEmail => "LampsPlusAccountVerification@LampsPlus.com";

        /// <summary>
        /// Get the account verification subject.
        /// </summary>
        public string LampsPlusAccountVerificationSubject => "LampsPlus.com Account Verification";
    }
}
