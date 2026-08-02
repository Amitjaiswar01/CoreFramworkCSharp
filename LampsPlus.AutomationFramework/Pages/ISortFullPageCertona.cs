using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface ISortFullPageCertona
    {
        #region Class Setup
        string DailySaleCalloutId { get; }
        string MainPrice { get; }
        string SaveCalloutId { get; }
        string AddressByClass { get; }
        string QlStoreLocationLinkByClass { get; }
        string QlStoreLocationInfoContactInfoByClass { get; }
        string StoreNameByClass { get; }
        string DailySaleCalloutClass { get; }
         string SaveCallOutId { get; }
        string MobileStoreAddressAndHoursByClass { get; }
        string MobileStoreNameByClass { get; }
        string MobileStrikeThroughPriceXpath { get; }
        string CertonaSimilarDesignsItemsFirstItem { get; }
        string PhoneAndTextNumbersClass { get; }
        string TextLabel { get; }
        string ProductPriceTypeXpath { get; }
        string SaleClass { get; }
        #endregion

        #region Page Elements
        IElement PhoneAndTextNumbers(int index);
        IElement AddressInformation { get; }
        IElement AddressLocalityField { get; }
        IElement AddressRegionField { get; }
        IElement FirstDisplayedSimilarDesignElement { get; }
        IElement FullPageCertonaSimilarDesignsTitleElement { get; }
	    IElement FullPageCertonaItemInSimilarDesignsSection { get; }
        IElement FullPageCertonaSimilarDesignsContainer { get; }
        IElement FullPageCertonaSimilarDesignsItemsFirstItem { get; }
        IElement PostalCodeField { get; }
        IElement StreetAddressField { get; }
        IElement StoreAddressAndHours { get; }
        IElement StoreDetailInfo { get; }
        IElement StoreName { get; }
        IElement StrikeThroughPrice { get; }
        ReadOnlyCollection<IElement> FullPageCertonaSimilarDesignsItems { get; }
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

        IElement DailySaleCallout { get; }
        IElement MainPriceOnSfp { get; }
        IElement MobileStrikeThroughPrice { get; }
        IElement SaveCallOut { get; }
        IElement ComparableValueCallOut { get; }
        IElement EndCallOut { get; }
        bool IsPriceVerbiageVisible { get; }
        bool IsEndDateVerbiageVisible { get; }
        bool IsMobileSaleVerbiage { get; }
    }
}
