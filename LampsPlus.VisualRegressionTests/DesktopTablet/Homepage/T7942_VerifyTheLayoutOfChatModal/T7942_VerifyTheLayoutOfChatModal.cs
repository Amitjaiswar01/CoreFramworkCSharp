using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Homepage.T7942_VerifyLayoutOfChatModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_Windows_VerifyTheLayoutOfChatModal : T7942_DesktopBase
    {
        public T7942_Windows_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture){ }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_Windows_VerifyTheLayoutOfChatModalPros : T7942_DesktopBase
    {
        public T7942_Windows_VerifyTheLayoutOfChatModalPros(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_Mac_VerifyTheLayoutOfChatModal : T7942_DesktopBase
    {
        public T7942_Mac_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture){ }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_Mac_VerifyTheLayoutOfChatModalPros : T7942_DesktopBase
    {
        public T7942_Mac_VerifyTheLayoutOfChatModalPros(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_iPad_VerifyTheLayoutOfChatModal : T7942_DesktopBase
    {
        public T7942_iPad_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7942_TabletEmulator_VerifyTheLayoutOfChatModal : T7942_DesktopBase
    {
        public T7942_TabletEmulator_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of "Chat" Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10664
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7942
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10664"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7942")]
    public abstract class T7942_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7942_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            // Arrangement : Navigate to homepage
            InitializeVisualTest(config);

            // Act : Check if test running in chat hour or not
            var headerChatOption = ProductDetail.IsChatIconEnabled();

            // Act : capture screenshot of the modal
            if (headerChatOption)
            {
                HeaderFooter.OpenHeaderChatModal();
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
                HeaderFooter.CloseChatModal();
            }
            else
            {
                Log.Message("Chat is outside business hours");
            }
        }
    }
}