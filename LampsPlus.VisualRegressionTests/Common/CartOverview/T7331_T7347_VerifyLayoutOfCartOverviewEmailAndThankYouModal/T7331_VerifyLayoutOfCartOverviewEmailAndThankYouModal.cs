using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7331_T7347_VerifyLayoutOfCartOverviewEmailAndThankYouModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7331_Windows_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7331_DesktopBase
    {
        public T7331_Windows_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7331_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7331_Mac_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7331_DesktopBase
    {
        public T7331_Mac_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7331_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7331_iPad_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7331_DesktopBase
    {
        public T7331_iPad_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7331_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7331_TabletEmulator_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7331_DesktopBase
    {
        public T7331_TabletEmulator_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7331_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    public class T7331_ShareSkus_Fixture : FixtureBase
    {
        public string RegularPricedShortSku { get; }
        public string SaleShortSku { get; }

        public T7331_ShareSkus_Fixture()
        {
            RegularPricedShortSku = ProductActions.GetItemNotOnSale;
            SaleShortSku = ProductActions.GetShortSkuOnClearance;
        }
    }

    /// <summary>
    /// Verify the Layout of Cart Overview Email And Thank You Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9780
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7331
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9780"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7331")]
    public abstract class T7331_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7331_ShareSkus_Fixture>
    {
        protected readonly T7331_ShareSkus_Fixture Fixture;

        protected T7331_DesktopBase(ITestOutputHelper output, T7331_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            Navigate to any Regular Products and Add them to the cart
            Navigate to any Sale Products and Add them to the cart
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.RegularPricedShortSku, "ProductActions.GetItemNotOnSale()");
            Assert.DatabaseObject(Fixture.SaleShortSku, "ProductActions.GetShortSkuOnClearance()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.RegularPricedShortSku));
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.SaleShortSku));

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, false, Cart.GetMoreYouMayLike(), maxDownOffset: 10);

            Cart.OpenAndFocusEmailModal();
            
            Browser.SwitchToDefaultContent();

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            Modal.CloseLpModal();

            Cart.EmailShoppingCart("testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com");
            Browser.SwitchToDefaultContent();

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}