using System.Collections.Generic;
using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T8003_T8004_VerifyTheLayoutOfTheFinishOrColorRelationshipWidget
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8003_Windows_VerifyLayoutOfFinishOrColorRelationshipWidget : T8003_DesktopBase
    {
        public T8003_Windows_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8003_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8003_Mac_VerifyLayoutOfFinishOrColorRelationshipWidget : T8003_DesktopBase
    {
        public T8003_Mac_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8003_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8003_iPad_VerifyLayoutOfFinishOrColorRelationshipWidget : T8003_DesktopBase
    {
        public T8003_iPad_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8003_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8003_TabletEmulator_VerifyLayoutOfFinishOrColorRelationshipWidget : T8003_DesktopBase
    {
        public T8003_TabletEmulator_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8003_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    public class T8003_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T8003_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuForFinishAndColorRelationshipWidget;
        }
    }


    /// <summary>
    /// Verify the layout of the Finish or Color Relationship Widget
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10920
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T8003
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10920"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T8003")]
    public abstract class T8003_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T8003_SharedSkus_Fixture>
    {
        protected readonly T8003_SharedSkus_Fixture Fixture;

        protected T8003_DesktopBase(ITestOutputHelper output, T8003_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has Relationship widget
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuForFinishAndColorRelationshipWidget");

            //Act: Take one of the SKUs in the pre-conditions and enter it at the end of the URL https://www.lampsplus.com/products/.
            Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP.");

            //Act: Scroll to relationship widget and capture screenshot for visible area
            ProductDetail.GetRelationshipWidgetSection();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreMoreYouMayLikeSection() });
        }
    }
}
