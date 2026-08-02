using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T122_VerifyRemoveLinkModifyTotalPrice
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T122_Windows_VerifyRemoveLinkModifiesTotalPrice : T122_DesktopBase
    {
        public T122_Windows_VerifyRemoveLinkModifiesTotalPrice(ITestOutputHelper output) : base(output) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void RemoveLinkModifiesTotalPrice(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking the 'Remove' link in the edit popup removes modified price.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9928
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T122
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9928"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T122")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]

    public abstract class T122_DesktopBase : TestsBaseDesktop
    {
        protected T122_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            Ensure to empty the cart
            Add an item to the cart page
            Note down the original value of subsku
            Click on Edit price link
            Enter the discount value on the discount field
            Apply the discount
            */
            var setup = new TestSetup(config, useEmployeeManagerAccount: true) { AccountConfig = {UseEmployeeManagerAccount = true}};
            InitializeFunctionalTest(config, setup: setup);

            var shortSku = ProductActions.GetRandomComboKitSku;
            Assert.DatabaseObject(shortSku, "ProductActions.GetRandomComboKitSku");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel {Sku = shortSku});

            var subSkuPriceWithoutDiscount = Cart.GetProductTotalPriceWithoutDiscount(1);
            Cart.OpenDiscountTooltip();
            Cart.EnterDiscountValue(5);
            Cart.GetSaleDiscountValue();

            Cart.SelectAddDiscountButton();

            /*Act:
            Click on Edit price link
            Remove the discount
            */
            Cart.OpenDiscountTooltip();
            Cart.RemoveAppliedDiscount();

            //Assert: Discount is removed and original price is displaying
            Assert.False(Cart.IsAdditionalDiscountDisplayed(), "Additional discount is displayed in order summary block");
            Assert.Equals(subSkuPriceWithoutDiscount, Cart.GetProductTotalPriceWithoutDiscount(1), "Item Price after applying and removing discount is not equal.");
            
        }
    }
}