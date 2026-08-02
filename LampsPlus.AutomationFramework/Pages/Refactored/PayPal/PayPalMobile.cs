using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.PayPal
{
    public class PayPalMobile : PayPalDesktop, IPayPalMobile
    {
        public PayPalMobile(IBrowser browser) : base(browser)
        {
        }
    }
}