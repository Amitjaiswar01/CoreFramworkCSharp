using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7375_T7377_VerifyLayoutOfPlaPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7377_iPhone_VerifyLayoutOfPlaPage : T7377_MobileBase
    {
        public T7377_iPhone_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7377 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7377_AndroidPhone_VerifyLayoutOfPlaPage : T7377_MobileBase
    {
        public T7377_AndroidPhone_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7377 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7377_Emulator_VerifyLayoutOfPlaPage : T7377_MobileBase
    {
        public T7377_Emulator_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7377 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    public class SharedSku_Fixture_T7377 : FixtureBase
    {
        public string PlaShortSku { get; }
        public List<Dictionary<string, string>> Url { get; }

        public SharedSku_Fixture_T7377()
        {
            PlaShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Url = SortActions.GetSortWithNoActiveAbTest();
        }
    }


    /// <summary>
    /// Verify the layout of the Pla page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7514
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7377
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7514"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7377")]
    public abstract class T7377_MobileBase : VisualTestsBaseMobile, IClassFixture<SharedSku_Fixture_T7377>
    {
        protected readonly SharedSku_Fixture_T7377 Fixture;

        protected T7377_MobileBase(ITestOutputHelper output, SharedSku_Fixture_T7377 fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Get a random SKU.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.PlaShortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            //Act: Navigate to PLA page by SKU.
            Browser.Navigate($"https://{Fixture.Url[0]["Url"]}?sfp={Fixture.PlaShortSku}");

            //Act: Once the user is on a PLA page, capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}