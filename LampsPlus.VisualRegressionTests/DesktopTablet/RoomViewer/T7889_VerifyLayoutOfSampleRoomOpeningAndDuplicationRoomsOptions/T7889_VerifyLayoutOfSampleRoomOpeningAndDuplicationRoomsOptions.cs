using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.RoomViewer.T7889_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7889_Windows_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions : T7889_DesktopBase
    {
        public T7889_Windows_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(ITestOutputHelper output, T7889_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7889_Mac_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions : T7889_DesktopBase
    {
        public T7889_Mac_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(ITestOutputHelper output, T7889_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7889_iPad_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions : T7889_DesktopBase
    {
        public T7889_iPad_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(ITestOutputHelper output, T7889_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7889_TabletEmulator_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions : T7889_DesktopBase
    {
        public T7889_TabletEmulator_VerifyLayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(ITestOutputHelper output, T7889_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSampleRoomOpeningAndDuplicationRoomsOptions(string config) => Validate(Validate, config);
    }


    public class T7889_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuWithRoomViewer { get; }

        public T7889_SharedProductSku_Fixture()
        {
            SkuWithRoomViewer = ProductActions.GetSkuThatHasArOption;
        }
    }


    /// <summary>
    /// Verify the Layout of Sample Room 'Opening and Duplication Rooms' Options
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10287
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7889
    /// </summary>
    [Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10287"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7889")]

    public abstract class T7889_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7889_SharedProductSku_Fixture>
    {
        protected readonly T7889_SharedProductSku_Fixture Fixture;

        protected T7889_DesktopBase(ITestOutputHelper output, T7889_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified sku that has Room Viewer option 
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.SkuWithRoomViewer, "ProductActions.GetSkuThatHasArOption;");
            var shortSku = Fixture.SkuWithRoomViewer;

            // Act: Navigate to AR Page and click on Duplicate Room Option 
            RoomViewerWorkflow.AddSingleProductToRoom(shortSku);
            RoomViewer.SelectDuplicateRoomOption();

            // Act: Capture the Screenshot of Visible Area
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Act: Select Create Room button
            RoomViewer.CreateDuplicateRoom();
            Assert.True(RoomViewer.IsNewUnknownRoom("2"), "New Room is not Displayed");

            // Act: Select Open Saved Room option and Capture screenshot of Visible Area
            RoomViewer.OpenSavedRoomModal();
            RoomViewer.WaitForSavedRoomsToDisplay();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}