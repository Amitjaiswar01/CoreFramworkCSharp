using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails.Visual
{
    public class OrderDetailsDesktopVisual : OrderDetailsDesktop, IOrderDetailsDesktopVisual
    {
        public OrderDetailsDesktopVisual(IBrowser browser) : base(browser) { }

        public IElement IgnoreMoreYouMayLikeSection()
        {
            return MoreYouMayLikeSection;
        }
    }
}