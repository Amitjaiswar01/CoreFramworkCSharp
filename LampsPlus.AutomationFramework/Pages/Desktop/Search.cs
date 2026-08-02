using System;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Search feature located on the header.
    /// </summary>
    public class Search : SearchBase
    {
        #region CSS Selector Strings
        private string SearchBtnId { get; } = "searchBtn";

        public override string SearchXpath => "//*[@id='search']";
        public override string SearchSubmitClass => "searchContainer";
        public override string GlobalSearchFieldId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement SearchButton => Browser.Locate.ElementByXpath("//*[@id='searchBtn']");
        public override IElement SearchField => Browser.Locate.ElementByXpath(SearchXpath);
        #endregion

        /// <inheritdoc />
        public Search(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }
    }
}
