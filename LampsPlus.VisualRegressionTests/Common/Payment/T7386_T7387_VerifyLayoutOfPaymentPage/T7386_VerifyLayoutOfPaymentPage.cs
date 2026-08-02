using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7386_T7387_VerifyLayoutOfPaymentPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7386_Windows_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7386_Windows_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7386_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7386_Mac_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7386_Mac_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7386_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7386_iPad_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7386_iPad_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7386_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7386_TabletEmulator_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7386_TabletEmulator_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7386_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    public class T7386_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7386_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }

    /// <summary>
    /// Verify the layout of the Payment Page with saved payment options and the Credit Card Information modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7521
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7386
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7521"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7386")]
    public abstract class DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7386_Fixture>
    {
        protected readonly T7386_Fixture Fixture;

        protected DesktopBase(ITestOutputHelper output, T7386_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrangement
             User has no saved addresses
             User has a saved payment option            
             User has identified a SKU and added it to the cart
            */
            InitializeVisualTest(config);

            var shortSku = Fixture.ShortSku;

            ShoppingCartWorkflow.EmptyCart();
            ManageAccountWorkflow.DeleteAllSavedAddresses();
            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);

            //Act. User proceeds to the Payment page
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(shortSku);

            //Capture a screenshot of the entire page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            //Act. On the Payment page, click the 'Details' link for the Saved Payment
            Payment.ClickOnPaymentDetailsLink();
            Assert.True(Modal.IsModalVisible(), "Modal element is not visible on the page.");

            //Capture a screenshot of the modal element
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, Modal.GetLpModalContent());
        }
    }
}
