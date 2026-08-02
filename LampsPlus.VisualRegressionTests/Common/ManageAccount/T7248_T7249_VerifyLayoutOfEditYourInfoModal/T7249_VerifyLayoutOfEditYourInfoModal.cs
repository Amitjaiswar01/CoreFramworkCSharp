using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7248_T7249_VerifyLayoutOfEditYourInfoModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7249_iPhone_VerifyLayoutOfEditYourInfoModal : T7249_MobileBase
    {
        public T7249_iPhone_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7249_AndroidPhone_VerifyLayoutOfEditYourInfoModal : T7249_MobileBase
    {
        public T7249_AndroidPhone_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7249_Emulator_VerifyLayoutOfEditYourInfoModal : T7249_MobileBase
    {
        public T7249_Emulator_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Your Information modal and the Thank you message on it.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9772
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7249
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9772"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7249")]
    public abstract class T7249_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7249_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page.
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            //Act: Click the Edit link in the Your Information section.
            ManageAccount.OpenYourInformationModal();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
