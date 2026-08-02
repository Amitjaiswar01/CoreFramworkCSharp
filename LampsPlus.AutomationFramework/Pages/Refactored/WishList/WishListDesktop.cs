using System.Collections.ObjectModel;
using System.Web.UI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.WishList
{
    public class WishListDesktop : IWishListDesktop
    {
        //Class members
        private string _btnCreateNewXpath = "//*[@id='newWishlist']/div/button ";
        private string _wishListQtyClass  = "wishlistQty";
        private string _freeShippingCalloutClass = "freeShippingCallout";
        private string _createWishListLinkId  = "createWishListLink";
        private string _deleteWishListLinkId  = "deleteWishListLink";
        private string _ctrlRemoveClass  = "ctrlRemove";
        private string _btnDeleteXpath = "//div[@id='deleteWishlistModal']/div[3]/button[1]";
        private string _newWishListNameId = "newWishListName";
        private string _openWishListLinkXpath = "//*[@id='openWishListLink']";
        private string _wishListHeaderNameId  = "wishlistHeaderName";
        private string _wlContinueShoppingClass = "wlContinueShopping";
        private string _wlItemOpenBtnClass = "wlItemOpenBtn";
        private string _addAllItemsToCartClass = "addAllItemsToCart";
        private string _deleteWishListLinkSelector = "#deleteWishListLink ";
        protected string EditIconClass  => "editIcon";
        protected string ContentEditableBtnSaveClass  => "contentEditableBtnSave";
        protected virtual string LinkAddToCartClass => "lnkAddToCart";
        protected virtual string WlOpenListItemNameClass => "wlOpenListItem__name";
        protected virtual string WishListProdImgClass => "wishlistResultProdImg";

        private IElement WishListProductImg(int index) => Browser.Locate.ElementsByClassName(WishListProdImgClass)[index];
        private IElement WishListProductQuantity(int index) => Browser.Locate.ElementsByClassName(_wishListQtyClass)[index];
        private IElement WishListFreeShippingCallout => Browser.Locate.ElementByClassName(_freeShippingCalloutClass);
        private IElement CreateWishListLink => Browser.Locate.ElementById(_createWishListLinkId);
        private IElement DeleteWishListButton => Browser.Locate.ElementById(_deleteWishListLinkId);
        private IElement OpenWishListButton => Browser.Locate.ElementByClassName(_wlItemOpenBtnClass);
        private IElement OpenWishListLink => Browser.Locate.ElementByXpath(_openWishListLinkXpath);
        private IElement PencilEditIcon => Browser.Locate.ElementByClassName(EditIconClass);
        private IElement GetAddToCartBtnByIndex(int index = 0) => WishListAddToCardBtns[index];
        private IElement ConfirmDeleteWishlistBtn => Browser.Locate.ElementByXpath(_btnDeleteXpath);
        protected IElement UpdateWishListNameBtn => Browser.Locate.ElementByClassName(ContentEditableBtnSaveClass);
        protected IElement WishListOpenList => Browser.Locate.ElementByXpath("//*[@id='wlOpenListContainer']");
        protected IElement WishListAddAllToCartButton => Browser.Locate.ElementByClassName(_addAllItemsToCartClass);
        protected ReadOnlyCollection<IElement> WishListItemNameElements => Browser.Locate.ElementsByClassName(WlOpenListItemNameClass);
        protected virtual IElement CreateNewEmptyWishlistBtn => Browser.Locate.ElementByXpath(_btnCreateNewXpath);
        protected virtual IElement CreateWishListNameField => Browser.Locate.ElementById(_newWishListNameId);
        protected virtual IElement WishListNameInputElement => Browser.Locate.ElementById(_wishListHeaderNameId);
        protected virtual IElement RemoveProductLink(int index) => Browser.Locate.ElementsByClassName(_ctrlRemoveClass)[index];
        protected virtual ReadOnlyCollection<IElement> WishListAddToCardBtns => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Button, LinkAddToCartClass);//Browser.Locate.ElementsByClassName(LinkAddToCartClass);

        private void ClearWishListHeaderNameFieldText()
        {
            var inputText = WishListNameInputElement.GetAttribute(HtmlTextWriterAttribute.Value.ToString())?.Length ?? 0;

            while (inputText > 0)
            {
                WishListNameInputElement.SendKeys(Keys.Backspace);
                inputText--;
            }
        }

        protected void ClearWishListName()
        {
            Browser.Wait.ForClickableElement(CreateWishListNameField);
            CreateWishListNameField.Click();
            CreateWishListNameField.Clear();
        }

        //Instances
        protected IBrowser Browser;
        protected OperatingSystem OperatingSystem;
        private readonly IModalDesktop _modal;
        
        public WishListDesktop(IBrowser browser, IModalDesktop modal, OperatingSystem operatingSystem)
        {
            Browser = browser;
            OperatingSystem = operatingSystem;
            _modal = modal;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/wish-list/";
        public virtual bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(LinkAddToCartClass.ToCssClassSelector()));
        public virtual int WishListItemsCount => Convert.ToInt32(Browser.ExecuteJs("return lp.globals.wishlistItemCount"));

        public virtual string GetWishListHeaderText()
        {
            return Browser.Locate.ElementById(_wishListHeaderNameId).Text;
        }

        public int GetWishListItemQty()
        {
            return Convert.ToInt32(Browser.Locate.ElementByClassName(_wishListQtyClass).GetAttribute("value"));
        }

        public string GetWishListItemSku()
        {
            Browser.Wait.ForDisplayedElement(GetAddToCartBtnByIndex(0));
            return GetAddToCartBtnByIndex(0).GetAttribute("data-sku");
        }

        public string GetWishListProductQty(int index)
        {
            return WishListProductQuantity(index).GetAttribute(HtmlTextWriterAttribute.Value.ToString());
        }

        public string GetWishListItemSkuList(int index)
        {
            return GetAddToCartBtnByIndex(index).GetAttribute("data-sku");
        }

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public virtual void CreateWishList(string name)
        {
            CreateWishListLink.Click();

            _modal.IsModalVisible();
            Browser.SwitchFocusToIframe(_modal.GetLpModal());

            ClearWishListName();

            CreateWishListNameField.SendKeys(name);

            CreateNewEmptyWishlistBtn.Click();

            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.CssSelector(_wlContinueShoppingClass.ToCssClassSelector()));
        }

        public virtual void OpenWishList()
        {
            Browser.Wait.ForClickableElement(OpenWishListLink);
            Browser.ClickByJs(OpenWishListLink);
            Browser.Wait.IsVisibleElement(By.CssSelector(_wlItemOpenBtnClass.ToCssClassSelector()));
        }

        public virtual void RenameWishList(string name)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(EditIconClass.ToCssClassSelector()));
            PencilEditIcon.Click();

            ClearWishListHeaderNameFieldText();

            WishListNameInputElement.SendKeys(name);
            Browser.Wait.IsVisibleElement(By.ClassName(ContentEditableBtnSaveClass));
            UpdateWishListNameBtn.Click();

            Browser.Wait.ForDomReady();
        }

        public virtual void DeleteWishList()
        {
            DeleteWishListItems();

            Browser.Wait.IsVisibleElement(By.XPath(_btnDeleteXpath));
            ConfirmDeleteWishlistBtn.Click();
            Browser.Wait.UntilElementDoesntExist(_modal.LpModalId);
        }

        public virtual void EmptyWishList()
        {
            if (Browser.Locate.ElementsBySelector(_deleteWishListLinkSelector).Count == 0)
            {
                return;
            }

            var wishListItemCount = Convert.ToInt32(Browser.ExecuteJs("return lp.globals.wishlistItemCount"));

            if (Browser.PageUrl != Urls.WishListPageUrl)
            {
                Browser.Navigate(Urls.WishListPageUrl);
                Browser.RefreshPage();
            }

            for (var i = wishListItemCount - 1; i >= 0; i--)
            {
                if (i != 0) { Browser.RefreshPage(); }
                Browser.Wait.ForDisplayedElement(RemoveProductLink(i));
                RemoveProductLink(i).Click();
                Browser.RefreshPage();
            }

            Browser.Wait.ForDomReady();

            DeleteWishList();

            Browser.RefreshPage();
        }

        public virtual void RemoveAllWishListItems()
        {
            var wishListItemCount = Convert.ToInt32(Browser.ExecuteJs("return lp.globals.wishlistItemCount"));

            if (Browser.PageUrl != Urls.WishListPageUrl)
            {
                Browser.Navigate(Urls.WishListPageUrl);
                Browser.RefreshPage();
            }

            for (var i = wishListItemCount - 1; i >= 0; i--)
            {
                if (i != 0) { Browser.RefreshPage(); }
                Browser.Wait.ForDisplayedElement(RemoveProductLink(i));
                RemoveProductLink(i).Click();
            }
        }

        public virtual void AddToCartByItemIndex(int wishlistItemIndex)
        {
            Browser.Wait.ForDomReady();
            Browser.ClickByJs(WishListAddToCardBtns[wishlistItemIndex]);
        }

        public virtual void AddAllWishlistSkusToCart()
        {
            Browser.Navigate(Urls.WishListPageUrl);
            Browser.Wait.ForPage(Urls.WishListPageUrl);
            WishListAddAllToCartButton.Click();
        }

        public virtual bool SelectWishListItemByName(string text)
        {
            Browser.Wait.AreAllElementsVisible(By.ClassName(WlOpenListItemNameClass),10);

            foreach (var itemName in WishListItemNameElements)
            {
                var actualItemText = TextActions.RegexNoTabsAndNewLines(itemName.Text).Trim();

                if (actualItemText != text) continue;
                Browser.Wait.ForClickableElement(itemName);
                itemName.Click();
                Browser.Wait.IsVisibleElement(By.ClassName(_wlItemOpenBtnClass));
                Browser.ClickByJs(OpenWishListButton);
                Browser.Wait.UntilElementUnloads(OpenWishListButton);
                Browser.Wait.ForDomReady();
                return true;
            }

            return false;
        }

        public bool CompareWishListItems(string originalWishListSkus, string openWishListSkus)
        {
            return originalWishListSkus.SequenceEqual(openWishListSkus);
        }

        public void DeleteWishListItems()
        {
            if (!DeleteWishListButton.IsInitialized) return;
            DeleteWishListButton.Click();
        }

        public IElement GetFreeShippingCallout()
        {
            return WishListFreeShippingCallout;
        }

        public void SelectPencilIcon()
        {
            PencilEditIcon.Click();
        }

        public bool IsWishListPageLoaded(int timeToWait)
        {
            return IsCurrentPage;
        }

        public List<Utilities.ProductModel> GetWishListItemsContent()
        {
            var totalWishListItems = Browser.Locate.ElementsByClassName(WishListProdImgClass).Count;

            var wishListProducts = new List<ProductModel>(); 

            for (var index = 0; index < totalWishListItems; index++) 
            { wishListProducts.Add(new ProductModel(WishListProductImg(index).GetAttribute("alt"), WishListProductImg(index).GetAttribute("data-sku"), WishListProductQuantity(index).GetAttribute("value"), WishListProductImg(index).GetAttribute("data-price"))); }

            return wishListProducts;
        }

        public bool DoesWishListMatchAddedProducts(Dictionary<string, int> addedProducts, List<ProductModel> productsInWishList)
        {
            var doesQuantityMatch = true;
            foreach (var product in productsInWishList)
            {
                if (addedProducts[product.Sku] != product.Quantity)
                {
                    doesQuantityMatch = false;
                    break;
                }
            }

            var selectedSkus = new List<string>(addedProducts.Keys);
            var skusInWishlist = new List<string>();
            var count = productsInWishList.Count;
            var i = 0;
            while ( i < count)
            {
                skusInWishlist.Add((productsInWishList[i].Sku));
                i++;
            }

            var doesSkuMatch = selectedSkus.Count == skusInWishlist.Count && selectedSkus.All(skusInWishlist.Contains);

            return doesQuantityMatch && doesSkuMatch;
        }

        public bool IsWishListEmpty()
        {
            return Browser.Wait.IsInvisibleElement(By.ClassName(WishListProdImgClass));
        }
    }
}