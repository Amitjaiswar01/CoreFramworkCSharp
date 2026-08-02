using System;
using System.Collections.Generic;
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
	public abstract class BillingBase : Page, IBilling
	{
		#region Class Setup
#pragma warning disable 1591
		/// <summary>
		/// <see cref="Desktop.CustomerInformation"/>
		/// </summary>
		public ICustomerInformation CustomerInformation;
#pragma warning restore 1591
		#endregion

		#region CSS Selector Strings
#pragma warning disable 1591
		public static string AgreeIntlOrderClass => "agreeIntlOrder";
		public static string CardCodeView29ErrorId => "cardCode-view29-error";
		public static string CardFullNameView29ErrorId => "cardFullName-view29-error";
		public static string CardMonthView29ErrorId => "cardMonth-view29-error";
		public static string CardNumberClass => "cardNumber";
		public static string CardNumberView29ErrorId => "cardNumber-view29-error";
        public static string CardVerificationClass => "cardVerification";
        public static string CardYearView29ErrorId => "cardYear-view29-error";
		public static string CartBreadcrumbId => "cartBreadcrumb";
		public static string CartPaymentClass => "cartPayment";
		public static string CreditCvvClass => "creditCvv";
        public static string DeleteIdClass => "deleteId";
		public static string ExpMonthClass => "expMonth";
		public static string ExpYearClass => "expYear";
		public static string FormWireTransferClassName => "formWireTransfer";
		public static string GiftCertContainerId => "giftCertContainer";
		public static string GiftCertNumId => "giftCertNum";
		public static string GiftCertAmountId => "giftCertAmount";
		public static string GiftCertApplyId => "giftCertApply";
		public static string MinimumOrderErrorClass => "minimumOrderError";
		public static string NameOnCardClass => "nameOnCard";
		public static string NewPaymentOptionClass => "addNewPaymentOption";
		public static string OsValueClass => "osValue";
		public static string PaymentInfoAddress1Id => "paymentInfoAddress1";
		public static string PaymentInfoAddress2Id => "paymentInfoAddress2";
		public static string PaymentInfoCheckNumId => "paymentInfoCheckNum";
		public static string PaymentInfoCityId => "paymentInfoCity";
		public static string PaymentInfoFirstNameId => "paymentInfoFirstName";
		public static string PaymentInfoLastNameId => "paymentInfoLastName";
		public static string PaymentInfoPhoneId => "paymentInfoPhone";
		public static string PaymentInfoStateId => "paymentInfoState";
		public static string PaymentInfoZipCodeId => "paymentInfoZipCode";
		public static string PaymentInfoCountryId => "paymentInfoCountry";
		public static string PaymentTypeOptionsId => "paymentTypeOptions";
		public static string PaymentTypeRadioClass => "paymentTypeRadio";
		public static string PlaceYourIntlOrderButtonId => "placeYourIntlOrderButton";
		public static string PropWarningClass => "propWarning";
		public static string PlaceYourOrderButtonId => "placeYourOrderButton";
		public static string PurchaseOrderNumberClass => "purchaseOrderNumber";
		public static string SameAsShippingView29Id => "sameAsShipping-view29";
		public static string SameAsShippingView28Id => "sameAsShipping-view28";
		public static string SameAsShippingCheckboxId => "sameAsShippingCheckbox";
		public static string SavePaymentClass => "savePaymentId";
		public static string SavePaymentsWithFormClass => "savedPaymentsWithForm";
		public static string SavePaymentId => "savePayment-view29";
		public static string ShowCountryFieldClass => "showCountryFieldId";
	    public static string ShowCountryFieldView27Id => "showCountryField-view27";
        public static string ShowCountryFieldView28Id => "showCountryField-view28";
		public static string ShowCountryFieldView29Id => "showCountryField-view29";
		public static string SingleShippingFirstNameView29Id => "singleShippingFirstName-view29";
		public static string SingleShippingFirstNameView29ErrorId => "singleShippingFirstName-view29-error";
		public static string SingleShippingLastNameView29Id => "singleShippingLastName-view29";
		public static string SingleShippingLastNameView29ErrorId => "singleShippingLastName-view29-error";
		public static string SingleShippingAddress1View29Id => "singleShippingAddress1-view29";
		public static string SingleShippingAddress1View29ErrorId => "singleShippingAddress1-view29-error";
		public static string SingleShippingAddress2View29Id => "singleShippingAddress2-view29";
		public static string SingleShippingCountryView28Id => "singleShippingCountry-view28";
		public static string SingleShippingCountryView29Id => "singleShippingCountry-view29";
		public static string SingleShippingCityView29Id => "singleShippingCity-view29";
		public static string SingleShippingCityView29ErrorId => "singleShippingCity-view29-error";
		public static string SingleShippingStateView29Id => "singleShippingState-view29";
		public static string SingleShippingStateView29ErrorId => "singleShippingState-view29-error";
		public static string SingleShippingZipCodeView29Id => "singleShippingZipCode-view29";
		public static string SingleShippingZipCodeView29ErrorId => "singleShippingZipCode-view29-error";
		public static string SingleShippingPhoneView29Id => "singleShippingPhone-view29";
		public static string SingleShippingPhoneView29ErrorId => "singleShippingPhone-view29-error";
		public static string UpdateIdClass => "updateId";

		public static string OrderIdStartToken => "orderId: '";
		public static int OrderIdLength => 21;
#pragma warning restore 1591
		#endregion

		#region Page Elements
#pragma warning disable 1591
		public IWebElement BillingFirstNameElement => Browser.Locate.ElementById(SingleShippingFirstNameView29Id);
		public IWebElement BillingLastNameElement => Browser.Locate.ElementById(SingleShippingLastNameView29Id);
		public IWebElement BillingAddressLine1Element => Browser.Locate.ElementById(SingleShippingAddress1View29Id);
		public IWebElement BillingAddressLine2Element => Browser.Locate.ElementById(SingleShippingAddress2View29Id);
		public IWebElement BillingCountryEmployeeElement => Browser.Locate.ElementById(SingleShippingCountryView28Id);
		public IWebElement BillingCountryElement => Browser.Locate.ElementById(SingleShippingCountryView29Id);
		public IWebElement BillingCityElement => Browser.Locate.ElementById(SingleShippingCityView29Id);
		public IWebElement BillingStateElement => Browser.Locate.ElementById(SingleShippingStateView29Id);
		public IWebElement BillingZipElement => Browser.Locate.ElementById(SingleShippingZipCodeView29Id);
		public IWebElement BillingPhoneElement => Browser.Locate.ElementById(SingleShippingPhoneView29Id);
		public IWebElement BreadcrumbShippingLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, CartBreadcrumb);
		public IWebElement CartBreadcrumb => Browser.Locate.ElementById(CartBreadcrumbId);
        public IWebElement CardCodeField => Browser.Locate.ElementByClassName(CreditCvvClass);
		public IWebElement CardNumberErrorElement => Browser.Locate.ElementById(CardNumberView29ErrorId);
		public IWebElement CardSecurityCodeErrorElement => Browser.Locate.ElementById(CardCodeView29ErrorId);
		public IWebElement CardExpirationMonthErrorElement => Browser.Locate.ElementById(CardMonthView29ErrorId);
		public IWebElement CardExpirationYearErrorElement => Browser.Locate.ElementById(CardYearView29ErrorId);
		public IWebElement CardNameErrorElement => Browser.Locate.ElementById(CardFullNameView29ErrorId);
        public IWebElement CardVerificationElement => Browser.Locate.ElementByClassName(CardVerificationClass);
	    public IWebElement ChangeCountryLinkEmployeeView27Element => Browser.Locate.ElementById(ShowCountryFieldView27Id);
        public IWebElement ChangeCountryLinkElement => Browser.Locate.ElementById(ShowCountryFieldView29Id);
		public IWebElement ChangeCountryLinkEmployeeView28Element => Browser.Locate.ElementById(ShowCountryFieldView28Id);
		public IWebElement ChangeCountryLinkEmployeeElement => Browser.Locate.ElementByClassName(ShowCountryFieldClass, Browser.Locate.ElementByClassName(FormWireTransferClassName));
		public IWebElement CartPaymentPage => Browser.Locate.ElementByClassName(CartPaymentClass);
		public IWebElement CheckNumberField => Browser.Locate.ElementById(PaymentInfoCheckNumId);
		public IWebElement CheckRadio => Browser.Locate.ElementsByClassName(PaymentTypeRadioClass)[3];
		public IWebElement CreditCardField => Browser.Locate.ElementByClassName(CardNumberClass);
		public IWebElement CreditCartRadio => Browser.Locate.ElementByClassName(PaymentTypeRadioClass);
		public IWebElement DetailsLink => Browser.Locate.ElementByClassName(OrderDetailsBase.DetailsClass);
		public IWebElement DeletePayment => Browser.Locate.ElementByClassName(DeleteIdClass);
		public IWebElement GiftCertificateNumber => Browser.Locate.ElementById(GiftCertNumId);
		public IWebElement GiftCertificateAmount => Browser.Locate.ElementById(GiftCertAmountId);
		public IWebElement GiftCartApplyButton => Browser.Locate.ElementById(GiftCertApplyId);
		public IWebElement InternationalAgreeCheckbox => Browser.Locate.ElementByClassName(AgreeIntlOrderClass);
		public IWebElement MinimumOrderError => Browser.Locate.ElementByClassName(MinimumOrderErrorClass);
		public IWebElement NameField => Browser.Locate.ElementByClassName(NameOnCardClass);
		public IWebElement NewPaymentOption => Browser.Locate.ElementByClassName(NewPaymentOptionClass);
		public IWebElement OrderTotalElement(int index) => Browser.Locate.ElementsByClassName(OsValueClass)[index];
		public IWebElement PaymentFirstNameField => Browser.Locate.ElementById(PaymentInfoFirstNameId);
		public IWebElement PaymentLastNameField => Browser.Locate.ElementById(PaymentInfoLastNameId);
		public IWebElement PaymentAddress1Field => Browser.Locate.ElementById(PaymentInfoAddress1Id);
		public IWebElement PaymentAddress2Field => Browser.Locate.ElementById(PaymentInfoAddress2Id);
		public IWebElement PaymentCityField => Browser.Locate.ElementById(PaymentInfoCityId);
		public IWebElement PaymentStateElement => Browser.Locate.ElementById(PaymentInfoStateId);
		public IWebElement PaymentPhoneField => Browser.Locate.ElementById(PaymentInfoPhoneId);
		public IWebElement PaymentTypeOptions => Browser.Locate.ElementById(PaymentTypeOptionsId);
		public IWebElement PaymentZipCodeField => Browser.Locate.ElementById(PaymentInfoZipCodeId);
		public IWebElement PaymentCountryField => Browser.Locate.ElementById(PaymentInfoCountryId);
		public IWebElement PayPalPaymentRadio => Browser.Locate.ElementByClassName(ActiveClass, PaymentTypeOptions);
		public IWebElement PlaceOrderButton => Browser.Locate.ElementById(PlaceYourOrderButtonId);
		public IWebElement PlaceIntlOrderButton => Browser.Locate.ElementById(PlaceYourIntlOrderButtonId);
		public IWebElement PaymentSameAsShippingCheckBox => Browser.Locate.ElementById(SameAsShippingCheckboxId);
        public IWebElement PaymentFirstNameErrorElement => Browser.Locate.ElementById(SingleShippingFirstNameView29ErrorId);
		public IWebElement PaymentLastNameErrorElement => Browser.Locate.ElementById(SingleShippingLastNameView29ErrorId);
		public IWebElement PaymentAddressLine1ErrorElement => Browser.Locate.ElementById(SingleShippingAddress1View29ErrorId);
		public IWebElement PaymentCityErrorElement => Browser.Locate.ElementById(SingleShippingCityView29ErrorId);
		public IWebElement PaymentStateErrorElement => Browser.Locate.ElementById(SingleShippingStateView29ErrorId);
		public IWebElement PaymentZipCodeErrorElement => Browser.Locate.ElementById(SingleShippingZipCodeView29ErrorId);
		public IWebElement PaymentPhoneErrorElement => Browser.Locate.ElementById(SingleShippingPhoneView29ErrorId);
		public IWebElement PurchaseOrderRadioButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Value, "PurchaseOrder");
		public IWebElement PropWarningContainer => Browser.Locate.ElementByClassName(PropWarningClass);
		public IWebElement PurchaseOrderNumberField => Browser.Locate.ElementByClassName((PurchaseOrderNumberClass));
		public IWebElement SameAsShippingCheckBox => Browser.Locate.ElementById(SameAsShippingView29Id);
		public IWebElement SameAsShippingEmployeeCheckBox => Browser.Locate.ElementById(SameAsShippingView28Id);
		public IWebElement SavePaymentCheckbox => Browser.Locate.ElementByClassName(SavePaymentClass);
		public IWebElement SavePaymentBox => Browser.Locate.ElementById(SavePaymentId);
		public IWebElement SavePayment => Browser.Locate.ElementByClassName(UpdateIdClass);
		public IWebElement WireTransferRadio => Browser.Locate.ElementsByClassName(PaymentTypeRadioClass)[1];

		public List<IWebElement> PaymentTypeRadios => Browser.Locate.ElementsByClassName(PaymentTypeRadioClass);
#pragma warning restore 1591
		#endregion

		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="browser"></param>
		/// <param name="customerInformation"></param>
		protected BillingBase(IBrowser browser, ICustomerInformation customerInformation) : base(browser) { CustomerInformation = customerInformation; }

		/// <summary>
		/// Select a month from the dropdown.
		/// </summary>
		/// <param name="month">Month to select.</param>
		public void SelectMonth(string month) { new SelectElement(Browser.Locate.ElementByClassName(ExpMonthClass)).SelectByText(month); }
	   
        /// <summary>
	    /// Select a month from the dropdown.
	    /// </summary>
	    /// <param name="month">Month to select.</param>
	    public void SelectMonthByValue(string month) { new SelectElement(Browser.Locate.ElementByClassName(ExpMonthClass)).SelectByValue(month); }
       
        /// <summary>
        /// Select a year from the dropdown.
        /// </summary>
        /// <param name="year">Year to select.</param>
        public void SelectYear(string year) { new SelectElement(Browser.Locate.ElementByClassName(ExpYearClass)).SelectByText(year); }

		/// <summary>
		/// Is the gift certificate container immediately visible?
		/// </summary>
		public bool IsGiftCertContainerVisible => Browser.Locate.ElementImmediatly(GiftCertContainerId.ToCssIdSelector()) != null;

		/// <summary>
		/// Is the prop warning container immediately visible?
		/// </summary>
		public bool IsPropWarningContainerVisible => Browser.Locate.ElementImmediatly(PropWarningClass.ToCssClassSelector()) != null;

		/// <summary>
		/// Is the saved payments element immediately visible?
		/// </summary>
		public bool IsSavedPaymentsElementVisible => Browser.Locate.ElementImmediatly(SavePaymentsWithFormClass.ToCssClassSelector()) != null;

		/// <summary>
		/// Get the order total.
		/// </summary>
		public string GetOrderTotal => OrderTotalElement(3).Text;

		/// <summary>
		/// PayPal radio data-payment attribute.
		/// </summary>
		public string PayPalRadioSelected => PayPalPaymentRadio.GetAttribute("data-payment");

		/// <summary>
		/// Is the given payment type available?
		/// </summary>
		/// <param name="paymentType">Payment type to check availability of.</param>
		/// <returns></returns>
		public bool IsPaymentTypeAvailable(string paymentType)
		{
			foreach (var paymentTypeRadio in PaymentTypeRadios)
			{
				var dataPaymentType = paymentTypeRadio.GetAttribute("value");

				if (dataPaymentType == paymentType)
				{
					return true;
				}
			}
			return false;
		}

        /// <summary>
        /// Enter Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        public void EnterBillingAddress(Address address)
        {
            BillingFirstNameElement.Clear();
            CustomerInformation.FillFormControlByText(BillingFirstNameElement, address.FirstName);

            BillingLastNameElement.Clear();
            CustomerInformation.FillFormControlByText(BillingLastNameElement, address.LastName);

            BillingAddressLine1Element.Clear();
            CustomerInformation.FillFormControlByText(BillingAddressLine1Element, address.AddressLine1);

            BillingAddressLine2Element.Clear();
            CustomerInformation.FillFormControlByText(BillingAddressLine2Element, address.AddressLine2);

            if (address.Country != "US")
            {
                ChangeCountryLinkElement.Click();
                CustomerInformation.FillFormSelectByValue(BillingCountryElement, address.Country);
            }

            BillingCityElement.Clear();
            CustomerInformation.FillFormControlByText(BillingCityElement, address.City);

            BillingPhoneElement.Clear();
            CustomerInformation.FillFormControlByText(BillingPhoneElement, address.Phone);

            BillingStateElement.Click();
            CustomerInformation.FillFormSelectByValue(BillingStateElement, address.State);
            BillingStateElement.Click();

            BillingZipElement.Clear();
            CustomerInformation.FillFormControlByText(BillingZipElement, address.ZipCode);
        }

	    /// <summary>
	    /// Enter International Billing Address information on Payment page.
	    /// </summary>
	    public void EnterIntBillingAddress()
	    {
	        var intAddress = new IntAddress();
	        ChangeCountryLinkElement.Click();
	        CustomerInformation.FillFormSelectByValue(BillingCountryElement, intAddress.Country);
	        EnterIntlBillingAddress(intAddress);
        }

	    /// <summary>
	    /// Enter Wire Transfer Billing Address information on Payment page.
	    /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
	    /// </summary>
	    public void EnterWireTransferBillingAddress(Address address)
	    {
	        PaymentFirstNameField.Clear();
	        CustomerInformation.FillFormControlByText(PaymentFirstNameField, address.FirstName);

	        PaymentLastNameField.Clear();
	        CustomerInformation.FillFormControlByText(PaymentLastNameField, address.LastName);

	        PaymentAddress1Field.Clear();
	        CustomerInformation.FillFormControlByText(PaymentAddress1Field, address.AddressLine1);

            PaymentAddress2Field.Clear();
	        CustomerInformation.FillFormControlByText(PaymentAddress2Field, address.AddressLine2);

	        if (address.Country != "US")
	        {
	            ChangeCountryLinkEmployeeView27Element.Click();
	            CustomerInformation.FillFormSelectByValue(BillingCountryElement, address.Country);
	        }

            PaymentStateElement.Click();
	        CustomerInformation.FillFormSelectByValue(PaymentStateElement, address.State);
	        PaymentStateElement.Click();

            PaymentCityField.Clear();
	        CustomerInformation.FillFormControlByText(PaymentCityField, address.City);

	        PaymentZipCodeField.Clear();
	        CustomerInformation.FillFormControlByText(PaymentZipCodeField, address.ZipCode);

	        PaymentPhoneField.Clear();
	        CustomerInformation.FillFormControlByText(PaymentPhoneField, address.Phone);
	    }

        /// <summary>
        /// Enter Billing Address information on Payment page for international orders.
        /// </summary>
        /// <param name="address"></param>
        public void EnterBillingAddressForIntlOrders(Address address)
        {
            EnterIntlBillingAddress(address);
            PaymentStateElement.Click();
            CustomerInformation.FillFormSelectByValue(PaymentStateElement, address.State);
        }

		/// <summary>
		/// Currently we  get order id from order confirmation page.
		/// This provides an alternative to get Order Id from decisionManager JS variable in HTML Source
		/// This is not currently in use, but we want to preserve this option.
		/// </summary>
		/// <returns></returns>
		public string GetOrderId()
		{
			var pageSource = Browser.PageSource;
			var start = pageSource.IndexOf(OrderIdStartToken, StringComparison.InvariantCultureIgnoreCase) + OrderIdStartToken.Length;
			var orderId = pageSource.Substring(start, OrderIdLength);
			return orderId;
		}

		/// <summary>
			/// Matches database value with front end billing information (credit card types)
			/// </summary>
			/// <param name="creditCardType"></param>
			/// <returns>Returns formatted credit card type description</returns>
			public static string GetCreditCardType(string creditCardType)
		{
			switch (creditCardType.ToUpper())
			{
				case "VISA":
					creditCardType = "VISA";
					break;
				case "DISC":
					creditCardType = "DISCOVER";
					break;
				case "MASTERCARD":
					creditCardType = "MASTERCARD";
					break;
				case "AMEX":
					creditCardType = "AMERICANEXPRESS";
					break;
			}

			return creditCardType;
		}

	    /// <inheritdoc />
        public void EnterCreditCardInfo(CreditCard creditCard)
	    {
	        CreditCardField.Clear();
	        CustomerInformation.FillFormControlByText(CreditCardField, creditCard.CardNumber);

	        CardCodeField.Clear();
	        CustomerInformation.FillFormControlByText(CardCodeField, creditCard.SecurityCode);

	        SelectMonthByValue(creditCard.ExpirationMonth.ToString());
	        SelectYear(creditCard.ExpirationYear.ToString());

	        NameField.Clear();
	        CustomerInformation.FillFormControlByText(NameField, creditCard.NameOnCard);

	    }

	    /// <inheritdoc />
        public void PlaceInternationalOrder()
	    {
	        if (!InternationalAgreeCheckbox.Selected)
	        {
	            InternationalAgreeCheckbox.Click();
	        }
	        Browser.Wait.ForDisplayedElement(PlaceIntlOrderButton);

	        PlaceIntlOrderButton.Click();
	    }

	    private void EnterIntlBillingAddress(Address address)
	    {
	        PaymentFirstNameField.Clear();
	        PaymentFirstNameField.SendKeys(address.FirstName);

	        PaymentLastNameField.Clear();
	        PaymentLastNameField.SendKeys(address.LastName);

	        PaymentAddress1Field.Clear();
	        PaymentAddress1Field.SendKeys(address.AddressLine1);

	        PaymentAddress2Field.Clear();
	        PaymentAddress2Field.SendKeys(address.AddressLine2);

	        PaymentCityField.Clear();
	        PaymentCityField.SendKeys(address.City);

	        PaymentZipCodeField.Clear();
	        PaymentZipCodeField.SendKeys(address.ZipCode);

	        PaymentPhoneField.Clear();
	        PaymentPhoneField.SendKeys(address.Phone);
	    }

	}
}
