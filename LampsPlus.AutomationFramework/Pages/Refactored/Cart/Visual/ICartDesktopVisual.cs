using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual
{
    public interface ICartDesktopVisual : ICartDesktop
    {
        IElement IgnoreShippingOptionModal();
        IElement IgnoreModalTimeCheck(int index);
        IElement IgnoreCartId();
        IElement IgnoreMoreYouMayLike();
        List<IElement> IgnoreCartIdAndMoreYouMayLike();
        List<IElement> IgnoreSaleCountdownCartIdAndMoreYouMayLike();
        List<IElement> IgnoreCartIdAndCartTitle();
    }
}
