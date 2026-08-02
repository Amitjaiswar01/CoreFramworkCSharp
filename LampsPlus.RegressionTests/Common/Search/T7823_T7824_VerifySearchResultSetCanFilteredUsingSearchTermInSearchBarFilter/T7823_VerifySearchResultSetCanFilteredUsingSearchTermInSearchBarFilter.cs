using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7823_T7824_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7823_Windows_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter : T7823_DesktopBase
    {
        public T7823_Windows_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7823_Mac_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter : T7823_DesktopBase
    {
        public T7823_Mac_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7823_iPad_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter : T7823_DesktopBase
    {
        public T7823_iPad_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7823_TabletEmulator_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter : T7823_DesktopBase
    {
        public T7823_TabletEmulator_VerifySearchResultSetCanFilteredUsingSearchTermInSearchBarFilter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Search Result Set Can be Filtered Using a Search Term in Search Bar Filter.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10174
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7823
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10927"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7823")]
    public abstract class T7823_DesktopBase : TestsBaseDesktop
    {
        protected T7823_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on the Category Sort Page : https://www.lampsplus.com/products/chandeliers/
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on a Sort page.");
            Sort.ExpandAllFilters();

            //Act : Search Black into the search box and verify right most breadcrumb is the search term
            var searchTerm = "Black";
            Assert.Equals(searchTerm, Sort.SearchFilterText(searchTerm), "Last Breadcrumb is not matching");

            //Assert : Verify url contains "s" attribute with search attribute
            var searchFilterUrl = Browser.PageUrl;
            var trimUrl = "s_black";
            Assert.StringContains(searchFilterUrl, trimUrl, "URL does not contain the search term");

            //Act : Navigate to Category Sort Page
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            //Act : Scroll down to Search bar at the bottom of Sort page
            Browser.ScrollIntoView(Sort.GetContextualSearchBar(), true);

            //Act : Search term as Black into the footer search box
            Sort.SearchInContextualSearchBarForSort(searchTerm);

            //Assert : Verify the last breadcrumb contains same search results and url contains "s" attribute with search attribute
            Assert.StringContains(searchFilterUrl, trimUrl, "URL does not contain the search term");
            Assert.Equals(searchTerm, Sort.GetIndividualBreadcrumbNames(1), "Last Breadcrumb is not matching");
        }
    }
}
