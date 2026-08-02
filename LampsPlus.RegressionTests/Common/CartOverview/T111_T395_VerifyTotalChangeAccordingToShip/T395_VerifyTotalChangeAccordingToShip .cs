using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T111_T395_VerifyTotalChangeAccordingToShip
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T395_iPhone_VerifyTotalChangeAccordingToShip : T395_MobileBase
    {
        public T395_iPhone_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T395_Emulator_VerifyTotalChangeAccordingToShip : T395_MobileBase
    {
        public T395_Emulator_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that the user can add a valid promo code to the cart.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9911
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T395
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9911"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T395")]
    public abstract class T395_MobileBase : TestsBaseMobile
    {
        protected T395_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : User add the item to the cart.
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetFinialSkuWithMultipleShippingOptions();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            // Assert: In the Order Summary section, Shipping & Processing is displayed $5 and the Order Total is Product Total plus $5.
            var shippingPrice = 5;
            Assert.Equals(shippingPrice, Cart.GetShippingChargeCost(), "Shipping & Processing total is not $5.00");
            var calculatedOrderTotal = Cart.GetProductTotal() + Cart.GetShippingChargeCost();
            Assert.Equals(Cart.GetOrderTotalCost(), calculatedOrderTotal, "Order Total is not equal to the Product Total + Shipping & Processing.");
        }
    }
}