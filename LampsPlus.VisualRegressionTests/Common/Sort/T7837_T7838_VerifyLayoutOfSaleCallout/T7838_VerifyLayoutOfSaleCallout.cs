using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7837_T7838_VerifyLayoutOfSaleCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7838_iPhone_VerifyLayoutOfSaleCallout : T7838_MobileBase
    {
        public T7838_iPhone_VerifyLayoutOfSaleCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfSaleCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7838_Android__VerifyLayoutOfSaleCallout : T7838_MobileBase
    {
        public T7838_Android__VerifyLayoutOfSaleCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSaleCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7838_Emulator_VerifyLayoutOfSaleCallout : T7838_MobileBase
    {
        public T7838_Emulator_VerifyLayoutOfSaleCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfSaleCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Sale Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9604
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7838
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9604"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7838")]
    public abstract class T7838_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7838_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to Sale Sort Page
            InitializeVisualTest(config);
            Browser.Navigate(Urls.OnSaleUrl);
            Assert.True(Sort.IsCurrentPage, "Current Page is not sale sort page");

            //Act: Scroll the page upto the Sale CallOut product
            Sort.ScrollDownToCallout("Sale");

            //Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}