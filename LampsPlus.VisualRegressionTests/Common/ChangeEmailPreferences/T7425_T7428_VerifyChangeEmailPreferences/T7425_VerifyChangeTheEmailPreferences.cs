using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
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
    public class T7425_Windows_VerifyChangeEmailPreferences : T7425_DesktopBase
    {
        public T7425_Windows_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
     public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7425_Mac_VerifyChangeEmailPreferences : T7425_DesktopBase
    {
        public T7425_Mac_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7425_iPad_VerifyChangeEmailPreferences : T7425_DesktopBase
    {
        public T7425_iPad_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7425_TabletEmulator_VerifyChangeEmailPreferences : T7425_DesktopBase
    {
        public T7425_TabletEmulator_VerifyChangeEmailPreferences(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyChangeEmailPreferences(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9797
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7425
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9797"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7425")]
    public abstract class T7425_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7425_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange : User has Navigated to Email Preference Page
            InitializeVisualTest(config);
            Email.Navigate();
            Assert.True(Email.IsCurrentPage, "User is not on Email Page");

            //Act : Capture a Screenshot of the Email Preference Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            //Act : Enter the Email ID and navigate to Sign in Page
            Email.GoToEmailPreferencesByEmail(LampsPlusAccounts.CustomerLoginAccount.UserName);
            Assert.True(SignIn.IsCurrentPage,"User is not on Sign In Page");

            //Act : Take Screenshot of the Sign In Page 
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {SignIn.GetUserNameFieldElement()});

            //Act : Enter the Password and Sign In
            SignIn.SignInWithPrefilledEmail(LampsPlusAccounts.CustomerLoginAccount.Password);
            Assert.True(Email.IsEmailPreferencesPage, "User is not on Email Preference Page");

            //Act : Take Screenshot of the Entire page 
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Email.IgnoreSubscribeAndUnsubscribeElements(), false, false, null, 0, 10, 0, 50);

            //Act : Click on Save Setting Button
            Email.SaveSettings();

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Email.IgnoreSubscribeAndUnsubscribeElements(), false, false, null, 0, 10, 0, 50);
        }
    }
}
