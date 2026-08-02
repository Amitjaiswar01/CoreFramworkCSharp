using System.Collections.Generic;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderHistory
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    //[Collection(LpTraits.UserRole.Employee)]
    public class T286_Windows_VerifyPayPalOrderForCsrLoginAccount : T286_DesktopBase
    {
        public T286_Windows_VerifyPayPalOrderForCsrLoginAccount(ITestOutputHelper output) : base(output) { }
        
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void PayPalOrderForCsrLoginAccount (string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T286_Mac_VerifyPayPalOrderForCsrLoginAccount : T280_DesktopBase
    {
        public T286_Mac_VerifyPayPalOrderForCsrLoginAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void PayPalOrderForCsrLoginAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T286_iPad_VerifyPayPalOrderForCsrLoginAccount : T280_DesktopBase
    {
        public T286_iPad_VerifyPayPalOrderForCsrLoginAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void PayPalOrderForCsrLoginAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T286_TabletEmulator_VerifyPayPalOrderForCsrLoginAccount : T280_DesktopBase
    {
        public T286_TabletEmulator_VerifyPayPalOrderForCsrLoginAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void PayPalOrderForCsrLoginAccount(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the information for a PayPal Order in the Order History.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5377
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T286
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5377"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T286")]
    public abstract class T286_DesktopBase : OrderHistoryTestsBase
    {
        protected T286_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config, Urls.OrderHistoryPageUrl);
            InitializeFramework(config, setup: setup);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            var paypalOrderIdFromDatabase = OrderActions.GetAnOrderIdPlacedWithPayPal();

            Assert.DatabaseObject(paypalOrderIdFromDatabase, "OrderActions.GetAnOrderIdPlacedWithPayPal()");

            OrderHistory.OrderIdField.SendKeys(paypalOrderIdFromDatabase.OrderId);
            OrderHistory.EmailField.SendKeys(paypalOrderIdFromDatabase.UserName);
            OrderHistory.CheckStatusBtn.Click();

            Browser.Wait.ForClickableElement(OrderHistory.ProductLink);

            var orderDetailsHistoryItems = OrderActions.GetOrderHistoryItems(paypalOrderIdFromDatabase.OrderId);

            VerifyOrderDetailsItemsTableForPayPalOrder(orderDetailsHistoryItems);
        }

        private void VerifyOrderDetailsItemsTableForPayPalOrder(List<OrderHistoryItems> orderHistoryItems)
        {
            foreach (var orderDetailItem in orderHistoryItems)
            {
                VerifyOrderDetails(orderDetailItem);

                var payPalPayment = OrderDetails.OrderDetailPayPal;
                Assert.Equals(payPalPayment , orderDetailItem.PaymentMethod.ToLower(), "Payment methods do not matched");
                Assert.Equals(OrderDetails.RemoveFormatting(OrderDetails.GetOrderShippingInfo()), OrderDetails.RemoveFormatting(OrderDetails.ShippingInfo(orderDetailItem)), "Shipping information does not match");
                Assert.Equals(OrderDetails.ShippingProcessing.Text, OrderDetails.ShippingProcessingPrice(orderDetailItem), $"{RecurringDataIssue}Shipping and Processing prices do not match");
            }
        }
    }
}
