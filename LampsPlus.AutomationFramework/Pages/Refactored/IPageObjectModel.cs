using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored
{
    public interface IPageObjectModel
    {
        string PageTitle { get; }
        string PageUrl { get; }
        bool IsCurrentPage { get; }
    }
}