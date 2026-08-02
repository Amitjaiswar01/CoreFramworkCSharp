using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface IWishList
    {
        #region CSS  Selector Strings
        string CertonaItemsId { get; }
        string FirstProductSku { get; }
        string HideMobileOverlayClass { get; }
        string LinkAddToCartClass { get; }
        #endregion

        #region Page Elements
        IElement MoreYouMayLikeWidgetContainer { get; }
        #endregion

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        string ProductName(int index);
        string ProductNameMobile(int index);
        string ProductSku(int index);
        string ProductQuantity(int index);
        IElement ProductInformation(int index);
        bool IsWishListAddToCartButtonVisible(int timeToWait);
    }
}