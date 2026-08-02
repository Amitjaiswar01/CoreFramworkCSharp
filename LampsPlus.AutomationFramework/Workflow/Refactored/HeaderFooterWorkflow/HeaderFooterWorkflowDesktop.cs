using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.Search;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.HeaderFooterWorkflow
{
    public class HeaderFooterWorkflowDesktop : IHeaderFooterWorkflowDesktop
    {
        public HeaderFooterWorkflowDesktop(IBrowser browser,ISearchDesktop search,IHeaderFooterDesktop headerFooter, ISortDesktop sort, IAssert assert)
        {
            _browser = browser;
            _headerFooter = headerFooter;
            _search = search;
            _sort = sort;
            _assert = assert;
        }

        //Desktop POM and Workflow instances
        private readonly ISearchDesktop _search;
        private readonly IHeaderFooterDesktop _headerFooter;
        private readonly ISortDesktop _sort;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Interface implementation
        public void SearchExecution()
        {
            _browser.RefreshPage();
            _search.EnterSearchTermOnStickyNavigation();
            _search.ExecuteSearch();
            _assert.True(_sort.IsCurrentPage, "Current page is not Sort page");
            _headerFooter.ScrollToFooter();
        }
    }
}
