using System;
using System.Collections.ObjectModel;
using System.Web.UI;

using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;

using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/billing/.
    /// </summary>
    public class MobilePayment : PaymentBase
    {
        /// <inheritdoc />
        public MobilePayment(IBrowser browser, ICustomerAddressInformation customerAddressInformation, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, customerAddressInformation, globalLocators, testsBase) { }

        #region Class Setup
        private string EditLinkStringSelector => "#shippingAddress > a";

        public override string CardYearView29ErrorId { get; } = "cardYear-view30-error";

        public override string ApartmentString => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {SecondLineClass.ToCssClassSelector()}").Text;
        public override string CityStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',')[0].TrimStart();
        public override string StateStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[2].Trim();
        public override string StreetAddressString => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {FirstLineClass.ToCssClassSelector()}").Text;
        public override string ZipCodeStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{PaymentInfoContentClass.ToCssClassSelector()} > {ThirdLineClass.ToCssClassSelector()}").Text.Split(',', ' ')[3].Trim();

        public override string CityString => throw new NotImplementedException();
        public override string StateString => throw new NotImplementedException();
        public override string ZipCodeString => throw new NotImplementedException();
        public override string DeliveryCallOutBtnSelector => throw new NotImplementedException();
        #endregion

        #region CSS Selector Strings
        private string CardExpirationId { get; } = "cardExpiration";

        public override string BillingCountryElementId { get; } = "buttonTrigger__singleShippingCountry-view27";
        public override string NewCardId { get; } = "newCard";
        public override string AgreeIntlOrderId { get; } = "agreeIntlOrder";
        #endregion

        #region Page Elements
        public override IElement BillingCountryElementID => Browser.Locate.ElementBySelector($"{BillingCountryElementId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Button}");
        public override IElement CardExpirationField => Browser.Locate.ElementBySelector(CardExpirationId.ToCssClassSelector());
        public override IElement EditLink => Browser.Locate.ElementBySelector(EditLinkStringSelector);
        public override IElement InternationalAgreeCheckbox => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, AgreeIntlOrderId);
        public override IElement NewPaymentOption => Browser.Locate.ElementByAttribute(AttributeSelectorType.Equals, HtmlTextWriterAttribute.For, NewCardId);
        public override IElement BillingCountryElement => Browser.Locate.ElementByXpath("//button[contains(@id,'singleShippingCountry-view')]");

        public override IElement CartBreadcrumb => throw new NotImplementedException();
        public override IElement ChangeCountryLinkEmployeeElement => throw new NotImplementedException();
        public override IElement CheckNumberField => throw new NotImplementedException();
        public override IElement CheckRadio => throw new NotImplementedException();
        public override IElement CreditCartRadio => throw new NotImplementedException();
        public override IElement DetailsLink => throw new NotImplementedException();
        public override IElement PurchaseOrderNumberField => throw new NotImplementedException();
        public override IElement PurchaseOrderRadioButton => throw new NotImplementedException();
        public override IElement WireTransferRadio => throw new NotImplementedException();
        public override IElement DeliveryAgreementBox => throw new NotImplementedException();
        #endregion

        public override void PlaceInternationalOrder()
		{
            Browser.Locate.ElementByAttribute(AttributeSelectorType.Equals, HtmlTextWriterAttribute.For, AgreeIntlOrderId).Click();

			PlaceIntlOrderButton.Click();

            Browser.SwitchToCurrentWindow();
        }

        public override void EnterCreditCardInfo(CreditCard creditCard)
        {
            Browser.Wait.ForDisplayedElement(CreditCardField).Click();
            CreditCardField.Clear();
            CustomerAddressInformation.FillFormControlByText(CreditCardField, creditCard.CardNumber);

            CardCodeField.Clear();
            CustomerAddressInformation.FillFormControlByText(CardCodeField, creditCard.SecurityCode);

            CardExpirationField.SendKeys(creditCard.ExpirationMonth.ToString("d2"));
            CardExpirationField.SendKeys(creditCard.ExpirationYear.ToString());

            NameField.Clear();
            CustomerAddressInformation.FillFormControlByText(NameField, creditCard.NameOnCard);
        }
    }
}
