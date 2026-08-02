using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Class to define all common elements on the Shipping and Billing pages.
    /// NOTE: The OrderSummaryBlock does not apply to the mobile view.
    /// If anything from this object is accessed in a mobile test it will throw a null reference exception by design.
    /// </summary>
    public class MobileCustomerAddressInformation : CustomerAddressInformationBase
    {
        /// <inheritdoc />
        public MobileCustomerAddressInformation(IBrowser browser, IOrderSummaryBlock orderSummaryBlock, IShipping shippingInstance, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, orderSummaryBlock, shippingInstance, globalLocators, testsBase)
        {
            OrderSummaryBlock = null;
            Framework = testsBase;
        }

        internal TestsBase Framework;
        private string ButtonTriggerSingleShippingStateId { get; } = "buttonTrigger__singleShippingState";
        private string PaymentTypeFormId { get; } = "paymentTypeForm";
        private string CaretWrpClass { get; } = "caretWrp";

        public override string AddAnotherAddressFieldLinkClass { get; } = "showAddressLine2Btn";
        public override string AddressFieldPairClass { get; } = "addressFieldPair";
        public override string CaliforniaString { get; } = "California";
        public override string FedExStateSelectorId { get; } = "lpSelectMobileDrawer__fedExShippingState";
        public override string FedExShippingStateXpath { get; } = "//*[@id='defaultAddressContainer']/form/fieldset/div[4]/div[1]/button";
        public override string FieldCheckboxClass { get; } = "fieldCheckbox";
        public override string ShowAddressLine2BtnClass { get; } = "showAddressLine2Btn";
        public override string LpSelectMobileDrawerClass { get; } = "lpSelectMobileDrawer";
        public override string NorthCarolinaString { get; } = "North Carolina";
        public override string PaymentInfoAddressFieldsetClass { get; } = "paymentInfoAddressFieldset";
        public override string PennsylvaniaString { get; } = "Pennsylvania";
        public override string SingleShippingCountryId { get; } = "shippingCountryField";
        public override string UnitedStatesString { get; } = "United States";

        public override string AddressLabelClass => throw new NotImplementedException();
        public override string AddAnotherAddressFieldLinkXpath => throw new NotImplementedException();

        private IElement PaymentForm => Browser.Locate.ElementBySelector(PaymentTypeFormId.ToCssIdSelector());

        public override IElement FedExShippingState => Browser.Locate.ElementByXpath(FedExShippingStateXpath);
        public override IElement AddAnotherAddressFieldLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, AddAnotherAddressFieldLinkClass);
        public override IElement FedExApartmentStateSelection => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, GlobalLocators.DataTextString, NorthCarolinaString);
        public override IElement IntAddAnotherAddressFieldLink => Browser.Locate.ElementBySelector(ShowAddressLine2BtnClass.ToCssClassSelector(), PaymentForm);
        public override IElement CountryField => Browser.Locate.ElementBySelector("#shippingCountryField > div > button");
        public override IElement CountrySelection => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, GlobalLocators.DataTextString, UnitedStatesString);
        public override IElement FedExAddressValidationHeader => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H1, FedExAddressValidationModal)[0];
        public override IElement SaveAddressCheckbox => Browser.Locate.ElementByClassName(FieldCheckboxClass);
        public override IElement SaveAddressCheckboxInput => Browser.Locate.ElementByClassName(FieldCheckboxClass).FindElement(By.TagName(HtmlTextWriterTag.Input.ToString()));
        public override IElement ShipToDifferentAddressButton => Browser.Locate.ElementBySelector(CaretWrpClass.ToCssClassSelector());
        public override IElement StateField => Browser.Locate.ElementBySelector(ButtonTriggerSingleShippingStateId.ToCssIdSelector());
        public override IElement StateSelection => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, GlobalLocators.DataTextString, PennsylvaniaString);
        public IElement FedExStateSelection => Browser.Locate.ElementById(FedExStateSelectorId);

        public override IElement ChangeShippingApplyButton => throw new NotImplementedException();
        public override IElement SelectShippingAddressModal => throw new NotImplementedException();
        public override IElement ShippingAddressOption => throw new NotImplementedException();
        public override IElement ShippingInformationModal => throw new NotImplementedException();
        
        public override void SelectCountry(IElement element, Address address)
        {
            element.Click();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.LpMobileDrawerElement);
            Browser.Wait.ForDomReady();

            var country = Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, GlobalLocators.DataValueString, address.Country);

            Browser.ScrollIntoView(country);
            Browser.Wait.ForClickableElement(CountrySelection);

            country.Click();
        }

        public override void SelectState(IElement element, string state)
        {
            element.Click();
            GlobalLocators.ClickDropdownByValue(GlobalLocators.StateDropdown, state);
        }
              
        public override void SelectFedExState(IElement element, string state)
        {
            element.Click();

            var option = Browser.Wait.ForElement(FedExStateSelection.FindElement(By.CssSelector($"[data-text*='{state}']")));

            Browser.Wait.ForElementToStopAnimating(option);

            Browser.ScrollIntoView(option);
            option.Click();

            Browser.Wait.ForElementToStopAnimating(option);
        }

        public override void EnterDifferentCountryValueOnPaymentPage(IElement element, Address address)
        {
            var country = Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, GlobalLocators.DataValueString, address.Country);

            Browser.ScrollIntoView(country);

            country.Click();
        }

        public override IElement GetCommonSaveAddressCheckbox(bool getMobileInput = false)
        {
            return getMobileInput ? SaveAddressCheckboxInput : SaveAddressCheckbox;
        }

        /// <summary>
        /// Enter International Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        public override void EnterIntBillingAddress(Address address)
        {
            var payment = TestsBase.Payment;

            if (payment.SameAsShippingCheckBox.Selected)
            {
                Browser.ExecuteJs("arguments[0].click()", payment.SameAsShippingCheckBox.InternalElement);
            }

            Browser.Wait.ForClickableElement(payment.ChangeCountryLinkClassElement);
            Browser.ScrollIntoView(payment.ChangeCountryLinkClassElement,true);
            Browser.ScrollToByPixelsVertical("50");
            payment.ChangeCountryLinkClassElement.Click();
            payment.BillingCountryElement.Click();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.LpMobileDrawerElement);

            EnterDifferentCountryValueOnPaymentPage(payment.BillingCountryElement, address);

            Browser.Wait.ForElementToStopAnimating(Shipping.NewShippingAddressFormContainer);

            Browser.Wait.IsVisibleElement(By.Id(payment.PaymentInfoPhoneId));
            Browser.ScrollIntoView(payment.PaymentPhoneField);
            IntAddAnotherAddressFieldLink.Click();

            payment.PaymentFirstNameField.Clear();
            payment.PaymentFirstNameField.SendKeys(address.FirstName);

            payment.PaymentLastNameField.Clear();
            payment.PaymentLastNameField.SendKeys(address.LastName);

            payment.PaymentAddress1Field.Clear();
            payment.PaymentAddress1Field.SendKeys(address.AddressLine1);
            payment.PaymentAddress1Field.SendKeys(Keys.Tab);

            payment.PaymentAddress2Field.Clear();
            payment.PaymentAddress2Field.SendKeys(address.AddressLine2);

            payment.PaymentCityField.Clear();
            payment.PaymentCityField.SendKeys(address.City);

            payment.PaymentZipCodeField.Clear();
            payment.PaymentZipCodeField.SendKeys(address.ZipCode);

            payment.PaymentPhoneField.Clear();
            payment.PaymentPhoneField.SendKeys(address.Phone);
        }
    }
}
