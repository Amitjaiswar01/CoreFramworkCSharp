using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SubscribeToEmailsWorkflow
{
    public class SubscribeToEmailsWorkflowMobile : ISubscribeToEmailsWorkflowMobile
    {
        //Class members

        public SubscribeToEmailsWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}