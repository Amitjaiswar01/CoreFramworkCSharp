namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common utility methods.
    /// </summary>
    public abstract class CommonWorkflowBase : WorkflowBase, ICommonWorkflow
    {
		protected CommonWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public abstract void CloseLpModal();

        /// <inheritdoc />
        public abstract void ConfirmDrawer();

        /// <inheritdoc />
        public abstract void CancelDrawer();

        /// <inheritdoc />
        public abstract void WaitForDrawerToStopAnimating();

        /// <inheritdoc />
        public abstract void ConfirmRemoveItemDrawer();
    }
}
