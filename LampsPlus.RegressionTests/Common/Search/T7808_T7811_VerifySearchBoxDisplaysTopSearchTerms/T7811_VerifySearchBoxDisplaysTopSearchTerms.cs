using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7808_T7811_VerifySearchBoxDisplaysTopSearchTerms
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7811_iPhone_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_iPhone_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7811_Android_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_Android_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7811_Emulator_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_Emulator_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Search Box Displays Top Search Terms
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10053
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7811
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10053"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7811")]
    public abstract class T7811_MobileBase : TestsBaseMobile
    {
        protected T7811_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to https://www.lampsplus.com/ 
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act: Click into the Search input box located in the page header.
            Search.SearchSuggestions();

            var totalTopCategorySearchesCount = Search.GetCountOfTopProductSearches();
            var topSearchModalData = Search.GetTopSearchesFromSearchModal();
            var topSearchAllContent = Search.GetParsedListOfTopCategories(topSearchModalData);

            //Assert: The list of 10 Top Searches is displayed.
            for (var topSearchesTerm = 0; topSearchesTerm < totalTopCategorySearchesCount; topSearchesTerm++)
            {
                var topSearchValue = Search.GetTopCategorySearchTerm(topSearchesTerm);

                Assert.StringContains(topSearchAllContent, topSearchValue, "Top Searches does not matches");
            }
        }
    }
}
