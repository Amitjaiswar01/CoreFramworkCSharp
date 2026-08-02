using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList
{
    public class WishListMobile : WishListDesktop, IWishListMobile
    {
        //Class members
        private string _calloutBtnList = "calloutBtnList";
        private string _createWishlistBtn = "createWishlistBtn";
        private string _newWishListNameXpath = "//*[@id='newWishlistName']";
        private string _removeWlItemClass  = "removeWlItem";
        private string _wishListMobileOptionsId = "wlOptionsOpen";
        private string _wishlistMobileMenuId  = "wishlistMobileMenu";
        private string _wishListOptionsDeleteClass = "wlOptionsDelete";
        private string _wishListAddToCartButtonClass  = "wlAddToCartButton";
        private string _wishlistMobileHeaderNameClass  = "wishlistMobileHeaderName";
        private string _wlOptionsNewClass = "wlOptionsNew";
        private string _wishListOptionsOpenWishlistClass = "wlOptionsOpenWishlist";
        private string _defaultWlContinueShoppingClass = "defaultWlContinueShopping";
        private string _wishlistItemNameXpath = "//*[@class='wishlistItemName ']/h2";
        private string _wishlistQtyInputClass = "wishlistQtyInput";
        private string _availableWishlistsClass = "availableWishlists__link";
        private string _emptyViewOpenWishlistBtnId = "emptyViewOpenWishlistBtn";

        protected override string LinkAddToCartClass => "wlAddToCartButton";
        protected override string WlOpenListItemNameClass => "availableWishlists__assetName";
        protected override string WishListProdImgClass => "wishlistItemImage";

        private IElement OpenWishListLink => Browser.Locate.ElementById(_emptyViewOpenWishlistBtnId);
        private IElement AvailableWishList(int index) => Browser.Locate.ElementsByClassName(_availableWishlistsClass)[index];
        private IElement WishlistMobileMenu => Browser.Locate.ElementById(_wishlistMobileMenuId);
        private IElement MobileOptionsBtn => Browser.Locate.ElementById(_wishListMobileOptionsId);
        private IElement MobileOptionsDeleteElement => Browser.Locate.ElementByClassName(_wishListOptionsDeleteClass);
        private IElement MobileOptionsCreateNewList => Browser.Locate.ElementByClassName(_wlOptionsNewClass);
        private IElement MobileOptionsOpenElement => Browser.Locate.ElementByClassName(_wishListOptionsOpenWishlistClass);
        private IElement WishlistName => Browser.Locate.ElementByXpath(_wishlistItemNameXpath);
        private IElement WishlistQty => Browser.Locate.ElementByClassName(_wishlistQtyInputClass);
        protected override IElement CreateNewEmptyWishlistBtn => Browser.Locate.ElementByClassName(_createWishlistBtn);
        protected override IElement CreateWishListNameField => Browser.Locate.ElementByXpath(_newWishListNameXpath);
        protected override IElement WishListNameInputElement => Browser.Locate.ElementByClassName(_wishlistMobileHeaderNameClass);
        protected override IElement RemoveProductLink(int index) => Browser.Locate.ElementsByClassName(_removeWlItemClass)[index];
        protected override ReadOnlyCollection<IElement> WishListAddToCardBtns => Browser.Locate.ElementsByClassName(_wishListAddToCartButtonClass);

        //Instances
        private readonly IMobileDrawer _drawer;

        public WishListMobile(IBrowser browser, IModalDesktop modal, IMobileDrawer drawer, OperatingSystem operatingSystem) : base(browser, modal, operatingSystem)
        {
            _drawer = drawer;
        }

        //Interface implementation
        public override int WishListItemsCount => Convert.ToInt32(Browser.ExecuteJs("return lp.globals.portfolioItemCount"));
        public override bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(LinkAddToCartClass.ToCssClassSelector()));
        
        public override string GetWishListHeaderText()
        {
            return Browser.Locate.ElementBySelector(_wishlistMobileHeaderNameClass.ToCssClassSelector()).Text;
        }

        public override void CreateWishList(string name)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));

            MobileOptionsBtn.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_wlOptionsNewClass.ToCssClassSelector()));

            MobileOptionsCreateNewList.Click();

            ClearWishListName();

            CreateWishListNameField.SendKeys(name);

            CreateNewEmptyWishlistBtn.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_calloutBtnList.ToCssClassSelector()));
        }

        public override void DeleteWishList()
        {
            Browser.RefreshPage();
            Browser.Wait.ForDomReady();
            if (!MobileOptionsBtn.IsInitialized) return;
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));

            MobileOptionsBtn.Click();
            Browser.Wait.ForMobileModalToFullyOpen(WishlistMobileMenu);

            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListOptionsDeleteClass.ToCssClassSelector()));
            MobileOptionsDeleteElement.Click();
            Browser.Wait.ForDomReady();

            _drawer.ConfirmDrawer();
            Browser.Wait.IsVisibleElement(By.Id(_emptyViewOpenWishlistBtnId));
        }

        public override void EmptyWishList()
        {
            var portfolioItemCount = WishListItemsCount;

            if (portfolioItemCount == 0)
            {
                return;
            }

            if (Browser.PageUrl != Urls.WishListPageUrl)
            {
                Browser.Navigate(Urls.WishListPageUrl);
                Browser.Wait.ForDomReady();
            }

            for (var i = portfolioItemCount - 1; i >= 0; i--)
            {
                Browser.ScrollIntoView(RemoveProductLink(i), true);
                Browser.Wait.ForClickableElement(RemoveProductLink(i));
                RemoveProductLink(i).Click();
                _drawer.ConfirmDrawer();
            }

            DeleteWishList();
        }

        public override void RemoveAllWishListItems()
        {
            var portfolioItemCount = WishListItemsCount;

            if (portfolioItemCount == 0)
            {
                return;
            }

            if (Browser.PageUrl != Urls.WishListPageUrl)
            {
                Browser.Navigate(Urls.WishListPageUrl);
                Browser.Wait.ForDomReady();
            }

            for (var i = portfolioItemCount - 1; i >= 0; i--)
            {
                Browser.ScrollIntoView(RemoveProductLink(i), true);
                Browser.Wait.ForClickableElement(RemoveProductLink(i));
                RemoveProductLink(i).Click();
                _drawer.ConfirmDrawer();
            }
        }

        public override void OpenWishList()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));
            Browser.RefreshPage();
            Browser.Wait.IsVisibleElement(By.Id(_wishListMobileOptionsId));
            MobileOptionsBtn.Click();
            Browser.Wait.ForMobileModalToFullyOpen(WishlistMobileMenu);
            MobileOptionsOpenElement.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(WlOpenListItemNameClass));
        }

        public override void AddToCartByItemIndex(int wishListItemIndex)
        {
            Browser.ClickByJs(WishListAddToCardBtns[wishListItemIndex]);
        }

        public override void RenameWishList(string name)
        {
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishlistMobileHeaderNameClass.ToCssClassSelector()));

            if (OperatingSystem == OperatingSystem.iPhone)
            {
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(WishListNameInputElement, ref xElementCoordinate, ref yElementCoordinate, 110);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
                Browser.Wait.ForDomReady();
            }
            else
            {
                Browser.ClickByJs(WishListNameInputElement);
            }

            Browser.ExecuteJs($"document.querySelector('{_wishlistMobileHeaderNameClass.ToCssClassSelector()}').innerText = '{name}'");

            Browser.Wait.IsVisibleElement(By.ClassName(ContentEditableBtnSaveClass),30);
            UpdateWishListNameBtn.Click();

            Browser.Wait.ForDomReady();
        }

        public override bool SelectWishListItemByName(string text)
        {
            Browser.Wait.AreAllElementsVisible(By.ClassName(WlOpenListItemNameClass));

            foreach (var itemName in WishListItemNameElements)
            {
                if (itemName.Text == text)
                {
                    itemName.Click();
                    Browser.Wait.IsVisibleElement(By.CssSelector(_wishListAddToCartButtonClass.ToCssClassSelector()));
                    Browser.RefreshPage();
                    return true;
                }
            }

            return false;
        }

        public override void AddAllWishlistSkusToCart()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));
            MobileOptionsBtn.Click();
            Browser.Wait.ForMobileModalToFullyOpen(WishlistMobileMenu);
            WishListAddAllToCartButton.Click();
        }

        public void WaitForEmptyWishListToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_defaultWlContinueShoppingClass.ToCssClassSelector()));
        }

        public void OpenWishListOptions()
        {
            Browser.ClickByJs(MobileOptionsBtn);
            Browser.Wait.ForMobileModalToFullyOpen(WishlistMobileMenu);
        }

        public List<string> GetProductNameAndQtyFromWishlist()
        {
            var listOfProductNameAndQty = new List<string> { WishlistName.Text, WishlistQty.GetAttribute("value") };
            return listOfProductNameAndQty;
        }

        public void OpenCreateNewListOption()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));
            MobileOptionsBtn.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_wlOptionsNewClass.ToCssClassSelector()));
            MobileOptionsCreateNewList.Click();
        }

        public void EnterNameForCreateNewWishList(string name)
        {
            ClearWishListName();
            CreateWishListNameField.SendKeys(name);
            CreateNewEmptyWishlistBtn.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_calloutBtnList.ToCssClassSelector()));
        }

        public void OpenDeleteWishListModal()
        {
            Browser.RefreshPage();
            Browser.Wait.ForDomReady();
            if (!MobileOptionsBtn.IsInitialized) return;
            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListMobileOptionsId.ToCssIdSelector()));

            MobileOptionsBtn.Click();
            Browser.Wait.ForMobileModalToFullyOpen(WishlistMobileMenu);

            Browser.Wait.IsVisibleElement(By.CssSelector(_wishListOptionsDeleteClass.ToCssClassSelector()));
            MobileOptionsDeleteElement.Click();
            Browser.Wait.ForDomReady();
        }

        public void OpenNewWishList(int index)
        {
            Browser.Wait.IsVisibleElement(By.Id(_emptyViewOpenWishlistBtnId));
            OpenWishListLink.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_availableWishlistsClass));
            AvailableWishList(index).Click();
        }
    }
}