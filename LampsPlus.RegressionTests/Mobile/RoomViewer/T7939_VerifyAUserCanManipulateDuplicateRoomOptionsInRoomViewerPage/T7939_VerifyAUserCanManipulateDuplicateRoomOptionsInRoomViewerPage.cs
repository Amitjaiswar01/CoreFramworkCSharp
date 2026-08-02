using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.RoomViewer.T7939_VerifyAUserCanManipulateDuplicateRoomOptionsInRoomViewerPage
{
    public class T7939_VerifyUserCanDuplicateRoom
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AugmentedReality)]
        public class T7939_iPhone_VerifyUserCanDuplicateRoom : T7939_MobileBase
        {
            public T7939_iPhone_VerifyUserCanDuplicateRoom(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void VerifyUserCanDuplicateRoom(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AugmentedReality)]
        public class T7939_Emulator_VerifyUserCanDuplicateRoom : T7939_MobileBase
        {
            public T7939_Emulator_VerifyUserCanDuplicateRoom(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void VerifyUserCanDuplicateRoom(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AugmentedReality)]
        public class T7939_Android_VerifyUserCanDuplicateRoom : T7939_MobileBase
        {
            public T7939_Android_VerifyUserCanDuplicateRoom(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
            public void VerifyUserCanDuplicateRoom(string config) => Validate(config);
        }


        /// <summary>
        /// Verify A User Can Manipulate "Duplicate Room" Options in Room Viewer Page
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10613
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7939 
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10613"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7939")]
        public abstract class T7939_MobileBase : TestsBaseMobile
        {
            protected T7939_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrange
                User is having short sku with Ar eligibility
                User has navigated to PDP that has Room Viewer option
                */
                InitializeFunctionalTest(config);
                var shortSku = ProductActions.GetSkufor2DRoom;
                ProductDetail.NavigateToProductDetailByShortSku(shortSku);
                Assert.True(ProductDetail.IsCurrentPage, "Current Page is Not Product Detail Page");

                // Act: Click on View in Your Room button and create a room
                RoomViewerWorkflow.ConfirmRoomViewerModal();
                Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");

                RoomViewer.OpenSampleRoom(1);
                var roomImage = RoomViewer.Get2dArProductHref();

                // Act: Tap on the Room option, select Duplicate option and create the room
                RoomViewer.SelectDuplicateRoom();
                Assert.True(RoomViewer.IsDuplicateRoomModalVisible(), "Duplicate Page not loaded properly");
                RoomViewer.CreateDuplicate2dRoom();
                var shortSkuArModal = RoomViewer.GetSkuData();
                var duplicateRoomImage = RoomViewer.Get2dArProductHref();

                // Assert : Verify 2d Duplicate Room
                Assert.Equals(shortSku.ToLower(), shortSkuArModal, "The Short sku is not matching");
                Assert.Equals(roomImage, duplicateRoomImage, "The Short sku is not matching");
            }
        }
    }
}
