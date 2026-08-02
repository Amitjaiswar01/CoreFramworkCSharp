using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public abstract class PaymentBase : Page, IPayment
	{
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="customerAddressInformation"></param>
        /// <param name="globalLocators"></param>
        /// <param name="testsBase"></param>
        protected PaymentBase(IBrowser browser, ICustomerAddressInformation customerAddressInformation, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser)
        {
            CustomerAddressInformation = customerAddressInformation;
            GlobalLocators = globalLocators;
            TestsBase = testsBase;
        }

        #region Class Setup
        /// <summary>
        /// <see cref="Desktop.CustomerAddressInformation"/>
        /// </summary>
        internal TestsBase TestsBase { get; }

        public ICustomerAddressInformation CustomerAddressInformation;
        public IGlobalLocators GlobalLocators;

        public string ErrorMessageString => "This field is required.";
        #endregion

        #region CSS Selector Strings
        private string OrderSummaryProductsQtyClass { get; } = "orderSummaryProducts__qty";
        private string OrderSummaryProductsSkuClass { get; } = "orderSummaryProducts__sku";

        public string CardVerificationClass { get; } = "cardVerification";
		public string CardNumberClass { get; } = "cardNumber";
        public string CartBreadcrumbId { get; } = "cartBreadcrumb";
		public string CreditCvvClass { get; } = "creditCvv";
		public string ExpMonthClass { get; } = "expMonth";
		public string ExpYearClass { get; } = "expYear";
        public string FirstLineClass { get; } = "firstLine";
		public string FormWireTransferClassName { get; } = "formWireTransfer";
        public string JsPaymentTypeOptionListClass { get; } = "jsPaymentTypeOptionList";
        public string NameOnCardClass { get; } = "nameOnCard";
        public string NewPaymentOptionClass { get; } = "addNewPaymentOption";
        public string OrderSummaryProductsClass { get; } = "orderSummaryProducts";
        public string PaymentInfoAddress1Id { get; } = "paymentInfoAddress1";
		public string PaymentInfoAddress2Id { get; } = "paymentInfoAddress2";
		public string PaymentInfoCityId { get; } = "paymentInfoCity";
        public string PaymentInfoContentClass { get; } = "paymentInfoContent";
        public string PaymentInfoFirstNameId { get; } = "paymentInfoFirstName";
		public string PaymentInfoLastNameId { get; } = "paymentInfoLastName";
		public string PaymentInfoPhoneId { get; } = "paymentInfoPhone";
		public string PaymentInfoStateId { get; } = "paymentInfoState";
		public string PaymentInfoZipCodeId { get; } = "paymentInfoZipCode";
		public string PaymentInfoCountryId { get; } = "paymentInfoCountry";
		public string PaymentTypeRadioClass { get; } = "paymentTypeRadio";
        public string PayPalPaymentRadioId { get; } = "PayPalPaymentType";
        public string PlaceYourIntlOrderButtonId { get; } = "placeYourIntlOrderButton";
		public string PlaceYourOrderButtonId { get; } = "placeYourOrderButton";
		public string PurchaseOrderNumberClass { get; } = "purchaseOrderNumber";
        public string PurchaseOrderRadioButtonXpath { get; } = "//*[@id=\"paymentTypeOptions\"]//label[@for=\"PurchaseOrderPaymentType\"]";
        public string SameAsShippingControlClass { get; } = "sameAsShippingControl";
		public string SameAsShippingCheckboxId { get; } = "sameAsShipping";//TODO Reverted back original locator
        public string SecondLineClass { get; } = "secondLine";
        public string ShippingInfoId { get; } = "shippingInfo";
        public string ShowCountryFieldClass { get; } = "showCountryFieldId";
        public string ShowCountryFieldViewClass { get; } = "showCountryFieldId";
		public string SingleShippingPhoneId { get; } = "singleShippingPhone";
        public string ThirdLineClass { get; } = "thirdLine";
        public string AgreementContainerClass { get; } = "agreementContainer";

        public abstract string BillingCountryElementId { get; }
        public abstract string NewCardId { get; }
        public abstract string AgreeIntlOrderId { get; }
        public abstract string CardYearView29ErrorId { get; }
        public abstract string ApartmentString { get; }
        public abstract string CityString { get; }
        public abstract string CityStringWithApartmentFieldActive { get; }
        public abstract string StateString { get; }
        public abstract string StateStringWithApartmentFieldActive { get; }
        public abstract string StreetAddressString { get; }
        public abstract string ZipCodeString { get; }
        public abstract string ZipCodeStringWithApartmentFieldActive { get; }
        public abstract string DeliveryCallOutBtnSelector { get; }
        #endregion

        #region Page Elements
        public IElement BillingPhoneElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, SingleShippingPhoneId);
        public IElement CardCodeField => Browser.Locate.ElementByClassName(CreditCvvClass);
        public IElement CardVerificationElement => Browser.Locate.ElementByClassName(CardVerificationClass);
        public IElement ChangeCountryLinkClassElement => Browser.Locate.ElementByClassName(ShowCountryFieldViewClass);
        public IElement CreditCardField => Browser.Locate.ElementByClassName(CardNumberClass);
        public IElement NameField => Browser.Locate.ElementByClassName(NameOnCardClass);
        public IElement OrderSummaryQuantity => Browser.Locate.ElementBySelector(OrderSummaryProductsQtyClass.ToCssClassSelector());
        public IElement OrderSummarySku => Browser.Locate.ElementBySelector(OrderSummaryProductsSkuClass.ToCssClassSelector());
        public IElement PaymentFirstNameField => Browser.Locate.ElementById(PaymentInfoFirstNameId);
        public IElement PaymentLastNameField => Browser.Locate.ElementById(PaymentInfoLastNameId);
        public IElement PaymentAddress1Field => Browser.Locate.ElementById(PaymentInfoAddress1Id);
        public IElement PaymentAddress2Field => Browser.Locate.ElementById(PaymentInfoAddress2Id);
        public IElement PaymentCityField => Browser.Locate.ElementById(PaymentInfoCityId);
        public IElement PaymentStateElement => Browser.Locate.ElementById(PaymentInfoStateId);
        public IElement PaymentPhoneField => Browser.Locate.ElementById(PaymentInfoPhoneId);
        public IElement PaymentZipCodeField => Browser.Locate.ElementById(PaymentInfoZipCodeId);
        public IElement PlaceIntlOrderButton => Browser.Locate.ElementById(PlaceYourIntlOrderButtonId);
        public IElement PlaceOrderButton => Browser.Locate.ElementById(PlaceYourOrderButtonId);
        public IElement SameAsShippingCheckBox => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, SameAsShippingCheckboxId);
        public IElement SameAsShippingCheckBoxContainer => Browser.Locate.ElementByClassName(SameAsShippingControlClass);
        public IElement SameAsShippingCheckBoxGeneric => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Label, SameAsShippingCheckBoxContainer, true);

        public abstract IElement BillingCountryElementID { get; }
        public abstract IElement BillingCountryElement { get; }
        public abstract IElement CardExpirationField { get; }
        public abstract IElement CartBreadcrumb { get; }
        public abstract IElement ChangeCountryLinkEmployeeElement { get; }
        public abstract IElement CheckNumberField { get; }
        public abstract IElement CheckRadio { get; }
        public abstract IElement CreditCartRadio { get; }
        public abstract IElement DetailsLink { get; }
        public abstract IElement EditLink { get; }
        public abstract IElement InternationalAgreeCheckbox { get; }
        public abstract IElement NewPaymentOption { get; }
        public abstract IElement PurchaseOrderNumberField { get; }
        public abstract IElement PurchaseOrderRadioButton { get; }
        public abstract IElement WireTransferRadio { get; }
        public abstract IElement DeliveryAgreementBox { get; }
        #endregion
        

		/// <summary>
		/// Select a month from the dropdown.
		/// </summary>
		/// <param name="month">Month to select.</param>
		public void SelectMonth(string month) { new SelectElement(Browser.Locate.ElementByClassName(ExpMonthClass).InternalElement).SelectByText(month); }

        /// <summary>
        /// Select a month from the dropdown.
        /// </summary>
        /// <param name="month">Month to select.</param>
        public void SelectMonthByValue(string month) { new SelectElement(Browser.Locate.ElementByClassName(ExpMonthClass).InternalElement).SelectByValue(month); }

        /// <summary>
        /// Select a year from the dropdown.
        /// </summary>
        /// <param name="year">Year to select.</param>
        public void SelectYear(string year) { new SelectElement(Browser.Locate.ElementByClassName(ExpYearClass).InternalElement).SelectByText(year); }

        /// <inheritdoc />
        public abstract void EnterCreditCardInfo(CreditCard creditCard);

        public abstract void PlaceInternationalOrder();

        public bool IsAgreementContainerVisible => Browser.Locate.ElementsByClassName(AgreementContainerClass).Count > 0;

        public bool IsPaymentPageVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(PlaceYourOrderButtonId.ToCssIdSelector()));
        }
    }
}
