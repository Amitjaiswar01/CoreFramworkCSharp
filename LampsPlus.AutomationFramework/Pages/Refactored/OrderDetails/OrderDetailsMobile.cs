using Automation.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails
{
    public class OrderDetailsMobile : OrderDetailsDesktop, IOrderDetailsMobile
    {
        //Class members
        private string _itemClass = "item";
        private string _itemsClass  = "items";
        private string _orderSummaryClass = "summary";

        protected override IElement OrderDetailsContainer => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Div, _itemsClass);
        protected override IElement OrderSummaryContainer => Browser.Locate.ElementByClassName(_orderSummaryClass);
        protected override ReadOnlyCollection<IElement> OrderDetailTableRows => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Div, _itemClass, OrderDetailsContainer);

        public OrderDetailsMobile(IBrowser browser) : base(browser)
        {
        }

        //Interface implementation
        public override string GetProductName(string shortSku) => Browser.Locate.ElementByClassName(ProductColClass, OrderDetailTableRows.First(r => r.Text.Contains(shortSku))).Text;
        public override string GetUnitPrice(string shortSku) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, OrderDetailTableRows.First(r => r.Text.Contains(shortSku)))[1].Text.Replace($"{PriceString} ", string.Empty);
        public override string GetProductQuantity(string shortSku) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, OrderDetailTableRows.First(r => r.Text.Contains(shortSku)))[0].Text.Replace($"{QuantityString} ", string.Empty);
        public override string GetProductStatus(string shortSku) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Strong, OrderDetailTableRows.First(r => r.Text.Contains(shortSku)))[1].Text;
    }
}