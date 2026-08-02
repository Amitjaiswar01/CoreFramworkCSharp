using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Shipping
{
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7976_Windows_VerifyTaxLabelChangesFromEstimatedTaxToTax : T7976_DesktopBase
    {
        public T7976_Windows_VerifyTaxLabelChangesFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7976_Mac_VerifyTaxLabelChangesFromEstimatedTaxToTax : T7976_DesktopBase
    {
        public T7976_Mac_VerifyTaxLabelChangesFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7976_Ipad_VerifyTaxLabelChangesFromEstimatedTaxToTax : T7976_DesktopBase
    {
        public T7976_Ipad_VerifyTaxLabelChangesFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7976_TabletEmulator_VerifyTaxLabelChangesFromEstimatedTaxToTax : T7976_DesktopBase
    {
        public T7976_TabletEmulator_VerifyTaxLabelChangesFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Tax Label Changes from "Estimated Tax" to "Tax"
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10784
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7976
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10784"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7976")]
    public abstract class T7976_DesktopBase : TestsBaseDesktop
    {
        protected T7976_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange - Add an item to the Cart with price between $10 and $25.
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel {Sku = shortSku});
            Assert.True(Cart.IsCurrentPage, "Current page is not a Cart page");

            /*Act
             Click on the 'Standard Shipping' link.
             In Shipping Options modal, enter a US-based Zip Code and click the 'Update' button.
            */
            Cart.OpenShippingOptions();
            Cart.ApplyZipCode(ZipCodeList.Ardmore);
            Cart.ShippingUpdate();
            var actualTaxLabelOnCart = Cart.GetTaxLabel();

            //Assert : Verify the correct Tax label is displayed
            Assert.Equals(Cart.EstimatedTaxLabel, actualTaxLabelOnCart, "Tax label is not matching");

            /* Act: 
            Click the 'Check Out Now' button.
            Enter a full, valid US-based address.
            Observe the Tax Label under the Order Summary block.
            */
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a shipping page");
            var address = new Address {State = StateCodeListUnitedStates.CA};
            CustomerAddressInformation.EnterShippingAddress(address);
            Browser.TabKeyboard();
            Browser.RefreshPage();
            var actualTaxLabelOnShipping = Cart.GetTaxLabel().TrimStart();
            
            //Assert : Verify the correct Tax label is displayed
            Assert.Equals(Shipping.TaxLabel, actualTaxLabelOnShipping, "Tax label is not matching");
        }
    }
}