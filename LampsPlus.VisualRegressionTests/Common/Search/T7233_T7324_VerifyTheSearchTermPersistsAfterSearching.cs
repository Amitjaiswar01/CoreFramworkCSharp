using System.Linq;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.Search
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_Windows_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_Windows_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_Mac_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_Mac_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_iPad_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_iPad_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7233_TabletEmulator_VerifySearchTermPersists : T7233_DesktopBase
    {
        public T7233_TabletEmulator_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_iPhone_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_iPhone_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_AndroidPhone_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_AndroidPhone_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7324_Emulator_VerifySearchTermPersists : T7324_MobileBase
    {
        public T7324_Emulator_VerifySearchTermPersists(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxNotEmpty(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Search term is still present in the Search box after searching for a keyword and landing on a sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7426
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7233
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7426"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7233")]
    public abstract class T7233_DesktopBase : T7233_T7324_Base
    {
        protected T7233_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the Search term is still present in the Search box after searching for a keyword and landing on a sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7426
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7324
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7426"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7324")]
    public abstract class T7324_MobileBase : T7233_T7324_Base
    {
        protected T7324_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config, Urls.HomePageUrl);

            var searchText = "lamp";
            var searchResultText = "lamp shades";
            
            TextContent(searchText);

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));
            Search.SearchField.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchClass.ToCssClassSelector()));
            Home.HomeHeaderElement.Click();

            Browser.Wait.IsInvisibleElement(By.CssSelector(Search.GlobalSearchClass.ToCssClassSelector()));
            Browser.ExecuteJs("document.querySelector('.ie11-wrapper > div.searchdropdown-root').setAttribute('style', 'display: block !important')");

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            TextContent(searchText);

            Browser.Wait.AreAllElementsVisible(By.XPath(Search.AutoSuggestDropDownResultXpath));
            Search.AutoSuggestDropDownResults.First(result => result.Text.Equals(searchResultText)).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));

            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Search.SearchField);
        } 
        private void TextContent(string searchText)
        {
            Search.SearchField.Click();
            Search.SearchField.Clear();
            Search.SearchField.SendKeys(searchText);
        }
    }


    public abstract class T7233_T7324_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7233_T7324_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            var searchText = "lamp";
            var searchResultText = "lamp shades";

            TextContent(searchText);

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.TopSearchesModalContentClass.ToCssClassSelector()));
            Search.HpTitleReDesign.Click(); 

            Browser.Wait.IsInvisibleElement(By.CssSelector(Search.TopSearchesModalContentClass.ToCssClassSelector()));
            Browser.ExecuteJs("document.querySelector('.ie11-wrapper > div.searchdropdown-root').setAttribute('style', 'display: block !important')");

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            TextContent(searchText);

            IElement subMenuElement = Search.AutoSuggestDropDownResults.FirstOrDefault(result => result.Text == searchResultText);

            if (subMenuElement != null)
            {
                subMenuElement.Click();
                SearchWorkflow.EnableSearch();
                Browser.Wait.UntilElementUnloads(subMenuElement, 30);
                ScreenCapturer.CaptureElementArea(Browser.PageUrl, Search.SearchField);
            }
            else
            {
                Assert.True(false, $"Could not find result '{searchResultText}' in auto suggest dropdown when searched term '{searchText}'");
            }
        }

        private void TextContent(string searchText)
        {
            Search.SearchField.Click();
            Search.SearchField.Clear();

            Browser.Wait.IsVisibleElement(By.XPath(Search.SearchXpath));
            Search.SearchField.SendKeys(searchText);
        }
    }
}
