using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/employee-tools/default.aspx
    /// </summary>
    public class EmployeeTools: EmployeeToolsBase
    {
        /// <inheritdoc />
        public EmployeeTools(IBrowser browser) : base(browser) { }
    }
}
