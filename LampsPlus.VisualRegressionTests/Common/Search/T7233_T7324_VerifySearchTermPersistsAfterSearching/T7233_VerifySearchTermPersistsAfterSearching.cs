using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7233_T7324_VerifySearchTermPersistsAfterSearching
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_Windows_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_Windows_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_Mac_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_Mac_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_iPad_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_iPad_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_TabletEmulator_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_TabletEmulator_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Search term is still present in the Search box after searching for a keyword and landing on a sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9873
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7233
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9873"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7233")]
    public abstract class T7233_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7233_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: User is on the Lamps Plus homepage.
            InitializeVisualTest(config);

            //Act: On the Lamps Plus homepage, enter lamp in the search field.
            var searchText = "lamp";
            Search.EnterSearchTerm(searchText);
            Search.DisplaySearchDropdownOnHomepage();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: From the auto-suggest options displayed for lamp, select the lamp shades option.
            Browser.RefreshPage();
            Search.EnterSearchTerm(searchText);
            var searchResultText = "lamp shades";
            var linkToClick = Search.GetAutoSuggestDropDownResults(searchResultText);
            var subMenuElement = Search.GetSearchFieldText(searchResultText);

            //Act: Once the Lamps Shades sort page is loaded, capture a screenshot of the Search element.
            if (subMenuElement != null)
            {
                Search.SelectOptionFromSearchDropdown(linkToClick);
                Sort.WaitForH1ToHaveSearchTerm(searchText);
                ScreenCapturer.CaptureElementArea(Browser.PageUrl, Search.GetSearchField());
            }
            else
            {
                Assert.True(false, $"Could not find result '{searchResultText}' in auto suggest dropdown when searched term '{searchText}'");
            }
        }
    }
}