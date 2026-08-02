using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7899_T7898_VerifyLayoutOfShipInModalOnPDP
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7899_Window_VerifyLayoutOfShipInModal : T7899_DesktopBase
    {
        public T7899_Window_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7899_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfShipInModal(string config) => Validate(Validate, config);
    }


    public class T7899_Mac_VerifyLayoutOfShipInModal : T7899_DesktopBase
    {
        public T7899_Mac_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7899_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfShipInModal(string config) => Validate(Validate, config);
    }


    public class T7899_Ipad_VerifyLayoutOfShipInModal : T7899_DesktopBase
    {
        public T7899_Ipad_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7899_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfShipInModal(string config) => Validate(Validate, config);
    }


    public class T7899_TabletEmulator_VerifyLayoutOfShipInModal : T7899_DesktopBase
    {
        public T7899_TabletEmulator_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7899_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfShipInModal(string config) => Validate(Validate, config);
    }


    public class T7899_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7899_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithShipInOption;
        }
    }


    /// <summary>
    /// Verify the layout of the Ships In Modal on the PDP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10370
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7899
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10370"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7899")]
    public abstract class T7899_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7899_SharedSkus_Fixture>
    {
        protected readonly T7899_SharedSkus_Fixture Fixture;

        protected T7899_DesktopBase(ITestOutputHelper output, T7899_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange : Find the sku that has Ship in option
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuThatQualifiesForReviews");

            // Act : Navigate to the PDP by shortSku. and Open the ship in modal
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");
            ProductDetail.OpenShipInModal();

            // Act : Capture the screenshot of the visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}