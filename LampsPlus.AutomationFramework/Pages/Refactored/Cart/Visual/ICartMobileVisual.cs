using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual
{
    public interface ICartMobileVisual : ICartMobile
    {
        IElement IgnoreShippingOptionModal();
        IElement IgnoreCartId();
        List<IElement> IgnoreCartIdAndMoreYouMayLike();
    }
}
