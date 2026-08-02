using System;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation
{
    public class CustomerAddressInformationMobile : CustomerAddressInformationDesktop, ICustomerAddressInformationMobile
    {
        public CustomerAddressInformationMobile(IBrowser browser, Log log , SessionSettings settings, IAddress address) : base(browser, log, settings, address) { }

        private string _addAnotherAddressFieldLinkClass = "showAddressLine2Btn";
        private string _stateSelectorId = "lpSelectMobileDrawer__singleShippingState";
        private string _buttonTriggerSingleShippingStateId = "buttonTrigger__singleShippingState";
        private string _billingStateSelectorId = "lpSelectMobileDrawer__singleShippingState-view25";
        private string _lpMobileDrawerClass = "lpMobileDrawer";
        private string _dataValueString = "data-value";
        private string _dataTextString = "data-text";
        private string _unitedStatesString = "United States";
        private string _billingStateFieldXpath = "//*[@id='buttonTrigger__singleShippingState-view25']";
        private string _fedExStateSelectorId  = "lpSelectMobileDrawer__fedExShippingState";
        private string _fedExShippingStateXpath = "//*[@id='defaultAddressContainer']/form/fieldset/div[4]/div[1]/button";

        protected override string MaintainMessage => Browser.Locate.ElementByXpath("//div[contains(@class,'fedExAddressValidationModal')]/h1").Text;
        protected override string AddressCorrectionsMessage => Browser.Locate.ElementByXpath("//div[contains(@class,'fedExAddressValidationModal')]/div[2]").Text;

        private IElement FedExStateSelection => Browser.Locate.ElementById(_fedExStateSelectorId);
        private IElement StateDropdown => Browser.Locate.ElementById(_stateSelectorId);
        private IElement LpMobileDrawerElement => Browser.Locate.ElementBySelector(_lpMobileDrawerClass.ToCssClassSelector());
        private IElement CountrySelection => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, _dataTextString, _unitedStatesString);

        protected override IElement FedExShippingState => Browser.Locate.ElementByXpath(_fedExShippingStateXpath);
        protected override IElement CountryField => Browser.Locate.ElementBySelector("#shippingCountryField > div > button");
        protected override IElement AddAnotherAddressFieldLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, _addAnotherAddressFieldLinkClass);
        protected override IElement StateField => Browser.Locate.ElementBySelector(_buttonTriggerSingleShippingStateId.ToCssIdSelector());
        protected override IElement BillingStateDropdown => Browser.Locate.ElementById(_billingStateSelectorId);
        protected override IElement BillingCountryElement => Browser.Locate.ElementByXpath("//button[contains(@id, 'singleShippingCountry')]");
        protected override IElement BillingStateElement => Browser.Locate.ElementByXpath(_billingStateFieldXpath);

        protected override IElement FedExAddressValidationHeader => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H1, FedExAddressValidationModal)[0];

        private void ClickBillingCountryElement()
        {
            Browser.ScrollIntoView(BillingLastNameElement);

            BillingCountryElement.Click();

            Browser.Wait.ForElementToStopAnimating(LpMobileDrawerElement);
        }

        //Interface implementation
        public override void SelectState(string state, bool isMultiAddress = false)
        {
            if (isMultiAddress)
            {
                Browser.ScrollIntoView(ApartmentSuiteOtherField);
                MultiAddressStateField.Click();
                SelectDropdownByValue(MultiAddressStateField, state);
            }
            else
            {
                Browser.ScrollIntoView(ApartmentSuiteOtherField);
                StateField.Click();
                SelectDropdownByValue(StateDropdown, state);
            }
        }

        protected override void SelectDropdownByValue(IElement element, string optionValue)
        {
            var valueAttribute = string.Equals(element.TagName, HtmlTextWriterTag.Select.ToString(), StringComparison.CurrentCultureIgnoreCase)
                ? HtmlTextWriterAttribute.Value.ToString().ToLower()
                : "data-value";

            var option = Browser.Wait.ForElement(element.FindElement(By.CssSelector($"[{valueAttribute}*={optionValue}]")));

            if (!element.Displayed)
            {
                element.Click();
            }

            Browser.Wait.ForElementToStopAnimating(option);

            Browser.ScrollIntoView(option);
            option.Click();

            Browser.Wait.ForElementToStopAnimating(option);
        }

        public override void SelectCountry(IElement element, IAddress address)
        {
            element.Click();

            Browser.Wait.ForElementToStopAnimating(LpMobileDrawerElement);
            Browser.Wait.ForDomReady();
            
            var country = Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, _dataValueString, address.Country);

            Browser.ScrollToElement(country);
            Browser.Wait.ForClickableElement(CountrySelection);

            country.Click();
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

        public override void ChangeBillingCountry(IAddress address)
        {
            ClickBillingCountryElement();

            var country = Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, _dataValueString, address.Country);

            Browser.ScrollIntoView(country);

            country.Click();
        }
    }
}
