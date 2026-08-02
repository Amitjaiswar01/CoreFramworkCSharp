using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T7981_T7983_VerifyPayPalWidgetPricesBetween30To1350
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7981_Windows_VerifyPayPalWidgetPricesBetween30To1350 : T7981_DesktopBase
    {
        public T7981_Windows_VerifyPayPalWidgetPricesBetween30To1350(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PayPalWidgetPricesBetween30To1350(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7981_Mac_VerifyPayPalWidgetPricesBetween30To1350 : T7981_DesktopBase
    {
        public T7981_Mac_VerifyPayPalWidgetPricesBetween30To1350(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PayPalWidgetPricesBetween30To1350(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7981_iPad_VerifyPayPalWidgetPricesBetween30To1350 : T7981_DesktopBase
    {
        public T7981_iPad_VerifyPayPalWidgetPricesBetween30To1350(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PayPalWidgetPricesBetween30To1350(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7981_TabletEmulator_VerifyPayPalWidgetPricesBetween30To1350 : T7981_DesktopBase
    {
        public T7981_TabletEmulator_VerifyPayPalWidgetPricesBetween30To1350(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PayPalWidgetPricesBetween30To1350(string config) => Validate(config);
    }


    /// <summary>
    /// Verify PayPal Widget is Displayed For Product Prices Between $30 and $1,350
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10807
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7981 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10807"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7981")]
    public abstract class T7981_DesktopBase : TestsBaseDesktop
    {
        protected T7981_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Identify SKUs with a price between $30 and $1,350
            InitializeFunctionalTest(config);

            var sku = ProductActions.GetSkuWithPriceBetween30And1350;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuWithPriceBetween30And1350");

            //Act: Navigate to Pdp and scroll to Paypal Widget
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not PDP page");
            ProductDetail.GetPayPalLogo();
            var productPricesPdp = decimal.Parse(TextActions.GetPriceTextOnly(ProductDetail.GetProductPrice().ToString(CultureInfo.InvariantCulture)));

            //Assert: Verification of PayPal Widget on PDP Page
            PayPalVerbiageCondition(productPricesPdp, ProductDetail.GetPayPalCalloutPDP());

            //Act: Add product to Cart & Confirm Paypal verbiage is displayed on cart page
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            ProductDetail.GetPayPalLogo();
            var cartProductPrices = Cart.GetOrderTotalCost();

            //Assert: Verification of PayPal Widget on Cart Page
            PayPalVerbiageCondition(cartProductPrices, ProductDetail.GetPayPalCalloutPDP());

            //Act: Proceed to Shipping Page
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not Shipping page");

            //Assert: Verification of PayPal Widget on Shipping Page
            ProductDetail.GetPayPalLogo();
            PayPalVerbiageCondition(cartProductPrices, ProductDetail.GetPayPalCalloutPDP());

            //Act: Add US Shipping Address & Proceed to Billing Page
            CustomerAddressInformation.EnterShippingAddress(new Address());
            ShoppingCartWorkflow.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "Current Page is not Payment page");
            ProductDetail.GetPayPalLogo();
            var billingProductPrices = Cart.GetOrderTotalCost();

            //Assert: Verification of PayPal Widget on Billing Page
            PayPalVerbiageCondition(billingProductPrices, ProductDetail.GetPayPalCalloutPDP());
        }

        private void PayPalVerbiageCondition(decimal productPricesPdp, string paypalCallOut)
        {
            var interestRate = ProductDetail.CalculatePayPalInterestRate(productPricesPdp);
            var retrieveSiteAmt = string.Concat(paypalCallOut.SkipWhile(c => c != '$').Skip(1).TakeWhile(c => char.IsDigit(c) || c == '.'));

            if (paypalCallOut.StartsWith("Pay"))
            {
                var upperLimit = (decimal.Parse(interestRate) + .01m).ToString(CultureInfo.InvariantCulture);
                var downLimit = (decimal.Parse(interestRate) - .01m).ToString(CultureInfo.InvariantCulture);

                if (upperLimit == retrieveSiteAmt)
                {
                    Assert.True(paypalCallOut.Equals($"Pay in 4 interest-free payments of ${retrieveSiteAmt} with PayPal. Learn more"), "PayPal Verbiage does not match for value in between $30 And $1350");
                }
                else if (downLimit == retrieveSiteAmt)
                {
                    Assert.True(paypalCallOut.Equals($"Pay in 4 interest-free payments of ${retrieveSiteAmt} with PayPal. Learn more"), "PayPal Verbiage does not match for value in between $30 And $1350");
                }
                else if (interestRate == retrieveSiteAmt)
                {
                    Assert.True(paypalCallOut.Equals($"Pay in 4 interest-free payments of ${retrieveSiteAmt} with PayPal. Learn more"), "PayPal Verbiage does not match for value in between $30 And $1350");
                }
                else
                {
                    throw new NotFoundException("The PayPal String doesn't contains any of the value");
                }
            }
            else
            {
                Assert.True(paypalCallOut.Replace("\r\n", string.Empty).Equals($"As low as ${decimal.Parse(TextActions.GetPriceTextOnly(paypalCallOut))}/mo. with PayPal. Learn more"), "PayPal Verbiage does not match for value in between $30 And $1350");
            }
        }
    }
}