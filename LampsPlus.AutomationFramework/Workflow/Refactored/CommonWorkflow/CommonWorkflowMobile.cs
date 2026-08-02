using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CommonWorkflow
{
    public class CommonWorkflowMobile : ICommonWorkflowMobile
    {
        //Class members

        public CommonWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Mobile POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}