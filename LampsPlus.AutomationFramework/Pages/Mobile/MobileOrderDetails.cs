using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/account/order-detail/{orderId}/{accountId}
    /// </summary>
    public class MobileOrderDetails : OrderDetailsBase
    {
        /// <inheritdoc />
        public MobileOrderDetails(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string ContentWrapClass { get; } = "contentWrap";
        public override string GetOrderId() => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H2, ContentWrapContainer, false)[1].Text;
        public override string GetOrderDate() => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, ContentWrapContainer, false)[0].Text;
        public override string H4Class { get; } = "h4";
        public override string ItemClass { get; } = "item";
        public override string ItemsClass { get; } = "items";
        public override string LincCareWidgetAnchor2014Class { get; } = "linc_care_widget_anchor_2014";
        public override string OrderSummaryClass { get; } = "summary";
        public override string ProductOverlayClass { get; } = "product_overlay";

        public override string OpenCartResultsClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        private IElement ContentWrapContainer => Browser.Locate.ElementByClassName(ContentWrapClass);

        public override IElement GetProductName(IElement row) => Browser.Locate.ElementByClassName(ProductColClass, row);
        public override IElement GetProductQty(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, row)[0];
        public override IElement GetProductStatus(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Strong, row)[1];
        public override IElement GetProductTracking(IElement row) => Browser.Locate.ElementByClassName(LincCareWidgetAnchor2014Class, row);
        public override IElement GetProductUnitPrice(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, row)[1];
        public override IElement OrderDateHeader => Browser.Locate.ElementByClassName(H4Class, ContentWrapContainer, false);
        public override IElement OrderDetailsContainer => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Div, ItemsClass);
        public override IElement OrderIdHeader => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H2, ContentWrapContainer, false)[0];
        public override IElement OrderSummaryContainer => Browser.Locate.ElementByClassName(OrderSummaryClass);
        public override IElement TrackItemLink => Browser.Locate.ElementByClassName(LincCareWidgetAnchor2014Class);

        public override ReadOnlyCollection<IElement> OrderDetailTableFirstRow =>
            Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Div, ItemClass, OrderDetailsContainer);
        public override ReadOnlyCollection<IElement> OrderDetailTableRows => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Div, ItemClass, OrderDetailsContainer);

        public override IElement GetProductExtPrice(IElement row) => throw new NotImplementedException();
        public override IElement OrderIdContainer => throw new NotImplementedException();
        public override IElement RewardNumber => throw new NotImplementedException();
        public override IElement OrderIdRmaHeading => throw new NotImplementedException();
        public override IElement GetProductQuantity(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, row)[0];
        public override IElement GetUnitPrice(IElement row) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, row)[1];
        #endregion
    }
}
