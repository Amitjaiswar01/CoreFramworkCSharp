using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface IPayPal
	{
        #region Page Elements
        IElement ContinueButton { get; }
        IElement EmailLoginInput { get; }
        IElement LoginEndButton { get; }
        IElement LoginStartButton { get; }
        IElement NextButton { get; }
        IElement PasswordLoginInput { get; }
        IElement PayPalLogInButton { get; }
		IElement PayPalNextButton { get; }
		IElement PayPalPassword { get; }
		IElement PayPalLogin { get; }
        IElement PayPalContinue { get; }
		IElement PayPalSpinner { get; }
        #endregion

        /// <summary>
        /// Check to see if PayPal spinner is currently active.
        /// </summary>
        bool IsPayPalSpinnerActive { get; }

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
