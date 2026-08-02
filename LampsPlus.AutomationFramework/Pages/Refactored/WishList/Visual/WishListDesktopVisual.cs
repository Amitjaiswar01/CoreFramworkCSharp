using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList.Visual
{
    public class WishListDesktopVisual : WishListDesktop, IWishListDesktopVisual
    {
        public WishListDesktopVisual(IBrowser browser, IModalDesktop modal, OperatingSystem operatingSystem) : base(browser, modal, operatingSystem)
        {
        }

        public IElement IgnoreWishListName()
        {
            Browser.Wait.ForDisplayedElement(WishListNameInputElement);
            return WishListNameInputElement;
        }

        public IElement IgnoreOpenList()
        {
            return WishListOpenList;
        }
    }
}