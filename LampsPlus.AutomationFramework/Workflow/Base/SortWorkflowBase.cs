using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior for sort pages.
    /// </summary>
    public abstract class SortWorkflowBase : WorkflowBase, ISortWorkflow
    {
        protected SortWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public void FindSingleProduct(string url, out string skuValue, out string priceValue)
        {
            TestsBase.Sort.Navigate(url);
            Browser.Wait.ForDomReady();
            skuValue = TestsBase.Sort.FirstDisplayedProductElement.GetAttribute(TestsBase.GlobalLocators.DataSkuAttribute);
            priceValue = TestsBase.Sort.FirstDisplayedProductElement.GetAttribute("data-price");
        }

        /// <inheritdoc />
        public void GoToSortByCategory(string category)
        {
            TestsBase.Sort.Navigate($"{Urls.HomePageUrl}/{Urls.ProductsUrlDirectory}/{category}");
        }

        /// <inheritdoc />
        public void GoToFirstAttributeFilterSortPageByCategory(string category)
        {
            GoToSortByCategory(category);
            GoToFirstAttributeFilterSortPage();
        }

        /// <inheritdoc />
        public void GoToFirstAttributeFilterSortPage()
        {
            TestsBase.Sort.Navigate(TestsBase.Sort.GetFirstAttributeFilterHrefValue);
        }

        /// <inheritdoc />
        public void FindSingleProductWithAttributesByCategory(string category, int attributeCount)
        {
            GoToFirstAttributeFilterSortPageByCategory(category);
            for (var i = 1; i < attributeCount; i++)
            {
                GoToFirstAttributeFilterSortPage();
            }
            TestsBase.Sort.FirstDisplayedProductElement.Click();
        }

        /// <inheritdoc />
        public void FindSingleProductByCategoryWithAttributes(string category)
        {
            FindSingleProductWithAttributesByCategory(category, 1);
        }

        /// <inheritdoc />
        public abstract void VisitMostPopularLampProductThatHasQuestionsAndAnswers();

        /// <inheritdoc />
        public bool IsMatchSkuPlaAndPdp(string url, string sku)
        {
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.Wait.IsVisibleElement(By.ClassName(TestsBase.SortPla.PlaMainImageClass));
            if (TestsBase.SortPla.PlaFrameElement != null)
            {
                Browser.Wait.IsVisibleElement(By.Id(TestsBase.SortPla.QlViewDetailsId));
                Browser.ClickByJs(TestsBase.SortPla.PlaMoreDetailsLinkElement);
                Browser.SwitchToDefaultContent();
                Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ProductDetail.PdProdSkuId.ToCssIdSelector()));
                Browser.Wait.ForDomReady();
                if (TestsBase.SortPla.PdpBodyElement != null && (string.CompareOrdinal(sku.ToLower(), TestsBase.ProductDetail.GetTitleSku.ToLower()) == 0)) { return true; }
            }

            return false;
        }

        /// <inheritdoc />
        public void FindNthProductByPosition(string sortPageUrl, int position)
        {
            Browser.Navigate(sortPageUrl);
            TestsBase.Sort.NthDisplayedProductElementForCertonaWidget(position).Click();
        }
    }
}
