using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Sort
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7835_Windows_VerifyLayoutOfMoreOptionsCallout : T7835_DesktopBase
    {
        public T7835_Windows_VerifyLayoutOfMoreOptionsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfMoreOptionsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7835_Mac_VerifyLayoutOfMoreOptionsCallout : T7835_DesktopBase
    {
        public T7835_Mac_VerifyLayoutOfMoreOptionsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfMoreOptionsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7835_iPad_VerifyLayoutOfMoreOptionsCallout : T7835_DesktopBase
    {
        public T7835_iPad_VerifyLayoutOfMoreOptionsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfMoreOptionsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7835_TabletEmulator_VerifyLayoutOfMoreOptionsCallout : T7835_DesktopBase
    {
        public T7835_TabletEmulator_VerifyLayoutOfMoreOptionsCallout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfMoreOptionsCallout(string config) => Validate(Validate, config);
    }

    
    /// <summary>
    /// Verify the Layout of the 'More Options' Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9588
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7835
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9588"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7835")]
    public abstract class T7835_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7835_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to Sort Page having More Options Callout
            InitializeVisualTest(config);
            Browser.Navigate(Urls.SixteenPlusMoreColorsUrl);
            Assert.True(Sort.IsCurrentPage, "Current Page is not sort page");

            //Act: Scroll the page upto the More Options CallOut product
            Sort.ScrollDownToCallout("More Options");

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}