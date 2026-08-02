using OpenQA.Selenium;
using System;
using Automation.Framework;
using Automation.Framework.Utilities;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock
{
    public class OrderSummaryBlockDesktop : IOrderSummaryBlockDesktop
    {
        //Class Members
        private string _osValueClass = "osValue";
        private string _proceedPaymentId = "proceedPayment"; 
        private string _editOrderClass = "editOrder";


        private IElement EditCartButton => Browser.Locate.ElementByClassName(_editOrderClass);
        protected IElement OrderSummaryBlockElement(int index) => Browser.Locate.ElementsByClassName(_osValueClass)[index];
        protected IElement ProceedToPaymentButton => Browser.Locate.ElementById(_proceedPaymentId);

        //Instances
        protected IBrowser Browser;
        private readonly OperatingSystem _operatingSystem;

        public OrderSummaryBlockDesktop(IBrowser browser, OperatingSystem operatingSystem)
        {
            Browser = browser;
            _operatingSystem = operatingSystem;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public bool IsOrderSummaryKioskPriceStatusVisible()
        {
            return Browser.Wait.IsVisibleElement(By.XPath("//*[text()='POS']"));
        }

        public decimal GetKioskProductTotalPrice()
        {
            const int orderSummaryKioskProductTotalIndex = 7;
            var kioskTotal = Convert.ToDecimal(OrderSummaryBlockElement(orderSummaryKioskProductTotalIndex).Text.Replace("$", ""));

            return kioskTotal;
        }

        public void ClickProceedToPaymentButton()
        {
            if (_operatingSystem == OperatingSystem.iPad)
            {
                Browser.ScrollIntoView(ProceedToPaymentButton);
                Browser.ScrollToByPixelsVertical("-70");
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(ProceedToPaymentButton, ref xElementCoordinate, ref yElementCoordinate, 110);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }
            else
            {
                Browser.Wait.ForClickableElement(ProceedToPaymentButton);
                ProceedToPaymentButton.Click();
            }
        }

        public bool IsProceedToPaymentButtonVisible() => Browser.Wait.IsVisibleElement(By.CssSelector(_proceedPaymentId.ToCssIdSelector()));

        public void NavigateBackToCartOverviewPage()
        {
            EditCartButton.Click();
        }
    }
}