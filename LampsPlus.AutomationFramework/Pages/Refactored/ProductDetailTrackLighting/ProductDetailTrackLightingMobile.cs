using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailTrackLighting
{
    public class ProductDetailTrackLightingMobile : ProductDetailTrackLightingDesktop, IProductDetailTrackLightingMobile
    {
        public ProductDetailTrackLightingMobile(IBrowser browser) : base(browser)
        {
        }
    }
}