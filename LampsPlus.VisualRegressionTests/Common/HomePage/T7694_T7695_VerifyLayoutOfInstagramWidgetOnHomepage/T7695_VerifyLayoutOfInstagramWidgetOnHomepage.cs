using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HomePage.T7694_T7695_VerifyLayoutOfInstagramWidgetOnHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_iPhone_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_iPhone_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_AndroidPhone_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_AndroidPhone_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_Emulator_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_Emulator_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_iPhone_Pro_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_iPhone_Pro_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_AndroidPhone_Pro_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_AndroidPhone_Pro_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7695_Emulator_Pro_VerifyLayoutOfInstagramWidgetOnHomepage : T7695_MobileBase
    {
        public T7695_Emulator_Pro_VerifyLayoutOfInstagramWidgetOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void LayoutOfInstagramWidgetOnLPHomepage(string config) => Validate(Validate, config);
    }

    /// <summary>
    /// Verify the Layout Of Instagram Widget On LP Homepage
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9802
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7695
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9802"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7695")]
    public abstract class T7695_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7695_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config, Urls.HomePageUrl);

            /* Act:
            Scrolling the page upto Pixlee Modal section
            Click on the first pixel image
            */
            Home.OpenInstagramWidget();

            // Act: Capture of the screenshot of the visible screen
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, Home.GetInstagramWidget(), new List<IElement>{ Home.IgnoreInstagramPixleeElement() });
        }
    }
}