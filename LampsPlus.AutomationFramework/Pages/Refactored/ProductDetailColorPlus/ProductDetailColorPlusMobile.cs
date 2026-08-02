using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailColorPlus
{
    public class ProductDetailColorPlusMobile : ProductDetailColorPlusDesktop, IProductDetailColorPlusMobile
    {
        public ProductDetailColorPlusMobile(IBrowser browser) : base(browser)
        {
        }
    }
}