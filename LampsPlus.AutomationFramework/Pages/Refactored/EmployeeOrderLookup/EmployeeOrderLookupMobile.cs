using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;

namespace LampsPlus.AutomationFramework.Pages.Refactored.EmployeeOrderLookup
{
    public class EmployeeOrderLookupMobile : EmployeeOrderLookupDesktop, IEmployeeOrderLookupMobile
    {
        public EmployeeOrderLookupMobile(IBrowser browser, ProductActions productActions, IAssert assert) : base(browser, productActions, assert)
        {
        }
    }
}