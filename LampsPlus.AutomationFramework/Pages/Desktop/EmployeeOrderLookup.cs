using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/employee-tools/EmployeeOrderLookup.aspx
    /// </summary>
    public class EmployeeOrderLookup : EmployeeOrderLookupBase
    {
        /// <inheritdoc />
        public EmployeeOrderLookup(IBrowser browser) : base(browser) { }
    }
}
