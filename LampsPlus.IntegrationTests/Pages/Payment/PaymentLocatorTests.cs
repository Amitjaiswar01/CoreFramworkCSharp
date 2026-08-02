using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Payment
{
	public class PaymentLocatorDesktopTest : PaymentLocatorTests
	{
		public PaymentLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Integration.PageObjectModel, "Payment")]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void LocatePaymentElementsTest(string config) => Locate(config);

		protected override void VerifyBillingPageElements()
		{
			VerifyElementDisplayed(() => Payment.EditLink);
			VerifyElementDisplayed(() => Payment.CartBreadcrumb);
			VerifyElementDisplayed(() => Payment.CardExpirationYearErrorElement);
			VerifyElementDisplayed(() => Payment.CreditCartRadio);
            VerifyElementDisplayed(() => Payment.ReviewYourOrderLink);

            Payment.ReviewYourOrderLink.Click();

            VerifyElementDisplayed(() => Payment.ReviewOrderQty);
            VerifyElementDisplayed(() => Payment.ReviewOrderSku);
            VerifyElementDisplayed(() => Payment.ReviewOrderSummary);

			VerifyElementNotImplemented(() => Payment.CardYearView29ErrorElement);
			VerifyElementNotImplemented(() => Payment.CreditCardOptionButtonElement);
			VerifyElementNotImplemented(() => Payment.Field);
			VerifyElementNotImplemented(() => Payment.PaymentEditLinkElement);
			VerifyElementNotImplemented(() => Payment.SavePaymentCheckBoxElement);
			VerifyElementNotImplemented(() => Payment.SavePaymentCheckElement);
			VerifyElementNotImplemented(() => Payment.PaymentOptionTab);
			VerifyElementNotImplemented(() => Payment.PaymentOptionTabs);
			VerifyElementNotImplemented(() => Payment.NewBillingAddressFormFullContent);
            VerifyElementNotImplemented(() => Payment.EditPaymentDetails);

			Browser.Navigate(Urls.HomePageUrl);
			SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);
			Home.EnterStoreInSession("0");

			ShoppingCartWorkflow.EmptyCart();
			ShoppingCartWorkflow.EmployeeCheckoutWithSingleItem(ProductActions.GetSkuGreaterThanTwoHundredDollars);
            var userAccountUnderTest = LampsPlusAccounts.CustomerServiceRegularLoginAccount;
            CustomerAddressInformation.EnterShippingAddress(new Address { Country = CountryCodeList.US, State = StateCodeListUnitedStates.CA, Email = userAccountUnderTest.UserName });  
			ShoppingCartWorkflow.ProceedToPayment();

			VerifyElementDisplayed(() => Payment.PaymentTypeRadios);

			//Gift Card elements
			VerifyElementDisplayed(() => Payment.GiftCertificateNumber);
			VerifyElementDisplayed(() => Payment.GiftCertificateAmount);
			VerifyElementDisplayed(() => Payment.GiftCartApplyButton);

            if (Payment.SameAsShippingCheckBox.Selected) { Payment.SameAsShippingCheckBox.Click(); }
            VerifyElementDisplayed(() => Payment.SameAsShippingCheckBox);

            Browser.Wait.ForDisplayedElement(Payment.ChangeCountryLinkEmployeeView28Element);
			VerifyElementDisplayed(() => Payment.ChangeCountryLinkEmployeeView28Element);
			Payment.ChangeCountryLinkEmployeeView28Element.Click();

			VerifyElementDisplayed(() => Payment.BillingCountryEmployeeElement);

			Browser.Locate.ClickDropdownByValue(Payment.BillingCountryEmployeeElement, CountryCodeList.GB);

			Browser.Wait.ForDisplayedElement(Payment.InternationalAgreeCheckbox);
			VerifyElementDisplayed(() => Payment.InternationalAgreeCheckbox);

			Payment.InternationalAgreeCheckbox.Click();
			VerifyElementDisplayed(() => Payment.PlaceIntlOrderButton);

			Browser.Locate.ClickDropdownByValue(Payment.PaymentCountryField, CountryCodeList.US);

			//Wire Transfer elements
			Payment.WireTransferRadio.Click();
			VerifyElementDisplayed(() => Payment.WireTransferRadio);

            Browser.Wait.ForDisplayedElement(Payment.SameAsShippingCheckBox);

            if (Payment.SameAsShippingCheckBox.Selected) { Payment.SameAsShippingCheckBox.Click(); }

            VerifyElementDisplayed(() => Payment.PaymentFirstNameField);
			VerifyElementDisplayed(() => Payment.PaymentLastNameField);
			VerifyElementDisplayed(() => Payment.PaymentAddress1Field);
			VerifyElementDisplayed(() => Payment.PaymentAddress2Field);
			VerifyElementDisplayed(() => Payment.PaymentCityField);
			VerifyElementDisplayed(() => Payment.PaymentStateElement);
			VerifyElementDisplayed(() => Payment.PaymentPhoneField);
			VerifyElementDisplayed(() => Payment.PaymentZipCodeField);

			Browser.Wait.ForDisplayedElement(Payment.ChangeCountryLinkEmployeeElement);
			VerifyElementDisplayed(() => Payment.ChangeCountryLinkEmployeeElement);
			Payment.ChangeCountryLinkEmployeeElement.Click();

			VerifyElementDisplayed(() => Payment.PaymentCountryField);
            VerifyElementDisplayed(() => Payment.SameAsShippingCheckBox);

            //Purchase order elements
            Payment.PurchaseOrderRadioButton.Click();
			VerifyElementDisplayed(() => Payment.PurchaseOrderRadioButton);
			VerifyElementDisplayed(() => Payment.PurchaseOrderNumberField);

			//Check order elements
			Payment.CheckRadio.Click();
			VerifyElementDisplayed(() => Payment.CheckRadio);
			VerifyElementDisplayed(() => Payment.CheckNumberField);

			Browser.Navigate(Urls.CartOverviewPageUrl);

			ShoppingCartWorkflow.EmptyCart();
			ShoppingCartWorkflow.EmployeeCheckoutWithSingleItem();
            CustomerAddressInformation.EnterShippingAddress(new Address { Country = CountryCodeList.US, State = StateCodeListUnitedStates.CA, Email = userAccountUnderTest.UserName }); 
            ShoppingCartWorkflow.ProceedToPayment();

            if (Payment.SameAsShippingCheckBox.Selected) { Payment.SameAsShippingCheckBox.Click(); }

            Browser.Wait.ForDisplayedElement(Payment.ChangeCountryLinkEmployeeView28Element);
			Payment.ChangeCountryLinkEmployeeView28Element.Click();

			Browser.Locate.ClickDropdownByValue(Payment.BillingCountryEmployeeElement, CountryCodeList.GB);

			Browser.Wait.ForDisplayedElement(Payment.MinimumOrderError);
			VerifyElementDisplayed(() => Payment.MinimumOrderError);

			Browser.Navigate(Urls.HomePageUrl);
			SignInWorkflow.SignOut();
			SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);

			Browser.Navigate(Urls.ManagePaymentOptionsPageUrl);
			ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
            Browser.Navigate(Urls.ManagePaymentOptionsPageUrl);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();

			ManageAccountWorkflow.DeleteAllSavedAddresses();

			ShoppingCartWorkflow.EmptyCart();
			ShoppingCartWorkflow.CheckoutWithSingleItem();
            CustomerAddressInformation.EnterShippingAddress(new Address { Country = CountryCodeList.US, State = StateCodeListUnitedStates.CA, Email = userAccountUnderTest.UserName }); 
            ShoppingCartWorkflow.ProceedToPayment();

			VerifyElementDisplayed(() => Payment.DetailsLink);
			VerifyElementDisplayed(() => Payment.NewPaymentOption);
			VerifyElementDisplayed(() => Payment.CardVerificationElement);

			Payment.NewPaymentOption.Click();

			Browser.Wait.ForDisplayedElement(Payment.SavePaymentCheckbox);

			VerifyElementDisplayed(() => Payment.SavePaymentCheckbox);

			Payment.DetailsLink.Click();

			VerifyElementDisplayed(() => Payment.SavePayment);
			VerifyElementDisplayed(() => Payment.DeletePayment);

			ManageAccountWorkflow.DeleteAllSavedPaymentOptions();

			Browser.Navigate(Urls.PaymentPageUrl);

			VerifyElementDisplayed(() => Payment.SavePaymentBox);
		}
	}


	public class PaymentLocatorMobileTest : PaymentLocatorTests
	{
		public PaymentLocatorMobileTest(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Integration.PageObjectModel, "Payment")]
		[SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void LocatePaymentElementsTest(string config) => Locate(config);

		protected override void VerifyBillingPageElements()
		{
            VerifyElementDisplayed(() => Payment.CreditCardOptionButtonElement);
            VerifyElementDisplayed(() => Payment.EditLink);
			VerifyElementDisplayed(() => Payment.PaymentOptionTab);
			VerifyElementDisplayed(() => Payment.PaymentOptionTabs);
			VerifyElementDisplayed(() => Payment.NewBillingAddressFormFullContent);

            VerifyElementNotImplemented(() => Payment.ReviewYourOrderLink);
            VerifyElementNotImplemented(() => Payment.ReviewOrderQty);
            VerifyElementNotImplemented(() => Payment.ReviewOrderSku);
            VerifyElementNotImplemented(() => Payment.ReviewOrderSummary);

			SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);

			Browser.Navigate(Urls.ManagePaymentOptionsPageUrl);
			ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
            Browser.Navigate(Urls.ManagePaymentOptionsPageUrl);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();

			ManageAccountWorkflow.DeleteAllSavedAddresses();

            ShoppingCartWorkflow.EmptyCart();
			ShoppingCartWorkflow.CheckoutWithSingleItem();
            var userAccountUnderTest = LampsPlusAccounts.CustomerServiceRegularLoginAccount;
            CustomerAddressInformation.EnterShippingAddress(new Address { Country = CountryCodeList.US, State = StateCodeListUnitedStates.CA, Email = userAccountUnderTest.UserName }); 
            ShoppingCartWorkflow.ProceedToPayment();

            VerifyElementDisplayed(() => Payment.Field);
            VerifyElementDisplayed(() => Payment.CardVerificationElement);
            VerifyElementDisplayed(() => Payment.PaymentEditLinkElement);
			VerifyElementDisplayed(() => Payment.NewPaymentOption);

            Payment.NewPaymentOption.Click();

			Browser.Wait.ForDisplayedElement(Payment.SavePaymentCheckElement);

			VerifyElementDisplayed(() => Payment.SavePaymentCheckElement);

            Payment.PaymentEditLinkElement.Click();

            Browser.ScrollToBottomOfWindow();

            VerifyElementDisplayed(() => Payment.EditPaymentDetails);
			VerifyElementDisplayed(() => Payment.SavePayment);
			VerifyElementDisplayed(() => Payment.DeletePayment);

			ManageAccountWorkflow.DeleteAllSavedPaymentOptions();

			Browser.Navigate(Urls.PaymentPageUrl);
			VerifyElementDisplayed(() => Payment.SavePaymentCheckBoxElement);

            Payment.SameAsShippingCheckBoxGeneric.Click();

            Browser.MoveToAndClickElement(Payment.ChangeCountryLinkElement);
            CustomerAddressInformation.EnterNewCountryValueOnPaymentPage(Payment.BillingCountryElement, new Address { Country = CountryCodeList.GB });
            
            Browser.Wait.ForDisplayedElement(Payment.InternationalAgreeCheckbox);
            VerifyElementDisplayed(() => Payment.InternationalAgreeCheckbox);
            VerifyElementDisplayed(() => Payment.PlaceIntlOrderButton);
            VerifyElementDisplayed(() => Payment.PaymentFirstNameField);
            VerifyElementDisplayed(() => Payment.PaymentLastNameField);
            VerifyElementDisplayed(() => Payment.PaymentAddress1Field);
            VerifyElementDisplayed(() => Payment.PaymentAddress2Field);
            VerifyElementDisplayed(() => Payment.PaymentCityField);
            VerifyElementDisplayed(() => Payment.PaymentStateElement);
            VerifyElementDisplayed(() => Payment.PaymentPhoneField);
            VerifyElementDisplayed(() => Payment.PaymentZipCodeField);

            VerifyElementNotImplemented(() => Payment.CardYearView29ErrorElement);
            VerifyElementNotImplemented(() => Payment.BillingCountryEmployeeElement);
            VerifyElementNotImplemented(() => Payment.CartBreadcrumb);
            VerifyElementNotImplemented(() => Payment.CardExpirationYearErrorElement);
            VerifyElementNotImplemented(() => Payment.ChangeCountryLinkEmployeeView28Element);
            VerifyElementNotImplemented(() => Payment.ChangeCountryLinkEmployeeElement);
            VerifyElementNotImplemented(() => Payment.CheckNumberField);
			VerifyElementNotImplemented(() => Payment.CheckRadio);
			VerifyElementNotImplemented(() => Payment.CreditCartRadio);
			VerifyElementNotImplemented(() => Payment.DetailsLink);
            VerifyElementNotImplemented(() => Payment.GiftCertificateNumber);
			VerifyElementNotImplemented(() => Payment.GiftCertificateAmount);
			VerifyElementNotImplemented(() => Payment.GiftCartApplyButton);
            VerifyElementNotImplemented(() => Payment.MinimumOrderError);
            VerifyElementNotImplemented(() => Payment.PaymentCountryField);
            VerifyElementsNotImplemented(() => Payment.PaymentTypeRadios);
            VerifyElementNotImplemented(() => Payment.PurchaseOrderRadioButton);
			VerifyElementNotImplemented(() => Payment.PurchaseOrderNumberField);
            VerifyElementNotImplemented(() => Payment.SameAsShippingCheckBox);            
            VerifyElementNotImplemented(() => Payment.SavePaymentCheckbox);
			VerifyElementNotImplemented(() => Payment.SavePaymentBox);
			VerifyElementNotImplemented(() => Payment.WireTransferRadio);
		}
	}


	/// <summary>
	/// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
	/// </summary>
	[Trait(LpTraits.Integration.PageObjectModel, "Payment")]
	public abstract class PaymentLocatorTests : PageObjectTestsBase
	{
		protected PaymentLocatorTests(ITestOutputHelper output) : base(output) { }

		public void Locate(string config)
		{
			InitializeFramework(config);

			BuildElementsList(Payment);

			ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();
            
            Payment.SameAsShippingCheckBoxGeneric.Click();
            Browser.Wait.ForElementToStopAnimating(Payment.BillingElement);
            VerifyElementDisplayed(() => Payment.BillingElement);

            VerifyElementDisplayed(() => Payment.BillingFirstNameElement);
			VerifyElementDisplayed(() => Payment.BillingLastNameElement);
			VerifyElementDisplayed(() => Payment.BillingAddressLine1Element);
			VerifyElementDisplayed(() => Payment.BillingAddressLine2Element);
			VerifyElementDisplayed(() => Payment.ChangeCountryLinkElement);

			Payment.ChangeCountryLinkElement.Click();
			Browser.MoveToAndClickElement(Payment.PlaceOrderButton);

			VerifyElementDisplayed(() => Payment.BillingCountryElement);
			VerifyElementDisplayed(() => Payment.BillingCityElement);
			VerifyElementDisplayed(() => Payment.BillingStateElement);
			VerifyElementDisplayed(() => Payment.BillingZipElement);
			VerifyElementDisplayed(() => Payment.BillingPhoneElement);
			VerifyElementDisplayed(() => Payment.CardCodeField);
			VerifyElementDisplayed(() => Payment.CardNumberErrorElement);
			VerifyElementDisplayed(() => Payment.CardSecurityCodeErrorElement);
			VerifyElementDisplayed(() => Payment.CardExpirationMonthErrorElement);
			VerifyElementDisplayed(() => Payment.CardNameErrorElement);
			VerifyElementDisplayed(() => Payment.CartPaymentPage);
			VerifyElementDisplayed(() => Payment.CreditCardField);
			VerifyElementDisplayed(() => Payment.PayPalPaymentRadio);
			VerifyElementDisplayed(() => Payment.SameAsShippingCheckBoxContainer);
			VerifyElementDisplayed(() => Payment.SameAsShippingCheckBox);
			VerifyElementDisplayed(() => Payment.SameAsShippingCheckBoxGeneric);
			VerifyElementDisplayed(() => Payment.PaymentTypeOptions);
			VerifyElementDisplayed(() => Payment.PaymentFirstNameErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentLastNameErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentAddressLine1ErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentCityErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentStateErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentZipCodeErrorElement);
			VerifyElementDisplayed(() => Payment.PaymentPhoneErrorElement);
			VerifyElementDisplayed(() => Payment.PlaceOrderButton);
			VerifyElementDisplayed(() => Payment.PropWarningContainer);
			VerifyElementDisplayed(() => Payment.NameField);

			VerifyBillingPageElements();
		}

		protected abstract void VerifyBillingPageElements();
	}
}
