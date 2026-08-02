using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7246_T7247_VerifyLayoutOfManageAccountPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7247_iPhone_VerifyTheLayoutOfManageAccountPage : T7247_MobileBase
    {
        public T7247_iPhone_VerifyTheLayoutOfManageAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void LayoutOfManageAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7247_AndroidPhone_VerifyTheLayoutOfManageAccountPage : T7247_MobileBase
    {
        public T7247_AndroidPhone_VerifyTheLayoutOfManageAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfManageAccountPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7247_Emulator_VerifyTheLayoutOfManageAccountPage : T7247_MobileBase
    {
        public T7247_Emulator_VerifyTheLayoutOfManageAccountPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfManageAccountPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Manage Account page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9778
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7247
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9778"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7247")]
    public abstract class T7247_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7247_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page: https://www.lampsplus.com/account/profile/
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
