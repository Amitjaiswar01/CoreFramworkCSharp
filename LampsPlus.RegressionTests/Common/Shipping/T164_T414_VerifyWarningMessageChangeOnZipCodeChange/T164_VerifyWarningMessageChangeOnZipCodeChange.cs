using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T164_T414_VerifyWarningMessageChangeOnZipCodeChange
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T164_Windows_VerifyWarningMessageChangeOnZipCodeChange : T164_DesktopBase
    {
        public T164_Windows_VerifyWarningMessageChangeOnZipCodeChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WarningMessageChangeOnZipCodeChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T164_Mac_VerifyWarningMessageChangeOnZipCodeChange : T164_DesktopBase
    {
        public T164_Mac_VerifyWarningMessageChangeOnZipCodeChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void WarningMessageChangeOnZipCodeChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T164_iPad_VerifyWarningMessageChangeOnZipCodeChange : T164_DesktopBase
    {
        public T164_iPad_VerifyWarningMessageChangeOnZipCodeChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void WarningMessageChangeOnZipCodeChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T164_TabletEmulator_VerifyWarningMessageChangeOnZipCodeChange : T164_DesktopBase
    {
        public T164_TabletEmulator_VerifyWarningMessageChangeOnZipCodeChange(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void WarningMessageChangeOnZipCodeChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the warning message and freight charge change when ZIP code is changed to Zone 3
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10067
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T164 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10067"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T164")]
    public abstract class T164_DesktopBase : TestsBaseDesktop
    {
        protected T164_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Identify a valid Sku
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetProductShortSkuWithZone3Shipping;

            //Act : Add product to Cart
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            //Act : On Cart page, enter 91311 zipcode and Checkout
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.Chatsworth, 0);
            Cart.CheckOut();

            //Act : On Shipping page, enter Alaska zipcode
            CustomerAddressInformation.EnterShippingAddress(new Address { State = StateCodeListUnitedStates.AK, ZipCode = ZipCodeList.Alaska });
            Shipping.WaitForShippingMethodsChangedContainer();

            //Act : Notedown the freight charges of Sku added to cart
            var dbShippingCost = ProductActions.GetProductFreightChargeWithZone3(shortSku);
            var shippingCostOnSite = Cart.GetShippingCost().ToString();
            var shippingCostOnModifyShippingBlock = TextActions.GetPriceTextOnly(Shipping.GetShippingCostFromModifyShippingBlock());

            //Assert :  Verify warning message displays that warns as shipping methods have changed
            Assert.StringContains(Shipping.GetShippingOptionsChangedContainer().Text, Messages.ShippingMessage.ShippingMessageChangedMessage, "Warning message that Shipping cost may have updated is not displayed");

            //Assert : Verify freight charges on site and in database
            Assert.Equals(TextActions.FormatToTwoDecimals(dbShippingCost.FreightCharge), shippingCostOnSite, "Freight charge does not match the correct amount.");

            //Assert : Verify Shipping & Processing value in the 'Order Summary' block and in the 'Optional - Modify Shipping Method'
            Assert.Equals(shippingCostOnModifyShippingBlock, shippingCostOnSite, "Freight charge does not match the correct amount.");
        }
    }
}