using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.Search
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7767_Windows_VerifyTheLayoutOfTheOpenBoxSearchPage : T7767_DesktopBase
    {
        public T7767_Windows_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7767_Mac_VerifyTheLayoutOfTheOpenBoxSearchPage : T7767_DesktopBase
    {
        public T7767_Mac_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7767_iPad_VerifyTheLayoutOfTheOpenBoxSearchPage : T7767_DesktopBase
    {
        public T7767_iPad_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7767_TabletEmulator_VerifyTheLayoutOfTheOpenBoxSearchPage : T7767_DesktopBase
    {
        public T7767_TabletEmulator_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_iPhone_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_iPhone_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_AndroidPhone_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_AndroidPhone_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7768_Emulator_VerifyTheLayoutOfTheOpenBoxSearchPage : T7768_MobileBase
    {
        public T7768_Emulator_VerifyTheLayoutOfTheOpenBoxSearchPage(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfOpenBoxSearchPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9152
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7767
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9152"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7767")]
    public abstract class T7767_DesktopBase : T7767_T7768_Base
    {
        protected T7767_DesktopBase(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9152
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7768
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9152"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7768")]
    public abstract class T7768_MobileBase : T7767_T7768_Base
    {
        protected T7768_MobileBase(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);
            string category = Fixture.RandomCategory;

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.OpenBoxSearchClass.ToCssClassSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            Sort.OpenBoxSearchElement.SendKeys(category);
            Sort.OpenBoxSearchElement.SendKeys(Keys.Enter);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class T7767_T7768_SharedCategory_Fixture : FixtureBase
    {
        public string RandomCategory { get; }

        public T7767_T7768_SharedCategory_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "bathroom vanity lights", "wall sconces", "table lamps", "floor lamps", "lamp shades" };
            int index = random.Next(list.Count);
            RandomCategory = list[index];
        }
    }


    public abstract class T7767_T7768_Base : VisualTestsBase, IClassFixture<T7767_T7768_SharedCategory_Fixture>
    {
        protected readonly T7767_T7768_SharedCategory_Fixture Fixture;       

        protected T7767_T7768_Base(ITestOutputHelper output, T7767_T7768_SharedCategory_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);
            string category = Fixture.RandomCategory;

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.OpenBoxSearchFieldId.ToCssIdSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);            

            Sort.OpenBoxSearchField.SendKeys(category);
            Sort.OpenBoxSearchField.SendKeys(Keys.Enter);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.OpenBoxSearchFieldId.ToCssIdSelector()));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
