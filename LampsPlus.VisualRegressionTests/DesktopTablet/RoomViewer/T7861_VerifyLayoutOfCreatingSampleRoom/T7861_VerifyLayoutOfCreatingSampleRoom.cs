using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.RoomViewer.T7861_VerifyLayoutOfCreatingSampleRoom
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7861_Windows_VerifyLayoutOfCreatingSampleRoom : T7861_DesktopBase
    {
        public T7861_Windows_VerifyLayoutOfCreatingSampleRoom(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCreatingSampleRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7861_Windows_VerifyLayoutOfCreatingSampleRoomForKiosk: T7861_DesktopBase
    {
        public T7861_Windows_VerifyLayoutOfCreatingSampleRoomForKiosk(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfCreatingSampleRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7861_Mac_VerifyLayoutOfCreatingSampleRoom : T7861_DesktopBase
    {
        public T7861_Mac_VerifyLayoutOfCreatingSampleRoom(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7861. Rework - ACD-10787")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCreatingSampleRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7861_iPad_VerifyLayoutOfCreatingSampleRoom : T7861_DesktopBase
    {
        public T7861_iPad_VerifyLayoutOfCreatingSampleRoom(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCreatingSampleRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7861_TabletEmulator_VerifyLayoutOfCreatingSampleRoom : T7861_DesktopBase
    {
        public T7861_TabletEmulator_VerifyLayoutOfCreatingSampleRoom(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCreatingSampleRoom(string config) => Validate(Validate, config);
    }


    public class T7861_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuWithRoomViewer { get; }

        public T7861_SharedProductSku_Fixture()
        {
            SkuWithRoomViewer = ProductActions.GetSkuThatHasArOption;
        }
    }


    /// <summary>
    /// Verify the Layout of Creating Sample Room
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10285
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7861
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10285"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7861")]

    public abstract class T7861_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7861_SharedProductSku_Fixture>
    {
        protected readonly T7861_SharedProductSku_Fixture Fixture;

        protected T7861_DesktopBase(ITestOutputHelper output, T7861_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified sku that has Room Viewer option 
            InitializeVisualTest(config);
            RoomViewer.DeleteSavedRooms();

            Assert.DatabaseObject(Fixture.SkuWithRoomViewer, "ProductActions.GetSkuThatHasArOption;");
            var shortSku = Fixture.SkuWithRoomViewer;

            // Act: User has navigated to PDP that has Room Viewer option and clicked on the View in your room button
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.ClickOnViewInYourRoom();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Act: User has opened Sample Room Modal and Captured Screenshot of Visible Screen
            ProductDetail.OpenSampleRoomModal();
            ScreenCapturer.CaptureScreen("Sample Room Modal", ScreenshotType.VisualAreaCapture);

            // Act : User has Opened sample room and Captured Screenshot of Entire Page
            ProductDetail.SelectSampleArRoom();
            Browser.SwitchToDefaultContent();
            Assert.True(RoomViewer.IsCurrentPage, "Current Page is Not Room Viewer Page");
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
