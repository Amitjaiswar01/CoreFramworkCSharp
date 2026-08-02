using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual
{
    public class CartMobileVisual : CartMobile, ICartMobileVisual
    {
        public CartMobileVisual(IBrowser browser, IModalDesktop modal, IMobileDrawer drawer, IAssert assert, ProductActions productActions) : base(browser, modal, drawer, assert, productActions)
        {
        }

        public IElement IgnoreShippingOptionModal()
        {
            return ShippingOptionModal;
        }

        public IElement IgnoreCartId()
        {
            return CartIdContainer;
        }

        public List<IElement> IgnoreCartIdAndMoreYouMayLike()
        {
            return new List<IElement> { CartIdContainer, CartMoreYouMayLikeContainer };
        }
    }
}