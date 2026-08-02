using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SubmittingOrdersWorkflow
{
    public class SubmittingOrdersWorkflowDesktop : ISubmittingOrdersWorkflowDesktop
    {
        //Class members

        public SubmittingOrdersWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}