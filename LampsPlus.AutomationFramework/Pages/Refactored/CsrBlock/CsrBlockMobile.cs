using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock
{
    public class CsrBlockMobile : CsrBlockDesktop, ICsrBlockMobile
    {
        public CsrBlockMobile(IBrowser browser) : base(browser) { }
    }
}
