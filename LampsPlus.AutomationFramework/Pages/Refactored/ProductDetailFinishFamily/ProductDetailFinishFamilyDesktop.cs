using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailFinishFamily
{
    public class ProductDetailFinishFamilyDesktop : IProductDetailFinishFamilyDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public ProductDetailFinishFamilyDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}