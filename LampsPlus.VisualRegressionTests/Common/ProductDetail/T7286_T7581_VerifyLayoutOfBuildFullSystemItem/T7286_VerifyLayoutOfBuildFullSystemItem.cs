using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7286_T7581_VerifyLayoutOfBuildFullSystemItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7286_Window_VerifyLayoutOfBuildFullSystemItem : T7286_DesktopBase
    {
        public T7286_Window_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7286_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7286_Mac_VerifyLayoutOfBuildFullSystemItem : T7286_DesktopBase
    {
        public T7286_Mac_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7286_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7286_iPad_VerifyLayoutOfBuildFullSystemItem : T7286_DesktopBase
    {
        public T7286_iPad_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7286_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7286_TabletEmulator_VerifyLayoutOfBuildFullSystemItem : T7286_DesktopBase
    {
        public T7286_TabletEmulator_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7286_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    public class T7286_ShareSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7286_ShareSku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithBuildFullSystemSkus().PrimarySku;
        }
    }


    /// <summary>
    /// Verify the layout for an item with Build Full System.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9836
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7286
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9836"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7286")]

    public abstract class T7286_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7286_ShareSku_Fixture>
    {
        protected readonly T7286_ShareSku_Fixture Fixture;

        protected T7286_DesktopBase(ITestOutputHelper output, T7286_ShareSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a BFS SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetByoDimmerWithItemOptions().PrimarySku");

            //Act: Navigate to the PDP of the SKU from the pre-conditions.
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Capture a screenshot of the entire page but ignore the Ships Today verbiage.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreMoreYouMayLikeSection()}, true, true, ProductDetail.IgnoreStockCheckWrapper(), 10, 0, 10);
        }
    }
}
