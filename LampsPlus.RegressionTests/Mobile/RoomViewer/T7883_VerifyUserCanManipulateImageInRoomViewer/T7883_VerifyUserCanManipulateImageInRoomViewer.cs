using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.RoomViewer.T7883_VerifyUserCanManipulateImageInRoomViewer
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7883_iPhone_VerifyUserCanManipulateImageInRoomViewer : T7883_MobileBase
    {
        public T7883_iPhone_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7883. Rework - ACD-10798")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AugmentedReality)]
    public class T7883_Emulator_VerifyUserCanManipulateImageInRoomViewer : T7883_MobileBase
    {
        public T7883_Emulator_VerifyUserCanManipulateImageInRoomViewer(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7883. Rework - ACD-10798")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanManipulateImageInRoomViewer(string config) => Validate(config);
    }


    /// <summary>
    /// Verify A User can Manipulate an Image Display in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10595
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7883
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10595"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7883")]
    public abstract class T7883_MobileBase : TestsBaseMobile
    {
        protected T7883_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Navigate to page that has AR option
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasArOption");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            RoomViewerWorkflow.ConfirmRoomViewerModal();

            Assert.True(RoomViewer.IsArPageContentVisible(), "Ar Page not loaded properly");
            RoomViewer.OpenSampleRoom(1);

            // Assert - Room Viewer page is displayed and sku added in room matches with Sku obtained from query
            Assert.True(RoomViewer.IsCurrentPage, "Room Viewer page is not displayed");
            Assert.Equals(shortSku, RoomViewer.GetSkuData(), "The Short sku is not matching");

            // Act - Hide the SKU
            RoomViewer.HideProduct();

            // Assert - SKU is not displayed in room and Add to Cart button is disabled
            Assert.True(RoomViewer.IsSkuDisplayed, "SKU image is not displayed in room");
            Assert.True(RoomViewer.IsAddToCartDisabled, "Add to cart is not disabled");

            // Act - Show the SKU
            RoomViewer.ShowProduct();

            // Assert - SKU is displayed in the room
            Assert.True(RoomViewer.IsImageInRoomEnabled(), "SKU image is not displayed in room");

            // Act - Deselect the SKU
            RoomViewer.SelectDeselectButton();

            // Assert -Verify Add to Cart, Bring Forward, Send Backward, Flip, Hide, Duplicate, Deselect and Remove buttons are disabled
            Assert.True(RoomViewer.IsAddToCartDisabled, "Add to cart is not disabled");
            Assert.True(RoomViewer.IsBringFwdDisabled, "Bring Forward is not disabled");
            Assert.True(RoomViewer.IsMoveBackDisabled, "Move to Back is not disabled");
            Assert.True(RoomViewer.IsFlipHorizontallyDisabled, "Flip Horizontally is not disabled");
            Assert.True(RoomViewer.IsHideDisabled, "Hide is not disabled");
            Assert.True(RoomViewer.IsDuplicateDisabled, "Duplicate is not disabled");
            Assert.True(RoomViewer.IsDeselectDisabled, "Deselect is not disabled");
            Assert.True(RoomViewer.IsRemoveDisabled, "Remove is not disabled");

            // Act - Remove the SKU
            RoomViewer.SelectRemoveButton();

            // Assert - No Products are being displayed within Sample room
            Assert.True(RoomViewer.GetListOfAllProductsOnRoomViewer().Count == 0, "Room Contains Product");

            // Act - Undo the SKU
            RoomViewer.SelectUndoButton();

            // Assert - Verify Header displays as "Products In This Room "
            Assert.True(RoomViewer.RoomContainsProducts, "Room Contains no Product");

            // Act - Duplicate the SKU
            RoomViewer.SelectDuplicateButton();

            var productsInRoomViewer = RoomViewer.GetListOfAllProductsOnRoomViewer();

            // Assert - Verify Same product Image is displayed as duplicated within the sample room
            Assert.Equals(productsInRoomViewer[0].Name, productsInRoomViewer[1].Name, "Image is not duplicated");
        }
    }
}