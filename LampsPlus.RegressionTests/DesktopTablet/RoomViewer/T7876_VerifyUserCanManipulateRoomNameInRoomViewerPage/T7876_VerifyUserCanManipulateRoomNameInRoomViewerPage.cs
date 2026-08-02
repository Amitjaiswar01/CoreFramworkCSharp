using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T7876_VerifyUserCanManipulateRoomNameInRoomViewerPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7876_Windows_VerifyAUserCanManipulateRoomNameInRoomViewerPage : T7876_DesktopBase
    {
        public T7876_Windows_VerifyAUserCanManipulateRoomNameInRoomViewerPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanManipulateRoomNameInRoomViewerPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T725_Mac_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T7876_DesktopBase
    {
        public T725_Mac_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanManipulateRoomNameInRoomViewerPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_iPad_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T7876_DesktopBase
    {
        public T725_iPad_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanManipulateRoomNameInRoomViewerPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_TabletEmulator_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T7876_DesktopBase
    {
        public T725_TabletEmulator_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCanManipulateRoomNameInRoomViewerPage(string config) => Validate(config);
    }


    /// <summary>
    /// Windows - Verify A Customer Can Manipulate "Room Name" in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10253
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7876
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10253"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7876")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T7876_DesktopBase : TestsBaseDesktop
    {
        protected T7876_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Navigate to page that has AR option
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasArOption");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            // Act - Open the AR room
            ProductDetail.NavigateToArPage();
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");

            // Assert - 2D AR Room should be created using the SKU 
            Assert.Equals(shortSku, RoomViewer.GetSkuData(), "The Short sku is not matching");

            // Act - Change the AR room name to CornerRoom
            var NewRoomName = "CornerRoom";
            RoomViewer.ChangeRoomName(NewRoomName);

            // Assert - 2D AR Room should be created using the SKU 
            Assert.Equals(NewRoomName, RoomViewer.GetRoomName(), "Room Name is Not Matching");
        }
    }
}
