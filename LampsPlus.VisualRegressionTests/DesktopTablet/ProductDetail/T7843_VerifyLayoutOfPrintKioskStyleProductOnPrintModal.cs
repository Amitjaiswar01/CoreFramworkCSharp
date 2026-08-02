using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7843_Windows_VerifyLayoutPrintKioskStyleProductOnPrintModal : T7843_DesktopBase
    {
        public T7843_Windows_VerifyLayoutPrintKioskStyleProductOnPrintModal(ITestOutputHelper output, T7843_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutPrintKioskStyleProductOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7843_Mac_VerifyLayoutPrintKioskStyleProductOnPrintModal : T7843_DesktopBase
    {
        public T7843_Mac_VerifyLayoutPrintKioskStyleProductOnPrintModal(ITestOutputHelper output, T7843_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutPrintKioskStyleProductOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7843_iPad_VerifyLayoutPrintKioskStyleProductOnPrintModal : T7843_DesktopBase
    {
        public T7843_iPad_VerifyLayoutPrintKioskStyleProductOnPrintModal(ITestOutputHelper output, T7843_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutPrintKioskStyleProductOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7843_TabletEmulator_VerifyLayoutPrintKioskStyleProductOnPrintModal : T7843_DesktopBase
    {
        public T7843_TabletEmulator_VerifyLayoutPrintKioskStyleProductOnPrintModal(ITestOutputHelper output, T7843_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutPrintKioskStyleProductOnPrintModal(string config) => Validate(Validate, config);
    }


    public class T7843_ShareSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7843_ShareSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Print Kiosk Style Product on Print Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9642
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7843
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9642"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7843")]
    public abstract class T7843_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7843_ShareSku_Fixture>

    {
        protected readonly T7843_ShareSku_Fixture Fixture;

        protected T7843_DesktopBase(ITestOutputHelper output, T7843_ShareSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrangement
            Sign in as Employee
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            /*Act
            Load the PDP page
            Click on Print Icon
            */
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            ProductDetail.ClickOnPrintIcon();

            ProductDetail.ClickOnPrintKioskStyleIcon();

            // Act: Capture the Screenshot of the page 
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}