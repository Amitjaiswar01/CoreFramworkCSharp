using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Workflow.Refactored.GlobalTeardownWorkflow;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.GlobalTeardownWorkflow
{
    public class GlobalTeardownWorkflowMobile : IGlobalTeardownWorkflowMobile
    {
        //Class members

        public GlobalTeardownWorkflowMobile(IBrowser browser, TestSetup testSetup, Log log, IHomeMobile home)
        {
            _browser = browser;
            _testSetup = testSetup;
            _log = log;
            _home = home;
        }

        //Mobile POM and Workflow instances
        private readonly IHomeMobile _home;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly TestSetup _testSetup;
        private readonly Log _log;

        //Interface implementation
        public void TearDown()
        {
            _log.Header("Begin Teardown");

            if (_testSetup.AccountConfig.ClearStoreInSessionOnTearDown)
            {
                _log.Message("Request to clear store in session");
                _home.EnterStoreInSession("0");
                _log.Message("Store in session cleared");
            }
        }
    }
}