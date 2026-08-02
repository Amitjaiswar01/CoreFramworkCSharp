using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails.Visual
{
    public interface IOrderDetailsDesktopVisual : IOrderDetailsDesktop
    {
        IElement IgnoreMoreYouMayLikeSection();
    }
}