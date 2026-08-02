namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior for the Sort page workflow.
    /// </summary>
    public interface ISortWorkflow
    {
        /// <summary>
        /// Finds Nth product by Sort Position on given sort page Url.
        /// </summary>
        /// <param name="sortPageUrl"></param>
        /// <param name="position"></param>
        void FindNthProductByPosition(string sortPageUrl, int position);

        /// <summary>
        /// Find a single product.
        /// </summary>
        /// <param name="url">URL to navigate to to find a product.</param>
        /// <param name="skuValue">Expected Sku for the first product.</param>
        /// <param name="priceValue">Expected price for the given product.</param>
        void FindSingleProduct(string url, out string skuValue, out string priceValue);

        /// <summary>
        /// Find a single product in the first available product category.
        /// </summary>
        /// <param name="category"></param>
        void FindSingleProductByCategoryWithAttributes(string category);

        /// <summary>
        /// Find a single product with the given attribute and category.
        /// </summary>
        /// <param name="category">Category of product to navigate to.</param>
        /// <param name="attributeCount">Attribute of product to find.</param>
        void FindSingleProductWithAttributesByCategory(string category, int attributeCount);

        /// <summary>
        /// Navigate to the first attribute filter on the sort page.
        /// </summary>
        void GoToFirstAttributeFilterSortPage();

        /// <summary>
        /// Go to the first filter attribute on the given category page.
        /// </summary>
        /// <param name="category">Category to navigate to.</param>
        void GoToFirstAttributeFilterSortPageByCategory(string category);

        /// <summary>
        /// Navigate to the given product category.
        /// </summary>
        /// <param name="category"></param>
        void GoToSortByCategory(string category);

        /// <summary>
        /// Does the SKU match for the PLA product on the Sort and PDP pages?
        /// </summary>
        /// <param name="url">URL of a product to navigate to.</param>
        /// <param name="sku">SKU of a PLA product to navigate to.</param>
        /// <returns></returns>
        bool IsMatchSkuPlaAndPdp(string url, string sku);

        /// <summary>
        /// Visits Most popular product pdp that has questions and answers from 1 of 4 lamp sort pages.
        /// </summary>
        void VisitMostPopularLampProductThatHasQuestionsAndAnswers();
    }
}
