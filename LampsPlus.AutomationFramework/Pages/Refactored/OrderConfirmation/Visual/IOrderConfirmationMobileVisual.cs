using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual
{
    public interface IOrderConfirmationMobileVisual : IOrderConfirmationMobile
    {
        List<IElement> IgnoreEmailIdAndOrderId();
        IElement IgnoreCreateAccountSuccessEmailElement();
        IElement IgnoreEmailUTagElement();
        IElement IgnoreOrderIdHeading();
        IElement IgnoreOrderSummaryContainer();
    }
}
