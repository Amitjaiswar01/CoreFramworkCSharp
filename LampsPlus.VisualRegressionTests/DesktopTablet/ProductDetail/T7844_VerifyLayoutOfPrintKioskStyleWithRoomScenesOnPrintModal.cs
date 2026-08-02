using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderDetails
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7844_Windows_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal : T7844_DesktopBase
    {
        public T7844_Windows_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(ITestOutputHelper output, T7844_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7844_Mac_VerifyLayoutPrintKioskStyleProductOnPrintModal : T7844_DesktopBase
    {
        public T7844_Mac_VerifyLayoutPrintKioskStyleProductOnPrintModal(ITestOutputHelper output, T7844_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutPrintKioskStyleProductOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7844_iPad_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal : T7844_DesktopBase
    {
        public T7844_iPad_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(ITestOutputHelper output, T7844_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7844_TabletEmulator_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal : T7844_DesktopBase
    {
        public T7844_TabletEmulator_VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(ITestOutputHelper output, T7844_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyLayoutOfPrintKioskStyleWithRoomScenesOnPrintModal(string config) => Validate(Validate, config);
    }


    public class T7844_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7844_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the print kiosk style with room scene on print modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9643
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7844
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9643"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7844")]
    public abstract class T7844_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7844_SharedProductSku_Fixture>
    {
        protected readonly T7844_SharedProductSku_Fixture Fixture;

        protected T7844_DesktopBase(ITestOutputHelper output, T7844_SharedProductSku_Fixture fixture) : base(output, fixture)
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
            Load the PDP page and Click on Print Icon
            */
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            ProductDetail.ClickOnPrintIcon();

            ProductDetail.ClickOnPrintKioskStyleWithRoomScene();

            /* Act: Capture the Screenshot of the page */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}