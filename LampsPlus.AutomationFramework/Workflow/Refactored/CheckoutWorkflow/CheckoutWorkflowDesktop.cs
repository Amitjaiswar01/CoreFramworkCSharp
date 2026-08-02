using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CheckoutWorkflow
{
    public class CheckoutWorkflowDesktop : ICheckoutWorkflowDesktop
    {
        //Class members

        public CheckoutWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}