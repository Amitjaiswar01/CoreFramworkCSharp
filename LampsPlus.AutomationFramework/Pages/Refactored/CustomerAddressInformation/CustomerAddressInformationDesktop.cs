using System.Collections.Generic;
using System.Web.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation
{
    public class CustomerAddressInformationDesktop : ICustomerAddressInformationDesktop
    {
        //Class Members
        private string _fieldCheckboxClass = "fieldCheckbox";
        private string _singleShippingEmailId = "singleShippingEmail";
        private string _multiAddressFirstNameXpath = "//*[@id=\"lpModal\"]//input[@id='singleShippingFirstName']";
        private string _showAnotherAddressFieldContainerClass = "showAnotherAddressFieldContainer";
        private string _lpModalId = "lpModal";
        private string _ariaExpandedAttribute = "aria-expanded";
        private string _singleShippingFirstNameId = "singleShippingFirstName";
        private string _singleShippingLastNameId = "singleShippingLastName";
        private string _singleShippingAddress1Id = "singleShippingAddress1";
        private string _singleShippingAddress2Id = "singleShippingAddress2";
        private string _singleShippingCityId = "singleShippingCity";
        private string _singleShippingPhoneId = "singleShippingPhone";
        private string _singleShippingStateId = "singleShippingState";
        private string _singleShippingZipCodeId = "singleShippingZipCode";
        private string _showAnotherAddressFieldClass = "showAnotherAddressField";
        private string _addAnotherAddressFieldLinkXpath = "//button[@class='showAnotherAddressField anchorLink']";
        private string _showCountryFieldId = "showCountryField";
        private string _singleShippingCountryId = "singleShippingCountry";
        private string _showCountryFieldClass = "showCountryFieldId";
        private string _billingStateSelectorId = "singleShippingState-view30";
        private string _jsShippingCountryClass = "jsShippingCountry";
        private string _shipZipFieldId = "shipZipField";
        private string _jsShipZipApplyClass = "jsShipZipApply";
        private string _paymentNameString = "lptestBilling";
        private string _fedExAddressValidationClass  = "fedExAddressValidation";
        private string _defaultAddressRadioId  = "defaultAddressRadio";
        private string _noChangeAddressRadioId  = "noChangeAddressRadio";
        private string _submitChangesClass = "submitChanges";
        private string _suggestedAddressRadioId  = "suggestedAddressRadio";
        private string _fedExShippingAddress2Id  = "fedExShippingAddress2";
        private string _suggestedAddressId  = "suggestedAddress";
        private string _saveAddressFromModalId  = "saveAddressFromModal";
        private string _paymentInfoPhoneId = "paymentInfoPhone";
        private string _paymentInfoZipCodeId = "paymentInfoZipCode";
        private string _paymentInfoCityId = "paymentInfoCity";
        private string _savedFullNameClass  = "savedFullName";
        private string _shippingAddressContainerClass = "shippingAddressContainer";
        private string _savedAddressClass  = "savedAddress";
        private string _shipToDifferentAddrClass = "shipToDifferentAddr";
        private string _fedExShippingCityId  = "fedExShippingCity";
        private string _fedExShippingZipCodeId  = "fedExShippingZipCode";
        private string _fedExShippingStateId  = "fedExShippingState";
        private string _billingCountrySelector = "[class= 'country valid']";
        private string _shippingAddressInfoContainerClass = "shippingAddressInfoContainer";

        protected virtual string BillingCountryElementId => "shippingCountryField-view30";
        protected string FedExShippingAddress1Id  = "fedExShippingAddress1";
        protected string FedExSuggestedAddress => Browser.Locate.ElementBySelector($"{_suggestedAddressId.ToCssIdSelector()} {HtmlTextWriterTag.Div.ToNthChildSelector(1)}").Text.ToLower();
        protected string FedExSuggestedCity => Browser.Locate.ElementBySelector($"{_suggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[0].TrimStart().ToLower();
        protected string FedExSuggestedState => Browser.Locate.ElementBySelector($"{_suggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[2].TrimStart().ToLower();
        protected string FedExSuggestedZipCode => Browser.Locate.ElementBySelector($"{_suggestedAddressId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}").Text.Split(',', ' ')[3].TrimStart().ToLower();
        protected string FedExSimilarAddress => SuggestedAddressElement.GetAttribute("data-suggested-streetlines");
        protected string FedExSimilarCity => SuggestedAddressElement.GetAttribute("data-suggested-city");
        protected string FedExSimilarState => SuggestedAddressElement.GetAttribute("data-suggested-stateorprovince");
        protected string FedExSimilarZipCode => SuggestedAddressElement.GetAttribute("data-suggested-postalcode");
        protected string FedExMaintainAddress => Browser.Locate.ElementBySelector("#keepAddress").GetAttribute("data-entered-street1");
        protected string FedExMaintainCity => Browser.Locate.ElementBySelector("#keepAddress").GetAttribute("data-entered-city");
        protected string FedExMaintainState => Browser.Locate.ElementBySelector("#keepAddress").GetAttribute("data-entered-stateorprovince");
        protected string FedExMaintainZipCode => Browser.Locate.ElementBySelector("#keepAddress").GetAttribute("data-entered-postalcode");
        protected virtual string AddressCorrectionsMessage => Browser.Locate.ElementByXpath("//*[@id='lpModalContent']/div/div[1]").Text;
        protected virtual string MaintainMessage => Browser.Locate.ElementByXpath("//*[@id='lpModalContent']/div/h1[2]").Text;

        private IElement SaveAddressFromModalButton => Browser.Locate.ElementById(_saveAddressFromModalId);
        private IElement ShipZipField => Browser.Locate.ElementById(_shipZipFieldId);
        private IElement ShipZipApplyBtn => Browser.Locate.ElementByClassName(_jsShipZipApplyClass);
        private IElement BillingCountryField => Browser.Locate.ElementBySelector(_billingCountrySelector);

        private IElement ShippingAddressInfoContainer => Browser.Locate.ElementByClassName(_shippingAddressContainerClass);
        private IElement SavedAddressFullName => Browser.Locate.ElementByClassName(_savedFullNameClass, ShippingAddressInfoContainer);
        private IElement SavedAddressShippingInfo => Browser.Locate.ElementByClassName(_savedAddressClass, ShippingAddressInfoContainer);

        private IElement GetCommonSaveAddressCheckbox(bool getMobileInput = false)
        {
            return SaveAddressCheckbox;
        }

        protected IElement SuggestedAddressElement => Browser.Locate.ElementById(_suggestedAddressId);
        protected IElement SuggestedAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, _suggestedAddressRadioId);
        protected IElement GetValidationErrorElement(IElement formControl) => Browser.Locate.ElementBySelector($"#{formControl.GetAttribute("Id")}-error");
        protected IElement FedExShippingZipCode => Browser.Locate.ElementById(_fedExShippingZipCodeId);
        protected IElement FedExShippingCity => Browser.Locate.ElementById(_fedExShippingCityId);
        protected IElement FedExShippingAddress1 => Browser.Locate.ElementById(FedExShippingAddress1Id);
        protected IElement EditMaintainCurrentAddressLink => Browser.Locate.ElementByAttribute(AttributeSelectorType.Equals, "data-category", "enteredAddress");
        protected IElement NoChangeAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, _noChangeAddressRadioId);
        protected IElement FedExAddressValidationModal => Browser.Locate.ElementByClassName(_fedExAddressValidationClass);
        protected IElement SubmitChanges=> Browser.Locate.ElementByClassName(_submitChangesClass);
        protected IElement FedExShippingAddress2 => Browser.Locate.ElementById(_fedExShippingAddress2Id);
        protected IElement DefaultAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, _defaultAddressRadioId);
        protected IElement ApartmentSuiteOtherField => Browser.Locate.ElementById(_singleShippingAddress2Id);
        protected IElement CityField => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingCityId);
        protected IElement EmailField => Browser.Locate.ElementBySelector(_singleShippingEmailId.ToCssIdSelector());
        protected IElement FirstNameField => Browser.Locate.ElementById(_singleShippingFirstNameId);
        protected IElement Iframe => Browser.Locate.ElementBySelector(_lpModalId.ToCssIdSelector());
        protected IElement LastNameField => Browser.Locate.ElementById(_singleShippingLastNameId);
        protected IElement ShippingCountryDropdown => Browser.Locate.ElementByClassName(_jsShippingCountryClass);
        protected IElement MultiAddressShowAnotherAddressFieldLink => Browser.Locate.ElementByClassName(_showAnotherAddressFieldContainerClass, Iframe);
        protected IElement MultiAddressFirstNameField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingFirstNameId, Iframe);
        protected IElement MultiAddressLastNameField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingLastNameId, Iframe);
        protected IElement MultiAddressAddress1Field => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingAddress1Id, Iframe);
        protected IElement MultiAddressAddress2Field => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingAddress2Id, Iframe);
        protected IElement MultiAddressCityField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingCityId, Iframe);
        protected IElement MultiAddressPhoneNumberField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingPhoneId, Iframe);
        protected IElement MultiAddressStateField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Select, HtmlTextWriterAttribute.Id, _singleShippingStateId, Iframe);
        protected IElement MultiAddressZipCodeField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingZipCodeId, Iframe);
        protected IElement PhoneField => Browser.Locate.ElementBySelector(_singleShippingPhoneId.ToCssIdSelector());
        protected IElement SaveAddressCheckbox => Browser.Locate.ElementImmediately($"{_fieldCheckboxClass.ToCssClassSelector()} > {HtmlTextWriterTag.Label}");
        protected IElement ShowCountryLink => Browser.Locate.ElementById(_showCountryFieldId);
        protected IElement StreetAddressField => Browser.Locate.ElementById(_singleShippingAddress1Id);
        protected IElement ZipPostalCodeField => Browser.Locate.ElementBySelector(_singleShippingZipCodeId.ToCssIdSelector());
        protected IElement BillingFirstNameElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingFirstNameId);
        protected IElement BillingLastNameElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingLastNameId);
        protected IElement BillingPhoneElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingPhoneId);
        protected IElement BillingZipElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingZipCodeId);
        protected IElement BillingAddressLine1Element => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingAddress1Id);
        protected IElement BillingAddressLine2Element => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _singleShippingAddress2Id);
        protected IElement ChangeCountryLinkElement => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, _showCountryFieldClass);
        protected IElement InternationalBillingCityElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _paymentInfoCityId);
        protected IElement InternationalBillingZipCodeElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _paymentInfoZipCodeId);
        protected IElement InternationalBillingPhoneElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, _paymentInfoPhoneId);
        protected IElement ShipToDifferentAddressButton => Browser.Locate.ElementBySelector(_shipToDifferentAddrClass.ToCssClassSelector());
        protected virtual IElement BillingStateElement => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Select, HtmlTextWriterAttribute.Id, _singleShippingStateId);
        protected virtual IElement BillingStateDropdown => Browser.Locate.ElementById(_billingStateSelectorId);
        protected virtual IElement AddAnotherAddressFieldLink => Browser.Locate.ElementByXpath(_addAnotherAddressFieldLinkXpath);
        protected virtual IElement CountryField => Browser.Locate.ElementById(_singleShippingCountryId);
        protected virtual IElement StateField => Browser.Locate.ElementById(_singleShippingStateId);
        protected virtual IElement BillingCountryElement => Browser.Locate.ElementByXpath("//*[contains(@id, \"singleShippingCountry\")]");
        protected virtual IElement FedExAddressValidationHeader => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H1, FedExAddressValidationModal)[1];
        protected virtual IElement FedExShippingState => Browser.Locate.ElementById(_fedExShippingStateId);

        private void EnterNewCountryValueOnPaymentPage(IElement element, IAddress address)
        {
            FillFormSelectByValue(element, address.Country);
        }

        private void SelectCountryByText(string country)
        {
            FillFormControlByText(BillingCountryElement, country);
        }

        private void SelectBillingState(IElement element, string state)
        {
            element.Click();
            
            SelectDropdownByValue(BillingStateDropdown, state);
        }

        protected void FillFormSelectByValue(IElement selectControl, string value) { Browser.Locate.ClickDropdownByValue(selectControl, value); }

        protected virtual void SelectDropdownByValue(IElement element, string optionValue)
        {
            element.FindElement(By.CssSelector($"[value={optionValue}]")).Click();
        }

        //Instances
        protected IBrowser Browser;
        protected Log Log;
        protected SessionSettings Settings;
        protected IAddress Address;

        public CustomerAddressInformationDesktop(IBrowser browser, Log log, SessionSettings settings, IAddress address)
        {
            Address = address;
            Browser = browser;
            Log = log;
            Settings = settings;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public bool IsLoggedInUser => (bool)Browser.ExecuteJs("return window.lp.globals.isLoggedIn");

        public void WaitForFedExModalToStopAnimating()
        {
            Browser.Wait.ForElementToStopAnimating(FedExAddressValidationModal);
        }

        public string GetSavedAddressFullName()
        {
            return SavedAddressFullName.Text;
        }

        public string GetSavedAddressShippingInfo()
        {
            return SavedAddressShippingInfo.Text;
        }

        public string GetShipToDifferentAddressButtonLabel()
        {
            return ShipToDifferentAddressButton.Text;
        }

        public bool IsSimilarVerifiedAddressDisplayed => Browser.Locate.DoesElementExistImmediately(_suggestedAddressRadioId);

        public Dictionary<string, IElement> ShippingElementsCollection => new Dictionary<string, IElement>
        {
            { "FirstNameField", FirstNameField },
            { "LastNameField", LastNameField },
            { "EmailField", EmailField },
            { "PhoneField", PhoneField },
            { "StreetAddressField", StreetAddressField },
            { "ApartmentSuiteOtherField", ApartmentSuiteOtherField },
            { "CountryField", CountryField },
            { "StateField", StateField },
            { "CityField", CityField },
            { "ZipPostalCodeField", ZipPostalCodeField },
        };

        public virtual List<IElement> ShippingElements => new List<IElement> {FirstNameField, LastNameField, EmailField, PhoneField, StreetAddressField,
            ApartmentSuiteOtherField, CountryField, StateField, CityField, ZipPostalCodeField };

        public Dictionary<string, IElement> GetFedExModalElements => new Dictionary<string, IElement>
        {
            { "FedExAddressValidationHeader", FedExAddressValidationHeader },
            { "DefaultAddressRadioElement", DefaultAddressRadioElement },
            { "SuggestedAddressRadioElement", SuggestedAddressRadioElement },
            { "NoChangeAddressRadioElement", NoChangeAddressRadioElement },
            { "SubmitChanges", SubmitChanges },
        };

        public Dictionary<string, IElement> GetFedExModalAddressElements => new Dictionary<string, IElement>
        {
            { "FedExShippingAddress1", FedExShippingAddress1 },
            { "FedExShippingAddress2", FedExShippingAddress2 },
            { "FedExShippingCity", FedExShippingCity },
            { "FedExShippingState", FedExShippingState },
            { "FedExShippingZipCode", FedExShippingZipCode }
        };

        public Dictionary<string, string> GetFedExModalApartmentActiveAddressText => new Dictionary<string, string>
        {
            { "FedExSuggestedAddress", FedExSuggestedAddress },
            { "FedExSuggestedCity", FedExSuggestedCity },
            { "FedExSuggestedState", FedExSuggestedState },
            { "FedExSuggestedZipCode", FedExSuggestedZipCode },
        };

        public Dictionary<string, string> GetFedExModalSuggestedAddressText => new Dictionary<string, string>
        {
            { "FedExSimilarAddress", FedExSimilarAddress },
            { "FedExSimilarCity", FedExSimilarCity },
            { "FedExSimilarState", FedExSimilarState },
            { "FedExSimilarZipCode", FedExSimilarZipCode },
        };

        public Dictionary<string, string> GetFedExModalMaintainAddressText => new Dictionary<string, string>
        {
            { "FedExMaintainAddress", FedExMaintainAddress },
            { "FedExMaintainCity", FedExMaintainCity },
            { "FedExMaintainState", FedExMaintainState },
            { "FedExMaintainZipCode", FedExMaintainZipCode },
            { "MaintainMessage", MaintainMessage },
            { "AddressCorrectionsMessage", AddressCorrectionsMessage },
        };

        public string GetValidationErrorMessage(IElement formControl)
        {
            var element = GetValidationErrorElement(formControl);

            return element != null ? element.Text : string.Empty;
        }

        public void FillFormControlByText(IElement formControl, string text)
        {
            switch (formControl.TagName.ToLowerInvariant())
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

        public virtual void SelectFedExState(IElement element, string state)
        {
            FillFormControlByText(element, state);
        }

        public void EnterApartmentAddress(string apartmentNumber)
        {
            FillFormControlByText(GetFedExShippingAddress2(), apartmentNumber);
        }

        public void SubmitFedExModalChanges()
        {
            SubmitChanges.Click();
            Browser.Wait.ForDomReady();
        }

        public IElement GetFedExShippingAddress2()
        {
            return FedExShippingAddress2;
        }

        public virtual void SelectState(string state, bool isMultiAddress = false)
        {
            FillFormControlByText(isMultiAddress ? MultiAddressStateField : StateField, state);
        }

        public virtual void SelectCountry(IElement element, IAddress address)
        {
            SelectDropdownByValue(element, address.Country);
        }
        
        public void SelectCountry(string country)
        {
            FillFormSelectByValue(ShippingCountryDropdown, country);
        }

        public void CheckShippingFormIsLoaded()
        {
            try //There are times on mobile devices that the Shipping page does not properly load the first time. 
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(_singleShippingFirstNameId.ToCssIdSelector()), -30);
            }
            catch
            {
                Log.Message("Original page load did not load Shipping form.");
                Browser.RefreshPage();
                Browser.Wait.IsVisibleElement(By.CssSelector(_singleShippingFirstNameId.ToCssIdSelector()), 15);
            }
        }

        public void EnterShippingAddress(IAddress address, bool isIntAddress = false, bool isMultiAddress = false)
        {
            if (isMultiAddress)
            {
                if (Settings.IsTabletView)
                {
                    ((IpadBrowser) Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>) Browser.Driver); //Switch to iOS Native context
                    Browser.Wait.IsVisibleElement(By.XPath("//XCUIElementTypeButton[@name='+ Add another address field']"));
                    Browser.Locate.ElementByXpath("//XCUIElementTypeButton[@name='+ Add another address field']").Click();
                    ((IpadBrowser) Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>) Browser.Driver); //Switch back to iOS WebView context
                }
                else
                {
                    Browser.Wait.IsVisibleElement(By.XPath(_multiAddressFirstNameXpath));

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

                SelectState(address.State, true);

                MultiAddressZipCodeField.Clear();
                FillFormControlByText(MultiAddressZipCodeField, address.ZipCode);

                return;
            }

            Browser.Wait.ForDomReady();

            CheckShippingFormIsLoaded();

            if (!Settings.IsMobileView)
            {
                if (Browser.Locate.ElementByClassName(_showAnotherAddressFieldClass).GetAttribute(_ariaExpandedAttribute) == "false")
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

            if(Browser.Locate.DoesElementExistImmediately(_singleShippingEmailId.ToCssIdSelector()))
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

            Browser.Wait.ForCondition(() => ShowCountryLink.GetAttribute(_ariaExpandedAttribute) == "true"); //Need to wait because otherwise the country isn't always changed from the US.

            SelectCountry(CountryField, address);
            Browser.Wait.ForElementToStopAnimating(CountryField);

            if (!isIntAddress)
            {
                SelectState(address.State);
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(_singleShippingZipCodeId.ToCssIdSelector()));
            ZipPostalCodeField.Clear();
            FillFormControlByText(ZipPostalCodeField, address.ZipCode);

            Browser.Wait.ForDomReady();

            // Save Address checkbox is only available for signed in customer
            if (IsLoggedInUser)
            {
                var checkbox = GetCommonSaveAddressCheckbox();
                var checkboxInput = GetCommonSaveAddressCheckbox(true);
                if (checkbox != null && checkbox.IsInitialized)
                {
                    if (address.SaveToProfile && !checkboxInput.Enabled) //Check checkbox if not checked
                    {
                        checkbox.Click();
                    }
                }
            }
        }

        public void UseSimilarVerifiedAddressOption()
        {
            SuggestedAddressRadioElement.Click();
        }

        public void AddAnotherAddressField()
        {
            AddAnotherAddressFieldLink.Click();
        }

        public void KeepCurrentAddressAtFedExModal(bool editButton = false)
        {
            NoChangeAddressRadioElement.Click();
            Browser.Wait.ForDisplayedElement(EditMaintainCurrentAddressLink);

            if (editButton)
            {
                EditMaintainCurrentAddressLink.Click();
            }
        }

        public void ClearFedExModalFields()
        {
            Browser.Wait.IsVisibleElement(By.Id(FedExShippingAddress1Id));

            FedExShippingAddress1.Clear();
            Browser.Wait.ForElementToStopAnimating(FedExShippingAddress1);
            FedExShippingAddress2.Clear();
            Browser.Wait.ForElementToStopAnimating(FedExShippingAddress2);
            FedExShippingCity.Clear();
            Browser.Wait.ForElementToStopAnimating(FedExShippingCity);
            FedExShippingZipCode.Clear();
        }

        public void EnterBillingAddress(IAddress address, bool isIntAddress = false)
        {
            AddAnotherAddressFieldLink.Click();

            BillingFirstNameElement.Clear();
            FillFormControlByText(BillingFirstNameElement, address.FirstName);

            BillingLastNameElement.Clear();
            FillFormControlByText(BillingLastNameElement, address.LastName);

            BillingAddressLine1Element.Clear();
            FillFormControlByText(BillingAddressLine1Element, address.AddressLine1);

            BillingAddressLine2Element.Clear();
            FillFormControlByText(BillingAddressLine2Element, address.AddressLine2);

            ChangeCountryLinkElement.Click();

            if (isIntAddress)
            {
                SelectCountry(BillingCountryElement, address);
                Browser.TabKeyboard();
                Browser.Wait.IsVisibleElement(By.Id(_paymentInfoCityId));
                InternationalBillingCityElement.Clear();
                FillFormControlByText(InternationalBillingCityElement, address.City);
                InternationalBillingPhoneElement.Clear();
                FillFormControlByText(InternationalBillingPhoneElement, address.Phone);
                InternationalBillingZipCodeElement.Clear();
                FillFormControlByText(InternationalBillingZipCodeElement, address.ZipCode);
            }
            else
            {
                Browser.TabKeyboard();
                CityField.Clear();
                FillFormControlByText(CityField, address.City);
                BillingPhoneElement.Clear();
                FillFormControlByText(BillingPhoneElement, address.Phone);
                BillingZipElement.Clear();
                FillFormControlByText(BillingZipElement, address.ZipCode);
            }
            
            if (!isIntAddress)
            {
                SelectBillingState(BillingStateElement, address.State);
            }
        }

        public virtual void ChangeBillingCountry(IAddress address)
        {
            EnterNewCountryValueOnPaymentPage(BillingCountryElement, address);
        }

        public void ChangeShippingZip(IAddress address)
        {
            ShipZipField.Clear();
            ShipZipField.SendKeys(address.ZipCode);
            ShipZipApplyBtn.Click();
        }

        public string GetPaymentName()
        {
            return _paymentNameString;
        }

        public void SaveAddressFromModal()
        {
            SaveAddressFromModalButton.Click();
        }

        public void SelectSavedAddressShippingInfo()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_shippingAddressInfoContainerClass));
            SavedAddressShippingInfo.Click();
        }
    }
}
