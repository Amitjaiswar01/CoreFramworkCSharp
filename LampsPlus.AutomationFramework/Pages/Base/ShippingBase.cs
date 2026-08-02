using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ShippingBase : Page, IShipping
    {
        /// <inheritdoc />
        protected ShippingBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        //public string 
        public string CalloutBtnXpath { get; } = "//*[@id='policiesContent']//a[contains(@class, 'calloutBtn')]";
        public string ContactInformationId { get; } = "contactInfoEmail";
        public string SingleShippingFirstNameId { get; } = "singleShippingFirstName";
        public string PoliciesContentId { get; } = "policiesContent";
        public string AddNewAddrClass { get; } = "addNewAddr";
        public string CalloutBtnClass { get; } = "calloutBtn";
        public string CartShippingClass { get; } = "cartShipping";
        public string EmailClass { get; } = "email";
        public string FieldCheckboxClass { get; } = "fieldCheckbox";
        public string FedExAddressValidationClass { get; } = "fedExAddressValidation";
        public string jsNewAddress { get; } = "jsNewAddress";
        public string JsShipMultipleLinkClass { get; } = "jsShipMultipleLink";
        public string JsShipSingleAddressLinkClass { get; } = "jsShipSingleAddress";
        public string IsMultipleShippingClass { get; } = "isMultipleShipping";
        public string PacContainerClass { get; } = "pac-container";
        public string PaymentInfoAddressFieldsetClass { get; } = "paymentInfoAddressFieldset";
        public string ProceedPaymentId { get; } = "proceedPayment";
        public string PromoCodeLineClass { get; } = "promoCodeLine";
        public string SaveAddressFromModalId { get; } = "saveAddressFromModal";
        public string SavedAddressClass { get; } = "savedAddress";
        public string SavedShippingAdressesModalId { get; } = "savedShippingAddressesModal";
        public string SavedFullNameClass { get; } = "savedFullName";
        public string ShipDropDownClass { get; } = "shippingAddressItem__shipAddresses";
        public string ShippingAddressContainerClass { get; } = "shippingAddressContainer";
        public string ShippingOptionsChangedContainerClass { get; } = "shippingOptionsChangedContainer";
        public string ShipToDifferentAddrClass { get; } = "shipToDifferentAddr";
        public string ShowCountryFieldId { get; } = "showCountryField";
        public string SingleAddressFormContainerId { get; } = "singleAddressFormContainer";
        public string SingleShippingAddress1Id { get; } = "singleShippingAddress1";
        public string SingleShippingAddress2Id { get; } = "singleShippingAddress2";
        public string SingleShippingCityId { get; } = "singleShippingCity";
        public string SingleShippingCountryId { get; } = "singleShippingCountry";
        public string SingleShippingFirstNameErrorId { get; } = "singleShippingFirstName-error";
        public string SingleShippingLastNameId { get; } = "singleShippingLastName";
        public string SingleShippingPhoneId { get; } = "singleShippingPhone";
        public string SingleShippingStateId { get; } = "singleShippingState";
        public string SingleShippingZipCodeId { get; } = "singleShippingZipCode";
        public string ShowAnotherAddressFieldContainerClass { get; } = "showAnotherAddressFieldContainer";
        public string UpdateAddrClass { get; } = "updateAddr";
        public string LpModalContentId { get; } = "lpModalContent";
        public string EditShippingAddressSaveBtn { get; } = "//*[@id='modalEditShipping']/form/button";
        public string MultipleShipppingGiftCardToClass { get; } = "shippingAddressItem__gcRecipient";
        public string MultipleShipppingProductNameClass { get; } = "shippingAddressItem__prodName";
        public string MultipleShipppingProductQtyClass { get; } = "shippingAddressItem__prodQty";
        public string MultipleShipppingProductQtySku { get; } = "shippingAddressItem__styleNum";
        public string MultipleShipppingGiftCardToXpath { get; } = "//*[@id='multipleAddressesContainer']//p";
        public string ValidationMessage { get; } = "validationMessage";
        public string ProceedPaymentXpath { get; } = "//button[@id='proceedPayment']";
        public string SelectShippingAddressString { get; } = "Select a Shipping Address";

        public abstract string AssetCounterContainerXpath { get; }
        public abstract string SelectShippingAddressClass { get; }
        public abstract string ShippingCellShippingCostClass { get; }
        public abstract string CartShippingId { get; }
        public abstract string ShippingTypeShippingCostClass { get; }
        public abstract string ShowAnotherAddressFieldClass { get; }
        public abstract string HideMobileDrawerClass { get; }
        public abstract string CartId { get; }
        public abstract string CartInfoBottom { get; }
        public abstract string LpMobileDrawerContainerClass { get; }
        public abstract string BdCartShippingId { get; }
        public abstract string AddNewAddressDrawerClass { get; }
        public abstract string LpMobileDrawerClass { get; }
        public abstract string LpmdRightClass { get; }
        public abstract string LpmdFullScreenClass { get; } 
        public abstract string shippingDrawerClass { get; }
        public abstract string ShippingAddressModalId { get; }
        #endregion

        #region Page Elements
        public IElement ContactInformation => Browser.Locate.ElementById(ContactInformationId);
        public virtual IElement SelectShippingAddress => Browser.Locate.ElementByClassName(SelectShippingAddressClass);
        public IElement EmailField => Browser.Locate.ElementByClassName(EmailClass);        
        public IElement MultipleShipppingProductName(int index) => Browser.Locate.ElementsByClassName(MultipleShipppingProductNameClass)[index];
        public IElement MultipleShipppingProductQty(int index) => Browser.Locate.ElementsByClassName(MultipleShipppingProductQtyClass)[index];
        public IElement MultipleShipppingProductSku(int index) => Browser.Locate.ElementsByClassName(MultipleShipppingProductQtySku)[index];
        public IElement ErrorMessage => Browser.Locate.ElementBySelector(ValidationMessage.ToCssClassSelector());
        public IElement NewAddressButton(int index) => Browser.Locate.ElementsByClassName(jsNewAddress)[index];
        public IElement ProceedToPaymentButton => Browser.Locate.ElementBySelector(ProceedPaymentId.ToCssIdSelector());
        public IElement PromotionDiscount => Browser.Locate.ElementByClassName(PromoCodeLineClass);
        public IElement ShippingFirstNameErrorValidation => Browser.Locate.ElementById(SingleShippingFirstNameErrorId);
        public IElement ShippingNotification => Browser.Locate.ElementById(PoliciesContentId);
        public IElement ShipToMultipleAddressesButton => Browser.Locate.ElementByClassName(JsShipMultipleLinkClass);
        public IElement ShipToSingleAddressButton => Browser.Locate.ElementByClassName(JsShipSingleAddressLinkClass);
        public IElement ShippingNotificationProceedToPaymentButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, CalloutBtnClass, ShippingNotification);
        public IElement ShipToDifferentAddrButton => Browser.Locate.ElementByClassName(ShipToDifferentAddrClass);
        public IElement SavedShippingAdressesModal => Browser.Locate.ElementById(SavedShippingAdressesModalId);
        public IElement ProceedToBilling => Browser.Locate.ElementByXpath(ProceedPaymentXpath);
        public abstract IElement SelectShippingAddressOption { get; }
        public abstract IElement ShippingCellShippingCost { get; }
        public abstract IElement ShippingPage { get; }
        public abstract IElement ShippingPageCartInfo { get; }
        public abstract IElement ShippingPageCartNumber { get; }
        public abstract IElement MobileAssetCounterButton { get; }
        public abstract IElement MobileShippingOptionsModal { get; }
        public abstract IElement NewShippingAddressFormContainer { get; }
        public abstract IElement ShippingInformationPageContainer { get; }
        public abstract IElement CloseShippingPage { get; }
        public abstract IElement SelectNonDefaultAddress { get; }
        public abstract IElement NewShippingAddressFormFullContent { get; }
        public abstract IElement MultipleShipppingGiftCardTo(int index);
        #endregion

        public bool IsShippingPageVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(ProceedPaymentId.ToCssIdSelector()));
        }

        public IElement ShipDropDown(int index) => Browser.Locate.ElementsByClassName(ShipDropDownClass)[index];

        public void ShipAddressInMuliMode(int index, string value) {

            SelectElement MultipleShipDropDown = new SelectElement(ShipDropDown(index).InternalElement);

            MultipleShipDropDown.SelectByValue(value);
        }

        public bool IsDifferentAddressScreenVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(SelectShippingAddressClass), timeToWait);
        }
    }
}
