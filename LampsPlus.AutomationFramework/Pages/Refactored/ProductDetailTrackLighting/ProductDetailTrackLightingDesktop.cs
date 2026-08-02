using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailTrackLighting
{
    public class ProductDetailTrackLightingDesktop : IProductDetailTrackLightingDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public ProductDetailTrackLightingDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}