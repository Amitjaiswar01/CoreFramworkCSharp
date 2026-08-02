using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7236_T7328_VerifyLayoutOfHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Windows_VerifyTheLayoutOfHomePage : T7236_DesktopBase
    {
        public T7236_Windows_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Windows_VerifyTheLayoutOfHomePageForPro : T7236_DesktopBase
    {
        public T7236_Windows_VerifyTheLayoutOfHomePageForPro(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Windows_VerifyTheLayoutOfHomePageForKiosk : T7236_DesktopBase
    {
        public T7236_Windows_VerifyTheLayoutOfHomePageForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Windows_VerifyTheLayoutOfHomePageForHospitality : T7236_DesktopBase
    {
        public T7236_Windows_VerifyTheLayoutOfHomePageForHospitality(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_HCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_HCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Mac_VerifyTheLayoutOfHomePage : T7236_DesktopBase
    {
        public T7236_Mac_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Mac_VerifyTheLayoutOfHomePageForPro : T7236_DesktopBase
    {
        public T7236_Mac_VerifyTheLayoutOfHomePageForPro(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Mac_VerifyTheLayoutOfHomePageForKiosk : T7236_DesktopBase
    {
        public T7236_Mac_VerifyTheLayoutOfHomePageForKiosk(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_Mac_VerifyTheLayoutOfHomePageForHospitality : T7236_DesktopBase
    {
        public T7236_Mac_VerifyTheLayoutOfHomePageForHospitality(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_HCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_HCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_iPad_VerifyTheLayoutOfHomePage : T7236_DesktopBase
    {
        public T7236_iPad_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7236_TabletEmulator_VerifyTheLayoutOfHomePage : T7236_DesktopBase
    {
        public T7236_TabletEmulator_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Homepage and Footer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9799
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7236
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9799"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7236")]
    public abstract class T7236_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7236_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Home page.
            InitializeVisualTest(config);

            //Act: Capture a screenshot of the entire page while ignoring the Hospitality banner.
            if (config.Contains("HCSI"))
            {
                ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl,  Home.IgnoreHospitalityElements(), true, true);
            }
            //Act: Capture a screenshot of the entire page while ignoring the info bar in the header.
            else if (config.Contains("SIS"))
            {
                HeaderFooter.IsEmployeeSignedInWithStoreInSession();
                ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { HeaderFooter.IgnoreInfoBarInHeader() }, true, true);
            }
            //Act: Capture a screenshot of the entire page while ignoring the Instagram and Recently Viewed widgets.
            else if (config.Contains("PCSI"))
            {
                ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, Home.GetBodyElement(), new List<IElement> { Home.IgnoreRecentlyViewedWidget(), Home.IgnoreInstagramFeed() }, true, true);
            }
            //Act: Capture a screenshot of the entire page while ignoring Instagram widget.
            else
            {
                ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Home.IgnoreInstagramFeed() }, true, true);
            }
        }
    }
}
