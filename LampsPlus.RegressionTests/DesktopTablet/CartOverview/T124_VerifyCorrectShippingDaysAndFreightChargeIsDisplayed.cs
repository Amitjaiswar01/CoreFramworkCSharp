using System;
using System.Linq;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview
{
    public class T124_Windows_VerifyCorrectShipAndFreightCharge : T124_DesktopBase
    {
        public T124_Windows_VerifyCorrectShipAndFreightCharge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void CorrectShipAndFreightCharge(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_Mac_VerifyCorrectShipAndFreightCharge : T124_DesktopBase
    {
        public T124_Mac_VerifyCorrectShipAndFreightCharge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "Bug - LP-53711")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void CorrectShipAndFreightCharge(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_iPad_VerifyCorrectShipAndFreightCharge : T124_DesktopBase
    {
        public T124_iPad_VerifyCorrectShipAndFreightCharge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "Bug - LP-53711")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void CorrectShipAndFreightCharge(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T124_TabletEmulator_VerifyCorrectShipAndFreightCharge : T124_DesktopBase
    {
        public T124_TabletEmulator_VerifyCorrectShipAndFreightCharge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "Bug - LP-53711")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void CorrectShipAndFreightCharge(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the correct shipping days and freight charge are displayed.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5376
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T124
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5376"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T124")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
	public abstract class T124_DesktopBase : ShoppingCartTestsBase
    {
        protected T124_DesktopBase(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            var setup = new TestSetup(config);
            InitializeFramework(config, setup: setup);

            var selectedProductDetail = ProductActions.GetDeliveryDays();

            Assert.DatabaseObject(selectedProductDetail, "ProductActions.GetDeliveryDays()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = selectedProductDetail.ShortSku });

            Browser.Wait.ForPageWait(Urls.CartOverviewPageUrl);

            ShoppingCartWorkflow.OpenShippingOptions(CountryCodeList.US, new Address().ZipCode);
            
            Browser.Wait.ForElementToStopAnimating(CartOverview.AvailableShippingOptionsContainer);

            var shippingOptions = ShoppingCartWorkflow.GetAvailableShippingOptions();
            var delvDaysValue = selectedProductDetail.FirstShipDays;
            var lastDelvDaysValue = selectedProductDetail.LastShipDays;

            var today = DateTime.Now.Date;
                   
            Assert.Equals(ShoppingCartWorkflow. AddBusinessDaysForStandardShipping(today, delvDaysValue),
               shippingOptions.First(p => p.ShippingType.Equals(ShippingTypes.Standard)).ArrivesDate, "Standard first Day shipping arrival date  does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDaysForStandardShipping(today, lastDelvDaysValue),
               shippingOptions.First(p => p.ShippingType.Equals(ShippingTypes.Standard)).LastArrivalDate, "Standard first Day shipping arrival date  does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(today, 3),
                shippingOptions.First(p => p.ShippingType.Equals(ShippingTypes.ThirdDay)).ArrivesDate, "Third Day shipping arrival date does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(today, 1),
                shippingOptions.First(p => p.ShippingType.Equals(ShippingTypes.NextDay)).ArrivesDate, "Next Day shipping arrival date  does not match.");

            Assert.Equals(ShoppingCartWorkflow.AddBusinessDays(today, 2),
                shippingOptions.First(p => p.ShippingType.Equals(ShippingTypes.SecondDay)).ArrivesDate, "Second Day shipping arrival date does not match.");
        }
    }
}
