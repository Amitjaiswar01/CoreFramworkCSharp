using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7771_T7772_VerifyLayoutOfStickyNavOnSfp
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7771_Windows_VerifyLayoutOfStickyNavOnSfp : T7771_DesktopBase
    {
        public T7771_Windows_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7771_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7771_Mac_VerifyLayoutOfStickyNavOnSfp : T7771_DesktopBase
    {
        public T7771_Mac_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7771_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7771_iPad_VerifyLayoutOfStickyNavOnSfp : T7771_DesktopBase
    {
        public T7771_iPad_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7771_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7771_TabletEmulator_VerifyLayoutOfStickyNavOnSfp : T7771_DesktopBase
    {
        public T7771_TabletEmulator_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7771_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    public class T7771_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public T7771_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Sticky Nav on the SFP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9885
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7771
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9885"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7771")]
    public abstract class T7771_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7771_SharedSkus_Fixture>
    {
        protected readonly T7771_SharedSkus_Fixture Fixture;

        protected T7771_DesktopBase(ITestOutputHelper output, T7771_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange : Identify a SKU and navigate to its SFP page
            InitializeVisualTest(config);

            var Sku = Fixture.ShortSku;
            Browser.Navigate(Urls.ProductFullPageBaseUrl + Sku);

            // Act : Scroll down view the Sticky Nav
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            // Act: Capture Screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}