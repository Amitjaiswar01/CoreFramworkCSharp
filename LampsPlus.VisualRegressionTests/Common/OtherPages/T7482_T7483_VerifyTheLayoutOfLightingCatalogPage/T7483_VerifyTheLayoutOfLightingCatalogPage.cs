using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages.T7482_T7483_VerifyTheLayoutOfLightingCatalogPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7483_iPhone_VerifyTheLayoutOfLightingCatalogPage : T7483_MobileBase
    {
        public T7483_iPhone_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7483_AndroidPhone_VerifyTheLayoutOfLightingCatalogPage : T7483_MobileBase
    {
        public T7483_AndroidPhone_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7483_Emulator_VerifyTheLayoutOfLightingCatalogPage : T7483_MobileBase
    {
        public T7483_Emulator_VerifyTheLayoutOfLightingCatalogPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfLightingCatalogPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Lighting Catalog Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7483
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7483")]
    public abstract class T7483_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7483_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            /*Arrange:
            User is on the homepage(https://www.lampsplus.com/)
            Scroll down to footer  
            */
            InitializeVisualTest(config, Urls.HomePageUrl);
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            //Act: On footer, click on catalog link https://www.lampsplus.com/lighting-catalog/
            HeaderFooter.LoadLightingCatalog();

            //Act: Once the page loads, Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true, true);
        }
    }
}
