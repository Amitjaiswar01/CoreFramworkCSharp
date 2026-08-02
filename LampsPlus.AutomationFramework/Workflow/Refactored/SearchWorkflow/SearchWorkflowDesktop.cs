using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SearchWorkflow
{
    public class SearchWorkflowDesktop : ISearchWorkflowDesktop
    {
        //Class members

        public SearchWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}