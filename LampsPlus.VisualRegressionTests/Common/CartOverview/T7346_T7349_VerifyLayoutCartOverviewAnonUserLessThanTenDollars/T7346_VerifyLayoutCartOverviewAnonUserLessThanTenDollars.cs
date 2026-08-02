using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7346_T7349_VerifyLayoutCartOverviewAnonUserLessThanTenDollars
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7346_Windows_VerifyLayoutCartOverViewLessThanTenDollars : T7346_DesktopBase
    {
        public T7346_Windows_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7346_Mac_VerifyLayoutCartOverViewLessThanTenDollars : T7346_DesktopBase
    {
        public T7346_Mac_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7346_iPad_VerifyLayoutCartOverViewLessThanTenDollars : T7346_DesktopBase
    {
        public T7346_iPad_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7346_TabletEmulator_VerifyLayoutCartOverViewLessThanTenDollars : T7346_DesktopBase
    {
        public T7346_TabletEmulator_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    public class ShareSku_Fixture : FixtureBase
    {
        public string LessThanTenDollarsShortSku { get; }

        public ShareSku_Fixture()
        {
            LessThanTenDollarsShortSku = ProductActions.GetLessThanTenDollarItem;
        }
    }


    /// <summary>
    /// Verify the layout of the Cart Overview page when an Anonymous user has less than $10 in the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9782
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7346
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9782"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7346")]
    public abstract class T7346_DesktopBase : VisualTestsBaseDesktop, IClassFixture<ShareSku_Fixture>
    {
        protected readonly ShareSku_Fixture Fixture;

        protected T7346_DesktopBase(ITestOutputHelper output, ShareSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected virtual void Validate(string config)
        {
            //Arrange: Add less than $10 item to Cart
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.LessThanTenDollarsShortSku, "ProductActions.GetLessThanTenDollarItem()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = Fixture.LessThanTenDollarsShortSku });

            // Act: Capture Screenshot of Visible Screen
            CaptureScreenshot();

            // Act: Hover over Check Out Now button to enable Tooltip
            Browser.MouseOverJScript(Cart.GetCheckOutNowButton());
            Assert.Displayed(Cart.GetToolTip(), Messages.PromoRelatedMessages.TooltipMsg);
            Browser.Wait.ForDomReady();

            // Act: Capture Screenshot of the Visible Screen
            CaptureScreenshot();

            // Act: Hover over Paypal button to enable Tooltip
            Browser.MouseOverJScript(Cart.GetPaypalButton());
            Assert.Displayed(Cart.GetToolTip(), Messages.PromoRelatedMessages.TooltipMsg);
            Browser.Wait.ForDomReady();

            // Act: Capture Screenshot the Visible Screen
            CaptureScreenshot();
        }

        private void CaptureScreenshot()
        {
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.GetMoreYouMayLike(), maxDownOffset: 10, maxRightOffset:10);
        }
    }
}