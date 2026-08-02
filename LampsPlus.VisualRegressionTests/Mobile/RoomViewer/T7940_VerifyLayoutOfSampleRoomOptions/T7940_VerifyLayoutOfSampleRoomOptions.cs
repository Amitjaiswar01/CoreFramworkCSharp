using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.RoomViewer.T7940_VerifyLayoutOfSampleRoomOptions
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7940_iPhone_VerifyLayoutOfSampleRoomOptions : T7940_MobileBase
    {
        public T7940_iPhone_VerifyLayoutOfSampleRoomOptions(ITestOutputHelper output, T7940_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfSampleRoomOptions(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7940_AndroidPhone_VerifyLayoutOfSampleRoomOptions : T7940_MobileBase
    {
        public T7940_AndroidPhone_VerifyLayoutOfSampleRoomOptions(ITestOutputHelper output, T7940_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSampleRoomOptions(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7940_Emulator_VerifyLayoutOfSampleRoomOptions : T7940_MobileBase
    {
        public T7940_Emulator_VerifyLayoutOfSampleRoomOptions(ITestOutputHelper output, T7940_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSampleRoomOptions(string config) => Validate(Validate, config);
    }


    public class T7940_SharedSku_Fixture : FixtureBase
    {
        public string SkuWith2DRoom { get; }

        public T7940_SharedSku_Fixture()
        {
            SkuWith2DRoom = ProductActions.GetSkufor2DRoom;
        }
    }


    /// <summary>
    /// Verify User Can Open A Saved Room and Duplicate Room in Room Viewer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10614
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7940
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10614"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7940")]
    public abstract class T7940_MobileBase : VisualTestsBaseMobile, IClassFixture<T7940_SharedSku_Fixture>
    {
        protected readonly T7940_SharedSku_Fixture Fixture;

        protected T7940_MobileBase(ITestOutputHelper output, T7940_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange: Identify a Product compatible for 2D AR Room
            InitializeVisualTest(config);
            var shortSku = Fixture.SkuWith2DRoom;
            Assert.DatabaseObject(shortSku, " ProductActions.GetSkufor2DRoom;");

            // Act: Navigate to PDP and go to AR Page
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            RoomViewerWorkflow.ConfirmRoomViewerModal();
            Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");

            // Act: Select Duplicate Room Option on AR Page
            RoomViewer.OpenSampleRoom(1);
            RoomViewer.SelectDuplicateRoom();

            // Act: Capture a Screenshot of the Visible region 
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Act: Select Open a Saved Room Option 
            Browser.RefreshPage();
            RoomViewer.OpenSavedRoom();

            // Act: Capture a Screenshot of the Visible region 
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
