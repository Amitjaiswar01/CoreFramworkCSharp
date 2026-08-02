using OpenQA.Selenium;
using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/account/profile/
    /// </summary>
    public class MobileManageAccount : ManageAccountBase
    {
        /// <inheritdoc />
        public MobileManageAccount(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        public override string SavedAddressXpath { get; } = "//*[@id='divShippingAddressContainer']/div";
        public override string BtnAddShippingAddressId { get; } = "addShippingAddressBtn";
        public override string BtnAddShippingAddressXpath { get; } = "//*[@id='addShippingAddressBtn']";
        public override string CardFormDrawerId { get; } = "cardFormDrawer";
        public override string EditChangePasswordXpath { get; } = "//*[@id='editChangePassword']";
        public override string EditEmailPrefXpath { get; } = "//*[@id=\"editEmailPref\"]";
        public override string ForEmailClass { get; } = "forEmail";
        public override string LpSelectMobileDrawerCardYearSelectId { get; } = "lpSelectMobileDrawer__cardYearSelect";
        public override string LpSelectMobileDrawerStateSelectId { get; } = "lpSelectMobileDrawer__stateSelect";
        public override string ManageAccountBackClass { get; } = "manage-account-link";
        public override string MonthDropdownXpath { get; } = "//*[contains(@for,'cardMonthSelect')]//following-sibling::div[1]/button";
        public override string PaymentOverlayXpath { get; } = "//*[@id='cardFormDrawer']//div[contains(@class, 'lpScrollContainer')]";
        public override string RewardsNumberClass { get; } = "manage-account__header__subtext";
        public override string ShippingAddressesLinkXpath => "//a[@href='/account/profile/shipping-addresses/' and text()='Manage']";
        public override string ShowCountryDropdownLinkId { get; } = "showCountryDropdownLink";
        public override string StateDropdownXpath { get; } = "//*[contains(@for,'stateSelect')]//following-sibling::div[1]/button";
        public override string SelectStateSelector { get; } = "//button[text()='Select State/Province']";
        public override string TxtFirstNameId { get; } = "txtShipToFirstName";
        public override string TxtLastNameId { get; } = "txtShipToLastName";
        public override string TxtPhoneId { get; } = "txtShippingPhone";
        public override string TxtShippingZipCodeId { get; } = "txtZip";
        public override string YearDropdownXpath { get; } = "//*[contains(@class,'fieldNoLabel')]/div[1]/button";
        public override string ShippingAddressLinkClass { get; } = "shippingAddressLink";
        public override string AccountTitleClass => throw new NotImplementedException();
        public override string CountryClass => throw new NotImplementedException();
        public override string DdlStateProvinceId => throw new NotImplementedException();
        public override string JsShowCountryClass => throw new NotImplementedException();
        public override string ManageShippingAddressContentXpath => throw new NotImplementedException();
        public override string ModalEditShippingId => throw new NotImplementedException();

        #region Page Elements
        private IElement DropdownMonth => Browser.Locate.ElementByXpath(MonthDropdownXpath);
        private IElement DropdownYear => Browser.Locate.ElementByXpath(YearDropdownXpath);
        private IElement DropdownState => Browser.Locate.ElementByXpath(StateDropdownXpath);
        public override IElement BtnAddShippingAddress => Browser.Locate.ElementBySelector(BtnAddShippingAddressId.ToCssIdSelector());
        public override IElement BtnSaveShippingAddress => Browser.Locate.ElementById(BtnSaveShippingAddressId);
        public override IElement ChangeEmailPreferencesLink => Browser.Locate.ElementByXpath(EditEmailPrefXpath);
        public override IElement ChangePasswordLink => Browser.Locate.ElementByXpath(EditChangePasswordXpath);
        public override IElement CreditCardYearSelectDrawer => Browser.Locate.ElementByAttributeStartsWith(HtmlTextWriterAttribute.Id, LpSelectMobileDrawerCardYearSelectId);
        public override IElement ManageShippingAddressesLinkForElement => Browser.Locate.ElementByXpath(ShippingAddressesLinkXpath);
        public override IElement ManageAccountBackButton => Browser.Locate.ElementByClassName(ManageAccountBackClass);
        public override IElement ModalWindow => GlobalLocators.LpMobileDrawerElement;
        public override IElement PaymentOptionDrawer => Browser.Locate.ElementById(CardFormDrawerId);
        public override IElement RewardsNumberElement => Browser.Locate.ElementByClassName(RewardsNumberClass);
        public override IElement ShippingCountryOption => Browser.Locate.ElementById(DdlCountryId);
        public override IElement ShippingFirstNameField => Browser.Locate.ElementById(TxtFirstNameId);
        public override IElement ShippingLastNameField => Browser.Locate.ElementById(TxtLastNameId);
        public override IElement ShippingPhoneField => Browser.Locate.ElementById(TxtPhoneId);
        public override IElement ShippingStateField => Browser.Locate.ElementByXpath(SelectStateSelector);
        public override IElement ShippingZipCodeField => Browser.Locate.ElementById(TxtShippingZipCodeId);
        public override IElement ShowCountryLink => Browser.Locate.ElementById(ShowCountryDropdownLinkId);
        public override IElement StateSelectDrawer => Browser.Locate.ElementByAttributeStartsWith(HtmlTextWriterAttribute.Id, LpSelectMobileDrawerStateSelectId);
        public override IElement EmailPreferencesHeader => Browser.Locate.ElementByClassName(ForEmailClass);
        public override IElement MyOrdersLink => throw new NotImplementedException();
        public override IElement MyPastOrderSection => throw new NotImplementedException();
        public override IElement OrderHistoryBreadcrumb => throw new NotImplementedException();
        public override IElement OrderId => throw new NotImplementedException();
        public override IElement RadioButton => throw new NotImplementedException();
        #endregion


        public override void SetPaymentCard(CreditCard testCreditCard)
        {

            TextCardNumberField.Clear();
            TextCardNumberField.SendKeys(testCreditCard.CardNumber);
            TextNameOnCardField.Clear();
            TextNameOnCardField.SendKeys(testCreditCard.NameOnCard);

            SelectShippingDropDownByValue(DropdownMonth, testCreditCard.ExpirationMonth.ToString());
            SelectShippingDropDownByValue(DropdownYear, testCreditCard.ExpirationYear.ToString());
            Browser.Wait.ForElementToStopAnimating(CreditCardYearSelectDrawer);
        }

        public override void SelectShippingDropDownByValue(IElement dropDownElement, string valueAttribute)
        {
            Browser.ScrollIntoView(dropDownElement);
            dropDownElement.Click();
            Browser.Wait.IsVisibleElement(By.XPath($"//*[@data-value='{valueAttribute}']"));
            var valueElement = Browser.Locate.ElementByXpath($"//*[@data-value='{valueAttribute}']");
            Browser.ScrollIntoView(valueElement);
            valueElement.Click();
        }

        public override void SetPaymentAddress(Address address)
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
            Browser.Wait.WaitForAjaxComplete();
            ZipCodeField.Clear();
            ZipCodeField.SendKeys(address.ZipCode);
            PhoneNumberField.Clear();
            PhoneNumberField.SendKeys(address.Phone);
        }
    }
}
