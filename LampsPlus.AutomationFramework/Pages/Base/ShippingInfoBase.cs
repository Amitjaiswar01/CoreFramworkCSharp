using System.Web.UI;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;


namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ShippingInfoBase : Page, IShippingInfo
    {
        /// <inheritdoc />
        protected ShippingInfoBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }
        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        #endregion

        #region CSS Selector Strings
        public static string PoliciesContentId => "policiesContent";
		public static string AddNewAddrClass => "addNewAddr";
        public static string CalloutBtnClass => "calloutBtn";
        public static string CartShippingClass => "cartShipping";
		public static string EmailClass => "email";
        public static string FedExAddressValidationClass => "fedExAddressValidation";
        public static string jsNewAddress => "jsNewAddress";
        public static string JsShipMultipleLinkClass => "jsShipMultipleLink";
        public static string PacContainerClass => "pac-container";
		public static string ProceedPaymentId => "proceedPayment";
        public static string SaveAddressFromModalId => "saveAddressFromModal";
		public static string SavedAddressClass => "savedAddress";
        public static string SavedShippingAdressesModalId => "savedShippingAddressesModal";
        public static string SavedFullNameClass => "savedFullName";
		public static string SaveToProfileInputValueAttribute => "SaveToProfile";
		public static string ShippingAddressContainerClass => "shippingAddressContainer";
		public static string ShippingOptionsChangedMessageClass => "shippingOptionsChangedMessage";
		public static string ShipToDifferentAddrClass => "shipToDifferentAddr";
		public static string ShowCountryFieldId => "showCountryField";
		public static string SingleAddressFormContainerId => "singleAddressFormContainer";
		public static string SingleShippingAddress1Id => "singleShippingAddress1";
		public static string SingleShippingAddress2Id => "singleShippingAddress2";
		public static string SingleShippingCityId => "singleShippingCity";
		public static string SingleShippingCountryId => "singleShippingCountry";
		public static string SingleShippingFirstNameId => "singleShippingFirstName";
		public static string SingleShippingLastNameId => "singleShippingLastName";
		public static string SingleShippingPhoneId => "singleShippingPhone";
		public static string SingleShippingStateId => "singleShippingState";
		public static string SingleShippingZipCodeId => "singleShippingZipCode";
		public static string UpdateAddrClass => "updateAddr";
        public static string LpModalContentId => "lpModalContent";
        public static string Address1Id => "address1";

        #endregion

        #region Page Elements
        public IElement NewAddressButton(int index) => Browser.Locate.ElementsByClassName(jsNewAddress)[index];
        public IElement ProceedToPaymentElement => Browser.Locate.ElementById(ProceedPaymentId);
		public IElement ShippingNotification => Browser.Locate.ElementById(PoliciesContentId);
        public IElement ShipToMultipleAddressesButton => Browser.Locate.ElementByClassName(JsShipMultipleLinkClass);
        public IElement EmailField => Browser.Locate.ElementByClassName(EmailClass);
        public IElement ShippingNotificationProceedToPaymentButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, CalloutBtnClass, ShippingNotification);
        
        public abstract IElement ShippingCellShippingCost { get; }
        public abstract IElement ShippingPage { get; }
        public abstract IElement ShippingHideMobileDrawer { get; }
        public abstract IElement ShippingPageCartNumber { get; }
        public abstract IElement NewShippingAddressFormContainer { get; }
        public abstract IElement ShippingInformationPageContainer { get; }
        public abstract IElement CloseShippingPage { get; }
        public abstract IElement SelectNonDefaultAddress { get; }
        #endregion
    }
}
