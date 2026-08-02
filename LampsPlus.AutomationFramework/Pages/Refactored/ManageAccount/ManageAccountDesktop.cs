using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount
{
    public class ManageAccountDesktop : IManageAccountDesktop
    {
        //Class members
        private string _accountTitleClass = "accountTitle";
        private string _shippingAddressesLinkXpath = "//a[@href='/account/profile/shipping-addresses/' and text()='Shipping Addresses']";
        private string _cardNumberName = "cardNumber";
        private string _cardFullNameName = "cardFullName";
        private string _cardMonthName = "cardMonth";
        private string _cardYearName = "cardYear";
        private string _changePasswordContentId = "changePasswordContent";
        private string _lastNameClass = "lastName";
        private string _address2Class = "address2";
        private string _address1Class = "address1";
        private string _cityClass = "city";
        private string _stateName = "State";
        private string _manageShippingAddressContentXpath = "//*[contains(@class,'jsAdd')]";
        private string _txtFirstNameId = "txtFirstName";
        private string _txtLastNameId = "txtLastName";
        private string _txtAddress1Id = "txtAddress1";
        private string _txtAddress2Id = "txtAddress2";
        private string _txtCityId = "txtCity";
        private string _jsShowCountryClass = "jsShowCountry";
        private string _countryClass = "country";
        private string _ddlStateProvinceId = "ddlStateProvince";
        private string _txtZipCodeId = "txtZipCode";
        private string _txtPhoneId = "txtPhone";
        private string _optionAddress3Class = "option__address3";
        private string _optionPhoneClass = "option__phone";
        private string _optionAddress1Class = "option__address1";
        private string _editPaymentOptionButtonXpath = "//div[@class='option'][1]//button[@class='option__action jsEdit anchorLink']";
        private string _paymentOptionsString = "/account/profile/paymentoptions/";
        private string _shippingAddressLinkClass = "shippingAddressLink";
        private string _modalEditShippingId = "modalEditShipping";
        private string _calloutBtnClass = "calloutBtn";
        private string _optionNameClass = "option__name";
        private string _btnSaveShippingAddressId = "btnSaveShippingAddress";
        private string _modalEditId = "modal-edit";
        private string _profilePhoneId = "profile-phone";
        private string _txtPhoneAttribute = "txtPhone";
        private string _btnSaveUserProfileId = "btnSaveUserProfile";
        private string _modifiedShippingPhoneNumber = "7777777777";
        private string _recentlyViewedContainerId = "recentlyViewedContainer";
        private string _editShippingAddressOptionButtonXpath = "//div[@class='option'][1]//a[@class='option__action jsEdit']";
        private string _modalEmailPreferencesId = "modal-emailpreferences";
        private string _btnSaveEmailPrefId = "btnSaveEmailPref";
        private string _emailPrefOptionsId = "emailPrefOptions";
        private string _emailPrefThankYouMessageXpath = "//*[@id=\"modalEmailPreferences\"]";
        private string _jsRemoveClass = "jsRemove";
        private string _optionCardTypeClass = "option__cardtype";
        private string _optionExpirationClass = "option__expiration";
        private string _optionBillingNameClass = "option__billing-name";
        private string _optionAddress2Class = "option__address2";
        private string _optionHeaderDefaultClass = "option__header--default";
        private string _lab90031Id = "lab_90031";
        private string _txtFaxId = "txtFax";
        private string _txtCellId = "txtCell";
        private string _profileFullname = "profile-fullname";
        private string _firstNameClass = "firstName";
        private string _zipCodeClass = "zipCode";
        private string _intPhoneClass = "intPhone";
        private string _modalChangePasswordId = "modal-changepassword";
        private string _txtNewPasswordId = "txtNewPassword";
        private string _txtConfirmPasswordId = "txtConfirmPassword";
        private string _changePasswordThankYouMessageSelector = "#lpModalContent .modal-message";
        private string _emailPreferencesResponseMessageSelector = "#emailPreferencesResponseMessage .modal-message";
        private string RewardNumber => RewardsNumberElement.Text.Replace("Customer #:", string.Empty).Trim();
        private string RemoveFormatting(string originalString) { return originalString.Replace("\r\n", string.Empty).Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace("+", string.Empty); }

        protected string BtnSavePasswordId => "btnSavePassword";
        protected string JsAddClass => "jsAdd";
        protected string JsSaveClass => "jsSave";
        protected string OptionClass => "option";

        private IElement FirstDeletedSavedPaymentLink => Browser.Locate.ElementByClassName(_jsRemoveClass);
        private IElement FirstCreditCardInfo => Browser.Locate.ElementByClassName(OptionClass);
        private IElement DefaultShippingAddressItemHeader => Browser.Locate.ElementByClassName(_optionHeaderDefaultClass);
        private IElement SubscribeRadioButton => Browser.Locate.ElementByXpath("(//label[text()='Subscribe'])[1]");
        private IElement UnsubscribeRadioButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.Id, _lab90031Id);
        private IElement AccountProfileFullName => Browser.Locate.ElementById(_profileFullname);
        private IElement SaveShippingAddressCheckBox => Browser.Locate.ElementById(_btnSaveShippingAddressId);
        private IElement ShippingCountryOption => Browser.Locate.ElementByClassName(_countryClass);
        private IElement TextExpMonthField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _cardMonthName);
        private IElement TextExpYearField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _cardYearName);
        private IElement DdlStateProvinceField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _stateName);

        protected IElement SaveEmailPreferencesButton => Browser.Locate.ElementById(_btnSaveEmailPrefId);
        protected IElement TextNewPasswordField => Browser.Locate.ElementById(_txtNewPasswordId);
        protected IElement TextConfirmPasswordField => Browser.Locate.ElementById(_txtConfirmPasswordId);
        protected IElement SaveNewPasswordBtn => Browser.Locate.ElementById(BtnSavePasswordId);
        protected IElement ShippingAddressLineOneField => Browser.Locate.ElementById(_txtAddress1Id);
        protected IElement ShippingCityField => Browser.Locate.ElementById(_txtCityId);
        protected IElement ShippingAddressLineTwoField => Browser.Locate.ElementBySelector(_txtAddress2Id.ToCssIdSelector());
        protected IElement AddPaymentOptionButton => Browser.Locate.ElementByClassName(JsAddClass);
        protected IElement TextCardNumberField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _cardNumberName);
        protected IElement TextNameOnCardField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _cardFullNameName);
        protected IElement SavePaymentBtn => Browser.Locate.ElementByClassName(JsSaveClass);
        protected IElement PhoneNumberField => Browser.Locate.ElementBySelector(_intPhoneClass.ToCssClassSelector());
        protected IElement FirstNameField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, _firstNameClass);
        protected IElement LastNameField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, _lastNameClass);
        protected IElement Address2Field => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, _address2Class);
        protected IElement AddressField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, _address1Class);
        protected IElement CityField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, _cityClass);
        protected IElement ZipCodeField => Browser.Locate.ElementBySelector(_zipCodeClass.ToCssClassSelector());
        protected IElement TextFaxField => Browser.Locate.ElementById(_txtFaxId);
        protected IElement TextCellField => Browser.Locate.ElementById(_txtCellId);
        protected IElement TextPhoneField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _txtPhoneAttribute);
        protected IElement SaveInfoBtn => Browser.Locate.ElementBySelector(_btnSaveUserProfileId.ToCssIdSelector());
        protected IElement RecentlyViewedWidgetContainer => Browser.Locate.ElementBySelector(_recentlyViewedContainerId.ToCssIdSelector());
        protected IElement RadioButtonSection => Browser.Locate.ElementById(_emailPrefOptionsId);
        protected virtual IElement EditShippingAddressOption => Browser.Locate.ElementByXpath(_editShippingAddressOptionButtonXpath);
        protected virtual IElement EditPaymentOption => Browser.Locate.ElementByXpath(_editPaymentOptionButtonXpath);
        protected virtual IElement ChangePasswordLink => Browser.Locate.ElementById(_modalChangePasswordId);
        protected virtual IElement AccountProfileEditInfo => Browser.Locate.ElementBySelector(_modalEditId.ToCssIdSelector());
        protected virtual IElement ChangePasswordModalMessage => Browser.Locate.ElementBySelector(_changePasswordThankYouMessageSelector);
        protected virtual IElement ManageShippingAddressesLinkForElement => Browser.Locate.ElementByXpath(_shippingAddressesLinkXpath);
        protected virtual IElement BtnAddShippingAddress => Browser.Locate.ElementByXpath(_manageShippingAddressContentXpath);
        protected virtual IElement BtnSaveShippingAddress => Browser.Locate.ElementBySelector($"#{_modalEditShippingId} .{_calloutBtnClass}");
        protected virtual IElement ShippingAddressFullnamePage => Browser.Locate.ElementBySelector($"{OptionClass.ToCssClassSelector().ToLastChildSelector()} {_optionNameClass.ToCssClassSelector()}");
        protected virtual IElement ShippingFirstNameField => Browser.Locate.ElementById(_txtFirstNameId);
        protected virtual IElement ShippingLastNameField => Browser.Locate.ElementById(_txtLastNameId);
        protected virtual IElement ShowCountryLink => Browser.Locate.ElementByClassName(_jsShowCountryClass);
        protected virtual IElement ShippingStateField => Browser.Locate.ElementById(_ddlStateProvinceId);
        protected virtual IElement ShippingZipCodeField => Browser.Locate.ElementById(_txtZipCodeId);
        protected virtual IElement ShippingPhoneField => Browser.Locate.ElementById(_txtPhoneId);
        protected virtual IElement ShippingCityStateZip => Browser.Locate.ElementBySelector($"{OptionClass.ToCssClassSelector().ToLastChildSelector()} {_optionAddress3Class.ToCssClassSelector()}");
        protected virtual IElement ShippingPhoneNumber => Browser.Locate.ElementBySelector($"{OptionClass.ToCssClassSelector().ToLastChildSelector()} {_optionPhoneClass.ToCssClassSelector()}");
        protected virtual IElement ShippingAddressLineOne => Browser.Locate.ElementBySelector($"{OptionClass.ToCssClassSelector().ToLastChildSelector()} {_optionAddress1Class.ToCssClassSelector()}");
        protected virtual IElement ChangeEmailPreferencesLink => Browser.Locate.ElementById(_modalEmailPreferencesId);
        protected virtual IElement SaveEmailPreferencesBtn => Browser.Locate.ElementById(_btnSaveEmailPrefId);
        protected virtual IElement ModalMessage => Browser.Locate.ElementBySelector(_emailPreferencesResponseMessageSelector);
        protected virtual IElement AccountProfilePhoneNumberField => Browser.Locate.ElementBySelector(_profilePhoneId.ToCssIdSelector());
        protected virtual IElement RewardsNumberElement => Browser.Locate.ElementByXpath("//div[@class='section__header--right']");
        protected virtual IElement ManagePaymentOptionsLinkForElement => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, HtmlTextWriterAttribute.Href, _paymentOptionsString);

        private ReadOnlyCollection<IElement> PaymentContainer => Browser.Locate.ElementsBySelector(OptionClass.ToCssClassSelector());

        //Instances
        protected IBrowser Browser;
        protected AccountActions AccountActions;
        protected IModalDesktop Modal;
        protected IAddress Address;
        protected IAssert Assert;

        public ManageAccountDesktop(IBrowser browser, AccountActions accountActions, IAssert assert, IModalDesktop modal, IAddress address)
        {
            Browser = browser;
            AccountActions = accountActions;
            Modal = modal;
            Address = address;
            Assert = assert;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/account/profile/";
        public string PaymentOptionsUrl => "paymentoptions/";
        public string ShippingAddressOptionsUrl => "shipping-addresses/";
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_shippingAddressLinkClass.ToCssClassSelector()));
        public bool IsManageShippingAddressesLinkVisible => ManageShippingAddressesLinkForElement.Displayed;
        public bool IsManagePaymentOptionsLinkVisible => (ManagePaymentOptionsLinkForElement.IsInitialized || ManagePaymentOptionsLinkForElement.Displayed);
        
        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public virtual IBrowser Navigate(string url)//Method to add page path
        {
            var expectedUrl = PageUrl + url;

            // Navigate to PageUrl
            Browser.Navigate(expectedUrl);

            return Browser;
        }

        public virtual void SaveNewPassword(string newPassword)
        {
            ChangePasswordLink.Click();

            TextNewPasswordField.SendKeys(newPassword);
            TextConfirmPasswordField.SendKeys(newPassword);
            SaveNewPasswordBtn.Click();

            Modal.IsModalVisible();

            Modal.GetLpModalClose().Click();
        }

        public void ResetAccountShippingAddresses()
        {
            AccountActions.ResetShippingAddresses(RewardNumber);
        }

        public void ResetAccountPaymentOptions()
        {
            AccountActions.ResetPaymentOptions(RewardNumber);
        }

        public virtual void AddNewPaymentMethod(CreditCard creditCard, IAddress address)
        {
            Browser.ClickOnButtonMultipleTimes(AddPaymentOptionButton, 5, IsPaymentFormVisible);

            SetPaymentCard(creditCard);
            SetPaymentAddress(address);

            SavePaymentBtn.Click();
            Browser.Wait.UntilElementUnloads(Browser.Locate.ElementBySelector(Modal.LpModalId.ToCssIdSelector()));
        }

        public virtual void EditPaymentOptionDetails(CreditCard creditCard, IAddress address)
        {
            Browser.ClickOnButtonMultipleTimes(EditPaymentOption, 5, IsPaymentFormVisible);

            SetPaymentCard(creditCard);
            Browser.Wait.ForDomReady();
            SetPaymentAddress(address);

            SavePaymentBtn.Click();

            Browser.Wait.ForElementToStopAnimating(Modal.GetLpModal());
        }

        public virtual void SetPaymentCard(CreditCard testCreditCard)
        {
            TextNameOnCardField.Clear();
            TextNameOnCardField.SendKeys(testCreditCard.NameOnCard);
            TextCardNumberField.Clear();
            TextCardNumberField.SendKeys(testCreditCard.CardNumber);

            new SelectElement(TextExpMonthField.InternalElement).SelectByValue(testCreditCard.ExpirationMonth.ToString());
            new SelectElement(TextExpYearField.InternalElement).SelectByValue(testCreditCard.ExpirationYear.ToString());
        }

        public virtual void SetPaymentAddress(IAddress address)
        {
            PhoneNumberField.Clear();
            PhoneNumberField.SendKeys(address.Phone);
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
            var state = address.State;
            Browser.ScrollIntoView(DdlStateProvinceField);
            new SelectElement(DdlStateProvinceField.InternalElement).SelectByValue(state);
            ZipCodeField.Clear();
            ZipCodeField.SendKeys(address.ZipCode);
        }

        public virtual string GetEmailPreferenceHeaderText()
        {
            return Browser.Locate.ElementByClassName(_accountTitleClass).Text;
        }

        public virtual void OpenShippingAddressForm()
        { 
            Browser.Wait.IsVisibleElement(By.XPath(_manageShippingAddressContentXpath));
            Browser.ClickByJs(BtnAddShippingAddress);
            Browser.Wait.IsVisibleElement(By.CssSelector(Modal.LpModalId.ToCssIdSelector()));
        }

        public virtual void SaveShippingAddress()
        {
            BtnSaveShippingAddress.Click();

            // Wait for the new address to be added to the document
            Browser.Wait.IsInvisibleElement(By.CssSelector(Modal.LpModalId.ToCssIdSelector()));
        }

        public virtual void AddNewShippingAddressToModal(IAddress shippingAddress)
        {
            ShippingFirstNameField.SendKeys(shippingAddress.FirstName);
            ShippingLastNameField.SendKeys(shippingAddress.LastName);
            ShippingAddressLineOneField.SendKeys(shippingAddress.AddressLine1);
            ShippingAddressLineTwoField.SendKeys(shippingAddress.AddressLine2);
            ShippingCityField.SendKeys(shippingAddress.City);

            if (!string.IsNullOrWhiteSpace(shippingAddress.Country))
            {
                ShowCountryLink.Click();
                new SelectElement(ShippingCountryOption.InternalElement).SelectByValue(shippingAddress.Country);
            }

            Browser.Wait.ForClickableElement(ShippingStateField, 5);

            if (shippingAddress.State == "N/A")
            {
                ShippingStateField.Click();
                Browser.Wait.ForElementToStopAnimating(ShippingStateField);
                new SelectElement(ShippingStateField.InternalElement).SelectByText("N/A");
            }
            else
                new SelectElement(ShippingStateField.InternalElement).SelectByValue(shippingAddress.State);

            ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
            ShippingPhoneField.SendKeys(shippingAddress.Phone);
        }

        public string GetShippingAddressFullName()
        {
            return ShippingAddressFullnamePage.Text;
        }

        public string GetShippingAddressStreetName()
        {
            return ShippingAddressLineOne.Text;
        }

        public string GetShippingAddressCityStateZipName()
        {
            return ShippingCityStateZip.Text.Trim();
        }

        public string GetShippingAddressPhoneNumber()
        {
            return ShippingPhoneNumber.Text;
        }

        public void AddShippingAddress(IAddress address)
        {
            Browser.Wait.ForClickableElement(AddPaymentOptionButton, 60).Click();
            Modal.IsModalVisible();

            if(PhoneNumberField.IsInitialized)
            {
                PhoneNumberField.Clear();
                PhoneNumberField.SendKeys(address.Phone);
                ZipCodeField.Click();
                ZipCodeField.Clear();
                ZipCodeField.SendKeys(address.ZipCode);
            }
            else
            {
                TextPhoneField.Clear();
                TextPhoneField.SendKeys(address.Phone);
                ShippingZipCodeField.Click();
                ShippingZipCodeField.Clear();
                ShippingZipCodeField.SendKeys(address.ZipCode);
            }

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
            new SelectElement(DdlStateProvinceField.InternalElement).SelectByValue(address.State);
            SaveShippingAddressCheckBox.Click();
            Browser.Wait.UntilElementUnloads(Modal.GetIframeModal());
            Browser.Wait.ForDomReady();
        }

        public virtual void SetNewPassword(string newPassword)
        {
            TextNewPasswordField.SendKeys(newPassword);
            TextConfirmPasswordField.SendKeys(newPassword);
            SaveNewPasswordBtn.Click();
        }

        public virtual void SetOriginalPassword(string originalPassword)
        {
            TextNewPasswordField.Clear();
            TextConfirmPasswordField.Clear();
            TextNewPasswordField.SendKeys(originalPassword);
            TextConfirmPasswordField.SendKeys(originalPassword);
            SaveNewPasswordBtn.Click();

            CloseChangePasswordThankYouModal();
        }

        public virtual void NavigateToChangePasswordLink()
        {
            Browser.Wait.IsVisibleElement(By.Id(_modalChangePasswordId));
            Browser.Wait.ForClickableElement(ChangePasswordLink).Click();
        }

        public bool IsPaymentOptionDeleted()
        {
            return Browser.Wait.UntilElementDoesntExist(OptionClass);
        }

        public void OpenYourInformationModal()
        {
            AccountProfileEditInfo.Click();
            Browser.Wait.ForDomReady();
        }

        public void EditAccountPhoneNumber()
        {
            var previousPhoneNumber = AccountProfilePhoneNumberField.Text;
            var formattedPreviousPhoneNumber = TextActions.RegexNoTabsAndNewLines(previousPhoneNumber);
            var phoneNumber = (Convert.ToInt64(formattedPreviousPhoneNumber) + 1).ToString();

            TextPhoneField.Clear();
            TextPhoneField.SendKeys(phoneNumber);
            SaveInfoBtn.Click();
            Browser.Wait.ForCondition(() => AccountProfilePhoneNumberField.Text != previousPhoneNumber);
            Assert.Equals(phoneNumber, AccountProfilePhoneNumberField.Text, $"Phone number does not match. /  {AccountProfilePhoneNumberField.Text}");
        }

        public void ResetAccountPhoneNumber()
        {
            AccountActions.ResetNamePhone(RewardNumber);
        }

        public virtual void OpenEditPaymentModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_editPaymentOptionButtonXpath));
            Browser.ClickOnButtonMultipleTimes(EditPaymentOption, 5, IsPaymentFormVisible);
        }

        public virtual void OpenEditShippingAddressModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_editShippingAddressOptionButtonXpath));
            Browser.ClickOnButtonMultipleTimes(EditShippingAddressOption, 5, IsShippingAddressFormVisible);
        }

        public virtual void ClosePaymentModal()
        {
            SavePaymentBtn.Click();
            Browser.Wait.UntilElementUnloads(Modal.GetLpModal());
        }

        public bool IsPaymentFormVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(JsSaveClass.ToCssClassSelector()), timeToWait);
        }

        public bool IsShippingAddressFormVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_btnSaveShippingAddressId.ToCssIdSelector()));
        }

        public void ChangeShippingPhoneNumber()
        {
            ShippingPhoneField.Clear();
            ShippingPhoneField.SendKeys(_modifiedShippingPhoneNumber);
            SaveShippingAddress();
        }

        public virtual void OpenEmailPreferencesModal()
        {
            ChangeEmailPreferencesLink.Click();
            Browser.Wait.ForDomReady();
        }

        public virtual void SaveEmailPreferences()
        {
            SaveEmailPreferencesButton.Click();
            Browser.Wait.IsVisibleElement(By.XPath(_emailPrefThankYouMessageXpath));
        }

        public void DeleteOneSavedPaymentOption()
        {
            var removeLink = FirstDeletedSavedPaymentLink;
            Browser.Wait.ForClickableElement(removeLink).Click();
            Browser.Wait.UntilElementUnloads(removeLink);
        }

        public int IsOnlyDefaultPaymentOptionAvailable()
        {
            return PaymentContainer.Count;
        }

        public string GetPaymentPhoneNumber()
        {
            Browser.Wait.ForClickableElement(Browser.Locate.ElementByClassName(_optionPhoneClass, FirstCreditCardInfo));
            return Browser.Locate.ElementByClassName(_optionPhoneClass, FirstCreditCardInfo).Text;
        }

        public string GetCreditCardNumber()
        {
            return Browser.Locate.ElementByClassName(_optionCardTypeClass, FirstCreditCardInfo).Text;
        }

        public string GetNameOnCreditCard()
        {
            return Browser.Locate.ElementByClassName(_optionNameClass, FirstCreditCardInfo).Text;
        }

        public string GetCreditCardExpirationDate()
        {
            return Browser.Locate.ElementByClassName(_optionExpirationClass, FirstCreditCardInfo).Text;
        }

        public string GetPaymentName()
        {
            return Browser.Locate.ElementByClassName(_optionBillingNameClass, FirstCreditCardInfo).Text;
        }

        public string GetPaymentAddressField1()
        {
            return Browser.Locate.ElementByClassName(_optionAddress1Class, FirstCreditCardInfo).Text;
        }

        public string GetPaymentAddressField2()
        {
            return Browser.Locate.ElementByClassName(_optionAddress2Class, FirstCreditCardInfo).Text;
        }

        public string GetPaymentCity()
        {
            return Browser.Locate.ElementByClassName(_optionAddress3Class, FirstCreditCardInfo).Text;
        }

        public void ClearAccountShippingFormFields()
        {
            FirstNameField.Clear();
            LastNameField.Clear();
            ShippingAddressLineOneField.Clear();
            ShippingAddressLineTwoField.Clear();
            ShippingCityField.Clear();
            ShippingZipCodeField.Clear();
            ShippingPhoneField.Clear();
        }

        public virtual IAddress GetFirstSavedShippingAddress()
        {
            var defaultShippingAddressContainer = Browser.Locate.ParentElement(DefaultShippingAddressItemHeader);
            var shippingAddress = new Address.Address
            {
                AddressLine1 = Browser.Locate.ElementByClassName(_optionAddress1Class, defaultShippingAddressContainer).Text,
                AddressLine2 = Browser.Locate.ElementByClassName(_optionAddress2Class, defaultShippingAddressContainer).Text,
                City = Browser.Locate.ElementByClassName(_optionAddress3Class, defaultShippingAddressContainer).Text, // City, State ZIP Country are all in the same container
                FirstName = Browser.Locate.ElementByClassName(_optionNameClass, defaultShippingAddressContainer).Text, // FirstName LastName
                Phone = Browser.Locate.ElementByClassName(_optionPhoneClass, defaultShippingAddressContainer).Text
            };

            return shippingAddress;
        }

        public void SelectNewOptionAndSave()
        {
            Browser.Wait.ForClickableElement(SubscribeRadioButton);

            SubscribeRadioButton.Click();
            SaveEmailPreferencesBtn.Click();
        }

        public virtual bool IsModalThankYouMessageVisible()
        {
            return Browser.Wait.ForDisplayedElement(ModalMessage).Text.Contains("Thank you");
        }

        public void Unsubscribe()
        {
            UnsubscribeRadioButton.Click();
        }

        public virtual void EditAccountContactNumbers(string phoneNumber, string faxNumber, string cellPhoneNumber)
        {
            AccountProfileEditInfo.Click();
            Modal.IsModalVisible();
            Browser.Wait.ForDisplayedElement(TextPhoneField).Clear();

            TextPhoneField.SendKeys(phoneNumber);
            TextFaxField.Clear();
            TextFaxField.SendKeys(faxNumber);
            TextCellField.Clear();
            TextCellField.SendKeys(cellPhoneNumber);
            SaveInfoBtn.Click();
        }

        public string GetProfilePhoneNumber()
        {
            return RemoveFormatting(AccountProfilePhoneNumberField.Text);
        }

        public void UpdateAccountProfile(string firstName, string lastName, string phoneNumber, string previousPhoneNumber = null)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
            LastNameField.Clear();
            LastNameField.SendKeys(lastName);
            TextPhoneField.Clear();
            TextPhoneField.SendKeys(phoneNumber);
            SaveInfoBtn.Click();
            if (previousPhoneNumber != null)
            {
                Browser.Wait.ForCondition(() => AccountProfilePhoneNumberField.Text != previousPhoneNumber);
            }
            else
            {
                Browser.Wait.ForDomReady();
            }
        }

        public string GetAccountProfileFullName()
        {
            return AccountProfileFullName.Text.Trim().ToLower();
        }

        public void CloseChangePasswordThankYouModal()
        {
            Browser.Wait.ForBoolCondition(Browser.Locate.ElementById(_changePasswordContentId).GetAttribute("class").Contains("hidden"));
            Modal.GetLpModalClose().Click();
        }
    }
}
