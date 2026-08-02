using System.Linq;
using System.Collections.Generic;
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
    public class T7751_Windows_VerifyLayoutOfTheFilterDisplaySets : T7751_DesktopBase
    {
        public T7751_Windows_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7751_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7751_Mac_VerifyLayoutOfTheFilterDisplaySets : T7751_DesktopBase
    {
        public T7751_Mac_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7751_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7751_iPad_VerifyLayoutOfTheFilterDisplaySets : T7751_DesktopBase
    {
        public T7751_iPad_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7751_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7751_TabletEmulator_VerifyLayoutOfTheFilterDisplaySets : T7751_DesktopBase
    {
        public T7751_TabletEmulator_VerifyLayoutOfTheFilterDisplaySets(ITestOutputHelper output, T7751_SharedUrls_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfTheFilterDisplaySets(string config) => Validate(Validate, config);
    }


    public class T7751_SharedUrls_Fixture : FixtureBase
    {
        public List<string> SortUrl { get; }

        public T7751_SharedUrls_Fixture()
        {
            SortUrl = ProductActions.GetSortUrlForFds();
            SortUrl = SortUrl.Distinct().ToList();
        }
    }


    /// <summary>
    /// Verify the layout of the Filter Display Sets
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9884
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7751
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9884"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7751")]
    public abstract class T7751_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7751_SharedUrls_Fixture>
    {
        protected readonly T7751_SharedUrls_Fixture Fixture;

        protected T7751_DesktopBase(ITestOutputHelper output, T7751_SharedUrls_Fixture fixture) : base(output, fixture)
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

                //Act : Capture visible page screenshot
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

                //Act : Scroll to the bottom of the page so that the sticky nav bar appears
                Browser.ScrollToBottomOfPage(Browser.PageUrl);
                Assert.True(Sort.DoesStickyHeaderDisplayOnSort(), "Sticky Navigation bar is not visible");

                //Act : Capture visible page screenshot
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            }
        }
    }
}