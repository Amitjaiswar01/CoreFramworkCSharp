using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ManageAccountBase : Page, IManageAccount
    {
        /// <inheritdoc />
        protected ManageAccountBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        #endregion

        #region CSS Selector Strings
        private string Address2Class { get; } = "address2";
        private string AddressClass { get; } = "address1";
        private string CityClass { get; } = "city";
        private string DdlStateProvinceName { get; } = "State";
        private string IntPhoneClass { get; } = "intPhone";
        private string LastNameClass { get; } = "lastName";
        private string NameOnCardName { get; } = "cardFullName";
        private string PaymentOptionExpirationMonthName { get; } = "cardMonth";
        private string PaymentOptionExpirationYearName { get; } = "cardYear";
        private string TxtAddress1Id { get; } = "txtAddress1";
        private string TxtCardNumberName { get; } = "cardNumber";
        private string TxtCityId { get; } = "txtCity";
        private string ZipCodeClass { get; } = "zipCode";
        public string AddPaymentOptionClass { get; } = "jsAdd";
        public string ChangePasswordLinkId { get; } = "modal-changepassword";
        public string DdlCountryId { get; } = "ddlCountry";
        public string EmailPreferencesLinkId { get; } = "modal-emailpreferences";
        public string FirstNameClass { get; } = "firstName";
        public string SavePaymentOptionButtonClass { get; } = "jsSave";
        public string MyPastOrderSectionClass { get; } = "ocSearch";
        public string OptionWrapperClass { get; } = "option__wrapper";
        public string PaymentOptionClass { get; } = "option";
        public string TxtAddress2Id { get; } = "txtAddress2";
        public string TxtZipCodeId { get; } = "txtZipCode";
        public string BtnSaveShippingAddressId { get; } = "btnSaveShippingAddress";
        public string DefaultAddressXpath { get; } = "//*[@id=\"divAddress1\"][contains(text(), '20250 Plummer St')]";
        public string UpdatedAddressXpath { get; } = "//*[@class=\"savedAddress\"][contains(text(), \"Plummer\")]";
        public string MyOrdersString => "My Orders";
        public string PaymentOptionsString => "/account/profile/paymentoptions/";
        public abstract string AccountTitleClass { get; }
        public abstract string BtnAddShippingAddressId { get; }
        public abstract string BtnAddShippingAddressXpath { get; }
        public abstract string CardFormDrawerId { get; }
        public abstract string CountryClass { get; }
        public abstract string DdlStateProvinceId { get; }
        public abstract string EditChangePasswordXpath { get; }
        public abstract string EditEmailPrefXpath { get; }
        public abstract string ForEmailClass { get; }
        public abstract string JsShowCountryClass { get; }
        public abstract string LpSelectMobileDrawerCardYearSelectId { get; }
        public abstract string LpSelectMobileDrawerStateSelectId { get; }
        public abstract string ManageAccountBackClass { get; }
        public abstract string ManageShippingAddressContentXpath { get; }
        public abstract string MonthDropdownXpath { get; }
        public abstract string ModalEditShippingId { get; }
        public abstract string RewardsNumberClass { get; }
        public abstract string PaymentOverlayXpath { get; }
        public abstract string SelectStateSelector { get; }
        public abstract string ShippingAddressesLinkXpath { get; }
        public abstract string ShowCountryDropdownLinkId { get; }
        public abstract string StateDropdownXpath { get; }
        public abstract string TxtFirstNameId { get; }
        public abstract string TxtLastNameId { get; }
        public abstract string TxtPhoneId { get; }
        public abstract string TxtShippingZipCodeId { get; }
        public abstract string YearDropdownXpath { get; }
        public abstract string SavedAddressXpath { get; }
        public abstract string ShippingAddressLinkClass { get; }
        #endregion

        #region Page Elements
        public IElement ShippingAddressLink => Browser.Locate.ElementByClassName(ShippingAddressLinkClass);
        public IElement AddPaymentOptionButton => Browser.Locate.ElementByClassName(AddPaymentOptionClass);
        public IElement DdlCountryField => Browser.Locate.ElementById(DdlCountryId);
        public IElement ManagePaymentOptionsLinkForElement => Browser.Locate.ElementByAttribute(AttributeSelectorType.Contains, HtmlTextWriterAttribute.Href, PaymentOptionsString);
        public IElement PhoneNumberField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, IntPhoneClass);
        public IElement SavePaymentBtn => Browser.Locate.ElementByClassName(SavePaymentOptionButtonClass);
        public IElement SavedPaymentOptions => Browser.Locate.ElementByClassName(PaymentOptionClass);
        public IElement ShippingAddressLineOneField => Browser.Locate.ElementById(TxtAddress1Id);
        public IElement ShippingAddressLineTwoField => Browser.Locate.ElementBySelector(TxtAddress2Id.ToCssIdSelector());
        public IElement DdlStateProvinceField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, DdlStateProvinceName);
        public IElement ShippingCityField => Browser.Locate.ElementById(TxtCityId);
        public IElement Address2Field => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, Address2Class);
        public IElement AddressField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, AddressClass);
        public IElement TextCardNumberField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, TxtCardNumberName);
        public IElement CityField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, CityClass);
        public IElement TextExpMonthField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, PaymentOptionExpirationMonthName);
        public IElement TextExpYearField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, PaymentOptionExpirationYearName);
        public IElement FirstNameField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, FirstNameClass);
        public IElement LastNameField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, LastNameClass);
        public IElement TextNameOnCardField => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, NameOnCardName);
        public IElement ZipCodeField => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Input, ZipCodeClass);
        public abstract IElement BtnAddShippingAddress { get; }
        public abstract IElement BtnSaveShippingAddress { get; }
        public abstract IElement ChangeEmailPreferencesLink { get; }
        public abstract IElement ChangePasswordLink { get; }
        public abstract IElement CreditCardYearSelectDrawer { get; }
        public abstract IElement ManageShippingAddressesLinkForElement { get; }
        public abstract IElement ManageAccountBackButton { get; }
        public abstract IElement ModalWindow { get; }
        public abstract IElement MyOrdersLink { get; }
        public abstract IElement MyPastOrderSection { get; }
        public abstract IElement OrderHistoryBreadcrumb { get; }
        public abstract IElement OrderId { get; }
        public abstract IElement PaymentOptionDrawer { get; }
        public abstract IElement RadioButton { get; }
        public abstract IElement RewardsNumberElement { get; }
        public abstract IElement ShippingCountryOption { get; }
        public abstract IElement ShippingFirstNameField { get; }
        public abstract IElement ShippingLastNameField { get; }
        public abstract IElement ShippingPhoneField { get; }
        public abstract IElement ShippingStateField { get; }
        public abstract IElement ShippingZipCodeField { get; }
        public abstract IElement ShowCountryLink { get; }
        public abstract IElement StateSelectDrawer { get; }
        public abstract IElement EmailPreferencesHeader { get; }
        #endregion

        public abstract void SelectShippingDropDownByValue(IElement dropDownElement, string valueAttribute);

        /// <inheritdoc />
        public void OpenAddShippingAddressModal()
        {
            Browser.Wait.ForClickableElement(BtnAddShippingAddress);
            Browser.ClickOnButtonMultipleTimes(BtnAddShippingAddress, 10, IsManageAccountShippingFormVisible);
        }

        public bool IsManageAccountShippingFormVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(BtnSaveShippingAddressId.ToCssIdSelector()));
        }

        public string RewardNumber => RewardsNumberElement.Text.Replace("Customer #:", string.Empty).Trim();

        /// <inheritdoc />
        public virtual void SetPaymentCard(CreditCard testCreditCard)
        {
            TextCardNumberField.Clear();
            TextCardNumberField.SendKeys(testCreditCard.CardNumber);
            TextNameOnCardField.Clear();
            TextNameOnCardField.SendKeys(testCreditCard.NameOnCard);

            new SelectElement(TextExpMonthField.InternalElement).SelectByValue(testCreditCard.ExpirationMonth.ToString());
            new SelectElement(TextExpYearField.InternalElement).SelectByValue(testCreditCard.ExpirationYear.ToString());
        }

        /// <inheritdoc />
        public virtual void SetPaymentAddress(Address address)
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
            new SelectElement(DdlStateProvinceField.InternalElement).SelectByValue(address.State);
            ZipCodeField.Clear();
            ZipCodeField.SendKeys(address.ZipCode);
        }
    }
}