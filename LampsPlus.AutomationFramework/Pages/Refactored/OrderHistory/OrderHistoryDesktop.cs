using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory
{
    public class OrderHistoryDesktop : IOrderHistoryDesktop
    {
        //Class members
        private string _orderIdFieldId  = "txtOrderId";
        private string _emailFieldId  = "txtEmailInput";
        private string _titleClass  = "title";
        private string _detailsClass  = "details";
        private string _dialogWindowClass = "dialog_window";
        private string _doubleOptinWarnNotShowCheckboxId = "doubleOptinWarnNotShowCheckbox";
        private string _doneButtonClass = "done_button";
        private string _trackMyOrderButtonClass = "linc_care_widget_anchor_2014";
        private string _recommendedProductsClass = "recommended_products_slider";
        private string _closeShippingUpdateModalClass = "MuiSvgIcon-root";
        protected string CheckStatusButtonId = "checkStatus";
        protected string RowClass = "row";
        protected string SummaryClass  = "summary";
        protected string OrderDateString  = "Order Date";
        protected virtual string LpContainerId  => "lpContainer";

        private bool OrderHeaderTitleExists(string headerTitle) => Browser.Locate.ElementWithText(OrderHeaderTitles, AttributeSelectorType.Contains, headerTitle).IsInitialized;
        private IElement LpContainer => Browser.Locate.ElementById(LpContainerId);
        private IElement CloseShippingUpdateModalBtn => Browser.Locate.ElementByClassName(_closeShippingUpdateModalClass);
        private IElement ShippingUpdatesModalDoneButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, _doneButtonClass);
        private IElement ShippingUpdatesModalOptInCheckbox => Browser.Locate.ElementById(_doubleOptinWarnNotShowCheckboxId);
        private ReadOnlyCollection<IElement> OrderHeaderTitles => Browser.Locate.ElementsByClassName(_titleClass, OrderDetails);
        private ReadOnlyCollection<IElement> H5LpContainerElements => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H5, LpContainer);
        private ReadOnlyCollection<IElement> OrderPreviewElements => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Tr, Browser.Locate.ElementByTagName(HtmlTextWriterTag.Tbody, LpContainer));
        protected virtual string OrderHistoryRowsTable => "stripedTable";
        protected IElement OrderDetails => Browser.Locate.ElementByClassName(_detailsClass);
        protected IElement OrderIdField => Browser.Locate.ElementById(_orderIdFieldId);
        protected IElement EmailField => Browser.Locate.ElementById(_emailFieldId);
        protected IElement CheckStatusBtn => Browser.Locate.ElementById(CheckStatusButtonId);
        protected IElement OrderSummaryContainer => Browser.Locate.ElementByClassName(SummaryClass);
        protected IElement TrackItemButton => Browser.Locate.ElementByClassName(_trackMyOrderButtonClass);
        protected virtual IElement OrderId => OrderPreviewElements.First().FindElement(By.TagName("a"));
        protected IElement RecommendedProductsOnTrackItemPage(int index) => Browser.Locate.ElementsByClassName(_recommendedProductsClass)[index];

        private bool DoesShippingUpdatesModalShow()
        {
            return SpinWait.SpinUntil(() => Browser.Locate.DoesElementExistImmediately(_dialogWindowClass.ToCssClassSelector()), TimeSpan.FromSeconds(5));
        }

        //Instances
        protected IBrowser Browser;

        public OrderHistoryDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/secure/account/order-history/";
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(CheckStatusButtonId.ToCssIdSelector()));

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public virtual void CheckOrderStatus(OrderIdModel order)
        {
            OrderIdField.SendKeys(order.OrderId);
            EmailField.SendKeys(order.UserName);
            CheckStatusBtn.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistoryRowsTable.ToCssClassSelector()));
        }

        public void ClickOnTrackOrder()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_trackMyOrderButtonClass.ToCssClassSelector()));
            TrackItemButton.Click();
        }

        public void WaitForMoreYouMayLikeWidget()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_recommendedProductsClass),20);
        }

        public string GetOrderTotal => Browser.Locate.ElementsByClassName(RowClass, OrderSummaryContainer).First((el) => el.GetAttribute(HtmlTextWriterAttribute.Class.ToString()).Contains("big")).FindElements(By.TagName(HtmlTextWriterTag.Span.ToString()))[1].Text;
        public virtual bool IsOrderIdVisible(OrderIdModel order) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H1, LpContainer, false).Text.Contains(order.OrderId);

        public virtual bool IsOrderDateVisible
        {
            get
            {
                var element = Browser.Locate.ElementWithText(H5LpContainerElements, AttributeSelectorType.Contains,
                    OrderDateString);
                return element.IsInitialized && element.Displayed;
            }
        }

        public bool IsBillingInformationVisible => OrderHeaderTitleExists("Billing Information");
        public bool IsShippingInformationVisible => OrderHeaderTitleExists("Ship to Address");
        public bool IsRewardNumberVisible => OrderHeaderTitleExists("Customer #");

        public void HandleShippingUpdatesModal()
        {
            if (DoesShippingUpdatesModalShow())
            {
                CloseShippingUpdateModalBtn.Click();
                Browser.Wait.UntilElementDoesntExist(_dialogWindowClass);
            }
        }

        public void NavigateToOrderDetailsPage()
        {
            OrderId.Click();
        }

    }

}