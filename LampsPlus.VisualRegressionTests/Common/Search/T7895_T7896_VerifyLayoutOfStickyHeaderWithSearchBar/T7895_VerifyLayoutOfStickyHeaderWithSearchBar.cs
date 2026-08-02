using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7895_T7896_VerifyLayoutOfStickyHeaderWithSearchBar
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7895_Windows_VerifyLayoutOfStickyHeaderWithSearchBar : T7895_DesktopBase
    {
        public T7895_Windows_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7895_Mac_VerifyLayoutOfStickyHeaderWithSearchBar : T7895_DesktopBase
    {
        public T7895_Mac_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7895_iPad_VerifyLayoutOfStickyHeaderWithSearchBar : T7895_DesktopBase
    {
        public T7895_iPad_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7895_TabletEmulator_VerifyLayoutOfStickyHeaderWithSearchBar : T7895_DesktopBase
    {
        public T7895_TabletEmulator_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify Layout of the Sticky Header with a Search Bar
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10344
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7895
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10344"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7895")]
    public abstract class T7895_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7895_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            // Act : Search for any term in Search input box
            var searchTerm = Search.GetSearchTerm();
            Search.EnterSearchTerm(searchTerm);

            Assert.True(Search.IsCurrentPage, "Search Result Page is not displayed.");

            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            // Act: Capture a screenshot of visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}