using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7426_T7432_VerifyLayoutOrderHistoryOrderDetailsForCustomers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7426_Windows_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7426_DesktopBase
    {
        public T7426_Windows_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7426_Mac_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7426_DesktopBase
    {
        public T7426_Mac_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7426_iPad_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7426_DesktopBase
    {
        public T7426_iPad_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7426_TabletEmulator_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7426_DesktopBase
    {
        public T7426_TabletEmulator_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Order History and Order Details page for Customers.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9812
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7426
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9812"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7426")]
    public abstract class T7426_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7426_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) {}

        protected void Validate(string config)
        {
            //Arrange : User has logged in as a Customer 
            InitializeVisualTest(config);

            //Act : Navigate to Order History Page
            OrderHistory.Navigate();

            //Act : Capture Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            //Act : Navigate to Order Details page by clicking on one of the Orders
            OrderHistory.NavigateToOrderDetailsPage();

            ////Act : Capture Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}