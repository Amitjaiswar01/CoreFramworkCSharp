using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory.Visual
{
    public interface IOrderHistoryDesktopVisual : IOrderHistoryDesktop
    {
        IElement IgnoreMoreYouMayLike();
        IElement IgnoreSimilarItem();
    }
}
