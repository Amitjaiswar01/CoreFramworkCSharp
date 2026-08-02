using System.Linq;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CertonaWorkflow
{
    public class CertonaWorkflowDesktop : ICertonaWorkflowDesktop
    {
        public CertonaWorkflowDesktop(IBrowser browser, IProductDetailDesktop productDetail, ProductActions productActions, IAssert assert)
        {
            _browser = browser;
            _productDetail = productDetail;
            _productActions = productActions;
            _assert = assert;
        }

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Desktop POM, Database and Workflow instances
        private readonly IProductDetailDesktop _productDetail;
        private readonly ProductActions _productActions;

        //Interface implementation
        public void VisitMultiplePages(int numberOfPages)
        {
            var randomSkus = _productActions.GetListableInStockShortSku(numberOfPages);
            _assert.True(randomSkus.Any(), "ProductActions.GetListableInStockShortSku(numberOfPages)");

            foreach (var sku in randomSkus)
            {
                _productDetail.NavigateToProductDetailByShortSku(sku);
            }
        }
    }
}
