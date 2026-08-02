using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T101_VerifyDiscountPriceFieldAndPercentDiscountFieldAreDisabled
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T101_Windows_VerifyDiscountPriceAndPercentDisabledForSis : T101_DesktopBase
    {
        public T101_Windows_VerifyDiscountPriceAndPercentDisabledForSis(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void DiscountPriceAndPercentDisabledForSis(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that discount price field and % discount field are disabled for SIS.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9915
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T101
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9915"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T101")]
    public abstract class T101_DesktopBase : TestsBaseDesktop
    {
        protected T101_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange: Enter in Kiosk session mode
            Login as Employee
            Clear Cart and then Add item to cart
            */
            InitializeFunctionalTest(config);

            CookieUtility.EnterStoreInSessionMode();

            SignIn.SignInFromHeader(LampsPlusAccounts.CustomerServiceRegularLoginAccount);

            ShoppingCartWorkflow.EmptyCart();

            var shortSku = ProductActions.GetListableInStockShortSku();

            Assert.DatabaseObject(shortSku, "ProductActions.GetListableInStockShortSku()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            /*Act: Click Pos Checkbox for item
            Click Edit Price Link for item
            */
            Cart.CheckPosBox();
            Cart.OpenEditPriceModal();

            //Assert: The Discount Price field and % Discount field in the 'edit price' modal are disabled.
            Assert.Equals("true", Cart.GetDiscountPriceField().GetAttribute("disabled"), "Discount price field in the 'edit price' modal are not disabled.");
            Assert.Equals("true", Cart.GetPercentDiscountPriceField().GetAttribute("disabled"), "% Discount field in the 'edit price' modal are not disabled.");
        }
    }
}