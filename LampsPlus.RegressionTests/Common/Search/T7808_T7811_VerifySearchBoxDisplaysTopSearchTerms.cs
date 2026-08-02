using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Search
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_Windows_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_Windows_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_Mac_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_Mac_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_iPad_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_iPad_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7808_TabletEmulator_VerifySearchBoxDisplaysTopSearchTerms : T7808_DesktopBase
    {
        public T7808_TabletEmulator_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7811_iPhone_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_iPhone_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7811_Android_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_Android_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7811_Emulator_VerifySearchBoxDisplaysTopSearchTerms : T7811_MobileBase
    {
        public T7811_Emulator_VerifySearchBoxDisplaysTopSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopSearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Search Box Displays Top Search Terms
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9408
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7808
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9408"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7808")]
    public abstract class T7808_DesktopBase : T7808_T7811_Base
    {
        protected T7808_DesktopBase(ITestOutputHelper output) : base(output) { } 
        protected override void ClickOnSearchHeader()
        {
            Search.SearchField.Click();
        }
    }


    /// <summary>
    /// Verify that the Search Box Displays Top Search Terms
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9408
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7811
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9408"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7811")]
    public abstract class T7811_MobileBase : T7808_T7811_Base
    {
        protected T7811_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void ClickOnSearchHeader()
        {
            Browser.ScrollToBottomOfPage(Urls.HomePageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));
            SearchWorkflow.EnableSearch();
        }
    }


    public abstract class T7808_T7811_Base : SearchTestsBase
    {
        protected T7808_T7811_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            ClickOnSearchHeader();

            Browser.Wait.IsInvisibleElement(By.CssSelector(Search.Ie11WrapperRecentSearchesHiddenClass.ToCssClassSelector()));

            var totalTopSearchesCount = Search.ListOfTopSearchesProducts.Count;

            var topSearchModalData = Search.TopSearchesModalContent.Text.Replace("Top Searches", string.Empty).TrimStart();
            var topSearchAllContent = topSearchModalData.Replace(Environment.NewLine, " ");

            for (var topSearchesTerm = 0; topSearchesTerm < totalTopSearchesCount; topSearchesTerm++)
            {
                var topSearchValue = Search.TopSearchesDropDown(topSearchesTerm).Text;

                Assert.StringContains(topSearchAllContent, topSearchValue, "Top Searches does not matches");
            }
        }

        protected abstract void ClickOnSearchHeader();
    }
}