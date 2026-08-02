using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search
{
    public class SearchDesktop : ISearchDesktop
    {
        //Class members
        private string _searchXpath = "//*[@id='search']";
        private string _autoCompleteRootSuggestionsClass = "autocomplete-root__suggestions";
        private string _searchFieldEmptyMessage = "Search field is not empty";
        private string _searchTerm = "couch";
        private string _firstRandomSearchTerm = "red table lamp";
        private string _secondRandomSearchTerm = "lightbulb";
        private string _searchBtnId = "searchBtn";
        private string _sortPageH1TagId = "sortPageH1Tag";
        private string _searchBtnXpath = "//*[@id='searchBtn']";
        private string _searchContainerXpath = "//*[@id=\"searchContainer\"]//div[text()='lamps']";
        private string _certonaSearchWidgetContainerItemClass = "certonaSearchWidgetContainer__item";
        private string _searchDropdownRootClearClass = "searchdropdown-root__clear";
        private string _viewAllRecentlyViewedSearchBtnId  = "viewAllRecentlyViewedSearchBtn";
        private string _topSearchContentItemClass = "topSearchContentItem";
        private string _searchDropdownRootSuggestionsClass = "searchdropdown-root__suggestions";
        private string _valueAttribute = "value";
        private string _searchDropdownRootSuggestionClass = "searchdropdown-root__suggestion";
        private string _hpTitleReDesignClass = "hpTitleReDesign";
        private string _searchContainerId = "searchContainer";
        private string _certonaContainerClass = "certonaSearchWidgetContainer__list";

        protected string RecentSearchesItemClass => "recentSearchesItem";
        protected string Ie11WrapperRecentSearchesHiddenClass = "ie11-wrapper.recentsearches.hidden";

        private IElement ViewAllButton => Browser.Locate.ElementById(_viewAllRecentlyViewedSearchBtnId);
        private IElement StickySearch => Browser.Locate.ElementById(_searchContainerId);
        private IElement RecentlyViewedItem(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementsBySelector(_certonaSearchWidgetContainerItemClass.ToCssClassSelector())[index]);
        private IElement ClearHistory => Browser.Locate.ElementByClassName(_searchDropdownRootClearClass);
        private IElement TopSearchesModalContent => Browser.Locate.ElementByClassName(_searchDropdownRootSuggestionsClass);
        private IElement TopSearchesDropDown(int index) => Browser.Locate.ElementsByClassName(_topSearchContentItemClass)[index];
        protected IElement AutoSuggestDropDown(int index) => Browser.Locate.ElementByXpath($"(//div[contains(@class,'searchdropdown-root__suggestion-wrapper')]/div)[{index}]");
        protected IElement SearchContainer => Browser.Locate.ElementByXpath(_searchContainerXpath);
        protected IElement SearchDropdownRecentlyViewedItems => Browser.Locate.ElementByClassName(_certonaContainerClass);
        protected virtual IElement SearchField => Browser.Locate.ElementByXpath(_searchXpath);
        protected virtual IElement SearchButton => Browser.Locate.ElementByXpath(_searchBtnXpath);
        protected virtual IElement HomePageHeaderElement => Browser.Locate.ElementBySelector(_hpTitleReDesignClass.ToCssClassSelector());

        private ReadOnlyCollection<IElement> ListOfTopSearchesProducts => Browser.Locate.ElementsByClassName(_topSearchContentItemClass);
        private ReadOnlyCollection<IElement> AutoSuggestDropDownResults => Browser.Locate.ElementsByClassName(_searchDropdownRootSuggestionClass);
        private ReadOnlyCollection<IElement> SearchSortH1 => Browser.Locate.ElementsBySelector(_sortPageH1TagId.ToCssClassSelector());
        protected ReadOnlyCollection<IElement> ListOfRecentlySearched => Browser.Locate.ElementsByClassName(RecentSearchesItemClass);

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected ProductActions ProductActions;

        public SearchDesktop(IBrowser browser, IAssert assert, ProductActions productActions)
        {
            Browser = browser;
            Assert = assert;
            ProductActions = productActions;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public virtual bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.Id(_searchBtnId));
        public bool IsStickySearchFieldVisible => Browser.Locate.DoesElementExistImmediately(_searchContainerId.ToCssClassSelector());

        public string GetSearchFieldEmptyMessage()
        {
            return _searchFieldEmptyMessage;
        }

        public string GetSearchTerm()
        {
            return _searchTerm;
        }

        public string GetRandomSearchTerm()
        {
            var random = new Random();
            var randomSearchTerm = new List<string> { _firstRandomSearchTerm, _secondRandomSearchTerm };
            var index = random.Next(randomSearchTerm.Count);
            return randomSearchTerm[index];
        }

        public bool IsAutoCompleteVisible => Browser.Locate.DoesElementExistImmediately(_autoCompleteRootSuggestionsClass.ToCssClassSelector());

        public virtual string GetSearchFieldText()
        {
            return SearchField.Text;
        }

        public virtual void EnterSearchTerm(string searchTerm)
        {
            Browser.Wait.IsVisibleElement(By.XPath(_searchXpath));
            SearchField.SendKeys(searchTerm);
            Browser.Wait.ForElement(SearchField);
        }

        public virtual void ClearSearchFieldText()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_searchXpath));
            SearchField.Clear();
        }

        public void EnterSearchTermOnStickyNavigation()
        {
            ExecuteSearch();
            SearchField.SendKeys("table lamps");
            Browser.Wait.ForDomReady();
        }

        public string GetStickySearchText()
        {
            Browser.Wait.IsVisibleElement(By.Id(_searchContainerId));
            return SearchField.GetAttribute("value");
        }   

        public string GetStickySearchFieldAlignmentText()
        {
            Browser.Wait.IsVisibleElement(By.Id(_searchContainerId));
            return StickySearch.GetCssValue("justify-content");
        }

        public virtual void SearchRandomTerm(List<string> randomTerms)
        {
            foreach (var searchValue in randomTerms)
            {
                ClearSearchFieldText();

                EnterSearchTerm(searchValue);

                Browser.Wait.IsVisibleElement(By.XPath(_searchBtnXpath));

                Browser.ClickByJs(SearchButton);
                if (SearchSortH1.Count > 0)
                {
                    Browser.Wait.IsVisibleElement(By.CssSelector(".SortFilterDisplaySetDropdowns"));
                }
            }
        }

        public virtual IEnumerable<string> GetRecentSearchTerms()
        {
            SearchField.Click();

            Browser.MouseOverOnElement(SearchField);
            Browser.Wait.IsVisibleElement(By.ClassName(RecentSearchesItemClass));

            var listOfRecentlySearchedTerms = ListOfRecentlySearched.Select(x => x.Text);

            return listOfRecentlySearchedTerms;
        }

        public string GetRecentlyViewedItemAttribute(int index, string attribute)      
        {
            return RecentlyViewedItem(index).GetAttribute(attribute);
        }

        public void ClickRecentlyViewedItemByIndex(int index)
        {
            RecentlyViewedItem(index).Click();
        }

        public string GetClearHistoryText()
        {
            return ClearHistory.GetCssValue("text-decoration");
        }

        public string GetViewAllText()
        {
            return ViewAllButton.GetCssValue("text-decoration");
        }

        public void SearchForMultipleRandomProducts(List<string> products)
        {
            foreach (var searchValue in products)
            {
                ClearSearchFieldText();
                EnterSearchTerm(searchValue);
                
                Browser.Wait.IsVisibleElement(By.XPath(_searchBtnXpath));
                Browser.ClickOnButtonMultipleTimes(SearchButton,3, x => Browser.Wait.IsInvisibleElement(By.ClassName(_searchDropdownRootSuggestionsClass)));
                Browser.Wait.WaitForAjaxComplete();
                Browser.Wait.ForDomReady();
            }
        }

        public virtual void SearchForSingleSku(string sku)
        {
            ClearSearchFieldText();
            EnterSearchTerm(sku);
            ExecuteSearch();
        }

        public virtual void SearchSuggestions()
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
            Browser.ExecuteJs(("document.querySelector('#searchContainer > div.ie11-wrapper.recentsearches.hidden').style.display = 'block'"));
            Browser.Wait.ForDomReady();
        }

        public void SearchTermHoverOver()
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
            Browser.ExecuteJs("document.querySelector('#searchContainer > div.ie11-wrapper.recentsearches.hidden').setAttribute('class', 'ie11-wrapper recentsearches')");
            Browser.Wait.ForDomReady();

            SearchField.Click();
            Browser.Wait.WaitForAjaxComplete();
            var autoSuggestTerm = AutoSuggestDropDown(2);
            Browser.MouseOverOnElementChain(autoSuggestTerm, SearchContainer);
        }

        public virtual void ExecuteSearch()
        {
            SearchButton.Click();
            Browser.Wait.ForDomReady();
        }

        public string GetSearchText(string searchTerm)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(searchTerm);
        }

        public virtual void OpenSearchBox()
        {
            SearchField.Click();
            Browser.Wait.IsInvisibleElement(By.CssSelector(Ie11WrapperRecentSearchesHiddenClass.ToCssClassSelector()));
        }

        public int GetCountOfTopProductSearches()
        {
            return ListOfTopSearchesProducts.Count;
        }

        public string GetSearchModalTopChandelierContent()
        {
            return TopSearchesModalContent.Text.Replace("Top Chandeliers Searches", string.Empty).TrimStart();
        }

        public string GetParsedListOfTopCategories(string categories)
        {
            return categories.Replace(Environment.NewLine, " ");
        }

        public string GetTopCategorySearchTerm(int indexOfSearchTerm)
        {
            return TopSearchesDropDown(indexOfSearchTerm).Text;
        }

        public virtual void ForceSearchDropdownOpen()
        {
            Browser.Wait.ForDomReady();

            ExecuteSearch();

            Browser.ExecuteJs(("document.querySelector('#searchContainer > div.ie11-wrapper.recentsearches.hidden').setAttribute('class', 'ie11-wrapper recentsearches')"));
        }

        public string GetTopSearchesFromSearchModal()
        {
            return TopSearchesModalContent.Text.Replace("Top Searches", string.Empty).TrimStart();
        }

        public string GetSearchTermFromSearchBox()
        {
            return SearchField.GetAttribute(_valueAttribute);
        }

        public IElement GetAutoSuggestDropDownResults(string textToFind)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_searchDropdownRootSuggestionClass.ToCssClassSelector()));
            return AutoSuggestDropDownResults.FirstOrDefault(result => result.Text == textToFind);
        }

        public virtual void SelectOptionFromSearchDropdown(IElement linkToClick)
        {
            if (!Browser.Locate.DoesElementExistImmediately(_searchDropdownRootSuggestionClass))
            {
                Browser.ExecuteJs(("document.querySelector('#searchContainer > div.ie11-wrapper > div.searchdropdown-root > div.searchdropdown-root__suggestions > div.searchdropdown-root__suggestion-wrapper').setAttribute('class', 'searchdropdown-root__suggestion-wrapper')"));
                linkToClick.Click();
            }
            else
            {
                linkToClick.Click();
            }
        }

        public void WaitForUrlToContainFirstFourCharactersOfSearchTerm(string searchTerm)
        {
            switch (searchTerm)
            {
                case "help":
                    Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("help-and-policies"));
                    break;
                case "contact":
                    Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("contact-us"));
                    break;
                case "wishlist":
                    Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("wish-list"));
                    break;
            }
        }

        public virtual void DisplaySearchDropdownOnHomepage()
        {
            HomePageHeaderElement.Click();
            Browser.Wait.IsInvisibleElement(By.CssSelector(_searchDropdownRootSuggestionsClass.ToCssClassSelector()));
            Browser.ExecuteJs("document.querySelector('.ie11-wrapper > div.searchdropdown-root').setAttribute('style', 'display: block !important')");
        }

        public IElement GetSearchFieldText(string searchResultText)
        {
            return AutoSuggestDropDownResults.FirstOrDefault(result => result.Text == searchResultText);
        }
    }
}
