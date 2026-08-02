using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual
{
    public class OrderConfirmationDesktopVisual : OrderConfirmationDesktop, IOrderConfirmationDesktopVisual
    {
        public OrderConfirmationDesktopVisual(IBrowser browser, OperatingSystem operatingSystem) : base(browser, operatingSystem)
        {
        }

        public IElement IgnoreEmailId()
        {
            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
            return EmailId;
        }

        public IElement IgnoreOrderId()
        {
            Browser.Wait.ForDisplayedElement(OrderId);
            return OrderId;
        }

        public IElement IgnoreCreateAccountEmail()
        {
            return CreateAccountEmail;
        }

        public IElement IgnoreOrderConfirmationContainer()
        {
            return OrderConfirmationContainer;
        }

        public IElement IgnoreEmailUTagElement()
        {
            return EmailUTagElement;
        }

        public IElement IgnoreOrderIdHeading()
        {
            return OrderIdHeading;
        }
    }
}
