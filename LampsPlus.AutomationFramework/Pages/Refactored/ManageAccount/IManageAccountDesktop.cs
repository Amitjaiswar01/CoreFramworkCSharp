using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount
{
    public interface IManageAccountDesktop : IPageObjectModel
    {
        IBrowser Navigate(string url);
        IBrowser Navigate();
        IAddress GetFirstSavedShippingAddress();
        string PaymentOptionsUrl { get; }
        string ShippingAddressOptionsUrl { get; }
        string GetEmailPreferenceHeaderText();
        string GetShippingAddressFullName();
        string GetShippingAddressStreetName();
        string GetShippingAddressCityStateZipName();
        string GetShippingAddressPhoneNumber();
        string GetNameOnCreditCard();
        string GetCreditCardExpirationDate();
        string GetPaymentName();
        string GetPaymentAddressField1();
        string GetPaymentCity();
        string GetPaymentAddressField2();
        string GetCreditCardNumber();
        string GetPaymentPhoneNumber();
        string GetProfilePhoneNumber();
        string GetAccountProfileFullName();
        bool IsManageShippingAddressesLinkVisible { get; }
        bool IsManagePaymentOptionsLinkVisible { get; }
        bool IsPaymentOptionDeleted();
        bool IsModalThankYouMessageVisible();
        int IsOnlyDefaultPaymentOptionAvailable();
        void ResetAccountShippingAddresses();
        void ResetAccountPaymentOptions();
        void AddNewPaymentMethod(CreditCard creditCard, IAddress address);
        void AddNewShippingAddressToModal(IAddress shippingAddress);
        void OpenShippingAddressForm();
        void SaveShippingAddress();
        void AddShippingAddress(IAddress address);
        void EditPaymentOptionDetails(CreditCard creditCard, IAddress address);
        void SetNewPassword(string newPassword);
        void SetOriginalPassword(string originalPassword);
        void NavigateToChangePasswordLink();
        void OpenYourInformationModal();
        void EditAccountPhoneNumber();
        void ResetAccountPhoneNumber();
        void OpenEditPaymentModal();
        void ClosePaymentModal();
        void OpenEditShippingAddressModal();
        void ChangeShippingPhoneNumber();
        void OpenEmailPreferencesModal();
        void SaveEmailPreferences();
        void DeleteOneSavedPaymentOption();
        void SaveNewPassword(string newPassword);
        void ClearAccountShippingFormFields();
        void SelectNewOptionAndSave();
        void EditAccountContactNumbers(string phoneNumber, string faxNumber, string cellPhoneNumber);
        void UpdateAccountProfile(string firstName, string lastName, string phoneNumber, string previousPhoneNumber = null);
        void Unsubscribe();
        void CloseChangePasswordThankYouModal();
    } 
}