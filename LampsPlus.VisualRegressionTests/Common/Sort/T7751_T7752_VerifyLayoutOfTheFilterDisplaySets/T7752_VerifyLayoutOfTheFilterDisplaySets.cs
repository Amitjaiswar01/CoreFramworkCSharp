using System.Linq;
using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7751_T7752_VerifyLayoutOfTheFilterDisplaySets
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7752_iPhone_VerifyLayoutOfTheFilterDisplaySets : T7752_MobileBase
    {
        public T7752_iPhone_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7752_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7752_Android_VerifyLayoutOfTheFilterDisplaySets : T7752_MobileBase
    {
        public T7752_Android_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7752_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7752_Emulator_VerifyLayoutOfTheFilterDisplaySets : T7752_MobileBase
    {
        public T7752_Emulator_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7752_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    public class T7752_SharedUrls_Fixture : FixtureBase
    {
        public List<string> SortUrl { get; }

        public T7752_SharedUrls_Fixture()
        {
            SortUrl = ProductActions.GetSortUrlForFds();
            SortUrl = SortUrl.Distinct().ToList();
        }
    }


    /// <summary>
    /// Verify the layout of the Filter Display Sets
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9884
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7752
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9884"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7752")]
    public abstract class T7752_MobileBase : VisualTestsBaseMobile, IClassFixture<T7752_SharedUrls_Fixture>
    {
        protected readonly T7752_SharedUrls_Fixture Fixture;

        protected T7752_MobileBase(ITestOutputHelper output, T7752_SharedUrls_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange : Find 2 qualifying Sort pages
            InitializeVisualTest(config);
            var sortUrl = Fixture.SortUrl;

            //Act : Loop to verify Filter Displays Sets of  two Urls returned from query
            foreach (var uniqueSortUrl in sortUrl)
            {
                //Act : Navigate to Sort page returned from query
                Browser.Navigate("https://www.lampsplus.com" + uniqueSortUrl);

                //Act : Expand filters on Sort page
                Sort.ExpandAllFilters();

                //Act : Capture entire page screenshot
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

                //Act : Close filter drawer and scroll to the bottom of the page so that the sticky nav bar appears
                Sort.CloseFilterMenu();
                Browser.ScrollToBottomOfPage(Browser.PageUrl);
                Assert.True(Sort.DoesStickyHeaderDisplayOnSort(), "Sticky Navigation bar is not visible");

                //Act : Capture visible page screenshot
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            }
        }
    }
}