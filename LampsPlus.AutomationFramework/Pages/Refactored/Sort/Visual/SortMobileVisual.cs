using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort.Visual
{
    public class SortMobileVisual : SortMobile, ISortMobileVisual
    {
        public SortMobileVisual(IBrowser browser, Log log, IMobileDrawer drawer) : base(browser, log, drawer) { }

        public List<IElement> IgnoreCertonaAndLpContainer()
        {
            return new List<IElement> { CertonaContainer, RecentlyViewedContainer };
        }

        public IElement IgnoreRecentlyViewedContainer()
        {
            return RecentlyViewedContainer;
        }

        public List<IElement> IgnoreFooterContainer()
        {
            return new List<IElement> { FooterContainers(1), FooterContainers(2) };
        }

        public IElement IgnoreLpContainer()
        {
            return LpContainer;
        }
    }
}