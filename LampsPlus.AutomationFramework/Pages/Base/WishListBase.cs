using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class WishListBase : Page, IWishList
    {
        /// <inheritdoc />
        protected WishListBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { }

        #region CSS Selector Strings
        public string CertonaItemsId { get; } = "certonaItems";
        public string WishListProdNameClass { get; } = "wishlistResultProdName";
        public string WishListQtyClass { get; } = "wishlistQty";
        public string WishListResultContainerClass { get; } = "wishlistResultContainer";
        public string WlProdRightClass { get; } = "wlProdRight";
        
        public abstract string FirstProductSku { get; }
        public abstract string HideMobileOverlayClass { get; }
        public abstract string LinkAddToCartClass { get; }
        public abstract string ProductNameMobile(int index);
        #endregion

        #region Page Elements
        public IElement MoreYouMayLikeWidgetContainer => Browser.Locate.ElementBySelector(CertonaItemsId.ToCssIdSelector());
        public IElement ProductInformation(int index) => Browser.Locate.ElementsBySelector($"{WishListResultContainerClass.ToCssClassSelector()} {HtmlTextWriterTag.Span}")[index];
        #endregion

        public string ProductName(int index) => Browser.Locate.ElementsByClassName(WishListProdNameClass)[index].Text.Split('#').First().Trim().Replace("- Style", "").Trim();
        public string ProductSku(int index) => ProductInformation(index).Text.Split('#').Last().Trim();
        public string ProductQuantity(int index) => Browser.Locate.ElementsByClassName(WishListQtyClass)[index].GetAttribute(HtmlTextWriterAttribute.Value.ToString());

        /// <inheritdoc />
        public abstract bool IsWishListAddToCartButtonVisible(int timeToWait);
    }
}