using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Pages.Desktop;
using OpenQA.Selenium;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class OrderDetailsBase : Page, IOrderDetails
    {
        /// <inheritdoc />
        protected OrderDetailsBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string DoneButtonClass { get; } = "done_button";
        private string DoubleOptinWarnNotShowCheckboxId { get; } = "doubleOptinWarnNotShowCheckbox";
        private string PaymentMethodClass { get; } = "paymentMethod";
        private string RowClass { get; } = "row";
        private string SectionClass { get; } = "section";
        private string SummaryClass { get; } = "summary";
        private string TableClass { get; } = "table";

        public string BreadCrumbClass { get; } = "sortBreadCrumb";
        public string DetailsClass { get; } = "details";
        public string DialogWindowClass { get; } = "dialog_window";
        public string LpContainerId { get; } = "lpContainer";
        public string PriceString { get; } = "Price:";
        public string ProductColClass { get; } = "productCol";
        public string QuantityString { get; } = "Qty:";
        public string ShippingColClass { get; } = "shippingCol";
        public string StatusColClass { get; } = "statusCol";
        public string OrderIdRmaHeadingXPath { get; } = "//*[@class='order']";  

        public abstract string OpenCartResultsClass { get; }
        public abstract string ContentWrapClass { get; }
        public abstract string H4Class { get; }
        public abstract string ItemClass { get; }
        public abstract string ItemsClass { get; }
        public abstract string LincCareWidgetAnchor2014Class { get; }
        public abstract string OrderSummaryClass { get; }
        public abstract string ProductOverlayClass { get; }
        #endregion

        #region Page Elements
        public IElement BreadCrumbElement => Browser.Locate.ElementBySelector(BreadCrumbClass.ToCssClassSelector());
        public IElement ShippingUpdatesModalDoneButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, DoneButtonClass);
        public IElement ShippingUpdatesModalOptInCheckbox => Browser.Locate.ElementById(DoubleOptinWarnNotShowCheckboxId);
        public IElement TableContainer => Browser.Locate.ElementByClassName(TableClass);

        public abstract IElement OrderIdContainer { get; }
		public abstract IElement OrderIdHeader { get; }
		public abstract IElement OrderDateHeader { get; }
        public abstract IElement OrderDetailsContainer { get; }
        public abstract IElement OrderSummaryContainer { get; }
        public abstract IElement OrderIdRmaHeading { get; }
        public abstract IElement RewardNumber { get; }
        public abstract IElement TrackItemLink { get; }

        /// <summary>
        /// Gets the IElement referencing the SPAN tag that contains the value of a given Order Summary rowLabel text.
        /// </summary>
        /// <param name="rowLabel">The Order Summary rowLabel text to locate. Comparison is not case-sensitive.</param>
        /// <returns></returns>
        public IElement GetOrderSummaryRowItem(string rowLabel)
		{
			// VS has a bug throwing "Evaluation of native methods in this context is not supported" error
			// when using Linq and doing string comparison, so let's do it the long way
			var rows = Browser.Locate.ElementsByClassName(RowClass, OrderSummarySection);
			foreach (var row in rows)
			{
				var span = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, row);

                // return the next element which contains the row value when the specified row rowLabel is found
                if (string.Equals(span.Text, rowLabel, StringComparison.OrdinalIgnoreCase)) { return Browser.Locate.NextSiblingElement(span); }

            }

            return null;
        }

        public ReadOnlyCollection<IElement> ListOfOrderDetailBreadCrumbLinks => BreadCrumbElement.FindElements(By.TagName("a"));
        public ReadOnlyCollection<IElement> OrderDetailsSections => Browser.Locate.ElementsByClassName(SectionClass, Details);

		public abstract ReadOnlyCollection<IElement> OrderDetailTableFirstRow { get; }
		public abstract ReadOnlyCollection<IElement> OrderDetailTableRows { get; }

        public abstract IElement GetProductName(IElement row);
        public abstract IElement GetProductStatus(IElement row);
        public abstract IElement GetProductTracking(IElement row);
        public abstract IElement GetProductUnitPrice(IElement row);
        public abstract IElement GetProductQty(IElement row);
        public abstract IElement GetProductExtPrice(IElement row);
        public abstract IElement GetProductQuantity(IElement row);
        public abstract IElement GetUnitPrice(IElement row);

        public IElement Details => Browser.Locate.ElementByClassName(DetailsClass);
        public IElement OrderItemTotal => GetOrderSummaryRowItem("Product Total:");
        public IElement OrderTax => GetOrderSummaryRowItem("Tax:");
        public IElement OrderTotal => GetOrderSummaryRowItem("Order Total:");
        public IElement OrderSummarySection => Browser.Locate.ElementByClassName(SummaryClass);
        public IElement ShippingProcessing => GetOrderSummaryRowItem("Shipping & Processing:");
        #endregion

        /// <inheritdoc />
        public abstract string GetOrderId();

        /// <inheritdoc />
        public abstract string GetOrderDate();

        public bool DoesShippingUpdatesModalShow()
        {
            return SpinWait.SpinUntil(() => Browser.Locate.DoesElementExistImmediately(DialogWindowClass.ToCssClassSelector()), TimeSpan.FromSeconds(5));
        }

        /// <inheritdoc />
        public string GetOrderBillingInfo() => ParseInfo(OrderDetailsSections[0], "BILLING INFORMATION:");

        /// <inheritdoc />
        public string GetOrderShippingInfo() => ParseInfo(OrderDetailsSections[1], "SHIP TO ADDRESS:");

        /// <inheritdoc />
        public string GetProductNameString(OrderHistoryItems orderDetailItem) { return DecodeHtmlString($"{orderDetailItem.ProductName} ({orderDetailItem.ShortSku})"); }

        /// <inheritdoc />
        public string GetSalesAssociateInfo() => ParseInfo(OrderDetailsSections[2], "SALES ASSOCIATE:");

        /// <inheritdoc />
        public string RemoveFormatting(string originalString) { return originalString.Replace("\r\n", string.Empty).Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace("+", string.Empty); }

        /// <inheritdoc />
        public string GetStatusString(OrderHistoryItems orderDetailItem)
        {
            if (orderDetailItem.FirstDeliveryDate.HasValue && !orderDetailItem.LastDeliveryDate.HasValue && orderDetailItem.OrderStatus == "Shipped") return $"Shipped {orderDetailItem.ShipDate:MMM. d, yyyy}";
            if (orderDetailItem.FirstDeliveryDate.HasValue && !orderDetailItem.LastDeliveryDate.HasValue && orderDetailItem.TrackingType != "Overnight") return "Status Pending";
            if (orderDetailItem.OrderStatus == "Canceled") return "Cancelled"; 
            if (orderDetailItem.OrderStatus == "Shipped") return $"Shipped {orderDetailItem.ShipDate:MMM. d, yyyy}";
            if (orderDetailItem.LastDeliveryDate.HasValue) return $"Arrives {orderDetailItem.FirstDeliveryDate:MMM. d} - {orderDetailItem.LastDeliveryDate:MMM. d}";

            return $"Arrives {orderDetailItem.FirstDeliveryDate:MMM. d}";
        }

        /// <inheritdoc />
        public string OrderDetailPayPal => GetOrderBillingInfo().Replace("PAID BY:", string.Empty).Trim().ToLower();

        /// <inheritdoc />
        public string FormatPrice(decimal price) => '$' + string.Format("{0:n}", price);

        /// <inheritdoc />
        public string ShippingProcessingPrice(OrderHistoryItems orderDetailItem) => decimal.ToDouble(orderDetailItem.FreightTotal).Equals(0) ? "FREE*" : FormatPrice(orderDetailItem.FreightTotal);

        /// <inheritdoc />
        public string ShippingInfo(OrderHistoryItems orderDetailItem)
        {
            Thread.Sleep(8000);

            var shippingInfoFromDatabase = ($"{orderDetailItem.ShipToFirstName} {orderDetailItem.ShipToLastName} {orderDetailItem.ShipToAddressLine1} {orderDetailItem.ShipToAddressLine2} {orderDetailItem.ShipToCity}, {orderDetailItem.ShipToState} {orderDetailItem.ShipToZipCode} {orderDetailItem.ShipToCountry} {orderDetailItem.ShipToPhoneNumber}").ToUpper();

            return shippingInfoFromDatabase;
        }

		protected string ParseInfo(IElement element, string title)
		{
			var chunks = element.Text.ToUpper().Split(new[] { title }, StringSplitOptions.RemoveEmptyEntries);

            return chunks.Length > 0 ? chunks[0].Trim() : string.Empty;
        }

        private static string FormatArrivalDate(DateTime? firstDeliveryDate, DateTime? lastDeliveryDate)
        {
            if (!firstDeliveryDate.HasValue && !lastDeliveryDate.HasValue) return "Status Pending";
            return lastDeliveryDate.HasValue ? $"Arrives {firstDeliveryDate:MMM. d} - {lastDeliveryDate:MMM. d}" : $"Arrives {firstDeliveryDate:MMM. d}";
        }

        public void HandleShippingUpdatesModal()
        {
            if (DoesShippingUpdatesModalShow())
            {
                ShippingUpdatesModalOptInCheckbox.Click();
                ShippingUpdatesModalDoneButton.Click();

                Browser.Wait.UntilElementDoesntExist(DialogWindowClass);
            }
        }
    }
}
