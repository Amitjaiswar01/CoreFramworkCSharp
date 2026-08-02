using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7917_Windows_VerifyTheLayoutOfHelpAndPoliciesPage : T7917_DesktopBase
    {
        public T7917_Windows_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7917_Mac_VerifyTheLayoutOfHelpAndPoliciesPage : T7917_DesktopBase
    {
        public T7917_Mac_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7917_iPad_VerifyTheLayoutOfHelpAndPoliciesPage : T7917_DesktopBase
    {
        public T7917_iPad_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7917_TabletEmulator_VerifyTheLayoutOfHelpAndPoliciesPage : T7917_DesktopBase
    {
        public T7917_TabletEmulator_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7910_iPhone_VerifyTheLayoutOfHelpAndPoliciesPage : T7910_MobileBase
    {
        public T7910_iPhone_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7910_AndroidPhone_VerifyTheLayoutOfHelpAndPoliciesPage : T7910_MobileBase
    {
        public T7910_AndroidPhone_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7910_Emulator_VerifyTheLayoutOfHelpAndPoliciesPage : T7910_MobileBase
    {
        public T7910_Emulator_VerifyTheLayoutOfHelpAndPoliciesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfHelpAndPoliciesPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Pros Landing Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7917
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7917")]
    public abstract class T7917_DesktopBase : T7917_T7910_Base
    {
        protected T7917_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the Layout of Pros Landing Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7910
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7910")]
    public abstract class T7910_MobileBase : T7917_T7910_Base
    {
        protected T7910_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    public abstract class T7917_T7910_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7917_T7910_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            HeaderFooter.FooterHelpLink.Click();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, false, true);
        }
    }
}