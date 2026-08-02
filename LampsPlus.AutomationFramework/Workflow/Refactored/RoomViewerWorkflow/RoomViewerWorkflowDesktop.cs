using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.RoomViewerWorkflow
{
    public class RoomViewerWorkflowDesktop : IRoomViewerWorkflowDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected ProductActions ProductActions;

        public RoomViewerWorkflowDesktop(IBrowser browser, IProductDetailDesktop productDetail, IAssert assert,
            IRoomViewerDesktop roomViewer )
        {
            _browser = browser;
            _assert = assert;
            _productDetail = productDetail;
            _roomViewer = roomViewer;
        }

        //Desktop POM and Workflow instances
        private readonly IRoomViewerDesktop _roomViewer;
        private readonly IProductDetailDesktop _productDetail;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;


        //Interface implementation
        public void AddMultipleItemsToRoom(List<ArProductModel> ArSku)
        {
            var count = 0;
            foreach (var option in ArSku)
            {

                if (count != 0)
                {
                    _browser.NavigateToPdp(option.ShortSku);
                    _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");
                    _productDetail.AddMultipleProductsToRoom();
                    _roomViewer.ARPageLoad();
                    _assert.True(_roomViewer.IsCurrentPage, "User is not on Room Viewer page.");
                }
                else
                {
                    _browser.NavigateToPdp(option.ShortSku);
                    _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");
                    _productDetail.NavigateToArPage();
                    count++;
                }
            }
        }

        public void AddSingleProductToRoom(string ShortSku)
        {
            _productDetail.NavigateToProductDetailByShortSku(ShortSku);
            _productDetail.NavigateToArPage();
            _assert.True(_roomViewer.IsCurrentPage, "This is not Ar Page");
        }
    }
}
