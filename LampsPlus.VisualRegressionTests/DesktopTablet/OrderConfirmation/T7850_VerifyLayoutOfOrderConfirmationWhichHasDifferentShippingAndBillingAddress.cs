using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7850_Windows_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress : T7850_DesktopBase
    {
        public T7850_Windows_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(ITestOutputHelper output, T7850_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7850_Mac_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress : T7850_DesktopBase
    {
        public T7850_Mac_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(ITestOutputHelper output, T7850_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7850_iPad_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress : T7850_DesktopBase
    {
        public T7850_iPad_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(ITestOutputHelper output, T7850_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7850_TabletEmulator_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress : T7850_DesktopBase
    {
        public T7850_TabletEmulator_VerifyLayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(ITestOutputHelper output, T7850_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfOrderConfirmationWhichHasDifferentShippingAndBillingAddress(string config) => Validate(Validate, config);
    }


    public class T7850_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuGreaterThanTwoHundredDollars { get; }

        public T7850_SharedProductSku_Fixture()
        {
            SkuGreaterThanTwoHundredDollars = ProductActions.GetSkuGreaterThanTwoHundredDollars;
        }
    }


    /// <summary>
    /// Verify the Layout of the Order Confirmation Page Which Has Different Shipping and Billing Address.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9652
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7850
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9652"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7850")]

    public abstract class T7850_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7850_SharedProductSku_Fixture>
    {
        protected readonly T7850_SharedProductSku_Fixture Fixture;

        protected T7850_DesktopBase(ITestOutputHelper output, T7850_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange:
             Delete Saved Addresses
             Delete Saved Payment Options
             User has two saved address
             Delete Cart
             User has identified a SKU > $200
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.SkuGreaterThanTwoHundredDollars, "ProductActions.GetSkuGreaterThanTwoHundredDollars");

            Browser.Navigate(Urls.ManageAccountPageUrl);
            Browser.Wait.ForDomReady();
            ManageAccount.ResetAccountShippingAddresses();
            ManageAccount.ResetAccountPaymentOptions();

            Browser.Navigate(Urls.ManageShippingAddressPageUrl);

            ManageAccount.AddShippingAddress(Address);
            Address.AddressLine1 = "9201 Winnetka Ave";
            ManageAccount.AddShippingAddress(Address);

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Cart.RemoveCartItems();

            var sku = Fixture.SkuGreaterThanTwoHundredDollars;

            // Act: Add Product to Cart > $100 and Navigate to Shipping Page.
            Browser.Navigate(Urls.LampsPlusProductsUrl + sku);
            ProductDetail.AddToCart();
            Cart.CheckOut();

            // Act: Click on SHIP TO A DIFFERENT ADDRESS.
            Shipping.WaitForShippingPageToLoad();
            Shipping.ShipToDifferentAddress();

            // Act: Select not default shipping address.
            Shipping.SelectNotDefaultShippingAddress(1);
            Shipping.WaitForModalToFullyClose();

            // Act: Proceed to Payment Page.
            Shipping.ProceedToPayment();

            // Act: Deselect Same as shipping checkbox and enter International Billing Address.
            Payment.SelectSameAsShippingCheckbox();

            CustomerAddressInformation.EnterBillingAddress(IntAddress, true);

            // Act: Select International Agreement Checkbox and Place Order.
            Payment.SelectInternationalAgreementAndPlaceOrder();
            Assert.True(OrderConfirmation.IsCurrentPage, "User is not on the Order Confirmation page.");
            OrderConfirmation.IsLincOptionWidgetVisible();

            // Act: Capture Visible Screen and Ignore Email and Order Id.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { OrderConfirmation.IgnoreEmailId(), OrderConfirmation.IgnoreOrderId() });
        }
    }
}
