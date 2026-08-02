using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Search.T7814_T7817_VerifyLayoutOfTopCategorySearchTerms
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_Windows_VerifyLayoutOfTopCategorySearchTerms : T7814_DesktopBase
    {
        public T7814_Windows_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_Mac_VerifyLayoutOfTopCategorySearchTerms : T7814_DesktopBase
    {
        public T7814_Mac_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_iPad_VerifyLayoutOfTopCategorySearchTerms : T7814_DesktopBase
    {
        public T7814_iPad_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_TabletEmulator_VerifyLayoutOfTopCategorySearchTerms : T7814_DesktopBase
    {
        public T7814_TabletEmulator_VerifyLayoutOfTopCategorySearchTerms(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfTopCategorySearchTerms(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9411
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7814
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9411"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7814")]

    public abstract class T7814_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7814_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to the Chandeliers Sort page (https://lampsplus.com/chandeliers) and select one random filter.
            InitializeVisualTest(config);
            Browser.DeleteAllCookies();
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on Sort page.");

            //Act: Click into the Search input box located in the page header.
            Search.ForceSearchDropdownOpen();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}