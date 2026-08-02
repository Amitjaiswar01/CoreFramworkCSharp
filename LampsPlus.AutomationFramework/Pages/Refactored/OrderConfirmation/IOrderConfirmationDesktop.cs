using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation
{
    public interface IOrderConfirmationDesktop : IPageObjectModel
    {
        string GetOrderIdNumber { get; }
        string GetEmail { get; }
        string GetOcPageProductName();
        void FillInCreateAccountForm(string password);
        void CloseCreateAccountModal();
        void IsLincOptionWidgetVisible();
        IElement GetOcPageShippingAddress();
        IElement GetOcPageBillingAddress();
    }
}