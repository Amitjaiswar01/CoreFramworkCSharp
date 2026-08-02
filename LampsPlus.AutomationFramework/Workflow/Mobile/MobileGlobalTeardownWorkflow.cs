using LampsPlus.AutomationFramework.Workflow.Desktop;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
	/// <summary>
	/// Mobile behavior used for global teardown workflow.
	/// </summary>
	public class MobileGlobalTeardownWorkflow : GlobalTeardownWorkflow
	{
		public MobileGlobalTeardownWorkflow(TestsBase testsBase) : base(testsBase) { }
	}
}
