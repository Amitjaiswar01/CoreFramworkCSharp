using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Constants.StreetCityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T186_T433_VerifyShippingCostIsCorrect
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T186_Windows_VerifyShippingCostIsCorrect : T186_DesktopBase
    {
        public T186_Windows_VerifyShippingCostIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ShippingCostIsCorrect(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T186_Mac_VerifyShippingCostIsCorrect : T186_DesktopBase
    {
        public T186_Mac_VerifyShippingCostIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ShippingCostIsCorrect(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T186_iPad_VerifyShippingCostIsCorrect : T186_DesktopBase
    {
        public T186_iPad_VerifyShippingCostIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ShippingCostIsCorrect(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T186_TabletEmulator_VerifyShippingCostIsCorrect : T186_DesktopBase
    {
        public T186_TabletEmulator_VerifyShippingCostIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ShippingCostIsCorrect(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the shipping cost is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10001
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T186
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10001"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T186")]
    public abstract class T186_DesktopBase : TestsBaseDesktop
    {
        protected T186_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add an item to the cart.
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetAnySkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuThatMeetsMinimumOrder()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            /*Act:
            Enter in the Zip code '99501' and click the Apply button.
            Take note of the shipping cost in the cart.
             */
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.Anchorage, 0);
            var cartShippingCost = Cart.GetShippingCost();

            /*Act
            Proceed to the Shipping page and enter in an address.
            Proceed to the Payment page.
             */
            Browser.ScrollToTopOfWindow();
            Cart.CheckOut();
            CustomerAddressInformation.EnterShippingAddress(new Address { AddressLine1 = StreetList.AnchorageAddress, City = CityList.Anchorage, State = StateCodeListUnitedStates.AK, ZipCode = ZipCodeList.Anchorage });

            ShoppingCartWorkflow.ProceedToPayment();
            var paymentShippingCost = Cart.GetShippingCost();

            //Assert: The shipping cost is same as the shopping cost from the cart.
            Assert.Equals(cartShippingCost, paymentShippingCost, $"Shipping cost from cart ({cartShippingCost}) and payment page ({paymentShippingCost}) do not match.");
        }
    }
}
