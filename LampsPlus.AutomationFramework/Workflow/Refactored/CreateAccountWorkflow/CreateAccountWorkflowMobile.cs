using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CreateAccountWorkflow
{
    public class CreateAccountWorkflowMobile : ICreateAccountWorkflowMobile
    {
        //Class members

        public CreateAccountWorkflowMobile(IBrowser browser)
        {
            _browser = browser;
        }

        //Mobile POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}