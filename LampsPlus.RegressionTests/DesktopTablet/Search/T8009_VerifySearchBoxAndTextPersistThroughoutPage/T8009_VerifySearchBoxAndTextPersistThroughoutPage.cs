using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Search.T8009_VerifySearchBoxAndTextPersistThroughoutPage
{
    //[Collection(LpTraits.BatchGroup.Desktop.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Search)]
    public class T8009_Windows_VerifySearchBoxAndTextPersistThroughoutPage : T8009_DesktopBase
    {
        public T8009_Windows_VerifySearchBoxAndTextPersistThroughoutPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SearchBoxAndSearchTextPersist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Search)]
    public class T8009_Mac_VerifySearchBoxAndTextPersistThroughoutPage : T8009_DesktopBase
    {
        public T8009_Mac_VerifySearchBoxAndTextPersistThroughoutPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchBoxAndSearchTextPersist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Search)]
    public class T8009_iPad_VerifySearchBoxAndTextPersistThroughoutPage : T8009_DesktopBase
    {
        public T8009_iPad_VerifySearchBoxAndTextPersistThroughoutPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchBoxAndSearchTextPersist(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Search)]
    public class T8009_TabletEmulator_VerifySearchBoxAndTextPersistThroughoutPage : T8009_DesktopBase
    {
        public T8009_TabletEmulator_VerifySearchBoxAndTextPersistThroughoutPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchBoxAndSearchTextPersist(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Search Box and Search Text Persists Throughout the Search Result Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10926
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T8009 
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10926"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T8009")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public class T8009_DesktopBase : TestsBaseDesktop
    {
        protected T8009_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: User is on Lamps Plus home page
            InitializeFunctionalTest(config);

            // Act: Get any search term and execute the search
            var searchTerm = Search.GetRandomSearchTerm();
            Search.EnterSearchTerm(searchTerm);
            Search.ExecuteSearch();
            Assert.True(Search.IsCurrentPage, "User is not on Search Result Page");

            /* Assert:
             Search Result page is displayed
             The Search box displayed on the search result page
             The Search Text is persistent in the search field
            */
            Assert.True(Browser.PageUrl.EndsWith("?s=1"), "Search result page is not displayed");
            Assert.True(Search.IsStickySearchFieldVisible, "Search box does not display on the page");
            Assert.Equals(searchTerm, Search.GetSearchTermFromSearchBox(), "Search term does not persist in the search box");
        }
    }
}