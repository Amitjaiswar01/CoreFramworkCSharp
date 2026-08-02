namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping
{
    public interface IShippingMobile : IShippingDesktop
    {
        void OpenOrderSummaryBlock();
        void SelectRequiredNoteText();
        void OpenAddNewAddressModal();
        string GetShippingValue();
    }
}
