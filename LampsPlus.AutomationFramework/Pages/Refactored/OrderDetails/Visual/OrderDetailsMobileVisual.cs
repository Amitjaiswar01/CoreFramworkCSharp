using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails.Visual
{
    public class OrderDetailsMobileVisual : OrderDetailsMobile, IOrderDetailsMobileVisual
    {
        public OrderDetailsMobileVisual(IBrowser browser) : base(browser) { }

        public IElement IgnoreMoreYouMayLikeSection()
        {
            return MoreYouMayLikeSection;
        }

    }
}