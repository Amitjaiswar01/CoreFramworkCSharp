using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7426_T7432_VerifyLayoutOrderHistoryOrderDetailsForCustomers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7432_iPhone_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7432_MobileBase
    {
        public T7432_iPhone_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7432_Android_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7432_MobileBase
    {
        public T7432_Android_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7432_Emulator_VerifyLayoutOrderHistoryOrderDetailsForCustomers : T7432_MobileBase
    {
        public T7432_Emulator_VerifyLayoutOrderHistoryOrderDetailsForCustomers(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyLayoutOrderHistoryOrderDetailsForCustomers(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Order History and Order Details page for Customers.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9812
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7432
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9812"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7432")]
    public abstract class T7432_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7432_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) {}

        protected void Validate(string config)
        {
            //Arrange : User has logged in as a Customer 
            InitializeVisualTest(config);

            //Act : Navigate to Order History Page
            OrderHistory.Navigate();

            //Act : Capture Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act : Navigate to Order Details page by clicking on one of the Orders
            OrderHistory.NavigateToOrderDetailsPage();

            //Act : Capture Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture,true);
        }
    }
}
