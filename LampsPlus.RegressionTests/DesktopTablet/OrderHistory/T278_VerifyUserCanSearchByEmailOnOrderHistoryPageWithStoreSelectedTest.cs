using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.OrderHistory
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    public class T278VerifyUserCanSearchByEmailOnOrderHistoryWithStoreSelectedTest : TestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T278VerifyUserCanSearchByEmailOnOrderHistoryWithStoreSelectedTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify a user can search by email on the Order History page with 'Store' selected.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5511
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T278
        /// </summary>
        [Trait(LpTraits.TaskId, "ACD-5511"), Trait(LpTraits.TId, "LP-T278")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Desktop_SNIS_ESI_Windows10_Chrome)]
        public void Test(string config)
        {
            var setup = new TestSetup(config, Urls.EmployeeOrderLookupPageUrl);

            InitializeFramework(setup);

            EmployeeOrderLookup.StoreRadioButton.Click();

            SoftVerify.True(EmployeeOrderLookup.PaginationDropdownPageOptions.Count > 0, "No page options in Pagination dropdown.");

            //var firstOrderFirstEmail = EmployeeOrderLookup.FirstOrderFirstEmail;

            var orderEmail = "tatayita@aol.com"; 

            EmployeeOrderLookup.OrderSearchInput.SendKeys(orderEmail);
            EmployeeOrderLookup.OrderSearchButton.Click();

            EmployeeOrderLookup.FirstOrder.Click();

            var orderDetailsFromDatabase = OrderActions.GetOrderHistoryItems(OrderDetails.GetOrderId());

            VerifyOrderDetailsItemsWithDatabase(orderDetailsFromDatabase);
        }

        private void VerifyOrderDetailsItemsWithDatabase(List<OrderHistoryItems> orderHistoryItems)
        {
            foreach (var orderDetailItem in orderHistoryItems)
            {
                var matchedRow = OrderDetails.OrderDetailTableRows.FirstOrDefault(r => r.Text.Contains(orderDetailItem.ShortSku));

                SoftVerify.Equals(orderDetailItem.OrderId, OrderDetails.GetOrderId(), "Order Ids do not match");
                SoftVerify.Equals(orderDetailItem.CreatedDate.Date, DateTime.Parse(OrderDetails.GetOrderDate(), new CultureInfo("en-US")).Date, "Order dates do not match");
                SoftVerify.Equals(OrderDetails.GetProductNameString(orderDetailItem), OrderDetails.GetProductName(matchedRow).Text, "Product Names do not match");
                SoftVerify.Equals(OrderDetails.GetStatusString(orderDetailItem), OrderDetails.GetProductStatus(matchedRow).Text, "Item statuses do not match");
                SoftVerify.Displayed(OrderDetails.TrackItemLink, "Track Item link not displayed");
                SoftVerify.Equals(OrderDetails.FormatPrice(orderDetailItem.UnitPrice), OrderDetails.GetProductUnitPrice(matchedRow).Text, "Unit prices do not match");
                SoftVerify.Equals(orderDetailItem.Quantity.ToString(), OrderDetails.GetProductQty(matchedRow).Text, "Quantities do not match");
                SoftVerify.Equals(OrderDetails.FormatPrice(orderDetailItem.ItemTotal), OrderDetails.OrderItemTotal.Text, "Item Total prices do not match");
                SoftVerify.Equals(OrderDetails.ShippingProcessingPrice(orderDetailItem), OrderDetails.ShippingProcessing.Text, "Shipping and Processing prices do not match");
                SoftVerify.Equals(OrderDetails.FormatPrice(orderDetailItem.TaxTotal), OrderDetails.OrderTax.Text, "Tax prices do not match");
                SoftVerify.Equals(OrderDetails.FormatPrice(orderDetailItem.OrderTotal), OrderDetails.OrderTotal.Text, "Order Total prices not match");
                SoftVerify.Equals(OrderDetails.FormatPrice(orderDetailItem.ExtPrice), OrderDetails.GetProductExtPrice(matchedRow).Text, "ExtPrices do not match");
                SoftVerify.Equals(OrderDetails.RemoveFormatting(OrderDetails.BillingInfo(orderDetailItem)), OrderDetails.RemoveFormatting(OrderDetails.GetOrderBillingInfo()), "Billing information not match");
                SoftVerify.Equals(OrderDetails.RemoveFormatting(OrderDetails.ShippingInfo(orderDetailItem)), OrderDetails.RemoveFormatting(OrderDetails.GetOrderShippingInfo()), "Shipping information not match");
                SoftVerify.StringContains(OrderDetails.GetSalesAssociateInfo(), orderDetailItem.SalesAssociate.ToString(), "Sales Associate not match");
                SoftVerify.Equals(orderDetailItem.RewardNumber.ToString(), OrderDetails.RewardNumber.Text, "Sales Associate not match");
            }
        }
    }
}
