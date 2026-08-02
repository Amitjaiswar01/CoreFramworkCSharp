using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7461_Windows_VerifyLayoutBrandLogoShopAllBrand : T7461_DesktopBase
    {
        public T7461_Windows_VerifyLayoutBrandLogoShopAllBrand(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutBrandLogoShopAllBrand(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7461_Mac_VerifyLayoutBrandLogoShopAllBrand : T7461_DesktopBase
    {
        public T7461_Mac_VerifyLayoutBrandLogoShopAllBrand(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutBrandLogoShopAllBrand(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7461_iPad_VerifyLayoutBrandLogoShopAllBrand : T7461_DesktopBase
    {
        public T7461_iPad_VerifyLayoutBrandLogoShopAllBrand(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutBrandLogoShopAllBrand(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7461_TabletEmulator_VerifyLayoutBrandLogoShopAllBrand : T7461_DesktopBase
    {
        public T7461_TabletEmulator_VerifyLayoutBrandLogoShopAllBrand(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutBrandLogoShopAllBrand(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Brand Logo and 'Shop all brand' links on the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8035
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7461
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8035"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7461")]
    public abstract class T7461_DesktopBase : T7461_T7462_Base
    {
        protected T7461_DesktopBase(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture) { }
    }


    public class T7461_T7462_SharedFixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7461_T7462_SharedFixture()
        {
            ShortSku = ProductActions.GetRandomBrandInfo().ShortSku;
        }
    }


    public abstract class T7461_T7462_Base : VisualTestsBase, IClassFixture<T7461_T7462_SharedFixture>
    {
        protected readonly T7461_T7462_SharedFixture Fixture;

        protected T7461_T7462_Base(ITestOutputHelper output, T7461_T7462_SharedFixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetRandomBrandInfo().ShortSku");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.ForDomReady();

            Browser.Wait.ForDisplayedElement(ProductDetail.ManufacturerLinkAnchor);
            Browser.ScrollIntoView(ProductDetail.ManufacturerLinkAnchor, true);

            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheck }, true);
        }
    }
}
