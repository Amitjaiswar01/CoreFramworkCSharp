using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort.Visual
{
    public interface ISortDesktopVisual : ISortDesktop
    {
        List<IElement> IgnoreCertonaAndLpContainer();
        IElement IgnoreSortResultProduct();
        IElement IgnoreSortPageFilterContainer();
    }
}