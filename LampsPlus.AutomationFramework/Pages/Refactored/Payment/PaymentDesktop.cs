using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Payment
{
    public class PaymentDesktop : IPaymentDesktop
    {
        //Class Members
        private string _errorMessageString = "This field is required.";
        private string _pleaseMakeSelectionString = "Please make a selection";
        private string _proceedToPaymentClass = "proceedToPayment";
        private string _sameAsShippingControlClass = "sameAsShippingControl";
        private string _singleShippingPhoneId = "singleShippingPhone";
        private string _singleShippingFirstNameId = "singleShippingFirstName";
        private string _singleShippingLastNameId = "singleShippingLastName";
        private string _singleShippingAddress1Id  = "singleShippingAddress1";
        private string _singleShippingCityId = "singleShippingCity";
        private string _singleShippingStateId = "singleShippingState";
        private string _singleShippingZipCodeId = "singleShippingZipCode";
        private string _showCountryFieldId = "showCountryField";
        private string _creditCvvClass = "creditCvv";
        private string _expMonthClass = "expMonth";
        private string _expYearClass = "expYear";
        private string _nameOnCardClass = "nameOnCard";
        private string _cardFullNameId = "cardFullName";
        private string _cardCodeId = "cardCode";
        private string _cardNumberId = "cardNumber";
        private string _cardMonthId  = "cardMonth";
        private string _cardYearView29ErrorId = "cardYear-view30-error";
        private string _minimumOrderErrorClass = "minimumOrderError";
        private string _detailsClass = "details";
        private string _agreeIntlOrderId = "agreeIntlOrder";
        private string _placeYourOrderButtonId = "placeYourOrderButton";
        private string _savePaymentsWithFormClass  = "savedPaymentsWithForm";
        private string _agreeIntlOrderWrapperClass = "agreeIntlOrderWrapper";
        private string _deleteIdClass = "deleteId";
        private string _paymentTypeRadioClass = "paymentTypeRadio";
        private string _haveGiftCardClass = "haveGiftCard";
        private string _giftCertNumId = "giftCertNum";
        private string _propWarningClass = "propWarning";
        private string _cartBreadcrumbId = "cartBreadcrumb";
        protected string CardNumberClass => "cardNumber";
        protected string ShippingInfoId => "shippingInfo";
        protected string PaymentInfoContentClass => "paymentInfoContent";
        protected string FirstLineClass => "firstLine";
        protected string SecondLineClass => "secondLine";
        protected string ThirdLineClass  => "thirdLine";
        protected string PlaceYourIntlOrderButtonId => "placeYourIntlOrderButton";
        protected virtual string StreetAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} >  {HtmlTextWriterTag.Div.ToNthChildSelector(2)}").Text.TrimStart().ToLower();
        protected virtual string ApartmentText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.TrimStart().ToLower();
        protected virtual string CityTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',')[0].TrimStart().ToLower();
        protected virtual string StateTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',', ' ')[2].Trim().ToLower();
        protected virtual string ZipCodeTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',', ' ')[3].Trim().ToLower();
        protected virtual string StreetSuggestedAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} >  {HtmlTextWriterTag.Div.ToNthChildSelector(2)}").Text;
        protected virtual string ApartmentSuggestedAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text;
        protected virtual string CitySuggestedAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',')[0].TrimStart();
        protected virtual string StateSuggestedAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[2].Trim();
        protected virtual string ZipCodeSuggestedAddressText => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[3].Trim();

        private IElement MinimumOrderError => Browser.Locate.ElementByClassName(_minimumOrderErrorClass);
        private IElement WireTransferRadio => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "WireTransferPaymentType");
        private IElement CheckRadio => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "PaperCheckPaymentType");
        private IElement PurchaseOrderRadioButton => Browser.Locate.ElementByXpath("//label[@for='PurchaseOrderPaymentType']");
        private IElement CheckNumberField => Browser.Locate.ElementByXpath("//label[@for='paymentInfoCheckNum']");
        private IElement PropWarningContainer => Browser.Locate.ElementByClassName(_propWarningClass);
        private IElement CartBreadcrumb => Browser.Locate.ElementById(_cartBreadcrumbId);
        private IElement CardExpirationYearErrorElement => Browser.Locate.ElementById(_cardYearView29ErrorId);
        private IElement DeliveryPolicyAgreementProceedToPayment => Browser.Locate.ElementByClassName(_proceedToPaymentClass);
        private IElement SameAsShippingCheckBoxGeneric => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "sameAsShipping");
        private IElement BillingPhoneElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingPhoneId);
        private IElement PlaceOrderButton => Browser.Locate.ElementById(_placeYourOrderButtonId);
        private ReadOnlyCollection<IElement> PaymentTypeRadios => Browser.Locate.ElementsByClassName(_paymentTypeRadioClass);

        protected IElement DeletePayment => Browser.Locate.ElementByClassName(_deleteIdClass);
        protected IElement PaymentFirstNameErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingFirstNameId);
        protected IElement PaymentLastNameErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingLastNameId);
        protected IElement PaymentAddressLine1ErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingAddress1Id);
        protected IElement PaymentCityErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingCityId);
        protected IElement PaymentStateErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingStateId);
        protected IElement PaymentZipCodeErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _singleShippingZipCodeId);
        protected IElement CardNameErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _cardFullNameId);
        protected IElement CardNumberErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _cardNumberId);
        protected IElement CardSecurityCodeErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _cardCodeId);
        protected IElement PlaceInternationalOrderButton => Browser.Locate.ElementById(PlaceYourIntlOrderButtonId);
        protected IElement CreditCardField => Browser.Locate.ElementByClassName(CardNumberClass);
        protected IElement CardCodeField => Browser.Locate.ElementByClassName(_creditCvvClass);
        protected IElement NameField => Browser.Locate.ElementByClassName(_nameOnCardClass);
        protected virtual IElement IAgreeCheckBox => Browser.Locate.ElementByXpath("//*[@id='agreeIntlOrder']");
        protected virtual IElement CardExpirationMonthErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _cardMonthId);
        protected virtual IElement MyAddressIsOutsideTheUsLink => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, _showCountryFieldId);
        protected virtual IElement DetailsLink => Browser.Locate.ElementByClassName(_detailsClass);
        protected virtual IElement GiftCardLink => Browser.Locate.ElementByClassName(_haveGiftCardClass);
        protected virtual IElement ShippingHeaderLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, CartBreadcrumb);

        private bool IsDeletePaymentButtonVisible(int timeToWait)
        {
            return Browser.Wait.IsInvisibleElement(By.CssSelector(_deleteIdClass.ToCssClassSelector()), timeToWait);
        }

        private void ClickOnInternationalPlaceOrderButton()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(PlaceYourIntlOrderButtonId.ToCssIdSelector()));
            PlaceInternationalOrderButton.Click();
        }

        protected virtual void ClickIAgreeCheckBox()
        {
            IAgreeCheckBox.Click();
        }

        private void SelectMonthByValue(string month) { new SelectElement(Browser.Locate.ElementByClassName(_expMonthClass).InternalElement).SelectByValue(month); }
        private void SelectYear(string year) { new SelectElement(Browser.Locate.ElementByClassName(_expYearClass).InternalElement).SelectByText(year); }

        //Instances
        protected IBrowser Browser;
        protected readonly IAssert Assert;
        private readonly IModalDesktop _modal;

        public PaymentDesktop(IBrowser browser, IAssert assert, IModalDesktop modal)
        {
            Browser = browser;
            Assert = assert;
            _modal = modal;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_placeYourOrderButtonId.ToCssIdSelector()));
        public bool IsMinimumOrderMessageVisible => Browser.Wait.IsVisibleElement(By.CssSelector(_minimumOrderErrorClass.ToCssClassSelector()));
        public bool IsMinimumOrderErrorMessageVisible => MinimumOrderError.Displayed;
        public bool IsProp65WarningDialogVisible => PropWarningContainer.Displayed;
        public bool IsSavedPaymentsElementVisible => Browser.Locate.ElementImmediately(_savePaymentsWithFormClass.ToCssClassSelector()).IsInitialized;
        public virtual bool IsInternationalCheckboxDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_agreeIntlOrderWrapperClass));

        public Dictionary<string, string> GetAddressTextWithApartmentFieldActive => new Dictionary<string, string>
        {
            { "StreetAddressText", StreetAddressText },
            { "ApartmentText", ApartmentText },
            { "CityTextWithApartmentFieldActive", CityTextWithApartmentFieldActive },
            { "StateTextWithApartmentFieldActive", StateTextWithApartmentFieldActive },
            { "ZipCodeTextWithApartmentFieldActive", ZipCodeTextWithApartmentFieldActive },
        };

        public Dictionary<string, string> GetSuggestedAddressText => new Dictionary<string, string>
        {
            { "StreetSuggestedAddressText", StreetSuggestedAddressText },
            { "ApartmentSuggestedAddressText", ApartmentSuggestedAddressText },
            { "CitySuggestedAddressText", CitySuggestedAddressText },
            { "StateSuggestedAddressText", StateSuggestedAddressText },
            { "ZipCodeSuggestedAddressText", ZipCodeSuggestedAddressText },
        };

        public void FillFormControlByText(IElement formControl, string text)
        {
            switch (formControl.TagName)
            {
                case "select":
                {
                    new SelectElement(formControl.InternalElement).SelectByValue(text);
                    break;
                }
                case "input":
                {
                    formControl.SendKeys(text);
                    break;
                }
            }
        }

        public void SelectDeliveryPolicyAgreementIfVisible()
        {
            Browser.Wait.ForCondition(() => Browser.Wait.ForPageWait(Urls.PaymentPageUrl) || Browser.Wait.ForPageWait(Urls.ShippingNotificationPageUrl), 30);

            if (Browser.PageUrl != Urls.ShippingNotificationPageUrl) return;
            DeliveryPolicyAgreementProceedToPayment.Click();
            Assert.True(IsCurrentPage, "Current page is not Payment page");
        }

        public void SelectSameAsShippingCheckbox()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_sameAsShippingControlClass.ToCssClassSelector()));
            SameAsShippingCheckBoxGeneric.Click();
            Browser.Wait.ForDisplayedElement(BillingPhoneElement);
        }

        public void PlaceOrder()
        {
            Assert.True(IsCurrentPage, "Current page is not Payment page");
            Browser.Wait.ForClickableElement(PlaceOrderButton).Click();
        }

        public virtual void EnterCreditCartInformation(CreditCard creditCard)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(CardNumberClass));
            CreditCardField.Click();
            CreditCardField.Clear();
            FillFormControlByText(CreditCardField, creditCard.CardNumber);

            CardCodeField.Clear();
            FillFormControlByText(CardCodeField, creditCard.SecurityCode);

            SelectMonthByValue(creditCard.ExpirationMonth.ToString());
            SelectYear(creditCard.ExpirationYear.ToString());

            NameField.Clear();
            FillFormControlByText(NameField, creditCard.NameOnCard);
        }

        public virtual Dictionary<string, string> GetPaymentPageFreeTypeFieldErrorMessages(int numberOfFreeTypeFieldsOnPaymentPage)
        {
            var fieldNameAndErrorMessage = new Dictionary<string, string>();

            for (var fieldIndex = 0; fieldIndex <= numberOfFreeTypeFieldsOnPaymentPage; fieldIndex++)
            {
                var errorMessage = GetListOfPaymentPageFreeTypeFields()[fieldIndex].Text;
                var fieldName = GetListOfPaymentPageFreeTypeFields()[fieldIndex].LocatorString;
                fieldNameAndErrorMessage.Add(fieldName, errorMessage);
            }

            return fieldNameAndErrorMessage;
        }

        public virtual Dictionary<string, string> GetPaymentPageDropdownFieldErrorMessages()
        {
            var fieldNameAndErrorMessage = new Dictionary<string, string>();

            var errorMessage = GetListOfPaymentPageDropdownFields()[0].Text;
            var fieldName = GetListOfPaymentPageDropdownFields()[0].LocatorString;
            fieldNameAndErrorMessage.Add(fieldName, errorMessage);
            
            return fieldNameAndErrorMessage;
        }

        public virtual List<IElement> GetListOfPaymentPageFreeTypeFields()
        {
            var paymentFreeTypeFields = new List<IElement>
            {
                PaymentFirstNameErrorElement,
                PaymentLastNameErrorElement,
                PaymentAddressLine1ErrorElement,
                PaymentCityErrorElement,
                PaymentZipCodeErrorElement,
                CardNumberErrorElement,
                CardSecurityCodeErrorElement,
                CardNameErrorElement
            };

            return paymentFreeTypeFields;
        }

        public virtual List<IElement> GetListOfPaymentPageDropdownFields()
        {
            var paymentDropdownFields = new List<IElement>
            {
                PaymentStateErrorElement,
                CardExpirationMonthErrorElement,
                CardExpirationYearErrorElement,
            };

            return paymentDropdownFields;
        }

        public string GetPaymentFieldErrorMessage()
        {
            return _errorMessageString;
        }

        public string GetDropdownFieldErrorMessage()
        {
            return _pleaseMakeSelectionString;
        }

        public string GetBillingPhoneNumberErrorMessage()
        {
            return BillingPhoneElement.Text;
        }

        public IElement GetMinimumOrderErrorMessage()
        {
            return MinimumOrderError;
        }

        public void ClickOnPaymentDetailsLink()
        {
            DetailsLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_deleteIdClass.ToCssClassSelector()));
        }

        public void SelectInternationalAgreementAndPlaceOrder()
        {
            ClickIAgreeCheckBox();
            ClickOnInternationalPlaceOrderButton();
        }

        public void EnableWireTransfer()
        {
            WireTransferRadio.Click();
            Browser.Wait.ForBoolCondition(Browser.Locate.ElementByXpath("//li[@data-payment='WireTransfer']")
                .GetAttribute("class").Contains("active"));
        }

        public void EnablePurchaseOrder()
        {
            PurchaseOrderRadioButton.Click();
            Browser.Wait.ForBoolCondition(Browser.Locate.ElementByXpath("//li[@data-payment='PurchaseOrder']")
                .GetAttribute("class").Contains("active"));
        }

        public void EnablePaperCheck()
        {
            CheckRadio.Click();
            Browser.Wait.ForBoolCondition(Browser.Locate.ElementByXpath("//li[@data-payment='PaperCheck']")
                .GetAttribute("class").Contains("active"));
        }

        public virtual void DeletePaymentOption()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_detailsClass.ToCssClassSelector()));
            DetailsLink.Click();
            _modal.IsModalVisible();
            Browser.ScrollIntoView(DeletePayment);
            Browser.Wait.IsVisibleElement(By.CssSelector(_deleteIdClass.ToCssClassSelector()));
            Browser.ClickOnButtonMultipleTimes(DeletePayment, 5, IsDeletePaymentButtonVisible);
        }

        public virtual bool IsPaymentTypeAvailable(string paymentType)
        {
            return PaymentTypeRadios.Select(paymentTypeRadio => paymentTypeRadio.GetAttribute(HtmlTextWriterAttribute.Value.ToString())).Any(dataPaymentType => dataPaymentType == paymentType);
        }

        public virtual void SelectGiftCardLink()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_haveGiftCardClass.ToCssClassSelector()));
            GiftCardLink.Click();
        }

        public bool IsGiftCertContainerVisible => Browser.Locate.ElementImmediately(_giftCertNumId.ToCssIdSelector()).IsInitialized;

        public void SelectCheckPaymentOption()
        {
            Browser.Wait.ForClickableElement(CheckRadio);
            CheckRadio.Click();
            Browser.Wait.ForDisplayedElement(CheckNumberField);
        }

        public IElement GetProp65WarningDialog()
        {
            return PropWarningContainer;
        }

        public void SelectShippingHeaderLink()
        {
            ShippingHeaderLink.Click();
        }

        public void ShowCountryField()
        {
            MyAddressIsOutsideTheUsLink.Click();
        }
    }
}
