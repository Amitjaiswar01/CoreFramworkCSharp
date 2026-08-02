namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock
{
    public interface IOrderSummaryBlockDesktop : IPageObjectModel
    {
        bool IsOrderSummaryKioskPriceStatusVisible();
        bool IsProceedToPaymentButtonVisible();
        decimal GetKioskProductTotalPrice();
        void ClickProceedToPaymentButton();
        void NavigateBackToCartOverviewPage();
    }
}
