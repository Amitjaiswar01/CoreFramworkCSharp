using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T7982_T7984_VerifyPayPalWidgetPriceBetween1500to9000
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7982_Windows_VerifyPayPalWidgetPriceBetween1500to9000 : T7982_DesktopBase
    {
        public T7982_Windows_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7982_Mac_VerifyPayPalWidgetPriceBetween1500to9000 : T7982_DesktopBase
    {
        public T7982_Mac_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7982_iPad_VerifyPayPalWidgetPriceBetween1500to9000 : T7982_DesktopBase
    {
        public T7982_iPad_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7982_TabletEmulator_VerifyPayPalWidgetPriceBetween1500to9000 : T7982_DesktopBase
    {
        public T7982_TabletEmulator_VerifyPayPalWidgetPriceBetween1500to9000(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PayPalWidgetPriceBetween1500To9000(string config) => Validate(config);
    }


    /// <summary>
    /// Verify PayPal Widget is Displayed For Product Price Between $1500 and $9000
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10808
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7982
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10808"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7982")]
    public abstract class T7982_DesktopBase : TestsBaseDesktop
    {
        protected T7982_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Identify SKUs with a price between $1500 and $9000
            InitializeFunctionalTest(config);

            var sku = ProductActions.GetSkuWithPriceMoreThan1500;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuWithPriceMoreThan1500");

            //Act: Navigate to Pdp and scroll to Paypal Widget
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not PDP page");
            ProductDetail.GetPayPalLogo();

            //Assert: Verify PayPal Widget on PDP Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            //Act: Add product to Cart & Confirm Paypal verbiage is displayed on cart page
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            ProductDetail.GetPayPalLogo();

            //Assert: Verify PayPal Widget on Cart Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            //Act: Proceed to Shipping Page
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not Shipping page");

            //Assert: Verify PayPal Widget on Shipping Page
            ProductDetail.GetPayPalLogo();
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());

            //Act: Add US Shipping Address & Proceed to Billing Page
            CustomerAddressInformation.EnterShippingAddress(new Address());
            ShoppingCartWorkflow.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "Current Page is not Payment page");
            ProductDetail.GetPayPalLogo();

            //Assert: Verify PayPal Widget on Billing Page
            PayPalVerbiageCondition(ProductDetail.GetPayPalCalloutPDP());
        }

        private void PayPalVerbiageCondition(string paypalCallOut)
        {
            if (paypalCallOut.StartsWith("Pay"))
            {
                Assert.Equals(paypalCallOut, Messages.PayPalPriceMessage.PayPalBetween1500And9000Message, "PayPal Callout does not match");
            }
            else
            {
                Assert.Equals(paypalCallOut.Replace("\r\n", string.Empty), $"As low as ${decimal.Parse(TextActions.GetPriceTextOnly(paypalCallOut))}/mo. with PayPal. Learn more", "PayPal Verbiage does not match for value in between $1500 And $9000");
            }
        }
    }
}