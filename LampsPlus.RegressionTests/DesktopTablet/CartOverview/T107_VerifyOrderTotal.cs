using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCounty;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.ShoppingCart
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    public class T107VerifyOrderTotal : ShoppingCartTestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T107VerifyOrderTotal(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify that the order total is correct.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5129
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T107  
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5129"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T107")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows10_Chrome_SNIS_UNSI)]
        public void Test(string config)
        {
            InitializeFramework(config);

            var zipCode = ZipCodeList.Phoenix;
            var shortSku = ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp);

            ConditionalVerify.DatabaseObject(shortSku, "ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp)");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            GlobalLocators.AddToCartButton.Click();

            Browser.Wait.ForClickableElement(ShoppingCart.ChangeShippingOptionsLink).Click();
            ShoppingCart.ShippingZipField.SendKeys(zipCode);
            ShoppingCart.ClickShippingOptionShipTabSearchButton();
            ShoppingCart.ClickShippingOptionShipTabUpdateButton();

            Verify.Equals(ShoppingCart.CalculateOrderTotalWithoutDiscount(), ShoppingCart.GetOrderTotalCost(), "Order total is not correct.");
        }
    }
}
