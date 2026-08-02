using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Workflow.Refactored.GlobalTeardownWorkflow;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.GlobalTeardownWorkflow
{
    public class GlobalTeardownWorkflowDesktop : IGlobalTeardownWorkflowDesktop
    {
        //Class members

        public GlobalTeardownWorkflowDesktop(IBrowser browser, TestSetup testSetup, Log log, IHomeDesktop home)
        {
            _browser = browser;
            _testSetup = testSetup;
            _log = log;
            _home = home;
        }

        //Desktop POM and Workflow instances
        private readonly IHomeDesktop _home;

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