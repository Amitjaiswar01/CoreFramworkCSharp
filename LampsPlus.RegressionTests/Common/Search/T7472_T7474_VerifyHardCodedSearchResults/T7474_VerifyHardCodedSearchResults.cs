using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7472_T7474_VerifyHardCodedSearchResults
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7474_iPhone_VerifyHardCodedSearchResult : T7474_MobileBase
    {
        public T7474_iPhone_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7474_Emulator_VerifyHardCodedSearchResult : T7474_MobileBase
    {
        public T7474_Emulator_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user is directed to the appropriate page when searching for a hard coded search term.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10055
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7474
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10055"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7474")]
    public abstract class T7474_MobileBase : TestsBaseMobile
    {
        protected T7474_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on Lamps Plus home page.
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            VerifyRedirectionBySearchTerm(Urls.HelpAndPoliciesPageUrl, "help");
            VerifyRedirectionBySearchTerm(Urls.ContactUsPageUrl, "contact");
            VerifyRedirectionBySearchTerm(Urls.WishListPageUrl, "wishlist");
        }

        private void VerifyRedirectionBySearchTerm(string expectedPage, string searchTerm)
        {
            //Act: Enter in a search term into the Search field and execute the search.
            var isSearchVisible = Search.IsSearchVisibleOnLandingPage();

            if (isSearchVisible)
            {
                Search.OpenSearchBox();
                Search.ClearSearchFieldText();
                Search.EnterSearchTerm(searchTerm);
                Search.ExecuteSearch();
            }
            else
            {
                Search.EnterSearchTerm(searchTerm);
                Search.ExecuteSearch();
            }

            Search.WaitForUrlToContainFirstFourCharactersOfSearchTerm(searchTerm);

            //Assert: The user is re-directed to the proper URL.
            Assert.Equals(expectedPage, Browser.PageUrl, "The user is not redirected to the " + searchTerm + " page.");
        }
    }
}
