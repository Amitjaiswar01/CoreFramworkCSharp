using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailMultiProduct
{
    public class ProductDetailMultiProductMobile : ProductDetailMultiProductDesktop, IProductDetailMultiProductMobile
    {
        public ProductDetailMultiProductMobile(IBrowser browser) : base(browser)
        {
        }
    }
}