using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7254_T7255_VerifyLayoutOfPaymentOptionsPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7255_iPhone_VerifyLayoutPaymentOptionsPage : T7255_MobileBase
    {
        public T7255_iPhone_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7255_AndroidPhone_VerifyLayoutPaymentOptionsPage : T7255_MobileBase
    {
        public T7255_AndroidPhone_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7255_Emulator_VerifyLayoutPaymentOptionsPage : T7255_MobileBase
    {
        public T7255_Emulator_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the entire Payment Options page in Manage Account.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9776
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7255
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9776"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7255")]
    public abstract class T7255_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7255_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has one saved payment method.
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.PaymentOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.PaymentOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");
            ManageAccount.AddNewPaymentMethod(CreditCards.TestVisaCard, Address);

            //Act: Capture a screenshot of the visual page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
