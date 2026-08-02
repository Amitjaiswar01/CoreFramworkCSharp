using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search.Visual
{
    public interface ISearchMobileVisual : ISearchMobile
    {
        IElement GetSearchField();
    }
}