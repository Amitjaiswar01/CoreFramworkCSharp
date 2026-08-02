using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderConfirmation.T7588_T7589_VerifyLayoutOfPagesForPhysicalGiftCard
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7588_Windows_VerifyLayoutOfPagesForPhysicalGiftCard : T7588_DesktopBase
    {
        public T7588_Windows_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7588_Mac_VerifyLayoutOfPagesForPhysicalGiftCard : T7588_DesktopBase
    {
        public T7588_Mac_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7588_iPad_VerifyLayoutOfPagesForPhysicalGiftCard : T7588_DesktopBase
    {
        public T7588_iPad_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7588_TabletEmulator_VerifyLayoutOfPagesForPhysicalGiftCard : T7588_DesktopBase
    {
        public T7588_TabletEmulator_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of the Pages for a Physical Gift Card
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9809
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7588
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9809"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7588")]
    public abstract class T7588_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7588_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User has Navigated to the Gift Card Page
            InitializeVisualTest(config);
            Browser.Navigate(Urls.GiftCardLandingPageUrl);
            Browser.Wait.ForDomReady();

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);

            //Act : Click on "Shop Now" Button to Navigate to Gift Card PDP
            Sort.NavigateToGiftCardPdp();
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP");

            //Act : Fill Out the Form on PDP
            ProductDetail.AddGiftCardDetails("LPQA Test");

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);

            //Act : Add Gift Card to the Cart 
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on Cart Page");

            //Act : Take Screenshot of the Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), false, true, Cart.GetMoreYouMayLike(), 20);
        }
    }
}
