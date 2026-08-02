using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/account/order-detail/{orderId}/{accountId}
    /// </summary>
    public class OrderDetails : OrderDetailsBase
    {
        /// <inheritdoc />
        public OrderDetails(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        public override string OpenCartResultsClass { get; } = "openCartResults";

        public override string ContentWrapClass => throw new NotImplementedException();
        public override string H4Class => throw new NotImplementedException();
        public override string ItemClass => throw new NotImplementedException();
        public override string ItemsClass => throw new NotImplementedException();
        public override string LincCareWidgetAnchor2014Class => throw new NotImplementedException();
        public override string OrderSummaryClass => throw new NotImplementedException();
        public override string ProductOverlayClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement GetProductName(IElement row) => Browser.Locate.ElementByClassName(ProductColClass, row);
        public override IElement GetProductStatus(IElement row) => Browser.Locate.ElementByClassName(StatusColClass, row);
        public override IElement GetProductTracking(IElement row) => Browser.Locate.ElementByClassName(ShippingColClass, row);
        public override IElement GetProductUnitPrice(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, row)[3];
        public override IElement GetProductQty(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, row)[4];
        public override IElement GetProductExtPrice(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, row)[5];
        public override IElement OrderDetailsContainer => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Table, OrderSummaryContainer);
        public override IElement OrderSummaryContainer => Browser.Locate.ElementById(LpContainerId);
        public override IElement OrderIdRmaHeading => Browser.Locate.ElementByXpath(OrderIdRmaHeadingXPath);
        public override IElement OrderIdContainer => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Table, OpenCartResultsClass);
        public override IElement OrderIdHeader => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H1, OrderIdRmaHeading, true);
        public override IElement OrderDateHeader => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H5, OrderIdRmaHeading, true);
        public override IElement RewardNumber => Browser.Locate.ElementBySelector($"{DetailsClass.ToCssClassSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)} > {HtmlTextWriterTag.Div.ToNthChildSelector(3)}", OrderSummaryContainer);
        public override IElement TrackItemLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Td, ShippingColClass), true);
        public override IElement GetProductQuantity(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, row)[4];
        public override IElement GetUnitPrice(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, row)[3];

        public override ReadOnlyCollection<IElement> OrderDetailTableFirstRow => Browser.Locate.ElementsByXpath("//*[@id=\"lpContainer\"]//tbody/tr");
        public override ReadOnlyCollection<IElement> OrderDetailTableRows => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Table} {HtmlTextWriterTag.Tbody} {HtmlTextWriterTag.Tr}", TableContainer);
        #endregion

        /// <inheritdoc />
        public override string GetOrderId() => ParseInfo(OrderIdHeader, "ORDER ID:");

        /// <inheritdoc />
        public override string GetOrderDate() => ParseInfo(OrderDateHeader, "ORDER DATE:");
    }
}
