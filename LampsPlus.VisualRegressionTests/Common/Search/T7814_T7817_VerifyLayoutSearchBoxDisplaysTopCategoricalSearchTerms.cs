using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Search
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_Windows_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7814_DesktopBase
    {
        public T7814_Windows_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7814. Rework - ACD-9872")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_Mac_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7814_DesktopBase
    {
        public T7814_Mac_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_iPad_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7814_DesktopBase
    {
        public T7814_iPad_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7814_TabletEmulator_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7814_DesktopBase
    {
        public T7814_TabletEmulator_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_iPhone_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7817_MobileBase
    {
        public T7817_iPhone_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7817. Rework - ACD-9872")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_AndroidPhone_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7817_MobileBase
    {
        public T7817_AndroidPhone_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7817_Emulator_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms : T7817_MobileBase
    {
        public T7817_Emulator_VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7817. Rework - ACD-9872")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyLayoutSearchBoxDisplaysTopCategoricalSearchTerms(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9411
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7814
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9411"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7814")]
    public abstract class T7814_DesktopBase : T7814_T7817_Base
    {
        protected T7814_DesktopBase(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        protected override void ApplyFilter()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));

            if(TestSetup.TestConfiguration.IsBaseLine)
            {
                Fixture.Filters = Sort.ApplyFilters(1);
            }
            else
            {
                Sort.ApplyFilters(1, false, Fixture.Filters);
            }
        }

        protected override void CaptureScreenshot()
        {
            Browser.Wait.ForDomReady();

            Search.SearchButton.Click();

            Browser.ExecuteJs(("document.querySelector('#searchContainer > div.ie11-wrapper.recentsearches.hidden').setAttribute('class', 'ie11-wrapper recentsearches')"));

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9411
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7817
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9411"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7817")]
    public abstract class T7817_MobileBase : T7814_T7817_Base
    {
        protected T7817_MobileBase(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture) { }

        protected override void ApplyFilter()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterButtonTriggerClass.ToCssClassSelector()));

            if (TestSetup.TestConfiguration.IsBaseLine)
            {
                Fixture.Filters = Sort.ApplyFilters(1);
            }
            else
            {
                Sort.ApplyFilters(1, false, Fixture.Filters);
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));
            SearchWorkflow.EnableSearch();
        }

        protected override void CaptureScreenshot()
        {
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.XPath(Search.RecentSearchTopElementXpath));
            Browser.Locate.ElementByXpath(Search.RecentSearchTopElementXpath).Click();
            
            Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches').setAttribute('class', 'ie11-wrapper recentsearches hidden')");
            Browser.ExecuteJs("document.querySelector('#globalSearch > div.ie11-wrapper.recentsearches.hidden').setAttribute('class', 'ie11-wrapper recentsearches')");

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class T7814_T7817_SharedCategory_Fixture : FixtureBase
    {
        public Dictionary<string, string>[] Filters { get; set; }
    }


    public abstract class T7814_T7817_Base : VisualTestsBase, IClassFixture<T7814_T7817_SharedCategory_Fixture>
    {
        protected readonly T7814_T7817_SharedCategory_Fixture Fixture;

        protected T7814_T7817_Base(ITestOutputHelper output, T7814_T7817_SharedCategory_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            ApplyFilter();

            CaptureScreenshot();
        }

        protected abstract void ApplyFilter();
        protected abstract void CaptureScreenshot();
    }
}
