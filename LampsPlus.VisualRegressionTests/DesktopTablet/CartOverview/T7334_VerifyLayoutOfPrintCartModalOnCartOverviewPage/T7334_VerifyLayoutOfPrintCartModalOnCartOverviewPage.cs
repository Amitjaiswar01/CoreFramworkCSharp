using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7334_VerifyLayoutOfPrintCartModalOnCartOverviewPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7334_Windows_VerifyLayoutOfPrintCartModalOnCartOverviewPage : T7334_DesktopBase
    {
        public T7334_Windows_VerifyLayoutOfPrintCartModalOnCartOverviewPage(ITestOutputHelper output, T7334_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfPrintCartModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7334_Mac_VerifyLayoutOfPrintCartModalOnCartOverviewPage : T7334_DesktopBase
    {
        public T7334_Mac_VerifyLayoutOfPrintCartModalOnCartOverviewPage(ITestOutputHelper output, T7334_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfPrintCartModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7334_iPad_VerifyLayoutOfPrintCartModalOnCartOverviewPage : T7334_DesktopBase
    {
        public T7334_iPad_VerifyLayoutOfPrintCartModalOnCartOverviewPage(ITestOutputHelper output, T7334_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfPrintCartModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7334_TabletEmulator_VerifyLayoutOfPrintCartModalOnCartOverviewPage : T7334_DesktopBase
    {
        public T7334_TabletEmulator_VerifyLayoutOfPrintCartModalOnCartOverviewPage(ITestOutputHelper output, T7334_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfPrintCartModalOnCartOverviewPage(string config) => Validate(Validate, config);
    }


    public class T7334_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7334_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Print Cart modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9784
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7334
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9784"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7334")]
    public abstract class T7334_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7334_SharedProductSku_Fixture>
    {
        protected readonly T7334_SharedProductSku_Fixture Fixture;

        protected T7334_DesktopBase(ITestOutputHelper output, T7334_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrangement:
            User is signed in as a employee
            User has cleared a existing cart
            User has added item to cart
            User is on the Cart page
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductionActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            /*Act:
             User clicks on Print link
             User selects large images
            */
            Cart.SelectPrintButton();
            Cart.SelectLargeImageOnPrintModal();

            //Act: Capture screenshot of Print Your Cart Modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModalContent());

            //Act: User clicks Print Button 
            Cart.SelectModalPrintCartBtn();

            //Act: Capture screenshot of modal
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, Modal.GetLpModalContent());
        }
    }
}
