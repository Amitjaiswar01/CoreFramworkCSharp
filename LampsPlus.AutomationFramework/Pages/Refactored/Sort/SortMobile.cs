using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Linq;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort
{
    public class SortMobile : SortDesktop, ISortMobile
    {
        //Class members
        private string _calloutWrapperClass = "tag";
        private string _sortFilterButtonTriggerClass = "setDT-2";
        private string _sortFilterDisplaySetDrawerButtonGroupClass = "SortFilterDisplaySetDrawer__buttonGroup";
        private string _sortFilterAttributeGroupClass = "SortFilterAttributeGroup";
        private string _sortResultContainerClass = "sortResultContainer";
        private string _productTitleClass = "productTitle";
        private string _salePriceAmountClass = "salePriceAmount";
        private string _toggleSortMenuClass = "toggleSortMenu";
        private string _toggleSortMenuXpath = "//div[@id='sortResultProducts']";
        private string _topFilterMenuXpath = "//*[@id='topFilterMenu']//button[contains(@class, 'toggleSortMenu')]";
        private string _filterAttributeParentElement  = "//*[@class='SortFilterDisplaySetDrawer__buttonGroup']";
        private string _sortFilterAttributeValueNameClass  = "SortFilterAttributeValue__name";
        private string _overlayContentWrapperCloseButtonClass  = "Overlay__contentWrapper__closeButton";
        private string _productPriceClass = "productPrice";
        private string _openBoxSearchInputClass = "openBoxSearch__input";
        private string _globalSearchFieldId = "globalSearchField";
        private string _closeFiltersMenu = "//span[contains(@class, 'lpIcon-close02')]";
        private string _searchFilterFieldClass = "adjacentButton";
        private string _searchFilterButtonXPath = "//*[@id='SearchBarFilter']//*[@class='calloutBtn']";
        private string _subBreadCrumbClass = "breadcrumbLevel";
        private string _openBoxSearchClass = "openBoxSearch__submit";
        private string _recentlyViewedContainerId = "recentlyViewedContainer";
        private string _breadCrumbClass = "breadCrumb";
        private string _breadcrumbLevelClass = "breadcrumbLevel";
        private string _headerClass = "heading";
        private string _sortFilterNestedDrawerClass = "SortFilterNestedDrawer";
        private string _sortNormalClass = "normal";
        private string _plaSortStickyFilterHeaderId = "pdpStickyHeader";
        private string _plaTitleLinkClass = "plaTitleLink";
        private string _sfpQuickLookId = "sfpQuicklook";
        private string _moreDetailsId = "moreDetails";
        private string _searchSalePageMobileId = "searchSalePageMobile";
        private string SpecialsString => "Specials";
        private string _productTitleClassMobileSelector  = ".sortResultInnerContainer > a";
        private string _unveilClass = "unveil";
        private string _priceCalloutClass = "priceCallout";
        private string _redClass = "red";
        private string _contextualSearchBarForSortClass = "contextualSearchBarForSort";
        private string _sortCallout2Selector = ".dark";
        private string _qtyLeftCalloutXpath = "//span[contains(text(),'Left')]";
        private string _topFilterMenuId = "topFilterMenu";
        private string _lpIconCloseSelector = ".Overlay__contentWrapper .lpIcon-close02";
        private string _sortSplashTitleLeftClass = "sortSplashTitleLeft";
        private string _paginationOuterWrapperClass = "paginationOuterWrapper";
        private string _paginationPrevClass = "paginationPrev";

        protected override string SortProductNameClass => "productTitle";
        protected override string SortResultProdPriceClass => "productPrice";
        protected override string SearchOpenBoxCallout => Browser.ExecuteJs($"return document.querySelector('#globalSearchField').value").ToString();
        protected override IElement ProdInfo => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Img, _unveilClass);
        protected override IElement PriceAndClearanceCallout(int index) => Browser.Locate.ElementsByClassName(_productPriceClass)[index];
        protected override IElement ContextualSearchBarForSort => Browser.Locate.ElementBySelector(".contextualSearchBarForSort #contextualSearchBarForSort");

        private bool IsFilterDrawerCloseButtonVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.XPath(_closeFiltersMenu));
        }

        private IElement ToggleSortFilterMenuCloseButton => Browser.Locate.ElementByXpath(_closeFiltersMenu);
        private IElement DisplayedMobileDrawerMenu => Browser.Locate.ElementByClassName("lpmmMenu");
        private IElement OpenBoxSearchElement => Browser.Locate.ElementByClassName(_openBoxSearchInputClass);
        private IElement OpenBoxSearchIcon => Browser.Locate.ElementByClassName(_openBoxSearchClass);
        private IElement SortFilterButton => Browser.Locate.ElementBySelector(_sortFilterButtonTriggerClass.ToCssClassSelector());
        private IElement ToggleSortFilterMenuButton => Browser.Locate.ElementBySelector(_toggleSortMenuClass.ToCssClassSelector());
        private IElement SearchButtonFilter => Browser.Locate.ElementByXpath(_searchFilterButtonXPath);
        private IElement PlaDetails => Browser.Locate.ElementById(_moreDetailsId);
        private IElement SearchFilterField(int index) => Browser.Locate.ElementsByClassName(_searchFilterFieldClass)[index];
        private IElement LeftCalloutOnClearance(int index) => Browser.Locate.ElementsByXpath(_qtyLeftCalloutXpath)[index];
        private IElement SortPageImg(int index) => Browser.Locate.ElementByXpath($"//*[@class='imageWrapper ']//img[contains(@data-position, '{index}')]");
        private IElement OpenBoxCallOut => Browser.Locate.ElementByClassName(_priceCalloutClass);
        private IElement HundredPlusCallOut => Browser.Locate.ElementByClassName(_sortNormalClass);
        private IElement QtyLeftCallOut => Browser.Locate.ElementByXpath(_qtyLeftCalloutXpath);
        private IElement FilterMenuCloseIcon => Browser.Locate.ElementBySelector(_lpIconCloseSelector);
        private IElement SortTitle => Browser.Locate.ElementByClassName(_sortSplashTitleLeftClass);
        private IElement PageNumElement(int index) => Browser.Locate.ElementByTagNameAndAttribute(HtmlTextWriterTag.A, AttributeSelectorType.Contains, HtmlTextWriterAttribute.Href, $"page_{index}");
        protected override IElement BreadCrumbElement => Browser.Locate.ElementByClassName(_breadCrumbClass);
        private ReadOnlyCollection<IElement> ListOfMobileFilterOptions => Browser.Locate.ElementsByClassName(_sortFilterAttributeValueNameClass);
        private ReadOnlyCollection<IElement> ListCalloutsElements => Browser.Locate.ElementsBySelector(_calloutWrapperClass.ToCssClassSelector());
        private ReadOnlyCollection<IElement> ListOfSaleProducts => Browser.Locate.ElementsByClassName(_productPriceClass);
        private ReadOnlyCollection<IElement> ListOfBreadCrumbLinks => BreadCrumbElement.FindElements(By.CssSelector(_breadcrumbLevelClass.ToCssClassSelector()));

        protected IElement RecentlyViewedContainer => Browser.Locate.ElementById(_recentlyViewedContainerId);
        protected override IElement H1BeforeFilters => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H1);
        protected override IElement DailySaleAndLimitedQtyItem(int index) => Browser.Locate.ElementsWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString, LeftString)[index];
        protected override IElement DisplayedProductAtIndex(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementsBySelector(_sortResultContainerClass.ToCssClassSelector())[index]);
        protected override IElement MoreFiltersElement => Browser.Locate.ElementByXpath(_topFilterMenuXpath);
        protected override IElement SortPageH1Tag => Browser.Locate.ElementByClassName(_headerClass).FindElement(By.TagName(HtmlTextWriterTag.H1.ToString()));
        protected override IElement SaleSearchField => Browser.Locate.ElementById(_searchSalePageMobileId);
        protected override IElement SoldOutCallout(int index) => Browser.Locate.ElementsBySelector(_sortCallout2Selector)[index];
        protected override IElement LeftCallout(int index) => Browser.Locate.ElementsBySelector(LeftCalloutClass)[index];

        protected override ReadOnlyCollection<IElement> ListOfProductLinksSortPage => Browser.Locate.ElementsBySelector(_productTitleClassMobileSelector);
        protected override ReadOnlyCollection<IElement> ListOfFilterAttributes => Browser.Locate.ElementsByClassName(_sortFilterDisplaySetDrawerButtonGroupClass);
        protected override ReadOnlyCollection<IElement> SortResultLinks => Browser.Locate.ElementsByXpath("//div[@class='sortResultInnerContainer']/a[@href]");
        protected override ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems => Browser.Locate.ElementsWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString, LeftString);
        protected override ReadOnlyCollection<IElement> ListOfBreadCrumbNames => Browser.Locate.ElementsByClassName(_subBreadCrumbClass);
        protected override ReadOnlyCollection<IElement> ListOfProductsOnSortPage => Browser.Locate.ElementsByXpath("//div[@class='sortResultInnerContainer']/a");

        // Filter Display Set IDs. See https://confluence.lampsplus.com:8093/display/WDP/Display+Type+Configurations for more information.
        protected override int SingleSelectFilterDisplaySetId => 24;
        protected override int MultiSelectFilterDisplaySetId => 25;

        protected override void ClickAttributeMenu(string attributeName, string attributeValue, List<string> filtersToExclude = null)
        {
            var filterIndex = GetListOfAvailableFiltersOnSortPage(filtersToExclude).Item1.FirstOrDefault(x => x.Value == attributeName).Key;

            ApplyFilterOption(attributeName, filter =>
            {
                var attributeMenuItems = ListOfMobileFilterOptions;
                return Browser.Wait.ForClickableElement(Browser.Locate.ElementWithText(attributeMenuItems, AttributeSelectorType.Contains, attributeValue));
            });
        }

        protected override string ApplyFilterOption(string filter, Func<IElement, IElement> getFilterOption)
        {
            var filterIndex = GetListOfAvailableFiltersOnSortPage().Item1.FirstOrDefault(x => x.Value == filter).Key;
            var filterId = FilterDisplayTypeId(filterIndex);
            Browser.Wait.IsVisibleElement(By.XPath(_topFilterMenuXpath));

            var button = Browser.Locate.ElementByXpath(_topFilterMenuXpath);
            Browser.ClickByJs(button);

            Browser.Wait.ForElementToStopAnimating(DisplayedMobileDrawerMenu);

            var selectedFilterAttribute = Browser.Locate.ElementByXpath($"{_filterAttributeParentElement}//div[text()='{filter}']");

            Browser.Wait.ForClickableElement(selectedFilterAttribute).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterAttributeGroupClass.ToCssClassSelector()));

            _drawer.WaitForMobileDrawerToLoad();

            var attributeMenu = Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, filter);
            var baseFilter = Browser.Wait.ForClickableElement(attributeMenu, 30);

            //Select filter option
            var filterOptions = getFilterOption(baseFilter);

            //If the filter has the number of results included (e.g. "Contemporary (2534)") using LocatorString strips it out.
            var filterOptionText = filterOptions.LocatorString;
            filterOptions.Click();

            if (filterId == MultiSelectFilterDisplaySetId && FilterDropdownApplyButton.IsInitialized)
            {
                FilterDropdownApplyButton.Click();
            }

            Browser.Wait.UntilElementDoesntExist(_overlayContentWrapperCloseButtonClass);
            Browser.Wait.ForDomReady();

            return filterOptionText;
        }
        
        protected override Tuple<Dictionary<int, string>, Dictionary<int, string>> GetListOfAvailableFiltersOnSortPage(List<string> filtersToExclude = null)
        {
            Browser.Wait.ForDomReady();

            var allFiltersOnSortPage = new Dictionary<int, string>();
            var excludedFilters = new Dictionary<int, string>();
            var totalNumberOfFilters = AvailableFilters;
            var filterIndex = 0;

            //Only use filters with ID 24 or 25. Other filter ID values are set to 'null'. Sale Filter is set up differently and should be avoided and is also set to 'null'
            while (filterIndex < totalNumberOfFilters)
            {
                if ((FilterDisplayTypeId(filterIndex) == SingleSelectFilterDisplaySetId || FilterDisplayTypeId(filterIndex) == MultiSelectFilterDisplaySetId) && (FilterName(filterIndex) == SaleString || FilterName(filterIndex) == PriceString || FilterName(filterIndex)== SpecialsString))
                {
                    allFiltersOnSortPage.Add(filterIndex, "null");
                    excludedFilters.Add(filterIndex, "null");
                }
                else if ((FilterDisplayTypeId(filterIndex) == SingleSelectFilterDisplaySetId) || (FilterDisplayTypeId(filterIndex) == MultiSelectFilterDisplaySetId))
                {
                    allFiltersOnSortPage.Add(filterIndex, FilterName(filterIndex));
                }

                filterIndex++;
            }

            return Tuple.Create(allFiltersOnSortPage, excludedFilters);
        }

        protected override bool DoesBreadcrumbContainSearchText(string searchText)
        {
            return Browser.Wait.ForCondition(() => GetBreadCrumbText(false).Contains(searchText));
        }

        public SortMobile(IBrowser browser, Log log, IMobileDrawer drawer) : base(browser, log)
        {
            _drawer = drawer;
        }
        
        //Instances
        private IMobileDrawer _drawer;

        //Interface implementation
        public IElement GetProductContainerBySku(string sku) => Browser.Locate.ElementBySelector(_calloutWrapperClass.ToCssClassSelector(), GetSkuContainerElement(sku));
        public override string GetProductPriceBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.CssSelector(_salePriceAmountClass.ToCssClassSelector())).Text;
        public override string GetProductPriceOfSku(string sku) => GetSkuContainerElement(sku).FindElement(By.CssSelector(_productPriceClass.ToCssClassSelector())).Text;
        public override string GetProductNameBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.CssSelector(_productTitleClass.ToCssClassSelector())).Text;
        public override bool HasSoldOutCallOut(string sku) => Browser.Locate.ElementWithText(ListCalloutsElements, AttributeSelectorType.Contains, SoldOutString).Displayed;
        public override bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.XPath(_topFilterMenuXpath));
        public override bool IsQtyLeftCalloutPresent => Browser.Wait.IsVisibleElement(By.ClassName(_sortNormalClass));
        public bool IsFilterButtonPresent => Browser.Wait.IsVisibleElement(By.ClassName(_toggleSortMenuClass));
        public bool DoesNumberOfResultsDisplay => Browser.Wait.IsVisibleElement(By.ClassName(ResultClass));

        public override ReadOnlyCollection<IElement> GetEntireBreadcrumbTrail()
        {
            return ListOfBreadCrumbLinks;
        }

        public void CloseMobileSortMenu()
        {
            if (ToggleSortFilterMenuButton.IsInitialized && ToggleSortFilterMenuButton.GetAttribute("aria-expanded") == "true")
            {
                ToggleSortFilterMenuCloseButton.Click();
            }
        }

        public override IElement GetSortResultProduct()
        {
            return Browser.Locate.ElementByXpath(_toggleSortMenuXpath);
        }

        public override int GetVisibleProductsCount()
        {
            var count = ProductContainersList.Count;
            if (count < 2) return 1;
            if (count == 2 && DisplayedProductAtIndex(1).Location.X < 50) return 1;
            if (count > 2 && DisplayedProductAtIndex(2).Location.X > 50) return 3;
            return 2;
        }

        public override string GetQuantityLeftForSkuOnSort(string sku)
        {
            return Browser.Wait.ForElement(
                    Browser.Locate.ElementBySelector($"{sku.ToUpper().ToTagNameAndAttributeCssSelector(HtmlTextWriterTag.Div, "data-sku")} {_sortNormalClass.ToCssClassSelector()}"), 15)
                .Text.Replace("Left", string.Empty);
        }

        public override void SelectFiltersAndAttributes(Dictionary<string, string> filters, List<string> filtersToExclude = null)
        {
            foreach (var filter in filters)
            {
                Browser.Wait.IsVisibleElement(By.XPath(_topFilterMenuXpath));

                ClickAttributeMenu(filter.Key, filter.Value, filtersToExclude);
            }
        }

        public override void ExpandAllFilters()
        {
          MoreFiltersElement.Click();
        }

        public override void WaitForFilter()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterButtonTriggerClass.ToCssClassSelector()), 10);
        }

        public override string SearchFilterText(string searchText)
        {
            Browser.Wait.ForElementToStopAnimating(SearchFilterField(1));

            SearchFilterField(1).Click();
            SearchFilterField(1).SendKeys(searchText);

            SearchButtonFilter.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuClass.ToCssClassSelector()));

            return ListOfBreadCrumbNames[1].Text;
        }

        public void SelectFilter()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuClass.ToCssClassSelector()));

            Browser.ClickOnButtonMultipleTimes(ToggleSortFilterMenuButton, 5, IsFilterDrawerCloseButtonVisible);

            Browser.ScrollIntoView(SearchFilterField(1));
        }

        public override IBrowser Navigate(string url)
        {
            var expectedUrl = PageUrl + url;

            // Navigate to PageUrl
            Browser.Navigate(expectedUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuClass.ToCssClassSelector()));

            return Browser;
        }

        public override bool DoesSortPageResultHaveOpenBoxCallout()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuClass.ToCssClassSelector()));

            return ListOfSaleProducts.Any(x => x.Text.Contains("Open Box"));
        }

        public override string SearchForCategory()
        {
            Browser.Wait.ForDisplayedElement(OpenBoxSearchElement);

            var searchText = GetCategory();
            OpenBoxSearchElement.SendKeys(searchText);
            OpenBoxSearchIcon.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_globalSearchFieldId.ToCssIdSelector()));

            return searchText;
        }

        public override void ClickProductWithQtyLeftCallout()
        {
            Browser.ScrollIntoView(SortFilterButton);
            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterButtonTriggerClass.ToCssClassSelector()));

            QtyLeftCallOut.Click();
        }

        public override List<string> GetLinksForGivenNumberOfProductsOnSortPage(int numberOfProducts)
        {
            Browser.Wait.ForDomReady();

            var productList = SortResultLinks;
            var listOfProductLinks = new List<string>();

            for (var i = 0; i < numberOfProducts; i++)
            {

                var productUrl = productList[i].GetAttribute(HtmlTextWriterAttribute.Href.ToString());
                listOfProductLinks.Add(productUrl);
                Browser.Wait.ForDomReady();

            }

            return listOfProductLinks;
        }

        public override void SelectFirstProductOnSortPage()
        {
            Browser.ClickByJs(SortPageImg(1));
        }

        public override string GetBreadCrumbText(bool removeForwardSlash = true)
        {
            if (removeForwardSlash)
            {
                return BreadCrumbElement.Text.Replace(" /  ", string.Empty);
            }

            return BreadCrumbElement.Text;
        }

        public override string GetH1TagText()
        {
            Browser.Wait.IsInvisibleElement(By.CssSelector(_sortFilterNestedDrawerClass.ToCssClassSelector()));
            return SortPageH1Tag.Text.ToLower();
        }

        public override void SearchForRandomCategory(string category)
        {
            OpenBoxSearchElement.Click();
            OpenBoxSearchElement.SendKeys(category);
            Browser.ClickByJs(OpenBoxSearchIcon);

            Browser.Wait.IsVisibleElement(By.CssSelector(_toggleSortMenuClass.ToCssClassSelector()));
        }

        public bool AreSortPageContainersVisible()
        {
            return Browser.Wait.AreAllElementsVisible(By.CssSelector(_sortResultContainerClass.ToCssClassSelector()));
        }

        public bool DoesStickyHeaderDisplayOnPdp()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_plaSortStickyFilterHeaderId.ToCssIdSelector()));
        }

        public IElement GetProductName()
        {
            return Browser.Locate.ElementByClassName(_plaTitleLinkClass);
        }

        public void AccessPdpThroughPlaProductName()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_plaTitleLinkClass));
            GetProductName().Click();
        }

        public void AccessPdpThroughMoreDetails()
        {
            Browser.Wait.IsVisibleElement(By.Id(_moreDetailsId));
            PlaDetails.Click();
        }

        public override string GetSaleCallout()
        {
            return _priceCalloutClass;
        }

        public bool IsSfpPageLoaded()
        {
            return Browser.Wait.IsVisibleElement(By.Id(_sfpQuickLookId));
        }

        public override string GetQtyLeftCallout()
        {
            return _sortNormalClass;
        }

        public override string GetQtyLeftCalloutLabel()
        {
            return Browser.Locate.ElementByClassName(_sortNormalClass).Text;
        }

        public override string GetDailySaleCallout()
        {
            return _redClass;
        }

        public override string GetDailySaleCalloutLabel()
        {
            return Browser.Locate.ElementByClassName(_redClass).Text.Replace("\r\n", " ");
        }

        public override void ScrollDownToCallout(string callout)
        {
            int maxIteration = int.Parse(PaginationLastDigit.Text);
            int iterationsCount = 0;
            bool calloutDisplayCheck = false;

            while (iterationsCount < maxIteration && !calloutDisplayCheck)
            {
                Browser.Wait.AreAllElementsVisible(By.CssSelector(SortResultProdPriceClass.ToCssClassSelector()));
                try
                {
                    if (callout == "Open Box" || callout == "Sale")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_priceCalloutClass));
                        Browser.ScrollIntoView(OpenBoxCallOut);
                    }
                    else if (callout == "100+ Colors")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_sortNormalClass));
                        Browser.ScrollIntoView(HundredPlusCallOut);
                    }
                    else if (callout == "Left")
                    {
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterButtonTriggerClass.ToCssClassSelector()));
                        Browser.ScrollIntoView(LeftCallout(0), true);
                    }
                    else if (callout == "Sold Out")
                    {
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortCallout2Selector));
                        Browser.ScrollIntoView(SoldOutCallout(0));
                    }
                    else if (callout == "Clearance Left")
                    {
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterButtonTriggerClass.ToCssClassSelector()));
                        Browser.ScrollIntoView(LeftCalloutOnClearance(0));
                    }

                    calloutDisplayCheck = true;
                }
                catch
                {
                    NextPageLinkElement.Click();
                    Browser.Wait.ForDomReady();
                    calloutDisplayCheck = false;
                }

                iterationsCount++;
            }
        }

        public override void NavigateToPdpFromSortByProductPosition(int positionOfProductOnSort)
        {
            SortPageImg(positionOfProductOnSort).Click();
        }

        public override string GetTextColor()
        {
            return Browser.Locate.ElementByClassName(_productPriceClass).GetCssValue("color");
        }

        public override string GetHundredPlusMoreColorsCallout()
        {
            return _sortNormalClass;
        }

        public IElement GetContextualSearchBarForSort()
        {
            return Browser.Locate.ElementByClassName(_contextualSearchBarForSortClass);
        }

        public void CloseFilterMenu()
        {
            Browser.ClickByJs(FilterMenuCloseIcon);
            Browser.Wait.IsVisibleElement(By.ClassName(_sortFilterButtonTriggerClass));
        }

        public override bool DoesStickyHeaderDisplayOnSort()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_topFilterMenuId.ToCssIdSelector()));
        }

        public string GetNumberOfResults()
        {
            Browser.Wait.ForDisplayedElement(NumberOfResults);
            return NumberOfResults.Text.Replace(" results", string.Empty).Replace(",", string.Empty);
        }

        public string GetSortTitleText()
        {
            return SortTitle.Text;
        }

        public override void NavigateToPageNumber(int pageNumber)
        {
            Browser.ScrollIntoView(Browser.Locate.ElementByClassName(_paginationOuterWrapperClass), true);
            Browser.ClickOnButtonMultipleTimes(PageNumElement(pageNumber),3, IsPageLoadedThroughPaginationNumber);
            PageNumElement(pageNumber).Click();
        }
        
        private bool IsPageLoadedThroughPaginationNumber(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(_paginationPrevClass));
        }
    }
}