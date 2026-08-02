using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Common behavior for account creation.
    /// </summary>
    public class CreateAccountWorkflow : CreateAccountWorkflowBase
    {
        public CreateAccountWorkflow(TestsBase testsBase) : base(testsBase) { }
    }
}
