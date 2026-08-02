using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Shipping.T7553_T7555_VerifyLayoutFedExApartmentModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7553_Windows_VerifyLayoutFedExApartmentModal : T7553_DesktopBase
    {
        public T7553_Windows_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7553_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7553_Mac_VerifyLayoutFedExApartmentModal : T7553_DesktopBase
    {
        public T7553_Mac_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7553_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7553_iPad_VerifyLayoutFedExApartmentModal : T7553_DesktopBase
    {
        public T7553_iPad_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7553_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7553_TabletEmulator_VerifyLayoutFedExApartmentModal : T7553_DesktopBase
    {
        public T7553_TabletEmulator_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7553_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    public class T7553_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7553_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the FedEx Apartment Verification modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8631
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7553
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8631"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7553")]
    public abstract class T7553_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7553_SharedSku_Fixture>
    {
        protected readonly T7553_SharedSku_Fixture Fixture;

        protected T7553_DesktopBase(ITestOutputHelper output, T7553_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU and added it to the cart.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(sku));
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Arrange: custom address
            var addressCustom = new Address
            {
                FirstName = "Test",
                LastName = "Test",
                AddressLine1 = "607 East Providencia Ave",
                City = "Burbank",
                ZipCode = "91501"
            };

            //Act: Enter Shipping page fields and FedEx modal is triggered
            ShoppingCartWorkflow.ShowFedExValidationModal( enterApartment: false, address: addressCustom);

            //Act: Capture a screenshot of the FedEx Validation modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}
