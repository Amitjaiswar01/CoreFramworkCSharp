using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IStores
	{
        #region CSS Selectors        
        string AllStoresListClass { get; }
        string CallForAppointmentInStoreId { get; }
        string DirectionsButtonXpath { get; }
        string DivStoreSearchResultClass { get; }
        string HeaderDropDownsMenuClass { get; }
        string LpIconCalendarClass { get; }
        string LpIconCallClass { get; }
        string LpIconCouponClass { get; }
        string LpIconDetailsClass { get; }
        string LpIconDirectionsClass { get; }
        string MapsString { get; }
        string MyStoreBlockClass { get; }
        string NavWishlistClass { get; }
        string ScottsdaleStoreXpath { get; }
        string StoreDetailsBtnClass { get; }
        string StoreDetailsLink { get; }
        string StoreLinksClass { get; }
        string StoresOptionsXpath { get; }
        string StoreZipCodeInputId { get; }
        string StorePickerSubmitId { get; }
        string MakeThisMyStoreClass { get; }
        string MyStoreClass { get; }
        string MakeThisMyStoreXpath { get; }
        string MakeMyStoreClass { get; }
        #endregion

        #region Page Elements
        IElement BopusSubmenu { get; }
        IElement MakeThisMyStoreButton { get; }
		IElement MakeThisMyStoreContainer { get; }
		IElement MyStoreButton { get; }
		IElement AddressLocalityField { get; }
		IElement AddressRegionField { get; }
		IElement GetDirectionsButton { get; }
		IElement PostalCodeField { get; }
		IElement StreetAddressField { get; }
		IElement OpenNow { get; }
		IElement MyStoreWrapper { get; }
		IElement AllStoresLampsPlus { get; }
		IElement SelectedStoreElement { get; set; }
		IElement SelectedStoreDetailsName { get; }
		IElement SelectedStoreDetailsLink { get; }
	    IElement StoreZipCodeInputElement { get; }
        IElement StoresDropDownMenu { get; }
        IElement StorePickerSubmitElement { get; }
	    IElement LpIconCouponElement { get; }
        IElement LpIconCalendarElement { get; }
        IElement LpIconCallElement { get; }
        IElement LpIconDetailsButton { get; }
        IElement LpIconDirectionsElement { get; }
	    IElement NearByZipStores { get; }
        IElement RandomStoreElement { get; }
	    IElement StorePhotosImgElement { get; }
	    IElement SelectStoreNearMeLinks { get; }
	    IElement RandomStoreNearMeElement { get; }

        ReadOnlyCollection<IElement> AllStoresLampsPlusLinks { get; }
		ReadOnlyCollection<IElement> LampsPlusStoreRegionLinks { get; }
        ReadOnlyCollection<IElement> StoreDetailBtns { get; }
        ReadOnlyCollection<IElement> StoreResults { get; }
        ReadOnlyCollection<IElement> StoreNearMeLinks { get; }
	    ReadOnlyCollection<IElement> StoreDetailsRegionLinks { get; }
        #endregion

        string BreadcrumbText { get; }
		string DropdownMyStoreName { get; }
		string DropdownMyStoreAddress { get; }
        string MakeThisMyStoreString { get; }
        string StoreAddress { get; }

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        /// <summary>
		/// Click Make This My Store Button if not already My Store.
		/// </summary>
		void ClickMakeThisMyStoreButton();

        /// <summary>
        /// Gets list of anchor element strings present in a store listing widget on a region page.
        /// </summary>
        /// <param name="storeResult"></param>
        /// <returns></returns>
        List<string> GetLinkTextFromStoreResult(IElement storeResult);

        /// <summary>
        /// Gets get string of Detail button result present in a store listing widget on a region page.
	    /// </summary>
        /// <param name="storeResult"></param>
        /// <returns></returns>
        string GetDetailBtnStoreResult(IElement storeResult);
	    /// <summary>
	    /// Gets get string of make this my store result button present in a store listing widget on a region page.
	    /// </summary>
	    /// <param name="storeResult"></param>
	    /// <returns></returns>
	    string GetMakeThisMyStoreResult(IElement storeResult);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        bool IsStoreSelected(int timeToWait);
        bool IsStoreSetToMyStore(int timeToWait);
    }
}
