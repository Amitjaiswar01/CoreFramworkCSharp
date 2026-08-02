using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailMcp
{
    public class ProductDetailMcpMobile : ProductDetailMcpDesktop, IProductDetailMcpMobile
    {
        //Class members
        private string _productDescSelector  = "//*[@id='pnlProductDescriptionyCollapsibleButton']";
        private IElement ProductDescDropDown => Browser.Locate.ElementByXpath(_productDescSelector);
        private IElement PopularColorsDropdown => Browser.Locate.ElementById(PopularColorsId);

        public ProductDetailMcpMobile(IBrowser browser) : base(browser)
        {
        }

        //Interface implementation
        public override int GetPopularColorsCount()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(PopularColorsId.ToCssIdSelector()));

            Browser.ScrollIntoView(ProductDescDropDown, true);

            PopularColorsDropdown.Click();

            Browser.Wait.ForClickableElement(PopularColorsTop).Click();
            return ListOfPopularColors.Count;
        }
    }
}