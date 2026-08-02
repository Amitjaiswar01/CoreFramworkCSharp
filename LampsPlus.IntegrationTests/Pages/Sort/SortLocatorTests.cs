using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Sort
{
    public class SortLocatorDesktopTest : SortLocatorTests
    {
        public SortLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Sort")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateSortElementsTest(string config) => Locate(config);

        protected override void VerifyToggleSortFilterMenu()
        {
            VerifyElementNotImplemented(() => Sort.ToggleSortFilterMenuButton);
            VerifyElementNotImplemented(() => Sort.ToggleSortFilterMenuCloseButton);
        }

        protected override void VerifyFilterDropdowns()
        {
            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.ColorFilterElement));
            VerifyElementDisplayed(() => Sort.ColorFilterDropdownElement);
            VerifyElementNotImplemented(() => Sort.FilterOverlay);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.PriceFilterElement));
            VerifyElementDisplayed(() => Sort.PriceFilterDropdownElement);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.SaleFilterElement));
			VerifyElementDisplayed(() => Sort.SaleFilterDropdownElement);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.SpecialsFilterElement));
            VerifyElementDisplayed(() => Sort.SpecialsFilterDropdownElement);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.StyleFilterElement));
            VerifyElementDisplayed(() => Sort.StyleFilterDropdownElement);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(Sort.CategoryFilterElement));
            VerifyElementDisplayed(() => Sort.CategoryFilterDropdownElement);
        }

        protected override void OpenFilterDropDown(IElement element)
        {
            Browser.MouseOverOnElement(element);
        }

        protected override void CloseFilterDropDown() { }

        protected override void VerifyStickyFilter()
        {
            Browser.ScrollToBottomOfWindow();
            VerifyElementDisplayed(() => Sort.StickyFilterElement);
            Browser.ScrollToTopOfWindow();
        }

        protected override void VerifyActiveFilter()
        {
            VerifyElementNotImplemented(() => Sort.ActiveFilterElement);
        }

    }


    public class SortLocatorMobileTest : SortLocatorTests
    {
        public SortLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Sort")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateSortElementsTest(string config) => Locate(config);

        protected override void VerifyToggleSortFilterMenu()
        {
            VerifyElementDisplayed(() => Sort.ToggleSortFilterMenuButton);
            Sort.ToggleSortFilterMenuButton.Click();
            VerifyElementDisplayed(() => Sort.ToggleSortFilterMenuCloseButton);
            Sort.ToggleSortFilterMenuCloseButton.Click();
        }

        protected override void VerifyFilterDropdowns()
        {
            VerifyElementDisplayed(() => Sort.FilterOverlay);
            VerifyElementNotImplemented(() => Sort.ColorFilterDropdownElement);
            VerifyElementNotImplemented(() => Sort.PriceFilterDropdownElement);
            VerifyElementNotImplemented(() => Sort.SaleFilterDropdownElement);
			VerifyElementNotImplemented(() => Sort.SpecialsFilterDropdownElement);
			VerifyElementNotImplemented(() => Sort.StyleFilterDropdownElement);
			VerifyElementNotImplemented(() => Sort.CategoryFilterDropdownElement);
        }

        protected override void OpenFilterDropDown(IElement element)
        {
            Browser.Wait.ForClickableElement(element).Click();
        }
        protected override void CloseFilterDropDown()
        {
            Browser.Wait.ForDisplayedElement(Sort.ActiveFilterElement).Click();
        }

        protected override void VerifyStickyFilter()
        {
            VerifyElementDisplayed(() => Sort.StickyFilterElement);
        }

        protected override void VerifyActiveFilter()
        {
            VerifyElementDisplayed(() => Sort.ActiveFilterElement);
        }

    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "Sort")]
    public abstract class SortLocatorTests : PageObjectTestsBase
    {
        protected SortLocatorTests(ITestOutputHelper output) : base(output) { }
        
        public void Locate(string config)
        {
            InitializeFramework(config, Urls.AllChandeliersSortPageUrl);
            BuildElementsList(Sort);
           
            VerifyElementDisplayed(() => Sort.H1BeforeFilters);
            VerifyElementDisplayed(() => Sort.SortPaginationCurrentElement);
            VerifyStickyFilter();
            VerifyNumberOfResults();

            VerifyBreadcrumbsFilterElement();

            VerifyElementDisplayed(() => Sort.RandomProductElement);
            VerifyElementDisplayed(() => Sort.ProductDescriptionLinksElement);
            VerifyElementDisplayed(() => Sort.FirstDisplayedProductElement);
            VerifyElementDisplayed(() => Sort.FirstDisplayedProductLink);
            VerifyElementDisplayed(() => Sort.BodyElement);
            VerifyElementDisplayed(() => Sort.MoreFiltersElement);
            VerifyElementDisplayed(() => Sort.SortResultProducts);
            
            Browser.Navigate(Urls.PictureFramesSortUrl);

            Browser.Navigate(Urls.DailySalesChandeliersUrl);

            VerifyElementDisplayed(() => Sort.DailySaleCallout);
            VerifyElementDisplayed(() => Sort.DailySaleAndLimitedQtyItem);
            VerifyElementDisplayed(() => Sort.DailySaleAndLimitedQtyItems);

			VerifyFilters();

            Browser.Navigate(Urls.HomePageUrl);
            Search.ExecuteSearch("blue lamp");

            VerifyElementDisplayed(() => Sort.NextPageLinkElement);
            VerifyElementDisplayed(() => Sort.PrevPageLinkElement);
            VerifyElementDisplayed(() => Sort.ProdPriceElement);
            VerifyElementDisplayed(() => Sort.ProdInfo);
            VerifyElementDisplayed(() => Sort.SortPageH1Tag);
            VerifyElementDisplayed(() => Sort.ProductContainersList);
            VerifyElementDisplayed(() => Sort.MoreLikeThisButton);


            VerifyElementDisplayed(() => Sort.ListOfProductsOnSortPage);
            VerifyElementDisplayed(() => Sort.ListOfProductLinksSortPage);
            VerifyElementDisplayed(() => Sort.ListOfSaleProducts);
            VerifyElementDisplayed(() => Sort.ListOfProductContainer);
            VerifyElementDisplayed(() => Sort.ListOfSortResultContainers);

            Browser.Navigate(Urls.ClearancePageAllProductUrl);
            Browser.Wait.ForDomReady();

            SearchForItemWithQuantityLeftCallout();

            VerifyElementDisplayed(() => Sort.SortPaginationElement);
            VerifyElementDisplayed(() => Sort.SortPaginationRangeElement);
            
            VerifyToggleSortFilterMenu();

            Browser.Navigate(Urls.SubCategoryUrls["Home Decor"]);
            Browser.Wait.ForDomReady();
            VerifyElementDisplayed(() => Sort.LandingPageSplashImage);
        }

        protected abstract void VerifyToggleSortFilterMenu();

        protected abstract void VerifyFilterDropdowns();

        protected abstract void OpenFilterDropDown(IElement element);

        protected abstract void CloseFilterDropDown();

        protected abstract void VerifyStickyFilter();

        protected abstract void VerifyActiveFilter();

        /// <summary>
        /// Verify the sort page has the number of results displayed
        /// </summary>
        private void VerifyNumberOfResults()
        {
            VerifyElementDisplayed(() => Sort.NumberOfResultsElement);
        }

        /// <summary>
        /// Verify breadcrumbs based on environment
        /// </summary>
        private void VerifyBreadcrumbsFilterElement()
		{
			Sort.ApplyRandomFilter();

            VerifyElementDisplayed(() => Sort.BreadCrumbElement);
            VerifyElementDisplayed(() => Sort.ListOfBreadCrumbLinks);
            Browser.Wait.ForDisplayedElement(Sort.ListOfBreadCrumbRemoveLinks.First(), 1000);
            VerifyElementDisplayed(() => Sort.ListOfBreadCrumbRemoveLinks);
		}

		private void VerifyFilters()
		{
			Browser.Navigate(Urls.NotFooSearchPageUrl);

			Sort.ExpandAllFilters();
			Browser.Wait.ForDisplayedElement(Sort.PriceFilterElement, 2);

			VerifyElementDisplayed(() => Sort.FinishFilterElement);
			VerifyElementDisplayed(() => Sort.StyleFilterElement);
            VerifyElementDisplayed(() => Sort.ColorFilterElement);		    
            if (Sort.FiltersListElement.IsInitialized)
		    {
		        VerifyElementDisplayed(() => Sort.FiltersListElement);
            }           
            VerifyElementDisplayed(() => Sort.HeightFilterElement);
			VerifyElementDisplayed(() => Sort.SizeFilterElement);
			VerifyElementDisplayed(() => Sort.TypeFilterElement);
			VerifyElementDisplayed(() => Sort.PriceFilterElement);
			VerifyElementDisplayed(() => Sort.CategoryFilterElement);
			VerifyElementDisplayed(() => Sort.UsageFilterElement);
			VerifyElementDisplayed(() => Sort.WidthFilterElement);
			VerifyElementDisplayed(() => Sort.SaleFilterElement);
			VerifyElementDisplayed(() => Sort.NumberOfLightsFilterElement);
			VerifyElementDisplayed(() => Sort.SpecialsFilterElement);
			VerifyElementDisplayed(() => Sort.CustomerRatingFilterElement);
			VerifyElementDisplayed(() => Sort.BrandFilterElement);
			VerifyElementDisplayed(() => Sort.ListOfFilterAttributes);
			VerifyElementDisplayed(() => Sort.ListOfVisibleFilterAttributes);

            VerifyFilterDropdowns();

            OpenFilterDropDown(Sort.ListOfFilterAttributes.First());
			Browser.Wait.ForDisplayedElement(Sort.FirstAttributeSubmenuElement, 15);
			VerifyElementDisplayed(() => Sort.FirstAttributeSubmenuElement);
            CloseFilterDropDown();

            OpenFilterDropDown(Sort.PriceFilterElement);
		    Browser.Wait.ForDisplayedElement(Sort.MinPriceFilterField, 15);
            VerifyElementDisplayed(() => Sort.MinPriceFilterField);
			VerifyElementDisplayed(() => Sort.MaxPriceFilterField);
			VerifyElementDisplayed(() => Sort.FilterPriceButtonElement);
            VerifyActiveFilter();
            CloseFilterDropDown();
		    OpenFilterDropDown(Sort.ColorFilterElement);
		    VerifyElementDisplayed(() => Sort.ColorFilterFirstElement);
		    CloseFilterDropDown();
        }

        private void SearchForItemWithQuantityLeftCallout()
        {
            for (int i = 0; i < 10; i++)
            {

                if (Sort.FirstShortSkuProductWithQtyLeftCallout.InternalElement != null)
                {
                    Browser.ScrollToElement(Sort.FirstShortSkuProductWithQtyLeftCallout);
                    VerifyElementDisplayed(() => Sort.FirstShortSkuProductWithQtyLeftCallout);
                    break;
                }
                Browser.ScrollToBottomOfWindow();
                Browser.ExecuteJs("window.scrollBy(0,-800)");
                Browser.Wait.ForClickableElement(Sort.NextPageLinkElement).Click();
                Browser.Wait.ForDomReady();
            }
        }
    }
}
