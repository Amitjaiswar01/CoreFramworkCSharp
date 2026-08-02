using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Pages.Refactored.WishList;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow
{
    public class WishListWorkflowMobile : IWishListWorkflowMobile
    {
        //Class members

        public WishListWorkflowMobile(IBrowser browser, IWishListMobile wishList, ISortMobile sort, IProductDetailMobile productDetail, IAssert assert)
        {
            _browser = browser;
            _wishList = wishList;
            _sort = sort;
            _productDetail = productDetail;
            _assert = assert;
        }

        //Desktop POM and Workflow instances
        private readonly IWishListMobile _wishList;
        private readonly ISortMobile _sort;
        private readonly IProductDetailMobile _productDetail;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Interface implementation

        public void AddSingleItemToWishList()
        {
            _sort.SelectSingleProduct(Urls.ContemporaryFloorLampsSortPageUrl);

            _assert.True(_productDetail.IsCurrentPage, "Current page is not ProductDetail page");

            _browser.ScrollIntoView(_productDetail.GetWishListButton());
            _browser.ExecuteJs("window.scrollBy(0,-200)"); //Puts the button far enough down so that the sticky header does not interfere with the click.

            _browser.ClickOnButtonMultipleTimes(_productDetail.GetWishListButton(), 5, _wishList.IsWishListPageLoaded);

            _assert.True(_wishList.IsCurrentPage, "Current page is not WishList page");
        }
        
        public List<string> AddMultipleAvailableItemsToWishList(string url, int numberOfProducts)
        {
            var skus = new List<string>();
            var index = 0;
            while (numberOfProducts > 0)
            {
                _browser.Navigate(url);
                _assert.True(_sort.IsCurrentPage, "User is not on the Sort page.");
                _sort.SelectSortPageSkuByIndex(index);
                numberOfProducts--;
                _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");

                if (_productDetail.IsAddToCartButtonVisible)
                {
                    skus.Add(_productDetail.GetProductSku());
                    _productDetail.AddToWishList();
                    _assert.True(_wishList.IsCurrentPage, "User is not on the Wish List.");
                }
                else
                {
                    _browser.Navigate(url);
                    _assert.True(_sort.IsCurrentPage, "User is not on the Sort page.");
                    _sort.SelectSortPageSkuByIndex(index + 1);
                    _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");
                    skus.Add(_productDetail.GetProductSku());
                    _productDetail.AddToWishList();
                    _assert.True(_wishList.IsCurrentPage, "User is not on the Wish List.");
                }
                index++;
            }
            return skus;
        }
    }
}