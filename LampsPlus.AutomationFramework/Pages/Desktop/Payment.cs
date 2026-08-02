using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
	/// <summary>
	/// https://www.lampsplus.com/secure/cart/billing/.
	/// </summary>
	public class Payment : PaymentBase
    {
        /// <inheritdoc />
        public Payment(IBrowser browser, ICustomerAddressInformation customerAddressInformation, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, customerAddressInformation, globalLocators, testsBase)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework;

        #region CSS Selectors
        public override string ApartmentString => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text;
        public override string CityString => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',')[0].TrimStart();
        public override string CityStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',')[0].TrimStart();
        public override string StateString => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[2].Trim();
        public override string StateStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',', ' ')[2].Trim();
        public override string StreetAddressString => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} >  {HtmlTextWriterTag.Div.ToNthChildSelector(2)}").Text;
        public override string ZipCodeString => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[3].Trim();
        public override string ZipCodeStringWithApartmentFieldActive => Browser.Locate.ElementBySelector($"{ShippingInfoId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(4)}").Text.Split(',', ' ')[3].Trim();
        public override string AgreeIntlOrderId { get; } = "agreeIntlOrder";
        public override string BillingCountryElementId { get; } = "shippingCountryField";
        public override string CardYearView29ErrorId { get; } = "cardYear-view32-error";
        public override string DeliveryCallOutBtnSelector { get; } = "a[class='calloutBtn']";
        public override string NewCardId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement BillingCountryElementID => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Div, HtmlTextWriterAttribute.Id, BillingCountryElementId);
		public override IElement EditLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, CartBreadcrumb);
		public override IElement CartBreadcrumb => Browser.Locate.ElementById(CartBreadcrumbId);
        public override IElement ChangeCountryLinkEmployeeElement => Browser.Locate.ElementByClassName(ShowCountryFieldClass, Browser.Locate.ElementByClassName(FormWireTransferClassName));
		public override IElement CheckNumberField => Browser.Locate.ElementByXpath("//input[@id='paymentInfoCheckNum']");
        public override IElement CheckRadio => Browser.Locate.ElementByXpath("//label[@for='PaperCheckPaymentType']");
		public override IElement CreditCartRadio => Browser.Locate.ElementByClassName(PaymentTypeRadioClass);
		public override IElement DetailsLink => Browser.Locate.ElementByClassName(Framework.OrderDetails.DetailsClass);
        public override IElement InternationalAgreeCheckbox => Browser.Locate.ElementByXpath("//*[@id='agreeIntlOrder']");
        public override IElement NewPaymentOption => Browser.Locate.ElementByClassName(NewPaymentOptionClass);
        public override IElement PurchaseOrderRadioButton => Browser.Locate.ElementByXpath("//label[@for='PurchaseOrderPaymentType']");
        public override IElement PurchaseOrderNumberField => Browser.Locate.ElementByClassName((PurchaseOrderNumberClass));
		public override IElement WireTransferRadio => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "WireTransferPaymentType");
        public override IElement BillingCountryElement => Browser.Locate.ElementByXpath("//select[contains(@id,'singleShippingCountry-view')]");
        public override IElement DeliveryAgreementBox => Browser.Locate.ElementBySelector(DeliveryCallOutBtnSelector);
        public override IElement CardExpirationField => throw new NotImplementedException();
        #endregion

        public override void PlaceInternationalOrder()
		{
            InternationalAgreeCheckbox.Click();

            PlaceIntlOrderButton.Click();
		}

        public override void EnterCreditCardInfo(CreditCard creditCard)
        {
            CreditCardField.Click();
            CreditCardField.Clear();
            CustomerAddressInformation.FillFormControlByText(CreditCardField, creditCard.CardNumber);

            CardCodeField.Clear();
            CustomerAddressInformation.FillFormControlByText(CardCodeField, creditCard.SecurityCode);

            SelectMonthByValue(creditCard.ExpirationMonth.ToString());
            SelectYear(creditCard.ExpirationYear.ToString());

            NameField.Clear();
            CustomerAddressInformation.FillFormControlByText(NameField, creditCard.NameOnCard);
        }
    }
}
