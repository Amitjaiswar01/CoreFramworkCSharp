using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface ISignIn
	{
        #region Class Setup
        string AccountSignInXpath { get; }
        string ConnectUsingFbId { get; }
        string ContinueShoppingId { get; }
        string CreateAccountLinkText { get; }
        string EmailId { get; }
        string MessageClass { get; }
        string PasswordId { get; }
        string ProfessionalPageSignUpClass { get; }
        string IconFacebookWhiteClass { get; }
        string SubmitFormBtn { get; }
        string SubtextClass { get; }
        #endregion

        #region Page Elements
        IElement ContinueShoppingButton { get; }
        IElement MessageElement { get; }
		IElement PasswordField { get; }
        IElement EmailField { get; }
        IElement SignInButton { get; }
        IElement SignInContainer { get; }
	    IElement SignInEmailField { get; }
	    IElement SignInPasswordField { get; }
        IElement CreateAccountLink { get; }
        IElement ConnectUsingFb { get; }
        IElement ConnectUsingFbButton { get; }
        IElement HeaderMenuSignInButton { get; }
        IElement CreateAccountButton { get; }
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
	}
}
