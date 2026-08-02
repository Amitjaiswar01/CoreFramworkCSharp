using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T545_T559_VerifySortPageLoadsCorrectElements
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T545_Windows_VerifySortPageLoadsCorrectElements : T545_DesktopBase
    {
        public T545_Windows_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T545_Mac_VerifySortPageLoadsCorrectElements : T545_DesktopBase
    {
        public T545_Mac_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T545_iPad_VerifySortPageLoadsCorrectElements : T545_DesktopBase
    {
        public T545_iPad_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T545_TabletEmulator_VerifySortPageLoadsCorrectElements : T545_DesktopBase
    {
        public T545_TabletEmulator_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the sort page loads properly and has the correct elements.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10081
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T545
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10081"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T545")]
    public abstract class T545_DesktopBase : TestsBaseDesktop
    {
        protected T545_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to All Chandeliers sort page
            var sortPageUrl = HeaderFooter.GetAllChandeliersLink();
            Browser.Navigate(sortPageUrl);

            //Act : Make a note of the number of results displayed
            var sortResultCount = Sort.GetPageContents()[0];

            /*Assert:
             Verify sort page for the selected category is displayed
             Verify Sort filters are displayed
             Verify pagination list is displayed
             Verify 'Next' button displayed at the end of the pagination list
             Verify  current page number selected
             Verify range of products is displayed at the end of the pagination
            */
            Assert.True(Browser.PageUrl.Contains(sortPageUrl), "Sort page url does not match expected url after initial load.");
            Assert.True(Sort.AreFiltersVisible, "Sort filters are not displayed");
            Assert.True(Sort.IsPaginationDisplayed, "Pagination list is not displayed");
            Assert.True(Sort.IsPaginationNextBtnDisplayed, "Next button is not displayed after pagination list");
            Assert.Equals("1", Sort.GetCurrentPageNumber(), "Current page number is not displayed");
            Assert.Equals($"1 - 84 of {sortResultCount} results", Sort.GetPaginationRange().Replace(",", ""), "The range of products is not displayed");

            //Act : Navigate to any page from page number 2 to 4
            var pageNumber = MathHelper.GetRandomNumber(3) + 1;
            Sort.NavigateToPageNumber(pageNumber);

            /*Assert:
             Verify selected page number displays in sort page Url
             Verify 'Next' and 'Prev' button displayed at the bottom of the page
            */
            Assert.True(Browser.PageUrl.Contains(sortPageUrl + "page_" + pageNumber), "Sort page url does not match expected url after navigating to a different page number.");
            Assert.True(Sort.IsPaginationNextBtnDisplayed, "Next button is not displayed after pagination list");
            Assert.True(Sort.IsPaginationPrevBtnDisplayed, "Prev button is not displayed after pagination list");
        }
    }
}