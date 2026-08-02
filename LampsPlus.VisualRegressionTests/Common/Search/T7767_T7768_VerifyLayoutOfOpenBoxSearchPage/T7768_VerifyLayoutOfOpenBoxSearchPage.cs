using System;
using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7767_T7768_VerifyLayoutOfOpenBoxSearchPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_iPhone_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_iPhone_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_AndroidPhone_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_AndroidPhone_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_Emulator_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_Emulator_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    public class T7768_SharedCategory_Fixture : FixtureBase
    {
        public string RandomCategory { get; }

        public T7768_SharedCategory_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "bathroom vanity lights", "wall sconces", "table lamps", "floor lamps", "lamp shades" };
            int index = random.Next(list.Count);
            RandomCategory = list[index];
        }
    }


    /// <summary>
    /// Verify the layout of the Open Box Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9870
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7768
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9870"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7768")]
    public abstract class T7768_MobileBase : VisualTestsBaseMobile, IClassFixture<T7768_SharedCategory_Fixture>
    {
        protected readonly T7768_SharedCategory_Fixture Fixture;

        protected T7768_MobileBase(ITestOutputHelper output, T7768_SharedCategory_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User is on the Open Box search page - https://www.lampsplus.com/products/openbox_view-open-box-items/
            InitializeVisualTest(config);
            Browser.Navigate(Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on a Sort page.");
            var category = Fixture.RandomCategory;

            //Act: Capture a screenshot of the visible page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Search randomly for one of the following categories: bathroom vanity lights, wall sconces, table lamps, floor lamps, or lamp shades.
            Sort.SearchForRandomCategory(category);

            //Act: Capture a screenshot of the visible page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
