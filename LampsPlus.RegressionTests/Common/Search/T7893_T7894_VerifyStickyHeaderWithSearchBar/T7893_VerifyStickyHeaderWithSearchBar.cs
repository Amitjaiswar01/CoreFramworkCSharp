using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7893_T7894_VerifyStickyHeaderWithSearchBar
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7893_Windows_VerifyStickyHeaderWithSearchBar : T7893_DesktopBase
    {
        public T7893_Windows_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7893_Mac_VerifyStickyHeaderWithSearchBar : T7893_DesktopBase
    {
        public T7893_Mac_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7893_iPad_VerifyStickyHeaderWithSearchBar : T7893_DesktopBase
    {
        public T7893_iPad_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7893_TabletEmulator_VerifyStickyHeaderWithSearchBar : T7893_DesktopBase
    {
        public T7893_TabletEmulator_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
    }
    /// <summary>
    /// Verify Sticky Header with a Search Bar
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10343
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7893
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10343"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7893")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public class T7893_DesktopBase : TestsBaseDesktop
    {
        protected T7893_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Navigate to the homepage
            InitializeFunctionalTest(config);

            /* Act : 
            Get the random search term
            Execute the search on the site
            */
            var searchTerm = Search.GetRandomSearchTerm();
            Search.EnterSearchTerm(searchTerm);
            Search.ExecuteSearch();
            var currentUrl = Browser.PageUrl;

            // Assert : Verify that LP site returns the page with search results.
            Assert.True(Sort.IsCurrentPage, "LP site not returned the page with search results.");

            // Act : Scroll down to Bottom of the Page
            Browser.ScrollToBottomOfPage(currentUrl);

            /* Act : 
            Get the expected alignment details
            Get the alignment details of the Search bar
            Get the search term text from the search bar
             */
            var expectedStickySearchFieldAlignment = "center";
            var stickySearchText = Search.GetStickySearchText();
            var actualStickySearchFieldAlignment = Search.GetStickySearchFieldAlignmentText();

            /* Assert :
            Verify if the alignment detail are matching with expected alignment details
            Verify if the sticky search bar is displayed or not
            Verify if the search term in sticky header matching to the search term on for which search is made 
            */
            Assert.True(Search.IsStickySearchFieldVisible, "Sticky search bar is not displayed");
            Assert.Equals(expectedStickySearchFieldAlignment, actualStickySearchFieldAlignment, "Sticky Search Field is not Center aligned");
            Assert.Equals(searchTerm, stickySearchText, "Search term is not matching.");

            // Act : Clear the search field
            Search.ClearSearchFieldText();

            /* Act :
            Get the new random search term
            Execute the search on the site
            */
            string searchTerm2 = Search.GetRandomSearchTerm();
            Search.EnterSearchTerm(searchTerm2);
            Search.ExecuteSearch();
            var currentUrl2 = Browser.PageUrl;

            // Assert : Verify that LP site returns the page with search results.
            Assert.True(Sort.IsCurrentPage, "LP site not returned the page with search results.");


            // Act : Scroll down to bottom of the Page
            Browser.ScrollToBottomOfPage(currentUrl2);

            /* Act :
            Get the expected alignment details
            Get the Search term text from the search bar
            */
            var stickySearchText2 = Search.GetStickySearchText();
            var actualStickySearchFieldAlignment2 = Search.GetStickySearchFieldAlignmentText();

            /* Assert :
            Verify if the alignment detail are matching with expected alignment details
            Verify if the search term in sticky header matching to the search term on for which search is made 
            */
            Assert.True(Search.IsStickySearchFieldVisible, "Sticky search bar is not displayed");
            Assert.Equals(expectedStickySearchFieldAlignment, actualStickySearchFieldAlignment2, "Sticky Search Field is not Center aligned");
            Assert.Equals(searchTerm2, stickySearchText2, "Search term is not matching.");
        }
    }
}