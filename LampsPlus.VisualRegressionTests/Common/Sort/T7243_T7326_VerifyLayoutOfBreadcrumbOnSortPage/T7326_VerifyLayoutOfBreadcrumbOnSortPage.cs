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
    public class T7326_iPhone_VerifyLayoutOfBreadcrumbOnSortPage : T7326_MobileBase
    {
        public T7326_iPhone_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7326_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: TT7326. Rework - ACD-10366")]
        //[RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7326_Android_VerifyLayoutOfBreadcrumbOnSortPage : T7326_MobileBase
    {
        public T7326_Android_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7326_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7326_Emulator_VerifyLayoutOfBreadcrumbOnSortPage : T7326_MobileBase
    {
        public T7326_Emulator_VerifyLayoutOfBreadcrumbOnSortPage(ITestOutputHelper output, T7326_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: TT7326. Rework - ACD-10366")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfBreadcrumbOnSortPage(string config) => Validate(Validate, config);
    }


    public class T7326_Fixture : FixtureBase
    {
        public Dictionary<string, string> Filters { get; }

        public T7326_Fixture()
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
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7326
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7428"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7326")]
    public abstract class T7326_MobileBase : VisualTestsBaseMobile, IClassFixture<T7326_Fixture>
    {
        protected readonly T7326_Fixture Fixture;

        protected T7326_MobileBase(ITestOutputHelper output, T7326_Fixture fixture) : base(output, fixture)
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
            Sort.ApplyFilters(0, predefinedFilters:Fixture.Filters);

            /*Act
            Capture a screenshot of the entire visible screen.   
            */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
