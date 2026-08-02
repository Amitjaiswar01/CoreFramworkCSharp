using System;
using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    public class MobileOrderSummaryBlock : OrderSummaryBlockBase
    {
        public MobileOrderSummaryBlock(IBrowser browser, TestsBase testsBase) : base(browser, testsBase)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework { get; }

        #region Class Setup
        public static string ProductTotalString => "Product Total";
        #endregion

        #region CSS Selector Strings
        public override string CloseButtonXpath { get; } = "//*[@id='orderSummaryDrawer']/div/div/div[1]/button";
        public override string OrderSummaryId { get; } = "!orderSummary";
        public override string ProductNameClass { get; } = "orderSummaryProducts__name";
        public override string ProductPriceClass { get; } = "orderSummaryProducts__price--purchase";
        public override string ProductQtyClass { get; } = "orderSummaryProducts__qty";

        public override string OrderSummaryContainer => throw new NotImplementedException();
        #endregion



        #region Page Elements
        public override IElement CloseButton => Browser.Locate.ElementByXpath(CloseButtonXpath);
        public override IElement OrderSummaryElement => Browser.Locate.ElementById(OrderSummaryId);
        public override IElement ProductName => Browser.Locate.ElementBySelector(ProductNameClass.ToCssClassSelector());
        public override IElement ProductPrice => Browser.Locate.ElementBySelector(ProductPriceClass.ToCssClassSelector());
        public override IElement ProductQty => Browser.Locate.ElementBySelector(ProductQtyClass.ToCssClassSelector());

        public override IElement ProductTotalValue
		{
			get
			{
				return GetOrderSummaryLineValueElement(ProductTotalString);
			}
		}

  		public ReadOnlyCollection<IElement> OrderSummaryTotalLabels() => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsLabelClass);
		public ReadOnlyCollection<IElement> OrderSummaryTotalValues() => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsValueClass);
		#endregion


		public IElement GetOrderSummaryLineValueElement(string label)
		{
			int index = 0;

			for(; index < OrderSummaryTotalLabels().Count; index++)
			{
				if(OrderSummaryTotalLabels()[index].Text.StartsWith(label, StringComparison.OrdinalIgnoreCase))
					break;
			}

			if(index < 0)
				return new Element();

			return OrderSummaryTotalValues()[index];
		}
	}
}
