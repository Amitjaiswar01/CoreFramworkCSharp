using Automation.Framework;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.CreateAccountWorkflow
{
    public class CreateAccountWorkflowDesktop : ICreateAccountWorkflowDesktop
    {
        //Class members

        public CreateAccountWorkflowDesktop(IBrowser browser)
        {
            _browser = browser;
        }

        //Desktop POM and Workflow instances

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
    }
}