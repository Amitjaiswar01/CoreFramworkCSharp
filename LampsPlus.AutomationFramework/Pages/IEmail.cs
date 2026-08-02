using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IEmail
	{
        #region Class Setup
        #endregion

        #region Page Elements
        IElement NotifyMeMessageElement { get; }

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
