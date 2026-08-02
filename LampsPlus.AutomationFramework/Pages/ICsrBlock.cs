using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface ICsrBlock
	{
        string SaleSourceXpath { get; }

		#region Page Elements
		IElement AddProfessionalAccountLink { get; }
		IElement ApplyMdPercentButton { get; }
		IElement ApplySAndPButton { get; }
		IElement CsrPanelElement { get; }
	    IElement ManualDiscountPercentTextBox { get; }
        IElement ReasonCodeDropdown { get; }
		IElement RemoveProfessionalAccountElement { get; }
		IElement SaleSourceField { get; }
		IElement SAndPField { get; }
		IElement SecondaryEmployeeField { get; }
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
		/// Select an option in the Sale Source dropdown.
		/// Use the available options from the SaleSource class.
		/// </summary>
		/// <param name="saleSource">Available Sale Source options.</param>
		void SelectSaleSource(string saleSource);

		/// <summary>
		/// Select an option in the Reason dropdown.
		/// Use the available options from the ReasonCode class.
		/// </summary>
		/// <param name="reasonCode">Available Reason Code options.</param>
		void SelectReasonCode(string reasonCode);

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
	}
}
