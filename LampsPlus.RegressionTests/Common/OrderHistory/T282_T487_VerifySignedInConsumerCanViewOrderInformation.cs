using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderHistory;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.OrderHistory
{
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T282_Windows_VerifyConsumerCanViewOrderInfo : T282_DesktopBase
    {
        public T282_Windows_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T282_Mac_VerifyConsumerCanViewOrderInfo : T282_DesktopBase
    {
        public T282_Mac_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T282_iPad_VerifyConsumerCanViewOrderInfo : T282_DesktopBase
    {
        public T282_iPad_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T282_TabletEmulator_VerifyConsumerCanViewOrderInfo : T282_DesktopBase
    {
        public T282_TabletEmulator_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderHistory)]
    public class T487_iPhone_VerifyConsumerCanViewOrderInfo : T487_MobileBase
    {
        public T487_iPhone_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T487_Emulator_VerifyConsumerCanViewOrderInfo : T487_MobileBase
    {
        public T487_Emulator_VerifyConsumerCanViewOrderInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void ConsumerCanViewOrderInfo(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a signed-in consumer can view order information.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5116
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T282
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5116"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T282")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T282_DesktopBase : T282_T487_Base
    {
        protected T282_DesktopBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify Ext Price.
        /// </summary>
        protected override void VerifyExtPrice(OrderHistoryItems orderDetailItem, IElement orderDetailElement)
        {
            Assert.Equals(OrderDetails.FormatPrice(orderDetailItem.ExtPrice), TextActions.NormalizeWhitespace(OrderDetails.GetProductExtPrice(orderDetailElement).Text), "ExtPrices do not match");
        }

        protected override void WaitForOrderDetailsPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.XPath(OrderDetails.OrderIdRmaHeadingXPath));
        }

        protected override void WaitForOrderHistoryPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderDetails.BreadCrumbClass.ToCssClassSelector()));
        }
    }


    /// <summary>
    /// Verify a signed-in consumer can view order information.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5224
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T487
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5224"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T487")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T487_MobileBase : T282_T487_Base
    {
        protected T487_MobileBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify Ext Price.
        /// </summary>
	    protected override void VerifyExtPrice(OrderHistoryItems orderDetailItem, IElement orderDetailElement) { }

        protected override void WaitForOrderDetailsPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(OrderDetails.ProductColClass));
        }

        protected override void WaitForOrderHistoryPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistory.OrderClass.ToCssClassSelector()));
        }
    }

    
    public abstract class T282_T487_Base : OrderHistoryTestsBase
    {
        protected T282_T487_Base(ITestOutputHelper output) : base(output) { }
       
        protected void Validate(string config)
        {
            var setup = new TestSetup(config, Urls.OrderHistoryPageUrl);
            InitializeFramework(config, setup: setup);

            WaitForOrderHistoryPageToLoad();

            var orderRows = OrderHistory.OrderPreviewElements;
            Assert.Condition(() => orderRows.Count > 0, "Empty order history!");

            var index = MathHelper.GetRandomNumber(orderRows.Count);
            var orderDetailsLink = OrderHistory.GetOrderPreview(orderRows[index]).OrderIdElement;
            var orderId = orderDetailsLink.Text;
            var orderDetails = OrderActions.GetOrderHistoryItems(orderId);
            orderDetailsLink.Click();

            WaitForOrderDetailsPageToLoad();
            
            VerifyOrderDetailsItemsWithDatabase(orderDetails);
        }

        /// <summary>
        /// Verify Ext Price
        /// </summary>
        protected abstract void VerifyExtPrice(OrderHistoryItems orderDetailItem, IElement orderDetailElement);

        protected abstract void WaitForOrderDetailsPageToLoad();

        protected abstract void WaitForOrderHistoryPageToLoad();

        private void VerifyOrderDetailsItemsWithDatabase(List<OrderHistoryItems> orderHistoryItems)
        {
            foreach (var orderDetailItem in orderHistoryItems)
            {
                var matchedRow = OrderDetails.OrderDetailTableFirstRow.SingleOrDefault(r => r.Text.Contains(orderDetailItem.ShortSku));
                var orderStatus = OrderHistory.FormatOrderStatusFromDatabase(orderDetailItem);

                Assert.Equals(orderDetailItem.OrderId, OrderDetails.GetOrderId(), "Order Ids do not match");
                Assert.Equals(orderDetailItem.CreatedDate.Date, DateTime.Parse(OrderDetails.GetOrderDate(), new CultureInfo("en-US")).Date, "Order dates do not match");
                Assert.Equals(OrderDetails.GetProductNameString(orderDetailItem), TextActions.NormalizeWhitespace(OrderDetails.GetProductName(matchedRow).Text), "Product Names do not match");
                Assert.Equals(OrderDetails.FormatPrice(orderDetailItem.UnitPrice), OrderDetails.GetProductUnitPrice(matchedRow).Text.Replace(OrderDetails.PriceString, string.Empty).Trim(), "Unit prices do not match");
                Assert.Equals(orderDetailItem.Quantity.ToString(), OrderDetails.GetProductQty(matchedRow).Text.Replace(OrderDetails.QuantityString, string.Empty).Trim(), "Quantities do not match");

                VerifyExtPrice(orderDetailItem, matchedRow);

                Assert.Equals(orderStatus, TextActions.NormalizeWhitespace(OrderDetails.GetProductStatus(matchedRow).Text), "Item statuses do not match");
                VerifyShippingDetails(orderDetailItem);
                VerifyBillingDetails(orderDetailItem);
            }
        }

        private void VerifyShippingDetails(OrderHistoryItems item)
        {
            var shippingInfoOnPage = OrderDetails.GetOrderShippingInfo();
            var shipToFullName = $"{item.ShipToFirstName}{(item.ShipToLastName != string.Empty ? $" {item.ShipToLastName}" : string.Empty)}";
            VerifyIfStringIsContained(shippingInfoOnPage, shipToFullName);
            VerifyIfStringIsContained(OrderDetails.RemoveFormatting(shippingInfoOnPage), OrderDetails.RemoveFormatting(item.ShipToAddressLine1));
            VerifyIfStringIsContained(OrderDetails.RemoveFormatting(shippingInfoOnPage), OrderDetails.RemoveFormatting(item.ShipToAddressLine2));
            VerifyIfStringIsContained(shippingInfoOnPage, $"{item.ShipToCity},{item.ShipToState} {item.ShipToZipCode} {item.ShipToCountry}");
        }

        private void VerifyBillingDetails(OrderHistoryItems item)
        {
            var billingInfoOnPage = OrderDetails.GetOrderBillingInfo();
            var billToFullName = $"{item.BillToFirstName}{(item.BillToLastName != string.Empty ? $" {item.BillToLastName}" : string.Empty)}";
            VerifyIfStringIsContained(billingInfoOnPage, billToFullName);
            VerifyIfStringIsContained(OrderDetails.RemoveFormatting(billingInfoOnPage), OrderDetails.RemoveFormatting(item.BillToAddressLine1));
            VerifyIfStringIsContained(OrderDetails.RemoveFormatting(billingInfoOnPage), OrderDetails.RemoveFormatting(item.BillToAddressLine2));
            VerifyIfStringIsContained(billingInfoOnPage, $"{item.BillToCity},{item.BillToState} {item.BillToZipCode} {item.BillToCountry}");
        }
    }
}
