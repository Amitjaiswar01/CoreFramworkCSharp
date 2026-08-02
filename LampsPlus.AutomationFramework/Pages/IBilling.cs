using System.Collections.Generic;

using Automation.Framework;
using Automation.Framework.Utilities;

using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IBilling
	{
		#region Page Elements
#pragma warning disable 1591
		IWebElement BillingFirstNameElement { get; }
		IWebElement BillingLastNameElement { get; }
		IWebElement BillingAddressLine1Element { get; }
		IWebElement BillingAddressLine2Element { get; }
		IWebElement BillingCountryEmployeeElement { get; }
		IWebElement BillingCountryElement { get; }
		IWebElement BillingCityElement { get; }
		IWebElement BillingStateElement { get; }
		IWebElement BillingZipElement { get; }
		IWebElement BillingPhoneElement { get; }
		IWebElement BreadcrumbShippingLink { get; }
		IWebElement CartBreadcrumb { get; }
		IWebElement CardCodeField { get; }
		IWebElement CardNumberErrorElement { get; }
		IWebElement CardSecurityCodeErrorElement { get; }
		IWebElement CardExpirationMonthErrorElement { get; }
		IWebElement CardExpirationYearErrorElement { get; }
		IWebElement CardNameErrorElement { get; }
		IWebElement ChangeCountryLinkElement { get; }
		IWebElement ChangeCountryLinkEmployeeView28Element { get; }
		IWebElement ChangeCountryLinkEmployeeElement { get; }
		IWebElement CartPaymentPage { get; }
		IWebElement CheckNumberField { get; }
		IWebElement CheckRadio { get; }
		IWebElement CreditCardField { get; }
		IWebElement CreditCartRadio { get; }
        IWebElement CardVerificationElement { get; }
        IWebElement DetailsLink { get; }
		IWebElement DeletePayment { get; }
		IWebElement GiftCertificateNumber { get; }
		IWebElement GiftCertificateAmount { get; }
		IWebElement GiftCartApplyButton { get; }
		IWebElement InternationalAgreeCheckbox { get; }
		IWebElement MinimumOrderError { get; }
		IWebElement NameField { get; }
		IWebElement NewPaymentOption { get; }
		IWebElement PaymentFirstNameField { get; }
		IWebElement PaymentLastNameField { get; }
		IWebElement PaymentAddress1Field { get; }
		IWebElement PaymentAddress2Field { get; }
		IWebElement PaymentCityField { get; }
		IWebElement PaymentStateElement { get; }
		IWebElement PaymentPhoneField { get; }
		IWebElement PaymentTypeOptions { get; }
		IWebElement PaymentZipCodeField { get; }
		IWebElement PaymentCountryField { get; }
		IWebElement PayPalPaymentRadio { get; }
		IWebElement PlaceOrderButton { get; }
		IWebElement PlaceIntlOrderButton { get; }
		IWebElement PaymentSameAsShippingCheckBox { get; }
		IWebElement PaymentFirstNameErrorElement { get; }
		IWebElement PaymentLastNameErrorElement { get; }
		IWebElement PaymentAddressLine1ErrorElement { get; }
		IWebElement PaymentCityErrorElement { get; }
		IWebElement PaymentStateErrorElement { get; }
		IWebElement PaymentZipCodeErrorElement { get; }
		IWebElement PaymentPhoneErrorElement { get; }
		IWebElement PurchaseOrderRadioButton { get; }
		IWebElement PropWarningContainer { get; }
		IWebElement PurchaseOrderNumberField { get; }
		IWebElement SameAsShippingCheckBox { get; }
		IWebElement SameAsShippingEmployeeCheckBox { get; }
		IWebElement SavePaymentCheckbox { get; }
		IWebElement SavePaymentBox { get; }
		IWebElement SavePayment { get; }
		IWebElement WireTransferRadio { get; }
		List<IWebElement> PaymentTypeRadios { get; }
#pragma warning restore 1591
		#endregion

		/// <summary>
		/// Is the gift certificate container immediately visible?
		/// </summary>
		bool IsGiftCertContainerVisible { get; }

		/// <summary>
		/// Is the prop warning container immediately visible?
		/// </summary>
		bool IsPropWarningContainerVisible { get; }

		/// <summary>
		/// Is the saved payments element immediately visible?
		/// </summary>
		bool IsSavedPaymentsElementVisible { get; }

		/// <summary>
		/// Get the order total.
		/// </summary>
		string GetOrderTotal { get; }

		/// <summary>
		/// PayPal radio data-payment attribute.
		/// </summary>
		string PayPalRadioSelected { get; }

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

		IWebElement OrderTotalElement(int index);

		/// <summary>
		/// Select a month from the dropdown.
		/// </summary>
		/// <param name="month">Month to select.</param>
		void SelectMonth(string month);

		/// <summary>
		/// Select a year from the dropdown.
		/// </summary>
		/// <param name="year">Year to select.</param>
		void SelectYear(string year);

		/// <summary>
		/// Is the given payment type available?
		/// </summary>
		/// <param name="paymentType">Payment type to check availability of.</param>
		/// <returns></returns>
		bool IsPaymentTypeAvailable(string paymentType);

        /// <summary>
        /// Enter Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        /// <param name="address"></param>
        void EnterBillingAddress(Address address);

	    /// <summary>
	    /// Enter Billing Address information on Payment page.
	    /// </summary>
        void EnterIntBillingAddress();

        /// <summary>
        /// Enter Billing Address information on Payment page for international orders.
        /// </summary>
        /// <param name="address"></param>
        void EnterBillingAddressForIntlOrders(Address address);

	    /// <summary>
	    /// Enter Billing Address information for wire transfer on Payment page.
	    /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
	    /// </summary>
	    void EnterWireTransferBillingAddress(Address address);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

	    /// <summary>
	    /// Enter in credit card info
	    /// </summary>
	    /// <param name="creditCard"></param>
	    void EnterCreditCardInfo(CreditCard creditCard);

	    /// <summary>
	    /// Placing a interntional order work flow.
	    /// </summary>
	    void PlaceInternationalOrder();

		/// <summary>
		/// Get Order Id on billing page
		/// </summary>
		/// <returns></returns>
		string GetOrderId();
	}
}