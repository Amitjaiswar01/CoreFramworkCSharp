using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7260_T7261_VerifyLayoutOfEditShippingAddress
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7260_Windows_VerifyTheLayoutOfEditShippingAddress : T7260_DesktopBase
    {
        public T7260_Windows_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7260_Mac_VerifyTheLayoutOfEditShippingAddress : T7260_DesktopBase
    {
        public T7260_Mac_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7260_iPad_VerifyTheLayoutOfEditShippingAddress : T7260_DesktopBase
    {
        public T7260_iPad_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7260_TabletEmulator_VerifyTheLayoutOfEditShippingAddress : T7260_DesktopBase
    {
        public T7260_TabletEmulator_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Shipping Address modal and Shipping Addresses page after adding and editing addresses.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9774
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7260
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9774"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7260")]
    public abstract class T7260_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7260_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page.
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedAddresses();

            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.ShippingAddressOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.ShippingAddressOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            /*Act:
            Click the Shipping Addresses link on the Manage Account page.
            Click the ADD SHIPPING ADDRESS link.
             */
            ManageAccount.OpenShippingAddressForm();

            //Act: Capture a screenshot of the Edit Shipping Address modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            /*Act:
            Fill out the Edit Shipping Address form.
            Click the SAVE button.
             */
            Address.State = StateCodeListUnitedStates.NV;
            ManageAccount.AddNewShippingAddressToModal(Address);
            ManageAccount.SaveShippingAddress();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act: Click the Edit link for the Default saved address.
            ManageAccount.OpenEditShippingAddressModal();

            //Act: Capture a screenshot of the Edit Shipping Address modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            //Act: Edit the Shipping Address phone number and save.
            ManageAccount.ChangeShippingPhoneNumber();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}
