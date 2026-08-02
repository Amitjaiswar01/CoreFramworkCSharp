using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;
using Skip = Xunit.Skip;


namespace LampsPlus.RegressionTests.Common.OrderHistory.T275_T486_VerifySignedOutUsersOnlySeePartialInfoOnTheOrderHistoryPage
{
    //[Collection(LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T486_iPhone_VerifyUsersSeePartialInfoOrderHistory : T486_MobileBase
    {
        public T486_iPhone_VerifyUsersSeePartialInfoOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UsersSeePartialInfoOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T486_Emulator_VerifyUsersSeePartialInfoOrderHistory : T486_MobileBase
    {
        public T486_Emulator_VerifyUsersSeePartialInfoOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UsersSeePartialInfoOrderHistory(string config) => Validate(config);
    }


    /// <summary>
    /// Verify signed out users only see partial info on the Order History page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5069
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T486
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5069"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T486")]
    public abstract class T486_MobileBase : TestsBaseMobile
    {
        protected T486_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrangement
            User has an Order ID and the email address associated with it
            Go to https://www.lampsplus.com/secure/account/order-history/
            */
            InitializeFunctionalTest(config, Urls.OrderHistoryPageUrl);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            Assert.True(OrderHistory.IsCurrentPage, "Current page is not OrderHistory page");
            var order = OrderActions.GetAnOrderIdPlacedWithin60Days();
            Log.Message($"Order ID selected for test: {order.OrderId}, User Name selected for test: {order.UserName}");

            /*Act
            Click check status
            */
            OrderHistory.CheckOrderStatus(order);

            /*Assert
            Order elements are visible to an anonymous user and match the values in the database
            */
            AssertOrderElementsAreVisible(order);

            /*Assert
            The following elements are NOT visible as an anonymous user:
            */
            AssertOrderElementsAreNotVisible(order);
        }

        private void AssertOrderElementsAreVisible(OrderIdModel order)
        {
            var orderHistoryItems = OrderActions.GetOrderHistoryItems(order.OrderId);
            var orderTotal = orderHistoryItems.FirstOrDefault()?.OrderTotal;
            VerifyOrderDetailsTable(orderHistoryItems);

            var orderTotalHistory = OrderHistory.GetOrderTotal;
            Assert.Equals(orderTotalHistory, OrderDetails.FormatPrice(orderTotal ?? 0), "Order Total not matched");
        }

        // Negative tests
        private void AssertOrderElementsAreNotVisible(OrderIdModel order)
        {
            Assert.True(!OrderHistory.IsOrderIdVisible(order), "Order ID should not be displayed");
            Assert.False(OrderHistory.IsOrderDateVisible, "Order Date should not be displayed");
            Assert.False(OrderHistory.IsBillingInformationVisible, "Billing Information should not be displayed");
            Assert.False(OrderHistory.IsShippingInformationVisible, "Shipping Information should not be displayed");
            Assert.False(OrderHistory.IsRewardNumberVisible, "Reward number should not be displayed");
        }

        private void VerifyOrderDetailsTable(List<OrderHistoryItems> orderHistoryItems)
        {
            foreach (var orderDetailItem in orderHistoryItems)
            {
                Assert.Equals(OrderDetails.GetDbProductName(orderDetailItem), OrderDetails.GetProductName(orderDetailItem.ShortSku), "Product Names do not match");
                Assert.Equals(OrderDetails.FormatPrice(orderDetailItem.UnitPrice), OrderDetails.GetUnitPrice(orderDetailItem.ShortSku), "Unit prices do not match");
                Assert.Equals(orderDetailItem.Quantity.ToString(), OrderDetails.GetProductQuantity(orderDetailItem.ShortSku), "Quantities do not match");
                Assert.Equals(OrderDetails.GetStatusString(orderDetailItem), OrderDetails.GetProductStatus(orderDetailItem.ShortSku), "Item statuses do not match");
                Assert.Equals(TextActions.FormatPrice(orderDetailItem.OrderTotal), OrderDetails.GetOrderTotal, "Order Total prices not match");
            }
        }
    }

}