using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping
{
    public interface IShippingDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        void WaitForShippingPageToLoad();
        void ProceedToPayment();
        void HandleFedExModalIfPresent();
        void ShipToDifferentAddress();
        void SelectNotDefaultShippingAddress(int index);
        void ShipToMultipleAddresses();
        void WaitForModalToFullyClose();
        void OpenNewAddressByIndex(int index);
        void WaitForShippingMethodsChangedContainer();
        string TaxLabel { get; }
        string GetShortSkuOnShipping();
        string GetShippingCostFromModifyShippingBlock();
        bool DoesFedExModalShow();
        IElement GetPromoCodeElementOnOrderSummary();
        IElement GetShippingOptionsChangedContainer();
    }
}
