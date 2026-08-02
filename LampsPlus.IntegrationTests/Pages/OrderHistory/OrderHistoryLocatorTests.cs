using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.OrderHistory
{
    public class OrderHistoryLocatorDesktopTest : OrderHistoryLocatorTests
    {
        public OrderHistoryLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "OrderHistory")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LocateOrderHistoryElementsTest(string config) => Locate(config);

        protected override void VerifyOrderHistory()
        {
            VerifyElementDisplayed(() => OrderHistory.OrderPreviewElements);
            VerifyElementDisplayed(() => OrderHistory.FirstOrderPreviewElement);
            OrderHistory.GetOrderPreview(OrderHistory.OrderPreviewElements[0]).OrderIdElement.Click();

            VerifyElementDisplayed(() => OrderHistory.LpContainer);
            VerifyElementDisplayed(() => OrderHistory.H5LpContainerElements);
            VerifyElementDisplayed(() => OrderHistory.OrderSummaryRows);
        }

        protected override void VerifyOrderHistoryDetailWebElements()
        {
            VerifyElementDisplayed(() => OrderHistory.OrderHeaderTitles);
            VerifyElementDisplayed(() => OrderHistory.OrderDetails);
            VerifyElementDisplayed(() => OrderHistory.OrderSummaryContainer);
            VerifyElementDisplayed(() => OrderHistory.OrderTotalElement);
            VerifyElementDisplayed(() => OrderHistory.OrderDetailElements);
            VerifyElementDisplayed(() => OrderHistory.PageHeadingElement);
            VerifyElementDisplayed(() => OrderHistory.Summary);
            VerifyElementDisplayed(() => OrderHistory.ProductLink);
        }
    }


    public class OrderHistoryLocatorMobileTest : OrderHistoryLocatorTests
    {
        public OrderHistoryLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "OrderHistory")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LocateOrderHistoryElementsTest(string config) => Locate(config);

        protected override void VerifyOrderHistory()
        {
            VerifyElementDisplayed(() => OrderHistory.OrderPreviewElements);
            VerifyElementDisplayed(() => OrderHistory.FirstOrderPreviewElement);
            OrderHistory.GetOrderPreview(OrderHistory.OrderPreviewElements[0]).OrderIdElement.Click();

            VerifyElementNotImplemented(() => OrderHistory.LpContainer);
            VerifyElementNotImplemented(() => OrderHistory.H5LpContainerElements);
            VerifyElementNotImplemented(() => OrderHistory.OrderSummaryRows);
            VerifyElementNotImplemented(() => OrderHistory.ProductLink);
        }

        protected override void VerifyOrderHistoryDetailWebElements()
        {
            VerifyElementDisplayed(() => OrderHistory.OrderHeaderTitles);
            VerifyElementDisplayed(() => OrderHistory.OrderDetails);
            VerifyElementDisplayed(() => OrderHistory.OrderSummaryContainer);
            VerifyElementDisplayed(() => OrderHistory.OrderTotalElement);            
            VerifyElementDisplayed(() => OrderHistory.OrderDetailElements);
            VerifyElementDisplayed(() => OrderHistory.PageHeadingElement);
            VerifyElementDisplayed(() => OrderHistory.Summary);
        }
    }


    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the Order History page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "OrderHistory")]
    public abstract class OrderHistoryLocatorTests : PageObjectTestsBase
    {
        protected OrderHistoryLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            const string orderId = "Test123456";
            const string emailId = "nonexistent@email.com";

            InitializeFramework(config, Urls.OrderHistoryPageUrl);
            BuildElementsList(OrderHistory);

            VerifyOrderHistory();

            VerifyOrderHistoryDetailWebElements();
             
            SignInWorkflow.SignOut();

            Browser.Navigate(Urls.OrderHistoryPageUrl);

            VerifyElementDisplayed(() => OrderHistory.OrderIdField);
            VerifyElementDisplayed(() => OrderHistory.EmailField);
            VerifyElementDisplayed(() => OrderHistory.CheckStatusBtn);

            OrderHistory.OrderIdField.SendKeys(orderId);
            OrderHistory.EmailField.SendKeys(emailId);
            OrderHistory.CheckStatusBtn.Click();

            VerifyElementDisplayed(() => OrderHistory.OrderSearchErrorMessageImmediately);

            // check order history page elements on valid order id and email
            var lincOrderIdFromDatabase = OrderActions.GetLincQualifyingOrders();

            OrderHistory.OrderIdField.Clear();
            OrderHistory.OrderIdField.SendKeys(lincOrderIdFromDatabase.OrderId);
            OrderHistory.EmailField.Clear();
            OrderHistory.EmailField.SendKeys(lincOrderIdFromDatabase.UserName);
            OrderHistory.CheckStatusBtn.Click();

            Browser.Wait.ForDisplayedElement(OrderHistory.OptInWidgetElement);
            VerifyElementDisplayed(() => OrderHistory.OptInWidgetElement);
        }

        protected abstract void VerifyOrderHistory();

        protected abstract void VerifyOrderHistoryDetailWebElements();
    }
}
