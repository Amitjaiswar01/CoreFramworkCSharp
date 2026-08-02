using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IPayment
	{
        #region Class Setup
        string ApartmentString { get; }
        string CityString { get; }
        string CityStringWithApartmentFieldActive { get; }
        string ErrorMessageString { get; }
        string JsPaymentTypeOptionListClass { get; }
        string PayPalPaymentRadioId { get; }
        string PurchaseOrderRadioButtonXpath { get; }
        string StateString { get; }
        string StateStringWithApartmentFieldActive { get; }
        string StreetAddressString { get; }
        string ZipCodeString { get; }
        string ZipCodeStringWithApartmentFieldActive { get; }
        string DeliveryCallOutBtnSelector { get; }
        #endregion

        #region Page Elements
        IElement BillingCountryElement { get; }
        IElement BillingCountryElementID { get; }
		IElement BillingPhoneElement { get; }
		IElement CardCodeField { get; }
        IElement ChangeCountryLinkClassElement { get; }
		IElement ChangeCountryLinkEmployeeElement { get; }
		IElement CardVerificationElement { get; }
		IElement CheckNumberField { get; }
		IElement CheckRadio { get; }
		IElement CreditCardField { get; }
		IElement CreditCartRadio { get; }
        IElement DetailsLink { get; }
		IElement EditLink { get; }
		IElement InternationalAgreeCheckbox { get; }
        IElement NameField { get; }
		IElement NewPaymentOption { get; }
        IElement OrderSummaryQuantity { get; }
        IElement OrderSummarySku { get; }
        IElement PaymentFirstNameField { get; }
		IElement PaymentLastNameField { get; }
		IElement PaymentAddress1Field { get; }
		IElement PaymentAddress2Field { get; }
		IElement PaymentCityField { get; }
		IElement PaymentStateElement { get; }
		IElement PaymentPhoneField { get; }
		IElement PaymentZipCodeField { get; }
		IElement PlaceOrderButton { get; }
		IElement PlaceIntlOrderButton { get; }
		IElement PurchaseOrderRadioButton { get; }
		IElement PurchaseOrderNumberField { get; }
        IElement SameAsShippingCheckBox { get; }
        IElement SameAsShippingCheckBoxContainer { get; }
		IElement SameAsShippingCheckBoxGeneric { get; }
        IElement WireTransferRadio { get; }
        IElement DeliveryAgreementBox { get; }
        #endregion

        bool IsPaymentPageVisible(int timeToWait);
        bool IsAgreementContainerVisible { get; }

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        /// <summary>
        /// Enter in credit card info
        /// </summary>
        /// <param name="creditCard"></param>
        void EnterCreditCardInfo(CreditCard creditCard);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Placing a international order work flow.
        /// </summary>
        void PlaceInternationalOrder();

        /// <summary>
        /// Select a month from the dropdown.
        /// </summary>
        /// <param name="month">Month to select.</param>
        void SelectMonth(string month);

	    /// <summary>
	    /// Select a month from the dropdown by value.
	    /// </summary>
	    /// <param name="month">Month to select.</param>
	    void SelectMonthByValue(string month);


        /// <summary>
        /// Select a year from the dropdown.
        /// </summary>
        /// <param name="year">Year to select.</param>
        void SelectYear(string year);

        #region CSS Selector Strings
        string CardVerificationClass { get; }
        string CardNumberClass { get; }
        string CardYearView29ErrorId { get; }
        string CartBreadcrumbId { get; }
        string CreditCvvClass { get; }
        string ExpMonthClass { get; }
        string ExpYearClass { get; }
        string FirstLineClass { get; }
        string FormWireTransferClassName { get; }
        string NameOnCardClass { get; }
        string NewPaymentOptionClass { get; }
        string OrderSummaryProductsClass { get; }
        string PaymentInfoAddress1Id { get; }
        string PaymentInfoAddress2Id { get; }
        string PaymentInfoCityId { get; }
        string PaymentInfoContentClass { get; }
        string PaymentInfoFirstNameId { get; }
        string PaymentInfoLastNameId { get; }
        string PaymentInfoPhoneId { get; }
        string PaymentInfoStateId { get; }
        string PaymentInfoZipCodeId { get; }
        string PaymentTypeRadioClass { get; }
        string PlaceYourIntlOrderButtonId { get; }
        string PlaceYourOrderButtonId { get; }
        string PurchaseOrderNumberClass { get; }
        string SameAsShippingControlClass { get; }
        string SameAsShippingCheckboxId { get; }
        string SecondLineClass { get; }
        string ShippingInfoId { get; }
        string ShowCountryFieldClass { get; }
        string ShowCountryFieldViewClass { get; }
        string SingleShippingPhoneId { get; }
        string ThirdLineClass { get; }
        string BillingCountryElementId { get; }
        string NewCardId { get; }
        string AgreeIntlOrderId { get; }
        string AgreementContainerClass { get; }
        #endregion
    }
}
