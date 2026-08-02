using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T7982_T7984_VerifyPayPalWidgetPriceBetween1500to9000
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7984_iPhone_VerifyPayPalWidgetPriceBetween1500to9000 : T7984_MobileBase
    {
        public T7984_iPhone_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7984_Android_VerifyPayPalWidgetPriceBetween1500to9000 : T7984_MobileBase
    {
        public T7984_Android_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7984_Emulator_VerifyPayPalWidgetPriceBetween1500to9000 : T7984_MobileBase
    {
        public T7984_Emulator_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    /// <summary>
    /// Verify PayPal Widget is Displayed For Product Price Between $1500 and $9000
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10808
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7984
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10808"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7984")]
    public abstract class T7984_MobileBase : TestsBaseMobile
    {
        protected T7984_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Identify SKUs with a price between $30 and $1,350
            InitializeFunctionalTest(config);

            var sku = ProductActions.GetSkuWithPriceMoreThan1500;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuWithPriceBetween30And1350");

            //Act: Navigate to Pdp and scroll to Paypal Widget
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not PDP page");
            ProductDetail.GetPayPalLogo();

            //Assert: Verification of PayPal Widget on PDP Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            //Act: Add product to Cart & Confirm Paypal verbiage is displayed on cart page
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            ProductDetail.GetPayPalLogo();

            //Assert: Verify PayPal Widget on Cart Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            //Act: Proceed to Shipping Page & Open Order Summary Block
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not Shipping page");
            Shipping.OpenOrderSummaryBlock();
            ProductDetail.GetPayPalLogo();

            //Assert: Verify PayPal Widget on Shipping Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            /*Act:
             Close Order Summary Block
             Add US Shipping Address
             Proceed to Billing Page
             Open Order Summary Block
            */
            Payment.CloseOrderSummaryDropdown();
            CustomerAddressInformation.EnterShippingAddress(new Address());
            ShoppingCartWorkflow.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "Current Page is not Payment page");
            Shipping.OpenOrderSummaryBlock();

            //Assert: Verify of PayPal Widget on Billing Page
            ProductDetail.GetPayPalLogo();
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());
        }

        private void PayPalVerbiageCondition(string paypalCallOut)
        {
            if (paypalCallOut.StartsWith("Pay"))
            {
                Assert.Equals(paypalCallOut, Messages.PayPalPriceMessage.PayPalBetween1500And9000Message, "PayPal Verbiage does not match for value in between $1500 And $9000");
            }
            else
            {
                Assert.Equals(paypalCallOut.Replace("\r\n", string.Empty), $"As low as ${decimal.Parse(TextActions.GetPriceTextOnly(paypalCallOut))}/mo. with PayPal. Learn more", "PayPal Verbiage does not match for value in between $1500 And $9000");
            }
        }
    }
}