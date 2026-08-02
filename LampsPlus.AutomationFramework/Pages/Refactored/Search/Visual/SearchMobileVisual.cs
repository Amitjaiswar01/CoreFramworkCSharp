using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search.Visual
{
    public class SearchMobileVisual : SearchMobile, ISearchMobileVisual
    {
        public SearchMobileVisual(IBrowser browser, IAssert assert, ProductActions productActions) : base(browser, assert, productActions) { }

        public IElement GetSearchField()
        {
            return SearchField;
        }
    }
}