using System.Collections.Generic;
using Automation.Framework;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7252_T7253_VerifyLayoutOfChangeEmailModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7253_iPhone_VerifyTheLayoutOfChangeEmailPrefModal : T7253_MobileBase
    {
        public T7253_iPhone_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7253_AndroidPhone_VerifyTheLayoutOfChangeEmailPrefModal : T7253_MobileBase
    {
        public T7253_AndroidPhone_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7253_Emulator_VerifyTheLayoutOfChangeEmailPrefModal : T7253_MobileBase
    {
        public T7253_Emulator_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences modal and the Thank you message on it.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7245
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7253
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7245"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7253")]
    public abstract class T7253_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7253_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page: https://www.lampsplus.com/account/profile/
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            //Act: Click the Email Preferences link on the Manage Account page.
            ManageAccount.OpenEmailPreferencesModal();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Click the SAVE button.
            ManageAccount.SaveEmailPreferences();

            //Act: Capture a screenshot of the visual screen.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ManageAccount.IgnoreSaveEmailPrefButton() }, floating: ManageAccount.IgnoreSaveEmailPrefButton(), maxDownOffset: 20);
        }
    }
}
