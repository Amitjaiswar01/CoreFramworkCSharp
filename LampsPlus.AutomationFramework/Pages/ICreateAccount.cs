using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface ICreateAccount
	{
        #region Class Setup
        string LampsPlusAccountActivationKey { get; }
        string LampsPlusAccountVerificationUrlEnd { get; }
        string Question { get; }
        string VerificationRegEx { get; }
        string CreateAccountBtnId { get; }
        string CreateAccountId { get; }
        #endregion

        #region Page Elements
        IElement AccountVerificationUserNameField { get; }
        IElement CreateAccountBtn { get; }
        IElement EmailField { get; }
        IElement FirstNameField { get; }
        IElement LastNameField { get; }
        IElement PasswordField { get; }
        IElement ProCreateAccountTitle { get; }
        IElement SecurityAnswerField { get; }
        IElement TogglePasswordVisibility { get; }
        IElement ZipCodeField { get; }

        #endregion

        /// <summary>
        /// Get the customer service email address.
        /// </summary>
        string CustomerServiceEmail { get; }

		/// <summary>
		/// Get the account verification subject.
		/// </summary>
		string LampsPlusAccountVerificationSubject { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Select the security question based on the value defined in the Question property of this class.
        /// </summary>
        void SelectSecurityQuestion();
	}
}
