using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailTiffanyColorPlus
{
    public class ProductDetailTiffanyColorPlusMobile : ProductDetailTiffanyColorPlusDesktop, IProductDetailTiffanyColorPlusMobile
    {
        public ProductDetailTiffanyColorPlusMobile(IBrowser browser) : base(browser)
        {
        }
    }
}