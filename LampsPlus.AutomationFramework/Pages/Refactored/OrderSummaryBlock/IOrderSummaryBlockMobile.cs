namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock
{
    public interface IOrderSummaryBlockMobile : IOrderSummaryBlockDesktop
    {
        string GetProductName();
        string GetProductPrice();
        string GetProductQuantity();
        string GetShortSku();
        void CloseOrderSummaryDrawer();
        void OpenOrderSummaryDrawer();
    }
}
