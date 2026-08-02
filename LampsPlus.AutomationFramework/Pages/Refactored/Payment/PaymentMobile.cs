using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Payment
{
    public class PaymentMobile :PaymentDesktop, IPaymentMobile
    {
        //Class members
        private string _cardExpirationId = "cardExpiration";
        private string _editCardClass = "editCard";
        private string _showCountryFieldId = "showCountryField";
        private string _editCardContainerId = "editCardContainer";
        private string _iAgreeCheckBoxXpath = "//*[@for = 'agreeIntlOrder']";
        private string _unstyledListClass = "unstyledList";
        private string _jsApplyGiftCardClass = "jsApplyGiftCard";
        private string _jsShippingHeaderClass = "jsShippingHeader";
        private string _jsShippingBillingHeaderCartTotalId = "jsShippingBillingHeaderCartTotal";
        private string _agreeIntlOrderId  = "agreeIntlOrder";
        private string _orderSummaryCheckoutXpath = "//*[@class='orderSummaryCheckout']/button";

        protected override string StreetAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {FirstLineClass.ToCssClassSelector()}").Text.TrimStart().ToLower();
        protected override string ApartmentText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {SecondLineClass.ToCssClassSelector()}").Text.TrimStart().ToLower();
        protected override string CityTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',')[0].TrimStart().ToLower();
        protected override string StateTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[2].Trim().ToLower();
        protected override string ZipCodeTextWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[3].Trim().ToLower();
        protected override string StreetSuggestedAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {FirstLineClass.ToCssClassSelector()}").Text;
        protected override string ApartmentSuggestedAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {SecondLineClass.ToCssClassSelector()}").Text;
        protected override string CitySuggestedAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',')[0].TrimStart();
        protected override string StateSuggestedAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[2].Trim();
        protected override string ZipCodeSuggestedAddressText => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[3].Trim();

        private IElement CardExpirationField => Browser.Locate.ElementBySelector(_cardExpirationId.ToCssClassSelector());
        private IElement EditPaymentDetails => Browser.Locate.ElementBySelector($"{_editCardContainerId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div}");
        private IElement PaymentEditLinkElement => Browser.Locate.ElementBySelector(_editCardClass.ToCssClassSelector());
        private IElement CartTotalContainer => Browser.Locate.ElementBySelector(_jsShippingBillingHeaderCartTotalId.ToCssIdSelector());
        private IElement BackToCheckoutBtn => Browser.Locate.ElementByXpath(_orderSummaryCheckoutXpath);

        protected override IElement CardExpirationMonthErrorElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, _cardExpirationId);
        protected override IElement DetailsLink => Browser.Locate.ElementBySelector(_editCardClass.ToCssClassSelector());
        protected override IElement MyAddressIsOutsideTheUsLink => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, _showCountryFieldId);
        protected override IElement IAgreeCheckBox => Browser.Locate.ElementByXpath(_iAgreeCheckBoxXpath);
        protected override IElement GiftCardLink => Browser.Locate.ElementBySelector(_jsApplyGiftCardClass.ToCssClassSelector());
        protected override IElement ShippingHeaderLink => Browser.Locate.ElementBySelector(_jsShippingHeaderClass.ToCssClassSelector());

        private ReadOnlyCollection<IElement> PaymentOptionTab => Browser.Locate.ElementsByClassName(_unstyledListClass);

        protected override void ClickIAgreeCheckBox()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(PlaceYourIntlOrderButtonId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(PlaceInternationalOrderButton);

            Browser.Locate.ElementByAttribute(AttributeSelectorType.Equals, HtmlTextWriterAttribute.For, _agreeIntlOrderId).Click();
        }

        //Instances
        public PaymentMobile(IBrowser browser, IAssert assert, IModalDesktop modal) : base(browser, assert, modal) { }

        //Interface implementation
        public override bool IsInternationalCheckboxDisplayed => Browser.Wait.IsVisibleElement(By.XPath(_iAgreeCheckBoxXpath));

        public IElement GetEditPaymentDetails()
        {
            return EditPaymentDetails;
        }

        public void PlaceInternationalOrder()
        {
            IAgreeCheckBox.Click();
            PlaceInternationalOrderButton.Click();
        }

        public override Dictionary<string, string> GetPaymentPageFreeTypeFieldErrorMessages(int numberOfFreeTypeFieldsOnPaymentPage)
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

        public override Dictionary<string, string> GetPaymentPageDropdownFieldErrorMessages()
        {
            var fieldNameAndErrorMessage = new Dictionary<string, string>();

            var errorMessage = GetListOfPaymentPageDropdownFields()[0].Text;
            var fieldName = GetListOfPaymentPageDropdownFields()[0].LocatorString;

            fieldNameAndErrorMessage.Add(fieldName, errorMessage);

            return fieldNameAndErrorMessage;
        }

        public override List<IElement> GetListOfPaymentPageFreeTypeFields()
        {
            var paymentFreeTypeFields = new List<IElement>
            {
                PaymentFirstNameErrorElement,
                PaymentLastNameErrorElement,
                PaymentAddressLine1ErrorElement,
                PaymentCityErrorElement,
                PaymentZipCodeErrorElement,
                CardNumberErrorElement,
                CardExpirationMonthErrorElement,
                CardSecurityCodeErrorElement,
                CardNameErrorElement
            };

            return paymentFreeTypeFields;
        }

        public override List<IElement> GetListOfPaymentPageDropdownFields()
        {
            var paymentDropdownFields = new List<IElement>
            {
                PaymentStateErrorElement,
            };

            return paymentDropdownFields;
        }

        public override void EnterCreditCartInformation(CreditCard creditCard)
        {
            Browser.Wait.ForDisplayedElement(CreditCardField).Click();
            CreditCardField.Clear(); 
            FillFormControlByText(CreditCardField, creditCard.CardNumber);

            CardCodeField.Clear();
            FillFormControlByText(CardCodeField, creditCard.SecurityCode);

            CardExpirationField.SendKeys(creditCard.ExpirationMonth.ToString("d2"));
            CardExpirationField.SendKeys(creditCard.ExpirationYear.ToString());

            NameField.Clear();
            FillFormControlByText(NameField, creditCard.NameOnCard);
        }

        public override void DeletePaymentOption()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_editCardClass.ToCssClassSelector()));
            PaymentEditLinkElement.Click();
            Browser.ScrollIntoView(DeletePayment);
            Browser.Wait.ForElementToStopAnimating(DeletePayment).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CardNumberClass.ToCssClassSelector()));
        }

        public override bool IsPaymentTypeAvailable(string paymentType)
        {
            return PaymentOptionTab.Select(paymentOptionTab => paymentOptionTab.GetAttribute(HtmlTextWriterAttribute.Value.ToString())).Any(dataPaymentType => dataPaymentType == paymentType);
        }

        public override void SelectGiftCardLink()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_jsApplyGiftCardClass.ToCssClassSelector()));
            GiftCardLink.Click();
        }

        public void OpenOrderSummaryDropdown()
        {
            Browser.Wait.IsVisibleElement(By.Id(_jsShippingBillingHeaderCartTotalId), 30);
            CartTotalContainer.Click();
        }

        public void CloseOrderSummaryDropdown()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_orderSummaryCheckoutXpath));
            BackToCheckoutBtn.Click();
        }
    }
}
