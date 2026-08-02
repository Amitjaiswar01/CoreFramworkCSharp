using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T104_VerifyPosColumnIsVisibleWithTotalsForEachProduct
{
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T104_Windows_VerifyPosColumnVisibleWithProductTotals : T104_DesktopBase
    {
        public T104_Windows_VerifyPosColumnVisibleWithProductTotals(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void VerifyPosColumnVisibleWithProductTotals(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T104_Mac_VerifyPosColumnVisibleWithProductTotals : T104_DesktopBase
    {
        public T104_Mac_VerifyPosColumnVisibleWithProductTotals(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void VerifyPosColumnVisibleWithProductTotals(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T104_iPad_VerifyPosColumnVisibleWithProductTotals : T104_DesktopBase
    {
        public T104_iPad_VerifyPosColumnVisibleWithProductTotals(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void VerifyPosColumnVisibleWithProductTotals(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T104_TabletEmulator_VerifyPosColumnVisibleWithProductTotals : T104_DesktopBase
    {
        public T104_TabletEmulator_VerifyPosColumnVisibleWithProductTotals(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SIS_ESI)]
        public void VerifyPosColumnVisibleWithProductTotals(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Pos Column Is Visible With Totals For Each Product.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9916
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T104
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9916"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T104")]
    public abstract class T104_DesktopBase : TestsBaseDesktop
    {
        protected T104_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Clear the Cart
            var setup = new TestSetup(config) { AccountConfig = { StoreInSessionStoreNumber = "12" } }; 
            InitializeFunctionalTest(config, setup: setup);

            // Act: Add Two Skus to Cart
            const int numberOfProductsToAddToCart = 2;
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.TableLampsSortPageUrl, numberOfProductsToAddToCart);
            
            // Assert : Check the POS Link
            Assert.True(Cart.IsCurrentPage, "User is not on Cart Page");
            var productCountInCart = Cart.GetCountOfAllProductsInCart();
            Assert.Equals(Cart.GetCountOfAllProductsInCart(), Cart.GetAllCartProductsPosLinkCount(), "Not all items on cart page have a POS checkbox.");

            // Act: Click on POS CheckBox
            Cart.CheckPosBoxForAllCartSkus(productCountInCart);

            // Assert : Check Order Summary section include POS Column
            Assert.StringContains(  Cart.GetOrderSummaryBlockText(), "POS", "Order Summary section doesn't include POS Column.");
            Assert.True(Cart.IsCheckOutNowButtonDisabled, "Checkout Now button is enabled");
        }
    }
}
