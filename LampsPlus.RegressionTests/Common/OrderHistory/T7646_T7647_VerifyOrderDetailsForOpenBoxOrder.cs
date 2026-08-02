using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderHistory;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.OrderHistory
{
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7646_Windows_VerifyOrderDetailsForOpenBoxOrder : T7646_DesktopBase
    {
        public T7646_Windows_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void OrderDetailsOpenBoxOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7646_Mac_VerifyOrderDetailsForOpenBoxOrder : T7646_DesktopBase
    {
        public T7646_Mac_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void OrderDetailsOpenBoxOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7646_iPad_VerifyOrderDetailsForOpenBoxOrder : T7646_DesktopBase
    {
        public T7646_iPad_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void OrderDetailsOpenBoxOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7646_TabletEmulator_VerifyOrderDetailsForOpenBoxOrder : T7646_DesktopBase
    {
        public T7646_TabletEmulator_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void OrderDetailsOpenBoxOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7647_iPhone_VerifyOrderDetailsForOpenBoxOrder : T7647_MobileBase
    {
        public T7647_iPhone_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void OrderDetailsOpenBoxOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7647_Emulator_VerifyOrderDetailsForOpenBoxOrder : T7647_MobileBase
    {
        public T7647_Emulator_VerifyOrderDetailsForOpenBoxOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the information for an Order in the Order History for a Linc Order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8863
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7646
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8863"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7646")]
    public abstract class T7646_DesktopBase : T7646_T7647_Base
    {
        protected T7646_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the information for an Order in the Order History for a Linc Order.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8863
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7647
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8863"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T489")]
    public abstract class T7647_MobileBase : T7646_T7647_Base
    {
        protected T7647_MobileBase(ITestOutputHelper output) : base(output) { }
    }
    public abstract class T7646_T7647_Base : OrderHistoryTestsBase
    {
        protected T7646_T7647_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {

            InitializeFramework(config, Urls.OrderHistoryPageUrl);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistory.CheckStatusButtonId.ToCssIdSelector()));

            SignInWorkflow.EnsureUserSignedOut();

            var order = OrderActions.GetOpenBoxOrder();

            OrderHistory.OrderIdField.SendKeys(order.OrderId);
            OrderHistory.EmailField.SendKeys(order.UserName);
            OrderHistory.CheckStatusBtn.Click();

            var orderHistoryItems = OrderActions.GetOrderHistoryItems(order.OrderId);

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistory.OrderHistoryRowsTable.ToCssClassSelector()));

            VerifyOrderHistoryItemsTable(orderHistoryItems);

            Assert.True(OrderHistory.OpenBoxLabel.Displayed, "Open Box Label is not visible");
        }


        private void VerifyOrderHistoryItemsTable(List<OrderHistoryItems> orderHistoryItems)
        {
            OrderDetails.HandleShippingUpdatesModal();

            foreach (var orderDetailItem in orderHistoryItems)
            {
                var matchRow = OrderDetails.OrderDetailTableRows.SingleOrDefault(r => r.Text.Contains(orderDetailItem.ShortSku));

                Log.Message("Order Id: " + orderDetailItem.OrderId);

                Assert.Equals(OrderDetails.RemoveFormatting(OrderDetails.GetProductName(matchRow).FindElement(By.TagName("a")).Text), OrderDetails.RemoveFormatting(OrderDetails.GetProductNameString(orderDetailItem)), "Product Name and Sku does not match");
                Assert.Equals(OrderDetails.GetProductStatus(matchRow).Text.Trim(), OrderDetails.GetStatusString(orderDetailItem).Trim(), "Item Status not matched");
                Assert.Equals(OrderDetails.GetProductQuantity(matchRow).Text.Replace("Qty: ", "").Trim(), orderDetailItem.Quantity.ToString(), "Quantity not matched");
                Assert.Equals(OrderDetails.GetUnitPrice(matchRow).Text.Replace("Price: ", "").Trim(), TextActions.FormatPrice(orderDetailItem.UnitPrice), "Unit price not matched");
                Assert.Equals(OrderDetails.OrderItemTotal.Text, TextActions.FormatPrice(orderDetailItem.ItemTotal), "Item Total prices do not match");
                Assert.Equals(OrderDetails.ShippingProcessing.Text.Trim(), OrderDetails.ShippingProcessingPrice(orderDetailItem), "Shipping and Processing prices do not match");
                Assert.Equals(OrderDetails.OrderTax.Text.Trim(), TextActions.FormatPrice(orderDetailItem.TaxTotal), "Tax prices do not match");
                Assert.Equals(OrderDetails.OrderTotal.Text.Trim(), TextActions.FormatPrice(orderDetailItem.OrderTotal), "Order Total prices not match");

            }
        }
    }
}
