using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.RoomViewer.T7888_VerifyLayoutOfSampleRoomSharingOption
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7888_Windows_VerifyLayoutOfSampleRoomSharingOption : T7888_DesktopBase
    {
        public T7888_Windows_VerifyLayoutOfSampleRoomSharingOption(ITestOutputHelper output, T7888_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfSampleRoomSharingOption(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7888_Mac_VerifyLayoutOfSampleRoomSharingOption : T7888_DesktopBase
    {
        public T7888_Mac_VerifyLayoutOfSampleRoomSharingOption(ITestOutputHelper output, T7888_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7888. Rework - ACD-10811")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSampleRoomSharingOption(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7888_iPad_VerifyLayoutOfSampleRoomSharingOption : T7888_DesktopBase
    {
        public T7888_iPad_VerifyLayoutOfSampleRoomSharingOption(ITestOutputHelper output, T7888_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSampleRoomSharingOption(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7888_TabletEmulator_VerifyLayoutOfSampleRoomSharingOption : T7888_DesktopBase
    {
        public T7888_TabletEmulator_VerifyLayoutOfSampleRoomSharingOption(ITestOutputHelper output, T7888_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfSampleRoomSharingOption(string config) => Validate(Validate, config);
    }


    public class T7888_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuWithRoomViewer { get; }

        public T7888_SharedProductSku_Fixture()
        {
            SkuWithRoomViewer = ProductActions.GetSkuThatHasArOption;
        }
    }


    /// <summary>
    /// Verify the Layout of Sample Room 'Sharing' Option
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10286
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7888
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10286"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7888")]

    public abstract class T7888_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7888_SharedProductSku_Fixture>
    {
        protected readonly T7888_SharedProductSku_Fixture Fixture;

        protected T7888_DesktopBase(ITestOutputHelper output, T7888_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified sku that has Room Viewer option 
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.SkuWithRoomViewer, "ProductActions.GetSkuThatHasArOption;");
            var shortSku = Fixture.SkuWithRoomViewer;

            // Act: User has navigated to PDP that has Room Viewer option and clicked on the View in your room button
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.NavigateToArPage();
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");

            // Act: Capture the screenshot of Email Modal
            RoomViewer.OpenAndFocusEmailModal();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            Modal.CloseLpModal();

            // Act: Capture the screenshot of Share Your Room Modal
            RoomViewer.OpenShareRoomModal();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            Modal.CloseLpModal();

            // Act: Capture the screenshot of Print Room Modal
            RoomViewer.OpenPrintRoomModal();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            Modal.CloseLpModal();
        }
    }
}

