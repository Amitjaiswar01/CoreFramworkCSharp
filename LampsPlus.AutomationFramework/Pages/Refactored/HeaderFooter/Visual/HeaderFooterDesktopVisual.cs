using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter.Visual
{
    public class HeaderFooterDesktopVisual : HeaderFooterDesktop, IHeaderFooterDesktopVisual
    {
        public HeaderFooterDesktopVisual(IBrowser browser, IAssert assert, IModalDesktop modal) : base(browser, assert, modal) { }

        public IElement IgnoreInfoBarInHeader()
        {
            return LpHeader;
        }
    }
}
