using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Payment
{
    public interface IPaymentDesktop : IPageObjectModel
    {
        void SelectDeliveryPolicyAgreementIfVisible();
        void SelectSameAsShippingCheckbox();
        void PlaceOrder();
        void EnterCreditCartInformation(CreditCard creditCard);
        void ClickOnPaymentDetailsLink();
        void SelectInternationalAgreementAndPlaceOrder();
        void ShowCountryField();
        void EnableWireTransfer();
        void EnablePurchaseOrder();
        void EnablePaperCheck();
        void SelectGiftCardLink();
        void DeletePaymentOption();
        void SelectShippingHeaderLink();
        void SelectCheckPaymentOption();
        string GetPaymentFieldErrorMessage();
        string GetDropdownFieldErrorMessage();
        string GetBillingPhoneNumberErrorMessage();
        IElement GetMinimumOrderErrorMessage();
        IElement GetProp65WarningDialog();
        List<IElement> GetListOfPaymentPageDropdownFields();
        List<IElement> GetListOfPaymentPageFreeTypeFields();
        bool IsProp65WarningDialogVisible { get; }
        bool IsMinimumOrderErrorMessageVisible { get; }
        bool IsMinimumOrderMessageVisible { get; }
        bool IsSavedPaymentsElementVisible { get; }
        bool IsInternationalCheckboxDisplayed { get; }
        bool IsPaymentTypeAvailable(string paymentType);
        bool IsGiftCertContainerVisible { get; }
        Dictionary<string, string> GetAddressTextWithApartmentFieldActive { get; }
        Dictionary<string, string> GetSuggestedAddressText { get; }
        Dictionary<string, string> GetPaymentPageDropdownFieldErrorMessages();
        Dictionary<string, string> GetPaymentPageFreeTypeFieldErrorMessages(int numberOfFreeTypeFieldsOnPaymentPage);
    }
}
