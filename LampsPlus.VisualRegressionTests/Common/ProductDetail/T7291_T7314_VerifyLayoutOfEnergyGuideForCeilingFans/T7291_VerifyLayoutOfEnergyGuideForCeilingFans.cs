using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7291_T7314_VerifyLayoutOfEnergyGuideForCeilingFans
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7291_Windows_VerifyLayoutOfEnergyGuideForCeilingFans : T7291_DesktopBase
    {
        public T7291_Windows_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7291_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7291_Mac_VerifyLayoutOfEnergyGuideForCeilingFans : T7291_DesktopBase
    {
        public T7291_Mac_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7291_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7291_iPad_VerifyLayoutOfEnergyGuideForCeilingFans : T7291_DesktopBase
    {
        public T7291_iPad_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7291_SharedSku_Fixture fixture) : base(output, fixture) { }


        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7291_TabletEmulator_VerifyLayoutOfEnergyGuideForCeilingFans : T7291_DesktopBase
    {
        public T7291_TabletEmulator_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7291_SharedSku_Fixture fixture) : base(output, fixture) { }


        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    public class T7291_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7291_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFanWithEnergyGuideIconShortSku;
        }
    }


    /// <summary>
    /// Verify the layout of the Energy Guide for Ceiling Fans.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9847
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7291
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9847"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7291")]
    public abstract class T7291_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7291_SharedSku_Fixture>
    {
        protected readonly T7291_SharedSku_Fixture Fixture;

        protected T7291_DesktopBase(ITestOutputHelper output, T7291_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a ceiling fan that has an energy guide
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetFanWithEnergyGuideIconShortSku().ShortSku");

            //Act: Take the SKU from the query in the pre-conditions and enter it into the 'Search' field on the Lamps Plus site and execute the search.
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Scroll to the Fan Features section and capture a screen shot of the visible screen.
            ProductDetail.FocusOnFanFeaturesSection();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Click on the 'Energy Guide' icon.
            ProductDetail.OpenEnergyGuide();

            //Act: Capture a screen shot of the 'Energy Guide' modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetEnergyInfoModal());
        }
    }
}
