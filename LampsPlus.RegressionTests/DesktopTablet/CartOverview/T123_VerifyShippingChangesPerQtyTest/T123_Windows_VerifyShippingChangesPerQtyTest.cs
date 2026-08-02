using System;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T123_VerifyShippingChangesPerQtyTest
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T123_Windows_VerifyShippingChangesPerQtyTest : T123_DesktopBase
    {
        public T123_Windows_VerifyShippingChangesPerQtyTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyShippingCostChangesPerQty(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T123_Mac_VerifyShippingChangesPerQtyTest : T123_DesktopBase
    {
        public T123_Mac_VerifyShippingChangesPerQtyTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyShippingCostChangesPerQty(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T123_iPad_VerifyShippingChangesPerQtyTest : T123_DesktopBase
    {
        public T123_iPad_VerifyShippingChangesPerQtyTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyShippingCostChangesPerQty(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T123_TabletEmulator_VerifyShippingChangesPerQtyTest : T123_DesktopBase
    {
        public T123_TabletEmulator_VerifyShippingChangesPerQtyTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyShippingCostChangesPerQty(string config) => Validate(config);
    }


    /// <summary>
    /// Verify line items and totals are adjusted correctly with changes in quantity.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9929
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T123
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9929"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T123")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T123_DesktopBase : TestsBaseDesktop
    {
        protected T123_DesktopBase(ITestOutputHelper output) : base(output) { }

        const string customShippingPrice = "10";

        protected virtual void Validate(string config)
        {
            // Arrange : Get combokitsku and add it to cart
            InitializeFunctionalTest(config);
            var comboSku = ProductActions.GetRandomComboKitSku;
            Assert.DatabaseObject(comboSku, "ProductActions.GetRandomComboKitSku");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(comboSku));
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Set the sale source and reason code for discount 
            CsrBlock.SetSaleSourceValue();
            CsrBlock.SetReasonCodeValue();

            // Act : Update the shipping destination for product for US zip code
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.Chatsworth, 0);
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Apply the customized shipping price and get the total shipping price 
            CsrBlock.ApplyShippingAndProcessingCost(customShippingPrice);
            
            var totalShipCostBeforeQtyChange = Cart.GetShippingTotal().ToString().Replace(".00", "");
            var correctedTotalShipCostBeforeQtyChange = Convert.ToInt32(totalShipCostBeforeQtyChange);

            // Assert : Customized shipping price and total shipping price matches
            Assert.Equals(customShippingPrice, totalShipCostBeforeQtyChange, "Shipping Price is not equal to $10 for one or both elements");

            // Act : Update the product quantity to two
            var productQuantity = "2";
            var correctedQty = Convert.ToInt32(productQuantity);
            Cart.ChangeItemQuantity(productQuantity);

            // Act : Get the updated total shipping cost and product shipping cost 
            var shipCostAfterQtyChange = Cart.GetShippingTotal().ToString().Replace(".00", "");
            var correctedShipCostAfterQtyChange = Convert.ToInt32(shipCostAfterQtyChange);
            var shippingPriceForProduct = Cart.GetShippingTotal().ToString().Replace(".00", "");

            var finalShipCostCalculated = (correctedTotalShipCostBeforeQtyChange * correctedQty);

            // Assert : Updated total shipping cost and product shipping cost matches 
            Assert.True(customShippingPrice != shippingPriceForProduct && customShippingPrice != shipCostAfterQtyChange, "Shipping Price with Qty 1 and Qty 2 are the same.");
            Assert.Equals(finalShipCostCalculated, correctedShipCostAfterQtyChange, "Shipping Price is not equal to $10 for one or both elements");
            Assert.Equals(shippingPriceForProduct, shipCostAfterQtyChange, "Shipping Prices are not in sync.");
        }
    }
}