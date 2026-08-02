using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T545_T559_VerifySortPageLoadsCorrectElements
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_iPhone_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_iPhone_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_iPhone_Pro_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_iPhone_Pro_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_ElasticSearch)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_Android_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_Android_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_Android_Pro_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_Android_Pro_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_Emulator_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_Emulator_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T559_Emulator_Pro_VerifySortPageLoadsCorrectElements : T559_MobileBase
    {
        public T559_Emulator_Pro_VerifySortPageLoadsCorrectElements(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_ElasticSearch)]
        public void VerifySortPageLoadsCorrectElements(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the sort page loads properly and has the correct elements.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10081
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T559
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10081"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T559")]
    public abstract class T559_MobileBase : TestsBaseMobile
    {
        protected T559_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);
            Home.Navigate();

            //Act : Navigate to All Chandeliers sort page
            var sortPageUrl = HeaderFooter.GetGlobalNavLink(HeaderFooter.GetChandeliersNavLink(), HeaderFooter.GetAllChandeliersLink());

            Browser.Navigate("https://" + sortPageUrl);
            Browser.ScrollToTopOfWindow();

            //Act : Make a note of the number of results displayed
            var sortResultCount = Sort.GetNumberOfResults();

            /*Assert:
             Verify sort page for the selected category is displayed
             Verify sort category and the number of sort results is displayed
             Verify sort FILTER button is displayed
            */
            Assert.True(Browser.PageUrl.Contains(sortPageUrl), "Sort page url does not match expected url after initial load.");
            Assert.True(Sort.GetSortTitleText().Contains("Chandelier"), "Sort Category is not displayed");
            Assert.True(Sort.DoesNumberOfResultsDisplay, "Number of Results is not displayed");
            Assert.True(Sort.IsFilterButtonPresent, "Filter button is not displayed");

            //Act : Scroll down to the bottom of the sort page
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            /*Assert:
            Verify pagination list is displayed
            Verify 'Next' arrow button displayed at the end of the pagination list
            Verify  current page number selected
            Verify range of products is displayed at the end of the pagination
            */
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
            Assert.True(Browser.PageUrl.Contains(sortPageUrl + "/page_" + pageNumber), "Sort page url does not match expected url after navigating to a different page number.");
            Assert.True(Sort.IsPaginationNextBtnDisplayed, "Next button is not displayed after pagination list");
            Assert.True(Sort.IsPaginationPrevBtnDisplayed, "Prev button is not displayed after pagination list");
        }
    }
}