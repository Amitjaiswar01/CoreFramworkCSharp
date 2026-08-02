namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior for checking out with one or more products.
    /// </summary>
    public class CheckoutWorkflowBase : WorkflowBase, ICheckoutWorkflow
    {
        public CheckoutWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <summary>
        /// Click the first product at the given URL.
        /// </summary>
        /// <param name="url"></param>
        public void SelectSingleProduct(string url)
        {
            TestsBase.Sort.Navigate(url);

            Browser.Wait.ForDomReady();

            Browser.ClickByJs(TestsBase.Sort.FirstDisplayedProductLink);

            Browser.Wait.ForDomReady();

        }
    }
}
