namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior used for global setup workflow.
    /// </summary>
    public interface IGlobalSetupWorkflow
    {
        /// <summary>
        /// Global initialization of framework.
        /// </summary>
        void Setup(bool skipHomePageNavigation = false);
    }
}
