using System;
using System.Linq;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/wish-list/
    /// </summary>
    public class WishList : WishListBase
    {
        /// <inheritdoc />
        public WishList(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings
        public override string LinkAddToCartClass { get; } = "lnkAddToCart";
        public override string HideMobileOverlayClass => throw new NotImplementedException();
        #endregion

        public override string FirstProductSku => ProductSku(0);

        /// <inheritdoc />
        public override bool IsWishListAddToCartButtonVisible(int timeToWait) => throw new NotImplementedException();
        public override string ProductNameMobile(int index) => Browser.Locate.ElementsByClassName(WlProdRightClass)[index].Text.Split('#').First().Trim().Replace("Style", "").Trim();
    }
}
