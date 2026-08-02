using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort
{
    public class SortDesktop : ISortDesktop
    {
        //Class members
        private string _certonaContainerClass = "certonaSearchWidgetContainer__list";
        private string _sortFilterDisplaySetDropdownsClass = "SortFilterDisplaySetDropdowns";
        private string _sortFilterAttrGroupDropdownXpath = "//div[contains(@class,'SortFilterAttrGroupDropdown')]";
        private string _sortFilterDropdownContentClass = "SortFilterDropdownContent";
        private string _sortResultProductsId = "sortResultProducts";
        private string _sortResultImgContainerClass = "sortResultImgContainer";
        private string _sortResultContainerId = "sortResultContainer";
        private string _sortCallout2Class = "sortCallout2";
        private string _salePriceClass = "salePrice";
        private string _sortResultProdImgClass = "sortResultProdImg";
        private string _sortResultLinkClass = "sortResultLink";
        private string _lpContainerId = "lpContainer";
        private string _moreFiltersBtnClass = "moreFiltersBtn";
        private string _moreFiltersLinkXpath = "//*[@id='sortFilterWrapper']//button[contains(@class, 'moreFiltersBtn')]";
        private string _sortResultProdPriceClass = "sortResultProdPrice";
        private string _openBoxSearchFieldId = "searchOpenBox";
        private string _subBreadCrumbClass = "breadCrumbValueWrapper";
        private string _buttonSecondaryClass = "Button--secondary";
        private string _showMoreBreadCrumbsBtnClass = "showMoreBreadCrumbsBtn";
        private string _showMoreBreadCrumbsBtnTextWrapperClass = "showMoreBreadCrumbsBtn__text-wrapper";
        private string _sortPageH1TagId = "SortPageH1Tag";
        private string _sortSplashTitleLeftClass = "sortSplashTitleLeft";
        private string _paginationNextClass = "paginationNext";
        private string _pipeListClass = "pipeList";
        private string _sortCallout6Class = "sortCallout6";
        private string _luxuryLightingH1TagClass = "pendingh1Text";
        private string _trendH1TagClass = "wideResponsiveSplash__heading";
        private string _sortBreadcrumbClass = "sortBreadCrumb"; 
        private string _sortCallout8Class = "sortCallout8";
        private string _btnAttributeGroupPriceId = "btnAttributeGroup__Price";
        private string _sortFilterMinMaxInputClass = "SortFilterMinMax__input";
        private string _placeholderAttrString = "placeholder";
        private string _sortFilterMinMaxArrowApplyBtnClass = "SortFilterMinMax__arrowApplyBtn";
        private string _breadcrumbHomeClass = "breadcrumbHome";
        private string _sortResultContainerClass = "sortResultContainer";
        private string _sortResultProdNameClass  = "sortResultProdName";
        private string _saleSearchFieldId = "searchSalePage";
        private string _sortSaveShipReturnsClass = "sortSaveShipReturns";
        private string _sortCallout3Class = "sortCallout3";
        private string _sortCallout1Class = "sortCallout1";
        private string _availableInStoreCheckboxXpath = "//*[@for='availableInStoreCheckbox']";
        private string _sortCallout4Class = "sortCallout4";
        private string _sixteenPlusMoreColorsCalloutClass = "sortCallout1";
        private string _hundredPlusMoreColorsCalloutClass = "sortCallout7";
        private string _openBoxCalloutXpath = "//span[text()='Open Box']";
        private string _lastClass = "last";
        private string _saleCalloutXpath = "//span[text()='Sale']";
        private string _salePriceXpath = "//span[@class='salePrice']";
        private string _seachBoxSortClass = "seachBoxSort";
        private string _contextualSearchBarBtnId = "contextualSearchBarBtn";
        private string _leftCalloutClass = "sortCallout5";
        private string _stickyFilterClass = "stickyFilter";
        private string _paginationClass = "pagination";
        private string _currentClass = "current";
        private string _paginationRangeClass = "paginationRange";
        private string _paginationPrevClass = "paginationPrev";
        private string _giftCardShopNowBtnXpath = "//*[contains(text(),'Shop Now')]";

        protected string SoldOutString => "Sold Out";
        protected string DailyString => "DAILY";
        protected string LeftString => "Left";
        protected string ViewOpenBoxText => "View Open Box Items";
        protected string DataUnbxdFacetTypeString => "data-unbxd-facet-type";
        protected string SearchFilterFieldClass => "searchInput";
        protected string SearchFilterButtonClass => "searchArrowBtn";
        protected string SaleString => "Sale";
        protected string BlackString => "Black";
        protected string AccentString => "Accent";
        protected string SpecialsString => "Specials";
        protected string LeftCalloutClass => "sortCallout5";
        protected string LeftCalloutOnClearanceClass => "sortCallout6";
        protected string TwentyFiveToFortyNinePriceString => "$25 - $49";
        protected string ResultClass => "result";
        protected string SortResultContainerLastXpath = "//div[contains(@id,'sortResultContainer')][last()]";
        protected string FilterName(int filterIndex) => Browser.ExecuteJs($"return lp.pageData.sortFilter.managedSortModels[0].AttributeGroups[{filterIndex}].Name;").ToString();
        protected string FilterAttributeName(int filterIndex, int filterAttributeIndex) => Browser.ExecuteJs($"return lp.pageData.sortFilter.managedSortModels[0].AttributeGroups[{filterIndex}].Attributes[0].AttributeValueGroups[0].AttributeValues[{filterAttributeIndex}].Name;").ToString();

        protected virtual string SortResultProdPriceClass => "sortResultProdPrice";
        protected virtual string SortProductNameClass => "sortResultProdName";
 
        protected int FilterDisplayTypeId(int index) => Convert.ToInt32(Browser.ExecuteJs($"return lp.pageData.sortFilter.managedSortModels[0].AttributeGroups[{index}].Attributes[0].AttributeFilterDisplayTypeId;"));
        protected int AvailableFilters => Convert.ToInt32(Browser.ExecuteJs("return lp.pageData.sortFilter.managedSortModels[0].AttributeGroups.length;"));
        protected int AvailableFilterAttributes(int filterIndex) => Convert.ToInt32(Browser.ExecuteJs($"return lp.pageData.sortFilter.managedSortModels[0].AttributeGroups[{filterIndex}].Attributes[0].AttributeValueGroups[0].AttributeValues.length;"));
        protected virtual string SearchOpenBoxCallout => Browser.ExecuteJs($"return document.querySelector('#searchOpenBox').value").ToString(); 

        private bool IsMoreFiltersElementVisibleOnSortPage() => Browser.Locate.ElementImmediately(_moreFiltersBtnClass.ToCssClassSelector()).IsInitialized && Browser.Locate.ElementImmediately(_moreFiltersBtnClass.ToCssClassSelector()).Displayed;
        
        private ReadOnlyCollection<IElement> ListOfProductContainer => Browser.Locate.ElementById(_sortResultProductsId).FindElements(By.ClassName(_sortResultImgContainerClass));
        private ReadOnlyCollection<IElement> ListOfSaleProducts => Browser.Locate.ElementById(_sortResultProductsId).FindElements(By.ClassName(SortResultProdPriceClass));

        private IElement GiftCardShopNowBtn => Browser.Locate.ElementByXpath(_giftCardShopNowBtnXpath);
        private IElement SixteenPlusMoreColorsCallout => Browser.Locate.ElementByClassName(_sixteenPlusMoreColorsCalloutClass);
        private IElement HundredPlusMoreColorsCallout => Browser.Locate.ElementByClassName(_hundredPlusMoreColorsCalloutClass);
        private IElement SearchFilterField(int index) => Browser.Locate.ElementsByClassName(SearchFilterFieldClass)[index];
        private IElement SearchFilterButton(int index) => Browser.Locate.ElementsByClassName(SearchFilterButtonClass)[index];
        private IElement SortResultProducts => Browser.Locate.ElementBySelector(_sortResultProductsId.ToCssIdSelector());
        private IElement SortSearchSku(string sku) => Browser.Locate.ElementBySelector($"{_sortResultContainerId.ToCssIdSelector()}{sku} > {HtmlTextWriterTag.Div}");
        private IElement OpenBoxSearchField => Browser.Locate.ElementById(_openBoxSearchFieldId);
        private IElement PriceFilter => Browser.Locate.ElementById(_btnAttributeGroupPriceId);
        private IElement FilterPriceButton => Browser.Locate.ElementByClassName(_sortFilterMinMaxArrowApplyBtnClass);
        private IElement MinPriceFilterField => Browser.Locate.ElementByClassNameAndAttributeEquals(_sortFilterMinMaxInputClass, _placeholderAttrString, "Min");
        private IElement MaxPriceFilterField => Browser.Locate.ElementByClassNameAndAttributeEquals(_sortFilterMinMaxInputClass, _placeholderAttrString, "Max");
        private IElement LuxuryLightingH1BeforeFilters => Browser.Locate.ElementByClassName(_luxuryLightingH1TagClass);
        private IElement TrendH1BeforeFilters => Browser.Locate.ElementByClassName(_trendH1TagClass);
        private IElement SortBreadcrumb => Browser.Locate.ElementByClassName(_sortBreadcrumbClass);
        private IElement BreadcrumbHomeLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, _breadcrumbHomeClass);
        private IElement AvailableAtThisLocationCheckbox => Browser.Locate.ElementByXpath(_availableInStoreCheckboxXpath);
        private IElement OpenBoxCallout => Browser.Locate.ElementByXpath(_openBoxCalloutXpath);
        private IElement LampsPlusChoiceCallOut => Browser.Locate.ElementByClassName(_sortCallout8Class);
        private IElement SaleCallOut => Browser.Locate.ElementByXpath(_saleCalloutXpath);
        private IElement MoreOptionsCallOut => Browser.Locate.ElementByClassName(_sortCallout3Class);
        private IElement ContextualSearchBar => Browser.Locate.ElementByClassName(_seachBoxSortClass);
        private IElement contextualSearchBarBtn => Browser.Locate.ElementById(_contextualSearchBarBtnId);
        private IElement LeftClearanceCallout(int index) => Browser.Locate.ElementsByClassName(LeftCalloutOnClearanceClass)[index];
        private IElement PaginationRange => Browser.Locate.ElementByClassName(_paginationRangeClass);

        protected IElement NthDisplayedProductElementForCertonaWidget(int position) => Browser.Locate.ElementByXpath($"(//*[contains(@id,'sortResultContainer')])[{position}]");
        protected IElement CertonaContainer => Browser.Locate.ElementByClassName(_certonaContainerClass);
        protected IElement LpContainer => Browser.Locate.ElementById(_lpContainerId);
        protected IElement FilterDropdownApplyButton => Browser.Locate.ElementByClassName(_buttonSecondaryClass);
        protected IElement NextPageLinkElement => Browser.Locate.ElementBySelector(_paginationNextClass.ToCssClassSelector());
        protected IElement SortResultProduct => Browser.Locate.ElementBySelector(_sortResultProductsId.ToCssIdSelector());
        protected IElement SortPageFilterContainer => Browser.Locate.ElementByClassName(_sortFilterDisplaySetDropdownsClass);
        protected IElement FirstDisplayedProductLink => Browser.Locate.ElementByClassName(SortProductNameClass);
        protected IElement FooterContainers(int index) => Browser.Locate.ElementsByClassName(_pipeListClass)[index];
        protected IElement PaginationLastDigit => Browser.Locate.ElementByClassName(_lastClass);
        protected IElement NumberOfResults => Browser.Locate.ElementByClassName(ResultClass);
        protected IElement SortPaginationCurrentElement => Browser.Locate.ElementBySelector($"{_paginationClass.ToCssClassSelector()} {_currentClass.ToCssClassSelector()} {HtmlTextWriterTag.A}");
        protected IElement PaginationNumber(int index) => Browser.Locate.ElementByXpath($"(//ul[@class='pagination']//li)[{index}]");

        protected virtual ReadOnlyCollection<IElement> ListOfProductLinksSortPage => Browser.Locate.ElementsByClassName(_sortResultProdNameClass);
        protected virtual IElement DailySaleAndLimitedQtyItem(int index) => Browser.Locate.ElementsWithText(ListOfProductContainer, AttributeSelectorType.Contains, DailyString, LeftString)[index];
        protected virtual IElement DisplayedProductAtIndex(int index) => Browser.Locate.ElementsByClassName(_sortResultProdImgClass)[index];
        protected virtual IElement GetSkuContainerElement(string sku) => Browser.Locate.ElementBySelector($"{_sortResultContainerId.ToCssIdSelector()}{sku}");
        protected virtual IElement MoreFiltersElement => Browser.Locate.ElementByXpath(_moreFiltersLinkXpath);
        protected virtual IElement FirstDisplayedProductElement => Browser.Locate.ElementByClassName(_sortResultProdImgClass);
        protected virtual IElement BreadCrumbElement => Browser.Locate.ElementByXpath("//*[@class='breadCrumbValueWrapper']/span");
        protected virtual IElement SortPageH1Tag => Browser.Locate.ElementById(_sortPageH1TagId);
        protected virtual IElement H1BeforeFilters => Browser.Locate.ElementBySelector(_sortSplashTitleLeftClass.ToCssClassSelector());
        protected virtual IElement SaleSearchField => Browser.Locate.ElementById(_saleSearchFieldId);
        protected virtual IElement ProdInfo => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Img, _sortResultProdImgClass);
        protected virtual IElement SoldOutCallout(int index) => Browser.Locate.ElementsByClassName(_sortCallout2Class)[index];
        protected virtual IElement PriceAndClearanceCallout(int index) => Browser.Locate.ElementsByXpath(_salePriceXpath)[index];
        protected virtual IElement LeftCallout(int index) => Browser.Locate.ElementsByClassName(LeftCalloutClass)[index];
        protected virtual IElement ContextualSearchBarForSort => Browser.Locate.ElementBySelector(".seachBoxSort #contextualSearchBarForSort");

        protected ReadOnlyCollection<IElement> GetAttributesOfAttributeMenu(IElement attributeMenu) => attributeMenu.FindElements(By.TagName("a"));
        protected ReadOnlyCollection<IElement> ProductContainersList => Browser.Locate.ElementsByClassName(_sortResultContainerClass, SortResultProducts);
        protected ReadOnlyCollection<IElement> GetAttributesOfFilter(IElement attributeMenu) => attributeMenu.FindElements(By.ClassName("SortFilterAttributeValue__name"));
        protected virtual ReadOnlyCollection<IElement> SortResultLinks => Browser.Locate.ElementsBySelector(_sortResultLinkClass.ToCssClassSelector());
        protected virtual ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems => Browser.Locate.ElementsWithText(ListOfProductContainer, AttributeSelectorType.Contains, DailyString, LeftString);
        protected virtual ReadOnlyCollection<IElement> ListOfBreadCrumbNames => Browser.Locate.ElementsByClassName(_subBreadCrumbClass);
        protected virtual ReadOnlyCollection<IElement> ListOfProductsOnSortPage => Browser.Locate.ElementsBySelector("div.sortResultContainer > a");

        // Filter Display Set IDs. See https://confluence.lampsplus.com:8093/display/WDP/Display+Type+Configurations for more information.
        protected virtual int SingleSelectFilterDisplaySetId => 1;
        protected virtual int MultiSelectFilterDisplaySetId => 11;

        private Dictionary<string, string> GetFiltersInCanonicalOrder
        {
            get
            {
                var filtersInCanonicalOrder = new Dictionary<string, string>
                {
                    { FinishString, BlackString },
                    {ColorString, BlackString },
                    {TypeString, AccentString},
                    {PriceString, TwentyFiveToFortyNinePriceString}
                };

                return filtersInCanonicalOrder;
            }
        }

        public IElement GetBreadCrumbElement()
        {
            return BreadCrumbElement;
        }

        public string SearchOpenBox()
        {
            return SearchOpenBoxCallout;
        }

        protected virtual bool DoesBreadcrumbContainSearchText(string searchText)
        {
            return Browser.Wait.IsVisibleElement(By.XPath($"//span[@class='{_subBreadCrumbClass}']/span[ text()='{searchText}']"));
        }

        protected virtual ReadOnlyCollection<IElement> ListOfFilterAttributes
        {
            get
            {
                Browser.Wait.ForDomReady();

                var filterList = Browser.Locate.ElementsByXpath(_sortFilterAttrGroupDropdownXpath);

                var selectedFilters = filterList.Where(attribute => attribute.GetAttribute(DataUnbxdFacetTypeString) != "Sale").ToList();

                return new ReadOnlyCollection<IElement>(selectedFilters);
            }
        }

        protected void WaitForSortPageSku(int index)
        {
            var skuPosition = index;
            Browser.Wait.ForElementToStopAnimating(DisplayedProductAtIndex(skuPosition),15);
        }

        protected virtual void ClickAttributeMenu(string attributeName, string attributeValue, List<string> filtersToExclude = null)
        {
            var filterIndex = GetListOfAvailableFiltersOnSortPage(filtersToExclude).Item1.FirstOrDefault(x => x.Value == attributeName).Key;

            if (FilterDisplayTypeId(filterIndex) == MultiSelectFilterDisplaySetId)
            {
                ApplyFilterOption(attributeName, filter =>
                {
                    var attributeMenuItems = GetAttributesOfFilter(filter);
                    return Browser.Wait.ForClickableElement(Browser.Locate.ElementWithText(attributeMenuItems, AttributeSelectorType.Contains, attributeValue));
                });
            }
            else
            {
                ApplyFilterOption(attributeName, filter =>
                {
                    var attributeMenuItems = GetAttributesOfAttributeMenu(filter);
                    return Browser.Wait.ForClickableElement(Browser.Locate.ElementWithText(attributeMenuItems, AttributeSelectorType.Equals, attributeValue));
                });
            }
        }

        protected virtual string ApplyFilterOption(string filter, Func<IElement, IElement> getFilterOption)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            //Click Filter menu
            var attributeMenu = Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, filter);
            var baseFilter = Browser.Wait.ForClickableElement(attributeMenu, 30);
            var filterIndex = GetListOfAvailableFiltersOnSortPage().Item1.FirstOrDefault(x => x.Value == filter).Key;
            var filterId = FilterDisplayTypeId(filterIndex);
            switch (filter)
            {
                case "Number of Lights":
                    Browser.Locate.ElementByXpath("//button[@id='btnAttributeGroup__Number-of-Lights']/span/div").Click();
                    break;
                case "Bottom Width":
                    Browser.Locate.ElementByXpath("//button[@id='btnAttributeGroup__Bottom-Width']/span/div").Click();
                    break;
                case "Top Width":
                    Browser.Locate.ElementByXpath("//button[@id='btnAttributeGroup__Top-Width']/span/div").Click();
                    break;
                case "Fan Control":
                    Browser.Locate.ElementByXpath("//button[@id='btnAttributeGroup__Fan-Control']/span/div").Click();
                    break;
            }

            Browser.Locate.ElementByXpath($"//button[@id='btnAttributeGroup__{filter}']/span/div").Click();

            //Click Filter option
            Browser.Wait.ForDomReady();
            var filterOption = getFilterOption(baseFilter);
            
            //If the filter has the number of results included (e.g. "Contemporary (2534)") using LocatorString strips it out.
            var filterOptionText = filterOption.LocatorString;
            Browser.Locate.ElementByXpath($"//div[@class='SortFilterAttributeValue__name' and text()='{filterOptionText}']").Click();
            Browser.Wait.ForDomReady();

            if (filterId == MultiSelectFilterDisplaySetId && FilterDropdownApplyButton.IsInitialized)
            {
                FilterDropdownApplyButton.Click();
            }

            Browser.Wait.IsInvisibleElement(By.CssSelector(_sortFilterDropdownContentClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            return filterOptionText;
        }

        protected virtual Tuple<Dictionary<int, string>, Dictionary<int, string>> GetListOfAvailableFiltersOnSortPage(List<string> filtersToExclude = null)
        {
            var allFiltersOnSortPage = new Dictionary<int, string>();
            var excludedFilters = new Dictionary<int, string>();
            var totalNumberOfFilters = AvailableFilters;
            var filterIndex = 0;

            //Only use filters with ID 1 or 11 for Desktop. Other filter ID values are set to 'null'. Sale Filter is set up differently and should be avoided and is also set to 'null'
            while (filterIndex < totalNumberOfFilters)
            {
                if (string.Equals(FilterName(filterIndex), SaleString, StringComparison.OrdinalIgnoreCase) || string.Equals(FilterName(filterIndex), SpecialsString, StringComparison.OrdinalIgnoreCase))
                {
                    //When filter name is "null", add it to allFiltersOnSortPage to maintain index integrity and also add to new dictionary that only includes keys that have a value of "null" for later comparison. 
                    allFiltersOnSortPage.Add(filterIndex, "null");
                    excludedFilters.Add(filterIndex, "null");
                }
                else if ((FilterDisplayTypeId(filterIndex) == SingleSelectFilterDisplaySetId && FilterName(filterIndex) != SaleString) 
                    || (FilterDisplayTypeId(filterIndex) == SingleSelectFilterDisplaySetId && FilterName(filterIndex) != SpecialsString) 
                    || FilterDisplayTypeId(filterIndex) == MultiSelectFilterDisplaySetId)
                {
                    var filterName = FilterName(filterIndex);

                    if (filtersToExclude == null)
                    {
                        allFiltersOnSortPage.Add(filterIndex, filterName);
                    }
                    else
                    {
                        var filterCannnotBeAdded = filtersToExclude.Any(x => x == filterName);

                        if (!filterCannnotBeAdded)
                        {
                            allFiltersOnSortPage.Add(filterIndex, filterName);
                        }
                    }
                }
                else
                {
                    //When filter name is "null", add it to allFiltersOnSortPage to maintain index integrity and also add to new dictionary that only includes keys that have a value of "null" for later comparison. 
                    allFiltersOnSortPage.Add(filterIndex, "null");
                    excludedFilters.Add(filterIndex, "null");
                }

                filterIndex++;
            }

            return Tuple.Create(allFiltersOnSortPage, excludedFilters);
        }

        //Instances
        protected IBrowser Browser;
        protected Log Log;

        public SortDesktop(IBrowser browser, Log log)
        {
            Browser = browser;
            Log = log;
        }

        //Interface implementation
        public string FinishString => "Finish";
        public string ColorString => "Color";
        public string PriceString => "Price";
        public string TypeString => "Type";
        public string LpDailySalesUrl => "ds_daily-savings/";
        public string CrystalChandeliersUrl => "chandeliers/style_crystal/";
        public string FreeShippingUrlFragmentString => "freeshipping_view-free-shipping-items/";
        public string FreeShippingFreeReturnString => "Free Shipping & Free Returns*";
        public string FreeShippingString => "Free Shipping*";
        public string TableLampsString => "Table Lamps";
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/products/";
        public string LastBreadcrumbText() => Browser.Locate.ElementByXpath("//*[@class='sortBreadCrumb']/span[6]").Text.Trim();
        public bool IsFreeShippingFilterApplied() => Browser.Wait.ForCondition(() => SortPageH1Tag.Text.Contains("View Free Shipping"));
        public virtual bool IsQtyLeftCalloutPresent => Browser.Wait.IsVisibleElement(By.ClassName(_sortCallout6Class));
        public bool IsAvailableAtThisLocationCheckboxPresent => Browser.Locate.ElementsByXpath(_availableInStoreCheckboxXpath).Count > 0;
        public bool DoesSortHaveQuantityAndDailySaleCallOut => DailySaleAndLimitedQtyItems.Count > 0;
        public bool AreFiltersVisible => Browser.Wait.IsVisibleElement(By.ClassName(_sortFilterDisplaySetDropdownsClass));
        public bool IsPaginationDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_paginationClass));
        public bool IsPaginationNextBtnDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_paginationNextClass));
        public bool IsPaginationPrevBtnDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_paginationPrevClass));

        public virtual bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
        public virtual bool HasSoldOutCallOut(string sku) => GetSkuContainerElement(sku).FindElements(By.ClassName(_sortCallout2Class)).Any();
        public virtual string GetProductPriceBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.ClassName(_salePriceClass)).Text;
        public virtual string GetProductNameBySku(string sku) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, GetSkuContainerElement(sku)).Text;
        public virtual string GetProductPriceOfSku(string sku) => GetSkuContainerElement(sku).FindElement(By.ClassName(_sortResultProdPriceClass)).Text;

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public virtual IBrowser Navigate(string url)//Method to add page path
        {
            var expectedUrl = PageUrl + url;

            // Navigate to PageUrl
            Browser.Navigate(expectedUrl);

            return Browser;
        }

        public string GetDisplayedProductAttribute(int index, string attribute)
        {
            return DisplayedProductAtIndex(index).GetAttribute(attribute);
        }

        public int GetProductNearOrOverTwoHundredDollarsPosition()
        {
            var pos = 1;
            var maxPricePos = 1;
            double maxPrice = 0;
            const int priceThreshold = 200;

            var productList = ProductContainersList;
            var numOfProducts = ListOfProductLinksSortPage.Count;
            for (var i = 0; i < numOfProducts; i++)
            {
                var productPrice = Convert.ToDouble(productList[i].FindElement(By.TagName(HtmlTextWriterTag.Img.ToString())).GetAttribute("data-price"));

                //if product is over 200 go to that product, no need to continue.
                if (productPrice > priceThreshold)
                {
                    pos = i + 1;
                    NthDisplayedProductElementForCertonaWidget(pos).Click();
                    break;
                }

                //check if this product is more expensive than previously saved prices.
                if (productPrice > maxPrice)
                {
                    maxPrice = productPrice;
                    maxPricePos = i + 1;
                }

                //if we are at the end of the list go to the highest priced item.
                if (i == numOfProducts - 1)
                {
                    pos = maxPricePos;
                    NthDisplayedProductElementForCertonaWidget(pos).Click();
                    break;
                }
            }
            return pos;
        }

        public virtual int GetVisibleProductsCount()
        {
            var visualProductBreakpointYCoordinate = 838;

            var productCount = ProductContainersList.Where((product, index) => {
                var location = DisplayedProductAtIndex(index).Location;
                return location.Y < visualProductBreakpointYCoordinate;
            }).Count();

            Log.Message($"Total count of products is: {productCount}");

            return productCount;
        }

        public virtual void WaitForFilter()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
        }

        public virtual void SelectFiltersAndAttributes(Dictionary<string, string> filters, List<string> filtersToExclude = null)
        {
            Browser.Wait.ForDomReady();
            ExpandAllFilters();

            foreach (var filter in filters)
            {
                Browser.Wait.ForDomReady();

                ClickAttributeMenu(filter.Key, filter.Value, filtersToExclude);

                Browser.Wait.ForDomReady();
            }
        }

        public virtual bool DoesSortPageResultHaveOpenBoxCallout()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_openBoxSearchFieldId.ToCssIdSelector()));

            return ListOfSaleProducts.Any(x => x.Text.Contains("Open Box"));
        }

        public virtual string GetBreadCrumbText(bool removeForwardSlash = true)
        {
            if (removeForwardSlash)
            {
                return BreadCrumbElement.Text.Replace(" /  ", string.Empty);
            }

            var returnedText = TextActions.RegexNoTabsAndNewLines(SortBreadcrumb.Text);
            returnedText = String.Concat(returnedText.Where(c => !Char.IsWhiteSpace(c)));
            return returnedText.Substring(0, 38);
        }

        public string GetViewOpenBoxText()
        {
            return ViewOpenBoxText;
        }

        public virtual string GetCategory()
        {
            var random = new Random();
            var list = new List<string> { "bathroom vanity lights", "wall sconces", "table lamps", "floor lamps", "lamp shades" };
            var index = random.Next(list.Count);
            return list[index];
        }

        public virtual string SearchForCategory()
        {
            var searchText = GetCategory();
            OpenBoxSearchField.SendKeys(searchText);
            OpenBoxSearchField.SendKeys(Keys.Enter);

            Browser.Wait.IsVisibleElement(By.CssSelector(_openBoxSearchFieldId.ToCssIdSelector()));

            return searchText;
        }

        public virtual void ExpandAllFilters()
        {
            if (IsMoreFiltersElementVisibleOnSortPage()) { MoreFiltersElement.Click(); }
        }

        public virtual string SearchFilterText(string searchText)
        {
            Browser.Wait.ForElementToStopAnimating(SearchFilterField(1));

            SearchFilterField(1).Click();
            SearchFilterField(1).SendKeys(searchText);

            Browser.ClickByJs(SearchFilterButton(1));

            Browser.Wait.IsVisibleElement(By.XPath($"//span[@class='{_subBreadCrumbClass}']/span[ text()='{searchText}']"));

            return TextActions.RegexNoTabsAndNewLines(ListOfBreadCrumbNames[1].Text.Trim());
        }

        public void SelectSortPageSkuByIndex(int index)
        {
            WaitForSortPageSku(index);

            var firstDisplayedProductOnSortPage = 0;
            if (DisplayedProductAtIndex(firstDisplayedProductOnSortPage).GetAttribute("title").Contains("Colorful Table Lamps"))
            {
                DisplayedProductAtIndex(index + 1).Click();
            }
            else
                DisplayedProductAtIndex(index).Click();
        }

        public void SelectSingleProduct(string url)
        {
            Browser.Navigate(url);
            Browser.Wait.ForDomReady();

            FirstDisplayedProductLink.Click();
        }

        public void NavigateToGiftCardPdp()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_giftCardShopNowBtnXpath));
            Browser.Wait.ForClickableElement(GiftCardShopNowBtn).Click();
        }

        public virtual void ClickProductWithQtyLeftCallout()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));

            for (var i = 0; i < DailySaleAndLimitedQtyItems.Count - 1;)
            {
                var dailySaleAndLimitedQtyItem = DailySaleAndLimitedQtyItem(i).Text;

                if (dailySaleAndLimitedQtyItem.Contains("1 Left"))
                {
                    i++;
                }
                else
                {
                    DailySaleAndLimitedQtyItem(i).Click();

                    break;
                }
            }
        }

        public virtual IElement GetSortResultProduct()
        {
            return Browser.Locate.ElementBySelector(_sortResultProductsId.ToCssIdSelector());
        }

        public virtual string GetQuantityLeftForSkuOnSort(string sku)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector($"{sku.ToUpper().ToTagNameAndAttributeCssSelector(HtmlTextWriterTag.Div, "data-sku")} {_sortCallout6Class.ToCssClassSelector()}"));

            return Browser.Wait.ForElement(
            Browser.Locate.ElementBySelector($"{sku.ToUpper().ToTagNameAndAttributeCssSelector(HtmlTextWriterTag.Div, "data-sku")} {_sortCallout6Class.ToCssClassSelector()}"), 5)
            .Text.Replace(" Left", string.Empty);
        }

        public void NavigateToPriceFilteredSortPage(string breadcrumbSortUrl, decimal price)
        {
            var lowPrice = (price - (decimal).01).ToString(); //Reduce price of SKU captured from PDP by one penny. This will be used to construct a custom Sort page URL that returns only a few results. 
            var highPrice = (price + (decimal).01).ToString(); //Increase price of SKU captured from PDP by one penny. This will be used to construct a custom Sort page URL that returns only a few results.
            var filteredUrl = $"{breadcrumbSortUrl}p_@@{lowPrice.Split('.')[0]}@@@{lowPrice.Split('.')[1]}-@-@@{highPrice.Split('.')[0]}@@@{highPrice.Split('.')[1]}";
            Browser.Navigate(filteredUrl);
            Browser.Wait.ForDomReady();
        }

        public string GetSoldOutString()
        {
            return SoldOutString;
        }

        public virtual List<string> GetLinksForGivenNumberOfProductsOnSortPage(int numberOfProducts)
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


        // Method is used to get searchable product name from db.
        public string GetProductNameByShortSkuFromDb(string productName)
        {
            // Replace - with the @
            string finalReady = productName.Replace("-", "@");

            // Replace blank spaces with - then replace / with - and replace &quot; with %22
            string correctProductName = Regex.Replace(finalReady, "\\s+", "-").Replace("/", "-").Replace("&quot;", "%22");

            return correctProductName;
        }


        public void NavigateToSearchSortPage(string productName, string shortSku)
        {
            var url = $"{Urls.HomePageUrl}{Urls.ProductsUrlDirectory}/{"s_"}{productName}";

            Browser.Navigate(url);
        }

        public bool DoesSkuExistOnSortPage(string sku)
        {
            return ListOfProductsOnSortPage.Any(link => link.GetAttribute(HtmlTextWriterAttribute.Href.ToString()).Contains(sku));
        }

        public void SearchPageForSku(string sku)
        {
            while (!DoesSkuExistOnSortPage(sku))
            {
                NextPageLinkElement.Click();
            }

            Browser.Wait.ForDomReady();
        }

        public void NavigateToOpenSearchSortPage(string productName, string shortSku)
        {
            var url = $"{Urls.LampsPlusOpenBoxUrl}/{"s_"}{productName}";

            Browser.Navigate(url);
            Browser.Wait.ForDomReady();
        }

        public void NavigateToSortByProductNameAndCategory(string category, string productName, string shortSku)
        {
            var url = $"{Urls.HomePageUrl}{Urls.ProductsUrlDirectory}/{category}/{"s_"}{productName}";

            Browser.Navigate(url);
            Browser.Wait.ForDomReady();

            // Scroll to the Product SKU
            Browser.ScrollIntoView(SortSearchSku(shortSku.ToUpper()),true);
        }


        public Dictionary<string, string> GetRandomFilterAndAttributeFromSortPage(List<string> filtersToExclude = null)
        {
            var filterAndAttribute = new Dictionary<string, string>();
            var filters = GetListOfAvailableFiltersOnSortPage(filtersToExclude);

            //Create a new dictionary that contains only filters that do not have a value of "null" and then randomly select one.
            var listOfAvailableFilterIndexes = filters.Item1.Select(x => x.Key).Except(filters.Item2.Select(y => y.Key)).ToList();
            var randomAvailableFilterIndex = new Random().Next(listOfAvailableFilterIndexes.Count);
            var randomFilterIndex = listOfAvailableFilterIndexes[randomAvailableFilterIndex];
            var listOfFilterAttributes = new List<string>();
            var totalNumberOfFilterAttributes = AvailableFilterAttributes(randomFilterIndex);
            var filterAttributeIndex = 0;

            while (filterAttributeIndex < totalNumberOfFilterAttributes)
            {
                listOfFilterAttributes.Add(FilterAttributeName(randomFilterIndex, filterAttributeIndex));
                filterAttributeIndex++;
            }

            var randomFilterAttributeIndex = new Random().Next(0, listOfFilterAttributes.Count);
            filterAndAttribute.Add(FilterName(randomFilterIndex), FilterAttributeName(randomFilterIndex, randomFilterAttributeIndex));

            return filterAndAttribute;
        }

        public virtual void SelectFirstProductOnSortPage()
        {
            FirstDisplayedProductElement.Click();
        }

        public string GetIndividualBreadcrumbNames(int breadcrumbIndex)
        {
            return ListOfBreadCrumbNames[breadcrumbIndex].Text.Replace("/", "").Trim();
        }

        public virtual ReadOnlyCollection<IElement> GetEntireBreadcrumbTrail()
        {
            return Browser.Locate.ElementsByClassName(_sortBreadcrumbClass);
        }

        public void ExpandSortPageBreadcrumbList()
        {
            Browser.Locate.ElementByClassName(_showMoreBreadCrumbsBtnClass).Click();
            Browser.Wait.ForCondition(() => Browser.Locate.ElementByClassName(_showMoreBreadCrumbsBtnTextWrapperClass).Text == "Show Less");
        }

        public Dictionary<string, string>[] ApplyFilters(int numberOfFilters, bool applyFiltersInOrder = false, Dictionary<string, string> predefinedFilters = null, List<string> filtersToExclude = null)
        {
            var appliedFilters = new List<Dictionary<string, string>>();

            if (applyFiltersInOrder)
            {
                var filters = GetFiltersInCanonicalOrder;

                SelectFiltersAndAttributes(filters);
                appliedFilters.Add(filters);
            }

            if (predefinedFilters != null)
            {
                SelectFiltersAndAttributes(predefinedFilters);

                appliedFilters.Add(predefinedFilters);
            }

            else
            {
                for (var i = 0; i < numberOfFilters; i++)
                {
                    var filters = GetRandomFilterAndAttributeFromSortPage(filtersToExclude);
                    appliedFilters.Add(filters);
                    SelectFiltersAndAttributes(filters, filtersToExclude);
                }
            }

            return appliedFilters.ToArray();
        }

        public virtual string GetH1TagText()
        {
            return SortPageH1Tag.Text.ToLower();
        }

        public void NavigateToSpecificSearchPath(string searchPath)
        {
            Navigate(searchPath);
        }

        public string GetH1TextBeforeAppliedFilters()
        {
            if (Browser.PageUrl.Contains("lp_luxury-lighting"))
            { 
                return LuxuryLightingH1BeforeFilters.Text;
            }

            return Browser.PageUrl.Contains("trend") ? TrendH1BeforeFilters.Text : (Browser.Locate.DoesElementExistImmediately(_sortSplashTitleLeftClass.ToCssClassSelector())) ? H1BeforeFilters.Text : SortPageH1Tag.Text;
        }

        public virtual void SearchForRandomCategory(string category)
        {
            OpenBoxSearchField.SendKeys(category);
            OpenBoxSearchField.SendKeys(Keys.Enter);

            Browser.Wait.IsVisibleElement(By.CssSelector(_openBoxSearchFieldId.ToCssIdSelector()));
            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForDomReady();
        }

        public void WaitForH1ToHaveSearchTerm(string searchText)
        {
            Browser.Wait.ForCondition(() => SortPageH1Tag.Text.ToLower().TrimEnd().Contains(searchText));
        }

        public void SearchLampsPlusChoiceProduct(Databases.Entities.ProductModel product, float price)
        {
            var finish = product.Finish;
            var style = product.Style;
            var usage = product.Usage;
            var type = product.Type;
            var sku = product.ShortSku;

            var hyphenInCategory = product.Category.Replace(" ", "-").ToLower();
            var url = "https://www.lampsplus.com/products/" + hyphenInCategory + "/";

            Browser.Navigate(url);

            NavigateToPriceFilteredSortPage(url, Convert.ToDecimal(price));

            Browser.Wait.ForDomReady();

            var urlPriceFilter = Browser.PageUrl.Replace("https://www.lampsplus.com/products", string.Empty);
            var categoryUrl = $"{Urls.LampsPlusProductsUrl}/{"style_" + style}/{"finish_" + finish}/{"usage_" + usage}/{"type_" + type}/{urlPriceFilter}";
            var sortUrl = categoryUrl.Replace("style_/", string.Empty).Replace("finish_/", string.Empty).Replace("usage_/", string.Empty).Replace("type_/", string.Empty);

            Browser.Navigate(sortUrl);
            Browser.Wait.ForDomReady();

            SearchPageForSku(sku);
        }

        public bool DoesLampsPlusChoiceBadgeDisplay()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_sortCallout8Class.ToCssClassSelector()));
        }

        public string GetFreeShippingFreeReturnLabel()
        {
            return Browser.Locate.ElementByClassName(_sortSaveShipReturnsClass).Text;
        }

        public void ApplyCustomPrice(decimal minPrice, decimal maxPrice)
        {
            PriceFilter.Click();

            Browser.Wait.ForDisplayedElement(MinPriceFilterField, 15).SendKeys(minPrice.ToString());

            MaxPriceFilterField.SendKeys(maxPrice.ToString());
            FilterPriceButton.Click();

            Browser.Wait.AreAllElementsVisible(By.CssSelector(SortResultProdPriceClass.ToCssClassSelector()));
        }

        public ReadOnlyCollection<IElement> GetListOfSaleProducts()
        {
            return ListOfSaleProducts;
        }

        public bool DoesSalePageResultHaveSaleCallOut()
        {
            bool flag = false;

            var saleCallout = ListOfSaleProducts.Count;
            var sortPageProductPosition = 0;

            for (var i = sortPageProductPosition; i < saleCallout; i++)
            {
                flag = ListOfSaleProducts[i].Text.Contains("Sale");
            }
            return flag;
        }

        public void SearchText(IElement searchElement, string searchText)
        {
            searchElement.SendKeys(searchText);
            searchElement.SendKeys(Keys.Enter);

            Browser.ScrollToBottomOfWindow();
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForDomReady();
        }

        public IElement GetSaleSearchField()
        {
            return SaleSearchField;
        }

        public bool DoesSaleSortPageResultHaveSaleOrClearanceCallOut()
        {
            bool flag = false;

            Browser.Wait.AreAllElementsVisible(By.CssSelector(SortResultProdPriceClass.ToCssClassSelector()));
            var saleCallout = ListOfSaleProducts.Count;
            var sortPageProductPositionAfterAddingFilter = 0;

            for (int i = sortPageProductPositionAfterAddingFilter; i < saleCallout; i++)
            {
                if (ListOfSaleProducts[i].Text.Contains("Sale") || ListOfSaleProducts[i].Text.Contains("Clearance"))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        public string GetBreadcrumbHomeLink()
        {
            return BreadcrumbHomeLink.GetAttribute(HtmlTextWriterAttribute.Href.ToString());
        }

        public string GetShippingCallOutLabel()
        {
            return Browser.Locate.ElementByClassName(_sortSaveShipReturnsClass).Text;
        }

        public string GetShippingCallOut()
        {
            return _sortSaveShipReturnsClass;
        }

        public string GetMoreOptionsCallout()
        {
            return _sortCallout3Class;
        }

        public string Get16PlusColorsCalloutLabel()
        {
            return  Browser.Locate.ElementByClassName(_sortCallout1Class).Text.Replace("\r\n", " ");
        }

        public string Get16PlusColorsCallout()
        {
            return _sortCallout1Class;
        }

        public string GetSkuWithCallout(string callout)
        {
            var elements = ProductContainersList;
            var shortSku = string.Empty;

            foreach (var element in elements)
            {
                if (Browser.Locate.ElementsByClassName(callout, element).Count > 0)
                {
                    shortSku = element.GetAttribute("data-sku");
                    break;
                }
            }

            return shortSku;
        }

        public List<string> GetPageContents()
        {
            var contentsItems = new List<string>
            {
                NumberOfResults.Text.Replace(" results", string.Empty).Replace(",", string.Empty),
                Browser.PageUrl,
                BreadCrumbElement.GetAttribute("href"),
            };

            return contentsItems;
        }

        public void SelectAvailableAtThisLocationCheckbox()
        {
            AvailableAtThisLocationCheckbox.Click();
        }

        public virtual string GetQtyLeftCallout()
        {
            return _sortCallout6Class;
        }

        public virtual string GetQtyLeftCalloutLabel()
        {
            return Browser.Locate.ElementByClassName(_sortCallout6Class).Text;
        }        
        
        public virtual string GetHundredPlusMoreColorsCallout()
        {
            return _hundredPlusMoreColorsCalloutClass;
        }

        public int GetQtyLeftValue()
        {
            var qtyLeftText = TextActions.GetIntegerOnly(GetQtyLeftCalloutLabel());
            var qtyLeftDisplayed = Convert.ToInt32(qtyLeftText);
            return qtyLeftDisplayed;
        }

        public virtual string GetDailySaleCallout()
        {
            return _sortCallout4Class;
        }

        public virtual string GetDailySaleCalloutLabel()
        {
            return Browser.Locate.ElementByClassName(_sortCallout4Class).Text;
        }

        public virtual void ScrollDownToCallout(string callout)
        {
            int maxIteration = int.Parse(PaginationLastDigit.Text);
            int iterationsCount = 0;
            bool calloutDisplayCheck = false;

            while (iterationsCount < maxIteration && !calloutDisplayCheck)
            {
                Browser.Wait.AreAllElementsVisible(By.CssSelector(SortResultProdPriceClass.ToCssClassSelector()));
                try
                {
                    if (callout == "16+ Colors")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_sixteenPlusMoreColorsCalloutClass));
                        Browser.ScrollIntoView(SixteenPlusMoreColorsCallout);
                    }
                    else if (callout == "Open Box")
                    {
                        Browser.Wait.IsVisibleElement(By.XPath(_openBoxCalloutXpath));
                        Browser.ScrollIntoView(OpenBoxCallout);
                    }
                    else if (callout == "Lamps Plus Choice")
                    {
                        Browser.Wait.ForElementToStopAnimating(SortResultProducts);
                        Browser.ScrollIntoView(LampsPlusChoiceCallOut);

                        Browser.Wait.ForElementToStopAnimating(SortResultProducts); 
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortCallout8Class.ToCssClassSelector()));
                    }
                    else if (callout == "100+ Colors")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_hundredPlusMoreColorsCalloutClass));
                        Browser.ScrollIntoView(HundredPlusMoreColorsCallout);
                    }
                    else if (callout == "Sale")
                    {
                        Browser.Wait.IsVisibleElement(By.XPath(_saleCalloutXpath));
                        Browser.ScrollIntoView(SaleCallOut);
                    }
                    else if (callout == "More Options")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_sortCallout3Class));
                        Browser.ScrollIntoView(MoreOptionsCallOut);
                    }
                    else if (callout == "Left")
                    {
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
                        Browser.ScrollIntoView(LeftCallout(0));
                    }
                    else if (callout == "Clearance Left")
                    {
                        Browser.Wait.IsVisibleElement(By.CssSelector(_sortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
                        Browser.ScrollIntoView(LeftClearanceCallout(0));
                    }
                    else if (callout == "Sold Out")
                    {
                        Browser.Wait.IsVisibleElement(By.ClassName(_sortCallout2Class));
                        Browser.ScrollIntoView(SoldOutCallout(0));
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

        public virtual void NavigateToPdpFromSortByProductPosition(int positionOfProductOnSort)
        {
            NthDisplayedProductElementForCertonaWidget(positionOfProductOnSort).Click();
        }

        public virtual string GetTextColor()
        {
            return Browser.Locate.ElementByClassName(_sortResultProdPriceClass).GetCssValue("color");
        }

        public bool DoesTextColorMatches(string color)
        {
            var saleCallout = ListOfSaleProducts.Count;
            var sortPageProductPosition = 0;

            for (var i = sortPageProductPosition; i < saleCallout; i++)
            {
                if (PriceAndClearanceCallout(i).GetCssValue("color") == color)
                    return true;
            }

            return false;
        }

        public string GetNonSaleProductFromSort()
        {
            Browser.Wait.AreAllElementsVisible(By.CssSelector(SortResultProdPriceClass.ToCssClassSelector()));
            var productsOnPage = ListOfSaleProducts.Count;
            int i;
            for (i = 0; i < productsOnPage;)
            {
                if (ListOfSaleProducts[i].Text.Contains("Sale") || ListOfSaleProducts[i].Text.Contains("Clearance"))
                {
                    i++;
                }
                else break;
            }
            return GetDisplayedProductAttribute(i, "data-sku");
        }

        public ProductModel GetContentsOf(string sku)
        {
            ProductModel randomSortProduct = null;
                randomSortProduct = new ProductModel
                {
                    Price = GetProductPriceOfSku(sku),
                    Name = GetProductNameBySku(sku)
                };

            return randomSortProduct;
        }

        public virtual string GetSaleCallout()
        {
            return _salePriceClass;
        }

        public IElement GetContextualSearchBar()
        {
            return ContextualSearchBar;
        }

        public void SearchInContextualSearchBarForSort(string searchText)
        {
            ContextualSearchBarForSort.SendKeys(searchText);
            contextualSearchBarBtn.Click();

            DoesBreadcrumbContainSearchText(searchText);
        }

        public virtual bool DoesStickyHeaderDisplayOnSort()
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(_stickyFilterClass));
        }

        public string GetCurrentPageNumber()
        {
            return SortPaginationCurrentElement.Text;
        }

        public string GetPaginationRange()
        {
            return PaginationRange.Text;
        }

        public virtual void NavigateToPageNumber(int pageNumber)
        {
            Browser.ScrollToElement(Browser.Locate.ElementByXpath(SortResultContainerLastXpath));
            PaginationNumber(pageNumber).Click();
        }
    }
}