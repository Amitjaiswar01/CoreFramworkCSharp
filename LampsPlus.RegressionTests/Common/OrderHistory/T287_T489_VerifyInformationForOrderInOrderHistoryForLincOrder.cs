using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderHistory;

namespace LampsPlus.RegressionTests.Common.OrderHistory
{
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T287_Windows_VerifyInfoForLincOrder : T287_DesktopBase
	{
		public T287_Windows_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T287_Mac_VerifyInfoForLincOrder : T287_DesktopBase
    {
        public T287_Mac_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T287_iPad_VerifyInfoForLincOrder : T287_DesktopBase
    {
        public T287_iPad_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T287_TabletEmulator_VerifyInfoForLincOrder : T287_DesktopBase
    {
        public T287_TabletEmulator_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T489_iPhone_VerifyInfoForLincOrder : T489_MobileBase
    {
        public T489_iPhone_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T489_Emulator_VerifyInfoForLincOrder : T489_MobileBase
    {
        public T489_Emulator_VerifyInfoForLincOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void InfoForLincOrder(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the information for an Order in the Order History for a Linc Order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5072
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T287
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5072"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T287")]
	public abstract class T287_DesktopBase : T287_T489_Base
	{
		protected T287_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


    /// <summary>
    /// Verify the information for an Order in the Order History for a Linc Order.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5062
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T489
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5062"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T489")]
	public abstract class T489_MobileBase : T287_T489_Base
	{
		protected T489_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T287_T489_Base : OrderHistoryTestsBase 
	{
		protected T287_T489_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            List<OrderHistoryItems> orderHistoryItems = null;
            bool checkLincWidget = false;
            while (!checkLincWidget)
            {
                Browser.Navigate(Urls.OrderHistoryPageUrl);

                var lincOrderIdFromDatabase = OrderActions.GetLincQualifyingOrders();

                Log.Message($"Order ID selected for test: {lincOrderIdFromDatabase.OrderId}, User Name selected for test: {lincOrderIdFromDatabase.UserName}");

                Assert.DatabaseObject(lincOrderIdFromDatabase, "OrderActions.GetLincQualifyingOrders()");

                Browser.Wait.ForDisplayedElement(OrderHistory.OrderIdField, 30);
                OrderHistory.OrderIdField.SendKeys(lincOrderIdFromDatabase.OrderId);
                OrderHistory.EmailField.SendKeys(lincOrderIdFromDatabase.UserName);
                OrderHistory.CheckStatusBtn.Click();

                orderHistoryItems = OrderActions.GetOrderHistoryItems(lincOrderIdFromDatabase.OrderId);
                try
                { 
                    checkLincWidget = Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistory.LincOptInWidgetClass.ToCssClassSelector()), 10);
                }
                catch
                {
                   Log.Message("This order does not have Linc Widget");
                }
            }

            VerifyOrderHistoryItemsTable(orderHistoryItems);

            Assert.True(OrderHistory.OptInWidgetElement.Displayed, "Linc widget is not visible");
        }

        private void VerifyOrderHistoryItemsTable(List<OrderHistoryItems> orderHistoryItems)
        {
            foreach (var orderDetailItem in orderHistoryItems)
            {
                var matchedRow = OrderDetails.OrderDetailTableRows.First(r => r.Text.Contains(orderDetailItem.ShortSku));

                Assert.Equals(OrderDetails.RemoveFormatting(OrderDetails.GetProductName(matchedRow).FindElement(By.TagName("a")).Text), OrderDetails.RemoveFormatting(OrderDetails.GetProductNameString(orderDetailItem)), "Product Name and Sku does not match");
                Assert.Equals(OrderDetails.GetProductStatus(matchedRow).Text.Trim(), OrderDetails.GetStatusString(orderDetailItem).Trim(), "Item Status not matched");
                Assert.Equals(orderDetailItem.Quantity.ToString(), OrderDetails.GetProductQty(matchedRow).Text.Replace(OrderDetails.QuantityString, string.Empty).Trim(), "Quantities do not match");
                Assert.Equals(OrderDetails.FormatPrice(orderDetailItem.UnitPrice), OrderDetails.GetProductUnitPrice(matchedRow).Text.Replace(OrderDetails.PriceString, string.Empty).Trim(), "Unit prices do not match");
                Assert.Equals(OrderDetails.OrderTotal.Text, TextActions.FormatPrice(orderDetailItem.OrderTotal), "Order Total prices not match");
                Assert.Equals(OrderDetails.ShippingProcessing.Text.Trim(), OrderDetails.ShippingProcessingPrice(orderDetailItem).Trim(), "Shipping and Processing prices do not match");
                Assert.Equals(OrderDetails.OrderTax.Text.Trim(), TextActions.FormatPrice(orderDetailItem.TaxTotal).Trim(), "Tax prices do not match");

                Assert.Displayed(OrderDetails.TrackItemLink, "Track Item Link is not displayed");
            }
        }
    }
}