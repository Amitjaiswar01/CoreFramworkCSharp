using System.Linq;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T118_VerifyDiscountGivenWhenEsiAppliesDiscountLessThanAllowed
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T118_Windows_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed : T118_DesktopBase
    {
        public T118_Windows_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void DiscEsiAppliesDiscLessAllowed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T118_Mac_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed : T118_DesktopBase
    {
        public T118_Mac_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void DiscEsiAppliesDiscLessAllowed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T118_iPad_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed : T118_DesktopBase
    {
        public T118_iPad_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void DiscEsiAppliesDiscLessAllowed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T118_TabletEmulator_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed : T118_DesktopBase
    {
        public T118_TabletEmulator_VerifyADiscountIsGivenAtOrderLevelWhenAnEsiAppliesADiscountLessThanAllowed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void DiscEsiAppliesDiscLessAllowed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a discount is given at the order level when ESI applies discount less than allowed
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9924
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T118
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9924"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T118")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]

    public abstract class T118_DesktopBase : TestsBaseDesktop
    {
        protected T118_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange: Add two unique products with quantity 1 to cart
            const int discountRate = 2;

            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetManualDiscountableShortSku();
            var shortSku1 = shortSku.First();

            Assert.DatabaseObject(shortSku1, "ProductActions.GetManualDiscountableShortSku()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel  { Sku = shortSku1});

            var shortSku2 = shortSku.Last();

            Assert.DatabaseObject(shortSku2, "ProductActions.GetManualDiscountableShortSku()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku2 });

            // Assert : Verify MD % Apply button is visible
            Assert.Displayed(CsrBlock.GetMdPercentageButton(), "Applied percent button not displayed.");

            // Act : Apply discount rate to the MD% block and check addition discount is visible
            CsrBlock.ApplyCartLevelDiscount(discountRate);
            Cart.IsAdditionalDiscountDisplayed();

            /* Assert : Verify Additional discount is applied and value is negative
            Verify Additional discounts label displayed correctly
            */
            Assert.True( Cart.GetAdditionalDiscounts() < 0, "Additional Discounts amount is not negative.");
            Assert.Equals(Cart.GetAdditionalDiscountLabel(), Cart.GetAdditionalDiscountsLabel(), "Additional Discounts label did not display");

            var subtotal = Cart.GetCalculateSubTotal(discountRate, true, true);
            var orderTotal = subtotal + Cart.GetShippingCost() + Cart.GetSaleTaxAmount();

            // Assert : Verify the order total and sub total displays correctly
            Assert.Equals(subtotal, Cart.GetSubTotalCost(), "Value of subtotal do not match.");
            Assert.Equals(orderTotal, Cart.GetOrderTotalCost(), "Order total do not match.");

            // Act : Delete the cart items
            Cart.CartElementDelete();
        }
    }
}
