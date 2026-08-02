using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort.Visual
{
    public class SortDesktopVisual : SortDesktop, ISortDesktopVisual
    {
        public SortDesktopVisual(IBrowser browser, Log log) : base(browser, log)
        {
        }

        public List<IElement> IgnoreCertonaAndLpContainer()
        {
            return new List<IElement> { CertonaContainer, LpContainer };
        }

        public IElement IgnoreSortResultProduct()
        {
            return SortResultProduct;
        }

        public IElement IgnoreSortPageFilterContainer()
        {
            return SortPageFilterContainer;
        }

        public List<IElement> IgnoreFooterContainer()
        {
            return new List<IElement> { FooterContainers(0), FooterContainers(1) };
        }
    }
}