using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping.Visual
{
    public class ShippingDesktopVisual : ShippingDesktop, IShippingDesktopVisual
    {
        public ShippingDesktopVisual(IBrowser browser, IModalDesktop modal) : base(browser, modal)
        {
        }
    }
}