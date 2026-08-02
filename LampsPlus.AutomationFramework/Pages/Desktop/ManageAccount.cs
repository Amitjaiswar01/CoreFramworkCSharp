using System;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/account/profile/
    /// </summary>
    public class ManageAccount : ManageAccountBase
    {
        /// <inheritdoc />
        public ManageAccount(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        public override string AccountTitleClass { get; } = "accountTitle";
        public override string CountryClass { get; } = "country";
        public override string DdlStateProvinceId { get; } = "ddlStateProvince";
        public override string JsShowCountryClass { get; } = "jsShowCountry";
        public override string ManageShippingAddressContentXpath { get; } = "//*[contains(@class,'jsAdd')]";
        public override string ModalEditShippingId { get; } = "modalEditShipping";
        public override string RewardsNumberClass { get; } = "section__header--right";
        public override string ShippingAddressesLinkXpath { get; } = "//a[@href='/account/profile/shipping-addresses/' and text()='Shipping Addresses']";
        public override string TxtFirstNameId { get; } = "txtFirstName";
        public override string TxtLastNameId { get; } = "txtLastName";
        public override string TxtPhoneId { get; } = "txtPhone";
        public override string SavedAddressXpath { get; } = "//*[@class='options']/div";
        public override string ShippingAddressLinkClass { get; } = "shippingAddressLink";
        public override string BtnAddShippingAddressId => throw new NotImplementedException();
        public override string BtnAddShippingAddressXpath => throw new NotImplementedException();
        public override string CardFormDrawerId => throw new NotImplementedException();
        public override string EditChangePasswordXpath => throw new NotImplementedException();
        public override string EditEmailPrefXpath => throw new NotImplementedException();
        public override string ForEmailClass => throw new NotImplementedException();
        public override string LpSelectMobileDrawerCardYearSelectId => throw new NotImplementedException();
        public override string LpSelectMobileDrawerStateSelectId => throw new NotImplementedException();
        public override string ManageAccountBackClass => throw new NotImplementedException();
        public override string MonthDropdownXpath => throw new NotImplementedException();
        public override string PaymentOverlayXpath => throw new NotImplementedException();
        public override string SelectStateSelector => throw new NotImplementedException();
        public override string ShowCountryDropdownLinkId => throw new NotImplementedException();
        public override string StateDropdownXpath => throw new NotImplementedException();
        public override string TxtShippingZipCodeId => throw new NotImplementedException();
        public override string YearDropdownXpath => throw new NotImplementedException();

        #region Page Elements
        public override IElement BtnAddShippingAddress => Browser.Locate.ElementByXpath(ManageShippingAddressContentXpath);
        public override IElement BtnSaveShippingAddress => Browser.Locate.ElementBySelector($"#{ModalEditShippingId} .{GlobalLocators.CalloutBtnClass}");
        public override IElement ChangeEmailPreferencesLink => Browser.Locate.ElementById(EmailPreferencesLinkId);
        public override IElement ChangePasswordLink => Browser.Locate.ElementById(ChangePasswordLinkId);
        public override IElement ManageShippingAddressesLinkForElement => Browser.Locate.ElementByXpath(ShippingAddressesLinkXpath);
        public override IElement ModalWindow => GlobalLocators.Iframe;
        public override IElement MyOrdersLink => Browser.Locate.ElementByLinkText(MyOrdersString);
        public override IElement MyPastOrderSection => Browser.Locate.ElementByXpath("//div[@class='ocSearch']");
        public override IElement OrderHistoryBreadcrumb => Browser.Locate.ElementByXpath("//a[text()='Order History']");
        public override IElement OrderId => Browser.Locate.ElementByXpath("//*[@id='main_rptOrders_trOrder_1']/td[1]");
        public override IElement RadioButton => Browser.Locate.ElementByXpath("//*[@id='RbAllEmployees']");
        public override IElement RewardsNumberElement => Browser.Locate.ElementByClassName(RewardsNumberClass);
        public override IElement ShippingCountryOption => Browser.Locate.ElementByClassName(CountryClass);
        public override IElement ShippingFirstNameField => Browser.Locate.ElementById(TxtFirstNameId);
        public override IElement ShippingLastNameField => Browser.Locate.ElementById(TxtLastNameId);
        public override IElement ShippingPhoneField => Browser.Locate.ElementById(TxtPhoneId);
        public override IElement ShippingStateField => Browser.Locate.ElementById(DdlStateProvinceId);
        public override IElement ShippingZipCodeField => Browser.Locate.ElementById(TxtZipCodeId);
        public override IElement ShowCountryLink => Browser.Locate.ElementByClassName(JsShowCountryClass);
        public override IElement EmailPreferencesHeader => Browser.Locate.ElementByClassName(AccountTitleClass);
        public override IElement CreditCardYearSelectDrawer => throw new NotImplementedException();
        public override IElement ManageAccountBackButton => throw new NotImplementedException();
        public override IElement PaymentOptionDrawer => throw new NotImplementedException();
        public override IElement StateSelectDrawer => throw new NotImplementedException();
        #endregion

        public override void SelectShippingDropDownByValue(IElement dropDownElement, string valueAttribute)
        {
            throw new NotImplementedException();
        }
    }
}
