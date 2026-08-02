using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Pages.Refactored.WishList;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow
{
    public class WishListWorkflowDesktop : IWishListWorkflowDesktop
    {
        //Class members

        public WishListWorkflowDesktop(IBrowser browser,  IWishListDesktop wishList, ISortDesktop sort,IProductDetailDesktop productDetail, IAssert assert, IHeaderFooterDesktop headerFooter)
        {
            _browser = browser;
            _sort = sort;
            _productDetail = productDetail;
            _wishList = wishList;
            _assert = assert;
            _headerFooter = headerFooter;
        }

        //Desktop POM and Workflow instances
        private readonly IWishListDesktop _wishList;
        private readonly ISortDesktop _sort;
        private readonly IProductDetailDesktop _productDetail;
        private readonly IHeaderFooterDesktop _headerFooter;
        
        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;


        //Interface implementation
        public void AddSingleItemToWishList()
        {
            var cachedWishlistCount = _wishList.WishListItemsCount;

            _sort.SelectSingleProduct(Urls.ContemporaryFloorLampsSortPageUrl);

            _assert.True(_productDetail.IsCurrentPage, "Current page is not ProductDetail page");

            _productDetail.AddToWishList();

            bool DidWishlistIncrease() => _wishList.WishListItemsCount > cachedWishlistCount;

            _browser.Wait.ForCondition(DidWishlistIncrease, 10);

            _browser.Navigate(Urls.WishListPageUrl);
        }

        public void AddToWishlistAndVerifyCount()
        {
            var cachedWishlistCount = _wishList.WishListItemsCount;
            _productDetail.AddToWishList();
            bool DidWishlistIncrease() => _wishList.WishListItemsCount > cachedWishlistCount;
            _browser.Wait.ForCondition(DidWishlistIncrease, 10);
        }

        public List<string> AddMultipleItemsToWishList(string url, int numberOfProducts)
        {
            var skus = new List<string>();
            var index = 0;
            while (numberOfProducts > 0)
            {
                _browser.RefreshPage();
                _browser.Navigate(url);

                _sort.SelectSortPageSkuByIndex(index);

                numberOfProducts--;
                _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");
                skus.Add(_productDetail.GetProductSku());
                _productDetail.AddToWishList();

                index++;
            }

            return skus;
        }
    }
}