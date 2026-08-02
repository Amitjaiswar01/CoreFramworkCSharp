using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/account/order-history/
    /// </summary>
    public class OrderHistory : OrderHistoryBase
    {
        /// <inheritdoc />
        public OrderHistory(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }

        #region CSS Selector Strings
        public override string OrderNextPageButtonId { get; } = "main_ctlOrderHistoryPagerTop_LbNext";
        public override string OrderHistoryRowsTable => "stripedTable";
        public override string OrderProductLinkXpath { get; } = "//*[@id='lpContainer']//td[contains(@class, 'productCol')]";
        public override string LpContainerId { get; } = "lpContainer";

        public override string ContentWrapClass => throw new NotImplementedException();
        public override string H4Class => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement GetOrderIdLink(string orderId) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementWithText(OrderPreviewElements, AttributeSelectorType.Contains, orderId));
        public override IElement FirstOrderPreviewElement => OrderPreviewElements.First().FindElement(By.TagName("a"));
        public override IElement LpContainer => Browser.Locate.ElementById(LpContainerId);
        public override IElement OrderNextPageButton => Browser.Locate.ElementById(OrderNextPageButtonId);
        public override IElement PageHeadingElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H1, LpContainer, false);
        public override IElement ProductLink => Browser.Locate.ElementByXpath(OrderProductLinkXpath);

        public override ReadOnlyCollection<IElement> OrderPreviewElements => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Tr, Browser.Locate.ElementByTagName(HtmlTextWriterTag.Tbody, LpContainer));
        public override ReadOnlyCollection<IElement> OrderDetailElements => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Tr, Browser.Locate.ElementByTagName(HtmlTextWriterTag.Tbody, LpContainer));
        public override ReadOnlyCollection<IElement> H5LpContainerElements => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H5, LpContainer);
        public override ReadOnlyCollection<IElement> OrderSummaryRows => Browser.Locate.ElementsByClassName(RowClass, OrderSummaryContainer);
        #endregion

        /// <inheritdoc />
        public override bool IsOrderDateHeadingVisible
        {
            get
            {
                var element = Browser.Locate.ElementWithText(H5LpContainerElements, AttributeSelectorType.Contains,
                    OrderDateString);
                return element.IsInitialized && element.Displayed;
            }
        }

        /// <inheritdoc />
        public override OrderPreview GetOrderPreview(IElement element)
        {
            OrderPreview orderPreview = new OrderPreview();
            ReadOnlyCollection<IElement> orderProperties = Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Td, element);

            orderPreview.OrderDateElement = orderProperties[0];
            orderPreview.OrderIdElement = Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, orderProperties[1]);
            orderPreview.WebSiteElement = orderProperties[2];
            orderPreview.OrderTotalElement = orderProperties[3];
            orderPreview.OrderStatusElement = orderProperties[4];            

            return orderPreview;
        }
    }
}
