using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7866_T7816_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7816_iPhone_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7816_MobileBase
    {
        public T7816_iPhone_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7816. Rework - ACD-10860")]
        //[RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItems(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7816_AndroidPhone_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7816_MobileBase
    {
        public T7816_AndroidPhone_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItems(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7816_Emulator_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems : T7816_MobileBase
    {
        public T7816_Emulator_VerifyLayoutOfRecentSearchesAndRecentlyViewedItems(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutSearchBoxDisplaysRecentSearchesAndRecentlyViewedItems(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify Layout - Search Box Displays Recent Searches And Recently Viewed Items
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10148
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7816
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10148"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7816")]

    public abstract class T7816_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7816_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrangement : User searches for two different free text and two different SKU.
            InitializeVisualTest(config, Urls.HomePageUrl);
            var randomTerm = new List<string> { "pendants", "lamps" };
            SearchWorkflow.SearchRandomTerm(randomTerm);

            var randomProduct = new List<string> { "1d961", "4n706" };
            SearchWorkflow.SearchForMultipleRandomProducts(randomProduct);

            //Act : Navigate to Homepage and check for Search suggestions.    
            Search.SearchSuggestions();

            //Act : Capture a screenshot of the visible screen.   
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Sort.IgnoreCertonaAndLpContainer());
        }
    }
}