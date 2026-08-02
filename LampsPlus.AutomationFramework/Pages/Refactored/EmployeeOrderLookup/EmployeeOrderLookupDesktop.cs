using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Databases.Entities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.EmployeeOrderLookup
{
    public class EmployeeOrderLookupDesktop : IEmployeeOrderLookupDesktop
    {
        //Class members
        private string _myOrderLinkText = "My Orders";
        private string _emailDropdownClass = "ocSearchType";
        private string _storeRadioBtnId = "RbAllEmployees";
        private string _storeDropDownId = "DdlStoreNum";
        private string _findPastOrderClass = "ocSearchInput";
        private string _searchBtnClass = "ocSearchBtn";

        private IElement MyOrderLink => Browser.Locate.ElementByLinkText(_myOrderLinkText);
        private IElement EmailDropDown => Browser.Locate.ElementByClassName(_emailDropdownClass);  
        private IElement StoreRadio => Browser.Locate.ElementById(_storeRadioBtnId);
        private IElement StoreDropDown => Browser.Locate.ElementById(_storeDropDownId); 
        private IElement FindPastOrder => Browser.Locate.ElementByClassName(_findPastOrderClass);
        private IElement SearchBtn => Browser.Locate.ElementByClassName(_searchBtnClass);

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected ProductActions ProductActions;

        public EmployeeOrderLookupDesktop(IBrowser browser, ProductActions productActions, IAssert assert)
        {
            Browser = browser;
            Assert = assert;
            ProductActions = productActions;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public void NavigateToMyOrderPage()
        {
            Browser.Wait.IsVisibleElement(By.LinkText(_myOrderLinkText));
            MyOrderLink.Click();
        }

        public void OpenEmployeeEmailDropDown()
        {
            EmailDropDown.Click();
        }

        public void OpenEmployeeStoreDropdown()
        {
            StoreRadio.Click();
            StoreDropDown.Click();
        }

        public void LocatePastOrders(OrderIdModel order)
        {
            FindPastOrder.SendKeys(order.UserName);
            SearchBtn.Click();
        }
    }
}