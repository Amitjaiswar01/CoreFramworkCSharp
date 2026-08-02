using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.EmployeeTools
{
    public class EmployeeToolsMobile : EmployeeToolsDesktop, IEmployeeToolsMobile
    {
        public EmployeeToolsMobile(IBrowser browser) : base(browser)
        {
        }
    }
}