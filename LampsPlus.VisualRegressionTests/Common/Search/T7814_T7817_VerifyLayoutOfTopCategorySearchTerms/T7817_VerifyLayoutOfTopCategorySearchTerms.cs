using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.Search.T7814_T7817_VerifyLayoutOfTopCategorySearchTerms
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_iPhone_VerifyLayoutOfTopCategorySearchTerms : T7817_MobileBase
    {
        public T7817_iPhone_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_AndroidPhone_VerifyLayoutOfTopCategorySearchTerms : T7817_MobileBase
    {
        public T7817_AndroidPhone_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_Emulator_VerifyLayoutOfTopCategorySearchTerms : T7817_MobileBase
    {
        public T7817_Emulator_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    public class T7817_SharedCategory_Fixture : FixtureBase
    {
        public Dictionary<string, string>[] Filters { get; set; }
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9872
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7817
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9872"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7817")]
    public abstract class T7817_MobileBase : VisualTestsBaseMobile, IClassFixture<T7817_SharedCategory_Fixture>
    {
        protected readonly T7817_SharedCategory_Fixture Fixture;

        protected T7817_MobileBase(ITestOutputHelper output, T7817_SharedCategory_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to the Chandeliers Sort page (https://lampsplus.com/chandeliers) and select one random filter.
            InitializeVisualTest(config);
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            //Act: Tap into the Search input box located in the page header.
            Assert.True(Sort.IsCurrentPage, "User is not on the Sort page.");
            Search.ForceSearchDropdownOpen();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
