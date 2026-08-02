using Automation.Framework.Utilities;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCounty;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.ShoppingCart
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    public class T129VerifyNonEsiUserCannotSubmitOrderLessThanTenDollars : ShoppingCartTestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T129VerifyNonEsiUserCannotSubmitOrderLessThanTenDollars(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify that a non-ESI user cannot submit an order that is less than $10
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5209
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T129
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5209"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T129"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void Test(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetLessThanTenDollarItem;

            ConditionalVerify.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });
            Browser.Wait.ForDomReady();
            ShoppingCartWorkflow.EnterCartZipCodeForShipping(CountryCodeList.UnitedStates, ZipCodeList.NorthSmithfield, ShippingTypes.Standard);

            SoftVerify.True(ElementActions.HasClass(ShoppingCart.CheckOutNowButton, "disabled"), "Checkout Now button is enabled");
            SoftVerify.True(ElementActions.HasClass(ShoppingCart.PayPalButtonContainer, "disabled"), "PayPalButton button is enabled");

            Browser.MouseOverOnElement(ShoppingCart.CheckOutNowButton);
            SoftVerify.True(ElementActions.HasClass(ShoppingCart.CheckOutBtnValidationTooltip, "showUp"), Messages.PromoRelatedMessages.TooltipMsg);
          
            Browser.MouseOverOnElement(ShoppingCart.PayPalButton);
            SoftVerify.True(ElementActions.HasClass(ShoppingCart.PaypalValidationTooltip, "showUp"), Messages.PromoRelatedMessages.TooltipMsg);
        }
    }
}
