using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7833_T7962_VerifyLayoutOfHundredPlusColorsCallout
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7833_Windows_VerifyLayoutOfOneHundredPlusColorsCallout : T7833_DesktopBase
    {
        public T7833_Windows_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfOneHundredPlusColorsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7833_Mac_VerifyLayoutOfOneHundredPlusColorsCallout : T7833_DesktopBase
    {
        public T7833_Mac_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfOneHundredPlusColorsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7833_iPad_VerifyLayoutOfOneHundredPlusColorsCallout : T7833_DesktopBase
    {
        public T7833_iPad_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfOneHundredPlusColorsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7833_TabletEmulator_VerifyLayoutOfOneHundredPlusColorsCallout : T7833_DesktopBase
    {
        public T7833_TabletEmulator_VerifyLayoutOfOneHundredPlusColorsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfOneHundredPlusColorsCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of the Layout of '100+ Colors' Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9586
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7833
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9586"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7833")]
    public abstract class T7833_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7833_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            // Arrange : Find a SKU that has the SixteenPlus Colors callout.
            InitializeVisualTest(config);

            // Act : Navigate to Search Sort Page and scroll to the product.
            Browser.Navigate(Urls.HundredPlusMoreColorsUrl);
            Assert.True(Sort.IsCurrentPage, "Current Page is not colorplus sort page");
            Sort.ScrollDownToCallout("100+ Colors");

            // Act : Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}