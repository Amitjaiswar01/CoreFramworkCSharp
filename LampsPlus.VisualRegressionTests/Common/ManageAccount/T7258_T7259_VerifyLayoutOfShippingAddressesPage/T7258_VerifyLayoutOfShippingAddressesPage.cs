using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Desktop;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7258_T7259_VerifyLayoutOfShippingAddressesPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7258_Windows_VerifyLayoutOfShippingAddressesPage : T7258_DesktopBase
    {
        public T7258_Windows_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory()]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7258_Mac_VerifyLayoutOfShippingAddressesPage : T7258_DesktopBase
    {
        public T7258_Mac_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7258_iPad_VerifyLayoutOfShippingAddressesPage : T7258_DesktopBase
    {
        public T7258_iPad_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7258_TabletEmulator_VerifyLayoutOfShippingAddressesPage : T7258_DesktopBase
    {
        public T7258_TabletEmulator_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the entire Shipping Addresses page in Manage Account.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9775
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7258
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9775"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7258")]
    public abstract class T7258_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7258_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has one saved address.
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedAddresses();

            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.ShippingAddressOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.ShippingAddressOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");
            ManageAccount.OpenShippingAddressForm();
            ManageAccount.AddNewShippingAddressToModal(Address);
            ManageAccount.SaveShippingAddress();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}
