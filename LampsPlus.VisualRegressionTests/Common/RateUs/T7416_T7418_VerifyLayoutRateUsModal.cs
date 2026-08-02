using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.VisualRegressionTests.Common.RateUs
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7416_Windows_VerifyLayoutRateUsModal : T7416_DesktopBase
    {
        public T7416_Windows_VerifyLayoutRateUsModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7416_Mac_VerifyLayoutRateUsModal : T7416_DesktopBase
    {
        public T7416_Mac_VerifyLayoutRateUsModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7416_iPad_VerifyLayoutRateUsModal : T7416_DesktopBase
    {
        public T7416_iPad_VerifyLayoutRateUsModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7416_TabletEmulator_VerifyLayoutRateUsModal : T7416_DesktopBase
    {
        public T7416_TabletEmulator_VerifyLayoutRateUsModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7418_iPhone_VerifyTheLayoutOfHomePage : T7418_MobileBase
    {
        public T7418_iPhone_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7418_Emulator_VerifyTheLayoutOfHomePage : T7418_MobileBase
    {
        public T7418_Emulator_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutRateUsModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Rate Us modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7588
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7416
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7588"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7416")]
    public abstract class T7416_DesktopBase : T7416_T7418_Base
    {
        protected T7416_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void CaptureRateUsModal()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Browser.Locate.ElementById(GlobalLocators.LpModalId));
        }

        protected override void CaptureRateUsThankYouModal()
        {
            Browser.Wait.ForDisplayedElement(HeaderFooter.RateUsConfirmationPage, 5);
            CaptureRateUsModal();
        }
    }


    /// <summary>
    /// Verify the layout of the Rate Us modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7588
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T418
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7588"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7418")]
    public abstract class T7418_MobileBase : T7416_T7418_Base
    {
        protected T7418_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void CaptureRateUsModal()
        {
            Browser.Wait.ForPageWait(Urls.RateUsUrl);
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }

        protected override void CaptureRateUsThankYouModal()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(HeaderFooter.RateUsConfirmationPageId.ToCssIdSelector()));
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, HeaderFooter.RateUsConfirmationPage);
        }
    }

    
    public abstract class T7416_T7418_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7416_T7418_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config, Urls.HomePageUrl);     

            Browser.ScrollToBottomOfPage(Urls.HomePageUrl);

            Browser.Wait.ForDisplayedElement(HeaderFooter.RateUs);
            HeaderFooter.RateUs.Click();

            CaptureRateUsModal();

            HeaderFooter.RateUsStarsFifthStarElement.Click();
            HeaderFooter.RateUsComment.Click();
            HeaderFooter.RateUsComment.SendKeys("LPQA test");

            HeaderFooter.SubmitRatingBtn.Click();
            Browser.Wait.ForDomReady();

            CaptureRateUsThankYouModal();
        }

        protected abstract void CaptureRateUsModal();

        protected abstract void CaptureRateUsThankYouModal();
    }
}
