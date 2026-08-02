using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona.Visual
{
    public class SortFullPageCertonaMobileVisual : SortFullPageCertonaMobile, ISortFullPageCertonaMobileVisual
    {
        public SortFullPageCertonaMobileVisual(IBrowser browser) : base(browser) { }

        public IElement IgnoreSimilarDesignsContainer()
        {
            return FullPageCertonaSimilarDesignsContainer;
        }
    }
}
