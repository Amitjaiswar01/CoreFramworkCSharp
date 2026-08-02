using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.Sort.T7375_T7377_VerifyLayoutOfPlaPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7375_Windows_VerifyLayoutOfPlaPage : T7375_DesktopBase
    {
        public T7375_Windows_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7375 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7375_Mac_VerifyLayoutOfPlaPage : T7375_DesktopBase
    {
        public T7375_Mac_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7375 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7375_iPad_VerifyLayoutOfPlaPage : T7375_DesktopBase
    {
        public T7375_iPad_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7375 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7375_TabletEmulator_VerifyLayoutOfPlaPage : T7375_DesktopBase
    {
        public T7375_TabletEmulator_VerifyLayoutOfPlaPage(ITestOutputHelper output, SharedSku_Fixture_T7375 fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPlaPage(string config) => Validate(Validate, config);
    }


    public class SharedSku_Fixture_T7375 : FixtureBase
    {
        public string PlaShortSku { get; }
        public List<Dictionary<string, string>> Url { get; }

        public SharedSku_Fixture_T7375()
        {
            PlaShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Url = SortActions.GetSortWithNoActiveAbTest();
        }
    }


    /// <summary>
    /// Verify the layout of the PLA page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7514
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7375
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7514"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7375")]
    public abstract class T7375_DesktopBase : VisualTestsBaseDesktop, IClassFixture<SharedSku_Fixture_T7375>
    {
        protected readonly SharedSku_Fixture_T7375 Fixture;

        protected T7375_DesktopBase(ITestOutputHelper output, SharedSku_Fixture_T7375 fixture) : base(output, fixture)
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
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), Sort.IgnoreSortResultProduct(), Sort.IgnoreSortPageFilterContainer() });
        }
    }
}

