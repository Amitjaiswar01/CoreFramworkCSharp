using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T125_VerifyExpeditedShippingDropdownIsNotAvailable
{
    public class T125_Windows_VerifyExpeditedShippingDropdownIsNotAvailable : T125_DesktopBase
    {
        public T125_Windows_VerifyExpeditedShippingDropdownIsNotAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void ExpeditedShippingIsNotAvailable(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T125_Mac_VerifyExpeditedShippingDropdownIsNotAvailable : T125_DesktopBase
    {
        public T125_Mac_VerifyExpeditedShippingDropdownIsNotAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void ExpeditedShippingIsNotAvailable(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T125_iPad_VerifyExpeditedShippingDropdownIsNotAvailable : T125_DesktopBase
    {
        public T125_iPad_VerifyExpeditedShippingDropdownIsNotAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void ExpeditedShippingIsNotAvailable(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T125_TabletEmulator_VerifyExpeditedShippingDropdownIsNotAvailable : T125_DesktopBase
    {
        public T125_TabletEmulator_VerifyExpeditedShippingDropdownIsNotAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void ExpeditedShippingIsNotAvailable(string config) => Validate(config);
    }


    /// <summary>
    /// Verify when the Expedited Processing should NOT display in the Shipping dropdown.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9931
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T125
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9931"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T125")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public abstract class T125_DesktopBase : TestsBaseDesktop
    {
        protected T125_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Get the SKU From Query
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetItemWithExpeditedShippingMoreThan3Days;
            Assert.DatabaseObject(shortSku, "ProductActions.GetItemWithExpeditedShippingMoreThan3Days");

            /* Act:
            Add the item to cart and apply the zipcode
            Expand the shipping option
            */
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            Assert.True(Cart.IsCurrentPage, "User is not on Cart Overview page.");

            var isExpeditedAvailable = ShoppingCartWorkflow.IsShippingTypeAvailable(CountryCodeList.US, new Address().ZipCode, ShippingTypes.Expedited);

            //Assert: Expedited processing does not show
            Assert.True(!isExpeditedAvailable, "Expedited shipping is displayed.");
        }
    }
}
