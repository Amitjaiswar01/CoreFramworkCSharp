using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
	/// <summary>
	/// Desktop behavior used for global teardown workflow.
	/// </summary>
	public class GlobalTeardownWorkflow : GlobalTeardownWorkflowBase
	{
		public GlobalTeardownWorkflow(TestsBase testsBase) : base(testsBase) {}
	}
}
