using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7809_T7812_VerifySearchBoxDisplaysTopCategorySearchTerms
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7812_iPhone_VerifySearchBoxDisplaysTopCategorySearchTerms : T7812_MobileBase
    {
        public T7812_iPhone_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7812_Android_VerifySearchBoxDisplaysTopCategorySearchTerms : T7812_MobileBase
    {
        public T7812_Android_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7812_Emulator_VerifySearchBoxDisplaysTopCategorySearchTerms : T7812_MobileBase
    {
        public T7812_Emulator_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Search Box Displays Top Categorical Search Terms.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10051
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7812
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10051"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7812")]
    public abstract class T7812_MobileBase : TestsBaseMobile
    {
        protected T7812_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to the Chandeliers sort page and select one random filter.
            InitializeFunctionalTest(config, Urls.AllChandeliersSortPageUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on the Sort page.");
            Sort.ApplyFilters(1);

            //Act: Click into the Search input box located in the page header.
            Search.OpenSearchBox();

            var totalTopCategorySearchesCount = Search.GetCountOfTopProductSearches();
            var topCategorySearchModalData = Search.GetSearchModalTopChandelierContent();
            var topSearchAllContent = Search.GetParsedListOfTopCategories(topCategorySearchModalData);

            //Verify: The list of 10 Top Chandeliers Searches is displayed.
            for (var topCategorySearchesTerm = 0; topCategorySearchesTerm < totalTopCategorySearchesCount; topCategorySearchesTerm++)
            {
                var topSearchValue = Search.GetTopCategorySearchTerm(topCategorySearchesTerm);

                Assert.StringContains(topSearchAllContent, topSearchValue, "Top Searches does not matches");
            }
        }
    }
}
