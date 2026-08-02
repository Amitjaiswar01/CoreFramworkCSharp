using System;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow
{
    public interface IShoppingCartWorkflowDesktop
    {
        int GetShippingTypeOptions();
        void AddItemsToCartBySku(ProductModel cartProductAddItems);
        void AddMultipleItemsToCart(string url = null, int numberOfProducts = 0, IList<string> listOfSkus = null);
        void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts);
        void EmptyCart();
        bool WaitForOrderStatusKioskPriceUpdate();
        bool IsShippingTypeAvailable(string countryCode, string zipCode, string shippingType);
        bool VerifyAddress(Address address, IElement addressElement);
        void ProceedToShippingPage();
        void ProceedToPaymentWithSingleProduct(string shortSku = "");
        void EnableTooltip(IElement element);
        void LoopThroughAndVerifyShippingOptions(int numOfShippingOptions);
        void LargeImageOnPrintModal();
        void OpenShippingOptions(string countryCode, string zipCode);
        void EmployeeProceedToPaymentPageWithSingleItem(string shortSku = "");
        void UpdateShippingStateFromPaymentPage(string state);
        void NavigateBackToShippingPageFromPaymentPage();
        void ProceedToPayment();
        void ShowFedExValidationModal(bool enterApartment = true, Address address = null);
        void GoToOrderConfirmationFromCartUsingCc();
        List<String> SelectRandomQuantityAndAddToCart(int totalItems);
        List<DateTime> GetUspsHolidays(int year);
        DateTime AddBusinessDays(DateTime inputDate, int numOfDays);
        DateTime AddBusinessDaysForStandardShipping(DateTime inputDate, int numOfDays);
        Address CreateNewSavedAddressFromModal(Address address = null, string shippingNameSuffix = "FromAutomation", int newAddressButtonIndex = 0);
        Address CreateNewSavedAddress(Address address = null, bool goBackToShippingPage = false);
    }
}
