using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort
{
    public interface ISortDesktop : IPageObjectModel
    {
        IBrowser Navigate(string url);
        IBrowser Navigate();
        bool DoesSortPageResultHaveOpenBoxCallout();
        bool DoesSalePageResultHaveSaleCallOut();
        bool DoesSaleSortPageResultHaveSaleOrClearanceCallOut();
        bool DoesSkuExistOnSortPage(string sku);
        bool HasSoldOutCallOut(string sku);
        bool DoesLampsPlusChoiceBadgeDisplay();
        bool IsFreeShippingFilterApplied();
        bool DoesTextColorMatches(string color);
        bool DoesStickyHeaderDisplayOnSort();
        bool IsQtyLeftCalloutPresent { get; }
        bool IsAvailableAtThisLocationCheckboxPresent { get; }
        bool DoesSortHaveQuantityAndDailySaleCallOut { get; }
        bool AreFiltersVisible { get; }
        bool IsPaginationDisplayed { get; }
        bool IsPaginationNextBtnDisplayed { get; }
        bool IsPaginationPrevBtnDisplayed { get; }
        void NavigateToSpecificSearchPath(string searchPath);
        void SelectSingleProduct(string url);
        void NavigateToSearchSortPage(string productName, string shortSku);
        void NavigateToOpenSearchSortPage(string productName, string shortSku);
        void NavigateToSortByProductNameAndCategory(string category, string productName, string shortSku);
        void ClickProductWithQtyLeftCallout();
        void SelectSortPageSkuByIndex(int index);
        void WaitForFilter();
        void SelectFiltersAndAttributes(Dictionary<string, string> filters, List<string> filtersToExclude = null);
        void ExpandAllFilters();
        void SearchPageForSku(string sku);
        string SearchForCategory();
        string SearchOpenBox();
        void NavigateToPriceFilteredSortPage(string breadcrumbSortUrl, decimal price);
        void SelectFirstProductOnSortPage();
        void ExpandSortPageBreadcrumbList();
        void NavigateToGiftCardPdp();
        void ScrollDownToCallout(string callout);
        void ApplyCustomPrice(decimal minPrice, decimal maxPrice);
        void SearchText(IElement searchElement, string searchText);
        void NavigateToPdpFromSortByProductPosition(int positionOfProductOnSort);
        void NavigateToPageNumber(int pageNumber);
        void SearchForRandomCategory(string category);
        void WaitForH1ToHaveSearchTerm(string searchText);
        void SearchLampsPlusChoiceProduct(Databases.Entities.ProductModel product, float price);
        void SelectAvailableAtThisLocationCheckbox();
        void SearchInContextualSearchBarForSort(string searchText);
        int GetVisibleProductsCount();
        int GetProductNearOrOverTwoHundredDollarsPosition();
        int GetQtyLeftValue();
        string GetQuantityLeftForSkuOnSort(string sku);
        string GetBreadcrumbHomeLink();
        string GetQtyLeftCallout();
        string GetQtyLeftCalloutLabel();
        string GetHundredPlusMoreColorsCallout();
        string GetTextColor();
        string GetSaleCallout();
        string GetDisplayedProductAttribute(int index, string attribute);
        string GetProductPriceBySku(string sku);
        string GetProductPriceOfSku(string sku);
        string GetProductNameBySku(string sku);
        string GetSoldOutString();
        string GetSkuWithCallout(string callout);
        string GetMoreOptionsCallout();
        string Get16PlusColorsCallout();
        string Get16PlusColorsCalloutLabel();
        string LpDailySalesUrl { get; }
        string FreeShippingUrlFragmentString { get; }
        string FreeShippingFreeReturnString { get; }
        string CrystalChandeliersUrl { get; }
        string GetProductNameByShortSkuFromDb(string sku);
        string LastBreadcrumbText();
        string FinishString { get; }
        string ColorString { get; }
        string PriceString { get; }
        string TypeString { get; }
        string TableLampsString { get; }
        string FreeShippingString { get; }
        string GetIndividualBreadcrumbNames(int breadcrumbIndex);
        string SearchFilterText(string searchText);
        string GetViewOpenBoxText();
        string GetH1TagText();
        string GetH1TextBeforeAppliedFilters();
        string GetBreadCrumbText(bool removeForwardSlash = true);
        string GetCategory();
        string GetFreeShippingFreeReturnLabel();
        string GetShippingCallOutLabel();
        string GetShippingCallOut();
        string GetDailySaleCallout();
        string GetDailySaleCalloutLabel();
        string GetNonSaleProductFromSort();
        string GetCurrentPageNumber();
        string GetPaginationRange();
        IElement GetSaleSearchField();
        IElement GetSortResultProduct();
        IElement GetBreadCrumbElement();
        IElement GetContextualSearchBar();
        ProductModel GetContentsOf(string sku);
        List<string> GetPageContents();
        List<string> GetLinksForGivenNumberOfProductsOnSortPage(int numberOfProducts);
        ReadOnlyCollection<IElement> GetEntireBreadcrumbTrail();
        ReadOnlyCollection<IElement> GetListOfSaleProducts();
        Dictionary<string, string> GetRandomFilterAndAttributeFromSortPage(List<string> filtersToExclude = null);
        Dictionary<string, string>[] ApplyFilters(int numberOfFilters, bool applyFiltersInOrder = false, Dictionary<string, string> predefinedFilters = null, List<string> filtersToExclude = null);
    }
}
