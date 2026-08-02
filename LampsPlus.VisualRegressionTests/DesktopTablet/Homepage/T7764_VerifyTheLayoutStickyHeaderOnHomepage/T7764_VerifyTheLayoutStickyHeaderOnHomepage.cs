using Automation.Framework.Enums;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Homepage.T7764_VerifyTheLayoutStickyHeaderOnHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)] 
    public class T7764_Windows_VerifyTheLayoutStickyHeaderOnHomepage : T7764_DesktopBase
    {
        public T7764_Windows_VerifyTheLayoutStickyHeaderOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutStickyHeaderTheHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7764_Mac_VerifyTheLayoutStickyHeaderOnHomepage : T7764_DesktopBase
    {
        public T7764_Mac_VerifyTheLayoutStickyHeaderOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { } 

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutStickyHeaderTheHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7764_iPad_VerifyTheLayoutStickyHeaderOnHomepage : T7764_DesktopBase
    { 
        public T7764_iPad_VerifyTheLayoutStickyHeaderOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutStickyHeaderTheHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7764_TabletEmulator_VerifyTheLayoutStickyHeaderOnHomepage : T7764_DesktopBase
    {
        public T7764_TabletEmulator_VerifyTheLayoutStickyHeaderOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutStickyHeaderTheHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sticky Header on the homepage.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9803
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7764
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9803"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7764")]
    public abstract class T7764_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7764_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            /* Arrangement
             Verify the layout of the Sticky Header on the homepage.
             */
            InitializeVisualTest(config);

            /*Act
            Scroll to the footer of the home page    
            */
            HeaderFooter.ScrollToFooter();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /*Act
            Hover over the 'Chandeliers' menu link in the sticky nav.
            */
            HeaderFooter.HoverOverChandelierStickyNavigation();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /*Act
            Click on the Search icon in the sticky nav.
            */
            Search.EnterSearchTermOnStickyNavigation();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /*Act
            Execute the search.
            */
            HeaderFooterWorkflow.SearchExecution();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}