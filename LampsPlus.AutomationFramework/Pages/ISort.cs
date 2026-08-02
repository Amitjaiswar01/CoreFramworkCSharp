using System.Collections.Generic;
using System.Collections.ObjectModel;

using Automation.Framework;
using Automation.Framework.Utilities;

using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface ISort
    {
        #region Class Setup
        string OverlayContentWrapperCloseButtonClass { get; }
        string AvailableAtThisLocationId { get; }
        string BreadCrumbXpath { get; }
        string ColorString { get; }
        string MoreFiltersLinkXpath { get; }
        string SizeString { get; }
        string TypeString { get; }
        string SpecialsString { get; }
        string FreeShippingString { get; }
        string FreeShippingUrlFragmentString { get; }
        string BrandString { get; }
        string UsageString { get; }
        string FinishString { get; }
        string StyleString { get; }
        string HeightString { get; }
        string PriceString { get; }
        string SaleString { get; }
        string SoldOutString { get; }
        string CategoryString { get; }
        string DailyString { get; }
        string LeftString { get; }
        string BreadCrumbClass { get; }
        string SuggestedProductsContainerClass { get; }
        string MymlSectionId { get; }
        string RecentlyViewedItemClass { get; }
        string SplashImageClass { get; }
        string SortResultImgContainerClass { get; }
        string SortFilterAttributeGroupClass { get; }
        string SortFilterAttributeValueNameClass { get; }
        string SortFilterDisplaySetDrawerButtonGroupClass { get; }
        string SortFilterAppliedFilterFilterValueClass { get; }
        string ProductTitleClass { get; }
        string ProductTitleClassMobileSelector { get; }
        string TopFilterMenuId { get; }
        string TopFilterMenuXpath { get; }
        string TopFilterMenuClass { get; }
        string FilterAttributeParentElement { get; }
        string SaleSearchFieldId { get; }
        string SearchBtnSalePageId { get; }
        string SortFilterAppliedFiltersClass { get; }
        string SortFilterAppliedFiltersCollapsibleClass { get; }
        string SortFilterCategoryClass { get; }
        string SortFilterDisplaySetDropdownsClass { get; }
        string SortFilterGenericHeaderClass { get; }
        string SortFilterWrapperId { get; }
        string SortItemAddToCartButtonClass { get; }
        string SortMoreLikeThisBtnClass { get; }
        string SortPageH1TagId { get; }
        string SortFilterDropdownContentClass { get; }
        string SortMenuId { get; }
        string ProductPriceClass { get; }
        string CalloutWrapperClass { get; }
        string HeaderClass { get; }
        string JsCertonaTitleClass { get; }
        string ToggleSortMenuClass { get; }
        string SortFilterButtonTriggerClass { get; }
        string SortFilterButtonTriggerSelector { get; }
        string SortNormalClass { get; }
        string CollapsibleDisclosureClass { get; }
        string FreeShippingCallOut { get; }
        string LpmmAccordionSubMenu { get; }
        string LpmmMenuOpenClass { get; }
        string LpmmSubMenuClass { get; }
        string LpmmSubOpenClass { get; }
        string MoreFiltersBtnClass { get; }
        string MoreLikeThisClass { get; }
        string OpenBoxSearchFieldId { get; }
        string PaginationNextXpath { get; }
        string PaginationPrevClass { get; }
        string PaginationNextClass { get; }
        string AppliedFiltersClass { get; }
        string FilterListElementId { get; }
        string AccordionMenuHeaderClass { get; }
        string RecentlyViewedItemId { get; }
        string ContainsTextSelector { get; }
        string SelectFilterByText { get; }
        string SortResultContainerClass { get; }
        string SortSplashTitleLeftClass { get; }
        string CloseFiltersMenu { get; }
        string FiltersScrollableContainer { get; }
        string IsStickyClass { get; }
        string SortResultProductsId { get; }
        string GiftCardShopNowBtnXpath { get; }
        string ShowMoreClass { get; }
        string SubBreadCrumbClass { get; }
        string UnveilClass { get; }
        string LpContainerId { get; }
        string CertonaContainerClass { get; }
        string SearchBarFilterId { get; }
        string SearchFilterFieldClass { get; }
        string SearchFilterButtonClass { get; }
        string SearchFilterButtonXPath { get; }
        string OpenBoxSearchClass { get; }
        string ProductContainerId { get; }

        #endregion

        #region Page Elements
        IElement  SearchButtonFilter { get; } 
        IElement SearchFilterButton(int index);
        IElement SearchFilterField(int index);
        IElement SearchBarFilter { get; }
        IElement LpContainer { get; }
        IElement ShowMore { get; }
        IElement SubBreadCrumb { get; }
        IElement CertonaContainer { get; }
        IElement AppliedFiltersContainer { get; }
        IElement AvailableAtThisLocationCheckbox { get; }
        IElement CollapsibleFilterContainer { get; }
        IElement ProductDescriptionLinksElement { get; }
        IElement FilterAttributeList { get; }
        IElement FirstDisplayedProductElement { get; }
        IElement FirstDisplayedProductLink { get; }
        IElement FirstProductElementOnSort { get; }
        IElement BreadCrumbElement { get; }
        IElement BrandFilterElement { get; }
        IElement UsageFilterElement { get; }
        IElement BodyElement { get; }
        IElement DailySaleCallout { get; }
        IElement LpDailySaleCalloutElement(int index);
        IElement Unveil(int index);
        IElement LampsPlusChoice { get; }
        IElement DailySaleAndLimitedQtyItem(int index);
        IElement FilterMenu { get; }
        IElement FilterOptionCloseButton { get; }
        IElement FiltersSubMenu { get; }
        IElement FilterSubMenuOpen { get; }
        IElement FilterOverlay { get; }
        IElement GiftCardBalanceSection { get; }
        IElement GiftCardShopNowBtn { get; }
        IElement LandingPageSplashImage { get; }
        IElement SaleItem(int index);
        IElement SortPageH1Tag { get; }
        IElement SortPageImg(int index);
        IElement ProdPriceElement { get; }
        IElement MobileFilterOptions { get; }
        IElement MobileFilterElement { get; }
        IElement NextPageLinkElement { get; }
        IElement PrevPageLinkElement { get; }
        IElement ProdInfo { get; }
        IElement ColorFilterDropdownElement { get; }
        IElement ColorFilterElement { get; }
        IElement ColorFilterFirstElement { get; }
        IElement FinishFilterElement { get; }
        IElement MoreFiltersElement { get; }
        IElement MoreYouMayLikeContainer { get; }
        IElement NumberOfResultsElement { get; }
        IElement FirstAttributeSubmenuElement { get; }
        IElement PriceFilterElement { get; }
        IElement PriceFilterDropdownElement { get; }
        IElement RandomProductElement { get; }
        IElement SaleFilterElement { get; }
        IElement SaleFilterDropdownElement { get; }
        IElement SaleSearchField { get; }
        IElement SearchBtnSalePage { get; }
        IElement SelectedSkuContainer(string sku);
        IElement SizeFilterElement { get; }
        IElement SpecialsFilterElement { get; }
        IElement SpecialsFilterDropdownElement { get; }
        IElement H1BeforeFilters { get; }
        IElement TypeFilterElement { get; }
        IElement StyleFilterElement { get; }
        IElement StyleFilterDropdownElement { get; }
        IElement HeightFilterElement { get; }
        IElement CategoryFilterElement { get; }
        IElement CategoryFilterDropdownElement { get; }
        IElement WidthFilterElement { get; }
        IElement NumberOfLightsFilterElement { get; }
        IElement CustomerRatingFilterElement { get; }
        IElement MinPriceFilterField { get; }
        IElement MaxPriceFilterField { get; }
        IElement NthDisplayedProductElement(int position);
        IElement FilterPriceButtonElement { get; }
        IElement ToggleSortFilterMenuButton { get; }
        IElement ToggleSortFilterMenuCloseButton { get; }
        IElement StickyFilterElement { get; }
        IElement FiltersListElement { get; }
        IElement MoreLikeThisButton { get; }
        IElement OpenBoxSearchElement { get; }
        IElement SortFilterWrapper { get; }
        IElement SortMenu { get; }
        IElement SortPageFilterContainer { get; }
        IElement SortPaginationElement { get; }
        IElement SortPaginationRangeElement { get; }
        IElement SortPaginationCurrentElement { get; }
        IElement ActiveFilterElement { get; }
        IElement MymlItem { get; }
        IElement MymlSection { get; }
        IElement RecentlyViewedItem { get; }
        IElement RecentlyViewedContainer { get; }
        IElement RecentlyViewedSection { get; }
        IElement RecentlyViewed { get; }
        IElement OpenBoxSearchField { get; }
        IElement QtyLeftCallout(int index);

        ReadOnlyCollection<IElement> ListCalloutsElements { get; }
        ReadOnlyCollection<IElement> ProductContainersList { get; }
        ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems { get; }
        ReadOnlyCollection<IElement> DisplayedFilters { get; }
        ReadOnlyCollection<IElement> ListOfAvailableFilters { get; }
        ReadOnlyCollection<IElement> ListOfFilterAttributes { get; }
        ReadOnlyCollection<IElement> ListOfVisibleFilterAttributes { get; }
        ReadOnlyCollection<IElement> ListOfProductsOnSortPage { get; }
        ReadOnlyCollection<IElement> ListOfProductLinksSortPage { get; }
        ReadOnlyCollection<IElement> ListOfSaleProducts { get; }
        ReadOnlyCollection<IElement> ListOfProductContainer { get; }
        ReadOnlyCollection<IElement> ListOfProductInfo { get; }
        List<string> ListOfBreadCrumbTexts { get; }
        ReadOnlyCollection<IElement> ListOfBreadCrumbLinks { get; }
        ReadOnlyCollection<IElement> ListOfBreadCrumbNames { get; }
        ReadOnlyCollection<IElement> ListOfBreadCrumbRemoveLinks { get; }
        ReadOnlyCollection<IElement> ListOfSortResultContainers { get; }
        ReadOnlyCollection<IElement> SaleItems { get; }
        ReadOnlyCollection<IElement> SortResultLinks { get; }
        #endregion

        /// <summary>
        /// Get the first displayed product's SKU.
        /// </summary>
        string FirstProductSku { get; }

        /// <summary>
        /// Get the first displayed product's SKU.
        /// </summary>
        string FirstProductSkuOnSort { get; }

        /// <summary>
        /// Get the first displayed product's price.
        /// </summary>
        string GetFirstProductPrice { get; }

        /// <summary>
        /// Get a product's name.
        /// </summary>
        string GetProductName { get; }

        /// <summary>
        /// Identify which products have the Free Shipping label.
        /// </summary>
        string FreeShippingLabel { get; }

        /// <summary>
        /// Identify the canonical href.
        /// </summary>
        string CanonicalHref { get; }

        /// <summary>
        /// Identify the first attribute filter in the breadcrumb trail.
        /// </summary>
        string GetFirstAttributeFilterHrefValue { get; }

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
                
        /// <summary>
        /// Get the first displayed product's SKU.
        /// </summary>
        string QtyLeftClass { get; }

        List<string> GetBreadCrumbText(int filterLength);

        IBrowser Browser { get; }

        IElement NthDisplayedProductElementForCertonaWidget(int position);
        IElement NthDisplayedProductElementForCertonaWidgetNoDiv(int position);
        IElement SortPaginationNthElement(int position);
        IElement SortResultProducts { get; }
        IElement SortItemAddToCarButtontAtIndex(int index);
        IElement FirstImageOnSort (int index);
        IElement DisplayedProductAtIndex(int index);
        IElement GetSkuContainerElement(string sku);
        IElement DailySkuSortResultContainer(string dailySaleSku);
        IElement GetSortResultSavePriceBySku(string sku);
        IElement SelectedFilter(string filter);
        ReadOnlyCollection<IElement> GetAttributesOfAttributeMenu(IElement attributeMenu);
        ReadOnlyCollection<IElement> ListOfMobileFilterOptions { get; }
        ReadOnlyCollection<IElement> ListOfMobileFilterUrlsByFilterName { get; }
        ReadOnlyCollection<IElement> ListOfFilterOptionsByFilterName(IElement filter);
        ReadOnlyCollection<IElement> ListOfFilterUrlsByFilterName(IElement filter);
        ReadOnlyCollection<IElement> FirstFilterOption(int index);
        ReadOnlyCollection<IElement> ListOfRandomFilterOptions(int index);


        /// <summary>
        /// Search the page for the given sku.
        /// </summary>
        /// <param name="sku">SKU to find on the page.</param>
        void SearchPageForSku(string sku);

        void SearchPageForProductName(string ProductName);

        /// <summary>
        /// Navigate to the given search page.
        /// </summary>
        /// <param name="searchPath">Search path to append ot the the Lamps Plus base URL.</param>
        void NavigateToSpecificSearchPath(string searchPath);

        /// <summary>
        /// Check a list of products for the Sale label.
        /// </summary>
        /// <param name="listOfSaleProducts"></param>
        /// <returns></returns>
        bool HasSaleLabel(IEnumerable<IElement> listOfSaleProducts);

        /// <summary>
        /// Check to verify a SKU is on the Sort page.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        bool DoesSkuExistOnSortPage(string sku);

        /// <summary>
        /// Check to see if a SKU has BOTH the Quantity Left and Daily Sale callout.
        /// </summary>
        /// <returns></returns>
        bool DoesProductHaveQuantityAndDailySaleCallOut();

        /// <summary>
        /// Click the given attribute menu.
        /// </summary>
        /// <param name="attributeName">Attribute name to find and click.</param>
        /// <param name="attributeValue">Attribute value to click after the menu loads.</param>
        void ClickAttributeMenu(string attributeName, string attributeValue);

        /// <summary>
        /// Wait for the requested element and mouse over it when it is found.
        /// </summary>
        /// <param name="element">Element to wait for.</param>
        void WaitForAttributeMenu(IElement element);

        /// <summary>
        /// Click the More Filters element if it is visible on the Sort page.
        /// </summary>
        void ExpandAllFilters();

        /// <summary>
        /// Check if the More Filters element is visible on the Sort page.
        /// </summary>
        /// <returns></returns>
        bool IsMoreFiltersElementVisibleOnSortPage();

        /// <summary>
        /// Check if "Available at this location" check-box is present
        /// </summary>
        /// <returns></returns>
        bool IsAvailableAtThisLocationOnSortPage { get;}

        /// <summary>
        /// Check to see if a product has a Sold Out callout.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        bool DoesProductHaveSoldOutCallOut(string sku);

        /// <summary>
        /// Get a product's name based on its SKU number.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        string GetProductNameBySku(string sku);

        /// <summary>
        /// Get a product's price based on its SKU number.
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        string GetProductPriceBySku(string sku);

        /// <summary>
        /// Identify the last breadcrumb.
        /// </summary>
        /// <returns></returns>
        string LastBreadcrumbText();

        /// <summary>
        /// Randomly clicks on filter attributes, if any present, and returns the filters applied by number of filter attempts
        /// </summary>
        /// <returns>array of information about applied filters</returns>
        Dictionary<string, string>[] ApplyFilters(int numberOfFilters = 1, bool applyFiltersInOrder = false, Dictionary<string, string>[] predefinedFilters = null);

        /// <summary>
        /// Randomly clicks on one filter option when hovered on a filter attribute menu and returns the filter option
        /// </summary>
        /// <returns>information about applied filter option</returns>
        Dictionary<string, string> GetRandomAppliedFilterOption(List<string> list = null);

        /// <summary>
        /// Clicks on filter option in designated option position (default first option) when hovered on the chosen filter attribute menu and returns the filter option
        /// </summary>
        /// <returns>information about applied filter option</returns>
        Dictionary<string, string> GetAppliedFilterOption(IElement filter, int optPosition = 1);

        /// <summary>
        /// Find the links for a list of products on the Sort page.
        /// </summary>
        /// <param name="numberOfProducts"></param>
        /// <returns></returns>
        List<string> FindLinksForGivenNumberOfProductsOnSortPage(int numberOfProducts);

        /// <summary>
        /// Navigates to Filtered sort page by provided sort url and price (filters one penny lower, one penny higher)
        /// </summary>
        /// <param name="breadcrumbSortUrl">Url for sort page retrieved from breadcrumb</param>
        /// <param name="price">Price of desired product to filter to</param>
        void NavigateToPriceFilteredSortPage(string breadcrumbSortUrl, decimal price);

        /// <summary>
        /// Gets quantity left from callout of sku that appears on sort page
        /// </summary>
        /// <param name="sku">Sku that appears on sort page with quantity left callout</param>
        /// <returns>string</returns>
        string GetQuantityLeftForSkuOnSort(string sku);

        /// <summary>
        /// Method to identify a product that has a More Options callout.
        /// </summary>
        string FindMoreOptionsCallout();

        /// <summary>
        /// Gets reference to the anchor link element given the product image inside it.
        /// </summary>
        /// <param name="productSku"></param>
        /// <returns></returns>
        IElement GetProductByDataSkuAttribute(string productSku);

        /// <summary>
        /// Gets reference to a randomly selected product element on the sort page.
        /// </summary>
        /// <returns></returns>
        SortPageProduct GetRandomProduct();

        /// <summary>
        /// Gets reference to a randomly selected product element on the sort page.
        /// </summary>
        /// <returns></returns>
        SortPageProduct GetRandomSortProduct();

        /// <summary>
        /// Gets reference to a randomly selected product element container on the sort page.
        /// </summary>
        /// <returns></returns>
        IElement GetRandomProductContainer();

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Click on a product on the sort page that is over $200 or if one is not found, click on highest priced product. 
        /// </summary>
        /// <returns>Position of product clicked</returns>
        int ClickProductNearOrOverTwoHundredDollars();

        /// <summary>
        /// Opens mobile sort filter menu and waits for it to stop animating.
        /// </summary>
        void OpenMobileSortFilterMenu();

        /// <summary>
        /// Filter option selection
        /// </summary>
        void ClickFilterAttributeMenu(string filter, string attributeFilter);

        void ToggleAppliedFiltersDropdown();

        void ClickFilterButton();

        void ClickFilterButtonAndOpenAppliedFiltersDropdown();

        void ApplyPriceFilter(string minPrice, string maxPrice);

        void WaitForFilterLink();

        void SearchLampsplusChoiceProduct(Databases.Entities.ProductModel product);
        string NavigationToMoreLikeThisPage();
        string NavigationToPdpPageByProduct();
        string ChooseFirstNormalProduct();
    }
}
