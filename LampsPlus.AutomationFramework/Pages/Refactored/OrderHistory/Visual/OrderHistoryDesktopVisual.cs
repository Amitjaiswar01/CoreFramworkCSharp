using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory.Visual
{
    public class OrderHistoryDesktopVisual : OrderHistoryDesktop, IOrderHistoryDesktopVisual
    {
        public OrderHistoryDesktopVisual(IBrowser browser) : base(browser)
        {
        }

        public IElement IgnoreMoreYouMayLike()
        {
            return RecommendedProductsOnTrackItemPage(0);
        }
        public IElement IgnoreSimilarItem()
        {
            return RecommendedProductsOnTrackItemPage(1);
        }
    }
}