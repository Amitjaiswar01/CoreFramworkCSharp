using OpenQA.Selenium;
using Automation.Framework;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock
{
    public class OrderSummaryBlockMobile : OrderSummaryBlockDesktop, IOrderSummaryBlockMobile
    {
        private string _productNameClass = "orderSummaryProducts__name";
        private string _productPriceClass = "orderSummaryProducts__price--purchase";
        private string _productQuantityClass = "orderSummaryProducts__qty";
        private string _productSkuClass = "orderSummaryProducts__sku";
        private string _closeOrderSummaryBlockSelector = "hideMobileDrawer";
        private string _cartIconClass = "navCart";

        private IElement CartIcon => Browser.Locate.ElementByClassName(_cartIconClass);
        private IElement ProductName => Browser.Locate.ElementByClassName(_productNameClass);
        private IElement ProductPrice => Browser.Locate.ElementByClassName(_productPriceClass);
        private IElement ProductQuantity => Browser.Locate.ElementByClassName(_productQuantityClass);
        private IElement ShortSku => Browser.Locate.ElementByClassName(_productSkuClass);
        private IElement CloseButton => Browser.Locate.ElementByClassName(_closeOrderSummaryBlockSelector);

        public OrderSummaryBlockMobile(IBrowser browser, OperatingSystem operatingSystem) : base(browser, operatingSystem) { }

        public string GetProductName()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_productNameClass));
            return ProductName.Text;
        }

        public string GetProductPrice()
        {
            return ProductPrice.Text.Replace("$", string.Empty);
        }

        public string GetProductQuantity()
        {
            return ProductQuantity.Text;
        }

        public string GetShortSku()
        {
            return ShortSku.Text.Replace("Style # ", string.Empty);
        }

        public void OpenOrderSummaryDrawer()
        {
            CartIcon.Click();
        }

        public void CloseOrderSummaryDrawer()
        {
            CloseButton.Click();
        }
    }
}
