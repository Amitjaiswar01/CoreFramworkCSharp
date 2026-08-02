using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7866_T7816_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7866_Windows_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7866_DesktopBase
    {
        public T7866_Windows_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItems(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7866_Mac_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7866_DesktopBase
    {
        public T7866_Mac_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7866. Rework - ACD-10786")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7866_iPad_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7866_DesktopBase
    {
        public T7866_iPad_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7866_TabletEmulator_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7866_DesktopBase
    {
        public T7866_TabletEmulator_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItem(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify Layout - Search Box Displays Recent Searches And Recently Viewed Items .
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10134
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7866
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10134"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7866")]
    public abstract class T7866_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7866_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrangement : User searches for two different free text and two different SKU.
            InitializeVisualTest(config);

            var randomTerm = new List<string> { "pendants", "lamps" };
            Search.SearchRandomTerm(randomTerm);

            var randomProduct = new List<string> { "1d961", "887V4" };
            Search.SearchForMultipleRandomProducts(randomProduct);

            //Act : Navigate to Homepage and check for Search suggestions.    
            Search.SearchSuggestions();

            //Act : Capture a screenshot of the visible screen.   
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{Search.IgnoreRecentlyViewedItems(), Home.IgnoreRecentlyViewedItems()});

            //Act : User Hover over on search term.    
            Search.SearchSuggestions();
            Search.SearchTermHoverOver();

            //Act :Capture a screenshot of the visible screen.   
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Search.IgnoreRecentlyViewedItems(), Home.IgnoreRecentlyViewedItems() });
        }
    }
}
