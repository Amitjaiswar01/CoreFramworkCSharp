using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CheckoutWorkflow
{
    public class CheckoutWorkflowMobile : ICheckoutWorkflowMobile
    {
        //Class members

        public CheckoutWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Mobile POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}