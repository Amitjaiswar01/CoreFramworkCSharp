using Automation.Framework;

using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/billing/.
    /// </summary>
    public class MobileBilling : BillingBase
    {
        /// <inheritdoc />
        public MobileBilling(IBrowser browser, ICustomerInformation customerInformation) : base(browser, customerInformation) { }
    }
}
