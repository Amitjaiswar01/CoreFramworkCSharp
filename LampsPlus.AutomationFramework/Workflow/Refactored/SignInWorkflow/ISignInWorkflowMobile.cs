namespace LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow
{
    public interface ISignInWorkflowMobile
    {
        void SignInAndClearSession(string userName, string password);
    }
}
