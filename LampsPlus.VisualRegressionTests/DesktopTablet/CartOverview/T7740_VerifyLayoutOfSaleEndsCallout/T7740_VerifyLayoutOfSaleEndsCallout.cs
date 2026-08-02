using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7740_VerifyLayoutOfSaleEndsCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_Windows_VerifyLayoutOfSaleEndsCallout : T7740_DesktopBase
    {
        public T7740_Windows_VerifyLayoutOfSaleEndsCallout(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_Windows_VerifyLayoutOfSaleEndsCalloutForPro : T7740_DesktopBase
    {
        public T7740_Windows_VerifyLayoutOfSaleEndsCalloutForPro(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_Mac_VerifyLayoutOfSaleEndsCallout : T7740_DesktopBase
    {
        public T7740_Mac_VerifyLayoutOfSaleEndsCallout(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_Mac_VerifyLayoutOfSaleEndsCalloutForPro : T7740_DesktopBase
    {
        public T7740_Mac_VerifyLayoutOfSaleEndsCalloutForPro(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_iPad_VerifyLayoutOfSaleEndsCallout : T7740_DesktopBase
    {
        public T7740_iPad_VerifyLayoutOfSaleEndsCallout(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_iPad_VerifyLayoutOfSaleEndsCalloutForPro : T7740_DesktopBase
    {
        public T7740_iPad_VerifyLayoutOfSaleEndsCalloutForPro(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_TabletEmulator_VerifyLayoutOfSaleEndsCallout : T7740_DesktopBase
    {
        public T7740_TabletEmulator_VerifyLayoutOfSaleEndsCallout(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7740_TabletEmulator_VerifyLayoutOfSaleEndsCalloutForPro : T7740_DesktopBase
    {
        public T7740_TabletEmulator_VerifyLayoutOfSaleEndsCalloutForPro(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void VerifyLayoutOfSaleEndsCallout(string config) => Validate(Validate, config);
    }

    public class T7740_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7740_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuForSaleEndsInCallout;
        }
    }


    /// <summary>
    /// Verify the layout of the 'Sale Ends in' callout on Cart Overview page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9783
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7740
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9783"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7740")]

    public abstract class T7740_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7740_SharedSkus_Fixture>
    {
        protected readonly T7740_SharedSkus_Fixture Fixture;

        protected T7740_DesktopBase(ITestOutputHelper output, T7740_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU 
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuForSaleEndsInCallout()");
            
            /*Act:
            If SKU is not available with sale countdown then skip the test
            Else, Add identified SKU to Cart
            */
            bool canSkip = string.IsNullOrWhiteSpace(shortSku);
            if (canSkip)
            {
                Skip.If(canSkip, "No data available for this test");
            }
            else
            {
                ShoppingCartWorkflow.EmptyCart();
                ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
                Assert.True(Cart.IsCurrentPage, "Current Page is not cart page");

                //Act: Capture Screenshot of visible screen
                ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreSaleCountdownCartIdAndMoreYouMayLike(), true, true, Cart.GetMoreYouMayLike(), maxDownOffset: 10, maxRightOffset:10);
            }
        }
    }
}