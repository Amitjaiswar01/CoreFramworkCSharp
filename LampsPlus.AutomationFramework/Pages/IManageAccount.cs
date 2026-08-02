using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// https://www.lampsplus.com/account/profile/
    /// </summary>
    public interface IManageAccount
    {
        #region Class Setup
        string ChangePasswordLinkId { get; }
        string EditChangePasswordXpath { get; }
        string EditEmailPrefXpath { get; }
        string MyOrdersString { get; }
        string FirstNameClass { get; }
        string SavePaymentOptionButtonClass { get; }
        string TxtAddress2Id { get; }
        string PaymentOverlayXpath { get; }
        string PaymentOptionClass { get; }
        string TxtFirstNameId { get; }
        string TxtShippingZipCodeId { get; }
        string SelectStateSelector { get; }
        string BtnSaveShippingAddressId { get; }
        string BtnAddShippingAddressXpath { get; }
        string BtnAddShippingAddressId { get; }
        string AddPaymentOptionClass { get; }
        string ManageShippingAddressContentXpath { get; }
        string ShippingAddressesLinkXpath { get; }
        string EmailPreferencesLinkId { get; }
        string ShippingAddressLinkClass { get; }
        string DefaultAddressXpath { get; }
        string OptionWrapperClass { get; }
        string UpdatedAddressXpath { get; }
        #endregion

        #region Page Elements
        IElement ShippingAddressLink { get; }
        IElement AddPaymentOptionButton { get; }
        IElement Address2Field { get; }
        IElement AddressField { get; }
        IElement BtnAddShippingAddress { get; }
        IElement BtnSaveShippingAddress { get; }
        IElement ChangeEmailPreferencesLink { get; }
        IElement ChangePasswordLink { get; }
        IElement CreditCardYearSelectDrawer { get; }
        IElement CityField { get; }
        IElement DdlCountryField { get; }
        IElement EmailPreferencesHeader { get; }
        IElement DdlStateProvinceField { get; }
        IElement FirstNameField { get; }
        IElement ManagePaymentOptionsLinkForElement { get; }
        IElement ManageShippingAddressesLinkForElement { get; }
        IElement ManageAccountBackButton { get; }
        IElement ModalWindow { get; }
        IElement MyOrdersLink { get; }
        IElement MyPastOrderSection { get; }
        IElement OrderHistoryBreadcrumb { get; }
        IElement OrderId { get; }
        IElement PaymentOptionDrawer { get; }
        IElement PhoneNumberField { get; }
        IElement RadioButton { get; }
        IElement RewardsNumberElement { get; }
        IElement SavedPaymentOptions { get; }
        IElement SavePaymentBtn { get; }
        IElement ShippingAddressLineOneField { get; }
        IElement ShippingAddressLineTwoField { get; }
        IElement ShippingCityField { get; }
        IElement ShippingCountryOption { get; }
        IElement ShippingFirstNameField { get; }
        IElement ShippingLastNameField { get; }
        IElement ShippingPhoneField { get; }
        IElement ShippingStateField { get; }
        IElement ShippingZipCodeField { get; }
        IElement ShowCountryLink { get; }
        IElement StateSelectDrawer { get; }
        IElement TextCardNumberField { get; }
        IElement TextExpMonthField { get; }
        IElement TextExpYearField { get; }
        IElement LastNameField { get; }
        IElement TextNameOnCardField { get; }
        IElement ZipCodeField { get; }
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        bool IsManageAccountShippingFormVisible(int timeToWait);

        /// <summary>
        /// User Profile rewardNumber
        /// </summary>
        string RewardNumber { get; }
        string SavedAddressXpath { get; }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Open the Add Shipping Address modal.
        /// </summary>
        void OpenAddShippingAddressModal();


        ///<summary>
        /// Set the payment credit card information.
        /// </summary>
        /// <param name="testCreditCard"></param>
        void SetPaymentCard(CreditCard testCreditCard);

        /// <summary>
        /// Set the payment billing address information.
        /// </summary>
        /// <param name="address"></param>
        void SetPaymentAddress(Address address);

        /// <summary>
        /// Select dropdown value
        /// </summary>
        /// <param name="elementDropdown"></param>
        /// <param name="valueAttribute"></param>
        void SelectShippingDropDownByValue(IElement elementDropdown, string valueAttribute);
    }
}
