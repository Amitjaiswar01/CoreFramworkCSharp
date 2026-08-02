namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation
{
    public interface IOrderConfirmationMobile : IOrderConfirmationDesktop
    {
        void WaitForOrderConfirmationPageToLoad();
    }
}