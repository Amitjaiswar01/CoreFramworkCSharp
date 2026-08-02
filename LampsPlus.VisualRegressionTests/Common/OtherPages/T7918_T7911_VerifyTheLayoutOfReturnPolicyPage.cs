using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7918_Windows_VerifyTheLayoutOfReturnPolicyPage : T7918_DesktopBase
    {
        public T7918_Windows_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7918_Mac_VerifyTheLayoutOfReturnPolicyPage : T7918_DesktopBase
    {
        public T7918_Mac_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7918_iPad_VerifyTheLayoutOfReturnPolicyPage : T7918_DesktopBase
    {
        public T7918_iPad_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7918_TabletEmulator_VerifyTheLayoutOfReturnPolicyPage : T7918_DesktopBase
    {
        public T7918_TabletEmulator_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7911_iPhone_VerifyTheLayoutOfReturnPolicyPage : T7911_MobileBase
    {
        public T7911_iPhone_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7911_AndroidPhone_VerifyTheLayoutOfReturnPolicyPage : T7911_MobileBase
    {
        public T7911_AndroidPhone_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7911_Emulator_VerifyTheLayoutOfReturnPolicyPage : T7911_MobileBase
    {
        public T7911_Emulator_VerifyTheLayoutOfReturnPolicyPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfReturnPolicyPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Return Policy Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7918
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7918")]
    public abstract class T7918_DesktopBase : T7918_T7911_Base
    {
        protected T7918_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the Layout of Return Policy Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7911
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7911")]
    public abstract class T7911_MobileBase : T7918_T7911_Base
    {
        protected T7911_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    public abstract class T7918_T7911_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7918_T7911_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            HeaderFooter.FooterReturnPolicyLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(HeaderFooter.FooterContainerClass.ToCssClassSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true, true);
        }
    }
}