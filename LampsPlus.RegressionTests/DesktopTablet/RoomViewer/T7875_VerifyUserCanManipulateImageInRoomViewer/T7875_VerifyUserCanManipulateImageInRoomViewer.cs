using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T7875_VerifyUserCanManipulateImageInRoomViewer
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7875_Windows_VerifyUserCanManipulateImageInRoomViewer : T7875_DesktopBase
    {
        public T7875_Windows_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyUserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7875_Mac_VerifyUserCanManipulateImageInRoomViewer : T7875_DesktopBase
    {
        public T7875_Mac_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Zephyr: T7875. Rework - ACD-10934")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyUserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }

    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7875_iPad_VerifyUserCanManipulateImageInRoomViewer : T7875_DesktopBase
    {
        public T7875_iPad_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyUserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7875_TabletEmulator_VerifyUserCanManipulateImageInRoomViewer : T7875_DesktopBase
    {
        public T7875_TabletEmulator_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyUserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }

    
    /// <summary>
    /// Verify A User can Manipulate an Image Display in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10252
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7875
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10252"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7875")]
    public abstract class T7875_DesktopBase : TestsBaseDesktop
    {
        protected T7875_DesktopBase(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            // Arrange - Navigate to page that has AR option
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasArOption");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.NavigateToArPage();
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");
            Assert.Equals(shortSku, RoomViewer.GetSkuData(), "The Short sku is not matching");

            // Act - Hide the SKU
            RoomViewer.SelectHideButton();

            // Assert - SKU is not displayed in room and thumbnail. Add to Cart and Save button are disabled
            Assert.True(RoomViewer.IsSkuDisplayed, "SKU image is displayed in room");
            Assert.True(RoomViewer.IsAddToCartDisabled, "Add to cart is not disabled");
            Assert.True(RoomViewer.IsSaveDisabled, "Save is not disabled");
            Assert.True(RoomViewer.BackToProductDisabled,"Thumbnail image is displayed");

            // Act - Show the SKU
            RoomViewer.SelectShowButton();

            // Assert - SKU is displayed in the room
            Assert.True(RoomViewer.BackToProductEnabled, "SKU image is not displayed in room");

            // Act - Deselect the SKU
            RoomViewer.SelectDeselectButton();

            // Assert - Add to Cart, Save, Hide, Deselect, Duplicate, Remove, BringFwd and MoveBack buttons are disabled and thumbnail image is not displayed
            Assert.True(RoomViewer.BackToProductDisabled, "Thumbnail image is displayed");
            Assert.True(RoomViewer.IsAddToCartDisabled, "Add to cart is not disabled");
            Assert.True(RoomViewer.IsSaveDisabled, "Save is not disabled");
            Assert.True(RoomViewer.IsHideDisabled, "Hide is not disabled");
            Assert.True(RoomViewer.IsDeselectDisabled, "Deselect is not disabled");
            Assert.True(RoomViewer.IsDuplicateDisabled, "Duplicate is not disabled");
            Assert.True(RoomViewer.IsRemoveDisabled, "Remove is not disabled");
            Assert.True(RoomViewer.IsBringFwdDisabled, "Bring Forward is not disabled");
            Assert.True(RoomViewer.IsMoveBackDisabled, "Move to Back is not disabled");
            Assert.True(RoomViewer.IsFlipHorizontallyDisabled, "Flip Horizontally is not disabled");

            // Act - Remove the SKU
            RoomViewer.SelectRemoveButton();

            //Assert - Header displays "Your room contains no products"
            Browser.SwitchToCurrentWindow();
            var actualNoProductText = RoomViewer.GetRoomContainsNoProductText();
            Assert.True(actualNoProductText.Contains("Your room contains no products"), "Room Contains Product");

            // Act - Undo the SKU
            RoomViewer.SelectUndoButton();

            // Assert - Header displays "1 Product In This Room"
            var actualProductText = RoomViewer.GetRoomContainsProductText();
            Assert.True(actualProductText.Contains("1 Product In This Room"), "Room contains no Product");

            // Act - Duplicate the SKU
            RoomViewer.SelectDuplicateButton();

            // Assert - Header displays "2 Product In This Room" 
            var actualProductText2 = RoomViewer.GetRoomContainsProductText();
            Assert.True(actualProductText2.Contains("2 Products In This Room"), "Room does not contain 2 Products");
        }
    }
}
