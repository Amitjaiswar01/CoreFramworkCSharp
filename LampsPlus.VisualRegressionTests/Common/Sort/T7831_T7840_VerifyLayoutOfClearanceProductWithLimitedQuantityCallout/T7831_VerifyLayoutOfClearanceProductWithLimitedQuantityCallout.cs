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
    public class T7831_Windows_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7831_DesktopBase
    {
        public T7831_Windows_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7831_Mac_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7831_DesktopBase
    {
        public T7831_Mac_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7831_iPad_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7831_DesktopBase
    {
        public T7831_iPad_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7831_TabletEmulator_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout : T7831_DesktopBase
    {
        public T7831_TabletEmulator_VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfClearanceProductWithLimitedQuantityCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of the Clearance Product With Limited Quantity Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9584
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7831
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9584"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7831")]
    public abstract class T7831_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected readonly FixtureBase Fixture;

        protected T7831_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { Fixture = fixture; }

        protected virtual void Validate(string config)
        {
            //Arrange: Find a SKU that has the Clearance and Limited Quantity Callout.
            InitializeVisualTest(config);
            Browser.Navigate(Urls.ClearanceViewPageUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on Sort page.");
            Sort.ScrollDownToCallout("Clearance Left");

            //Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
