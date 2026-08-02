using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList.Visual
{
    public class WishListMobileVisual : WishListMobile, IWishListMobileVisual
    {
        public WishListMobileVisual(IBrowser browser, IModalDesktop modal, IMobileDrawer drawer, OperatingSystem operatingSystem) : base(browser, modal, drawer, operatingSystem)
        {
        }
    }
}