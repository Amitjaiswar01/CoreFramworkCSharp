using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7233_T7324_VerifySearchTermPersistsAfterSearching
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_iPhone_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_iPhone_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_AndroidPhone_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_AndroidPhone_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_Emulator_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_Emulator_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Search term is still present in the Search box after searching for a keyword and landing on a sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9873
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7324
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9873"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7324")]
    public abstract class T7324_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7324_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Lamps Plus homepage.
            InitializeVisualTest(config, Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on Home page.");

            //Act: On the Lamps Plus homepage, enter lamp in the search field.
            var searchText = "lamp";
            Search.EnterSearchTerm(searchText);
            Search.DisplaySearchDropdownOnHomepage();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: From the auto-suggest options displayed for lamp, select the lamp shades option.
            var searchResultText = "lamp shades";
            var linkToClick = Search.GetAutoSuggestDropDownResults(searchResultText);

            //Act: Once the Lamps Shades sort page is loaded, capture a screenshot of the Search element.
            Search.SelectOptionFromSearchDropdown(linkToClick);
            Sort.WaitForFilter();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Search.GetSearchField());
        }
    }
}