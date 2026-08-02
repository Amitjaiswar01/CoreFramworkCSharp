using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7919_Windows_VerifyTheLayoutOfIdeasAndAdvicePage : T7919_DesktopBase
    {
        public T7919_Windows_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7919_Mac_VerifyTheLayoutOfIdeasAndAdvicePage : T7919_DesktopBase
    {
        public T7919_Mac_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7919_iPad_VerifyTheLayoutOfIdeasAndAdvicePage : T7919_DesktopBase
    {
        public T7919_iPad_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7919_TabletEmulator_VerifyTheLayoutOfIdeasAndAdvicePage : T7919_DesktopBase
    {
        public T7919_TabletEmulator_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7912_iPhone_VerifyTheLayoutOfIdeasAndAdvicePage : T7912_MobileBase
    {
        public T7912_iPhone_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7912_AndroidPhone_VerifyTheLayoutOfIdeasAndAdvicePage : T7912_MobileBase
    {
        public T7912_AndroidPhone_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7912_Emulator_VerifyTheLayoutOfIdeasAndAdvicePage : T7912_MobileBase
    {
        public T7912_Emulator_VerifyTheLayoutOfIdeasAndAdvicePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfIdeasAndAdvicePage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Ideas and Advice Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7919
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7919")]
    public abstract class T7919_DesktopBase : T7919_T7912_Base
    {
        protected T7919_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void NavigateToIdeasAndAdvice()
        {
            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            HeaderFooter.FooterAdviceAndTipsLink.Click();
        }
    }


    /// <summary>
    /// Verify the Layout of Ideas and Advice Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7912
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7912")]
    public abstract class T7912_MobileBase : T7919_T7912_Base
    {
        protected T7912_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void NavigateToIdeasAndAdvice()
        {
            Browser.Navigate(Urls.IdeasAdviceUrlProd);
        }
    }


    public abstract class T7919_T7912_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7919_T7912_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            NavigateToIdeasAndAdvice();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, false, true);
        }

        protected abstract void NavigateToIdeasAndAdvice();
    }
}
