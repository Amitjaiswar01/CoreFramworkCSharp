using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7252_T7253_VerifyLayoutOfChangeEmailModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7252_Windows_VerifyTheLayoutOfChangeEmailPrefModal : T7252_DesktopBase
    {
        public T7252_Windows_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7252_Mac_VerifyTheLayoutOfChangeEmailPrefModal : T7252_DesktopBase
    {
        public T7252_Mac_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]

        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7252_iPad_VerifyTheLayoutOfChangeEmailPrefModal : T7252_DesktopBase
    {
        public T7252_iPad_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7252_TabletEmulator_VerifyTheLayoutOfChangeEmailPrefModal : T7252_DesktopBase
    {
        public T7252_TabletEmulator_VerifyTheLayoutOfChangeEmailPrefModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfEmailPrefModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences modal and the Thank you message on it.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9771
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7252
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9771"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7252")]
    public abstract class T7252_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7252_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page: https://www.lampsplus.com/account/profile/
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            //Act: Click the Email Preferences link on the Manage Account page.
            ManageAccount.OpenEmailPreferencesModal();

            //Act: Capture a screenshot of the Change Email Preferences modal element.
            ScreenCapturer.CaptureElementAreaWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { ManageAccount.IgnoreRadioButtons() });

            //Act: Click the SAVE button.
            ManageAccount.SaveEmailPreferences();

            //Act: Capture a screenshot of the Change Email Preferences modal element.
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { ManageAccount.IgnoreRadioButtons() });
        }
    }
}