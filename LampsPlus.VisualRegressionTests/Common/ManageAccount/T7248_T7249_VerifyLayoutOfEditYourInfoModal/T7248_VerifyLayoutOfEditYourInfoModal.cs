using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7248_T7249_VerifyLayoutOfEditYourInfoModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7248_Windows_VerifyLayoutOfEditYourInfoModal : T7248_DesktopBase
    {
        public T7248_Windows_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7248_Mac_VerifyLayoutOfEditYourInfoModal : T7248_DesktopBase
    {
        public T7248_Mac_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7248_iPad_VerifyLayoutOfEditYourInfoModal : T7248_DesktopBase
    {
        public T7248_iPad_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7248_TabletEmulator_VerifyLayoutOfEditYourInfoModal : T7248_DesktopBase
    {
        public T7248_TabletEmulator_VerifyLayoutOfEditYourInfoModal(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfEditYourInfoModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Your Information modal and the Thank you message on it.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9772
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7248
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9772"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7248")]
    public abstract class T7248_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7248_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            //Arrange: User is on the Manage Account page.
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            //Act: Click the Edit link in the Your Information section.
            ManageAccount.OpenYourInformationModal();

            //Act: Capture a screenshot of the Edit My Information modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            /*Act:
            Edit the value in the Phone field.
            Click the SAVE button.
             */
            ManageAccount.EditAccountPhoneNumber();

            //Act: Capture a screenshot of the Edit My Information modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());

            //Data cleanup
            ManageAccount.ResetAccountPhoneNumber();
        }
    }
}
