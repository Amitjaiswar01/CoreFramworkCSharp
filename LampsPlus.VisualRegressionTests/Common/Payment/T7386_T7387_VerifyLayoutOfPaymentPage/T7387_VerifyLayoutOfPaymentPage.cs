using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7386_T7387_VerifyLayoutOfPaymentPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7387_iPhone_VerifyTheLayoutOfPaymentPage : T7387_MobileBase
    {
        public T7387_iPhone_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7387_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7387_AndroidPhone_VerifyTheLayoutOfPaymentPage : T7387_MobileBase
    {
        public T7387_AndroidPhone_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7387_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7387_Emulator_VerifyTheLayoutOfPaymentPage : T7387_MobileBase
    {
        public T7387_Emulator_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7387_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    public class T7387_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7387_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }

    /// <summary>
    /// Verify the layout of the Payment Page with saved payment options and the Credit Card Information modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7521
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7387
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7521"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7387")]
    public abstract class T7387_MobileBase : VisualTestsBaseMobile, IClassFixture<T7387_Fixture>
    {
        protected readonly T7387_Fixture Fixture;

        protected T7387_MobileBase(ITestOutputHelper output, T7387_Fixture fixture) : base(output, fixture)
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
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act. On the Payment page, click the 'Details' link for the Saved Payment
            Payment.ClickOnPaymentDetailsLink();

            //Capture a screenshot of the modal element
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, Payment.GetEditPaymentDetails());
        }
    }
}