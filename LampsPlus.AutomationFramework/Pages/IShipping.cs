using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface IShipping
    {
        #region Page Elements
        IElement ContactInformation { get; }
        IElement SelectShippingAddress { get; }
        IElement ProceedToPaymentButton { get; }
        IElement PromotionDiscount { get; }
        IElement SelectShippingAddressOption { get; }
        IElement ShippingNotification { get; }
        IElement ShipToMultipleAddressesButton { get; }
		IElement ShipToSingleAddressButton { get; }
        IElement MobileAssetCounterButton { get; }
        IElement MobileShippingOptionsModal { get; }
        IElement NewAddressButton(int index);
        IElement EmailField { get; }
		IElement ShippingPage { get; }
		IElement ShippingCellShippingCost { get; }
        IElement ShippingFirstNameErrorValidation { get; }
        IElement ShippingNotificationProceedToPaymentButton { get; }
        IElement ShippingPageCartInfo { get; }
        IElement ShippingPageCartNumber { get; }
        IElement NewShippingAddressFormContainer { get; }
        IElement ShippingInformationPageContainer { get; }
        IElement CloseShippingPage { get; }
        IElement SelectNonDefaultAddress { get; }
        IElement NewShippingAddressFormFullContent { get; }
        IElement ErrorMessage { get; }
        IElement ShipToDifferentAddrButton { get; }
        IElement SavedShippingAdressesModal { get; }
        IElement MultipleShipppingGiftCardTo(int index);
        IElement MultipleShipppingProductName(int index);
        IElement MultipleShipppingProductQty(int index);
        IElement MultipleShipppingProductSku(int index);      
        IElement ProceedToBilling { get; }
        #endregion

        void ShipAddressInMuliMode(int index, string value);

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

        string CalloutBtnXpath { get; }
        string ShippingAddressModalId { get; }
        string AssetCounterContainerXpath { get; }
        string IsMultipleShippingClass { get; }
        string ShipDropDownClass { get; }
        string SingleShippingFirstNameId { get; }
        string ShowAnotherAddressFieldClass { get; }
        string PoliciesContentId { get; }
        string AddNewAddrClass { get; } 
        string CalloutBtnClass { get; } 
        string CartShippingClass { get; } 
        string EmailClass { get; } 
        string FieldCheckboxClass { get; } 
        string FedExAddressValidationClass { get; }
        string jsNewAddress { get; } 
        string JsShipMultipleLinkClass { get; } 
        string JsShipSingleAddressLinkClass { get; } 
        string PacContainerClass { get; } 
        string PaymentInfoAddressFieldsetClass { get; }
        string ProceedPaymentId { get; }
        string PromoCodeLineClass { get; }
        string SaveAddressFromModalId { get; } 
        string SavedAddressClass { get; } 
        string SavedShippingAdressesModalId { get; } 
        string SavedFullNameClass { get; }
        string ShippingAddressContainerClass { get; }
        string ShippingOptionsChangedContainerClass { get; }
        string ShipToDifferentAddrClass { get; }
        string ShowCountryFieldId { get; } 
        string SingleAddressFormContainerId { get; } 
        string SingleShippingAddress1Id { get; } 
        string SingleShippingAddress2Id { get; } 
        string SingleShippingCityId { get; }
        string SingleShippingCountryId { get; } 
        string SingleShippingFirstNameErrorId { get; } 
        string SingleShippingLastNameId { get; }
        string SingleShippingPhoneId { get; } 
        string SingleShippingStateId { get; } 
        string SingleShippingZipCodeId { get; } 
        string ShowAnotherAddressFieldContainerClass { get; } 
        string UpdateAddrClass { get; }
        string LpModalContentId { get; }
        string EditShippingAddressSaveBtn { get; }
        string ShippingCellShippingCostClass { get; }
        string CartShippingId { get; }
        string ShippingTypeShippingCostClass { get; }
        string HideMobileDrawerClass { get; } 
        string CartId { get; }
        string CartInfoBottom { get; }
        string LpMobileDrawerContainerClass { get; }
        string BdCartShippingId { get; } 
        string AddNewAddressDrawerClass { get; } 
        string LpMobileDrawerClass { get; }
        string LpmdRightClass { get; }
        string LpmdFullScreenClass { get; }
        string shippingDrawerClass { get; } 
        string ProceedPaymentXpath { get; }
        string SelectShippingAddressClass { get; }
        string SelectShippingAddressString { get; }
        bool IsShippingPageVisible(int timeToWait);
        bool IsDifferentAddressScreenVisible(int timeToWait);
    }
}