using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7472_T7474_VerifyHardCodedSearchResults
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_Windows_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_Windows_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_Mac_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_Mac_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_iPad_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_iPad_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_TabletEmulator_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_TabletEmulator_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user is directed to the appropriate page when searching for a hard coded search term.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10055
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7472 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10055"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7472")]
    public abstract class T7472_DesktopBase : TestsBaseDesktop
    {
        protected T7472_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on Lamps Plus home page.
            InitializeFunctionalTest(config);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            VerifyRedirectionBySearchTerm(Urls.HelpAndPoliciesPageUrl, "help");
            VerifyRedirectionBySearchTerm(Urls.ContactUsPageUrl, "contact");
            VerifyRedirectionBySearchTerm(Urls.WishListPageUrl, "wishlist");
        }

        private void VerifyRedirectionBySearchTerm(string expectedPage, string searchTerm)
        {
            //Act: Enter in a search term into the Search field and execute the search.
            Search.ClearSearchFieldText();
            Search.EnterSearchTerm(searchTerm);
            Search.ExecuteSearch();
            Search.WaitForUrlToContainFirstFourCharactersOfSearchTerm(searchTerm);

            //Assert: The user is re-directed to the proper URL.
            Assert.Equals(expectedPage, Browser.PageUrl, "The user is not redirected to the " + searchTerm + " page.");
        }
    }
}
