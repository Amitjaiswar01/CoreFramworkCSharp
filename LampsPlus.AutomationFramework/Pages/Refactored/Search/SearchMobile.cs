using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search
{
    public class SearchMobile : SearchDesktop, ISearchMobile
    {
        //Class Members
        private string _searchSubmitClass = "searchSubmit";
        private string _searchIconXpath = "//button[@class='toggleSearch']";
        private string _globalSearchFieldId = "globalSearchField";
        private string _homeLightingHeaderClass  = "homeLighting__header";
        private string _searchOpenClass = "searchOpen";
        private string _ie11wrapperRecentSearchesClass = "#globalSearch>div.ie11-wrapper.recentsearches";
        private string _globalSearchHiddenClass = ".globalSearch.hidden";
        private string _headingSelector = ".heading";
        private string _toggleSortMenuSelector = ".toggleSortMenu";
        private string _focusedGlobalSearchClass = "focusedGlobalSearch";

        private IElement SearchIcon => Browser.Locate.ElementByXpath(_searchIconXpath);
        protected override IElement SearchField => Browser.Locate.ElementBySelector(_globalSearchFieldId.ToCssIdSelector());
        protected override IElement SearchButton => Browser.Locate.ElementByClassName(_searchSubmitClass);
        protected override IElement HomePageHeaderElement => Browser.Locate.ElementBySelector(_homeLightingHeaderClass.ToCssClassSelector());

        private ReadOnlyCollection<IElement> SearchSortH1 => Browser.Locate.ElementsBySelector(_headingSelector);

        private void ToggleEnableSearch()
        {
            if (Browser.Locate.DoesElementExistImmediately(_searchOpenClass.ToCssClassSelector())) return;
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.XPath(_searchIconXpath));
            Browser.ClickWithTapByElementCoordinates(Browser.Locate.ElementByXpath(_searchIconXpath));
        }

        public SearchMobile(IBrowser browser, IAssert assert, ProductActions productActions) : base(browser, assert, productActions){ }


        //Interface implementation
        public override bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_searchSubmitClass.ToCssClassSelector()));
        public bool IsSearchBoxVisible => Browser.Wait.IsVisibleElement(By.CssSelector(_globalSearchFieldId.ToCssIdSelector()));

        public override void ClearSearchFieldText()
        {
            SearchIcon.Click();
            SearchIcon.Click();
            Browser.Wait.ForClickableElement(SearchField).Clear();
        }

        public override void EnterSearchTerm(string searchTerm)
        {
            SearchField.SendKeys(searchTerm);
            Browser.Wait.ForElement(SearchField);
        }

        public override void SearchRandomTerm(List<string> randomTerms)
        {
            foreach (var searchValue in randomTerms)
            {
                ClearSearchFieldText();

                EnterSearchTerm(searchValue);

                Browser.ClickByJs(SearchButton);

                Browser.ScrollToBottomOfPage(Browser.PageUrl);
                Browser.ScrollToTopOfWindow();

                if (SearchSortH1.Count > 0)
                {
                    Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuSelector));
                }
            }
        }

        public override string GetSearchFieldText()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_globalSearchFieldId.ToCssIdSelector()),30);
            Browser.Wait.ForDomReady();
            var value = SearchField.GetAttribute("value");
            return value;
        }

        public override void ExecuteSearch()
        {
            Browser.ClickByJs(SearchButton);
            Browser.Wait.ForDomReady(30);
        }

        public override IEnumerable<string> GetRecentSearchTerms()
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(_homeLightingHeaderClass.ToCssClassSelector()));

            ToggleEnableSearch();
            SearchField.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(RecentSearchesItemClass));
            var listOfRecentlySearchedTerms = ListOfRecentlySearched.Select(x => x.Text);

            return listOfRecentlySearchedTerms;
        }

        public void SearchForRandomTerm(string searchValue)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_globalSearchFieldId.ToCssIdSelector()));
            SearchField.Click();
            SearchField.Clear();
            Browser.Wait.IsVisibleElement(By.CssSelector(_searchSubmitClass.ToCssClassSelector()));
            SearchField.Clear();
            SearchField.SendKeys(searchValue);
            SearchField.SendKeys(Keys.Enter);
            Browser.Wait.ForDomReady();
        }

        public void SearchForRandomProduct(string searchValue)
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(_globalSearchFieldId.ToCssIdSelector()));

            ClearSearchFieldText();
            Browser.Wait.IsVisibleElement(By.CssSelector(_searchSubmitClass.ToCssClassSelector()));

            EnterSearchTerm(searchValue);
            SearchButton.Click();
        }

        public override void SearchSuggestions()
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
            SearchField.Click();
            Browser.Wait.ForDomReady();
            HomePageHeaderElement.Click();
            Browser.Wait.UntilElementDoesntExist(_focusedGlobalSearchClass);
            Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches.hidden').setAttribute('class', 'ie11-wrapper recentsearches')");
        }

        public override void OpenSearchBox()
        {
            Browser.Wait.ForDomReady();
            ToggleEnableSearch();
            Browser.Wait.IsInvisibleElement(By.CssSelector(Ie11WrapperRecentSearchesHiddenClass.ToCssClassSelector()));
        }

        private void ClickSearch()
        {
            Browser.Wait.ForClickableElement(SearchIcon).Click();
            Browser.Wait.ForClickableElement(SearchField).Click();
        }

        public override void ForceSearchDropdownOpen()
        {
            Browser.Wait.ForDomReady();
            ClickSearch();
            Browser.ScrollToBottomOfWindow();
            Browser.ScrollToTopOfWindow();

            if (Browser.Locate.DoesElementExistImmediately(_ie11wrapperRecentSearchesClass))
            {
                Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches').setAttribute('class', 'ie11-wrapper recentsearches hidden')");
                Browser.Wait.ForDomReady();
                Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches.hidden').style.display = 'block'");
            }
            else
            {
                Browser.Wait.ForDomReady();
                Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches.hidden').style.display = 'block'");
            }
        }

        public override void SearchForSingleSku(string sku)
        {
            Browser.Wait.ForDomReady();
            ClearSearchFieldText();
            Browser.Wait.IsVisibleElement(By.CssSelector(_searchSubmitClass.ToCssClassSelector()));

            EnterSearchTerm(sku);
            SearchButton.Click();

            Browser.Wait.ForDomReady();
            ToggleEnableSearch();
            Browser.Wait.IsVisibleElement(By.CssSelector(_searchSubmitClass.ToCssClassSelector()));
        }

        public override void SelectOptionFromSearchDropdown(IElement linkToClick)
        {
            linkToClick.Click();
        }

        public bool IsSearchVisibleOnLandingPage()
        {
            Browser.Wait.ForDomReady();
            return Browser.Locate.DoesElementExistImmediately(_globalSearchHiddenClass);
        }

        public override void DisplaySearchDropdownOnHomepage()
        {
            HomePageHeaderElement.Click();
            Browser.Wait.UntilElementDoesntExist(_focusedGlobalSearchClass);
            Browser.ExecuteJs("document.querySelector('.searchdropdown-root').style.removeProperty('display');");
        }
    }
}
