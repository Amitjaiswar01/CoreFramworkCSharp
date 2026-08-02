using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// https://www.lampsplus.com/employee-tools/EmployeeOrderLookup.aspx
	/// </summary>
	public interface IEmployeeOrderLookup
	{
        #region Page Elements
        IElement FirstOrder { get; }
		IElement MyOrdersRadioButton { get; }
		IElement OrderSearchButton { get; }
		IElement OrderSearchInput { get; }
		IElement PaginationDropdown { get; }
		IElement StoreRadioButton { get; }
		IElement SearchTypeDropdown { get; }

		ReadOnlyCollection<IElement> PaginationDropdownPageOptions { get; }
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
