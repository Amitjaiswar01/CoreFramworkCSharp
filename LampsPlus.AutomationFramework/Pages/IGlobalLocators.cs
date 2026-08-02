using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile.
    /// </summary>
    public interface IGlobalLocators
    {
        #region Class Setup
        string NotifyMyPhoneNumberString { get; }
        #endregion

        #region CSS Selector Strings
        string RemoveCartItemButtonClass { get; }
        string AddToCartMultiproductId { get; }
        string AriaDisabledAttributeString { get; }
        string AriaExpandedAttribute { get; }
        string AriaLabelledByAttribute { get; }
        string AttributeNameType { get; }
        string CanonicalAttribute { get; }
        string CalloutBtnClass { get; }
        string CalloutBtnList { get; }
        string ChkSelectClass { get; }
        string ConfirmDrawerActionClass { get; }
        string CountrySelectorId { get; }
        string DataEnteredCityString { get; }
        string DataEnteredPostalCodeString { get; }
        string DataEnteredStateOrProvinceString { get; }
        string DataEnteredStreet1String { get; }
        string DataFacetTypeString { get; }
        string DataImgPathString { get; }
        string DataUnbxdFacetNameString { get; }
        string DataUnbxdFacetTypeString { get; }
        string DataPaymentAttribute { get; }
        string DataPriceAttribute { get; }
        string DataSkuAttribute { get; }
        string DataTextString { get; }
        string DataValueString { get; }
        string DisplayNoneValue { get; }
        string EmailId { get; }
        string ErrorClass { get; }
        string GlobalHtml { get; }
        string HiddenClass { get; }
        string HideMobileDrawerClass { get; }
        string InputTypeFileAttribute { get; }
        string InputTypeRadioAttribute { get; }
        string LpDropdownPanelClass { get; }
        string LoadingClass { get; }
        string LoadWrapperId { get; }
        string LpMobileOverlayCloseClass { get; }
        string LpModalBackdropId { get; }
        string LpModalContentId { get; }
        string LpModalCloseId { get; }
        string LpModalId { get; }
        string LpModalXpath { get; }
        string LpMobileDrawerContainerClass { get; }
        string LpMobileDrawerClass { get; }
        string LpMobileOverlayVideoClass { get; }
        string LpMobileOverlayClass { get; }
        string LpMobileOverlayContentClass { get; }
        string LpmcToggleCollapsibleClass { get; }
        string LpmmMenuClass { get; }
        string LpmmMenuContainer { get; }
        string LpmmOpenClass { get; }
        string ModalIframeId { get; }
        string OsLabelClass { get; }
        string OsValueClass { get; } 
        string PdAddToCartId { get; }
        string PdAddToCartXpath { get; }
        string PriceValue { get; }
        string RemoveItemClass { get; }
        string CloseLpModalClass { get; }
        string SelectedTextString { get; }
        string StateSelectorId { get; }
        string BillingStateSelectorId { get; }
        string StyleString { get; }
        string SubMenuCloseButtonXpath { get; }
        string ValueAttribute { get; }
        string ValueString { get; }
        string CustomerPhotoClass { get; }
        string ProductNameId { get; }
        #endregion

        #region Page Elements
        IElement CustomerPhoto { get; }
        IElement AddToCartButton { get; }
        IElement AllPageContent { get; }
        IElement BannerCandleholders { get; }
        IElement BillingStateDropdown { get; }
        IElement CalloutButton { get; }
        IElement CandleHoldersAnimatedGif { get; }
        IElement CloseDrawerButton { get; }
        IElement CountryDropdown { get; }
        IElement DisplayedMobileDrawerMenu { get; }
        IElement ErrorMessageElement { get; }
        IElement GlobalMenu { get; }
        IElement Iframe { get; }
        IElement IframeModal { get; }
        IElement LpDropdownPanel { get; }
        IElement LpModalBackdrop { get; }
        IElement LpModalContent { get; }
        IElement LpMobileDrawerElement { get; }
        IElement LpMobileOverlayElement { get; }
        IElement LpMobileOverlayVideoElement { get; }
        IElement LpModalCloseElement { get; }
        IElement LpModalCloseVideoElement { get; }
        IElement MobileDrawerMenuInnerContainer { get; }
        IElement OpenBoxItemCallout { get; }
        IElement PdpDrawerElement { get; }
        IElement CloseLpModal { get; }
        IElement PlaAddToCartElement { get; }
        IElement ProductSelectedCheckBox(int index);
        IElement StateDropdown { get; }
        IElement RemoveCartItemButton(int index);
        ReadOnlyCollection<IElement> PdpDrawerElements { get; }

        #endregion

        void ClickDropdownByValue(IElement element, string optionValue);
    }
}
