using System;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCounty;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.ShippingInfo
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    public class T164VerifyShippingMethodsHaveChanged : ShippingInfoTestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T164VerifyShippingMethodsHaveChanged(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the warning message and freight charge change when ZIP code is changed to Zone 3.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5233
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T164 
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5233"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T164"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void Test(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
 
            // fetch data from database
            var shortSku = ProductActions.GetProductShortSkuWithZone3Shipping;

            ConditionalVerify.DatabaseObject(shortSku, "ProductActions.GetProductShortSkuWithZone3Shipping()");

            // add item to cart via API
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            // enter shipping info for cart item
            ShoppingCartWorkflow.EnterCartZipCodeForShipping(CountryCodeList.UnitedStates, ZipCodeList.Chatsworth);

            Browser.Navigate(Urls.ShippingInfoPageUrl);

            // prepare inputs to Shipping Address form
            var shippingAddress = new Address // TODO: Objects should be built with helpers in page objects.
            {
                ZipCode = "99901",
                State = StateCodeListUnitedStates.Alaska
            };

            // fill out shipping address form
            ShoppingCartWorkflow.EnterShippingAddress(shippingAddress);

            // get data from database and UI then verify them
            var dbShippingCost = ProductActions.GetProductFreightChargeWithZone3(shortSku);

            ConditionalVerify.DatabaseObject(dbShippingCost, "ProductActions.GetProductFreightChargeWithZone3(shortSku)");

            Browser.MouseOverOnElement(CustomerInformation.ProceedToPaymentButton);

            var uiShippingCost = Convert.ToDecimal(ShoppingCart.GetShippingCellShippingCost());
            var uiOrderSummaryShippingCost = Convert.ToDecimal(CustomerInformation.GetOrderSummaryShippingCost());

            SoftVerify.Displayed(CustomerInformation.ShippingOptionsChangedMessage, "Shipping options change message is not displayed.");
            SoftVerify.Equals(dbShippingCost.FreightCharge, uiShippingCost, "Freight charge does not match the correct amount.");
            SoftVerify.Equals(uiShippingCost, uiOrderSummaryShippingCost, "Shipping cost does not match the correct amount.");
        }
    }
}
