using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CommonWorkflow
{
    public class CommonWorkflowDesktop : ICommonWorkflowDesktop
    {
        //Class members

        public CommonWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}