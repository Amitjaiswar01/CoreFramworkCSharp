using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.MagicMerchandizer
{
    public class MagicMerchandizerMobile : MagicMerchandizerDesktop, IMagicMerchandizerMobile
    {
        public MagicMerchandizerMobile(IBrowser browser) : base(browser)
        {
        }
    }
}