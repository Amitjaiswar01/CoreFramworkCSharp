using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.GoogleAnalyticsWorkflow
{
    public interface IGoogleAnalyticsWorkflowMobile
    {
        void ValidateAbTestGaData(List<Dictionary<string, string>> sortAbTestInfo, string sortPath, int reps);

        Dictionary<string, string[]> GetAndFormatUtagData();
    }
}