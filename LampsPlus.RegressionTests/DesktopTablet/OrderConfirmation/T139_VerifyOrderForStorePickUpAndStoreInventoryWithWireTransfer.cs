using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T139_Windows_VerifyPickupAndInvWithWireTrans : T139_DesktopBase
	{
		public T139_Windows_VerifyPickupAndInvWithWireTrans(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T139. Rework - ACD-10910")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
		public void PickupAndInvWithWireTrans(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T139_Mac_VerifyPickupAndInvWithWireTrans : T139_DesktopBase
    {
        public T139_Mac_VerifyPickupAndInvWithWireTrans(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void PickupAndInvWithWireTrans(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T139_iPad_VerifyPickupAndInvWithWireTrans : T139_DesktopBase
    {
        public T139_iPad_VerifyPickupAndInvWithWireTrans(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void PickupAndInvWithWireTrans(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T139_TabletEmulator_VerifyPickupAndInvWithWireTrans : T139_DesktopBase
    {
        public T139_TabletEmulator_VerifyPickupAndInvWithWireTrans(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void PickupAndInvWithWireTrans(string config) => Validate(config);
    }


    /// <summary>
	/// Verify that an order can be placed for a Store Pickup and Store Inventory item and using the Wire Transfer Payment method.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6569
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T139
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6569"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T139")]
	[Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
	//[Collection(LpTraits.UserRole.Employee)]
	public abstract class T139_DesktopBase : OrderConfirmationTestsBase
    {
		protected T139_DesktopBase(ITestOutputHelper output) : base(output) { }
        
		protected void Validate(string config)
        {
            var setup = new TestSetup(config) { AccountConfig = { ClearStoreInSessionOnSetup = true } };
            InitializeFramework(config, setup: setup);

            // Add Items To Cart
            var shortSku1 = ProductActions.GetBopusEligibleSku;
            Assert.DatabaseObject(shortSku1, "ProductActions.GetBopusEligibleSku()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku1 });
            var shortSku2 = ProductActions.GetLessThanTenDollarItem;
            Assert.DatabaseObject(shortSku2, "ProductActions.GetLessThanTenDollarItem()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku2 });
            
            // Cart
            CartOverview.RemovePromoCode();
            var zipCode = ProductActions.GetRandomStoreZipCode();
            Assert.DatabaseObject(zipCode, "ProductActions.GetRandomStoreZipcCode()");

            Assert.Equals(Urls.CartOverviewPageUrl, Browser.PageUrl, "Cart Url does not match.");

            CartOverview.SelectStorePickupShippingOption(1, zipCode);
            CartOverview.SelectStoreInventoryShippingOption(0, zipCode);
            Browser.ScrollToTopOfWindow();
            CsrBlock.SelectSaleSource(Sources.CartSources.SalesPhone);
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            CartOverview.CheckOutNowButton.Click();

            // Shipping
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
            Assert.Equals(Urls.ShippingPageUrl, Browser.PageUrl, "Shipping Url does not match.");
            Shipping.EmailField.Clear();
            Shipping.EmailField.SendKeys("lamps_plus_test_automation@mailinator.com");
            OrderSummaryBlock.ProceedToPaymentButton.Click();

            // Billing
            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));
            Assert.Equals(Browser.PageUrl, Urls.PaymentPageUrl, "Not on the billing page");
            ShoppingCartWorkflow.EmployeePlaceOrderWithDefaultAddressViaWireTransfer();
            Assert.Equals(Browser.PageUrl, Urls.OrderConfirmationPageUrl, "Not on the order confirmation page");

            // Order Confirmation - Verify Order Confirmation Details
            Browser.Wait.AreAllElementsVisible(By.ClassName(OrderConfirmation.OrderConfirmationOrderIdClass));
            var storePickup = OrderConfirmation.OrderItemShipmentLabel(0);
            var storePickupAvailableNow = OrderConfirmation.OrderItemShipmentLabel(1);
            VerifyOrderShippingLabel(storePickup.InternalElement, OrderConfirmation.StorePickupLabel);
            VerifyOrderShippingLabel(storePickupAvailableNow.InternalElement, OrderConfirmation.StorePickupAvailableNowLabel);
        }
    }
}