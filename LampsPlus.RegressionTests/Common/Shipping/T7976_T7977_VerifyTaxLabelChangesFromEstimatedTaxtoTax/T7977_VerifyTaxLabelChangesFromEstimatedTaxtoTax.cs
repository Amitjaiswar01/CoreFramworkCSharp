using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping
{
    //[Collection(LpTraits.BatchGroup.Mobile.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T7977_iPhone_VerifyTaxLabelChangeFromEstimatedTaxToTax : T7977_MobileBase
    {
        public T7977_iPhone_VerifyTaxLabelChangeFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7977_Emulator_VerifyTaxLabelChangeFromEstimatedTaxToTax : T7977_MobileBase
    {
        public T7977_Emulator_VerifyTaxLabelChangeFromEstimatedTaxToTax(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyTaxLabelChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Tax Label Changes from "Estimated Tax" to "Tax"
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10784
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7977
    /// </summary>
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10784"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7977")]
    public abstract class T7977_MobileBase : TestsBaseMobile
    {
        protected T7977_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange - Add an item to the Cart between $10 and $25.
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            Assert.True(Cart.IsCurrentPage, "Current page is not a Cart page");

            /* Act 
            Click on the 'Standard Shipping' link.
            In Shipping Options modal, enter a US-based Zip Code and click the 'Update' button.
            */
            Cart.OpenShippingOptions();
            Cart.ApplyZipCode(ZipCodeList.Chatsworth);
            Cart.ShippingUpdate();
            var actualTaxLabelOnCart = Cart.GetTaxLabel();

            //Assert : Verify the correct Tax label is displayed
            Assert.Equals(Cart.EstimatedTaxLabel.Replace(":",""), actualTaxLabelOnCart, "Tax label is not matching");

            /* Act
            Click the 'Check Out Now' button.
            Enter a full, valid US-based address.
            Observe the Tax Label under the Order Summary block.
            */
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a shipping page");
            var address = new Address { State = StateCodeListUnitedStates.CA};
            CustomerAddressInformation.EnterShippingAddress(address);
            Shipping.SelectRequiredNoteText();
            Payment.OpenOrderSummaryDropdown();
            ShoppingCartWorkflow.WaitForTaxLabelToUpdate();
            var actualTaxLabelOnShipping = Cart.GetTaxLabel();

            //Assert : Verify the correct Tax label is displayed
            Assert.Equals(Shipping.TaxLabel.Replace(":", ""), actualTaxLabelOnShipping, "Tax label is not matching");
        }
    }
}
