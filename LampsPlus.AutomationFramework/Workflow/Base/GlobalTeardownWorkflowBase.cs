namespace LampsPlus.AutomationFramework.Workflow.Base
{
	/// <summary>
	/// Common behavior used for global teardown workflow.
	/// </summary>
	public abstract class GlobalTeardownWorkflowBase : WorkflowBase, IGlobalTeardownWorkflow
    {
        protected GlobalTeardownWorkflowBase(TestsBase testsBase) : base(testsBase) { }

		public void TearDown()
		{
			TestsBase.Log.Header("Begin Teardown");

			if (TestsBase.TestSetup.AccountConfig.ClearStoreInSessionOnTearDown)
			{
				TestsBase.Log.Message("Request to clear store in session");
				TestsBase.Home.EnterStoreInSession("0");
				TestsBase.Log.Message("Store in session cleared");
			}
			
		}
	}
}
