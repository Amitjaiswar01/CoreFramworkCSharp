using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailColorPlus
{
    public class ProductDetailColorPlusDesktop : IProductDetailColorPlusDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public ProductDetailColorPlusDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}