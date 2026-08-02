using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7342_VerifyLayoutOfMinimumPricingPolicyModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7342_Windows_VerifyLayoutOfMinimumPricingPolicyModal : T7342_DesktopBase
    {
        public T7342_Windows_VerifyLayoutOfMinimumPricingPolicyModal(ITestOutputHelper output, T7342_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfMinimumPricingPolicyModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7342_Mac_VerifyLayoutOfMinimumPricingPolicyModal : T7342_DesktopBase
    {
        public T7342_Mac_VerifyLayoutOfMinimumPricingPolicyModal(ITestOutputHelper output, T7342_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfMinimumPricingPolicyModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7342_TabletEmulator_VerifyLayoutOfMinimumPricingPolicyModal : T7342_DesktopBase
    {
        public T7342_TabletEmulator_VerifyLayoutOfMinimumPricingPolicyModal(ITestOutputHelper output, T7342_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfMinimumPricingPolicyModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7342_iPad_VerifyLayoutOfMinimumPricingPolicyModal : T7342_DesktopBase
    {
        public T7342_iPad_VerifyLayoutOfMinimumPricingPolicyModal(ITestOutputHelper output, T7342_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfMinimumPricingPolicyModal(string config) => Validate(Validate, config);

        protected override void Validate(string config)
        {
            // Arrange: User has identified sku with UMRP
            InitializeVisualTest(config, useEmployeeManagerAccount: true);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetShortSkuWithUmrp()");
            ShoppingCartWorkflow.EmptyCart();

            // Act: User has navigated to PDP and added product to cart 
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));

            // Act: User has applied manual discount of 10 % with reason 10%
            Cart.ApplyDiscountForProductOnIpad(Fixture.DiscountPercentage, "Sale price");

            // Act: User has captured screen shot of the modal
            Cart.OpenEditPriceModalOnIpad(); //CartEditPriceElement needs to be clicked again on iPad to open and capture MinimumPricingModal.

            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }


    public class T7342_SharedProductSku_Fixture : FixtureBase
    {
        private const string Discount_Percentage = "10";
        public string ShortSku { get; }
        public string DiscountPercentage { get; }

        public T7342_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuWithUmrp;
            DiscountPercentage = Discount_Percentage;
        }
    }


    /// <summary>
    /// Verify the layout of the Minimum Pricing Policy modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9795
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7342
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9795"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7342")]

    public abstract class T7342_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7342_SharedProductSku_Fixture>
    {
        protected readonly T7342_SharedProductSku_Fixture Fixture;

        protected T7342_DesktopBase(ITestOutputHelper output, T7342_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified sku with UMRP
            InitializeVisualTest(config, useEmployeeManagerAccount: true);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetShortSkuWithUmrp()");
            ShoppingCartWorkflow.EmptyCart();

            // Act: User has navigated to PDP and added product to cart 
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));

            // Act: User has applied manual discount of 10 % with reason 10%
            Cart.ApplyDiscountForProduct(Fixture.DiscountPercentage, "Sale price");

            // Act: User has captured screen shot of the modal
            Modal.IsModalVisible();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}