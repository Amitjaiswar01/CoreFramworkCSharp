using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Search
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_Windows_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_Windows_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_Mac_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_Mac_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_iPad_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_iPad_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7472_TabletEmulator_VerifyHardCodedSearchResult : T7472_DesktopBase
    {
        public T7472_TabletEmulator_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7474_iPhone_VerifyHardCodedSearchResult : T7474_MobileBase
    {
        public T7474_iPhone_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7474. Rework - ACD-10538")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7474_Emulator_VerifyHardCodedSearchResult : T7474_MobileBase
    {
        public T7474_Emulator_VerifyHardCodedSearchResult(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7474. Rework - ACD-10538")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchTermRedirection(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user is directed to the appropriate page when searching for a hard coded search term.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8285
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7472 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8285"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7472")]
    public abstract class T7472_DesktopBase : T7472_T7474_Base
    {
        protected T7472_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyRedirectionBySearchTerm(string expectedPage, string searchTerm)
        {
            Browser.Wait.ForDomReady();
            Search.SearchField.Click();
            Search.ClearSearchFieldText();
            Search.ExecuteSearch(searchTerm);
            SearchWorkflow.EnableSearch();

            var firstFourCharactersOfSearchTerm = searchTerm.Substring(0, 4);

            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains(firstFourCharactersOfSearchTerm));
            Assert.Equals(expectedPage, Browser.PageUrl, "The user is not redirected to the " + searchTerm + " page.");
        }
    }


    /// <summary>
    /// Verify that the user is directed to the appropriate page when searching for a hard coded search term.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8285
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7474
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8285"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7474")]
    public abstract class T7474_MobileBase : T7472_T7474_Base
    {
        protected T7474_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyRedirectionBySearchTerm(string expectedPage, string searchTerm)
        {
            bool isSearchExposed = Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));

            if (isSearchExposed)
            {
                Browser.Wait.ForDomReady();
                Search.ExecuteSearch(searchTerm);
                SearchWorkflow.EnableSearch();
            }
            else
            {
                Browser.Wait.ForDomReady();
                Search.SearchField.Click();
                Search.ClearSearchFieldText();
                Search.ExecuteSearch(searchTerm);
            }

            var firstFourCharactersOfSearchTerm = searchTerm.Substring(0, 4);

            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains(firstFourCharactersOfSearchTerm));
            Assert.Equals(expectedPage, Browser.PageUrl, "The user is not redirected to the " + searchTerm + " page.");
        }

        protected override void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            VerifyRedirectionBySearchTerm(Urls.HelpAndPoliciesPageUrl, "help");

            VerifyRedirectionBySearchTerm(Urls.ContactUsPageUrl, "contact");

            VerifyRedirectionBySearchTerm(Urls.WishListPageUrl, "wishlist");
        }
    }


    public abstract class T7472_T7474_Base : SearchTestsBase
    {
        protected T7472_T7474_Base(ITestOutputHelper output) : base(output) { }


        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            VerifyRedirectionBySearchTerm(Urls.HelpAndPoliciesPageUrl, "help");

            VerifyRedirectionBySearchTerm(Urls.ContactUsPageUrl, "contact");

            VerifyRedirectionBySearchTerm(Urls.WishListPageUrl, "wishlist");
        }

        protected abstract void VerifyRedirectionBySearchTerm (string expectedPage, string searchTerm);
    }
}
