using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T115_VerifyAnEmployeeCanApplyDiscount
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T115_Windows_VerifyEmployeeCanApplyDiscount : T115_DesktopBase
    {
        public T115_Windows_VerifyEmployeeCanApplyDiscount (ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void EmpApplyDiscLessThanPctAllow(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T115_Mac_VerifyEmployeeCanApplyDiscount : T115_DesktopBase
    {
        public T115_Mac_VerifyEmployeeCanApplyDiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void EmpApplyDiscLessThanPctAllow(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T115_iPad_VerifyEmployeeCanApplyDiscount : T115_DesktopBase
    {
        public T115_iPad_VerifyEmployeeCanApplyDiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void EmpApplyDiscLessThanPctAllow(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T115_TabletEmulator_VerifyEmployeeCanApplyDiscount : T115_DesktopBase
    {
        public T115_TabletEmulator_VerifyEmployeeCanApplyDiscount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void EmpApplyDiscLessThanPctAllow(string config) => Validate(config);
    }


    /// <summary>
    /// Verify an employee can apply a discount that is less than the percentage allowed.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9921
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T115
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9921"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T115")]
    public abstract class T115_DesktopBase : TestsBaseDesktop
    {
        protected T115_DesktopBase(ITestOutputHelper output) : base(output) { }

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
            Click on Edit price link
            Enter the discount value on the discount field
            Apply the discount
            */
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