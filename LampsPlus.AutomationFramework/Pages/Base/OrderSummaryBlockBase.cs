using Automation.Framework;
using Automation.Framework.Core;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class OrderSummaryBlockBase : Page, IOrderSummaryBlock
    {
        /// <inheritdoc />
        protected OrderSummaryBlockBase(IBrowser browser, TestsBase testsBase) : base(browser)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework;

        #region CSS Selector Strings
        private string EditOrderClass { get; } = "editOrder";
        private string FrClass { get; } = "fr";
        private string GroupClass { get; } = "group";
        private string OrderSummaryContainerId { get; } = "orderSummaryContainer";
        private string OrderTotalClass { get; } = "orderTotal";

        public string ProceedPaymentId { get; } = "proceedPayment";

        public abstract string CloseButtonXpath { get; }
        public abstract string OrderSummaryContainer { get; }
        public abstract string OrderSummaryId { get; }
        public abstract string ProductNameClass { get; }
        public abstract string ProductPriceClass { get; }
        public abstract string ProductQtyClass { get; }
        #endregion

        #region Page Elements
        public IElement EditOrderButton => Browser.Locate.ElementByClassName(EditOrderClass);
        public IElement OrderSummaryBlockContainer => Browser.Locate.ElementById(OrderSummaryContainerId);
        public IElement OrderSummaryRow(int index) => Browser.Locate.ElementsByClassName(GroupClass, OrderSummaryElement)[index];
        public IElement OrderTotal => Browser.Locate.ElementByClassName(OrderTotalClass);
        public IElement OrderTotalValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderTotal);
        public IElement OrderSummaryBlockElement(int index) => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsValueClass)[index];
        public IElement PosProductTotal => Browser.Locate.ElementByClassNames(OrderSummaryElement, false, Framework.GlobalLocators.OsValueClass, FrClass);
        public IElement ProceedToPaymentButton => Browser.Locate.ElementById(ProceedPaymentId);
        public IElement ProductTotalLabel => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsLabelClass, OrderSummaryRow(0));
        public IElement PromoCodeLineLabel(int index) => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsLabelClass)[index];
        public IElement PromoCodeLineValue(int index) => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsValueClass)[index];
        public IElement ShippingAndProcessingLabel => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsLabelClass, OrderSummaryRow(1));
        public IElement ShippingAndProcessingValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(1));
        public IElement TaxLabel => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsLabelClass, OrderSummaryRow(2));
        public IElement TaxValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(2));

        public abstract IElement CloseButton {get;}
        public abstract IElement OrderSummaryElement { get; }
        public abstract IElement ProductName { get; }
        public abstract IElement ProductPrice { get; }
        public abstract IElement ProductQty { get; }
        public abstract IElement ProductTotalValue { get; }
        #endregion

        /// <inheritdoc />
        public void WaitForKioskPriceToUpdate()
        {
            Browser.Wait.ForCondition(() => PosProductTotal.Text != "$0.00");
        }

        /// <inheritdoc />
        public string GetPromoCodeLabel()
        {
            return PromoCodeLineLabel(1).Text; //2nd osLabel on OC page 
        }

        /// <inheritdoc />
        public string GetAdditionalDiscounts()
        {
            return OrderSummaryBlockElement(1).Text.Replace("-", string.Empty).Trim();
        }

        /// <inheritdoc />
        public string GetSandP()
        {
            return OrderSummaryBlockElement(3).Text;
        }
    }
}
