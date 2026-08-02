using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T114_VerifyProductPriceIsCalculatedCorrectlyWhenEnterADiscount
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T114_Windows_VerifyProductPriceCalculationWhenEnteringADiscount : T114_DesktopBase
    {
        public T114_Windows_VerifyProductPriceCalculationWhenEnteringADiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void ProductPriceCalculationWhenEnteringADiscount(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T114_Mac_VerifyProductPriceCalculationWhenEnteringADiscount : T114_DesktopBase
    {
        public T114_Mac_VerifyProductPriceCalculationWhenEnteringADiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void ProductPriceCalculationWhenEnteringADiscount(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T114_iPad_VerifyProductPriceCalculationWhenEnteringADiscount : T114_DesktopBase
    {
        public T114_iPad_VerifyProductPriceCalculationWhenEnteringADiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void ProductPriceCalculationWhenEnteringADiscount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T114_TabletEmulator_VerifyProductPriceCalculationWhenEnteringADiscount : T114_DesktopBase
    {
        public T114_TabletEmulator_VerifyProductPriceCalculationWhenEnteringADiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void ProductPriceCalculationWhenEnteringADiscount(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the price is calculated correctly when entering a discount.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9920
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T114
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9920"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T114")]

    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public abstract class T114_DesktopBase : TestsBaseDesktop
    {
        protected T114_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            Ensure to empty the cart
            Add an item to the cart page
            */
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            /*Act:
            On CSR block, Open the Sale Source dropdown and choose any value
            Click on Edit price link
            Enter the discount value on the discount field
            Apply the discount
            */
            CsrBlock.SetSaleSourceValue();
            Cart.OpenDiscountTooltip();
            Cart.EnterDiscountValue(5);

            var updatedDiscount = Cart.GetSaleDiscountValue();

            Cart.SelectAddDiscountButton(); 

            /*Assert:
            Verify Additional Discount is visible under the summary block
            Verify the subtotal and line item value matches
            */
            Assert.Displayed(Cart.GetAdditionalDiscount(), "Additional discount not displayed");
            Assert.Equals(updatedDiscount, Cart.GetSubTotal().ToString(), $"{RecurringDataIssue}Subtotal value do not match.");
        }
    }
}
