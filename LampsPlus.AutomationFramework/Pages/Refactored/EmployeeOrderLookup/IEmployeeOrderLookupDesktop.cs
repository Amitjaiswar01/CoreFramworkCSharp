using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.EmployeeOrderLookup
{
    public interface IEmployeeOrderLookupDesktop : IPageObjectModel
    {
        void NavigateToMyOrderPage();
        void OpenEmployeeEmailDropDown();
        void OpenEmployeeStoreDropdown();
        void LocatePastOrders(OrderIdModel order);
    }
}