using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Shipping.T7552_T7554_VerifyLayoutFedExModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7552_Windows_VerifyLayoutFedExModal : T7552_DesktopBase
    {
        public T7552_Windows_VerifyLayoutFedExModal(ITestOutputHelper output, T7552_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7552_Mac_VerifyLayoutFedExModal : T7552_DesktopBase
    {
        public T7552_Mac_VerifyLayoutFedExModal(ITestOutputHelper output, T7552_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7552_iPad_VerifyLayoutFedExModal : T7552_DesktopBase
    {
        public T7552_iPad_VerifyLayoutFedExModal(ITestOutputHelper output, T7552_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7552_TabletEmulator_VerifyLayoutFedExModal : T7552_DesktopBase
    {
        public T7552_TabletEmulator_VerifyLayoutFedExModal(ITestOutputHelper output, T7552_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    public class T7552_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7552_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the FedEx Verification modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8631
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7552
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8631"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7552")]
    public abstract class T7552_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7552_SharedSku_Fixture>
    {
        protected readonly T7552_SharedSku_Fixture Fixture;

        protected T7552_DesktopBase(ITestOutputHelper output, T7552_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /* Arrange:
            1. Add any item to cart.
            2. Proceed to the Shipping page. 
            */
            InitializeVisualTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Open FexEx modal
            ShoppingCartWorkflow.ShowFedExValidationModal(enterApartment: false);

            //Act: Capture a screenshot of the FedEx Validation modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, CustomerAddressInformation.GetFedExAddressValidationModal());
        }
    }
}