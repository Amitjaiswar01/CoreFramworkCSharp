using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class SortBase : Page, ISort
    {
        /// <inheritdoc />
        protected SortBase(IBrowser browser, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser)
        {
            GlobalLocators = globalLocators;
            _testsBase = testsBase;
        }


        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }

        private readonly TestsBase _testsBase;

        public string BrandString => "Brand";
        public string ColorString => "Color";
        public string DailyString => "DAILY";
        public string FinishString => "Finish";
        public string FreeShippingUrlFragmentString => "freeshipping_view-free-shipping-items/";
        public string ImageString => "image";
        public string LeftString => "Left";
        public string SoldOutString => "Sold Out";
        public string PriceString => "Price";
        public string SaleString => "Sale";
        public string SizeString => "Size";
        public string SkuString => "sku";
        public string SpecialsString => "Specials";
        public string FreeShippingString => "Free Shipping";
        public string TypeString => "Type";
        public string UsageString => "Usage";
        public string StyleString => "Style";
        public string HeightString => "Height";
        public string CategoryString => "Category";
        public string WidthString => "Width";
        public string NumberOfLightsString => "Number of Lights";
        public string CustomerRatingString => "Customer Rating";
        public string SortResultSavePriceClass => "sortResultSavePrice";
        public string SearchBarFilterId => "SearchBarFilter";
        public abstract string MoreFiltersLinkXpath { get; }
        public abstract string OpenBoxSearchClass { get; }
        public abstract string ShowMoreClass { get; }
        public abstract string SubBreadCrumbClass { get; }
        public abstract string SortPageImgXpath { get; }
        public abstract string ProductContainerId { get; }
        #endregion

        #region CSS Selector Strings
        private string BdProdSortId { get; } = "bdProdSort";
        private string BreadcrumbLevelClass { get; } = "breadcrumbLevel";
        private string CurrentPageClass { get; } = "current";        
        private string PaginationClass { get; } = "pagination";
        private string PaginationRangeClass { get; } = "paginationRange";
        private string PlaceholderAttrString { get; } = "placeholder";
        private string RecentlyViewedSectionClass { get; } = "recentlyViewedWrapper";
        private string RecentlyViewedId { get; } = "appRecommendedItemsRecentlyViewed";
        private string SortAttMenuDDListItemVisibleClass { get; } = "sortAttMenuDD-list-item--visible";
        private string SortCallout3Class { get; } = "sortCallout3";
        private string SortFilterMinMaxInputClass { get; } = "SortFilterMinMax__input";
        private string SortFilterMinMaxArrowApplyBtnClass { get; } = "SortFilterMinMax__arrowApplyBtn";
        private string SortResultContainerId { get; } = "sortResultContainer";
        private string AvailableInStoreId { get; } = "availableInStoreCheckbox";

        public string LpContainerId { get; } = "lpContainer";
        public string CertonaContainerClass { get; } = "certonaSearchWidgetContainer__list";
        public string BreadCrumbXpath { get; } = "//div[@class='sortBreadCrumb']";
        public string DivAttMenuId { get; } = "divAttMenu";
        public string DivBrandId { get; } = "divBrand";
        public string DivUsageId { get; } = "divUsage";
        public string BtnAttributeGroupStyleId { get; } = "btnAttributeGroup__Style";
        public string DivHeightId { get; } = "divHeight";
        public string BtnAttributeGroupCategoryId { get; } = "btnAttributeGroup__Category";
        public string DivWidthId { get; } = "divWidth";
        public string DivNumberOfLightsId { get; } = "divNumberofLights";
        public string DivCustomerRatingId { get; } = "divCustomerRating";
        public string BtnAttributeGroupColorId { get; } = "btnAttributeGroup__Color";
        public string BtnAttributeGroupFinishId { get; } = "btnAttributeGroup__Finish";
        public string BtnAttributeGroupPriceId { get; } = "btnAttributeGroup__Price";
        public string BtnAttributeGroupSaleId { get; } = "btnAttributeGroup__Sale";
        public string DivSizeId { get; } = "divSize";
        public string DivTypeId { get; } = "divType";
        public string DropdownClass { get; } = "Dropdown";
        public string FilterHeaderClass { get; } = "filterHeader";
        public string FreeShippingCallOut { get; } = "Free Shipping*";
        public string GiftCardBalanceSectionClass { get; } = "gift-cards__balance";
        public string GiftCardShopNowBtnXpath { get; } = "//*[contains(text(),'Shop Now')]";
        public string BtnAttributeGroupSpecialsId { get; } = "btnAttributeGroup__Specials";
        public string ImageWrapperClass { get; } = "imageWrapper";
        public string JsCertonaTitleClass { get; } = "jsCertonaTitle";
        public string MoreFiltersBtnClass { get; } = "moreFiltersBtn";
        public string MoreLikeThisClass { get; } = "moreLikeThis";
        public string OpenBoxSearchFieldId { get; } = "searchOpenBox";
        public string PaginationNextClass { get; } = "paginationNext";
        public string PaginationNextXpath { get; } = "//a[contains(@class,'paginationNext')]";
        public string PaginationPrevClass { get; } = "paginationNext";
        public string SalePriceClass { get; } = "salePrice";
        public string SalePriceAmountClass { get; } = "salePriceAmount";
        public string SortFilterAttributeValueClass { get; } = "SortFilterAttributeValue";
        public string SortFilterDropdownContentClass { get; } = "SortFilterDropdownContent";
        public string SortItemAddToCartButtonClass { get; } = "ProductSortItemAddToCartButton";
        public string SortAttMenuDdClass { get; } = "sortAttMenuDD";
        public string SortCallout2Class { get; } = "sortCallout2";
        public string SortCallout4Class { get; } = "sortCallout4";
        public string SortCallout8Class { get; } = "sortCallout8";
        public string SortCallout6Class { get; } = "sortCallout6";
        public string SortFilterAttrGroupDropdownClass { get; } = "SortFilterAttrGroupDropdown";
        public string SortFilterDisplaySetDropdownsClass { get; } = "SortFilterDisplaySetDropdowns";
        public string SortPageH1TagId { get; } = "SortPageH1Tag";
        public string SortSplashTitleLeftClass { get; } = "sortSplashTitleLeft";
        public string SortResultContainerClass { get; } = "sortResultContainer";
        public string SortResultProdImgClass { get; } = "sortResultProdImg";
        public string SortResultProdNameClass { get; } = "sortResultProdName";
        public string SortResultProdPriceClass { get; } = "sortResultProdPrice";
        public string SortResultProductsId { get; } = "sortResultProducts";
        private string SortSaveShipReturnsClass { get; } = "sortSaveShipReturns";
        public string StickyFilterClass { get; } = "stickyFilter";
        public string UnveilClass { get; } = "unveil";
        public string SortMoreLikeThisBtnClass { get; } = "sortMoreLikeThisBtn";
        public string SortResultsProdinfoClass { get; } = "sortResultsProdInfo";
        public string MymlSectionId { get; } = "CertonaContainer";

        public abstract string OverlayContentWrapperCloseButtonClass { get; }
        public abstract string AvailableAtThisLocationId { get; }
        public abstract string BreadCrumbClass { get; }
        public abstract string SuggestedProductsContainerClass { get; }
        public abstract string RecentlyViewedItemClass { get; }
        public abstract string SplashImageClass { get; }
        public abstract string SortResultImgContainerClass { get; }
        public abstract string SortResultProdInfoClass { get; }
        public abstract string SortFilterAttributeGroupClass { get; }
        public abstract string SortFilterAttributeValueNameClass { get; }
        public abstract string SortFilterDisplaySetDrawerButtonGroupClass { get; }
        public abstract string SortFilterAppliedFilterFilterValueClass { get; }
        public abstract string ProductTitleClass { get; }
        public abstract string ProductTitleClassMobileSelector { get; }
        public abstract string TopFilterMenuId { get; }
        public abstract string TopFilterMenuXpath { get; }
        public abstract string TopFilterMenuClass { get; }
        public abstract string FilterAttributeParentElement { get; }
        public abstract string SaleSearchFieldId { get; }
        public abstract string SearchBtnSalePageId { get; }
        public abstract string SortFilterAppliedFiltersClass { get; }
        public abstract string SortFilterAppliedFiltersCollapsibleClass { get; }
        public abstract string SortFilterCategoryClass { get; }
        public abstract string SortFilterGenericHeaderClass { get; }
        public abstract string SortFilterWrapperId { get; }
        public abstract string SortMenuId { get; }
        public abstract string ProductPriceClass { get; }
        public abstract string CalloutWrapperClass { get; }
        public abstract string HeaderClass { get; }
        public abstract string ToggleSortMenuClass { get; }
        public abstract string SortFilterButtonTriggerClass { get; }
        public abstract string SortFilterButtonTriggerSelector { get; }
        public abstract string SortNormalClass { get; }
        public abstract string CollapsibleDisclosureClass { get; }
        public abstract string LpmmAccordionSubMenu { get; }
        public abstract string LpmmMenuOpenClass { get; }
        public abstract string LpmmSubMenuClass { get; }
        public abstract string LpmmSubOpenClass { get; }
        public abstract string AppliedFiltersClass { get; }
        public abstract string FilterListElementId { get; }
        public abstract string AccordionMenuHeaderClass { get; }
        public abstract string RecentlyViewedItemId { get; }
        public abstract string ContainsTextSelector { get; }
        public abstract string SelectFilterByText { get; }
        public abstract string CloseFiltersMenu { get; }
        public abstract string FiltersScrollableContainer { get; }
        public abstract string IsStickyClass { get; }
        public abstract string QtyLeftClass { get; }
        public abstract string SearchFilterFieldClass { get; }
        public abstract string SearchFilterButtonClass { get; }
        public abstract string SearchFilterButtonXPath { get; }
        #endregion

        #region Page Elements
        //Elements that are the same in both Desktop and Mobile.
        public IElement SearchBarFilter => Browser.Locate.ElementById(SearchBarFilterId);
        public IElement LpContainer => Browser.Locate.ElementById(LpContainerId);
        public IElement CertonaContainer => Browser.Locate.ElementByClassName(CertonaContainerClass);
        public IElement SubBreadCrumb => Browser.Locate.ElementByClassName(SubBreadCrumbClass);
        public IElement ShowMore => Browser.Locate.ElementByClassName(ShowMoreClass);
        public IElement BodyElement => Browser.Locate.ElementById(BdProdSortId);
        public IElement DailySkuSortResultContainer(string dailySaleSku) => Browser.Locate.ElementById($"{SortResultContainerId}{dailySaleSku}");
        public IElement FilterAttributeList => Browser.Locate.ElementByClassName(SortAttMenuDDListItemVisibleClass);
        public IElement FirstAttributeSubmenuElement => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Li, ListOfFilterAttributes[0])[0];
        public IElement FirstDisplayedProductLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(SortResultContainerClass, SortResultProducts));
        public IElement FirstProductElementOnSort => Browser.Locate.ElementByClassName(SortResultContainerClass, SortResultProducts);
        public IElement GetSkuContainerElement(string sku) => Browser.Locate.ElementBySelector($"{SortResultContainerId.ToCssIdSelector()}{sku}");
        public IElement GetSortResultSavePriceBySku(string sku) => Browser.Locate.ElementByClassName(SortResultSavePriceClass, GetSkuContainerElement(sku));
        public IElement GiftCardBalanceSection => Browser.Locate.ElementByClassName(GiftCardBalanceSectionClass);
        public IElement GiftCardShopNowBtn => Browser.Locate.ElementByXpath(GiftCardShopNowBtnXpath);
        public IElement H1BeforeFilters => Browser.Locate.ElementBySelector(SortSplashTitleLeftClass.ToCssClassSelector());
        public IElement NextPageLinkElement => Browser.Locate.ElementBySelector(PaginationNextClass.ToCssClassSelector());
        public IElement NthDisplayedProductElementForCertonaWidget(int position) => Browser.Locate.ElementByXpath($"(//*[contains(@id,'sortResultContainer')])[{position}]");
        public IElement NthDisplayedProductElementForCertonaWidgetNoDiv(int position) => Browser.Locate.ElementByXpath($"//*[@id='certonaItems']/div[{position}]//img");
        public IElement PrevPageLinkElement => Browser.Locate.ElementBySelector(PaginationPrevClass.ToCssClassSelector());
        public IElement SortItemAddToCarButtontAtIndex(int index) => Browser.Locate.ElementsByClassName(SortItemAddToCartButtonClass)[index];
        public IElement SortPageFilterContainer => Browser.Locate.ElementByClassName(SortFilterDisplaySetDropdownsClass);
        public IElement SortPaginationElement => Browser.Locate.ElementByXpath(PaginationNextXpath);
        public IElement SortPaginationRangeElement => Browser.Locate.ElementByClassName(PaginationRangeClass);
        public IElement SortPaginationCurrentElement => Browser.Locate.ElementBySelector($"{PaginationClass.ToCssClassSelector()} {CurrentPageClass.ToCssClassSelector()} {HtmlTextWriterTag.A}");
        public IElement SortPaginationNthElement(int position) => Browser.Locate.ElementBySelector($"{PaginationClass.ToCssClassSelector()} {HtmlTextWriterTag.Li.ToNthChildSelector(position)} {HtmlTextWriterTag.A}");
        public IElement SortResultProducts => Browser.Locate.ElementBySelector(SortResultProductsId.ToCssIdSelector());
		public IElement MinPriceFilterField => Browser.Locate.ElementByClassNameAndAttributeEquals(SortFilterMinMaxInputClass, PlaceholderAttrString, "Min");
		public IElement MaxPriceFilterField => Browser.Locate.ElementByClassNameAndAttributeEquals(SortFilterMinMaxInputClass, PlaceholderAttrString, "Max");
        public IElement FilterPriceButtonElement => Browser.Locate.ElementByClassName(SortFilterMinMaxArrowApplyBtnClass);
        public IElement MoreLikeThisButton => Browser.Locate.ElementBySelector(SortMoreLikeThisBtnClass.ToCssClassSelector());
        public IElement RecentlyViewedSection => Browser.Locate.ElementByClassName(RecentlyViewedSectionClass);
        public IElement RecentlyViewed => Browser.Locate.ElementById(RecentlyViewedId);
        public IElement OpenBoxSearchField => Browser.Locate.ElementById(OpenBoxSearchFieldId);
        public IElement MymlSection => Browser.Locate.ElementById(MymlSectionId);
        public IElement Unveil(int index) => Browser.Locate.ElementsByClassName(UnveilClass)[index];

        //List of elements that are the same in desktop and mobile views.
        public ReadOnlyCollection<IElement> ProductContainersList => Browser.Locate.ElementsByClassName(SortResultContainerClass, SortResultProducts);
        public ReadOnlyCollection<IElement> GetAttributesOfAttributeMenu(IElement attributeMenu) => attributeMenu.FindElements(By.TagName("a"));
        public ReadOnlyCollection<IElement> ListOfVisibleFilterAttributes => Browser.Locate.DisplayedElements(ListOfFilterAttributes);
        public ReadOnlyCollection<IElement> ListOfBreadCrumbLinks => BreadCrumbElement.FindElements(By.CssSelector(BreadcrumbLevelClass.ToCssClassSelector()));
        public ReadOnlyCollection<IElement> SaleItems => Browser.Locate.ElementsWithText(ListOfProductContainer, AttributeSelectorType.HasAttribute, SaleString);

        //Elements that have different implementations in between Desktop and Mobile
        public abstract IElement SearchButtonFilter { get; }
        public abstract IElement SearchFilterButton(int index);
        public abstract IElement SearchFilterField(int index);
        public abstract IElement AppliedFiltersContainer { get; }
        public abstract IElement AvailableAtThisLocationCheckbox { get; }
        public abstract IElement BrandFilterElement { get; }
        public abstract IElement CollapsibleFilterContainer { get; }
        public abstract IElement ColorFilterDropdownElement { get; }
        public abstract IElement ColorFilterElement { get; }
        public abstract IElement ColorFilterFirstElement { get; }
        public abstract IElement DailySaleAndLimitedQtyItem(int index);
        public abstract IElement SaleItem(int index);
        public abstract IElement DailySaleCallout { get; }
        public abstract IElement LampsPlusChoice { get; }
        public abstract IElement DisplayedProductAtIndex(int index);
        public abstract IElement FilterMenu { get; }
        public abstract IElement FiltersSubMenu { get; }
        public abstract IElement FilterSubMenuOpen { get; }
        public abstract IElement FilterOptionCloseButton { get; }
        public abstract IElement FilterOverlay { get; }
        public abstract IElement FinishFilterElement { get; }
        public abstract IElement FirstDisplayedProductElement { get; }
        public abstract IElement LandingPageSplashImage { get; }
        public abstract IElement MobileFilterElement { get; }
        public abstract IElement MobileFilterOptions { get; }
        public abstract IElement MoreFiltersElement { get; }
        public abstract IElement MoreYouMayLikeContainer { get; }
        public abstract IElement NthDisplayedProductElement(int position);
        public abstract IElement OpenBoxSearchElement { get; }
        public abstract IElement PriceFilterElement { get; }
        public abstract IElement PriceFilterDropdownElement { get; }
        public abstract IElement ProdInfo { get; }
        public abstract IElement ProdPriceElement { get; }
        public abstract IElement ProductDescriptionLinksElement { get; }
        public abstract IElement RandomProductElement { get; }
        public abstract IElement RecentlyViewedContainer { get; }
        public abstract IElement SaleFilterElement { get; }
        public abstract IElement SaleFilterDropdownElement { get; }
        public abstract IElement SaleSearchField { get; }
        public abstract IElement SearchBtnSalePage { get; }
        public abstract IElement SelectedSkuContainer(string sku);
        public abstract IElement SizeFilterElement { get; }
        public abstract IElement SortFilterWrapper { get; }
        public abstract IElement SortMenu { get; }
        public abstract IElement SortPageH1Tag { get; }
        public abstract IElement SortPageImg(int index);
        public abstract IElement SpecialsFilterElement { get; }
        public abstract IElement SpecialsFilterDropdownElement { get; }
        public abstract IElement TypeFilterElement { get; }
        public abstract IElement FirstImageOnSort (int index);
        public abstract IElement UsageFilterElement { get; }
        public abstract IElement StyleFilterElement { get; }
        public abstract IElement StyleFilterDropdownElement { get; }
        public abstract IElement HeightFilterElement { get; }
        public abstract IElement CategoryFilterElement { get; }
        public abstract IElement CategoryFilterDropdownElement { get; }
        public abstract IElement WidthFilterElement { get; }
        public abstract IElement NumberOfLightsFilterElement { get; }
        public abstract IElement CustomerRatingFilterElement { get; }
        public abstract IElement StickyFilterElement { get; }
        public abstract IElement FiltersListElement { get; }
        public abstract IElement NumberOfResultsElement { get; }
        public abstract IElement QtyLeftCallout(int index);
        public abstract IElement LpDailySaleCalloutElement(int index);
        public abstract ReadOnlyCollection<IElement> ListOfProductsOnSortPage { get; }

        //Elements that exist in one implementation but not the other.
        public abstract IElement ToggleSortFilterMenuButton { get; }
        public abstract IElement ToggleSortFilterMenuCloseButton { get; }
        public abstract IElement ActiveFilterElement { get; }
        public abstract IElement BreadCrumbElement { get; }
        public abstract IElement MymlItem { get; }
        public abstract IElement SelectedFilter(string filter);
        public abstract IElement GetProductImageByContainer(IElement sortResultContainer);
        public abstract IElement RecentlyViewedItem { get; }
        public abstract ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems { get; }
        public abstract ReadOnlyCollection<IElement> DisplayedFilters { get; }
        public abstract ReadOnlyCollection<IElement> ListOfAvailableFilters { get; }
        public abstract ReadOnlyCollection<IElement> ListOfFilterAttributes { get; }
        public abstract ReadOnlyCollection<IElement> ListOfProductContainer { get; }
        public abstract ReadOnlyCollection<IElement> ListOfProductInfo { get; }
        public abstract ReadOnlyCollection<IElement> ListOfProductLinksSortPage { get; }
        public abstract ReadOnlyCollection<IElement> ListOfSaleProducts { get; }
        public abstract ReadOnlyCollection<IElement> ListOfMobileFilterOptions { get; }
        public abstract ReadOnlyCollection<IElement> ListOfMobileFilterUrlsByFilterName { get; }
        public abstract ReadOnlyCollection<IElement> ListOfFilterOptionsByFilterName(IElement filter);
        public abstract ReadOnlyCollection<IElement> ListOfFilterUrlsByFilterName(IElement filter);
        
        //List of elements that exist in Desktop view and not mobile.
        public abstract ReadOnlyCollection<IElement> ListOfBreadCrumbRemoveLinks { get; }
        public abstract ReadOnlyCollection<IElement> ListOfSortResultContainers { get; }
        public abstract ReadOnlyCollection<IElement> ListOfBreadCrumbNames { get; }
        public abstract ReadOnlyCollection<IElement> FirstFilterOption(int index);
        public abstract ReadOnlyCollection<IElement> ListOfRandomFilterOptions(int index);
        public abstract ReadOnlyCollection<IElement> ListCalloutsElements { get; }
        public abstract ReadOnlyCollection<IElement> SortResultLinks { get; }

        #endregion


        /// <inheritdoc />
        public void SearchPageForSku(string sku)
        {
            while (!DoesSkuExistOnSortPage(sku))
            {
                NextPageLinkElement.Click();
            }

            Browser.Wait.ForDomReady();
        }

        public void SearchPageForProductName(string productName)
        {
            if (!DoesProductNameExistOnSortPage(productName))
            {
                WaitForFilterLink();
            }
            
        }

        /// <inheritdoc />
        public void NavigateToSpecificSearchPath(string searchPath)
        {
            Navigate($"{Urls.HomePageUrl}{searchPath}");
        }

        /// <inheritdoc />
        public bool HasSaleLabel(IEnumerable<IElement> listOfSaleProducts)
        {
            return listOfSaleProducts.Select(label => label.FindElements(By.CssSelector(SalePriceClass.ToCssClassSelector())).Any(x => x.Text.Equals("Sale"))).Any();
        }

        /// <inheritdoc />
        public bool DoesSkuExistOnSortPage(string sku)
        {
            foreach (var link in ListOfProductsOnSortPage)
            {
                if (link.GetAttribute(HtmlTextWriterAttribute.Href.ToString()).Contains(sku)) { return true; }
            }

            return false;
        }

        public bool DoesProductNameExistOnSortPage(string ProductName)
        {
            foreach (var link in ListOfProductsOnSortPage)
            {
                if (link.InternalElement.Text.Contains(ProductName)) { return true; }
            }

            return false;
        }

        public List<string> GetBreadCrumbText(int filterLength)
        {
            var breadcrumbs = ListOfBreadCrumbTexts;
            var list = new List<string>();
            for (int x = breadcrumbs.Count - filterLength; x < breadcrumbs.Count; x++)
            {
                list.Add(breadcrumbs[x].ToLower());
            }
            return list;
        }

        protected List<string> GetFiltersInCanonicalOrder
        {
            get
            {
                var filtersInCanonicalOrder = new List<string>
                {
                    FinishString,
                    ColorString,
                    TypeString,
                    PriceString
                };

                return filtersInCanonicalOrder;
            }
        }

        protected List<string> GetSpecificFilters
        {
            get
            {
                var listOfSpecificFilters = new List<string>
                {
                    FinishString,
                    StyleString,
                    ColorString,
                    HeightString,
                    SizeString,
                    TypeString,
                    PriceString
                };

                return listOfSpecificFilters;
            }
        }

        /// <inheritdoc />
        public bool DoesProductHaveQuantityAndDailySaleCallOut() => DailySaleAndLimitedQtyItems.Count > 0;

        /// <inheritdoc />
        public abstract void ClickAttributeMenu(string attributeName, string attributeValue);

        public abstract void ClickFilterAttributeMenu(string attributeName, string attributeValue);

        /// <inheritdoc />
		public abstract void WaitForAttributeMenu(IElement element);

        /// <inheritdoc />
        public abstract void ExpandAllFilters();

        public abstract void ToggleAppliedFiltersDropdown();

        public abstract void ClickFilterButton();

        public abstract void ClickFilterButtonAndOpenAppliedFiltersDropdown();

        /// <inheritdoc />
        public abstract List<string> ListOfBreadCrumbTexts { get; }

        /// <inheritdoc />
        public bool IsMoreFiltersElementVisibleOnSortPage() => Browser.Locate.ElementImmediately(MoreFiltersBtnClass.ToCssClassSelector()).IsInitialized && Browser.Locate.ElementImmediately(MoreFiltersBtnClass.ToCssClassSelector()).Displayed;

        /// <inheritdoc />
        public bool IsAvailableAtThisLocationOnSortPage => Browser.Locate.ElementById(AvailableInStoreId.ToCssIdSelector()).IsInitialized;

        /// <inheritdoc />
        public abstract bool DoesProductHaveSoldOutCallOut(string sku);

        /// <inheritdoc />
        public abstract string GetProductNameBySku(string sku);

        /// <inheritdoc />
        public abstract string GetProductPriceBySku(string sku);

        /// <inheritdoc />
        public string FirstProductSku => FirstDisplayedProductElement.GetAttribute(GlobalLocators.DataSkuAttribute);

        /// <inheritdoc />
        public string FirstProductSkuOnSort => FirstProductElementOnSort.GetAttribute(GlobalLocators.DataSkuAttribute);

        /// <inheritdoc />
        public abstract string GetFirstProductPrice { get; }

        /// <inheritdoc />
        public string GetProductName => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, ProductDescriptionLinksElement).Text;

        /// <inheritdoc />
        public string FreeShippingLabel => Browser.Locate.ElementByClassName(SortSaveShipReturnsClass).Text;

        /// <inheritdoc />
        public string CanonicalHref => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Link, HtmlTextWriterAttribute.Rel, GlobalLocators.CanonicalAttribute, Browser.Locate.ElementByTagName(HtmlTextWriterTag.Head)).GetAttribute(HtmlTextWriterAttribute.Href.ToString());

        /// <inheritdoc />
        public string LastBreadcrumbText() => BreadCrumbElement.Text.Split('/').Last().Trim().Remove(9);

        /// <inheritdoc />
        public string GetFirstAttributeFilterHrefValue => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, FirstAttributeSubmenuElement).GetAttribute(HtmlTextWriterAttribute.Href.ToString());


        /// <inheritdoc />
        public abstract Dictionary<string, string>[] ApplyFilters(int numberOfFilters, bool applyFiltersInOrder = false, Dictionary<string, string>[] predefinedFilters = null);

        /// <inheritdoc />
        public abstract Dictionary<string, string> GetRandomAppliedFilterOption(List<string> list = null);

        /// <inheritdoc />
        public Dictionary<string, string> GetAppliedFilterOption(IElement filter, int optPosition = 1)
        {
            var filterInfo = new Dictionary<string, string>();

            filterInfo.Add("name", filter.Text);

            filter.Click();

            WaitForAttributeMenu(filter);

            var filterOption = ListOfFilterOptionsByFilterName(filter)[optPosition - 1];
            filterInfo.Add("value", filterOption.GetAttribute("data-unbxd-facet-name"));

            var a = filterOption.GetAttribute(HtmlTextWriterAttribute.Href.ToString()).Split('/');
            var b = Browser.PageUrl.Split('/');
            var diff = a.Except(b);

            filterInfo.Add("chunk", diff.FirstOrDefault());
            Browser.Wait.ForClickableElement(filterOption).Click();

            return filterInfo;
        }

        /// <inheritdoc />
		public List<string> FindLinksForGivenNumberOfProductsOnSortPage(int numberOfProducts)
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

        /// <inheritdoc />
        public void NavigateToPriceFilteredSortPage(string breadcrumbSortUrl, decimal price)
        {
            var lowPrice = (price - (decimal).01).ToString();
            var highPrice = (price + (decimal).01).ToString();
            var filteredUrl = $"{breadcrumbSortUrl}p_@@{lowPrice.Split('.')[0]}@@@{lowPrice.Split('.')[1]}-@-@@{highPrice.Split('.')[0]}@@@{highPrice.Split('.')[1]}";
            Browser.Navigate(filteredUrl);
            Browser.Wait.ForDomReady();
        }

        /// <inheritdoc />
        public abstract string GetQuantityLeftForSkuOnSort(string sku);

        /// <inheritdoc />
        public string FindMoreOptionsCallout()
        {
            var elements = ProductContainersList;
            var shortSku = string.Empty;

            foreach (var element in elements)
            {
                if (Browser.Locate.ElementsByClassName(SortCallout3Class, element).Count > 0)
                {
                    shortSku = element.GetAttribute(GlobalLocators.DataSkuAttribute);
                    break;
                }
            }

            return shortSku;
        }

        /// <inheritdoc />
        public SortPageProduct GetRandomProduct()
        {
            var productElementContainers = Browser.Locate.ElementsByClassName(SortResultContainerClass);
            if (productElementContainers.Count < 1)
                return null;

            var index = MathHelper.GetRandomNumber(productElementContainers.Count);
            var productElementContainer = productElementContainers[index];
            var productElement = GetProductImageByContainer(productElementContainer);
            return new SortPageProduct(productElement);
        }

        /// <inheritdoc />
        public SortPageProduct GetRandomSortProduct()
        {
            var productContainer = GetRandomProductContainer();
            var product = GetProductImageByContainer(productContainer);

            return new SortPageProduct(product);
        }

        /// <inheritdoc />
        public abstract IElement GetRandomProductContainer();

        /// <inheritdoc />
        public IElement GetProductByDataSkuAttribute(string productSku)
        {
            return Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Img,
                GlobalLocators.DataSkuAttribute, productSku);
        }

        /// <inheritdoc />
        public int ClickProductNearOrOverTwoHundredDollars()
        {
            var pos = 1;
            var maxPricePos = 1;
            double maxPrice = 0;
            const int priceThreshold = 200;

            var productList = ProductContainersList;
            var numOfProducts = ListOfProductLinksSortPage.Count;
            for (var i = 0; i < numOfProducts; i++)
            {
                var productPrice = Convert.ToDouble(productList[i].FindElement(By.TagName(HtmlTextWriterTag.Img.ToString())).GetAttribute(GlobalLocators.DataPriceAttribute));

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

        /// <inheritdoc />
        public abstract void OpenMobileSortFilterMenu();

        public abstract void ApplyPriceFilter(string minPrice, string maxPrice);

        public abstract void WaitForFilterLink();

        public abstract void SearchLampsplusChoiceProduct(Databases.Entities.ProductModel product);

        public abstract string NavigationToMoreLikeThisPage();

        public string NavigationToPdpPageByProduct()
        {
            Browser.Wait.AreAllElementsVisible(By.ClassName(SortResultContainerClass));
            var product = DisplayedProductAtIndex(0);
            var sortPageProduct = new SortPageProduct(product);
            var sku = sortPageProduct.ProductSku;
            product.Click();

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(_testsBase.ProductDetail.ProductSkuId.ToCssIdSelector()));

            return sku;
        }

        public string ChooseFirstNormalProduct()
        {
            var pdpSku = string.Empty;

            var getAllSku = ProductContainersList.Where(x => !string.IsNullOrEmpty(x.GetAttribute("data-sku"))).Select(x => x.GetAttribute("data-sku")).ToList();

            for (var productPosition = 0; productPosition < getAllSku.Count; productPosition++)
            {
                if (getAllSku[productPosition].Length >= 6) continue;

                pdpSku = getAllSku[productPosition];
                DisplayedProductAtIndex(productPosition).Click();

                break;
            }

            return pdpSku;
        }
    }
}