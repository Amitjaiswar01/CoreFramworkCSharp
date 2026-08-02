using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.RoomViewer.T7886_VerifyUserCanManipulateOpenSavedRoomOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7886_iPhone_VerifyUserCanManipulateOpenSavedRoomOption : T7886_MobileBase
    {
        public T7886_iPhone_VerifyUserCanManipulateOpenSavedRoomOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7886. Rework - ACD-10722")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanManipulateSavedRoomOption(string config) => Validate(config);

    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7886_Emulator_VerifyUserCanManipulateOpenSavedRoomOption : T7886_MobileBase
    {
        public T7886_Emulator_VerifyUserCanManipulateOpenSavedRoomOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanManipulateSavedRoomOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify A User Can Manipulate "Open a Saved Room" Option in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10659
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7886
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10659"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7886")]
    public abstract class T7886_MobileBase : TestsBaseMobile
    {
        protected T7886_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            User is having short sku with Ar eligibility
            User has navigated to PDP that has Room Viewer option
             */
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkufor2DRoom;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkufor2DRoom");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            // Act: Click on View in Your Room button and create a room
            RoomViewerWorkflow.ConfirmRoomViewerModal();
            Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");
            RoomViewer.OpenSampleRoom(1);
            var roomImage = RoomViewer.Get2dArProductHref();

            // Act: Tap on the Room option, select Saved Room Option
            RoomViewer.OpenSavedRoom();
            var savedRoomHeader = RoomViewer.GetSavedRoomHeader();

            // Assert: Verify Saved Room Header Text
            Assert.Equals(savedRoomHeader, "Your Saved Rooms", "Saved Room Header is Incorrect");

            // Act: Select Active Room and Navigate to PDP by Clicking on Product Thumbnail in Room 
            RoomViewer.SelectSavedRoom(0);
            RoomViewer.SelectProductInRoom();
            Assert.True(ProductDetail.IsCurrentPage,"This is not a PDP" );

            // Act: Click on View in Your Room and Create a different room 
            ProductDetail.ClickOnViewInYourRoom();
            RoomViewer.StartNewRoom();
            RoomViewer.OpenSampleRoom(2);

            // Act: Tap on the Room option, select Saved Room Option and Click on Inactive Room 
            RoomViewer.OpenSavedRoom();
            RoomViewer.SelectSavedRoom(1);
            var savedRoomImage = RoomViewer.Get2dArProductHref();

            // Assert: Verify that the user as selected the room they had created previously
            Assert.Equals(roomImage, savedRoomImage, "Room image is not correct");
        }
    }
}