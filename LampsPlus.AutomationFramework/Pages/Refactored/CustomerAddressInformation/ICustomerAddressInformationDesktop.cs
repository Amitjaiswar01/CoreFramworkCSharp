using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation
{
    public interface ICustomerAddressInformationDesktop : IPageObjectModel
    {
        void EnterShippingAddress(IAddress address, bool isIntAddress = false, bool isMultiAddress = false);
        void EnterBillingAddress(IAddress address, bool isIntAddress = false);
        void ChangeBillingCountry(IAddress country);
        void SelectCountry(IElement element, IAddress address);
        void SelectCountry(string country);
        void ChangeShippingZip(IAddress address);
        void SelectState(string state, bool isMultiAddress = false);
        void FillFormControlByText(IElement formControl, string text);
        void EnterApartmentAddress(string apartmentNumber);
        void SubmitFedExModalChanges();
        void SaveAddressFromModal();
        void AddAnotherAddressField();
        void KeepCurrentAddressAtFedExModal(bool editButton = false);
        void ClearFedExModalFields();
        void WaitForFedExModalToStopAnimating();
        void SelectFedExState(IElement element, string state);
        void UseSimilarVerifiedAddressOption();
        void SelectSavedAddressShippingInfo();
        string GetPaymentName();
        string GetSavedAddressFullName();
        string GetSavedAddressShippingInfo();
        string GetShipToDifferentAddressButtonLabel();
        string GetValidationErrorMessage(IElement formControl);
        bool IsLoggedInUser { get; }
        bool IsSimilarVerifiedAddressDisplayed { get; }
        Dictionary<string, IElement> ShippingElementsCollection { get; }
        Dictionary<string, string> GetFedExModalApartmentActiveAddressText { get; }
        Dictionary<string, string> GetFedExModalSuggestedAddressText { get; }
        Dictionary<string, IElement> GetFedExModalElements { get; }
        Dictionary<string, IElement> GetFedExModalAddressElements { get; }
        Dictionary<string, string> GetFedExModalMaintainAddressText { get; }
    }
}
