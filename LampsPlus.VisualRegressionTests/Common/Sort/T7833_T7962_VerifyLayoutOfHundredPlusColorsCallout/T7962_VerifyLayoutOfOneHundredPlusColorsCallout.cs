using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7833_T7962_VerifyLayoutOfHundredPlusColorsCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7962_iPhone_VerifyLayoutOfOneHundredPlusColorsCallout : T7962_MobileBase
    {
        public T7962_iPhone_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOneHundredPlus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7962_Android_VerifyLayoutOfOneHundredPlusColorsCallout : T7962_MobileBase
    {
        public T7962_Android_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfOneHundredPlus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7962_Emulator_VerifyLayoutOfOneHundredPlusColorsCallout : T7962_MobileBase
    {
        public T7962_Emulator_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOneHundredPlus(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of 100+ Colors Callout
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10748
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7962
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10748"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7962")]
    public abstract class T7962_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7962_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to Color plus Sort Page
            InitializeVisualTest(config);
            Browser.Navigate(Urls.HundredPlusMoreColorsUrl);
            Assert.True(Sort.IsCurrentPage, "Current Page is not color plus sort page");

            //Act: Scroll the page upto the one Hundred Plus CallOut product
            Sort.ScrollDownToCallout("100+ Colors");

            //Act: Capture a screenshot of the visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
