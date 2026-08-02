using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual
{
    public class CartDesktopVisual : CartDesktop, ICartDesktopVisual
    {
        public CartDesktopVisual(IBrowser browser, IModalDesktop modal, IAssert assert, ProductActions productActions) : base(browser, modal, assert, productActions)
        {
        }

        public IElement IgnoreShippingOptionModal()
        {
            return ShippingOptionModal;
        }

        public IElement IgnoreModalTimeCheck(int index)
        {
            return ModalTimeCheck(index);
        }
        
        public List<IElement> IgnoreCartIdAndMoreYouMayLike()
        {
            return new List<IElement> { CartIdContainer, CartMoreYouMayLikeContainer };
        }

        public IElement IgnoreCartId()
        {
            return CartIdContainer;
        }

        public IElement IgnoreMoreYouMayLike()
        {
            return CartMoreYouMayLikeContainer;
        }

        public List<IElement> IgnoreSaleCountdownCartIdAndMoreYouMayLike()
        {
            return new List<IElement> {SaleCountdown, CartIdContainer, CartMoreYouMayLikeContainer};
        }

        public List<IElement> IgnoreCartIdAndCartTitle()
        {
            return new List<IElement>{CartIdContainer,CartTitle};
        }
    }
}