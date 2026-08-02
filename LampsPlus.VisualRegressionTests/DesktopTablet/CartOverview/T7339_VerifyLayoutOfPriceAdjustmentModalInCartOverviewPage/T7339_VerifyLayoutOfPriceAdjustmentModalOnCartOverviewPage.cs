using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7339_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7339_Windows_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage : T7339_DesktopBase
    {
        public T7339_Windows_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(ITestOutputHelper output, T7339_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7339_iPad_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage : T7339_DesktopBase
    {
        public T7339_iPad_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(ITestOutputHelper output, T7339_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7339_TabletEmulator_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage : T7339_DesktopBase
    {
        public T7339_TabletEmulator_VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(ITestOutputHelper output, T7339_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
       public void VerifyLayoutOfPriceAdjustmentModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    public class T7339_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7339_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }


    /// <summary>
    /// Verify Layout Of Price Adjustment Modal On CartOverview Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9789
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7339
    /// </summary>
    public abstract class T7339_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7339_SharedSku_Fixture>
    {
        protected readonly T7339_SharedSku_Fixture Fixture;

        protected T7339_DesktopBase(ITestOutputHelper output, T7339_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /* Arrangement
             User has no Items in Cart 
             User has Identified a SKU with price between $10 - $20 and added to Cart
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            ShoppingCartWorkflow.EmptyCart();

            ProductDetail.AddSingleProductToCart(Fixture.ShortSku);
            Assert.True(Cart.IsCurrentPage, "User is Not on Cart Page");

            /* Act
             User opens Clicks on Discount Tooltip Modal 
             Captures Discount Tooltip 
            */
            Cart.OpenDiscountTooltip();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetDiscountToolTipModal());
        }
    }
}