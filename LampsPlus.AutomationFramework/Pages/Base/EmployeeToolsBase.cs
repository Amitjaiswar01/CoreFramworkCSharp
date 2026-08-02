using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// https://www.lampsplus.com/employee-tools/default.aspx
    /// </summary>
    public abstract class EmployeeToolsBase : Page, IEmployeeTools
    {
        /// <inheritdoc />
        protected EmployeeToolsBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string BdContentId { get; } = "bdContent";
        private string QuickShortSku1Id { get; } = "quickShortSku1";
        private string QuickAddToCartId { get; } = "quickAddToCart";
        #endregion

        #region Page Elements
        public IElement EmployeeToolBodyElement => Browser.Locate.ElementById(BdContentId);
        public IElement QuickShortSkuElement => Browser.Locate.ElementById(QuickShortSku1Id);
        public IElement QuickAddToCartElement => Browser.Locate.ElementById(QuickAddToCartId);
        #endregion
    }
}
