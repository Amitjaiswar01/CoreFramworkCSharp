using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.Homepage.T7943_VerifyTheLayoutOfChatModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7943_iPhone_VerifyTheLayoutOfChatModal : T7943_MobileBase
    {
        public T7943_iPhone_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7943_Android_VerifyTheLayoutOfChatModal : T7943_MobileBase
    {
        public T7943_Android_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7943_Emulator_VerifyTheLayoutOfChatModal : T7943_MobileBase
    {
        public T7943_Emulator_VerifyTheLayoutOfChatModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfChatModalOnHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of "Chat" Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10664
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7943
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10664"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7943")]
    public abstract class T7943_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7943_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User on the Lamps Plus homepage.
            InitializeVisualTest(config);

            Browser.Navigate(Urls.HomePageUrl);

            // Act : Check if test running in chat hour or not
            var headerChatOption = ProductDetail.IsChatIconEnabled();
                
            // Act : Capture screenshot of the modal
            if (headerChatOption)
            {
                HeaderFooter.OpenFooterChatModal();
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
