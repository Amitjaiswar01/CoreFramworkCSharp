using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T7879_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom
{ 
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7879_Windows_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom : T7879_DesktopBase
    {
        public T7879_Windows_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CustomerCanOpenSavedRoomAndDuplicateRoom(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7879_Mac_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom : T7879_DesktopBase
    {
        public T7879_Mac_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CustomerCanOpenSavedRoomAndDuplicateRoom(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7879_iPad_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom : T7879_DesktopBase
    {
        public T7879_iPad_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CustomerCanOpenSavedRoomAndDuplicateRoom(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7879_TabletEmulator_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom : T7879_DesktopBase
    {
        public T7879_TabletEmulator_VerifyCustomerCanOpenSavedRoomAndDuplicateRoom(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CustomerCanOpenSavedRoomAndDuplicateRoom(string config) => Validate(config);
    }


    /// <summary>
    /// Windows - Verify A Customer Can Manipulate "Open a Saved Room and Duplicate Room" Options in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10256
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7879
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10256"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7879")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]

    public abstract class T7879_DesktopBase : TestsBaseDesktop
    {
        protected T7879_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Navigate to Room Viewer Page with a SKU 
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuToCreate2DRoom");
            RoomViewerWorkflow.AddSingleProductToRoom(shortSku);

            // Act - Create a Duplicate Room on AR Page 
            var roomImage = RoomViewer.GetArCanvasHref();
            var productImageInRoom = RoomViewer.GetArProductHref();

            RoomViewer.OpenDuplicateRoom();
            Assert.True(RoomViewer.IsNewUnknownRoom("2"), "New Room is not Displayed");

            // Assert - Room Image and Product Image from the Original Ar page matches with the New one
            Assert.Equals(roomImage, RoomViewer.GetArCanvasHref(), "Room Image is Different from the Original");
            Assert.Equals(productImageInRoom, RoomViewer.GetArProductHref(), "Product Image is Different from the Original");

            // Act - Open Saved Room Modal
            RoomViewer.OpenSavedRoomModal();

            // Assert - "Your Saved Rooms" is Displayed as the Title of the Modal
            Assert.Equals(RoomViewer.GetSavedRoomModalTitle(), "Your Saved Rooms", "Saved Room Modal Title is Incorrect");

            //Act - Select Non Active Room in the Modal
            RoomViewer.OpenNonActiveRoom();
            Assert.True(RoomViewer.IsNewUnknownRoom("1"), "New Room is not Displayed");

            // Assert - Room Image and Product Image from the Original Ar page matches with this Ar Page
            Assert.Equals(roomImage, RoomViewer.GetArCanvasHref(), "Room Image is Different from the Original");
            Assert.Equals(productImageInRoom, RoomViewer.GetArProductHref(), "Product Image is Different from the Original");
        }
    }
}