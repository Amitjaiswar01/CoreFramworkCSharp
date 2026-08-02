using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.RoomViewer.T7420_VerifyLayoutOfCreatingA2DRoom
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7420_iPhone_VerifyLayoutOfCreatingA2DRoom : T7420_MobileBase
    {
        public T7420_iPhone_VerifyLayoutOfCreatingA2DRoom(ITestOutputHelper output, T7420_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "Adam and Dmytro need to determine how to upload photo on Sauce Labs")]
        // [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfCreatingA2DRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7420_AndroidPhone_VerifyLayoutOfCreatingA2DRoom : T7420_MobileBase
    {
        public T7420_AndroidPhone_VerifyLayoutOfCreatingA2DRoom(ITestOutputHelper output, T7420_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [Theory(Skip = "Adam and Dmytro need to determine how to upload photo on Sauce Labs")]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCreatingA2DRoom(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7420_Emulator_VerifyLayoutOfCreatingA2DRoom : T7420_MobileBase
    {
        public T7420_Emulator_VerifyLayoutOfCreatingA2DRoom(ITestOutputHelper output, T7420_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "Adam and Dmytro need to determine how to upload photo on Sauce Labs")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCreatingA2DRoom(string config) => Validate(Validate, config);
    }


    public class T7420_ShareSkus_Fixture : FixtureBase
    {
        public string SkuWithRoomViewer { get; }

        public T7420_ShareSkus_Fixture()
        {
            SkuWithRoomViewer = ProductActions.GetSkuThatHasArOption;
        }
    }

    /// <summary>
    /// Verify the Layout of Creating A 2D Room.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10290
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7420
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10290"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7420")]
    public abstract class T7420_MobileBase : VisualTestsBaseMobile, IClassFixture<T7420_ShareSkus_Fixture>
    {
        protected readonly T7420_ShareSkus_Fixture Fixture;

        protected T7420_MobileBase(ITestOutputHelper output, T7420_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange: Locate the sku's for 2D Room 
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.SkuWithRoomViewer, "ProductActions.GetSkuThatHasArOption;");
            var shortSku = Fixture.SkuWithRoomViewer;

            // Act: User has navigated to PDP that has Room Viewer option and clicked on the View in your room button
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.ClickOnViewInYourRoom();

            Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");

            /* Act:
            User has captured screenshot of visible screen
            Click on 2D Viewer
            Upload the photo
             */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            RoomViewer.ChooseArViewType(1);

            RoomViewer.UploadPhoto();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            RoomViewer.SelectEraseButton();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            RoomViewer.SelectEraseCancelButton();
            RoomViewer.SelectRotateButton();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            RoomViewer.SelectCropButton();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            RoomViewer.SelectCropCancelButton();
            RoomViewer.SelectProceedButton();

            // Act: User has captured screenshot of visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
