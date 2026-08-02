using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using System;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Class to define all common elements in the Order Summary Block
    /// </summary>
    public class OrderSummaryBlock : OrderSummaryBlockBase
    {
        /// <inheritdoc />
        public OrderSummaryBlock(IBrowser browser, TestsBase testsBase) : base(browser, testsBase)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework;

        #region CSS Selector Strings
        public override string OrderSummaryContainer { get; } = "orderSummary";

        public override string CloseButtonXpath => throw new NotImplementedException();
        public override string OrderSummaryId => throw new NotImplementedException();
        public override string ProductNameClass => throw new NotImplementedException();
        public override string ProductPriceClass => throw new NotImplementedException();
        public override string ProductQtyClass => throw new NotImplementedException();
        #endregion



        #region Page Elements
        public override IElement OrderSummaryElement => Browser.Locate.ElementById(OrderSummaryContainer);
		public override IElement ProductTotalValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(0));

        public override IElement CloseButton => throw new NotImplementedException();
        public override IElement ProductName => throw new NotImplementedException();
        public override IElement ProductPrice => throw new NotImplementedException();
        public override IElement ProductQty => throw new NotImplementedException();
        #endregion

    }
}
