using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping.Visual
{
    public class ShippingMobileVisual : ShippingMobile, IShippingMobileVisual
    {
        public ShippingMobileVisual(IBrowser browser, IModalDesktop modal) : base(browser, modal)
        {
        }

        public IElement IgnoreMobileShippingOptionsModal()
        {
            return GetMobileShippingOptionsModal;
        }
    }
}