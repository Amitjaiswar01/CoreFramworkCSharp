using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7809_T7812_VerifySearchBoxDisplaysTopCategorySearchTerms
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_Windows_VerifySearchBoxDisplaysTopCategorySearchTerms : T7809_DesktopBase
    {
        public T7809_Windows_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_Mac_VerifySearchBoxDisplaysTopCategorySearchTerms : T7809_DesktopBase
    {
        public T7809_Mac_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_iPad_VerifySearchBoxDisplaysTopCategorySearchTerms : T7809_DesktopBase
    {
        public T7809_iPad_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_TabletEmulator_VerifySearchBoxDisplaysTopCategorySearchTerms : T7809_DesktopBase
    {
        public T7809_TabletEmulator_VerifySearchBoxDisplaysTopCategorySearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorySearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Search Box Displays Top Categorical Search Terms.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10051
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7809
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10051"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7809")]
    public abstract class T7809_DesktopBase : TestsBaseDesktop
    {
        protected T7809_DesktopBase(ITestOutputHelper output) : base(output) { }

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
