using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SortWorkflow
{
    public class SortWorkflowDesktop : ISortWorkflowDesktop
    {
        //Class members

        public SortWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}