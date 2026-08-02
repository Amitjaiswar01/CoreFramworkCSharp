using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SortWorkflow
{
    public class SortWorkflowMobile : ISortWorkflowMobile
    {
        //Class members

        public SortWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}