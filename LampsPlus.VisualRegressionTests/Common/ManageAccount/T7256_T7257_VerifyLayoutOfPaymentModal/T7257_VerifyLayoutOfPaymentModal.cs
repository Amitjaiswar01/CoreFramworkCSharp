using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7256_T7257_VerifyLayoutOfPaymentModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7257_iPhone_VerifyPaymentOptionsLayout : T7257_MobileBase
    {
        public T7257_iPhone_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7257_AndroidPhone_VerifyPaymentOptionsLayout : T7257_MobileBase
    {
        public T7257_AndroidPhone_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7257_Emulator_VerifyPaymentOptionsLayout : T7257_MobileBase
    {
        public T7257_Emulator_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Payment modal and Payment Options page after editing and deleting payment methods.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9773
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7257
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9773"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7257")]
    public abstract class T7257_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7257_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has 2 saved Payment methods.
            InitializeVisualTest(config, Urls.ManagePaymentOptionsPageUrl);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);
            Address.FirstName = CustomerAddressInformation.GetPaymentName();
            Address.LastName = CustomerAddressInformation.GetPaymentName();
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestMasterCard);

            //Act: Click the Edit link for the Default payment method.
            ManageAccount.OpenEditPaymentModal();

            //Act: Capture a screenshot of the entire screen.
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, ManageAccount.GetPaymentScrollableOverlay(), true, true, new List<IElement> { ManageAccount.IgnoreFirstNameElement(), ManageAccount.IgnoreLastNameElement(), ManageAccount.IgnoreAddress2Element() }, ManageAccount.IgnoreAddress2Element(), 45);

            //Act: Click the SAVE button in the Edit Payment modal.
            ManageAccount.ClosePaymentModal();

            //Act: Capture a screenshot of the entire screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);

            //Act: Click the Remove link for the second payment option (non-default).
            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();

            //Act: Capture a screenshot of the entire screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);
        }
    }
}
