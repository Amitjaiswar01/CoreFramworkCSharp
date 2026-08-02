using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7808_T7811_VerifySearchBoxDisplaysTopSearchTerms
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_Windows_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_Windows_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_Mac_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_Mac_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_iPad_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_iPad_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_TabletEmulator_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_TabletEmulator_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Search Box Displays Top Search Terms
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10053
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7808
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10053"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7808")]
    public abstract class T7808_DesktopBase : TestsBaseDesktop
    {
        protected T7808_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to https://www.lampsplus.com/ 
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act: Click into the Search input box located in the page header.
            Search.OpenSearchBox();

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
