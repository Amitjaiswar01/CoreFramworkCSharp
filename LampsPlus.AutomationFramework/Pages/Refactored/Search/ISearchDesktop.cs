using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Search
{
    public interface ISearchDesktop : IPageObjectModel
    {
        void ClearSearchFieldText();
        void EnterSearchTermOnStickyNavigation();
        void EnterSearchTerm(string searchTerm);
        void SearchForMultipleRandomProducts(List<string> products);
        void SearchRandomTerm(List<string> randomTerms);
        void SearchSuggestions();
        void SearchTermHoverOver();
        void ExecuteSearch();
        void ClickRecentlyViewedItemByIndex(int index);
        void OpenSearchBox();
        void ForceSearchDropdownOpen();
        void SearchForSingleSku(string sku);
        void SelectOptionFromSearchDropdown(IElement linkToClick);
        void WaitForUrlToContainFirstFourCharactersOfSearchTerm(string searchTerm);
        void DisplaySearchDropdownOnHomepage();
        string GetSearchModalTopChandelierContent();
        string GetSearchFieldEmptyMessage();
        string GetSearchTerm();
        string GetSearchFieldText();
        string GetRandomSearchTerm();
        string GetParsedListOfTopCategories(string categories);
        string GetTopCategorySearchTerm(int indexOfSearchTerm);
        string GetSearchText(string searchTerm);
        string GetRecentlyViewedItemAttribute(int index, string attribute);
        string GetClearHistoryText();
        string GetViewAllText();
        string GetTopSearchesFromSearchModal();
        string GetSearchTermFromSearchBox();
        bool IsAutoCompleteVisible { get; }
        string GetStickySearchText();
        string GetStickySearchFieldAlignmentText();
        bool IsStickySearchFieldVisible { get; }
        int GetCountOfTopProductSearches();
        IElement GetAutoSuggestDropDownResults(string textToFind);
        IElement GetSearchFieldText(string searchResultText);
        IEnumerable<string> GetRecentSearchTerms();
    }
}
