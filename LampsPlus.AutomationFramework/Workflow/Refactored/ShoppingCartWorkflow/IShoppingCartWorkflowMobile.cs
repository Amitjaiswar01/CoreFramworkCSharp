using System;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow
{
    public interface IShoppingCartWorkflowMobile
    {
        int GetShippingTypeOptions();
        bool VerifyAddress(Address address, IElement addressElement);
        void AddItemsToCartBySku(ProductModel cartProductAddItems);
        void AddMultipleItemsToCart(string url = null, int numberOfProducts = 0, IList<string> listOfSkus = null);
        void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts);
        void EmptyCart();
        void ProceedToPaymentWithSingleProduct(string shortSku = "");
        void LoopThroughAndVerifyShippingOptions(int numOfShippingOptions);
        void UpdateShippingStateFromPaymentPage(string state);
        void NavigateBackToShippingPageFromPaymentPage();
        void ProceedToPayment();
        void GoToOrderConfirmationFromCartUsingCc();
        void ShowFedExValidationModal(bool enterApartment = true, Address address = null);
        void WaitForTaxLabelToUpdate();
        List<String> SelectRandomQuantityAndAddToCart(int totalItems);
        Address CreateNewSavedAddress(Address address = null, bool goBackToShippingPage = false);
    }
}
