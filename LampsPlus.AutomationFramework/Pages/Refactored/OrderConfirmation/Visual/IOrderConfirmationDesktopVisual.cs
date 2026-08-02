using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual
{
    public interface IOrderConfirmationDesktopVisual : IOrderConfirmationDesktop
    {
        IElement IgnoreEmailId();
        IElement IgnoreOrderId();
        IElement IgnoreCreateAccountEmail();
        IElement IgnoreOrderConfirmationContainer();
        IElement IgnoreEmailUTagElement();
        IElement IgnoreOrderIdHeading();
    }
}
