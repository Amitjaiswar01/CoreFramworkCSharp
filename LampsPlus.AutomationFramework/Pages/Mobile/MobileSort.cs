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

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/wall-lamps/type_art-shade/.
    /// </summary>
    public class MobileSort : SortBase
    {
        /// <inheritdoc />
        public MobileSort(IBrowser browser, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, globalLocators, testsBase)
        {
            _testsBase = testsBase;
        }

        private readonly TestsBase _testsBase;

        #region CSS Selector Strings

        private string FilterOptionCloseButtonXpath { get; } = "//div[contains(@class,'SortFilterNestedDrawer')]//span[contains(@class, 'lpIcon-close02')]";
        private string OverlayContentClass { get; } = "OverlayContent";
        private string AppliedFilterClearButtonClass { get; } = "SortFilterAppliedFilterClearButton__container";
        private string ButtonTertiaryClass { get; } = "Button--tertiary";
        private string SortResultsXpath { get; } = "//div[contains(text(),\"results\")]";
        private string CollapsibleTitleClass { get; } = "CollapsibleTitle";
        private string AttributeValueGroupListClass { get; } = "attributeValueGroupList";

        public override string SearchFilterFieldClass { get; } = "adjacentButton";
        public override string OverlayContentWrapperCloseButtonClass { get; } = "Overlay__contentWrapper__closeButton";
        public override string OpenBoxSearchClass { get; } = "openBoxSearch__input";
        public override string SaleSearchFieldId { get; } = "searchSalePageMobile";
        public override string SearchBtnSalePageId { get; } = "searchBtnSalePageMobile";
        public override string SortFilterAttributeGroupClass { get; } = "SortFilterAttributeGroup";
        public override string SortFilterAttributeValueNameClass { get; } = "SortFilterAttributeValue__name";
        public override string SortFilterDisplaySetDrawerButtonGroupClass { get; } = "SortFilterDisplaySetDrawer__buttonGroup";
        public override string SortFilterAppliedFilterFilterValueClass { get; } = "SortFilterAppliedFilter__filterValue";
        public override string ProductTitleClass { get; } = "productTitle";
        public override string ProductTitleClassMobileSelector { get; } = ".sortResultInnerContainer > a";
        public override string TopFilterMenuId { get; } = "topFilterMenu";
        public override string TopFilterMenuXpath { get; } = "//*[@id='topFilterMenu']//button[contains(@class, 'toggleSortMenu')]";
        public override string TopFilterMenuClass { get; } = "#topFilterMenu [class*='Sort']";
        public override string FilterAttributeParentElement { get; } = "//*[@class='SortFilterDisplaySetDrawer__buttonGroup']";
        public override string SortFilterAppliedFiltersClass { get; } = "SortFilterAppliedFilters";
        public override string SortFilterAppliedFiltersCollapsibleClass { get; } = "SortFilterAppliedFiltersCollapsible";
        public override string SortFilterCategoryClass { get; } = "sortFilterCategory";
        public override string SortFilterGenericHeaderClass { get; } = "SortFilterGenericHeader";
        public override string SortFilterWrapperId { get; } = "sortFilterWrapper";
        public override string SortMenuId { get; } = "sortMenu";
        public override string ProductPriceClass { get; } = "productPrice";
        public override string CalloutWrapperClass { get; } = "tag";
        public override string HeaderClass { get; } = "heading";
        public override string ToggleSortMenuClass { get; } = "toggleSortMenu";
        public override string SortFilterButtonTriggerClass { get; } = "setDT-2";
        public override string SortFilterButtonTriggerSelector { get; } = ".setDT-2 [type='button']";
        public override string SortNormalClass { get; } = "normal";
        public override string CollapsibleDisclosureClass { get; } = "CollapsibleDisclosure";
        public override string LpmmAccordionSubMenu { get; } = "lpmmAccordion__submenu";
        public override string LpmmMenuOpenClass { get; } = "lpmmMenuOpen";
        public override string LpmmSubMenuClass { get; } = "lpmmSubMenu";
        public override string LpmmSubOpenClass { get; } = "lpmmSubOpen";
        public override string AppliedFiltersClass { get; } = "appliedFilters";
        public override string FilterListElementId { get; } = "sortFilterWrapper";
        public override string AccordionMenuHeaderClass { get; } = "lpmmAccordion__header";
        public override string BreadCrumbClass { get; } = "breadCrumb";
        public override string ProductContainerId { get; } = "sortResultProducts";
        public override string RecentlyViewedItemId { get; } = "recentlyViewedContainer";
        public override string ContainsTextSelector { get; } = "//*[contains(text(),";
        public override string SelectFilterByText { get; } = "//*[contains(@class,'lpmmSubMenu')]//*[contains(text(),";
        public override string CloseFiltersMenu { get; } = "//span[contains(@class, 'lpIcon-close02')]";
        public override string FiltersScrollableContainer { get; } = ".lpmmSubMenu.lpmmOpen .lpScrollContainer";
        public override string IsStickyClass { get; } = "is-sticky";
        public override string SortResultImgContainerClass { get; } = ".unveil";
        public override string QtyLeftClass { get; } = "normal";
        public override string SortPageImgXpath { get; } = "//*[@id='certonaItems']//img";
        public override string SearchFilterButtonXPath { get; } = "//*[@id='SearchBarFilter']//*[@class='calloutBtn']";
        public override string AvailableAtThisLocationId => throw new NotImplementedException();
        public string MymlItemXpath { get; } = "//*[@id='certonaItems']/div[1]"; 
        public override string SuggestedProductsContainerClass => throw new NotImplementedException();
        public override string RecentlyViewedItemClass => throw new NotImplementedException();
        public override string SplashImageClass => throw new NotImplementedException();
        public override string ShowMoreClass => throw new NotImplementedException();
        public override string SubBreadCrumbClass { get; } = "breadcrumbLevel"; 
        public override string SortResultProdInfoClass => throw new NotImplementedException();
        public override string MoreFiltersLinkXpath => throw new NotImplementedException();
        public override string SearchFilterButtonClass => throw new NotImplementedException();
        #endregion
        //#sortMenu
        //

        #region Page Elements
        //Elements that exist in both implementations but have different names.
        public override IElement SearchFilterField(int index) => Browser.Locate.ElementsByClassName(SearchFilterFieldClass)[index];
        public override IElement AppliedFiltersContainer => Browser.Locate.ElementByClassName(SortFilterAppliedFiltersCollapsibleClass);
        public override IElement BrandFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, BrandString);
        public override IElement CollapsibleFilterContainer => Browser.Locate.ElementByClassName(CollapsibleDisclosureClass);
        public override IElement ColorFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, ColorString);
        public override IElement ColorFilterFirstElement => Browser.Locate.ElementBySelector($"{LpmmSubMenuClass.ToCssClassSelector()} {HtmlTextWriterTag.Li.ToFirstChildSelector()} {HtmlTextWriterTag.A.ToString()}");
        public override IElement DailySaleCallout => Browser.Locate.ElementWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString);
        public override IElement DisplayedProductAtIndex(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementsBySelector(SortResultContainerClass.ToCssClassSelector())[index]);
        public override IElement LpDailySaleCalloutElement (int index) => Browser.Locate.ElementsWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString)[index];
        public override IElement FilterMenu => Browser.Locate.ElementBySelector($"{SortMenuId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li} > {HtmlTextWriterTag.Button}");
        public override IElement FilterOptionCloseButton => Browser.Locate.ElementByXpath(FilterOptionCloseButtonXpath);
        public override IElement FiltersSubMenu => Browser.Locate.ElementByClassName(LpmmSubMenuClass);
        public override IElement FilterSubMenuOpen => Browser.Locate.ElementByClassName(LpmmSubOpenClass);
        public override IElement FilterOverlay => Browser.Locate.ElementByClassName(LpmmMenuOpenClass);
        public override IElement FinishFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, FinishString);
        public override IElement FirstDisplayedProductElement => Browser.Locate.ElementBySelector(UnveilClass.ToCssClassSelector());
        public override IElement LandingPageSplashImage => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Img, HtmlTextWriterAttribute.Alt, "Home Decor");
        public override IElement MobileFilterOptions => Browser.Locate.ElementBySelector(OverlayContentClass.ToCssClassSelector());
        public override IElement MobileFilterElement =>Browser.Locate.ElementBySelector(SortFilterAttributeGroupClass.ToCssClassSelector());
        public override IElement MoreFiltersElement => Browser.Locate.ElementBySelector(TopFilterMenuClass);
        public override IElement OpenBoxSearchElement => Browser.Locate.ElementByClassName(OpenBoxSearchClass);
        public override IElement PriceFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, PriceString);
        public override IElement ProductDescriptionLinksElement => Browser.Locate.ElementByClassName(ProductTitleClass);
        public override IElement ProdInfo => Browser.Locate.ElementByAttributeEquals(ItemPropAttribute, SkuString);
        public override IElement ProdPriceElement => Browser.Locate.ElementByClassName(ProductPriceClass);
        public override IElement RandomProductElement => Browser.Locate.ElementsBySelector("#sortResultProducts [class='unveil']").OrderBy(x => new Guid()).First();
        public override IElement SaleFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, SaleString);
        public override IElement SaleSearchField => Browser.Locate.ElementById(SaleSearchFieldId);
        public override IElement SearchBtnSalePage => Browser.Locate.ElementById(SearchBtnSalePageId);
        public override IElement SelectedSkuContainer(string sku) => Browser.Locate.ElementBySelector(CalloutWrapperClass.ToCssClassSelector(), GetSkuContainerElement(sku));
        public override IElement SizeFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, SizeString);
        public override IElement SortFilterWrapper => Browser.Locate.ElementById(SortFilterWrapperId);
        public override IElement SortMenu => Browser.Locate.ElementById(SortMenuId);
        public override IElement FirstImageOnSort(int index) => Browser.Locate.ElementsByXpath(SortPageImgXpath)[index];
        public override IElement SortPageH1Tag => Browser.Locate.ElementByClassName(HeaderClass).FindElement(By.TagName(HtmlTextWriterTag.H1.ToString()));
        public override IElement SortPageImg(int index) => Browser.Locate.ElementByXpath($"//*[@class='imageWrapper ']//img[contains(@data-position, '{index + 1}')]");
        public override IElement SpecialsFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, SpecialsString);
        public override IElement TypeFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, TypeString);
        public override IElement UsageFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, UsageString);
        public override IElement StyleFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, StyleString);
        public override IElement HeightFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, HeightString);
        public override IElement CategoryFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, CategoryString);
        public override IElement WidthFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, WidthString);
        public override IElement NumberOfLightsFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, NumberOfLightsString);
        public override IElement NthDisplayedProductElement(int position) => Browser.Locate.ElementBySelector($".{SortResultContainerClass.ToNthChildSelector(position)}> {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.A}");
        public override IElement CustomerRatingFilterElement => Browser.Locate.ElementWithText(ListOfFilterAttributes, AttributeSelectorType.Equals, CustomerRatingString);
        public override IElement DailySaleAndLimitedQtyItem(int index) => Browser.Locate.ElementsWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString, LeftString)[index];
        public override IElement ToggleSortFilterMenuButton => Browser.Locate.ElementBySelector(ToggleSortMenuClass.ToCssClassSelector());
        public override IElement ToggleSortFilterMenuCloseButton => Browser.Locate.ElementByXpath(CloseFiltersMenu);
        public override IElement StickyFilterElement => Browser.Locate.ElementByClassName(FilterHeaderClass);
        public override IElement FiltersListElement => Browser.Locate.ElementById(FilterListElementId);
        public override IElement NumberOfResultsElement => Browser.Locate.ElementByXpath(SortResultsXpath);
        public override IElement ActiveFilterElement => Browser.Locate.ElementByClassNames(AccordionMenuHeaderClass, ActiveClass);
        public override IElement BreadCrumbElement => Browser.Locate.ElementByClassName(BreadCrumbClass);
        public override IElement RecentlyViewedItem => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementById(RecentlyViewedItemId));
        public override IElement QtyLeftCallout(int index) => Browser.Locate.ElementsByClassName(QtyLeftClass)[index];
        public override IElement SelectedFilter(string filter) => Browser.Locate.ElementByXpath($"//span/div[text()='{filter}']/ancestor::button[@class='Button']");
        public override IElement SaleItem(int index) => Browser.Locate.ElementByXpath($"//*[@class='sortResultInnerContainer']//parent::div[contains(@data-position, '{index + 1}')]");
        public override IElement SearchButtonFilter => Browser.Locate.ElementByXpath(SearchFilterButtonXPath);
        public override ReadOnlyCollection<IElement> FirstFilterOption(int index) => Browser.Locate.ElementsBySelector($"{SortFilterWrapperId.ToCssIdSelector()} > {HtmlTextWriterTag.Li.ToNthChildSelector(index)} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li.ToNthChildSelector(1)} > {HtmlTextWriterTag.A}");
        public override ReadOnlyCollection<IElement> ListOfAvailableFilters => Browser.Locate.ElementsBySelector($"{SortFilterWrapperId.ToCssIdSelector()} > {HtmlTextWriterTag.Li} > {HtmlTextWriterTag.A}");
        public override ReadOnlyCollection<IElement> ListOfFilterAttributes => Browser.Locate.ElementsByClassName(SortFilterDisplaySetDrawerButtonGroupClass);
        public override ReadOnlyCollection<IElement> ListOfProductContainer => Browser.Locate.ElementsByClassName(ImageWrapperClass, SortResultProducts);
        public override ReadOnlyCollection<IElement> ListOfProductLinksSortPage => Browser.Locate.ElementsBySelector(ProductTitleClassMobileSelector);
        public override ReadOnlyCollection<IElement> ListOfRandomFilterOptions(int index) => Browser.Locate.ElementsBySelector($"{SortFilterWrapperId.ToCssIdSelector()} > {HtmlTextWriterTag.Li.ToNthChildSelector(index)} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li} > {HtmlTextWriterTag.A}");
        public override ReadOnlyCollection<IElement> ListOfSaleProducts => Browser.Locate.ElementsByClassName(ProductPriceClass);
        public override ReadOnlyCollection<IElement> ListOfMobileFilterOptions => Browser.Locate.ElementsByClassName(SortFilterAttributeValueNameClass);
        public override ReadOnlyCollection<IElement> ListOfMobileFilterUrlsByFilterName => Browser.Locate.ElementByClassName(SortFilterAttributeGroupClass).FindElement(By.TagName(HtmlTextWriterTag.Ul.ToString())).FindElements(By.TagName(HtmlTextWriterTag.A.ToString()));
        public override ReadOnlyCollection<IElement> DailySaleAndLimitedQtyItems => Browser.Locate.ElementsWithText(ListCalloutsElements, AttributeSelectorType.Contains, DailyString, LeftString);
        public override ReadOnlyCollection<IElement> DisplayedFilters => Browser.Locate.ElementsByClassName(SortFilterDisplaySetDrawerButtonGroupClass);
        public override ReadOnlyCollection<IElement> ListOfProductInfo => Browser.Locate.ElementById(SortResultProductsId).FindElements(By.ClassName(SortResultContainerClass));
        public override ReadOnlyCollection<IElement> ListOfProductsOnSortPage => Browser.Locate.ElementsBySelector("div.sortResultInnerContainer > a");

        //List of elements that are implemented in Desktop view and not mobile view.

        public override ReadOnlyCollection<IElement> ListOfBreadCrumbRemoveLinks
        {
            get
            {
                var appliedFilters = Browser.Locate.ElementByClassName(SortFilterAppliedFiltersClass);

                if (!appliedFilters.Displayed)
                {
                    MoreFiltersElement.Click();
                }

                Browser.Wait.ForDisplayedElement(appliedFilters, 1000);

                return appliedFilters.FindElements(By.TagName(HtmlTextWriterTag.A.ToString()));
            }
        }

        public override ReadOnlyCollection<IElement> ListOfBreadCrumbNames => BreadCrumbElement.FindElements(By.ClassName(SubBreadCrumbClass));
        public override ReadOnlyCollection<IElement> ListOfSortResultContainers => Browser.Locate.ElementsByClassName(SortResultContainerClass);

        public override ReadOnlyCollection<IElement> ListCalloutsElements => Browser.Locate.ElementsBySelector(CalloutWrapperClass.ToCssClassSelector());

        //Elements that exist in Desktop view but not Mobile view.
        public override IElement SearchFilterButton(int index) => throw new NotImplementedException();
        public override IElement AvailableAtThisLocationCheckbox => throw new NotImplementedException();
        public override IElement MoreYouMayLikeContainer => throw new NotImplementedException();
        public override IElement StyleFilterDropdownElement => throw new NotImplementedException();
        public override IElement CategoryFilterDropdownElement => throw new NotImplementedException();
        public override IElement SaleFilterDropdownElement => throw new NotImplementedException();
        public override IElement SpecialsFilterDropdownElement => throw new NotImplementedException();
        public override IElement PriceFilterDropdownElement => throw new NotImplementedException();
        public override IElement ColorFilterDropdownElement => throw new NotImplementedException();
        public override IElement MymlItem => Browser.Locate.ElementByXpath(MymlItemXpath); 
        public override IElement LampsPlusChoice => throw new NotImplementedException();
        public override IElement RecentlyViewedContainer => Browser.Locate.ElementById(RecentlyViewedItemId);

        public override ReadOnlyCollection<IElement> ListOfFilterOptionsByFilterName(IElement filter) => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfFilterUrlsByFilterName(IElement filter) => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> SortResultLinks => Browser.Locate.ElementsByXpath("//div[@class='sortResultInnerContainer']/a[@href]");
        #endregion

        /// <inheritdoc />
        public override void WaitForAttributeMenu(IElement element)
        {
            element.Click();
        }

        /// <inheritdoc />
        public override void ExpandAllFilters() => MoreFiltersElement.Click();

        /// <inheritdoc />
        public override List<string> ListOfBreadCrumbTexts
        {
            get
            {
                if (MoreFiltersElement.GetAttribute(GlobalLocators.AriaExpandedAttribute) == "false")
                {
                    Browser.Wait.IsVisibleElement(By.CssSelector(TopFilterMenuClass));
                    MoreFiltersElement.Click();

                    Browser.Wait.IsVisibleElement(By.CssSelector(CollapsibleTitleClass.ToCssClassSelector()));
                    CollapsibleFilterContainer.Click();
                    Browser.Wait.ForElementToStopAnimating(AppliedFiltersContainer);
                }

                Browser.Wait.IsVisibleElement(By.CssSelector(CollapsibleTitleClass.ToCssClassSelector()));

                CollapsibleFilterContainer.Click();
                Browser.Wait.ForElementToStopAnimating(AppliedFiltersContainer);
                var appliedFilters = Browser.Locate.ElementByClassName(SortFilterAppliedFiltersClass);

                return appliedFilters.FindElements(By.ClassName(AppliedFilterClearButtonClass)).Select(e =>e.Text).ToList();
            }
        }

        /// <inheritdoc />
        public override bool DoesProductHaveSoldOutCallOut(string sku) => Browser.Locate.ElementWithText(ListCalloutsElements, AttributeSelectorType.Contains, SoldOutString).Displayed;

        /// <inheritdoc />
        public override IElement GetProductImageByContainer(IElement sortResultContainer) => Browser.Locate.ElementByClassName(UnveilClass, sortResultContainer);

        /// <inheritdoc />
        public override string GetProductNameBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.CssSelector(ProductTitleClass.ToCssClassSelector())).Text;

        /// <inheritdoc />
        public override string GetProductPriceBySku(string sku) => GetSkuContainerElement(sku).FindElement(By.CssSelector(SalePriceAmountClass.ToCssClassSelector())).Text;
        public override string GetFirstProductPrice => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Img, UnveilClass).GetAttribute(GlobalLocators.DataPriceAttribute);

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
                var filter = predefinedFilters[0].ElementAt(0).Value;
                var filterOption = predefinedFilters[0].ElementAt(1).Value;

                ApplyPredefinedFilterOption(filter, filterOption);

                var filterAndFilterOption = new Dictionary<string, string> { { filter, filterOption } };
                appliedFilters.Add(filterAndFilterOption);
            }

            else
            {
                for (var i = 0; i < numberOfFilters; i++)
                {
                    Browser.Wait.IsVisibleElement(By.CssSelector(TopFilterMenuClass));

                    MoreFiltersElement.Click();

                    Browser.Wait.IsVisibleElement(By.ClassName(CollapsibleTitleClass));

                    // Only apply a displayed filter. If no filters are available, close Filter menu.
                    if (ListOfFilterAttributes == null || ListOfFilterAttributes.Count == 0)
                    {
                        Browser.Wait.IsVisibleElement(By.XPath(CloseFiltersMenu));
                        ToggleSortFilterMenuCloseButton.Click();
                        break;
                    }

                    var specificFilters = GetSpecificFilters;

                    var displayedFilters = new List<string>();

                    foreach (var displayedFilter in DisplayedFilters)
                    {
                        displayedFilters.Add(displayedFilter.FindElement(By.ClassName(SortFilterGenericHeaderClass)).Text);
                    }

                    var availableFilters = specificFilters.Intersect(displayedFilters).ToList();

                    if (availableFilters.Count == 0) break;

                    appliedFilters.Add(GetRandomAppliedFilterOption());  // click to apply filters
                }
            }

            return appliedFilters.ToArray();
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetRandomAppliedFilterOption(List<string> unWantedFilters = null)
        {
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.DisplayedMobileDrawerMenu);

            var filters = new Dictionary<string, string>();

            var specificFilters = GetSpecificFilters;

            var displayedFilters = new List<string>();

            foreach (var displayedFilter in DisplayedFilters)
            {
                displayedFilters.Add(displayedFilter.FindElement(By.ClassName(SortFilterGenericHeaderClass)).Text);
            }

            var availableFilters = specificFilters.Intersect(displayedFilters).ToList();

            // Only apply a filter from a pre-determined list.
            var filterListIndex = MathHelper.GetRandomNumber(availableFilters.Count);
            var filterName = availableFilters[filterListIndex];

            filters.Add("name", filterName);

            Browser.Wait.ForElement(SelectedFilter(filterName));
            Browser.ScrollIntoView(SelectedFilter(filterName));

            SelectedFilter(filterName).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterAttributeGroupClass.ToCssClassSelector()));

            // Select a random filter option from the selected filter above.
            var randomFilterOptionIndex = MathHelper.GetRandomNumber(ListOfMobileFilterOptions.Count);
            var filterOption = ListOfMobileFilterOptions[randomFilterOptionIndex];
            var filterOptionUrl = ListOfMobileFilterUrlsByFilterName[randomFilterOptionIndex];

            filters.Add("value", filterOption.Text);

            var a = filterOptionUrl.GetAttribute(HtmlTextWriterAttribute.Href.ToString()).Split('/');
            var b = Browser.PageUrl.Split('/');
            var diff = a.Except(b);

            filters.Add("urlFragment", diff.FirstOrDefault());

            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterAttributeGroupClass.ToCssClassSelector()));
            Browser.ScrollIntoView(filterOption);
            filterOption.Click();

            Browser.Wait.UntilElementDoesntExist(OverlayContentWrapperCloseButtonClass);

            return filters;
        }

        /// <inheritdoc />
        public override void OpenMobileSortFilterMenu()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterButtonTriggerClass.ToCssClassSelector()));

            Browser.Wait.ForClickableElement(ToggleSortFilterMenuButton).Click();

            // Failsafe, sometimes one click doesn't do it.
            if (GlobalLocators.DisplayedMobileDrawerMenu == null)
            {
                ToggleSortFilterMenuButton.Click();
            }

            // Inner container is the animating element
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.MobileDrawerMenuInnerContainer);
        }

        /// <inheritdoc />
        public override string GetQuantityLeftForSkuOnSort(string sku)
        {
            return Browser.Wait.ForElement(
                    Browser.Locate.ElementBySelector($"{sku.ToUpper().ToTagNameAndAttributeCssSelector(HtmlTextWriterTag.Div, GlobalLocators.DataSkuAttribute)} {SortNormalClass.ToCssClassSelector()}"), 15)
                .Text.Replace(" Left", string.Empty);
        }

        /// <inheritdoc />
        public override void ClickAttributeMenu(string attributeName, string attributeValue)
        {
            ApplyFilterOption(attributeName, e =>
            {
                var attributeMenuItems = GetAttributesOfAttributeMenu(e);
                return Browser.Locate.ElementWithText(attributeMenuItems, AttributeSelectorType.Equals, attributeValue);
            });
        }

        public override void ClickFilterAttributeMenu(string filter, string attributeValue)
        {
            var attributeMenu = Browser.Locate.ElementByXpath($"{FilterAttributeParentElement}/a[text()='{filter}']");

            var baseFilter = Browser.Wait.ForClickableElement(attributeMenu);

            baseFilter.Click();

            Browser.Wait.ForElementToStopAnimating(baseFilter);

            var filterOption = Browser.Locate.ElementByXpath($"{SelectFilterByText}'{attributeValue}')]");

            Browser.ScrollIntoView(filterOption);

            filterOption.Click();
        }

        public override IElement GetRandomProductContainer()
        {
            return ListOfSortResultContainers.OrderBy(e => Guid.NewGuid()).First();
        }

        public override void ToggleAppliedFiltersDropdown()
        {
            if (Browser.Locate.ElementByClassName("CollapsibleTitle__caret--closed").IsInitialized && Browser.Locate.ElementByClassName("CollapsibleTitle__caret--closed").Displayed)
            {
                CollapsibleFilterContainer.Click();
                Browser.Wait.ForElementToStopAnimating(AppliedFiltersContainer);
            }
        }

        public override void ClickFilterButton()
        {
            Browser.Wait.IsVisibleElement(By.XPath(TopFilterMenuXpath));
            var button = ToggleSortFilterMenuButton;
            button.Click();
            Browser.Wait.WaitForAjaxComplete();
        }

        public override void ClickFilterButtonAndOpenAppliedFiltersDropdown()
        {
            ClickFilterButton();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.DisplayedMobileDrawerMenu);
            ToggleAppliedFiltersDropdown();
        }


        private string ApplyFilterOption(string filter, Func<IElement, IElement> getFilterOption)
        {
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.XPath(TopFilterMenuXpath));

            var button = Browser.Locate.ElementByXpath(TopFilterMenuXpath);
            button.Click();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.DisplayedMobileDrawerMenu);

            var selectedFilterAttribute = Browser.Locate.ElementByXpath($"{FilterAttributeParentElement}//div[text()='{filter}']");
            var availableFilters = DisplayedFilters;
            var visibleFilterOptions = new List<string>();

            foreach (var filters in availableFilters)
            {
                visibleFilterOptions.Add(filters.Text);
            }

            selectedFilterAttribute.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterAttributeGroupClass.ToCssClassSelector()));

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.MobileDrawerMenuInnerContainer);

            //Select first filter option
            var filterOptions = ListOfMobileFilterOptions[0];
            var filterOptionText = filterOptions.Text;

            filterOptions.Click();

            Browser.Wait.UntilElementDoesntExist(OverlayContentWrapperCloseButtonClass);
            Browser.Wait.ForDomReady();

            return filterOptionText;
        }

        private void ApplyPredefinedFilterOption(string filter, string prefedinedOption)
        {
            Browser.Wait.ForDomReady();

            //Open Filters mobile menu
            var button = Browser.Locate.ElementByXpath(TopFilterMenuXpath);
            button.Click();
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.DisplayedMobileDrawerMenu);

            //Click Filter
            var selectedFilterAttribute = Browser.Locate.ElementByXpath($"{FilterAttributeParentElement}//div[text()='{filter}']");
            selectedFilterAttribute.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterAttributeGroupClass.ToCssClassSelector()));
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.MobileDrawerMenuInnerContainer);

            //Click Filter option
            Browser.Locate.ElementByXpath($"//div[@class='SortFilterAttributeValue__name' and text()='{prefedinedOption}']").Click();

            Browser.Wait.UntilElementDoesntExist(OverlayContentWrapperCloseButtonClass);
            Browser.Wait.ForDomReady();
        }

        public override void ApplyPriceFilter(string minPrice, string maxPrice)
        {
            var url = Browser.PageUrl;
            var minDollar = minPrice.Remove(minPrice.IndexOf(".", StringComparison.Ordinal));
            var minCents = minPrice.Substring(minPrice.IndexOf(".", StringComparison.Ordinal) + 1);
            var maxDollar = maxPrice.Remove(maxPrice.IndexOf(".", StringComparison.Ordinal));
            var maxCents = maxPrice.Substring(maxPrice.IndexOf(".", StringComparison.Ordinal) + 1);

            Browser.Navigate($"{url}p_@@{minDollar}@@@{minCents}-@-@@{maxDollar}@@@{maxCents}");
        }

        public override void WaitForFilterLink()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(SortFilterButtonTriggerSelector),30);
        }
        public override void SearchLampsplusChoiceProduct(Databases.Entities.ProductModel product)
        {
            throw new NotImplementedException();
        }

        public override string NavigationToMoreLikeThisPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(SortMoreLikeThisBtnClass.ToCssClassSelector()));
            MoreLikeThisButton.Click();

            Browser.Wait.ForDomReady();
            Browser.Wait.ForPageWait(Urls.MoreLikeThisPageBaseUrl);

            return Browser.PageUrl;
        }

    }
}
