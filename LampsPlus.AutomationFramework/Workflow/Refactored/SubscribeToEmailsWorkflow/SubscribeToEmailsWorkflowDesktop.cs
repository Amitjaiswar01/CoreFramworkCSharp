using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SubscribeToEmailsWorkflow
{
    public class SubscribeToEmailsWorkflowDesktop : ISubscribeToEmailsWorkflowDesktop
    {
        //Class members

        public SubscribeToEmailsWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}