using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Sort
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7834_Windows_VerifyLayoutOf16PlusCallout : T7834_DesktopBase
    {
        public T7834_Windows_VerifyLayoutOf16PlusCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOf16PlusCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7834_Mac_VerifyLayoutOf16PlusCallout : T7834_DesktopBase
    {
        public T7834_Mac_VerifyLayoutOf16PlusCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOf16PlusCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7834_iPad_VerifyLayoutOf16PlusCallout : T7834_DesktopBase
    {
        public T7834_iPad_VerifyLayoutOf16PlusCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOf16PlusCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7834_TabletEmulator_VerifyLayoutOf16PlusCallout : T7834_DesktopBase
    {
        public T7834_TabletEmulator_VerifyLayoutOf16PlusCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOf16PlusCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of '16+ Colors' Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9587
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7834
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9587"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7834")]
    public abstract class T7834_DesktopBase : T7834_Base
    {
        protected T7834_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    public abstract class T7834_Base : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7834_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) {}

        protected virtual void Validate(string config)
        {
            // Arrange : Find a SKU that has the SixteenPlus Colors callout.
            InitializeVisualTest(config);

            // Act : Navigate to Search Sort Page and scroll to the product.
            Browser.Navigate(Urls.SixteenPlusMoreColorsUrl);
            Sort.ScrollDownToCallout("16+ Colors");

            // Act : Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}