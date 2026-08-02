using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T7748_T7749_VerifyPayPalWidgetWithProductPriceProductLessThan30dollar
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7749_iPhone_PayPalWidgetWithPriceProductLessThan30dollar : T7749_MobileBase
    {
        public T7749_iPhone_PayPalWidgetWithPriceProductLessThan30dollar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void PayPalWidgetDisplayedWithDifferentVerbiage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7749_Android_PayPalWidgetWithPriceProductLessThan30dollar : T7749_MobileBase
    {
        public T7749_Android_PayPalWidgetWithPriceProductLessThan30dollar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void PayPalWidgetDisplayedWithDifferentVerbiage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7749_Emulator_PayPalWidgetWithPriceProductLessThan30dollar : T7749_MobileBase
    {
        public T7749_Emulator_PayPalWidgetWithPriceProductLessThan30dollar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PayPalWidgetDisplayedWithDifferentVerbiage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify PayPal Widget is Displayed With Different Verbiage Depending On Product Price Range.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10806
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7749
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10806"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7749")]
    public abstract class T7749_MobileBase : TestsBaseMobile
    {
        protected T7749_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange : Identify SKUs with a price less than $30
            InitializeFunctionalTest(config);

            var skuPriceLessThan30 = ProductActions.GetSkuWithPriceLessThan30;
            Assert.DatabaseObject(skuPriceLessThan30, "ProductActions.GetSkuWithPriceLessThan30");

            // Act : Navigate to Pdp and scroll to Paypal Widget
            ProductDetail.NavigateToProductDetailByShortSku(skuPriceLessThan30);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not PDP page");
            ProductDetail.GetPayPalLogo();

            // Assert : Verification of Widget on PDP Page
            Assert.Equals(ProductDetail.GetPayPalCalloutPDP(), Messages.PayPalPriceMessage.PayPalLessThan30Message, "PayPal Later Verbiage does not match for Less than $30");

            // Act : Add product to Cart
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Act: Confirm Paypal verbiage is displayed on cart page
            ProductDetail.GetPayPalLogo();

            // Assert : Verification on Cart Page
            Assert.Equals(ProductDetail.GetPayPalCalloutPDP(), Messages.PayPalPriceMessage.PayPalLessThan30Message, "PayPal Later Verbiage does not match for Less than $30");

            // Act: Proceed to Shipping Page
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not Shipping page");

            // Assert : Verification on Shipping Page
            Shipping.OpenOrderSummaryBlock();
            ProductDetail.GetPayPalLogo();
            Assert.Equals(ProductDetail.GetPayPalCalloutPDP(), Messages.PayPalPriceMessage.PayPalLessThan30Message, "PayPal Later Verbiage does not match for Less than $30");

            // Act : Add US Shipping Address
            Payment.CloseOrderSummaryDropdown();
            CustomerAddressInformation.EnterShippingAddress(new Address());

            // Act : Proceed to Billing Page
            ShoppingCartWorkflow.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "Current Page is not Payment page");
            Shipping.OpenOrderSummaryBlock();
            ProductDetail.GetPayPalLogo();

            // Assert : Verification on Billing Page
            Assert.Equals(ProductDetail.GetPayPalCalloutPDP(), Messages.PayPalPriceMessage.PayPalLessThan30Message, "PayPal Later Verbiage does not match for Less than $30");

            // Act : Data CleanUp
            Payment.CloseOrderSummaryDropdown();
            ShoppingCartWorkflow.EmptyCart();
        }
    }
}