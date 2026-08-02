using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7335_VerifyLayoutOfCartOverviewAndPrintForStoreInSession
{
    public class T7335_Windows_VerifyLayoutOfCartOverviewAndPrintForStoreInSession : T7335_DesktopBase
    {
        public T7335_Windows_VerifyLayoutOfCartOverviewAndPrintForStoreInSession(ITestOutputHelper output, T7335_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfCartOverviewAndPrintForStoreInSession(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7335_Mac_VerifyLayoutOfCartOverviewAndPrintForStoreInSession : T7335_DesktopBase
    {
        public T7335_Mac_VerifyLayoutOfCartOverviewAndPrintForStoreInSession(ITestOutputHelper output, T7335_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfCartOverviewAndPrintForStoreInSession(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7335_iPad_VerifyLayoutOfCartOverviewAndPrintForStoreInSession : T7335_DesktopBase
    {
        public T7335_iPad_VerifyLayoutOfCartOverviewAndPrintForStoreInSession(ITestOutputHelper output, T7335_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void LayoutOfCartOverviewAndPrintForStoreInSession(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7335_TabletEmulator_VerifyLayoutOfCartOverviewAndPrintForStoreInSession : T7335_DesktopBase
    {
        public T7335_TabletEmulator_VerifyLayoutOfCartOverviewAndPrintForStoreInSession(ITestOutputHelper output, T7335_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void LayoutOfCartOverviewAndPrintForStoreInSession(string config) => Validate(Validate, config);
    }

    
    public class T7335_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7335_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Cart Overview page and Print modal for Store in Session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9792
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7335
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9792"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7335")]

    public abstract class T7335_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7335_SharedProductSku_Fixture>
    {
        protected readonly T7335_SharedProductSku_Fixture Fixture;

        protected T7335_DesktopBase(ITestOutputHelper output, T7335_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange : Sign in as an Employee with the store in session.
             Empty Cart
            */
            var setup = new AccountConfiguration { StoreInSessionStoreNumber = "12" };

            InitializeVisualTest(config, accountConfiguration: setup);
            ShoppingCartWorkflow.EmptyCart();

            //Act: Add Item to Cart.
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage");
            
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = sku });
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act: Take Screenshot of Cart Page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() });

            //Act: Click Pos CheckBox for item 
            Cart.CheckPosBox();

            //Act: Take Screenshot of visible screen.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() });

            //Act: Hover Over on Checkout Now button
            Browser.MouseOverJScript(Cart.GetCheckOutNowButton());

            //Act: Take Screenshot of visible screen.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() });

            //Act: Click Print link
            Cart.SelectPrintButton();

            //Act: Capture screenshot of Print Your Cart Modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModalContent());

            //Act: Click on the 'Print In-Store' button.
            Cart.SelectPrintInStoreButton();

            //Act: Capture screenshot of Print Your Cart Modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModalContent());

            //Act: Close Print Modal
            Modal.CloseLpModal();

            //Act: UNCHECK the POS checkbox
            Cart.CheckPosBox();

            //Act: Take Screenshot of Cart Page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() });
        }
    }
}
