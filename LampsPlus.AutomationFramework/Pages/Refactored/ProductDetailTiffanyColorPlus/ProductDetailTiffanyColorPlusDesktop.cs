using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailTiffanyColorPlus
{
    public class ProductDetailTiffanyColorPlusDesktop : IProductDetailTiffanyColorPlusDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public ProductDetailTiffanyColorPlusDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}