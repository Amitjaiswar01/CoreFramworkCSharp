using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.RoomViewerWorkflow
{
    public class RoomViewerWorkflowMobile : IRoomViewerWorkflowMobile
    {
        //Class members

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected ProductActions ProductActions;

        public RoomViewerWorkflowMobile(IBrowser browser, IProductDetailMobile productDetail, IAssert assert,
            IRoomViewerMobile roomViewer)
        {
            _browser = browser;
            _assert = assert;
            _productDetail = productDetail;
            _roomViewer = roomViewer;
        }

        //Desktop POM and Workflow instances
        private readonly IRoomViewerMobile _roomViewer;
        private readonly IProductDetailMobile _productDetail;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Interface implementation
        public void ConfirmRoomViewerModal()
        {
            int maxIteration = 10; int iterationsCount = 0;
            bool roomViewerDisplayCheck = false;
            while (iterationsCount < maxIteration && !roomViewerDisplayCheck)
            {
                _browser.RefreshPage();
               _productDetail.ClickOnViewInYourRoom();

                try
                {
                    _assert.True(_roomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");
                    roomViewerDisplayCheck = true;
                }
                catch
                {
                    roomViewerDisplayCheck = false;
                }
                iterationsCount++;
            }

            if (roomViewerDisplayCheck == false)
            {
                _productDetail.ClickOnViewInYourRoom();
                Assert.True(_roomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");
            }
        }
    }
}
