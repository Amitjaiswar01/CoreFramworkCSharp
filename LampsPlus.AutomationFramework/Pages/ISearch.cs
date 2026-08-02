using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface ISearch
	{
		#region Class Setup
        string SearchSubmitClass { get; }
        string GlobalSearchFieldId { get; }
        string SearchXpath { get; }
        string PacTargetInputClass { get; }
		#endregion

		#region Page Elements
		IElement SearchField { get; }
        IElement SearchButton { get; }
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
		/// Clear the search field text input by pressing the backspace key until the test has been cleared.
		/// </summary>
		void ClearSearchFieldText();


		/// <summary>
		/// Execute a search with the given search term using the site search feature.
		/// </summary>
		/// <param name="searchTerm">Term to search for on the Lamps Plus website.</param>
		void ExecuteSearch(string searchTerm);

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
    }
}
