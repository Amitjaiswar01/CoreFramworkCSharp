using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7344_VerifyLayoutOfErrorMessageAndDeletingCart
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7344_Window_VerifyLayoutOfErrorMessageAndDeletingCart : T7344_DesktopBase
    {
        public T7344_Window_VerifyLayoutOfErrorMessageAndDeletingCart(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfErrorMessageAndDeletingCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7344_Mac_VerifyLayoutOfErrorMessageAndDeletingCart : T7344_DesktopBase
    {
        public T7344_Mac_VerifyLayoutOfErrorMessageAndDeletingCart(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfErrorMessageAndDeletingCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7344_iPad_VerifyLayoutOfErrorMessageAndDeletingCart : T7344_DesktopBase
    {
        public T7344_iPad_VerifyLayoutOfErrorMessageAndDeletingCart(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfErrorMessageAndDeletingCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7344_TabletEmulator_VerifyLayoutOfErrorMessageAndDeletingCart : T7344_DesktopBase
    {
        public T7344_TabletEmulator_VerifyLayoutOfErrorMessageAndDeletingCart(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfErrorMessageAndDeletingCart(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the error message for adding SKU 99999 and deleting the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9793
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7344
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9793"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7344")]
    public abstract class T7344_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7344_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrangement: Login as Employee and No Item in Cart
            InitializeVisualTest(config, useEmployeeManagerAccount: true);

            ShoppingCartWorkflow.EmptyCart();

            /*Act
            Navigate to Cart Page.
            In the Add by Style# field, type in 99999 and hit enter.
            */
            Cart.Navigate();
            Cart.EnterSkuInAddByStyle(Cart.GetInvalidShortSku());

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            /*Act
            Navigate to Sort Page.
            Add any item to cart.
            On the Cart Page, Click the Delete link.
            */
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.AllChandeliersSortPageUrl, 1);

            Cart.OpenDeleteCartModal();

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}