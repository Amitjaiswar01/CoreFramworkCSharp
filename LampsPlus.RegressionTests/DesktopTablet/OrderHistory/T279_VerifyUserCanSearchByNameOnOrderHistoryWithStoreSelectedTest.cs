using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.OrderHistory
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    public class T279VerifyUserCanSearchByNameOnOrderHistoryWithStoreSelectedTest : TestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T279VerifyUserCanSearchByNameOnOrderHistoryWithStoreSelectedTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify a user can search by name on the Order History page with 'Store' selected.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5301
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T279
        /// </summary>
        [Trait(LpTraits.TaskId, "ACD-5301"), Trait(LpTraits.TId, "LP-T279"), Trait(LpTraits.UserRole, LpTraits.Employee)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows10_Chrome_SNIS_ESI)]
        public void Test(string config)
        {
            InitializeFramework(config, Urls.EmployeeOrderLookupPageUrl);

            EmployeeOrderLookup.StoreRadioButton.Click();

            Browser.Locate.ClickDropdownByValue(EmployeeOrderLookup.SearchTypeDropdown, "2");

            SoftVerify.True(EmployeeOrderLookup.PaginationDropdownPageOptions.Count > 0, "No page options in Pagination dropdown.");

            var orderFirstName = "ESTHELA";

            EmployeeOrderLookup.OrderSearchInput.SendKeys(orderFirstName);
            //EmployeeOrderLookup.OrderSearchInput.SendKeys(EmployeeOrderLookup.FirstOrderFirstName);
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
                SoftVerify.Equals(OrderDetails.RemoveFormatting(OrderDetails.BillingInfo(orderDetailItem)), OrderDetails.RemoveFormatting(OrderDetails.GetOrderBillingInfo()), "Billing information does not match");
                SoftVerify.Equals(OrderDetails.RemoveFormatting(OrderDetails.ShippingInfo(orderDetailItem)), OrderDetails.RemoveFormatting(OrderDetails.GetOrderShippingInfo()), "Shipping information not match");
                SoftVerify.StringContains(OrderDetails.GetSalesAssociateInfo(), orderDetailItem.SalesAssociate.ToString(), "Sales Associate not match");
                SoftVerify.Equals(orderDetailItem.RewardNumber.ToString(), OrderDetails.RewardNumber.Text, "Sales Associate not match");
            }
        }
    }
}
