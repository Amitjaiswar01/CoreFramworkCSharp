using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Search feature located on the header.
    /// </summary>
    public class MobileSearch : SearchBase
    {
        /// <inheritdoc />
        public MobileSearch(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }

        #region CSS Selector Strings
        public override string SearchSubmitClass { get; } = "searchSubmit";
        public override string GlobalSearchFieldId { get; } = "globalSearchField";
        public override string SearchXpath => throw new NotImplementedException();
        #endregion

        #region Page Elements
		public override IElement SearchButton => Browser.Locate.ElementByClassName(SearchSubmitClass);
        public override IElement SearchField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, GlobalSearchFieldId);

        public override void ExecuteSearch(string searchTerm)
        {
            Browser.Wait.IsVisibleElement(By.Id(GlobalSearchFieldId));
            SearchField.Click();
            ClearSearchFieldText();
            SearchField.SendKeys(searchTerm);
            Browser.Wait.IsVisibleElement(By.ClassName(SearchSubmitClass));
            SearchButton.Click();
        }
        #endregion
    }
}
