using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7254_T7255_VerifyLayoutOfPaymentOptionsPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7254_Windows_VerifyLayoutPaymentOptionsPage : T7254_DesktopBase
    {
        public T7254_Windows_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7254_Mac_VerifyLayoutPaymentOptionsPage : T7254_DesktopBase
    {
        public T7254_Mac_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7254_iPad_VerifyLayoutPaymentOptionsPage : T7254_DesktopBase
    {
        public T7254_iPad_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7254_TabletEmulator_VerifyLayoutPaymentOptionsPage : T7254_DesktopBase
    {
        public T7254_TabletEmulator_VerifyLayoutPaymentOptionsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutPaymentOptionsPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the entire Payment Options page in Manage Account.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9776
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7254
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9776"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7254")]
    public abstract class T7254_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7254_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has one saved payment method.
            InitializeVisualTest(config);
            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.PaymentOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.PaymentOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");
            ManageAccount.AddNewPaymentMethod(CreditCards.TestVisaCard, Address);

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
