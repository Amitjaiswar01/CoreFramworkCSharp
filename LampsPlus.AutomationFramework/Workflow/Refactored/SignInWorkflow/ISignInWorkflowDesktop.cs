namespace LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow
{
    public interface ISignInWorkflowDesktop
    {
        void SignInAndClearSession(string userName, string password);
    }
}