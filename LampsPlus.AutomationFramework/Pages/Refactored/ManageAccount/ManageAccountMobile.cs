using System;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount
{
    public class ManageAccountMobile : ManageAccountDesktop, IManageAccountMobile
    {
        //Class members
        private string _forEmailClass = "forEmail";
        private string _editChangePasswordXpath = "//*[@id='editChangePassword']";
        private string _manageAccountMessageClass = "manage-account__message";
        private string _changePasswordDrawerId  = "changePasswordDrawer";
        private string _changePasswordCloseButtonXpath  = "//*[@id='changePasswordDrawer']//button[contains(@class, 'lpMobileOverlayClose')]";
        private string _shippingAddressesLinkXpath = "//a[@href='/account/profile/shipping-addresses/' and text()='Manage']";
        private string _cardFormDrawerId = "cardFormDrawer";
        private string _monthDropdownXpath = "//*[contains(@for,'cardMonthSelect')]//following-sibling::div[1]/button";
        private string _yearDropdownXpath  = "//*[contains(@class,'fieldNoLabel')]/div[1]/button";
        private string _lpSelectMobileDrawerCardYearSelectId = "lpSelectMobileDrawer__cardYearSelect";
        private string _stateDropdownXpath = "//*[contains(@for,'stateSelect')]//following-sibling::div[1]/button";
        private string _lpSelectMobileDrawerStateSelectId = "lpSelectMobileDrawer__stateSelect";
        private string _addShippingAddressBtnId = "addShippingAddressBtn";
        private string _txtShipToFirstNameId = "txtShipToFirstName";
        private string _txtAddress2Id = "txtAddress2";
        private string _countryDropDownSelector = "//*[@for='ddlCountry']//following-sibling::div[1]/button";
        private string _lpSelectMobileDrawerDdlStateSelector = "#lpSelectMobileDrawer__ddlState";
        private string _txtZipId = "txtZip";
        private string _txtShipToLastNameId = "txtShipToLastName";
        private string _showCountryDropdownLinkId = "showCountryDropdownLink";
        private string _selectStateSelector = "//button[text()='Select State/Province']";
        private string _txtShippingPhoneId = "txtShippingPhone";
        private string _btnSaveShippingAddressId = "btnSaveShippingAddress";
        private string _divFullNameId = "divFullname";
        private string _divAddress1Id = "divAddress1";
        private string _divCityStateZipId = "divCityStateZip";
        private string _divPhoneNumberId = "divPhoneNumber";
        private string _editCustomerAccountId = "editCustomerAccount";
        private string _lpScrollContainerClass = "lpScrollContainer";
        private string _editPaymentButtonXpath = "//button[@class='option__action anchorLink jsEdit']";
        private string _addShippingAddressDrawerId = "addShippingAddressDrawer";
        private string _editShippingAddressOptionButtonXpath = "//div[@id='divShippingAddressContainer'][1]//a[@class='section__data__link edit-address']";
        private string _editEmailPrefXpath = "//*[@id=\"editEmailPref\"]";
        private string _emailPrefThankYouMessageXpath = "//*[@id=\"emailPrefDrawer\"]//div[@class=\"manage-account__message\"]";
        private string _selectedStateDropdownXpath = "//button[@id='buttonTrigger__ddlState']";
        private string _stateShippingXpath = "//*[@id=\"lpSelectMobileDrawer__ddlState\"]//ul";
        private string _divShippingAddressContainerId = "divShippingAddressContainer";
        private string _divAddress2Id = "divAddress2";
        private string _divCountryId = "divCountry";
        private string _emailPrefCloseButtonXpath = "//*[@id=\"emailPrefDrawer\"]//button[contains(@class, 'lpMobileOverlayClose')]";
        private string _accountDrawerId = "accountDrawer";
        private string _profilePhoneNumberId = "profile-phonenumber";
        private string _paymentOptionsLinkClass = "paymentOptionsLink";
        private string _divFullnameSelector = ".section__data-default #divFullname";
        private string _divCityStateZipSelector = ".section__data-default #divCityStateZip";
        private string _manageAccountHeaderSubtextClass = "manage-account__header__subtext";
        private string _divAddress1Selector = ".section__data-default #divAddress1";
        private string _divPhoneNumberSelector = ".section__data-default #divPhoneNumber";

        private IElement PaymentOptionDrawer => Browser.Locate.ElementById(_cardFormDrawerId);
        private IElement SavedPaymentOptions => Browser.Locate.ElementByClassName(OptionClass);
        private IElement DropdownMonth => Browser.Locate.ElementByXpath(_monthDropdownXpath);
        private IElement DropdownYear => Browser.Locate.ElementByXpath(_yearDropdownXpath);
        private IElement CreditCardYearSelectDrawer => Browser.Locate.ElementByAttributeStartsWith(HtmlTextWriterAttribute.Id, _lpSelectMobileDrawerCardYearSelectId);
        private IElement DropdownState => Browser.Locate.ElementByXpath(_stateDropdownXpath);
        private IElement StateSelectDrawer => Browser.Locate.ElementByAttributeStartsWith(HtmlTextWriterAttribute.Id, _lpSelectMobileDrawerStateSelectId);
        private IElement ScrollablePaymentOverlay => Browser.Locate.ElementsByClassName(_lpScrollContainerClass)[5];
        private IElement ScrollableShippingAddressOverlay => Browser.Locate.ElementsByClassName(_lpScrollContainerClass)[3];
        private IElement ShippingAddressDrawer => Browser.Locate.ElementBySelector(_addShippingAddressDrawerId.ToCssIdSelector());
        private IElement EmailPreferencesModalCloseButton => Browser.Locate.ElementByXpath(_emailPrefCloseButtonXpath);
        private IElement AccountProfileDrawer => Browser.Locate.ElementById(_accountDrawerId);
        private IElement ChangePasswordModalCloseButton => Browser.Locate.ElementByXpath(_changePasswordCloseButtonXpath);

        private void WaitForSavedPaymentOptionToRender()
        {
            Browser.Wait.ForMobileModalToFullyClose(PaymentOptionDrawer);

            // On Mobile, after saving payment option, the site (using CSS transition) changes the opacity of the container of old payment option to 0.
            // Then it updates the old payment option with new payment option, then it sets the container to opacity 1 to show the updated content.
            // That's why we have to wait for opacity 1 to make sure the updated payment option is fully rendered on the page.
            Browser.Wait.ForCondition(() => Browser.GetElementOpacity(SavedPaymentOptions) == "1");
        }

        private void SelectShippingDropDownByValue(IElement dropDownElement, string valueAttribute)
        {
            Browser.ScrollIntoView(dropDownElement);
            dropDownElement.Click();
            Browser.Wait.IsVisibleElement(By.XPath($"//*[@data-value='{valueAttribute}']"));
            var valueElement = Browser.Locate.ElementByXpath($"//*[@data-value='{valueAttribute}']");
            Browser.ScrollIntoView(valueElement);
            valueElement.Click();
        }

        private void ClickDropdownByValue(IElement element, string optionValue)
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

        private bool IsManageAccountShippingFormVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_btnSaveShippingAddressId.ToCssIdSelector()), timeToWait);
        }

        private bool IsSavePasswordScreeVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(BtnSavePasswordId.ToCssIdSelector()), timeToWait);
        }

        protected override IElement ChangePasswordLink => Browser.Locate.ElementByXpath(_editChangePasswordXpath);
        protected override IElement ChangePasswordModalMessage => Browser.Locate.ElementBySelector($"{_changePasswordDrawerId.ToCssIdSelector()} {_manageAccountMessageClass.ToCssClassSelector()}");
        protected override IElement ManageShippingAddressesLinkForElement => Browser.Locate.ElementByXpath(_shippingAddressesLinkXpath);
        protected override IElement BtnAddShippingAddress => Browser.Locate.ElementBySelector(_addShippingAddressBtnId.ToCssIdSelector());
        protected override IElement ShippingFirstNameField => Browser.Locate.ElementById(_txtShipToFirstNameId);
        protected override IElement ShippingLastNameField => Browser.Locate.ElementById(_txtShipToLastNameId);
        protected override IElement ShowCountryLink => Browser.Locate.ElementById(_showCountryDropdownLinkId);
        protected override IElement ShippingStateField => Browser.Locate.ElementByXpath(_selectStateSelector);
        protected override IElement ShippingZipCodeField => Browser.Locate.ElementById(_txtZipId);
        protected override IElement ShippingPhoneField => Browser.Locate.ElementById(_txtShippingPhoneId);
        protected override IElement BtnSaveShippingAddress => Browser.Locate.ElementById(_btnSaveShippingAddressId);
        protected override IElement AccountProfileEditInfo => Browser.Locate.ElementById(_editCustomerAccountId);
        protected override IElement EditPaymentOption => Browser.Locate.ElementByXpath(_editPaymentButtonXpath);
        protected override IElement EditShippingAddressOption => Browser.Locate.ElementByXpath(_editShippingAddressOptionButtonXpath);
        protected override IElement ChangeEmailPreferencesLink => Browser.Locate.ElementByXpath(_editEmailPrefXpath);
        protected override IElement AccountProfilePhoneNumberField => Browser.Locate.ElementBySelector(_profilePhoneNumberId.ToCssIdSelector());
        protected override IElement ManagePaymentOptionsLinkForElement => Browser.Locate.ElementByClassName(_paymentOptionsLinkClass);
        protected override IElement ShippingAddressFullnamePage => Browser.Locate.ElementBySelector(_divFullnameSelector);
        protected override IElement ShippingCityStateZip => Browser.Locate.ElementBySelector(_divCityStateZipSelector);
        protected override IElement RewardsNumberElement => Browser.Locate.ElementByClassName(_manageAccountHeaderSubtextClass);
        protected override IElement ShippingAddressLineOne => Browser.Locate.ElementBySelector(_divAddress1Selector);
        protected override IElement ShippingPhoneNumber => Browser.Locate.ElementBySelector(_divPhoneNumberSelector);

        //Instances
        public ManageAccountMobile(IBrowser browser, AccountActions accountActions, IAssert assert, IModalDesktop modal, IAddress address) : base(browser, accountActions, assert, modal, address) { }

        //Interface implementation
        public override void SaveNewPassword(string newPassword)
        {
            Browser.Wait.ForClickableElement(ChangePasswordLink);
            Browser.ClickOnButtonMultipleTimes(ChangePasswordLink, 5, IsSavePasswordScreeVisible);

            TextNewPasswordField.SendKeys(newPassword);
            TextConfirmPasswordField.SendKeys(newPassword);

            SaveNewPasswordBtn.Click();
            Browser.Wait.ForDisplayedElement(ChangePasswordModalMessage);

            ChangePasswordModalCloseButton.Click(); // On mobile, changing the password occurs in a modal and must be closed to proceed.
        }

        public override void AddNewPaymentMethod(CreditCard creditCard, IAddress address)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(JsAddClass.ToCssClassSelector()));
            Browser.ClickOnButtonMultipleTimes(AddPaymentOptionButton, 5, IsPaymentFormVisible);

            SetPaymentCard(creditCard);
            SetPaymentAddress(address);

            Browser.ScrollIntoView(SavePaymentBtn);
            Browser.Wait.IsVisibleElement(By.CssSelector(JsSaveClass.ToCssClassSelector()));
            SavePaymentBtn.Click();

            WaitForSavedPaymentOptionToRender();
        }

        public override void SetPaymentCard(CreditCard testCreditCard)
        {
            Browser.Wait.ForMobileModalToFullyOpen(PaymentOptionDrawer);
            TextCardNumberField.Clear();
            TextCardNumberField.SendKeys(testCreditCard.CardNumber);
            TextNameOnCardField.Clear();
            TextNameOnCardField.SendKeys(testCreditCard.NameOnCard);

            SelectShippingDropDownByValue(DropdownMonth, testCreditCard.ExpirationMonth.ToString());
            SelectShippingDropDownByValue(DropdownYear, testCreditCard.ExpirationYear.ToString());
            Browser.Wait.ForElementToStopAnimating(CreditCardYearSelectDrawer);
        }

        public override void SetPaymentAddress(IAddress address)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(address.FirstName);
            LastNameField.Clear();
            LastNameField.SendKeys(address.LastName);
            AddressField.Clear();
            AddressField.SendKeys(address.AddressLine1);
            Address2Field.Clear();
            Address2Field.SendKeys(address.AddressLine2);
            CityField.Clear();
            CityField.SendKeys(address.City);
            SelectShippingDropDownByValue(DropdownState, address.State);
            Browser.Wait.ForElementToStopAnimating(StateSelectDrawer);
            ZipCodeField.Click();
            ZipCodeField.Clear();
            ZipCodeField.SendKeys(address.ZipCode);
            PhoneNumberField.Clear();
            PhoneNumberField.SendKeys(address.Phone);
        }

        public override string GetEmailPreferenceHeaderText()
        {
            return Browser.Locate.ElementByClassName(_forEmailClass).Text;
        }

        public override void OpenShippingAddressForm()
        {
            ManageShippingAddressesLinkForElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_addShippingAddressBtnId.ToCssIdSelector()));
            Browser.ClickOnButtonMultipleTimes(BtnAddShippingAddress, 5, IsManageAccountShippingFormVisible);
        }

        public override void AddNewShippingAddressToModal(IAddress shippingAddress)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_txtShipToFirstNameId.ToCssIdSelector()));
            ShippingFirstNameField.SendKeys(shippingAddress.FirstName);
            ShippingLastNameField.SendKeys(shippingAddress.LastName);
            ShippingAddressLineOneField.SendKeys(shippingAddress.AddressLine1);
            Browser.Wait.IsVisibleElement(By.CssSelector(_txtAddress2Id.ToCssIdSelector()));
            ShippingAddressLineTwoField.SendKeys(shippingAddress.AddressLine2);
            ShippingCityField.SendKeys(shippingAddress.City);

            if (!string.IsNullOrWhiteSpace(shippingAddress.Country))
            {
                ShowCountryLink.Click();
                SelectShippingDropDownByValue(Browser.Locate.ElementByXpath(_countryDropDownSelector), shippingAddress.Country);
            }

            Browser.Wait.ForClickableElement(ShippingStateField, 5);
            ShippingStateField.Click();

            if (shippingAddress.State == "N/A")
            {
                Browser.Wait.IsVisibleElement(By.XPath($"//*[@data-text='{shippingAddress.State}']"));
                var valueElement = Browser.Locate.ElementByXpath($"//*[@data-text='{shippingAddress.State}']");
                Browser.ScrollIntoView(valueElement);
                valueElement.Click();
            }
            else
            {
                ClickDropdownByValue(Browser.Locate.ElementBySelector(_lpSelectMobileDrawerDdlStateSelector), shippingAddress.State);
            }

            Browser.Wait.IsInvisibleElement(By.CssSelector(_lpSelectMobileDrawerDdlStateSelector));
            Browser.Wait.IsVisibleElement(By.CssSelector(_txtZipId.ToCssIdSelector()));
            ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
            ShippingPhoneField.SendKeys(shippingAddress.Phone);
        }

        public override void SaveShippingAddress()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_btnSaveShippingAddressId.ToCssIdSelector()));
            Browser.ClickByJs(BtnSaveShippingAddress);
            Browser.Wait.ForCondition(() => ShippingAddressDrawer.GetAttribute("aria-hidden") == "true");
        }

        public override void SetNewPassword(string newPassword) 
        { 
            Browser.Wait.IsVisibleElement(By.Id(BtnSavePasswordId));

            TextNewPasswordField.SendKeys(newPassword);
            TextConfirmPasswordField.SendKeys(newPassword);

            SaveNewPasswordBtn.Click();
            Browser.Wait.ForDisplayedElement(ChangePasswordModalMessage);
        }

        public override void NavigateToChangePasswordLink()
        {
            Browser.Wait.ForClickableElement(ChangePasswordLink);
            Browser.ExecuteJs("arguments[0].click()", ChangePasswordLink.InternalElement);
        }

        public override void SetOriginalPassword(string originalPassword)
        {
            Browser.Wait.IsVisibleElement(By.Id(BtnSavePasswordId));

            TextNewPasswordField.Clear();
            TextConfirmPasswordField.Clear();
            TextNewPasswordField.SendKeys(originalPassword);
            TextConfirmPasswordField.SendKeys(originalPassword);

            SaveNewPasswordBtn.Click();
            Browser.Wait.ForDisplayedElement(ChangePasswordModalMessage);

            ChangePasswordModalCloseButton.Click(); 
        }

        public override void ClosePaymentModal()
        {
            Browser.ScrollIntoView(SavePaymentBtn);
            SavePaymentBtn.Click();
            Browser.Wait.ForMobileModalToFullyClose(PaymentOptionDrawer);
            Browser.Wait.ForCondition(() => Browser.GetElementOpacity(SavedPaymentOptions) == "1");
        }

        public IElement GetPaymentScrollableOverlay()
        {
            return ScrollablePaymentOverlay;
        }

        public IElement GetShippingAddressScrollableOverlay()
        {
            return ScrollableShippingAddressOverlay;
        }

        public override void OpenEditPaymentModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_editPaymentButtonXpath));
            Browser.ClickOnButtonMultipleTimes(EditPaymentOption, 5, IsPaymentFormVisible);
        }

        public override void OpenEditShippingAddressModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_editShippingAddressOptionButtonXpath));
            Browser.ClickOnButtonMultipleTimes(EditShippingAddressOption, 5, IsShippingAddressFormVisible);
        }

        public override void SaveEmailPreferences()
        {
            SaveEmailPreferencesButton.Click();
            Browser.Wait.IsVisibleElement(By.XPath(_emailPrefThankYouMessageXpath));
        }

        public override void EditPaymentOptionDetails(CreditCard creditCard, IAddress address)
        {
            Browser.Wait.ForClickableElement(EditPaymentOption).Click();
            Browser.Wait.ForMobileModalToFullyOpen(PaymentOptionDrawer);

            SetPaymentCard(creditCard);
            SetPaymentAddress(address);

            Browser.ScrollIntoView(SavePaymentBtn);
            Browser.Wait.IsVisibleElement(By.CssSelector(JsSaveClass.ToCssClassSelector()));
            SavePaymentBtn.Click();

            WaitForSavedPaymentOptionToRender();
        }

        public void ClearSelectedState()
        {
            var stateDropDown = Browser.Locate.ElementByXpath(_selectedStateDropdownXpath);
            stateDropDown.Click();
            ClickDropdownByValue(Browser.Locate.ElementByXpath(_stateShippingXpath), "Select");
        }

        public override IAddress GetFirstSavedShippingAddress()
        {
            var defaultShippingAddressContainer = Browser.Locate.ElementBySelector(_divShippingAddressContainerId.ToCssIdSelector());

            var shippingAddress = new Address.Address
            {
                AddressLine1 = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divAddress1Id, defaultShippingAddressContainer).Text,
                AddressLine2 = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divAddress2Id, defaultShippingAddressContainer).Text,
                City = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divCityStateZipId, defaultShippingAddressContainer).Text, // City, State ZIP are all in the same container
                FirstName = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divFullNameId, defaultShippingAddressContainer).Text, // FirstName LastName
                Phone = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divPhoneNumberId, defaultShippingAddressContainer).Text,
                Country = Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Id, _divCountryId, defaultShippingAddressContainer).Text
            };

            return shippingAddress;
        }

        public override void OpenEmailPreferencesModal()
        {
            ChangeEmailPreferencesLink.Click();
            Browser.Wait.ForDisplayedElement(SaveEmailPreferencesBtn);
        }

        public void CloseEmailPreferencesModal()
        {
            EmailPreferencesModalCloseButton.Click();
            Browser.Wait.ForDomReady();
        }

        public override void EditAccountContactNumbers(string phoneNumber, string faxNumber, string cellPhoneNumber)
        {
            AccountProfileEditInfo.Click();
            Browser.Wait.ForMobileModalToFullyOpen(AccountProfileDrawer);
            Browser.Wait.ForDisplayedElement(TextPhoneField).Clear();

            TextPhoneField.SendKeys(phoneNumber);
            TextFaxField.Clear();
            TextFaxField.SendKeys(faxNumber);
            TextCellField.Clear();
            TextCellField.SendKeys(cellPhoneNumber);
            SaveInfoBtn.Click();

            Browser.Wait.ForMobileModalToFullyClose(AccountProfileDrawer);
        }

        public override bool IsModalThankYouMessageVisible()
        {
            return Browser.Wait.IsVisibleElement(By.XPath(_emailPrefThankYouMessageXpath));
        }

        public bool IsRewardNumberVisible()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_manageAccountHeaderSubtextClass.ToCssClassSelector()));
        }

        public void SelectAddShippingAddress()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_addShippingAddressBtnId.ToCssIdSelector()));
            Browser.ClickOnButtonMultipleTimes(BtnAddShippingAddress, 5, IsManageAccountShippingFormVisible);
        }
    }
}