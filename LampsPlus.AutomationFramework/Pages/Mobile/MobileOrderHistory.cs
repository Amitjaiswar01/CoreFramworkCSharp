using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/account/order-history/
    /// </summary>
    public class MobileOrderHistory : OrderHistoryBase
    {
        /// <inheritdoc />
        public MobileOrderHistory(IBrowser browser, TestsBase testsBase) : base(browser, testsBase)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework;

        #region CSS Selector Strings
        public override string ContentWrapClass { get; } = "contentWrap"; 
        public override string H4Class { get; } = "h4";
        public override string LpContainerId { get; } ="//*[@id='applicationNode']/div[2]/div[1]";
        public override string OrderHistoryRowsTable { get; } = "items";

        public override string OrderProductLinkXpath => throw new NotImplementedException();
        public override string OrderNextPageButtonId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement GetOrderIdLink(string orderId) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(Framework.OrderHistory.OrderClass));
        public override IElement FirstOrderPreviewElement => OrderPreviewElements.First().FindElement(By.TagName("a"));
        public override IElement PageHeadingElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H2);

        public override ReadOnlyCollection<IElement> OrderPreviewElements => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Div, OrderClass);
        public override ReadOnlyCollection<IElement> OrderDetailElements => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Div, ItemClass);

        public override IElement LpContainer => Browser.Locate.ElementByXpath(LpContainerId);
        public override IElement OrderNextPageButton => throw new NotImplementedException();
        public override IElement ProductLink => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> H5LpContainerElements => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> OrderSummaryRows => throw new NotImplementedException();
        #endregion

        /// <inheritdoc />
        public override bool IsOrderDateHeadingVisible
        {
            get
            {
                var element = Browser.Locate.ElementByClassName(H4Class, Browser.Locate.ElementByClassName(ContentWrapClass));
                return element.IsInitialized && element.Displayed;
            }
        }

        /// <inheritdoc />
        public override OrderPreview GetOrderPreview(IElement element)
        {
            OrderPreview orderPreview = new OrderPreview();
            ReadOnlyCollection<IElement> orderProperties = Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Div, element);

            orderPreview.OrderDateElement = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, element);
            orderPreview.OrderIdElement = Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, orderProperties[0]);
            orderPreview.WebSiteElement = Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, orderProperties[1])[1];
            orderPreview.OrderTotalElement = Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, orderProperties[2])[1];
            orderPreview.OrderStatusElement = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, orderProperties[3]);

            return orderPreview;
        }
    }
}
