using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort.Visual
{
    public interface ISortMobileVisual : ISortMobile
    {
        List<IElement> IgnoreCertonaAndLpContainer();
        List<IElement> IgnoreFooterContainer();
        IElement IgnoreLpContainer();
        IElement IgnoreRecentlyViewedContainer();
    }
}