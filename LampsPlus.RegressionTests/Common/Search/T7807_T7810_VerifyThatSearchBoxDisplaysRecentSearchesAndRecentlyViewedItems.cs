using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.RegressionTests.Common.Search
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7807_Windows_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7807_DesktopBase
    {
        public T7807_Windows_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7807_Mac_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7807_DesktopBase
    {
        public T7807_Mac_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7807. Rework - CI-2909")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7807_iPad_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7807_DesktopBase
    {
        public T7807_iPad_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7807_TabletEmulator_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7807_DesktopBase
    {
        public T7807_TabletEmulator_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_iPhone_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7810_MobileBase
    {
        public T7810_iPhone_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_Emulator_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7810_MobileBase
    {
        public T7810_Emulator_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_Android_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed : T7810_MobileBase
    {
        public T7810_Android_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that Search Box Displays Recent Searches And Recently Viewed Items
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9407
    /// Test Case Link:https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7807
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9407"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7807")]
    public abstract class T7807_DesktopBase : T7807_T7810_Base
    {
        protected T7807_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void FreeText(List<string> randomTerm)
        {
            var inputBox = randomTerm;

            foreach (var searchValue in inputBox)
            {
                Search.HighLightTextInSearchBoxAndDelete();

                Search.SearchField.Click();

                Browser.Wait.IsVisibleElement(By.XPath(Search.SearchXpath));
                Search.ExecuteSearch(searchValue);

                Browser.Wait.ForDomReady();
            }
        }

        protected override void SearchHeader()
        {
            Search.SearchField.Click();

            Browser.MouseOverOnElement(Search.SearchField);
            Browser.Wait.IsVisibleElement(By.ClassName(Search.SearchRecentSearchesItemClass));
        }
    }


    /// <summary>
    /// Verify that Search Box Displays Recent Searches And Recently Viewed Items
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9407
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7810
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9407"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7810")]
    public abstract class T7810_MobileBase : T7807_T7810_Base
    {
        protected T7810_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void FreeText(List<string> randomTerm)
        {
            var inputBox = randomTerm;

            foreach (var searchValue in inputBox)
            {
                Browser.Wait.ForDomReady();
                Search.SearchField.Click();

                Search.ClearSearchFieldText();

                Browser.Wait.IsVisibleElement(By.CssSelector(Search.SearchSubmitClass.ToCssClassSelector()));

                Search.SearchField.SendKeys(searchValue);

                Search.SearchButton.Click();

                Browser.Wait.ForDomReady();
            }
        }

        protected override void SearchHeader()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Home.HomeHeaderClass.ToCssClassSelector()));
            Browser.ScrollToBottomOfPage(Urls.HomePageUrl);

            SearchWorkflow.EnableSearch();

            Browser.MouseOverOnElement(Search.SearchField);

            Browser.Wait.IsVisibleElement(By.ClassName(Search.SearchRecentSearchesItemClass));
        }
    }


    public abstract class T7807_T7810_Base : SearchTestsBase
    {
        protected T7807_T7810_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Browser.ClearAllCookies();

            var randomTerm = new List<string>
                {"bathroom", "chandeliers"};

            FreeText(randomTerm);

            var randomSkus = ProductActions.GetListableInStockShortSku(4);

            foreach (var sku in randomSkus)
            {
                ProductDetail.NavigateToProductDetailByShortSku(sku);
                Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
                Browser.ScrollToBottomOfPage(ProductDetail.PdAddToCartStickyId);
            }

            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();

            SearchHeader();

            var listFreeTextSearch = Search.ListOfRecentlySearch;

            //Verify FreeText Term
            for (var searchTerm = 0; randomTerm.Count-1 > searchTerm; searchTerm++)
            {
                var randomFreeTextSearch = randomTerm[searchTerm];

                var searchDropDownText = listFreeTextSearch[1 - searchTerm].Text;

                Assert.Equals(randomFreeTextSearch, searchDropDownText, "Search Team does not match");
            }

            var randomSkuResult = string.Join(", ", randomSkus.ToArray());

            //Verify Recently Viewed
            for (var sku = 3; sku < randomSkus.Count-1; sku--)
            {
                var recentlyViewDropDownItem = Search.RecentlyViewedItem(3 - sku).GetAttribute("data-certonasku");

                Assert.StringContains(randomSkuResult, recentlyViewDropDownItem, "Recently Viewed Sku doesn't match");

                if (sku == 1)
                    break;
            }

            var clearSearchText = Search.ClearHistory.GetCssValue("text-decoration");

            var viewAllText = Search.ViewAllButton.GetCssValue("text-decoration");

            Assert.StringContains(clearSearchText, "underline", "underline is not present");
            Assert.StringContains(viewAllText, "underline", "underline is not present");

            Search.RecentlyViewedItem(0).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
        }

        protected abstract void FreeText(List<string> randomTerm);
        protected abstract void SearchHeader();
    }
}