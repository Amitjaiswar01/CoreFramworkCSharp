using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailMultiProduct
{
    public class ProductDetailMultiProductDesktop : IProductDetailMultiProductDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public ProductDetailMultiProductDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}