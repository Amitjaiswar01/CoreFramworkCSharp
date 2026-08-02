using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory.Visual
{
    public class OrderHistoryMobileVisual : OrderHistoryMobile, IOrderHistoryMobileVisual
    {
        public OrderHistoryMobileVisual(IBrowser browser) : base(browser)
        {
        }
    }
}
