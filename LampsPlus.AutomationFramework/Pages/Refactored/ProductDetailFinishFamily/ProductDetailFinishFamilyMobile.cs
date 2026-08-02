using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailFinishFamily
{
    public class ProductDetailFinishFamilyMobile : ProductDetailFinishFamilyDesktop, IProductDetailFinishFamilyMobile
    {
        public ProductDetailFinishFamilyMobile(IBrowser browser) : base(browser)
        {
        }
    }
}