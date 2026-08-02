using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7256_T7257_VerifyLayoutOfPaymentModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7256_Windows_VerifyPaymentOptionsLayout : T7256_DesktopBase
    {
        public T7256_Windows_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7256_Mac_VerifyPaymentOptionsLayout : T7256_DesktopBase
    {
        public T7256_Mac_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7256_iPad_VerifyPaymentOptionsLayout : T7256_DesktopBase
    {
        public T7256_iPad_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7256_TabletEmulator_VerifyPaymentOptionsLayout : T7256_DesktopBase
    {
        public T7256_TabletEmulator_VerifyPaymentOptionsLayout(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void PaymentOptionsLayout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Payment modal and Payment Options page after editing and deleting payment methods.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9773
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7256
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9773"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7256")]
    public abstract class T7256_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7256_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has 2 saved Payment methods.
            InitializeVisualTest(config);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);
            Address.FirstName = CustomerAddressInformation.GetPaymentName();
            Address.LastName = CustomerAddressInformation.GetPaymentName();
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestMasterCard);

            //Act: Click the Edit link for the Default payment method.
            ManageAccount.OpenEditPaymentModal();

            //Act: Capture a screenshot of the Edit Payment modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            //Act: Click the SAVE button in the Edit Payment modal.
            ManageAccount.ClosePaymentModal();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ManageAccount.IgnoreRecentlyViewedWidgetContainer() });

            //Act: Click the Remove link for the second payment option (non-default).
            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ManageAccount.IgnoreRecentlyViewedWidgetContainer() });
        }
    }
}
