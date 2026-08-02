using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Search;

namespace LampsPlus.RegressionTests.Mobile.Search
{
    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7869_iPhone_VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage : MobileBase
    {
        public T7869_iPhone_VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage(ITestOutputHelper output) : base(output) { }
    
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7869_Emulator_VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage : MobileBase
    {
        public T7869_Emulator_VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxAndSearchTextPersistsThroughoutSearchResultPage(string config) => Validate(config);
    }


    /// <summary>
	/// Verify the Search Box and Search Text Persists Throughout the Search Result Pages.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10211
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7869
	/// </summary>
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10211"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7869")]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    public class MobileBase : SearchTestsBaseMobile
    {
        protected MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            // Arrangement: Identify Random Search Term.
            var randomSearchTerm = Search.GetRandomSearchTerm();

            // Act: Locate Search Field and Enter Random Search Term in Search Field.
            Search.EnterSearchTerm(randomSearchTerm);

            // Act: Tap the Search Icon to Execute the Search.
            Search.ExecuteSearch();

            // Assert: Verify Search Result page is displayed or not.
            Assert.True(Search.IsCurrentPage, "Search Result Page is not displayed.");

            // Assert: Verify Search Box displayed on the Search Results page or not.
            Assert.True(Search.IsSearchBoxVisible, "The Search Box is not displayed on the Search Results Page.");

            // Assert: Verify Search Text is Persistent in the Search Field or not.
            Assert.Equals(randomSearchTerm, Search.GetSearchFieldText(), "The Search Text is not Persistent in the Search Field");
        }
    }
}
