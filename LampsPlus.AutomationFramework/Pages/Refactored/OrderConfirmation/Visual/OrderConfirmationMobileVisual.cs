using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual
{
    public class OrderConfirmationMobileVisual : OrderConfirmationMobile, IOrderConfirmationMobileVisual
    {
        public OrderConfirmationMobileVisual(IBrowser browser, OperatingSystem operatingSystem) : base(browser, operatingSystem)
        {
        }

        public List<IElement> IgnoreEmailIdAndOrderId()
        {
            return new List<IElement> { EmailId, OrderId };
        }

        public IElement IgnoreCreateAccountSuccessEmailElement()
        {
            return CreateAccountSuccessEmailElement;
        }

        public IElement IgnoreEmailUTagElement()
        {
            return EmailUTagElement;
        }

        public IElement IgnoreOrderIdHeading()
        {
            return OrderIdHeading;
        }

        public IElement IgnoreOrderSummaryContainer()
        {
            return OrderSummaryContainer;
        }
    }
}
