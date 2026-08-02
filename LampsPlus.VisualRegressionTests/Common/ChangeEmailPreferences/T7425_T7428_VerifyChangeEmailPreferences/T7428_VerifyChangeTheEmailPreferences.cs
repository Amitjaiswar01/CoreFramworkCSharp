using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ChangeEmailPreferences.T7425_T7428_VerifyChangeEmailPreferences
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7428_iPhone_VerifyChangeEmailPreferences : T7428_MobileBase
    {
        public T7428_iPhone_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7428_Android_VerifyChangeEmailPreferences : T7428_MobileBase
    {
        public T7428_Android_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7428_Emulator_VerifyChangeEmailPreferences : T7428_MobileBase
    {
        public T7428_Emulator_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9797
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7428
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9797"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7428")]
    public abstract class T7428_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7428_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate Email Preference Page
            InitializeVisualTest(config);
            Email.Navigate();
            Assert.True(Email.IsCurrentPage, "User is not on Email Page");

            //Act : Select Preferences Tab
            Email.SelectPreferenceTab();

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act : Enter Email ID, click on Change Preferences Button and Navigate to Sign In Page
            Email.GoToEmailPreferencesByEmail(LampsPlusAccounts.CustomerLoginAccount.UserName);
            Assert.True(SignIn.IsCurrentPage, "User is not on Sign In Page");

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { SignIn.GetUserNameFieldElement()});

            //Act : Enter the Password and Sign In
            SignIn.SignInWithPrefilledEmail(LampsPlusAccounts.CustomerLoginAccount.Password);
            Assert.True(Email.IsEmailPreferencesPage, "User is not on Email Preference Page");

            //Act : Capture Screesnhot of the Entire Page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Email.IgnoreSubscribeAndUnsubscribeElements(), true, maxDownOffset: 10);

            //Act : Click on Update Preference Button
            Email.UpdatePreference();

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Email.IgnoreSubscribeAndUnsubscribeElements(), true, maxDownOffset: 10);
        }
    }
}
