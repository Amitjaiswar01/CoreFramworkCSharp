using Automation.Framework;
using LampsPlus.AutomationFramework.Pages;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Workflow base file.
    /// </summary>
    public class WorkflowBase
    {
        public WorkflowBase(TestsBase testsBase) { TestsBase = testsBase; }

        internal TestsBase TestsBase { get; }

        internal IBrowser Browser => TestsBase.Browser;

        internal IScreenCapturer ScreenCapturer => TestsBase.ScreenCapturer;

        internal IGlobalLocators GlobalLocators => TestsBase.GlobalLocators;
    }
}
