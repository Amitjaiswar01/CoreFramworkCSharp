using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Payment
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7390_Window_VerifyPaymentOptionsLayoutForEmployee : T7390_DesktopBase
    {
        public T7390_Window_VerifyPaymentOptionsLayoutForEmployee(ITestOutputHelper output, T7390_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyPaymentOptionsLayoutForEmployee(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7390_Mac_VerifyPaymentOptionsLayoutForEmployee : T7390_DesktopBase
    {
        public T7390_Mac_VerifyPaymentOptionsLayoutForEmployee(ITestOutputHelper output, T7390_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyPaymentOptionsLayoutForEmployee(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7390_iPad_VerifyPaymentOptionsLayoutForEmployee : T7390_DesktopBase
    {
        public T7390_iPad_VerifyPaymentOptionsLayoutForEmployee(ITestOutputHelper output, T7390_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyPaymentOptionsLayoutForEmployee(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7390_TabletEmulator_VerifyPaymentOptionsLayoutForEmployee : T7390_DesktopBase
    {
        public T7390_TabletEmulator_VerifyPaymentOptionsLayoutForEmployee(ITestOutputHelper output, T7390_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyPaymentOptionsLayoutForEmployee(string config) => Validate(Validate, config);
    }


    public class T7390_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7390_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the available payment options for an Employee on the Payment page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7523
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7390
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7523"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7390")]
    public abstract class T7390_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7390_SharedSku_Fixture>
    {
        protected readonly T7390_SharedSku_Fixture Fixture;

        protected T7390_DesktopBase(ITestOutputHelper output, T7390_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            /* Arrange:
            User has identified a SKU from the query and added product to the cart.
            User has proceeded to the Shipping page 
            */
            CookieUtility.ExitStoreInSessionMode();
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetAnySkuWithProductDetailPage()");
            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            CsrBlock.SetSaleSourceValue();
            ShoppingCartWorkflow.ProceedToShippingPage();

            /* Act:
            Fill out the Shipping Information form
            Proceed to the Payment page
            Capture a screenshot of the visible screen
            */
            CustomerAddressInformation.EnterShippingAddress(Address);
            Shipping.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "User is not on Payment page.");
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act:
            Click on the Wire Transfer radio button
            Capture a screenshot of the visible screen
            */
            Payment.EnableWireTransfer();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act:
            Click on the P.O. radio button
            Capture a screenshot of the visible screen
            */
            Payment.EnablePurchaseOrder();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act:
            Click on the Check radio button
            Capture a screenshot of the visible screen
            */
            Payment.EnablePaperCheck();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
