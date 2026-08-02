using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T111_T395_VerifyTotalChangeAccordingToShip
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T111_Windows_VerifyOrderTotalChangeAccordingToShip : T111_DesktopBase
    {
        public T111_Windows_VerifyOrderTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T111_Mac_VerifyOrderTotalChangeAccordingToShip : T111_DesktopBase
    {
        public T111_Mac_VerifyOrderTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T111_iPad_VerifyOrderTotalChangeAccordingToShip : T111_DesktopBase
    {
        public T111_iPad_VerifyOrderTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T111_TabletEmulator_VerifyOrderTotalChangeAccordingToShip : T111_DesktopBase
    {
        public T111_TabletEmulator_VerifyOrderTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the order total changes according to the shipping option selected.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9911
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T111
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9911"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T111")]
    public abstract class T111_DesktopBase : TestsBaseDesktop
    {
        protected T111_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: User has added an item to the cart.
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