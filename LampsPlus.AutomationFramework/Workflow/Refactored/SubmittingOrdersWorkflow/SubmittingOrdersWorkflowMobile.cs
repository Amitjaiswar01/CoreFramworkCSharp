using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SubmittingOrdersWorkflow
{
    public class SubmittingOrdersWorkflowMobile : ISubmittingOrdersWorkflowMobile
    {
        //Class members

        public SubmittingOrdersWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}