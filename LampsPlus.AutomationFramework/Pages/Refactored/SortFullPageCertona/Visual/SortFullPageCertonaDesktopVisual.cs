using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona.Visual
{
    public class SortFullPageCertonaDesktopVisual : SortFullPageCertonaDesktop, ISortFullPageCertonaDesktopVisual
    {
        public SortFullPageCertonaDesktopVisual(IBrowser browser) : base(browser)
        {

        }

        public IElement IgnoreSimilarDesignsContainer()
        {
            return FullPageCertonaSimilarDesignsContainer;
        }
    }
}
