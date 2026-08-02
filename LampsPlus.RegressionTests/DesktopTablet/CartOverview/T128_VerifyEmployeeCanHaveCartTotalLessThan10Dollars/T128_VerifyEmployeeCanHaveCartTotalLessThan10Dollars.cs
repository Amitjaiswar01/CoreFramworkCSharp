using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T128_VerifyEmployeeCanHaveCartTotalLessThan10Dollars
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T128_Windows_VerifyEmpCanCheckoutWithLess10Dollars : T128_DesktopBase
    {
        public T128_Windows_VerifyEmpCanCheckoutWithLess10Dollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void AnEmployeeCanCheckoutWithLessThan10(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T128_Mac_VerifyEmpCanCheckoutWithLess10Dollars : T128_DesktopBase
    {
        public T128_Mac_VerifyEmpCanCheckoutWithLess10Dollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void AnEmployeeCanCheckoutWithLessThan10(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T128_iPad_VerifyEmpCanCheckoutWithLess10Dollars : T128_DesktopBase
    {
        public T128_iPad_VerifyEmpCanCheckoutWithLess10Dollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void AnEmployeeCanCheckoutWithLessThan10(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T128_TabletEmulator_VerifyEmpCanCheckoutWithLess10Dollars : T128_DesktopBase
    {
        public T128_TabletEmulator_VerifyEmpCanCheckoutWithLess10Dollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void AnEmployeeCanCheckoutWithLessThan10(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an ESI can proceed to the shipping page with a cart total of less than $10.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9932
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T128 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9932"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T128")]
    public abstract class T128_DesktopBase : TestsBaseDesktop
    {
        protected T128_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrangement
            User Login as ESI.
            Delete Cart.
            ESI has added product below $10 to cart.
            Remove Promo code from cart if any applied.
            */
            var setup = new TestSetup(config);
            InitializeFunctionalTest(config, setup: setup);

            var sku = ProductActions.GetLessThanTenDollarItem;
            Assert.DatabaseObject(sku, "ProductActions.GetLessThanTenDollarItem()");
            
            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = sku });

            Cart.RemovePromoCode();

            /* Act
            Choose any Sale Source from the drop-down in the CSR block.
            Click on the Check Out Now button.
            */
            CsrBlock.SetSaleSourceValue();
            
            ShoppingCartWorkflow.ProceedToShippingPage();

            // Assertion: ESI is able to proceed to the shipping page.
            Assert.True(Shipping.IsCurrentPage, "ESI User is not on Shipping page.");
        }
    }
}
