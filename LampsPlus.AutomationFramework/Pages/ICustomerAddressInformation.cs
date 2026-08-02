using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;

using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface ICustomerAddressInformation
    {
        #region Class Setup
        Address Address { get; set; }
        IntAddress IntAddress { get; set; }
        IOrderSummaryBlock OrderSummaryBlock { get; set; }

       
        string AddAnotherAddressFieldLinkClass { get; }
        string AddAnotherAddressFieldLinkXpath { get; }
        string AddressFieldPairClass { get; }
        string ApartmentArdmoreString { get; }
        string CaliforniaString { get; }
        string CityArdmoreString { get; }
        string FedExShippingAddress1Id { get; }
        string FedExShippingStateXpath { get; }
        string FedExStateSelectorId { get; }
        string FieldCheckboxClass { get; }
        string ShowAddressLine2BtnClass { get; }
        string LpSelectMobileDrawerClass { get; }                     
        string PaymentInfoAddressFieldsetClass { get; }
        string PennsylvaniaString { get; }
        string SingleShippingCountryId { get; }
        string StreetAddressArdmoreString { get; }
        string SubmitChangesClass { get; }
        string SuggestedAddressRadioId { get; }
        string UnitedStatesString { get; }
        #endregion

        #region CSS Selector Strings
        string AddressLabelClass { get; }
        string FedExSuggestedAddressString { get; }
        string FedExSuggestedCityString { get; }
        string FedExSuggestedStateString { get; }
        string FedExSuggestedZipCodeString { get; }
        string NorthCarolinaString { get; }
        #endregion

        #region Page Elements
        IElement AddAnotherAddressFieldLink { get; }

        IElement IntAddAnotherAddressFieldLink { get; }

        /// <summary>
        /// 'Add New Address' button located on the 'Select a Shipping Address' modal for logged in customers. Accessed when the 'Ship to A Different Address' button is clicked.
        /// </summary>
        IElement AddNewAddressButton { get; }

        /// <summary>
        /// Container that contains all the shipping fields.
        /// </summary>
        IElement AddressContainerElement { get; }

        /// <summary>
        /// Address 2 field element.
        /// </summary>
        IElement ApartmentSuiteOtherField { get; }

        IElement FedExApartmentStateSelection { get; }

        /// <summary>
        /// Change shipping option apply button field element on the shipping form.
        /// </summary>
        IElement ChangeShippingApplyButton { get; }

        /// <summary>
        /// City field element on the shipping form.
        /// </summary>
        IElement CityField { get; }

        /// <summary>
        /// Country dropdown element on the shipping form.
        /// </summary>
        IElement CountryField { get; }

        IElement CountrySelection { get; }
        
        /// <summary>
        /// Use Default Address Radio element in Fed Ex module.
        /// </summary>
        IElement DefaultAddressRadioElement { get; }

        /// <summary>
        /// Email field element.
        /// </summary>
        IElement EmailField { get; }

        /// <summary>
        /// Edit link for Maintain Current Address
        /// </summary>
        IElement EditMaintainCurrentAddressLink { get; }
        IElement FedExModalKeepAddressOption { get; }
        IElement MultiAddressShowAnotherAddressFieldLink { get; }
        IElement MultiAddressAddress1Field { get; }
        IElement MultiAddressAddress2Field { get; }
        IElement MultiAddressCityField { get; }
        IElement MultiAddressFirstNameField { get; }
        IElement MultiAddressLastNameField { get; }
        IElement MultiAddressPhoneNumberField { get; }
        IElement MultiAddressStateField { get; }
        IElement MultiAddressZipCodeField { get; }
        IElement SelectShippingAddressModal { get; }
        IElement ShippingInformationModal { get; }
        IElement ShippingAddressOption { get; }

        /// <summary>
        /// H1 header inside FedEx Address validation box.
        /// </summary>
        IElement FedExAddressValidationHeader { get; }

        /// <summary>
        /// FedEx Street Address field.
        /// </summary>
        IElement FedExShippingAddress1 { get; }

        /// <summary>
        /// FedEx Appt/Suite/Other field.
        /// </summary>
        IElement FedExShippingAddress2 { get; }

        /// <summary>
        /// FedEx City field.
        /// </summary>
        IElement FedExShippingCity { get; }

        /// <summary>
        /// FedEx State field.
        /// </summary>
        IElement FedExShippingState { get; }

        /// <summary>
        /// FedEx Zip/Postal Code field.
        /// </summary>
        IElement FedExShippingZipCode { get; }

        /// <summary>
        /// FedEx Address validation box that appears with an address correction suggestion.
        /// </summary>
        IElement FedExAddressValidationModal { get; }

        /// <summary>
        /// First name field element.
        /// </summary>
        IElement FirstNameField { get; }

        /// <summary>
        /// Google Auto-Complete modal that appears when the user starts entering in an address.
        /// </summary>
        IElement GoogleAutocompleteElement { get; }

        /// <summary>
        /// Last name field element.
        /// </summary>
        IElement LastNameField { get; }

        /// <summary>
        /// Phone field element.
        /// </summary>
        IElement PhoneField { get; }

        /// <summary>
        /// Maintain entered address on Fed Ex Validation
        /// </summary>
        IElement NoChangeAddressRadioElement { get; }

        /// <summary>
        /// Proceed to payment button element.
        /// </summary>
        IElement ProceedToPaymentButton { get; }

        /// <summary>
        /// Checkbox to save a newly added address through the shipping address modal for logged in customers.
        /// </summary>
        IElement SaveAddressCheckbox { get; }

        /// <summary>
        /// Checkbox input to get checked/selected state
        /// </summary>
        IElement SaveAddressCheckboxInput { get; }

        /// <summary>
        /// Save button located on the shipping address modal for logged in customers.
        /// </summary>
        IElement SaveAddressFromModalButton { get; }

        /// <summary>
        /// Full Name portion of the Saved Shipping Address for logged in customers.
        /// </summary>
        IElement SavedAddressFullName { get; }

        /// <summary>
        /// Address portion of the Saved Shipping Address for logged in customers.
        /// </summary>
        IElement SavedAddressShippingInfo { get; }

        /// <summary>
        /// Default address used for logged in customers provided they have a saved address.
        /// </summary>
        IElement ShippingAddressInfoContainer { get; }

        /// <summary>
        /// Verbiage displayed when a shipping charge has changed on the Shipping page.
        /// </summary>
        IElement ShippingOptionsChangedMessage { get; }

        /// <summary>
        /// 'Ship To A Different Address' button located on the shipping page for a customer that already has a saved address.
        /// </summary>
        IElement ShipToDifferentAddressButton { get; }

        /// <summary>
        /// Link that shows the 'Country' dropdown when clicked.
        /// </summary>
        IElement ShowCountryLink { get; }

        /// <summary>
        /// Link that shows the 'State' dropdown when clicked.
        /// </summary>
        IElement ShowStateLink { get; }

        /// <summary>
        /// State field element.
        /// </summary>
        IElement StateField { get; }
        IElement StateSelection { get; }

        /// <summary>
        /// Address 1 field element.
        /// </summary>
        IElement StreetAddressField { get; }

        /// <summary>
        /// Suggested Address element in Fed Ex module.
        /// </summary>
        IElement SuggestedAddressElement { get; }

        /// <summary>
        /// Use Simiar Verified Address Radio element in Fed Ex module.
        /// </summary>
        IElement SuggestedAddressRadioElement { get; }

        /// <summary>
        /// Zip code field element.
        /// </summary>
        IElement ZipPostalCodeField { get; }

        /// <summary>
        /// Error message shown whenever there is a field validation error.
        /// </summary>
        IElement GetValidationErrorElement(IElement formControl);

        IElement SubmitChangesElement { get; }

        /// <summary>
        /// Common method to get the Save address checkbox on desktop and mobile, since the markup differs on both.
        /// </summary>
        /// <param name="getMobileInput">Get the actual checkbox input element when on mobile (to check "selected" value).</param>
        IElement GetCommonSaveAddressCheckbox(bool getMobileInput);
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        bool FormControlValidationErrorMessageDisplayed(IElement formControl);

        /// <summary>
        /// List of form field elements and their default values.
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<string, IElement>> RequiredFormControls();

        string GetValidationErrorMessage(IElement formControl);

        /// <summary>
        /// Find shipping cost and remove currency sign.
        /// </summary>
        string GetOrderSummaryShippingCost(bool removeCurrencySign = true);

        /// <summary>
        /// Clear and enter text value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        void ClearAndEnterText(IElement element, string text);

        /// <summary>
        /// Method to clear form fields.
        /// </summary>
        void ClearFormControl(IElement formControl);

        /// <summary>
        /// Enter Billing Address information on Payment page for international orders.
        /// </summary>
        /// <param name="address"></param>
        void EnterBillingAddressForIntlOrders(Address address);

        /// <summary>
        /// Enter Billing Address information on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        void EnterIntBillingAddress(Address address);

        /// <summary>
        /// Enter in a different country on the Payment page.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="address"></param>
        void EnterDifferentCountryValueOnPaymentPage(IElement element, Address address);

        /// <summary>
        /// Enter Shipping Address information on Shipping page.
        /// The ShippingAddress object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        void EnterShippingAddress(Address address, UserRole userrole = default, bool isIntAddress = false, bool isMultiAddress = false);

        /// <summary>
        /// Enter Billing Address information for wire transfer on Payment page.
        /// The Billing Address object has useful default values. Change them when you instantiate it as necessary.
        /// </summary>
        void EnterWireTransferBillingAddress(Address address);

        /// <summary>
        /// Fill in the street address and let Google suggest an action.
        /// </summary>
        /// <param name="street">Street address to enter in the field.</param>
        void FillStreetAddressFieldAndLetGoogleSuggestionAct(string street);

        /// <summary>
        /// Enter text into a form field.
        /// </summary>
        void FillFormControlByText(IElement formControl, string text);

        void FillFormSelectByValue(IElement selectControl, string value);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Proceed to the payment screen.
        /// </summary>
        void ProceedToPayment();

        /// <summary>
        /// Fill out the State form file filed on the Billing Address form.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="state"></param>
        void SelectState(IElement element, string state);

        /// <summary>
        /// Select FedEx state 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="state"></param>
        void SelectFedExState(IElement element, string state);

        /// <summary>
        /// Check if Shipping page loaded the correct form.
        /// </summary>
        void CheckShippingFormIsLoaded();

        /// <summary>
        /// Does FedEx Modal Show
        /// </summary>
        bool DoesFedExModalShow();
    }
}