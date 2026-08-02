using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SearchWorkflow
{
    public interface ISearchWorkflowMobile
    {
        void SearchRandomTerm(List<string> randomTerms);
        void SearchForMultipleRandomProducts(List<string> products);
    }
}