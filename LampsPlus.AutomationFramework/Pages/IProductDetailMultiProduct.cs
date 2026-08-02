using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface IProductDetailMultiProduct
	{
        #region Class Setup
        string MultiProductAvailableOptionsText { get; }
        string MultiSelectClass { get; }
        string CllOutClass { get; }
        string MultiOptionMenuOpenId { get; }
        string MultiProdOptionPriceClass { get; }
        string MultiProdSizeOptionsOption { get; }
        string OptionSectionTitleClass { get; }
        string ProdOptionDescriptionClass { get; }
        bool IsMultiProductOverlayOpen(int timeToWait);
        #endregion

        #region Page Elements
        IElement AvailableOptionsSectionTitle { get; }
	    IElement MultiProdSizeOptionsElement { get; }
		IElement MultiProdSizeOptions { get; }
        IElement SelectedMultiProductDropdownOption { get; }
		IElement ShipsFreeWithOrdersOver49CallOutForMultiProduct { get; }
        IElement UnselectedMultiProductDropdownOption { get; }
	    
		ReadOnlyCollection<IElement> MultiProductPrices { get; }
		ReadOnlyCollection<IElement> MultiProductOptionNames { get; }
		ReadOnlyCollection<IElement> MultiProductDropdownOptions { get; }
	    ReadOnlyCollection<IElement> MultiProductRadioOptions { get; }
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
        /// Get callout text for shipping elements ShipsFreeWithOrdersOver49CallOut or
        /// ShipsFreeWithOrdersOver49CallOutForMultiProduct depending on product type.
        /// </summary>
        /// <returns></returns>
        string GetShippingCallOut();
    }
}
