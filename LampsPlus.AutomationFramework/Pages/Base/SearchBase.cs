using Automation.Framework;
using OpenQA.Selenium;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class SearchBase : Page, ISearch
    {
        /// <inheritdoc />
        protected SearchBase(IBrowser browser, TestsBase testsBase) : base(browser) { }

        #region CSS Selector Strings
        public string PacTargetInputClass { get; } = "pac-target-input";

        public abstract string SearchSubmitClass { get; }
        public abstract string GlobalSearchFieldId { get; }
        public abstract string SearchXpath { get; }
        #endregion

        #region Page Elements
        public abstract IElement SearchButton { get; }
        public abstract IElement SearchField { get; }
        #endregion

        /// <inheritdoc />
        public void ClearSearchFieldText()
        {
            var searchField = SearchField; //Added to avoid stale element exception.
            searchField.Clear();
        }

        /// <inheritdoc />
        public virtual void ExecuteSearch(string searchTerm)
        {
            Browser.Wait.ForElement(SearchField);
            ClearSearchFieldText();
            SearchField.SendKeys(searchTerm);
            Browser.Wait.IsVisibleElement(By.ClassName(SearchSubmitClass));
            Browser.ClickByJs(SearchButton);
        }
	}
}
