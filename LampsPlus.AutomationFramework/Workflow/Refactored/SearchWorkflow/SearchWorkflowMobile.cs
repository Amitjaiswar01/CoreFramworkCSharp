using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Search;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SearchWorkflow
{
    public class SearchWorkflowMobile : ISearchWorkflowMobile
    {
        //Class members

        public SearchWorkflowMobile(IBrowser browser, ISearchMobile search, ISortMobile sort, IProductDetailMobile productDetail)
        {
            _browser = browser;
            _search = search;
            _sort = sort;
            _productDetail = productDetail;
        }

        //Mobile POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly ISearchMobile _search;
        private readonly ISortMobile _sort;
        private readonly IProductDetailMobile _productDetail;

        //Interface implementation

        public void SearchRandomTerm(List<string> randomTerms)
        {
            foreach (var searchValue in randomTerms)
            {
                _search.SearchForRandomTerm(searchValue);
                _sort.AreSortPageContainersVisible();
            }
        }

        public void SearchForMultipleRandomProducts(List<string> products)
        {
            foreach (var searchValue in products)
            {
                _search.SearchForRandomProduct(searchValue);
                _productDetail.AreRelatedItemsVisible();
            }
        }
    }
}