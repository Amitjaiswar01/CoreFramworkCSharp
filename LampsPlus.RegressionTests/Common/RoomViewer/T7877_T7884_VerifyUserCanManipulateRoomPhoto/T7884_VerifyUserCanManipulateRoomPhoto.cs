using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.RoomViewer.T7877_T7884_VerifyUserCanManipulateRoomPhoto
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7884_iPhone_VerifyUserCanManipulateRoomPhoto : T7884_MobileBase
    {
        public T7884_iPhone_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7884_Emulator_VerifyUserCanManipulateRoomPhoto : T7884_MobileBase
    {
        public T7884_Emulator_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    /// <summary>
    /// Verify A User Can Manipulate "Room Photo" in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10657
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7884
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10657"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7884")]
    public abstract class T7884_MobileBase : TestsBaseMobile
    {
        protected T7884_MobileBase(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            //Arrange: Navigate to any PDP
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatCanCreate2DRoom");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            /*Act:
            Navigate to View In Your Page
            Store the href value of Background applied through PDP page 
            */
            RoomViewerWorkflow.ConfirmRoomViewerModal();
            Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");
            RoomViewer.OpenSampleRoom(1);

            /*Assert:
            Verify the navigated page is room viewer
            Verify Database Sku and Room Sku are the same 
            */
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");

            var sampleBackgroundFromPdp = RoomViewer.GetArCanvasHref(0);
            Assert.Equals(shortSku, RoomViewer.GetSkuData(), "The Short sku is not matching");

            /*Act:
            Click on Change room photo button
            Select the background image from the sample photo section
            Store the new href background value from Change room section
            */
            RoomViewer.ChangeRoomBackground();

            Assert.True(RoomViewer.IsChooseSampleRoomVisible, "Ar sample room section is visible");

            RoomViewer.ChooseSampleImageFromChangeRoomImageSection();

            var sampleBackgroundFromAr = RoomViewer.GetArCanvasHref(0);

            //Assert: Verify both image href value are different 
            Assert.False(sampleBackgroundFromPdp == sampleBackgroundFromAr, "Both the selected sample images are same");
        }
    }
}