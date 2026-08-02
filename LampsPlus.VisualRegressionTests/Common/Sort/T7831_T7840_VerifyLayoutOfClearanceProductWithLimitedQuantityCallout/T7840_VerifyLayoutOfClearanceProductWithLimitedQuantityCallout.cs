using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7831_T7840_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7840_iPhone_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7840_MobileBase
    {
        public T7840_iPhone_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7840_Android_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7840_MobileBase
    {
        public T7840_Android_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7840_Emulator_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7840_MobileBase
    {
        public T7840_Emulator_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of the Clearance Product With Limited Quantity Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9584
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7840
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9584"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7840")]
    public abstract class T7840_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected readonly FixtureBase Fixture;

        protected T7840_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { Fixture = fixture; }

        protected virtual void Validate(string config)
        {
            //Arrange: Find a SKU that has the Clearance and Limited Quantity Callout.
            InitializeVisualTest(config);
            Browser.Navigate(Urls.ClearanceViewPageUrl);
            Sort.ScrollDownToCallout("Clearance Left");

            //Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
