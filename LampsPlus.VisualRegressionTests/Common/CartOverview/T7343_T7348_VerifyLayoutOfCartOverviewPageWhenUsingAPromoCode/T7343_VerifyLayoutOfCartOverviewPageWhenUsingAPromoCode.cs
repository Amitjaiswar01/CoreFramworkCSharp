using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7343_T7348_VerifyLayoutOfCartOverviewPageWhenUsingAPromoCode
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_Windows_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_Windows_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_Mac_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_Mac_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_iPad_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_iPad_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_TabletEmulator_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_TabletEmulator_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    public class T7343_ShareSkus_Fixture : FixtureBase
    {
        public string ShortSkuThatMeetsMinimumOrder { get; }

        public T7343_ShareSkus_Fixture()
        {
            ShortSkuThatMeetsMinimumOrder = ProductActions.GetShortSkuThatMeetsMinimumOrder;
        }
    }


    /// <summary>
    /// Verify Layout Of CartOverview Page When Using A PromoCode.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9781
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7343
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9781"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7343")]

    public abstract class T7343_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7343_ShareSkus_Fixture>
    {
        protected readonly T7343_ShareSkus_Fixture Fixture;

        protected T7343_DesktopBase(ITestOutputHelper output, T7343_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            User has added the SKU item to the cart.
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSkuThatMeetsMinimumOrder, "ProductionActions.GetShortSkuThatMeetsMinimumOrder");

            /*Act
            Navigate to the Cart Overview page.
            Click on the 'Add Promo Code' link on the Cart Overview page. 
            */
            ProductDetail.AddSingleProductToCart(Fixture.ShortSkuThatMeetsMinimumOrder);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            Cart.OpenPromoCodeEntryField();

            // Act: Capture screenshot of visible screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.GetMoreYouMayLike(), offset: 10);

            /*Act
            Enter 1234 in the promo code field
            Capture a screenshot of the Visible Screen.
            */
            var invalidPromoCode = Cart.GetInvalidPromoCodeValue();
            Cart.UpdatePromoCode(invalidPromoCode);
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.GetMoreYouMayLike(), offset: 10);

            /*Act
            Enter AutoPromoCodeTest in the promo code field
            Capture a screenshot of the Visible Screen.
            */
            Cart.ClearPromoCode();
            Cart.UpdatePromoCode(PromoCodeList.AutoPromoCodeTest.Name);
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.GetMoreYouMayLike(), maxDownOffset: 10);
        }
    }
}