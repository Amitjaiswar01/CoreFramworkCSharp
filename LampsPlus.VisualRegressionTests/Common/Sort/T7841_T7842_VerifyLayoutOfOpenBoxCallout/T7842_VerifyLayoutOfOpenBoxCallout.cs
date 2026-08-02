using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7841_T7842_VerifyLayoutOfOpenBoxCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7842_iPhone_VerifyLayoutOfOpenBoxCallout : T7842_MobileBase
    {
        public T7842_iPhone_VerifyLayoutOfOpenBoxCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]

        public void VerifyLayoutOfOpenBoxCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7842_Android_VerifyLayoutOfOpenBoxCallout : T7842_MobileBase
    {
        public T7842_Android_VerifyLayoutOfOpenBoxCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfOpenBoxCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7842_Emulator_VerifyLayoutOfOpenBoxCallout : T7842_MobileBase
    {
        public T7842_Emulator_VerifyLayoutOfOpenBoxCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfOpenBoxCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of 'Open Box' Callout
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9605
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7842
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9605"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7842")]
    public abstract class T7842_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7842_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to Open Box Sort Page
            InitializeVisualTest(config);
            Browser.Navigate(Urls.LampsPlusOpenBoxUrl);

            //Act: Scroll the page upto the Open Box CallOut product
            Sort.ScrollDownToCallout("Open Box");

            //Act: Capture a screenshot of the visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}