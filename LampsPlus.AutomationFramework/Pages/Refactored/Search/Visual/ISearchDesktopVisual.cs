using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search.Visual
{
    public interface ISearchDesktopVisual : ISearchDesktop
    {
        IElement GetSearchField();
        IElement IgnoreRecentlyViewedItems();
    }
}