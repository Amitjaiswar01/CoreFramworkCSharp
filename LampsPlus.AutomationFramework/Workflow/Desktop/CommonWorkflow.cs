using LampsPlus.AutomationFramework.Workflow.Base;
using System;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
	/// <summary>
	/// Common utility methods for Desktop tests.
	/// </summary>
	public class CommonWorkflow : CommonWorkflowBase
	{
	    /// <inheritdoc />
	    public CommonWorkflow(TestsBase testsBase) : base(testsBase) { }

		/// <inheritdoc />
		public override void CloseLpModal()
		{
			Browser.ExecuteJs($"document.getElementById('{TestsBase.GlobalLocators.LpModalCloseId}').click()");
			Browser.Wait.UntilElementDoesntExist(TestsBase.GlobalLocators.LpModalId);
		}

        /// <inheritdoc />
        public override void CancelDrawer() => throw new NotImplementedException();

        /// <inheritdoc />
        public override void ConfirmDrawer() => throw new NotImplementedException();

	    /// <inheritdoc />
	    public override void WaitForDrawerToStopAnimating() => throw new NotImplementedException();

        /// <inheritdoc />
		public override void ConfirmRemoveItemDrawer() => throw new NotImplementedException();
    }
}
