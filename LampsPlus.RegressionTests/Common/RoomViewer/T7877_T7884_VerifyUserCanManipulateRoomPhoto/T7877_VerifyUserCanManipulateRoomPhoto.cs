using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.RoomViewer.T7877_T7884_VerifyUserCanManipulateRoomPhoto
{
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7877_Windows_VerifyUserCanManipulateRoomPhoto : T7877_DesktopBase
    {
        public T7877_Windows_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7877_Mac_VerifyUserCanManipulateRoomPhoto : T7877_DesktopBase
    {
        public T7877_Mac_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7877_iPad_VerifyUserCanManipulateRoomPhoto : T7877_DesktopBase
    {
        public T7877_iPad_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7877_TabletEmulator_VerifyUserCanManipulateRoomPhoto : T7877_DesktopBase
    {
        public T7877_TabletEmulator_VerifyUserCanManipulateRoomPhoto(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCanManipulateRoomPhoto(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a customer can manipulate Room Photo in Room Viewer Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10254
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7877
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10254"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7877")]
    
    public abstract class T7877_DesktopBase : TestsBaseDesktop
    {
        protected T7877_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to any PDP
            InitializeFunctionalTest(config); 

            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatCanCreate2DRoom");

            /*Act:
            Navigate to View In Your Page
            Store the href value of Background applied through PDP page 
            */
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.NavigateToArPage();

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
            RoomViewer.SelectChangeRoomBtn();

            Assert.True(RoomViewer.IsChooseSampleRoomVisible, "Ar sample room section is visible");

            RoomViewer.ChooseSampleImageFromChangeRoomImageSection();

            var sampleBackgroundFromAr = RoomViewer.GetArCanvasHref(0);

            //Assert: Verify both image href value are different 
            Assert.False(sampleBackgroundFromPdp == sampleBackgroundFromAr, "Both the selected sample images are same");
        }
    }
}