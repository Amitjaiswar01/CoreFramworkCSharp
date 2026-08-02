using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7332_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7332_Windows_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages : T7332_DesktopBase
    {
        public T7332_Windows_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages(ITestOutputHelper output, T7332_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfPrintModalWhenSelectingLargeImages(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7332_Mac_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages : T7332_DesktopBase
    {
        public T7332_Mac_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages(ITestOutputHelper output, T7332_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfPrintModalWhenSelectingLargeImages(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7332_iPad_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages : T7332_DesktopBase
    {
        public T7332_iPad_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages(ITestOutputHelper output, T7332_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfPrintModalWhenSelectingLargeImages(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7332_TabletEmulator_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages : T7332_DesktopBase
    {
        public T7332_TabletEmulator_VerifyTheLayoutOfPrintModalWhenSelectingLargeImages(ITestOutputHelper output, T7332_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfPrintModalWhenSelectingLargeImages(string config) => Validate(Validate, config);
    }


    public class T7332_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7332_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuOnClearance;
        }
    }


    /// <summary>
    /// Verify the layout of the Print modal when selecting Large Images.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9790
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7332
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9790"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7332")]
    public abstract class T7332_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7332_SharedProductSku_Fixture>
    {
        protected readonly T7332_SharedProductSku_Fixture Fixture;

        protected T7332_DesktopBase(ITestOutputHelper output, T7332_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has added Clearance sku to cart
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetShortSkuOnClearance()");
            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));

            // Act: Click on print button and uncheck all the check box and select large radio button
            ShoppingCartWorkflow.LargeImageOnPrintModal();

            // Act: Capture a screenshot of the print modal element
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetIframe());

            // Act: Click on Print button 
            Cart.ClosePrintModal();

            // Act: Capture a screenshot of the print modal element
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetIframe());
        }
    }
}
