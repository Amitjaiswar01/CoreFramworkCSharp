
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual
{
    public interface IManageAccountMobileVisual : IManageAccountDesktop
    {
        IElement GetPaymentScrollableOverlay();
        IElement IgnoreFirstNameElement();
        IElement IgnoreLastNameElement();
        IElement IgnoreAddress2Element();
        IElement IgnoreSaveEmailPrefButton();
        IElement GetShippingAddressScrollableOverlay();
    }
}
