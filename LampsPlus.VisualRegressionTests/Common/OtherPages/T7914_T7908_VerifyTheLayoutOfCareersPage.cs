using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7914_Windows_VerifyTheLayoutOfCareersPage : T7914_DesktopBase
    {
        public T7914_Windows_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7914_Mac_VerifyTheLayoutOfCareersPage : T7914_DesktopBase
    {
        public T7914_Mac_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7914_iPad_VerifyTheLayoutOfCareersPage : T7914_DesktopBase
    {
        public T7914_iPad_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7914_TabletEmulator_VerifyTheLayoutOfCareersPage : T7914_DesktopBase
    {
        public T7914_TabletEmulator_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7908_iPhone_VerifyTheLayoutOfCareersPage : T7908_MobileBase
    {
        public T7908_iPhone_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7908_AndroidPhone_VerifyTheLayoutOfCareersPage : T7908_MobileBase
    {
        public T7908_AndroidPhone_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7908_Emulator_VerifyTheLayoutOfCareersPage : T7908_MobileBase
    {
        public T7908_Emulator_VerifyTheLayoutOfCareersPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCareersPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of Careers Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7914
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7914")]
    public abstract class T7914_DesktopBase : T7914_T7908_Base
    {
        protected T7914_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void CaptureCareers()
        {
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Home.CareersLinks });
        }
    }


    /// <summary>
    /// Verify the Layout of Careers Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10544
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7908
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10544"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7908")]
    public abstract class T7908_MobileBase : T7914_T7908_Base
    {
        protected T7908_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void CaptureCareers()
        {
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, false, false, true);
        }
    }


    public abstract class T7914_T7908_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7914_T7908_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            HeaderFooter.FooterCareersLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(HeaderFooter.OpenPositionsBtnClass.ToCssClassSelector()));

            CaptureCareers();
        }

        protected abstract void CaptureCareers();
    }
}