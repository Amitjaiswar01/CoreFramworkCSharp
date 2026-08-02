using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
	/// <summary>
	/// https://www.lampsplus.com/wish-list/
	/// </summary>
	public class MobileWishList : WishListBase
	{
        /// <inheritdoc />
        public MobileWishList(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings

        private string WishListAddToCartButtonClass { get; } = "wlAddToCartButton";
        private string WlSkuClass { get; } = "wlSKU";
        public override string HideMobileOverlayClass { get; } = "hideMobileOverlay";
        public override string LinkAddToCartClass { get;} = "wlAddToCartButton";
        #endregion

        #region Page Elements
        public IElement WlSku => Browser.Locate.ElementByClassName(WlSkuClass);

        #endregion

        public override string FirstProductSku => WlSku.Text.Replace("Style #", "").Replace(" ", string.Empty);

        public override string ProductNameMobile(int index) => Browser.Locate.ElementsByClassName(WlProdRightClass)[index].Text;

        /// <inheritdoc />

        public override bool IsWishListAddToCartButtonVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(WishListAddToCartButtonClass.ToCssClassSelector()));
        }
    }
}
