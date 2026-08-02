using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7232_T7321_VerifyLayoutOfHeaderMenus
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7232_Windows_VerifyLayoutOfHeaderMenusOnHomepage : T7232_DesktopBase
    {
        public T7232_Windows_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7232_Mac_VerifyLayoutOfHeaderMenusOnHomepage : T7232_DesktopBase
    {
        public T7232_Mac_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7232_iPad_VerifyLayoutOfHeaderMenusOnHomepage : T7232_DesktopBase
    {
        public T7232_iPad_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7232_TabletEmulator_VerifyLayoutOfHeaderMenusOnHomepage : T7232_DesktopBase
    {
        public T7232_TabletEmulator_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Header menus on the Homepage appear correctly when they are open.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9800
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7232
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9800"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7232")]
    public abstract class T7232_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7232_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User on the Lamps Plus homepage.
            InitializeVisualTest(config);

            //Act: Hover over the Inspiration menu.
            HeaderFooter.OpenInspirationMenu();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Hover over the Saved menu.
            HeaderFooter.OpenSavedMenu();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Hover over the Session menu.
            HeaderFooter.OpenSessionMenu();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Hover over the Stores menu.
            HeaderFooter.OpenStoresMenu();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Hover over the My Account menu.
            HeaderFooter.OpenMyAccountMenu();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
