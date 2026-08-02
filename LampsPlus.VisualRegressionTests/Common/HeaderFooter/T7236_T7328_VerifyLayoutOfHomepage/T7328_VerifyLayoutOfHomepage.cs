using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using xRetry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7236_T7328_VerifyLayoutOfHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_iPhone_VerifyTheLayoutOfHomePage : T7328_MobileBase
    {
        public T7328_iPhone_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_iPhone_VerifyTheLayoutOfHomePagePros : T7328_MobileBase
    {
        public T7328_iPhone_VerifyTheLayoutOfHomePagePros(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_AndroidPhone_VerifyTheLayoutOfHomePage : T7328_MobileBase
    {
        public T7328_AndroidPhone_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_AndroidPhone_VerifyTheLayoutOfHomePagePros : T7328_MobileBase
    {
        public T7328_AndroidPhone_VerifyTheLayoutOfHomePagePros(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_Emulator_VerifyTheLayoutOfHomePage : T7328_MobileBase
    {
        public T7328_Emulator_VerifyTheLayoutOfHomePage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7328_Emulator_VerifyTheLayoutOfHomePagePros : T7328_MobileBase
    {
        public T7328_Emulator_VerifyTheLayoutOfHomePagePros(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void LayoutOfHomePage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Homepage and Footer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9799
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7328
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9799"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7328")]
    public abstract class T7328_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7328_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Home page.
            InitializeVisualTest(config, Urls.HomePageUrl);

            //Act: Capture a screenshot of the entire page while ignoring the Instagram widget.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Home.IgnoreInstagramFeed() }, true, true);
        }
    }
}
