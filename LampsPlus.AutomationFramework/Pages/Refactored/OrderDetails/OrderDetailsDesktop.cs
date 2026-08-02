using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails
{
    public class OrderDetailsDesktop : IOrderDetailsDesktop
    {
        //Class members
        private string _tableClass = "table";
        private string _lpContainerId = "lpContainer";
        private string _rowClass = "row";
        private string _summaryClass = "summary";
        private string _openBoxLabelClass = "open-box-label";
        private string _requestReturnXpath = "//*[contains(text(),'Request a Return')]";
        private string _certonaWrapperClass = "certonaWrapper";
        protected string DecodeHtmlString(string value) => HttpUtility.HtmlDecode(value);
        protected string ProductColClass => "productCol";
        protected string PriceString => "Price:";
        protected string QuantityString => "Qty:";
        protected string StatusColClass => "statusCol";

        private IElement OpenBoxBadge => Browser.Locate.ElementByClassName(_openBoxLabelClass);
        private IElement OrderSummarySection => Browser.Locate.ElementByClassName(_summaryClass);
        private IElement TableContainer => Browser.Locate.ElementByClassName(_tableClass);
        private IElement RequestReturnLink => Browser.Locate.ElementByXpath(_requestReturnXpath);
        protected IElement MoreYouMayLikeSection => Browser.Locate.ElementByClassName(_certonaWrapperClass);
        protected virtual IElement OrderSummaryContainer => Browser.Locate.ElementById(_lpContainerId);

        protected virtual IElement OrderDetailsContainer => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Table, OrderSummaryContainer);

        protected virtual ReadOnlyCollection<IElement> OrderDetailTableRows => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Table} {HtmlTextWriterTag.Tbody} {HtmlTextWriterTag.Tr}", TableContainer);

        protected IElement GetOrderSummaryRowItem(string rowLabel)
        {
            var rows = Browser.Locate.ElementsByClassName(_rowClass, OrderSummarySection);
            foreach (var row in rows)
            {
                var span = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, row);

                // return the next element which contains the row value when the specified row rowLabel is found
                if (string.Equals(span.Text, rowLabel, StringComparison.OrdinalIgnoreCase))
                {
                    return Browser.Locate.NextSiblingElement(span);
                }

            }

            return null;
        }


        //Instances
        protected IBrowser Browser;

        public OrderDetailsDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
        public string FormatPrice(decimal price) => '$' + string.Format("{0:n}", price);

        public string GetDbProductName(OrderHistoryItems orderDetailItem)
        {
            return DecodeHtmlString($"{orderDetailItem.ProductName} ({orderDetailItem.ShortSku})");
        }

        public virtual string GetProductName(String shortSku)
        {
            if (OpenBoxBadge.IsInitialized)
            {
                return Browser.Locate.ElementByClassName(ProductColClass, OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(shortSku))).Text.TrimStart().Replace("OPEN BOX ITEM", "").Trim();
            }
            else
            {
               return Browser.Locate.ElementByClassName(ProductColClass, OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(shortSku))).Text.TrimStart(); 
            }
        }

        public virtual string GetUnitPrice(string shortSku) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(shortSku)))[3].Text.Replace($"{PriceString} ", string.Empty);

        public virtual string GetProductQuantity(string shortSku) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(shortSku)))[4].Text.Replace($"{QuantityString} ", string.Empty);

        public virtual string GetProductStatus(string shortSku) => Browser.Locate.ElementByClassName(StatusColClass, OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(shortSku))).Text;

        public string GetStatusString(OrderHistoryItems orderDetailItem)
        {
            if (orderDetailItem.FirstDeliveryDate.HasValue && !orderDetailItem.LastDeliveryDate.HasValue &&
                orderDetailItem.OrderStatus == "Shipped") return $"Shipped {orderDetailItem.ShipDate:MMM. d, yyyy}";
            if (orderDetailItem.FirstDeliveryDate.HasValue && !orderDetailItem.LastDeliveryDate.HasValue &&
                orderDetailItem.TrackingType != "Overnight") return "Status Pending";
            if (orderDetailItem.OrderStatus == "Canceled") return "Cancelled";
            if (orderDetailItem.OrderStatus == "Shipped") return $"Shipped {orderDetailItem.ShipDate:MMM. d, yyyy}";
            if (orderDetailItem.LastDeliveryDate.HasValue)
                return
                    $"Arrives {orderDetailItem.FirstDeliveryDate:MMM. d} - {orderDetailItem.LastDeliveryDate:MMM. d}";

            return $"Arrives {orderDetailItem.FirstDeliveryDate:MMM. d}";
        }

        public string GetOrderTotal => GetOrderSummaryRowItem("Order Total:").Text;

        public void NavigateToRequestReturnModal()
        {
            RequestReturnLink.Click();
        }
    }
}
