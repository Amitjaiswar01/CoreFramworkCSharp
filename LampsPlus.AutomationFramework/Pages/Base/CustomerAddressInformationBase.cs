using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using Automation.Framework.Core;
using OpenQA.Selenium.Appium;
using Page = Automation.Framework.Core.Page;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Base
{

    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class CustomerAddressInformationBase : Page, ICustomerAddressInformation
    {
        /// <inheritdoc />
        protected CustomerAddressInformationBase(IBrowser browser, IOrderSummaryBlock orderSummaryBlock, IShipping shippingInstance, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser)
        {
            Address = new Address();
            IntAddress = new IntAddress();
            OrderSummaryBlock = orderSummaryBlock;
            GlobalLocators = globalLocators;
            TestsBase = testsBase;
            Shipping = shippingInstance;
        }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        internal TestsBase TestsBase { get; }

        public Address Address { get; set; }
        public IntAddress IntAddress { get; set; }
        public IOrderSummaryBlock OrderSummaryBlock { get; set; }
        protected IShipping Shipping { get; }

        public string AgreeIntlOrderId => "agreeIntlOrder";
        public string ApartmentArdmoreString => "1";
        public string CityArdmoreString => "Ardmore";
        public string StreetAddressArdmoreString => "116 Ardmore";

        #endregion

        #region CSS Selector Strings
        private string DataCategoryAttribute { get; } = "data-category";
        private string DefaultAddressRadioId { get; } = "defaultAddressRadio";
        private string EnteredAddress { get; } = "enteredAddress";
        private string FedExShippingAddress2Id { get; } = "fedExShippingAddress2";
        private string FedExShippingCityId { get; } = "fedExShippingCity";
        private string FedExShippingStateId { get; } = "fedExShippingState";
        private string FedExShippingZipCodeId { get; } = "fedExShippingZipCode";
        private string MultiAddressFirstNameXpath { get; } = "//*[@id=\"lpModal\"]//input[@id='singleShippingFirstName']";
        private string KeepAddressId { get; } = "keepAddress";
        private string NoChangeAddressRadioId { get; } = "noChangeAddressRadio";
        private string ShowAnotherAddressFieldContainerClass { get; } = "showAnotherAddressFieldContainer";
        private string SingleShippingEmailId { get; } = "singleShippingEmail";
        private string SuggestedAddressId { get; } = "suggestedAddress";

        public string FedExShippingAddress1Id { get; } = "fedExShippingAddress1";
        public string SubmitChangesClass { get; } = "submitChanges";
        public string SuggestedAddressRadioId { get; } = "suggestedAddressRadio";
        public string FedExSuggestedAddressString => Browser.Locate.ElementBySelector($"{SuggestedAddressId.ToCssIdSelector()} {HtmlTextWriterTag.Div.ToNthChildSelector(1)}").Text;
        public string FedExSuggestedCityString => Browser.Locate.ElementBySelector($"{SuggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[0].TrimStart();
        public string FedExSuggestedStateString => Browser.Locate.ElementBySelector($"{SuggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[2].TrimStart();
        public string FedExSuggestedZipCodeString => Browser.Locate.ElementBySelector($"{SuggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[3].TrimStart();

        public abstract string AddressLabelClass { get; }
        public abstract string AddAnotherAddressFieldLinkClass { get; }
        public abstract string AddAnotherAddressFieldLinkXpath { get; }
        public abstract string AddressFieldPairClass { get; }
        public abstract string CaliforniaString { get; }
        public abstract string FedExStateSelectorId { get; }
        public abstract string FedExShippingStateXpath { get; }
        public abstract string FieldCheckboxClass { get; }
        public abstract string ShowAddressLine2BtnClass { get; }
        public abstract string LpSelectMobileDrawerClass { get; }
        public abstract string NorthCarolinaString { get; }
        public abstract string PaymentInfoAddressFieldsetClass { get; }
        public abstract string PennsylvaniaString { get; }
        public abstract string SingleShippingCountryId { get; }
        public abstract string UnitedStatesString { get; }
        #endregion

        #region Page Elements
        public IElement AddNewAddressButton => Browser.Locate.ElementByClassName(Shipping.AddNewAddrClass);
        public IElement AddressContainerElement => Browser.Locate.ElementById(Shipping.SingleAddressFormContainerId);
        public IElement ApartmentSuiteOtherField => Browser.Locate.ElementById(Shipping.SingleShippingAddress2Id);
        public IElement CityField => Browser.Locate.ElementById(Shipping.SingleShippingCityId);
        public IElement DefaultAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, DefaultAddressRadioId);
        public IElement EmailField => Browser.Locate.ElementBySelector(SingleShippingEmailId.ToCssIdSelector());
        public IElement EditMaintainCurrentAddressLink => Browser.Locate.ElementByAttribute(AttributeSelectorType.Equals, DataCategoryAttribute, EnteredAddress);
        public IElement FedExAddressValidationModal => Browser.Locate.ElementByClassName(Shipping.FedExAddressValidationClass); //TODO Changed locator strategy
        public IElement FedExModalKeepAddressOption => Browser.Locate.ElementById(KeepAddressId);
        public IElement FedExShippingAddress1 => Browser.Locate.ElementById(FedExShippingAddress1Id);
        public IElement FedExShippingAddress2 => Browser.Locate.ElementById(FedExShippingAddress2Id);
        public IElement FedExShippingCity => Browser.Locate.ElementById(FedExShippingCityId);
        public virtual IElement FedExShippingState => Browser.Locate.ElementById(FedExShippingStateId);
        public IElement FedExShippingZipCode => Browser.Locate.ElementById(FedExShippingZipCodeId);
        public IElement FirstNameField => Browser.Locate.ElementById(Shipping.SingleShippingFirstNameId);
        public IElement GetValidationErrorElement(IElement formControl) => Browser.Locate.ElementBySelector($"#{formControl.GetAttribute("Id")}-error");
        public IElement GoogleAutocompleteElement => Browser.Locate.ElementByClassName(Shipping.PacContainerClass);
        public IElement LastNameField => Browser.Locate.ElementById(Shipping.SingleShippingLastNameId);
        public IElement MultiAddressAddress1Field => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingAddress1Id, GlobalLocators.Iframe);
        public IElement MultiAddressAddress2Field => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingAddress2Id, GlobalLocators.Iframe);
        public IElement MultiAddressCityField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingCityId, GlobalLocators.Iframe);
        public IElement MultiAddressFirstNameField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingFirstNameId, GlobalLocators.Iframe);
        public IElement MultiAddressLastNameField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingLastNameId, GlobalLocators.Iframe);
        public IElement MultiAddressPhoneNumberField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingPhoneId, GlobalLocators.Iframe);
        public IElement MultiAddressShowAnotherAddressFieldLink => Browser.Locate.ElementByClassName(ShowAnotherAddressFieldContainerClass, GlobalLocators.Iframe);
        public IElement MultiAddressStateField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Select, HtmlTextWriterAttribute.Id, Shipping.SingleShippingStateId, GlobalLocators.Iframe);
        public IElement MultiAddressZipCodeField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, Shipping.SingleShippingZipCodeId, GlobalLocators.Iframe);
        public IElement NoChangeAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, NoChangeAddressRadioId);
        public IElement PhoneField => Browser.Locate.ElementBySelector(Shipping.SingleShippingPhoneId.ToCssIdSelector());
        public IElement ProceedToPaymentButton => Browser.Locate.ElementById(Shipping.ProceedPaymentId);
        public IElement SaveAddressFromModalButton => Browser.Locate.ElementById(Shipping.SaveAddressFromModalId);
        public IElement SavedAddressFullName => Browser.Locate.ElementByClassName(Shipping.SavedFullNameClass, ShippingAddressInfoContainer);
        public IElement SavedAddressShippingInfo => Browser.Locate.ElementByClassName(Shipping.SavedAddressClass, ShippingAddressInfoContainer);
        public IElement ShippingAddressInfoContainer => Browser.Locate.ElementByClassName(Shipping.ShippingAddressContainerClass);
        public IElement ShippingOptionsChangedMessage => Browser.Locate.ElementByClassName(Shipping.ShippingOptionsChangedContainerClass);
        public IElement ShowCountryLink => Browser.Locate.ElementById(Shipping.ShowCountryFieldId);
        public IElement ShowStateLink => Browser.Locate.ElementById(Shipping.SingleShippingStateId);
        public IElement StreetAddressField => Browser.Locate.ElementById(Shipping.SingleShippingAddress1Id);
        public IElement SubmitChangesElement => Browser.Locate.ElementByClassName(SubmitChangesClass);
        public IElement SuggestedAddressElement => Browser.Locate.ElementById(SuggestedAddressId);
        public IElement SuggestedAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, SuggestedAddressRadioId);
        public IElement ZipPostalCodeField => Browser.Locate.ElementBySelector(Shipping.SingleShippingZipCodeId.ToCssIdSelector());
        
        public abstract IElement AddAnotherAddressFieldLink { get; }
        public abstract IElement IntAddAnotherAddressFieldLink { get; }
        public abstract IElement ChangeShippingApplyButton { get; }
        public abstract IElement CountryField { get; }
        public abstract IElement CountrySelection { get; }
        public abstract IElement FedExAddressValidationHeader { get; }
        public abstract IElement FedExApartmentStateSelection { get; }
        public abstract IElement SaveAddressCheckbox { get; }
        public abstract IElement SaveAddressCheckboxInput { get; }
        public abstract IElement SelectShippingAddressModal { get; }
        public abstract IElement ShippingAddressOption { get; }
        public abstract IElement ShippingInformationModal { get; }
        public abstract IElement ShipToDifferentAddressButton { get; }
        public abstract IElement StateField { get; }
        public abstract IElement StateSelection { get; }

        public abstract IElement GetCommonSaveAddressCheckbox(bool getMobileInput = false);
        #endregion

        /// <inheritdoc />
        public void ProceedToPayment()
        {
            Browser.Wait.IsVisibleElement(By.Id(Shipping.ProceedPaymentId));
            Browser.ClickByJs(Shipping.ProceedToPaymentButton);
        }

        /// <inheritdoc />
        public void FillStreetAddressFieldAndLetGoogleSuggestionAct(string street)
        {
            ClearAndEnterText(StreetAddressField, street);
            Browser.Wait.ForDisplayedElement(GoogleAutocompleteElement);  //This line works only when element displays
        }

        /// <inheritdoc />
        public void ClearFormControl(IElement formControl)
        {
            switch (formControl.TagName)
            {
                case "input":
                    {
                        formControl.Clear();
                        break;
                    }
            }
        }

        /// <inheritdoc />
        public void FillFormControlByText(IElement formControl, string text)
        {
            var tag = formControl.TagName;

            if (tag.CaseInsensitiveContains("Select"))
            {
                new SelectElement(formControl.InternalElement).SelectByValue(text);
            }
            else if (tag.CaseInsensitiveContains("input"))
            {
                formControl.SendKeys(text);
            }
        }

        public void ClearAndEnterText(IElement element, string text)
        {
            ClearFormControl(element);
            FillFormControlByText(element, text);
        }

        public void FillFormSelectByValue(IElement selectControl, string value) { Browser.Locate.ClickDropdownByValue(selectControl, value); }

        public bool FormControlValidationErrorMessageDisplayed(IElement formControl) { return GetValidationErrorElement(formControl)?.Displayed ?? false; }

        public string GetValidationErrorMessage(IElement formControl)
        {
            var element = GetValidationErrorElement(formControl);

            return element != null ? element.Text : string.Empty;
        }

        /// <inheritdoc />
        public List<KeyValuePair<string, IElement>> RequiredFormControls()
        {
            return new List<KeyValuePair<string, IElement>>
            {
                new KeyValuePair<string, IElement>(Address.FirstName, FirstNameField),
                new KeyValuePair<string, IElement>(Address.LastName, LastNameField),
                new KeyValuePair<string, IElement>(Address.AddressLine1, StreetAddressField),
                new KeyValuePair<string, IElement>(Address.City, CityField),
                new KeyValuePair<string, IElement>(Address.State, StateField),
                new KeyValuePair<string, IElement>(Address.ZipCode, ZipPostalCodeField),
                new KeyValuePair<string, IElement>(Address.Email, EmailField)
            };
        }

        /// <inheritdoc />
        public string GetOrderSummaryShippingCost(bool removeCurrencySign = true)
        {
            var orderSummaryShippingCost = OrderSummaryBlock.ShippingAndProcessingValue.Text;
            return removeCurrencySign ? orderSummaryShippingCost.Replace("$", string.Empty) : orderSummaryShippingCost;
        }

        /// <inheritdoc />
        public void EnterShippingAddress(Address address, UserRole userrole, bool isIntAddress = false, bool isMultiAddress = false)
        {
            Browser.Wait.ForDomReady();

            if (isMultiAddress)
            {
                if (TestsBase.Settings.IsTabletView)
                {
                    ((IpadBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
                    Browser.Wait.IsVisibleElement(By.XPath("//XCUIElementTypeButton[@name='+ Add another address field']"));
                    Browser.Locate.ElementByXpath("//XCUIElementTypeButton[@name='+ Add another address field']").Click();
                    ((IpadBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context
                }
                else
                {
                    Browser.Wait.IsVisibleElement(By.XPath(MultiAddressFirstNameXpath));

                    MultiAddressShowAnotherAddressFieldLink.Click();
                }

                MultiAddressFirstNameField.Clear();
                FillFormControlByText(MultiAddressFirstNameField, address.FirstName);

                MultiAddressLastNameField.Clear();
                FillFormControlByText(MultiAddressLastNameField, address.LastName);

                MultiAddressAddress1Field.Clear();
                FillFormControlByText(MultiAddressAddress1Field, address.AddressLine1);

                MultiAddressAddress2Field.Clear();
                FillFormControlByText(MultiAddressAddress2Field, address.AddressLine2);

                MultiAddressCityField.Clear();
                FillFormControlByText(MultiAddressCityField, address.City);

                MultiAddressPhoneNumberField.Clear();
                FillFormControlByText(MultiAddressPhoneNumberField, address.Phone);

                SelectState(MultiAddressStateField, address.State);

                MultiAddressZipCodeField.Clear();
                FillFormControlByText(MultiAddressZipCodeField, address.ZipCode);

                return;
            }

            Browser.Wait.ForDomReady();

            CheckShippingFormIsLoaded();

            if (!TestsBase.Settings.IsMobileView)
            {
                if (Browser.Locate.ElementByClassName(Shipping.ShowAnotherAddressFieldClass).GetAttribute(GlobalLocators.AriaExpandedAttribute) == "false")
                {
                    AddAnotherAddressFieldLink.Click();
                }
            }
            else
            {
                AddAnotherAddressFieldLink.Click();
            }
            
            Browser.Wait.ForDisplayedElement(FirstNameField).Clear();
            FillFormControlByText(FirstNameField, address.FirstName);

            LastNameField.Clear();
            FillFormControlByText(LastNameField, address.LastName);

            if (Browser.Locate.DoesElementExistImmediately(SingleShippingEmailId.ToCssIdSelector()))
            {
                EmailField.Clear();
                FillFormControlByText(EmailField, address.Email);
            }

            PhoneField.Clear();
            FillFormControlByText(PhoneField, address.Phone);

            StreetAddressField.Clear();
            FillFormControlByText(StreetAddressField, address.AddressLine1);

            Browser.Wait.ForElementToStopAnimating(ApartmentSuiteOtherField);
            StreetAddressField.SendKeys(Keys.Tab); //Without this, one of the auto-complete options is sometimes selected which causes a failure.

            ApartmentSuiteOtherField.Clear();
            FillFormControlByText(ApartmentSuiteOtherField, address.AddressLine2);

            CityField.Clear();
            FillFormControlByText(CityField, address.City);

            Browser.Wait.ForElementToStopAnimating(ShowCountryLink);
            ShowCountryLink.Click();

            Browser.Wait.ForCondition(() => ShowCountryLink.GetAttribute("aria-expanded") == "true"); //Need to wait because otherwise the country isn't always changed from the US.

            SelectCountry(CountryField, address);
            Browser.Wait.ForElementToStopAnimating(CountryField);

            if (!isIntAddress)
            {
                SelectState(StateField, address.State);
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SingleShippingZipCodeId.ToCssIdSelector()));
            ZipPostalCodeField.Clear();
            FillFormControlByText(ZipPostalCodeField, address.ZipCode);
        }

        public abstract void SelectCountry(IElement element, Address address);
        public abstract void SelectState(IElement element, string state);
        public abstract void SelectFedExState(IElement element, string state);

        /// <summary>
        /// Enter Wire Transfer Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        public void EnterWireTransferBillingAddress(Address address)
        {
            var payment = TestsBase.Payment;

            EnterBasicAddressDetails(address);

            if (address.Country != "US")
            {
                payment.ChangeCountryLinkEmployeeElement.Click();
                FillFormSelectByValue(payment.BillingCountryElement, address.Country);
            }

            payment.PaymentStateElement.Click();
            FillFormSelectByValue(payment.PaymentStateElement, address.State);
            payment.PaymentStateElement.Click();
        }

        /// <summary>
        /// Enter International Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        public virtual void EnterIntBillingAddress(Address address)
        {
            var payment = TestsBase.Payment;

            if (payment.SameAsShippingCheckBox.Selected)
            {
                Browser.ExecuteJs("arguments[0].click()", payment.SameAsShippingCheckBox.InternalElement);
                Browser.Wait.ForDisplayedElement(TestsBase.Payment.BillingPhoneElement);
            }

            Browser.ScrollIntoView(payment.ChangeCountryLinkClassElement);

            Browser.ClickByJs(payment.ChangeCountryLinkClassElement);

            Browser.Wait.ForDisplayedElement(payment.BillingCountryElement);
            payment.BillingCountryElement.Click();

            Browser.Wait.ForElementToStopAnimating(payment.BillingCountryElement);

            EnterDifferentCountryValueOnPaymentPage(payment.BillingCountryElement, address);

            Browser.Wait.IsVisibleElement(By.Id(AgreeIntlOrderId));

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

        private void EnterBasicAddressDetails(Address address)
        {
            var payment = TestsBase.Payment;

            IntAddAnotherAddressFieldLink.Click();

            payment.PaymentFirstNameField.Clear();
            payment.PaymentFirstNameField.SendKeys(address.FirstName);

            payment.PaymentLastNameField.Clear();
            payment.PaymentLastNameField.SendKeys(address.LastName);

            payment.PaymentAddress1Field.Clear();
            payment.PaymentAddress1Field.SendKeys(address.AddressLine1);

            payment.PaymentAddress2Field.Clear();
            payment.PaymentAddress2Field.SendKeys(address.AddressLine2);

            payment.PaymentCityField.Clear();
            payment.PaymentCityField.SendKeys(address.City);

            payment.PaymentZipCodeField.Clear();
            payment.PaymentZipCodeField.SendKeys(address.ZipCode);

            payment.PaymentPhoneField.Clear();
            payment.PaymentPhoneField.SendKeys(address.Phone);
        }

        /// <summary>
        /// Enter Billing Address information on Payment page for international orders.
        /// </summary>
        /// <param name="address"></param>
        public void EnterBillingAddressForIntlOrders(Address address)
        {
            var payment = TestsBase.Payment;

            EnterBasicAddressDetails(address);
            FillFormSelectByValue(payment.PaymentStateElement, address.State);
        }

        public void CheckShippingFormIsLoaded()
        {
            try //There are times on mobile devices that the Shipping page does not properly load the first time. 
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SingleShippingFirstNameId.ToCssIdSelector()), -30);
            }
            catch
            {
                Log.Message("Original page load did not load Shipping form.");
                Browser.RefreshPage();
                Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SingleShippingFirstNameId.ToCssIdSelector()), 15);
            }
        }

        public abstract void EnterDifferentCountryValueOnPaymentPage(IElement element, Address address);

        public bool DoesFedExModalShow()
        {
            return SpinWait.SpinUntil(() => Browser.Locate.DoesElementExistImmediately(Shipping.FedExAddressValidationClass.ToCssClassSelector()), TimeSpan.FromSeconds(5));
        }
    }


    /// <summary>
    /// Class for creating a Shipping address object with default values used for populating Shipping address form. 
    /// Note: Use Country and State codes not names.
    /// </summary>
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SaveToProfile { get; set; }
        protected string EmailFormatted = string.Format("testautomation{0}@mailinator.com", DateTime.Now.ToString("yyyyMMddHHmmssFF"));

        /// <summary>
        /// Default values for entering in Shipping information.
        /// </summary>
        /// <param name="nameSuffix"></param>
        public Address(string nameSuffix = "")
        {
            // Set default values
            FirstName = $"lptest{nameSuffix}";
            LastName = $"lptest{nameSuffix}";
            AddressLine1 = "20250 Plummer St";
            AddressLine2 = "lptest";
            City = "Chatsworth";
            State = StateCodeListUnitedStates.CA; // Use State code not name
            Country = CountryCodeList.US; // Use Country code not name
            ZipCode = ZipCodeList.Chatsworth;
            Phone = "1234567890";
            Email = EmailFormatted;
            SaveToProfile = false;
        }
    }


    /// <summary>
    /// Class for creating a Shipping address object with default values used for populating International Shipping address form. 
    /// Note: Use Country and State codes not names.
    /// </summary>
    public class IntAddress : Address
    {
        /// <summary>
        /// Default values for entering in International Shipping information.
        /// </summary>
        /// <param name="nameSuffix"></param>
        public IntAddress(string nameSuffix = "")
        {
            const string lpTest = "lptest";
            // Set default values
            FirstName = $"{lpTest}{nameSuffix}";
            LastName = $"{lpTest}{nameSuffix}";
            AddressLine1 = "22 Baker Street";
            AddressLine2 = "lptest";
            City = "London";
            Country = CountryCodeList.GB; // Use Country code not name
            ZipCode = "W1U3BW";
            Phone = "1234567890";
            Email = EmailFormatted;
            SaveToProfile = false;
        }
    }
}
