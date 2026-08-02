using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7895_T7896_VerifyLayoutOfStickyHeaderWithSearchBar
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7896_iPhone_VerifyLayoutOfStickyHeaderWithSearchBar : T7896_MobileBase
    {
        public T7896_iPhone_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7896_AndroidPhone_VerifyLayoutOfStickyHeaderWithSearchBar : T7896_MobileBase
    {
        public T7896_AndroidPhone_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7896_Emulator_VerifyLayoutOfStickyHeaderWithSearchBar : T7896_MobileBase
    {
        public T7896_Emulator_VerifyLayoutOfStickyHeaderWithSearchBar(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfStickyHeaderWithSearchBar(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify Layout of the Sticky Header with a Search Bar
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10344
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7896
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10344"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7896")]
    public abstract class T7896_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7896_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on Home page.");

            // Act : Search for any term in Search input box
            var searchTerm = Search.GetSearchTerm();
            Search.EnterSearchTerm(searchTerm);

            Assert.True(Search.IsCurrentPage, "Search Result Page is not displayed.");

            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            // Act: Capture a screenshot of visual screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}