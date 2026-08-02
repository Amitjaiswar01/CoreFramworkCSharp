using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T124_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_Windows_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed : T124_DesktopBase
    {
        public T124_Windows_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void CorrectShippingDaysAndFreightChargeIsDisplayed(string config) => Validate(config);
    }

    
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_Mac_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed : T124_DesktopBase
    {
        public T124_Mac_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void CorrectShippingDaysAndFreightChargeIsDisplayed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_iPad_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed : T124_DesktopBase
    {
        public T124_iPad_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void CorrectShippingDaysAndFreightChargeIsDisplayed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_TabletEmulator_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed : T124_DesktopBase
    {
        public T124_TabletEmulator_VerifyCorrectShippingDaysAndFreightChargesIsDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void CorrectShippingDaysAndFreightChargeIsDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the correct shipping days and freight charge are displayed.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9930
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T124
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9930"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T124")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.Employee)]
    public abstract class T124_DesktopBase : TestsBaseDesktop
    {
        protected T124_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add an product to the cart
            InitializeFunctionalTest(config);

            var selectedProductDetail = ProductActions.GetDeliveryDays();
            Assert.DatabaseObject(selectedProductDetail, "ProductActions.GetDeliveryDays()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = ProductActions.GetDeliveryDays().ShortSku });

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act: Click on Shipping Option modal & apply the US zip code
            ShoppingCartWorkflow.OpenShippingOptions(CountryCodeList.US, ZipCodeList.Chatsworth);
            
            //Assert: Verify the shipping arrival days
            Assert.Equals(ShoppingCartWorkflow.AddBusinessDaysForStandardShipping(DateTime.Now.Date, selectedProductDetail.FirstShipDays),
                Cart.GetAvailableShippingOptions().First(p => p.ShippingType.Equals(ShippingTypes.Standard)).ArrivesDate, "Standard first Day shipping arrival date does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDaysForStandardShipping(DateTime.Now.Date, selectedProductDetail.LastShipDays),
                Cart.GetAvailableShippingOptions().First(p => p.ShippingType.Equals(ShippingTypes.Standard)).LastArrivalDate, "Standard first Day shipping arrival date does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(DateTime.Now.Date, 3),
                Cart.GetAvailableShippingOptions().First(p => p.ShippingType.Equals(ShippingTypes.ThirdDay)).ArrivesDate, "Third Day shipping arrival date does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(DateTime.Now.Date, 1),
                Cart.GetAvailableShippingOptions().First(p => p.ShippingType.Equals(ShippingTypes.NextDay)).ArrivesDate, "Next Day shipping arrival date does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(DateTime.Now.Date, 2),
                Cart.GetAvailableShippingOptions().First(p => p.ShippingType.Equals(ShippingTypes.SecondDay)).ArrivesDate, "Second Day shipping arrival date does not match.");
        }
    }
}