using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.Shipping.T7991_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7991_iPhone_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress : T7991_MobileBase
    {
        public T7991_iPhone_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress(ITestOutputHelper output, T7991_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfOcPageWithDifferentShipAndBillAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7991_AndroidPhone_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress : T7991_MobileBase
    {
        public T7991_AndroidPhone_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress(ITestOutputHelper output, T7991_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfOcPageWithDifferentShipAndBillAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7991_Emulator_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress : T7991_MobileBase
    {
        public T7991_Emulator_VerifyLayoutOfOcPageWithDifferentShipAndBillAddress(ITestOutputHelper output, T7991_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfOcPageWithDifferentShipAndBillAddress(string config) => Validate(Validate, config);
    }


    public class T7991_SharedSkus_Fixture : FixtureBase
    {
        public string Shortsku { get; }
        public Address ShippingAddress1 { get; }
        public Address ShippingAddress2 { get; }

        public T7991_SharedSkus_Fixture()
        {
            Shortsku = ProductActions.GetSkuGreaterThanTwoHundredDollars;
            ShippingAddress1 = new Address { };
            ShippingAddress2 = new Address { AddressLine1 = "9201 Winnetka Ave" };
        }
    }


    /// <summary>
    /// Verify the Layout of the Order Confirmation Page Which Has Different Shipping and Billing Address
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10853
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7991
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10853"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7991")]
    public abstract class T7991_MobileBase : VisualTestsBaseMobile, IClassFixture<T7991_SharedSkus_Fixture>
    {
        protected readonly T7991_SharedSkus_Fixture Fixture;

        protected T7991_MobileBase(ITestOutputHelper output, T7991_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange: User has two saved addresses & Add a Product to cart 
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedAddresses();

            Browser.Navigate(Urls.ManageAccountPageUrl);
            ManageAccountWorkflow.AddMultipleShippingAddress(Fixture.ShippingAddress1, Fixture.ShippingAddress2);

            var sku = Fixture.Shortsku;
            Assert.DatabaseObject(Fixture.Shortsku, "ProductActions.GetSkuGreaterThanTwoHundredDollars;");

            ProductDetail.AddSingleProductToCart(sku);
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            /* Act:
             Proceed to Shipping Page
             Tab on Address box
             From Select a Shipping Address modal select non default address
             Proceed to Place an order
            */
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not shipping page");

            CustomerAddressInformation.SelectSavedAddressShippingInfo();
            Shipping.SelectNotDefaultShippingAddress(1);
            Shipping.WaitForModalToFullyClose();

            Shipping.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "Current page is not Billing page");

            Payment.SelectSameAsShippingCheckbox();
            CustomerAddressInformation.EnterBillingAddress(IntAddress, true);

            Payment.SelectInternationalAgreementAndPlaceOrder();
            Assert.True(OrderConfirmation.IsCurrentPage, "User is not on the Order Confirmation page.");

            // Act: Capture Visible Screen and Ignore Email and Order Id
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, OrderConfirmation.IgnoreEmailIdAndOrderId(), true);

            // Act: Data Clean Up
            Browser.Navigate(Urls.ManageAccountPageUrl);
            ManageAccountWorkflow.DeleteAllSavedAddresses();
        }
    }
}