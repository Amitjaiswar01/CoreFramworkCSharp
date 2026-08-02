using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7333_VerifyLayoutOfPrintWhenSelectingSmallImages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7333_Windows_VerifyLayoutWidgetHomepage : T7333_DesktopBase
    {
        public T7333_Windows_VerifyLayoutWidgetHomepage(ITestOutputHelper output, T7333_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfPrintWhenSelectingSmallImage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7333_Mac_VerifyLayoutOfPrintWhenSelectingSmallImages : T7333_DesktopBase
    {
        public T7333_Mac_VerifyLayoutOfPrintWhenSelectingSmallImages(ITestOutputHelper output, T7333_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfPrintWhenSelectingSmallImage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7333_iPad_VerifyLayoutOfPrintWhenSelectingSmallImages : T7333_DesktopBase
    {
        public T7333_iPad_VerifyLayoutOfPrintWhenSelectingSmallImages(ITestOutputHelper output, T7333_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfPrintWhenSelectingSmallImage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7333_TabletEmulator_VerifyLayoutOfPrintWhenSelectingSmallImages : T7333_DesktopBase
    {
        public T7333_TabletEmulator_VerifyLayoutOfPrintWhenSelectingSmallImages(ITestOutputHelper output, T7333_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfPrintWhenSelectingSmallImage(string config) => Validate(Validate, config);
    }


    public class T7333_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public T7333_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuOnClearance;
        }
    }


    /// <summary>
    /// Verify Layout Of Print When Selecting Small Images
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9791
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7333
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9791"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7333")]
    public abstract class T7333_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7333_SharedProductSku_Fixture>
    {
        protected readonly T7333_SharedProductSku_Fixture Fixture;

        protected T7333_DesktopBase(ITestOutputHelper output, T7333_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /* Arrange
            Get clearance sku from the database
            Empty the cart before adding it into cart
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetShortSkuOnClearance()");

            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            /* Act:
            Click on Print Link
            Uncheck all the checkboxes
            Ensure Small Image radio btn is selected
            */
            Cart.SelectPrintButton();
            Cart.SelectNoneLinkUnderPrintYourCartModal();
            Cart.SmallImagesRadioButton();

            // Act: Capture the screenshot of the modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            // Act: Click the Print Cart button
            Cart.SelectModalPrintCartBtn();

            // Act: Capture the screenshot of the modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}