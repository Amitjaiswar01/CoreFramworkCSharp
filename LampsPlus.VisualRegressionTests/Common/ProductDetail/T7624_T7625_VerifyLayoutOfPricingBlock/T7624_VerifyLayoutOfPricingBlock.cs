using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7624_T7625_VerifyLayoutOfPricingBlock
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7624_Windows_VerifyLayoutOfPricingBlockOnPdp : T7624_DesktopBase
    {
        public T7624_Windows_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7624_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7624_Mac_VerifyLayoutOfPricingBlockOnPdp : T7624_DesktopBase
    {
        public T7624_Mac_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7624_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7624_iPad_VerifyLayoutOfPricingBlockOnPdp : T7624_DesktopBase
    {
        public T7624_iPad_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7624_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7624_TabletEmulator_VerifyLayoutOfPricingBlockOnPdp : T7624_DesktopBase
    {
        public T7624_TabletEmulator_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7624_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    public class T7624_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7624_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuForPricingBlock;
        }
    }


    /// <summary>
    /// Verify the layout of the Pricing Block values on the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9838
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7624
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9838"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7624")]
    public abstract class T7624_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7624_SharedSku_Fixture>
    {
        protected readonly T7624_SharedSku_Fixture Fixture;

        protected T7624_DesktopBase(ITestOutputHelper output, T7624_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;

        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a viable SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuForPricingBlock");

            //Act: User has navigated to the PDP for the SKU identified in the pre-conditions
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });
        }
    }
}
