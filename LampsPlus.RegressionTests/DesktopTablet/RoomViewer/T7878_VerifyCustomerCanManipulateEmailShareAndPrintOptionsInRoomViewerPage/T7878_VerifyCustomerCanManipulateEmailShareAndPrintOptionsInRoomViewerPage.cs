using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T7878_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7878_Windows_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage : T7878_DesktopBase
   {
       public T7878_Windows_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage(ITestOutputHelper output) : base(output) { }

       [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
       [SkippableTheory]
       [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
       public void VerifyRoomViewerFunctionality(string config) => Validate(config);
   }


   [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
   public class T7878_Mac_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage : T7878_DesktopBase
    {
       public T7878_Mac_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage(ITestOutputHelper output) : base(output) { }

       [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
       [SkippableTheory]
       [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
       public void VerifyRoomViewerFunctionality(string config) => Validate(config);
   }


   [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
   public class T7878_iPad_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage : T7878_DesktopBase
    {
       public T7878_iPad_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage(ITestOutputHelper output) : base(output) { }

       [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
       [SkippableTheory]
       [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
       public void VerifyRoomViewerFunctionality(string config) => Validate(config);
   }


   [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
   public class T7878_TabletEmulator_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage : T7878_DesktopBase
    {
       public T7878_TabletEmulator_VerifyCustomerCanManipulateEmailShareAndPrintOptionsInRoomViewerPage(ITestOutputHelper output) : base(output) { }

       [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
       [SkippableTheory]
       [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
       public void VerifyRoomViewerFunctionality(string config) => Validate(config);
   }


    ///<summary>
    /// Windows - Verify A Customer Can Manipulate "Email, Share and Print" Options in Room Viewer Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10255
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7878
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10255"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7878")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T7878_DesktopBase : TestsBaseDesktop
    { 
        protected T7878_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - 2D AR Room should be created using the SKU
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();

            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasArOption");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            ProductDetail.NavigateToArPage();
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");

            // Assert : 2D AR Room should be created using the SKU
            Assert.Equals(shortSku, RoomViewer.GetSkuData(), "The Short sku is not matching");

            // Act : Open Email modal and submit email form
            RoomViewer.OpenAndFocusEmailModal();

            // Assert : EmailRoom modal is displayed
            Assert.Displayed((Modal.GetLpModal()), "Modal is not opened");

            // Act : Submit Email form
            RoomViewer.RoomViewerEmail(new[] {"testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com"});

            // Assert : "Email Sent!" modal is displayed.
            Assert.True(RoomViewer.IsEmailNotificationDisplayed, "Email sent message not displayed");
            Browser.RefreshPage();
            Assert.True(Modal.IsModalNotVisible(), "Modal is displayed");

            // Act: Open ShareRoom modal
            RoomViewer.OpenShareRoomModal();

            // Assert : Share Room modal is displayed.
            Assert.True(RoomViewer.IsShareRoomModalDisplayed, "Share modal is not displayed");
            Modal.CloseLpModal();
            Assert.True(Modal.IsModalNotVisible(), "Modal is displayed");

            // Act: Open print room modal
            RoomViewer.OpenPrintRoomModal();

            // Assert : Print Room modal is displayed.
            Assert.True(RoomViewer.IsPrintModalDisplayed, "Print modal is not displayed");
            Modal.CloseLpModal();
            Assert.True(Modal.IsModalNotVisible(), "Modal is displayed");
        }
    }
}
