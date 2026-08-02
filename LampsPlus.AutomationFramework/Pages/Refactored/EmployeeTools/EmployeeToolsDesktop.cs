using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.EmployeeTools
{
    public class EmployeeToolsDesktop : IEmployeeToolsDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public EmployeeToolsDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}