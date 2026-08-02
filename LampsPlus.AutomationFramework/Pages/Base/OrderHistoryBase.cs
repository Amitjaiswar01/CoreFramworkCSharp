using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;
using OpenQA.Selenium;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class OrderHistoryBase : Page, IOrderHistory
    {
        /// <inheritdoc />
        protected OrderHistoryBase(IBrowser browser, TestsBase testsBase) : base(browser)
        {
            TestsBase = testsBase;
        }

        internal TestsBase TestsBase { get; }

        #region Class Setup
        public string OrderDateString { get; } = "Order Date";
        #endregion

        #region CSS Selector Strings
        private string DetailsClass { get; } = "details";
        private string EmailFieldId { get; } = "txtEmailInput";
        private string LincCareWidgetAnchor2014Class { get; } = "linc_care_widget_anchor_2014";
        private string OpenBoxLabelClass { get; } = "open-box-label";
        private string OrderHistoryErrorMsgId { get; } = "orderHistoryErrorMsg";
        private string OrderIdFieldId { get; } = "txtOrderId";
        private string StatusColClass { get; } = "statusCol";
        private string TitleClass { get; } = "title";

        public string CheckStatusButtonId { get; } = "checkStatus";
        public string ItemClass { get; } = "item";
        public string LincOptInWidgetClass { get; } = "linc-optin-widget";
        public string OrderClass { get; } = "order";
        public string ProductColClass { get; } = "productCol";
        public string RowClass { get; } = "row";
        public string SummaryClass { get; } = "summary";
        public string TrackItemClass { get; } = "linc_care_widget_anchor_2014 ";
        public abstract string LpContainerId { get; }
        public abstract string OrderProductLinkXpath { get; }
        public abstract string OrderHistoryRowsTable { get; }
        public abstract string ContentWrapClass { get; }
        public abstract string H4Class { get; }
        public abstract string OrderNextPageButtonId { get; }
        #endregion

        #region Page Elements

        public IElement TrackItem => Browser.Locate.ElementByClassName(TrackItemClass);
        public IElement OrderDetailContainer => Browser.Locate.ElementByXpath("//*[@id='emailCartContainer']");
        public IElement OrderDetails => Browser.Locate.ElementByClassName(DetailsClass);
        public IElement OrderSearchErrorMessageImmediately => Browser.Locate.ElementImmediately(OrderHistoryErrorMsgId.ToCssIdSelector());
		public IElement OrderSummaryContainer => Browser.Locate.ElementByClassName(SummaryClass);
		public IElement OrderIdField => Browser.Locate.ElementById(OrderIdFieldId);
		public IElement EmailField => Browser.Locate.ElementById(EmailFieldId);
		public IElement CheckStatusBtn => Browser.Locate.ElementById(CheckStatusButtonId);
        public IElement OptInWidgetElement => Browser.Locate.ElementByClassName(LincOptInWidgetClass);
        public IElement OpenBoxLabel => Browser.Locate.ElementByClassName(OpenBoxLabelClass);
        public IElement OrderTotalElement => Browser.Locate.ElementsByClassName(RowClass, OrderSummaryContainer).First((el) => el.GetAttribute(HtmlTextWriterAttribute.Class.ToString()).Contains("big")).FindElements(By.TagName(HtmlTextWriterTag.Span.ToString()))[1];
        public IElement GetTrackItemButton(IElement cell) => Browser.Locate.ElementByClassName(LincCareWidgetAnchor2014Class, cell);
        public IElement GetTrackingOrderNumber(string orderNumberClass) => Browser.Locate.ElementByClassName(orderNumberClass);
        public IElement Summary => Browser.Locate.ElementByClassName(SummaryClass);

        public ReadOnlyCollection<IElement> OrderHeaderTitles => Browser.Locate.ElementsByClassName(TitleClass, OrderDetails);

        public abstract IElement FirstOrderPreviewElement { get; }
        public abstract IElement GetOrderIdLink(string orderId);        
        public abstract IElement LpContainer { get; }
        public abstract IElement OrderNextPageButton { get; }
        public abstract IElement PageHeadingElement { get; }
        public abstract IElement ProductLink { get; }

        public abstract ReadOnlyCollection<IElement> OrderPreviewElements { get; }
        public abstract ReadOnlyCollection<IElement> OrderDetailElements { get; }        
        public abstract ReadOnlyCollection<IElement> H5LpContainerElements { get; }
        public abstract ReadOnlyCollection<IElement> OrderSummaryRows { get; }
        #endregion


        /// <inheritdoc />
        public bool IsBillingInformationSelectorVisible => OrderHeaderTitleExists("Billing Information");

        /// <inheritdoc />
        public abstract bool IsOrderDateHeadingVisible { get; }

        public abstract OrderPreview GetOrderPreview(IElement element);

        /// <inheritdoc />
        public bool IsRewardNumberSelectorVisible => OrderHeaderTitleExists("Customer #");

        /// <inheritdoc />
        public bool IsShippingInformationSelectorVisible => OrderHeaderTitleExists("Ship to Address");

        /// <inheritdoc />
        public string GetProductName(IElement row) => DecodeHtmlString(Browser.Locate.ElementByClassName(ProductColClass, row).Text.Trim());

        /// <inheritdoc />
        public string GetItemStatus(IElement row) => Browser.Locate.ElementByClassName(StatusColClass, row).Text.Trim();

        /// <inheritdoc />
        public string GetUnitPrice(IElement row) => Browser.Locate.ElementBySelector(HtmlTextWriterTag.Td.ToNthChildSelector(4), row).Text.Trim();

        /// <inheritdoc />
        public string GetQuantity(IElement row) => Browser.Locate.ElementBySelector(HtmlTextWriterTag.Td.ToNthChildSelector(5), row).Text.Trim();

        /// <inheritdoc />
        public string GetTotalFromSummaryByName(string totalName)
        {
            var summaryRows = Browser.Locate.ElementsByClassName(RowClass, Summary);
			var price = summaryRows.Where(x => x.Text.ToLower().Trim().Contains(totalName.ToLower()))
				.Select(x => Browser.Locate.ElementBySelector(HtmlTextWriterTag.Span.ToLastChildSelector(), x).Text.Trim().Replace("$", "")).First();

			return !price.ToLower().Contains("free") && !price.Contains("- -") ? price : "0.00";
		}

		private bool OrderHeaderTitleExists(string headerTitle) => Browser.Locate.ElementWithText(OrderHeaderTitles, AttributeSelectorType.Contains, headerTitle).IsInitialized;

        /// <inheritdoc />
        public string FormatOrderStatusFromDatabase(OrderHistoryItems orderHistoryItem)
		{
			string orderStatus;

			switch (orderHistoryItem.OrderStatus)
			{
				// Canceled value is spelled wrong in the database
				case "Canceled":
					orderStatus = "Cancelled";
					break;
				// Pending value is formatted to a readable string on page
				case "Pending":
					orderStatus = FormatArrivalDate(orderHistoryItem.FirstDeliveryDate,
						orderHistoryItem.LastDeliveryDate);
					break;
				default:
					orderStatus = orderHistoryItem.OrderStatus;
					break;
			}

			return orderStatus;
		}

		private static string FormatArrivalDate(DateTime? firstDeliveryDate, DateTime? lastDeliveryDate)
		{
			if (!firstDeliveryDate.HasValue && !lastDeliveryDate.HasValue) return "Status Pending";
			return lastDeliveryDate.HasValue ? $"Arrives {firstDeliveryDate:MMM. d} - {lastDeliveryDate:MMM. d}" : $"Arrives {firstDeliveryDate:MMM. d}";
		}
	}
}
