using System.Collections.ObjectModel;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class GlobalLocatorsBase : Page, IGlobalLocators
    {
        #region Class Setup
        public abstract string BillingStateSelectorId { get; }
        public abstract string CalloutBtnList { get; }
        public abstract string ConfirmDrawerActionClass { get; }
        public abstract string CountrySelectorId { get; }
        public abstract string GlobalHtml { get; }
        public abstract string HideMobileDrawerClass { get; }
        public abstract string LpDropdownPanelClass { get; }
        public abstract string LpmcToggleCollapsibleClass { get; }
        public abstract string LpmmMenuClass { get; }
        public abstract string LpmmMenuContainer { get; }
        public abstract string LpmmOpenClass { get; }
        public abstract string LpMobileDrawerClass { get; }
        public abstract string LpMobileOverlayClass { get; }
        public abstract string LpMobileOverlayContentClass { get; }
        public abstract string LpModalXpath { get; }
        public abstract string NotifyMyPhoneNumberString { get; }
        public abstract string RemoveItemClass { get; }
        public abstract string CloseLpModalClass { get; }
        public abstract string RemoveCartItemButtonClass { get; }
        #endregion

        #region CSS Selector Strings

        private string CartAddDimmerId { get; } = "CartAddDimmer";
        private string OpenBoxItemCalloutClass { get; } = "openBoxTag";
        public string CustomerPhotoClass { get; } = "instagramFeed";
        public string AddToCartMultiproductId { get; } = "AddToCart_Multiproduct";
        public string AriaDisabledAttributeString { get; } = "aria-disabled";
        public string AriaExpandedAttribute { get; } = "aria-expanded";
        public string AriaLabelledByAttribute { get; } = "aria-labelledby";
        public string AttributeNameType { get; } = "type";
        public string CalloutBtnClass { get; } = "calloutBtn";
        public string CanonicalAttribute { get; } = "canonical";
        public string ChkSelectClass { get; } = "chkSelect";
        public string DataEnteredCityString { get; } = "data-entered-city";
        public string DataEnteredPostalCodeString { get; } = "data-entered-postalcode";
        public string DataEnteredStateOrProvinceString { get; } = "data-entered-stateorprovince";
        public string DataEnteredStreet1String { get; } = "data-entered-street1";
        public string DataFacetTypeString { get; } = "data-facet-type";
        public string DataImgPathString { get; } = "data-imgpath";
        public string DataPaymentAttribute { get; } = "data-payment";
        public string DataPriceAttribute { get; } = "data-price";
        public string DataSkuAttribute { get; } = "data-sku";
        public string DataTextString { get; } = "data-text";
        public string DataUnbxdFacetNameString { get; } = "data-unbxd-facet-name";
        public string DataUnbxdFacetTypeString { get; } = "data-unbxd-facet-type";
        public string DataValueString { get; } = "data-value";
        public string DisplayNoneValue { get; } = "display: none;";
        public string EmailId { get; } = "email";
        public string ErrorClass { get; } = "error";
        public string HiddenClass { get; } = "hidden";
        public string InputTypeFileAttribute { get; } = "file";
        public string InputTypeRadioAttribute { get; } = "radio";
        public string LoadingClass { get; } = "loading";
        public string LoadWrapperId { get; } = "loadWrapper";
        public string LpModalBackdropId { get; } = "lpModalBackdrop";
        public string LpModalContentId { get; } = "lpModalContent";
        public string LpModalCloseId { get; } = "lpModalClose";
        public string LpModalId { get; } = "lpModal";
        public string LpMobileDrawerContainerClass { get; } = "lpMobileDrawerContainer";
        public string LpMobileOverlayCloseClass { get; } = ".sectionTitle .lpMobileOverlayClose";
        public string LpMobileOverlayVideoClass { get; } = "lpMobileOverlay--withVideo";
        public string ModalIframeId { get; } = "modalIframe";
        public string OsLabelClass { get; } = "osLabel";
        public string OsValueClass { get; } = "osValue";
        public string PdAddToCartId { get; } = "pdAddToCart";
        public string PdAddToCartXpath { get; } = "//*[@id='pdAddToCart']";
        public string PriceValue { get; } = "price";
        public string SelectedTextString { get; } = "selected";
        public string StyleString { get; } = "style";
        public string StateSelectorId { get; } = "lpSelectMobileDrawer__singleShippingState";
        public string SubMenuCloseButtonXpath { get; } = "//*[@id=\"prodOptionsMenu\"]/div/div/div[1]/button";
        public string ValueAttribute { get; } = "value";
        public string ValueString { get; } = "value";
        public string ProductNameId { get; } = "h1ProductName";
        #endregion

        #region Page Elements
        // Add to cart has another ID if it is on a multi-product pdp.
        public IElement CustomerPhoto => Browser.Locate.ElementByClassName(CustomerPhotoClass);
        public IElement AddToCartButton => Browser.Locate.ElementByXpath("//button[@id='pdAddToCart']");
        public IElement ErrorMessageElement => Browser.Locate.ElementByClassName(ErrorClass);
        public IElement OpenBoxItemCallout => Browser.Locate.ElementByClassName(OpenBoxItemCalloutClass);
        public IElement PlaAddToCartElement => Browser.Locate.ElementById(PdAddToCartId);
        public IElement ProductSelectedCheckBox(int index) => Browser.Locate.ElementsByClassName(ChkSelectClass)[index];

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public abstract IElement AllPageContent { get; }
        public abstract IElement GlobalMenu { get; }
        public abstract IElement Iframe { get; }
        public abstract IElement IframeModal { get; }
        public abstract IElement LpModalBackdrop { get; }
        public abstract IElement LpModalContent { get; }
        public abstract IElement LpMobileDrawerElement { get; }
        public abstract IElement LpMobileOverlayElement { get; }
        public abstract IElement LpModalCloseElement { get; }
        public abstract IElement CloseLpModal { get; }

        //Elements that exist in Desktop view and NOT Mobile view.
        public abstract IElement CalloutButton { get; }

        //Elements that exist in Mobile and NOT Desktop view.
        public abstract IElement LpDropdownPanel { get; }
        public abstract IElement BannerCandleholders { get; }
        public abstract IElement CandleHoldersAnimatedGif { get; }
        public abstract IElement CloseDrawerButton { get; }
        public abstract IElement CountryDropdown { get; }
        public abstract IElement DisplayedMobileDrawerMenu { get; }
        public abstract IElement LpMobileOverlayVideoElement { get; }
        public abstract IElement LpModalCloseVideoElement { get; }
        public abstract IElement MobileDrawerMenuInnerContainer { get; }
        public abstract IElement PdpDrawerElement { get; }
        public abstract IElement StateDropdown { get; }
        public abstract IElement BillingStateDropdown { get; }
        public abstract IElement RemoveCartItemButton(int index);

        public abstract ReadOnlyCollection<IElement> PdpDrawerElements { get; }
        #endregion

        /// <inheritdoc />
        protected GlobalLocatorsBase(IBrowser browser) : base(browser) { }

        public abstract void ClickDropdownByValue(IElement element, string optionValue);
    }
}
