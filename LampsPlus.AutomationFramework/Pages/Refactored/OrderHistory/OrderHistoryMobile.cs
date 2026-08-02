using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory
{
    public class OrderHistoryMobile : OrderHistoryDesktop, IOrderHistoryMobile
    {
        //Class members
        private string _h4Class  = "h4";
        private string _contentWrapClass  = "contentWrap";
        private string _anchorLinkClass = "anchorLink";
        protected override string OrderHistoryRowsTable => "items";
        protected override string LpContainerId  => "//*[@id='applicationNode']/div[2]/div[1]";
        protected override IElement OrderId => Browser.Locate.ElementByClassName(_anchorLinkClass);

        public OrderHistoryMobile(IBrowser browser) : base(browser)
        {
        }

        //Interface implementation
        public override void CheckOrderStatus(OrderIdModel order)
        {
            OrderIdField.SendKeys(order.OrderId);
            EmailField.SendKeys(order.UserName);
            CheckStatusBtn.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistoryRowsTable.ToCssClassSelector()));
            //Browser.Wait.ForClickableElement(OrderHistory.ProductLink);
        }

        public override bool IsOrderIdVisible(OrderIdModel order) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H2).Text.Contains(order.OrderId);

        public override bool IsOrderDateVisible
        {
            get
            {
                var element = Browser.Locate.ElementByClassName(_h4Class, Browser.Locate.ElementByClassName(_contentWrapClass));
                return element.IsInitialized && element.Displayed;
            }
        }
    }
}