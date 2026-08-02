using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7243_T7326_VerifyLayoutOfBreadcrumbOnSortPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7243_Windows_VerifyLayoutOfBreadcrumbOnSortPage : T7243_DesktopBase
    {
        public T7243_Windows_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7243_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7243_Mac_VerifyLayoutOfBreadcrumbOnSortPage : T7243_DesktopBase
    {
        public T7243_Mac_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7243_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7243_iPad_VerifyLayoutOfBreadcrumbOnSortPage : T7243_DesktopBase
    {
        public T7243_iPad_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7243_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7243_TabletEmulator_VerifyLayoutOfBreadcrumbOnSortPage : T7243_DesktopBase
    {
        public T7243_TabletEmulator_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7243_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    public class T7243_Fixture : FixtureBase
    {
        public Dictionary<string, string> Filters { get; }

        public T7243_Fixture()
        {
            Filters = new Dictionary<string, string>
            {
                { "Style", "Contemporary" },
                { "Color", "Beige" },
                { "Type", "Shag" }
            };
        }
    }


    /// <summary>
    /// Verify the layout of the breadcrumb elements on the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7428
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7243
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7428"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7243")]
    public abstract class T7243_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7243_Fixture>
    {
        protected readonly T7243_Fixture Fixture;

        protected T7243_DesktopBase(ITestOutputHelper output, T7243_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrangement
            User is on a Sort Page.
            */
            InitializeVisualTest(config);
            Sort.Navigate("s_not-foo/");
            Assert.True(Sort.IsCurrentPage, "User is not on Sort page.");
            
            /*Act
            On the Sort page, select three filters.    
            */
            Sort.ApplyFilters(0, predefinedFilters: Fixture.Filters);

            /*Act
            Capture a screenshot of the entire visible screen.   
            */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
