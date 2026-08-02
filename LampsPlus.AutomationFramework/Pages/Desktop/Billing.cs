using Automation.Framework;

using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
	/// <summary>
	/// https://www.lampsplus.com/secure/cart/billing/.
	/// </summary>
	public class Billing : BillingBase
	{
		/// <inheritdoc />
		public Billing(IBrowser browser, ICustomerInformation customerInformation) : base(browser, customerInformation) { }

    }
}
