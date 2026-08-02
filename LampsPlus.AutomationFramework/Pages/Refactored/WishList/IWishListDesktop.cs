using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList
{
    public interface IWishListDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        int WishListItemsCount { get; }
        int GetWishListItemQty();
        string GetWishListItemSku();
        string GetWishListItemSkuList(int index);
        string GetWishListProductQty(int index);
        string GetWishListHeaderText();
        void RenameWishList(string name);
        void CreateWishList(string name);
        void RemoveAllWishListItems();
        void OpenWishList();
        void DeleteWishList();
        void SelectPencilIcon();
        void DeleteWishListItems();
        void AddToCartByItemIndex(int wishlistItemIndex);
        void EmptyWishList();
        void AddAllWishlistSkusToCart();
        bool SelectWishListItemByName(string text);
        bool CompareWishListItems(string originalWishListSkus, string openWishListSkus);
        bool IsWishListPageLoaded(int timeToWait);
        bool DoesWishListMatchAddedProducts(Dictionary<string, int> addedProducts, List<ProductModel> productsInWishList);
        bool IsWishListEmpty();
        IElement GetFreeShippingCallout();
        List<Utilities.ProductModel> GetWishListItemsContent();
    }
}