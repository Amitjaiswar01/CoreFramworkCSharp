using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList.Visual
{
    public interface IWishListDesktopVisual : IWishListDesktop
    {
        IElement IgnoreWishListName();
        IElement IgnoreOpenList();
    }
}
