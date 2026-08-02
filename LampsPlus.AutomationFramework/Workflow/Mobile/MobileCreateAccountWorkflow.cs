using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Common behavior for account creation.
    /// </summary>
    public class MobileCreateAccountWorkflow : CreateAccountWorkflowBase
    {
        public MobileCreateAccountWorkflow(TestsBase testsBase) : base(testsBase){}
    }
}
