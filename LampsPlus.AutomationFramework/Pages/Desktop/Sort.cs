using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;

using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/wall-lamps/type_art-shade/.
    /// </summary>
    public class Sort : SortBase
    {
        /// <inheritdoc />
        public Sort(IBrowser browser, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, globalLocators, testsBase)
        {
            _testsBase = testsBase;
        }

        private readonly TestsBase _testsBase;


        #region CSS Selector Strings
        public override string SearchFilterButtonClass { get; } = "searchArrowBtn";
        public override string SearchFilterFieldClass { get; } = "searchInput";
        public string MymlItemClass { get; } = "sortMYMLProdContainer";
        public string SortResultLinkClass { get; } = "sortResultLink"; 
        public static string MoreYouMayLikeItemClass => "moreYouMayLikeItem";
        public override string AvailableAtThisLocationId { get; } = "//*[@for='availableInStoreCheckbox']";        
        public override string BreadCrumbClass { get; } = "sortBreadCrumb";
        public override string MoreFiltersLinkXpath { get; } = "//*[@id='sortFilterWrapper']//button[contains(@class, 'moreFiltersBtn')]";
        public override string SuggestedProductsContainerClass { get; } = "suggestedProductsContainer";
        public override string RecentlyViewedItemClass { get; } = "slick-track";
        public override string SaleSearchFieldId { get; } = "searchSalePage";
        public override string SearchBtnSalePageId { get; } = "searchBtnSalePage";
        public override string SplashImageClass { get; } = "splash__image";
        public override string SortResultImgContainerClass { get; } = "sortResultImgContainer";
        public override string SortResultProdInfoClass { get; } = "sortResultsProdInfo";
        public override string ShowMoreClass { get; } = "showMoreBreadCrumbsBtn__text-wrapper";
        public override string QtyLeftClass { get; } = "sortCallout6 ";
        public override string ProductPriceClass { get; } = "sortResultProdPrice";

        public override string SearchFilterButtonXPath => throw new NotImplementedException();
        public override string OverlayContentWrapperCloseButtonClass => throw new NotImplementedException();
        public override string SortFilterAttributeGroupClass => throw new NotImplementedException();
        public override string SortFilterAttributeValueNameClass => throw new NotImplementedException();
        public override string SortFilterDisplaySetDrawerButtonGroupClass => throw new NotImplementedException();
        public override string SortFilterAppliedFilterFilterValueClass => throw new NotImplementedException();
        public override string ProductTitleClass => throw new NotImplementedException();
        public override string ProductTitleClassMobileSelector => throw new NotImplementedException();
        public override string TopFilterMenuId => throw new NotImplementedException();
        public override string TopFilterMenuXpath => throw new NotImplementedException();
        public override string TopFilterMenuClass => throw new NotImplementedException();
        public override string FilterAttributeParentElement => throw new NotImplementedException();
        public override string SortFilterAppliedFiltersClass => throw new NotImplementedException();
        public override string SortFilterAppliedFiltersCollapsibleClass => throw new NotImplementedException();
        public override string SortFilterCategoryClass => throw new NotImplementedException();
        public override string SortFilterGenericHeaderClass => throw new NotImplementedException();
        public override string SortFilterWrapperId => throw new NotImplementedException();
        public override string SortMenuId => throw new NotImplementedException();
        public override string CalloutWrapperClass => throw new NotImplementedException();
        public override string HeaderClass => throw new NotImplementedException();
        public override string ToggleSortMenuClass => throw new NotImplementedException();
        public override string SortFilterButtonTriggerClass => throw new NotImplementedException();
        public override string SortFilterButtonTriggerSelector => throw new NotImplementedException();
        public override string SortNormalClass => throw new NotImplementedException();
        public override string CollapsibleDisclosureClass => throw new NotImplementedException();
        public override string LpmmAccordionSubMenu => throw new NotImplementedException();
        public override string LpmmMenuOpenClass => throw new NotImplementedException();
        public override string LpmmSubMenuClass => throw new NotImplementedException();
        public override string LpmmSubOpenClass => throw new NotImplementedException();
        public override string AppliedFiltersClass => throw new NotImplementedException();
        public override string FilterListElementId => throw new NotImplementedException();
        public override string SortPageImgXpath => throw new NotImplementedException();
        public override string AccordionMenuHeaderClass => throw new NotImplementedException();
        public override string RecentlyViewedItemId => throw new NotImplementedException();
        public override string ContainsTextSelector => throw new NotImplementedException();
        public override string SelectFilterByText => throw new NotImplementedException();
        public override string CloseFiltersMenu => throw new NotImplementedException();
        public override string FiltersScrollableContainer => throw new NotImplementedException();
        public override string IsStickyClass => throw new NotImplementedException();
        public override string OpenBoxSearchClass => throw new NotImplementedException();
        public override string ProductContainerId => throw new NotImplementedException();
        public override string SubBreadCrumbClass { get; } = "breadCrumbValueWrapper";
        private string SortFilterAttrGroupDropdownXpath { get; } = "//div[contains(@class,'SortFilterAttrGroupDropdown')]";
        #endregion

        #region Page Elements
        //Elements that exist in both implementations but have different names.
        public override IElement SearchFilterButton(int index) => Browser.Locate.ElementsByClassName(SearchFilterButtonClass)[index];
        public override IElement SearchFilterField(int index) => Browser.Locate.ElementsByClassName(SearchFilterFieldClass)[index];
        public override IElement BrandFilterElement => Browser.Locate.ElementById(DivBrandId);
        public override IElement ColorFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupColorId);
        public override IElement ColorFilterElement => Browser.Locate.ElementById(BtnAttributeGroupColorId);
        public override IElement ColorFilterFirstElement => Browser.Locate.ElementBySelector($"{BtnAttributeGroupColorId.ToCssIdSelector()} {SortAttMenuDdClass.ToCssClassSelector()} {HtmlTextWriterTag.Li.ToFirstChildSelector()} {HtmlTextWriterTag.A.ToString()}");
        public override IElement DailySaleAndLimitedQtyItem(int index) => Browser.Locate.ElementsWithText(ListOfProductContainer, AttributeSelectorType.Contains, DailyString, LeftString)[index];
        public override IElement DailySaleCallout => Browser.Locate.ElementByClassName(SortCallout4Class);
        public override IElement LpDailySaleCalloutElement(int index) => Browser.Locate.ElementsByClassName(SortCallout4Class)[index];
        public override IElement SaleItem(int index) => Browser.Locate.ElementWithText(ListOfProductInfo , AttributeSelectorType.Contains, SaleString);
        public override IElement LampsPlusChoice => Browser.Locate.ElementByClassName(SortCallout8Class);
        public override IElement DisplayedProductAtIndex(int index) => Browser.Locate.ElementsByClassName(SortResultProdImgClass)[index];
        public override IElement FinishFilterElement => Browser.Locate.ElementById(BtnAttributeGroupFinishId);
        public override IElement FirstDisplayedProductElement => Browser.Locate.ElementByClassName(SortResultProdImgClass);
        public override IElement QtyLeftCallout(int index) => Browser.Locate.ElementsByClassName(QtyLeftClass)[index];


        public override IElement AvailableAtThisLocationCheckbox => Browser.Locate.ElementByXpath(AvailableAtThisLocationId);
        public override IElement LandingPageSplashImage => Browser.Locate.ElementByClassName(SplashImageClass);
        public override IElement MoreFiltersElement => Browser.Locate.ElementByXpath(MoreFiltersLinkXpath);
        public override IElement PriceFilterElement => Browser.Locate.ElementById(BtnAttributeGroupPriceId);
        public override IElement PriceFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupPriceId);
        public override IElement ProdInfo => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Img, SortResultProdImgClass);
        public override IElement ProdPriceElement => Browser.Locate.ElementByClassName(SortResultProdPriceClass);
        public override IElement ProductDescriptionLinksElement => Browser.Locate.ElementByClassName(SortResultProdNameClass);
        public override IElement RandomProductElement => Browser.Locate.ElementsByClassName(SortResultProdImgClass).OrderBy(x => new Guid()).First();
        public override IElement SaleFilterElement => Browser.Locate.ElementById(BtnAttributeGroupSaleId);
        public override IElement SaleFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupSaleId);
        public override IElement SaleSearchField => Browser.Locate.ElementById(SaleSearchFieldId);
        public override IElement SearchBtnSalePage => Browser.Locate.ElementById(SearchBtnSalePageId);
        public override IElement SizeFilterElement => Browser.Locate.ElementById(DivSizeId);
        public override IElement SortPageH1Tag => Browser.Locate.ElementById(SortPageH1TagId);
        public override IElement NumberOfResultsElement => Browser.Locate.ElementByXpath("//span[@class='result']");
        public override IElement SpecialsFilterElement => Browser.Locate.ElementById(BtnAttributeGroupSpecialsId);
        public override IElement SpecialsFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupSpecialsId);
        public override IElement TypeFilterElement => Browser.Locate.ElementById(DivTypeId);
        public override IElement UsageFilterElement => Browser.Locate.ElementById(DivUsageId);
        public override IElement StyleFilterElement => Browser.Locate.ElementById(BtnAttributeGroupStyleId);
        public override IElement StyleFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupStyleId);
        public override IElement HeightFilterElement => Browser.Locate.ElementById(DivHeightId);
        public override IElement CategoryFilterElement => Browser.Locate.ElementById(BtnAttributeGroupCategoryId);
        public override IElement CategoryFilterDropdownElement => Browser.Locate.ElementByClassNameAndAttributeEquals(DropdownClass, GlobalLocators.AriaLabelledByAttribute, BtnAttributeGroupCategoryId);
        public override IElement WidthFilterElement => Browser.Locate.ElementById(DivWidthId);
        public override IElement NumberOfLightsFilterElement => Browser.Locate.ElementById(DivNumberOfLightsId);
        public override IElement CustomerRatingFilterElement => Browser.Locate.ElementById(DivCustomerRatingId);
        public override IElement GetProductImageByContainer(IElement sortResultContainer) => Browser.Locate.ElementByClassName(SortResultProdImgClass, sortResultContainer);
        public override IElement StickyFilterElement => Browser.Locate.ElementByClassName(StickyFilterClass);
        public override IElement FiltersListElement => Browser.Locate.ElementById(DivAttMenuId);
        public override IElement BreadCrumbElement => Browser.Locate.ElementBySelector(BreadCrumbClass.ToCssClassSelector());
        public override IElement MoreYouMayLikeContainer => Browser.Locate.ElementByClassNameAndAttributeEquals(SuggestedProductsContainerClass, "data-capture-section", "MoreYouMayLikeWidget");
        public override IElement MymlItem => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(MymlItemClass));
        public override IElement NthDisplayedProductElement(int position) => Browser.Locate.ElementBySelector($".{SortResultContainerClass.ToNthChildSelector(position)}");
        public override IElement RecentlyViewedContainer => Browser.Locate.ElementByClassName(MoreYouMayLikeItemClass);
        public override IElement RecentlyViewedItem => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Div, Browser.Locate.ElementByClassName(RecentlyViewedItemClass));
        public override IElement SelectedFilter(string filter) => Browser.Locate.ElementByClassNameAndAttributeEquals(SortFilterAttrGroupDropdownClass, GlobalLocators.DataUnbxdFacetTypeString, filter);

        // Elements that exist in Mobile view but not Desktop view.
        public override IElement SearchButtonFilter => throw new NotImplementedException();
        public override IElement ActiveFilterElement => throw new NotImplementedException();
        public override IElement AppliedFiltersContainer => throw new NotImplementedException();
        public override IElement FilterMenu => throw new NotImplementedException();
        public override IElement FiltersSubMenu => throw new NotImplementedException();
        public override IElement FilterSubMenuOpen => throw new NotImplementedException();
        public override IElement FilterOptionCloseButton => throw new NotImplementedException();
        public override IElement FilterOverlay => throw new NotImplementedException();
        public override IElement CollapsibleFilterContainer => throw new NotImplementedException();
        public override IElement MobileFilterOptions => throw new NotImplementedException();
        public override IElement MobileFilterElement => throw new NotImplementedException();
        public override IElement SelectedSkuContainer(string sku) => throw new NotImplementedException();
        public override IElement SortFilterWrapper => throw new NotImplementedException();
        public override IElement SortMenu => throw new NotImplementedException();
        public override IElement SortPageImg(int index) => throw new NotImplementedException();
        public override IElement ToggleSortFilterMenuButton => throw new NotImplementedException();
        public override IElement ToggleSortFilterMenuCloseButton => throw new NotImplementedException();
        public override IElement FirstImageOnSort(int index) => throw new NotImplementedException();
        public override IElement OpenBoxSearchElement => throw new NotImplementedException();

        //List of elements that exist in both implementations but are located differently.
        public override ReadOnlyCollection<IElement> ListOfBreadCrumbNames => Browser.Locate.ElementsByClassName(SubBreadCrumbClass);
        public override ReadOnlyCollection<IElement> ListOfProductsOnSortPage => Browser.Locate.ElementsBySelector("div.sortResultContainer > a");
        public override ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems => Browser.Locate.ElementsWithText(ListOfProductContainer, AttributeSelectorType.Contains, DailyString, LeftString);

        public override ReadOnlyCollection<IElement> ListOfFilterAttributes
        {
            get
            {
                Browser.Wait.ForDomReady();

                var filterList = Browser.Locate.ElementsByXpath(SortFilterAttrGroupDropdownXpath);

                var selectedFilters = filterList.Where(attribute => attribute.GetAttribute(GlobalLocators.DataUnbxdFacetTypeString) != "Sale").ToList();

                return new ReadOnlyCollection<IElement>(selectedFilters);
            }
        }

        public override ReadOnlyCollection<IElement> ListOfProductContainer => Browser.Locate.ElementById(SortResultProductsId).FindElements(By.ClassName(SortResultImgContainerClass));
        public override ReadOnlyCollection<IElement> ListOfProductLinksSortPage => Browser.Locate.ElementsByClassName(SortResultProdNameClass);
        public override ReadOnlyCollection<IElement> ListOfSaleProducts => Browser.Locate.ElementById(SortResultProductsId).FindElements(By.ClassName(SortResultProdPriceClass));
        public override ReadOnlyCollection<IElement> ListOfFilterOptionsByFilterName(IElement filter) => Browser.Locate.ElementByClassName(SortFilterDropdownContentClass, filter).FindElement(By.TagName(HtmlTextWriterTag.Ul.ToString())).FindElements(By.ClassName(SortFilterAttributeValueClass));
        public override ReadOnlyCollection<IElement> ListOfFilterUrlsByFilterName(IElement filter) => Browser.Locate.ElementByClassName(SortFilterDropdownContentClass, filter).FindElement(By.TagName(HtmlTextWriterTag.Ul.ToString())).FindElements(By.TagName(HtmlTextWriterTag.A.ToString()));
        public override ReadOnlyCollection<IElement> ListOfProductInfo => Browser.Locate.ElementById(SortResultProductsId).FindElements(By.ClassName(SortResultContainerClass));

        //List of elements that exist in Desktop view and not mobile.
        public override ReadOnlyCollection<IElement> ListOfBreadCrumbRemoveLinks => BreadCrumbElement.FindElements(By.XPath("//*[contains(@class,\"lpIcon-close01\")]"));
        public override ReadOnlyCollection<IElement> ListOfSortResultContainers => Browser.Locate.ElementsByClassName(SortResultContainerClass);
        public override ReadOnlyCollection<IElement> DisplayedFilters => Browser.Locate.ElementsByClassName(SortFilterAttrGroupDropdownClass);
        public override ReadOnlyCollection<IElement> SortResultLinks => Browser.Locate.ElementsBySelector(SortResultLinkClass.ToCssClassSelector());
        public override ReadOnlyCollection<IElement> ListOfAvailableFilters => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> FirstFilterOption(int index) => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfMobileFilterOptions => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfMobileFilterUrlsByFilterName => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfRandomFilterOptions(int index) => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListCalloutsElements => throw new NotImplementedException();
      
        #endregion

        public override void ClickFilterAttributeMenu(string attributeName, string attributeValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void WaitForAttributeMenu(IElement element)
        {
            Browser.MouseOverOnElement(Browser.Wait.ForClickableElement(element));
            Browser.Wait.ForDomReady();
        }

        /// <inheritdoc />
        public override void ExpandAllFilters()
        {
            if (IsMoreFiltersElementVisibleOnSortPage()) { MoreFiltersElement.Click(); }
        }

        /// <inheritdoc />
        public override List<string> ListOfBreadCrumbTexts => BreadCrumbElement.Text.Split('/').ToList();

        /// <inheritdoc />
        public override bool DoesProductHaveSoldOutCallOut(string sku) => GetSkuContainerElement(sku).FindElements(By.ClassName(SortCallout2Class)).Any();

        /// <inheritdoc />
        public override string GetProductNameBySku(string sku) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, GetSkuContainerElement(sku)).Text;

        /// <inheritdoc />
        public override string GetProductPriceBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.ClassName(SalePriceClass)).Text;

        public override string GetFirstProductPrice => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Img, SortResultProdImgClass).GetAttribute(GlobalLocators.DataPriceAttribute);

        /// <inheritdoc />
        public override Dictionary<string, string>[] ApplyFilters(int numberOfFilters, bool applyFiltersInOrder = false, Dictionary<string, string>[] predefinedFilters = null)
        {
            var appliedFilters = new List<Dictionary<string, string>>();

            if (applyFiltersInOrder)
            {
                var filters = GetFiltersInCanonicalOrder;

                var filterAndFilterOption = new Dictionary<string, string>();

                foreach (var filter in filters)
                {
                    var filterOption = ApplyFilterOption(filter, e => ListOfFilterOptionsByFilterName(e)[0]);

                    filterAndFilterOption.Add(filter, filterOption);
                    appliedFilters.Add(filterAndFilterOption);
                }
            }

            if (predefinedFilters != null)
            {
                ExpandAllFilters();

                var filter = predefinedFilters[0].ElementAt(0).Value;
                var filterOption = predefinedFilters[0].ElementAt(1).Value;

                ApplyPredefinedFilterOption(filter, filterOption);

                var filterAndFilterOption = new Dictionary<string, string>();
                filterAndFilterOption.Add(filter, filterOption);
                appliedFilters.Add(filterAndFilterOption);
            }

            else
            {
                for (var i = 0; i < numberOfFilters; i++)
                {
                    // Only apply a displayed filter. More Filters button shows hidden filters.
                    if (ListOfFilterAttributes == null || ListOfFilterAttributes.Count == 0)
                        break;

                    var specificFilters = GetSpecificFilters;
                    var displayedFilters = new List<string>();

                    foreach (var displayedFilter in DisplayedFilters)
                    {
                        displayedFilters.Add(displayedFilter.GetAttribute(GlobalLocators.DataUnbxdFacetTypeString));
                    }

                    var availableFilters = specificFilters.Intersect(displayedFilters).ToList();

                    if (availableFilters.Count == 0) break;

                    appliedFilters.Add(GetRandomAppliedFilterOption()); // click to apply filters
                    Browser.Wait.ForDomReady(30);
                }
            }

            return appliedFilters.ToArray();
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetRandomAppliedFilterOption(List<string> unWantedFilters = null)
        {
            var filters = new Dictionary<string, string>();

            ExpandAllFilters();

            var specificFilters = GetSpecificFilters;

            var displayedFilters = new List<string>();

            foreach (var displayedFilter in DisplayedFilters)
            {
                displayedFilters.Add(displayedFilter.GetAttribute(GlobalLocators.DataUnbxdFacetTypeString));
            }

            var availableFilters = unWantedFilters != null ? displayedFilters.Except(unWantedFilters).ToList() : specificFilters.Intersect(displayedFilters).ToList();

            // Only apply a filter from a pre-determined list.
            var filterListIndex = MathHelper.GetRandomNumber(availableFilters.Count);

            var filterName = availableFilters[filterListIndex];

            filters.Add("name", filterName);

            SelectedFilter(filterName).Click();
            Browser.Wait.ForDomReady();

            // Select a random filter option from the selected filter above.
            var randomFilterOptionIndex = MathHelper.GetRandomNumber(ListOfFilterOptionsByFilterName(SelectedFilter(filterName)).Count);
            var filterOption = ListOfFilterOptionsByFilterName(SelectedFilter(filterName))[randomFilterOptionIndex];
            var filterOptionUrl = ListOfFilterUrlsByFilterName(SelectedFilter(filterName))[randomFilterOptionIndex];

            filters.Add("value", filterOption.GetAttribute(GlobalLocators.DataUnbxdFacetNameString));

            var a = filterOptionUrl.GetAttribute(HtmlTextWriterAttribute.Href.ToString()).Split('/');
            var b = Browser.PageUrl.Split('/');
            var diff = a.Except(b);

            filters.Add("urlFragment", diff.FirstOrDefault());

            Browser.Wait.ForDisplayedElement(filterOption,15);

            filterOption.Click();

            Browser.Wait.UntilElementUnloads(filterOption);

            return filters;
        }

        /// <inheritdoc />
        public override void OpenMobileSortFilterMenu() => throw new NotImplementedException();

        public override void ToggleAppliedFiltersDropdown() => throw new NotImplementedException();

        public override void ClickFilterButton() => throw new NotImplementedException();

        public override void ClickFilterButtonAndOpenAppliedFiltersDropdown() => throw new NotImplementedException();

        /// <inheritdoc />
        public override string GetQuantityLeftForSkuOnSort(string sku)
        {
            return Browser.Wait.ForElement(
                    Browser.Locate.ElementBySelector($"{sku.ToUpper().ToTagNameAndAttributeCssSelector(HtmlTextWriterTag.Div, "data-sku")} {SortCallout6Class.ToCssClassSelector()}"), 5)
                .Text.Replace(" Left", string.Empty);
        }

        /// <inheritdoc />
		public override void ClickAttributeMenu(string attributeName, string attributeValue)
        {
            ApplyFilterOption(attributeName, e =>
            {
                ReadOnlyCollection<IElement> attributeMenuItems = GetAttributesOfAttributeMenu(e);
                return Browser.Wait.ForClickableElement(Browser.Locate.ElementWithText(attributeMenuItems, AttributeSelectorType.Equals, attributeValue));
            });
        }

        public override IElement GetRandomProductContainer()
        {
            return Browser.Locate.ElementByClassName(SortResultContainerClass);
        }

        public override void ApplyPriceFilter(string minPrice, string maxPrice)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(DivAttMenuId.ToCssIdSelector()));

            if (MoreFiltersElement.Displayed)
            {
                MoreFiltersElement.Click();
                Browser.Wait.ForClickableElement(PriceFilterElement).Click();
            }

            Browser.Wait.ForDisplayedElement(MinPriceFilterField, 15).SendKeys(minPrice);

            MaxPriceFilterField.SendKeys(maxPrice);
            FilterPriceButtonElement.Click();
        }
        
        private string ApplyFilterOption(string filter, Func<IElement, IElement> getFilterOption)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_testsBase.Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            //Click Filter menu
            var attributeMenu = Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, filter);
            var baseFilter = Browser.Wait.ForClickableElement(attributeMenu, 30);
            Browser.Locate.ElementByXpath($"//button[@id='btnAttributeGroup__{filter}']/span/div").Click();

            //Click Filter option
            var filterOption = getFilterOption(baseFilter);
            var filterOptionText = filterOption.Text;
            Browser.Locate.ElementByXpath($"//div[@class='SortFilterAttributeValue__name' and text()='{filterOptionText}']").Click();
            
            Browser.Wait.IsInvisibleElement(By.CssSelector(SortFilterDropdownContentClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            return filterOptionText;
        }

        private void ApplyPredefinedFilterOption(string filter, string prefedinedOption)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_testsBase.Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            //Click Filter menu
            Browser.Locate.ElementByXpath($"//button[@id='btnAttributeGroup__{filter}']/span/div").Click();

            //Click Filter option
            Browser.Locate.ElementByXpath($"//div[@class='SortFilterAttributeValue__name' and text()='{prefedinedOption}']").Click();
            
            Browser.Wait.IsInvisibleElement(By.CssSelector(SortFilterDropdownContentClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();
        }

        public override void WaitForFilterLink()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
        }

        public override void SearchLampsplusChoiceProduct(Databases.Entities.ProductModel product)
        {
            var finish = product.Finish;
            var style = product.Style;
            var usage = product.Usage;
            var type = product.Type;
            var sku = product.ShortSku;

            _testsBase.ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(_testsBase.ProductDetail.PdAddToPortfolioNormalId.ToCssIdSelector()));

            var price = _testsBase.ProductDetail.GetProductPrice();
            
            var hypenInCategory = product.Category.Replace(" ", "-").ToLower();
            var url = "https://www.lampsplus.com/products/" + hypenInCategory + "/";

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

        public override string NavigationToMoreLikeThisPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(SortMoreLikeThisBtnClass.ToCssClassSelector()));

            Browser.Wait.ForClickableElement(MoreLikeThisButton).Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.ForPageWait(Urls.MoreLikeThisPageBaseUrl);

            return Browser.PageUrl;
        }

    }
}