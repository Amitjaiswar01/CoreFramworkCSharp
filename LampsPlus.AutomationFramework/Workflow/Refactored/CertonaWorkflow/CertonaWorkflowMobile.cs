using System.Linq;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CertonaWorkflow
{
    public class CertonaWorkflowMobile : ICertonaWorkflowMobile
    {
        public CertonaWorkflowMobile(IBrowser browser, IProductDetailMobile productDetail, ProductActions productActions, IAssert assert)
        {
            _browser = browser;
            _productDetail = productDetail;
            _productActions = productActions;
            _assert = assert;
        }

        //Mobile POM and Workflow instances
        private readonly IProductDetailMobile _productDetail;
        private readonly ProductActions _productActions;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Interface implementation
        public void VisitMultiplePages(int numberOfPages)
        {
            var randomSkus = _productActions.GetListableInStockShortSku(numberOfPages);
            _assert.True(randomSkus.Any(), "ProductActions.GetListableInStockShortSku(numberOfPages)");

            foreach (var sku in randomSkus)
            {
                _productDetail.NavigateToProductDetailByShortSku(sku);
                _browser.ScrollToBottomOfPageJs();
            }
        }
    }
}
