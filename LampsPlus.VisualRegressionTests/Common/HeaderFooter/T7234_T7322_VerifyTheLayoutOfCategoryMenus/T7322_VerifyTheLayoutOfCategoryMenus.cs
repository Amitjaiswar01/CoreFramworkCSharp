using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7234_T7322_VerifyTheLayoutOfCategoryMenus
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7322_iPhone_VerifyTheLayoutOfCategoryMenus : T7322_MobileBase
    {
        public T7322_iPhone_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7322_AndroidPhone_VerifyTheLayoutOfCategoryMenus : T7322_MobileBase
    {
        public T7322_AndroidPhone_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7322_Emulator_VerifyTheLayoutOfCategoryMenus : T7322_MobileBase
    {
        public T7322_Emulator_VerifyTheLayoutOfCategoryMenus(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCategoryMenus(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Category menus appears correctly when the user interacts with them.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7424
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7322
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7424"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7322")]
    public abstract class T7322_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7322_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            /*Arrange: 
            User is on the Lamps Plus Homepage.
            */
            InitializeVisualTest(config, Urls.HomePageUrl);
            Browser.Wait.ForDomReady();

            /* Act:
            On the Homepage, tap on the hamburger menu.
            Tap on the 'On Sale' category.
            Capture a screenshot of the visible page.
            Repeat the test for all the main Categories under the 'Shop By Category' section.
            */
            TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            HeaderFooter.OpenLpMenu();

            foreach (var navElement in HeaderFooter.GetNavElements())
            {
                Browser.Wait.ForElementToStopAnimating(Browser.Locate.ElementBySelector("lpCollapsible".ToCssClassSelector()));

                Browser.ScrollIntoView(navElement);
                Browser.Wait.ForDisplayedElement(navElement).Click();
                Browser.Wait.ForDomReady();

                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture, true, true);

                Browser.Wait.ForDisplayedElement(navElement).Click();
            }
        }
    }
}