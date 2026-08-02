using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList

{
    public interface IWishListMobile : IWishListDesktop
    {
        void WaitForEmptyWishListToLoad();
        void OpenWishListOptions();
        void OpenCreateNewListOption();
        void OpenDeleteWishListModal();
        void EnterNameForCreateNewWishList(string name);
        void OpenNewWishList(int index);
        List<string> GetProductNameAndQtyFromWishlist();
    }
}