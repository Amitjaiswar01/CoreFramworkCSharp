using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class EmployeeOrderLookupBase : Page, IEmployeeOrderLookup
    {
        /// <inheritdoc />
        protected EmployeeOrderLookupBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string DdlSearchTypeId { get; } = "DdlSearchType";
        private string DdlStoreNumberId { get; } = "DdlStoreNum";
        private string MainCtlOrderHistoryPagerTopDdlPageNumberId { get; } = "main_ctlOrderHistoryPagerTop_ddlPageNumber";
        private string RbAllEmployeesId { get; } = "RbAllEmployees";
        private string RbMyOrdersId { get; } = "RbMyOrders";
        private string TrTemplateClass { get; } = "trTemplate";
        private string TxtNameSearchId { get; } = "TxtNameSearch";

        public string LbSearchId { get; } = "LbSearch";
        #endregion

        #region Page Elements
        public IElement FirstOrder => Browser.Locate.ElementByClassName(TrTemplateClass);
	    public IElement MyOrdersRadioButton => Browser.Locate.ElementById(RbMyOrdersId);
	    public IElement OrderSearchButton => Browser.Locate.ElementById(LbSearchId);
	    public IElement OrderSearchInput => Browser.Locate.ElementById(TxtNameSearchId);
	    public IElement PaginationDropdown => Browser.Locate.ElementById(MainCtlOrderHistoryPagerTopDdlPageNumberId);
        public IElement SearchTypeDropdown => Browser.Locate.ElementById(DdlSearchTypeId);
        public IElement StoreNumberDropDown => Browser.Locate.ElementById(DdlStoreNumberId);
        public IElement StoreRadioButton => Browser.Locate.ElementById(RbAllEmployeesId);

	    public ReadOnlyCollection<IElement> PaginationDropdownPageOptions => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Option, PaginationDropdown);
        public ReadOnlyCollection<IElement> Orders => Browser.Locate.ElementsByClassName(TrTemplateClass);
        #endregion
    }
}
