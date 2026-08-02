using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.ContactUs
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7757_Windows_VerifyLayoutOfContactUsPageAndEmailModal : T7757_DesktopBase
    {
        public T7757_Windows_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7757_Mac_VerifyLayoutOfContactUsPageAndEmailModal : T7757_DesktopBase
    {
        public T7757_Mac_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7757_iPad_VerifyLayoutOfContactUsPageAndEmailModal : T7757_DesktopBase
    {
        public T7757_iPad_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7757_TabletEmulator_VerifyLayoutOfContactUsPageAndEmailModal : T7757_DesktopBase
    {
        public T7757_TabletEmulator_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7758_iPhone_VerifyLayoutOfContactUsPageAndEmailModal : T7758_MobileBase
    {
        public T7758_iPhone_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7758_Android_VerifyLayoutOfContactUsPageAndEmailModal : T7758_MobileBase
    {
        public T7758_Android_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7758_Emulator_VerifyLayoutOfContactUsPageAndEmailModal : T7758_MobileBase
    {
        public T7758_Emulator_VerifyLayoutOfContactUsPageAndEmailModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfTheContactUsPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Contact Us page and Email modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9104
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7757
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9104"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7757")]
    public abstract class T7757_DesktopBase : T7757_T7758_Base
    {
        protected T7757_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ContactUs.ContactEmailClass.ToCssClassSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }

        protected override void TakeModalScreenshot()
        {
            ContactUs.EmailUsButton.Click();

            Browser.Wait.ForDisplayedElement(ContactUs.SendEmailButtonModal);

            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, ContactUs.EmailUsModal);
        }
    }


    /// <summary>
    /// Verify the layout of the Contact Us page and Email modal.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9104
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7758
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9104"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7758")]
    public abstract class T7758_MobileBase : T7757_T7758_Base
    {
        protected T7758_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(ContactUs.ContactConnectClass));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
        protected override void TakeModalScreenshot() { }
    }


    public abstract class T7757_T7758_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7757_T7758_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.ContactUsPageUrl);

            TakeScreenshot();

            TakeModalScreenshot();
        }
        protected abstract void TakeScreenshot();

        protected abstract void TakeModalScreenshot();
    }
}