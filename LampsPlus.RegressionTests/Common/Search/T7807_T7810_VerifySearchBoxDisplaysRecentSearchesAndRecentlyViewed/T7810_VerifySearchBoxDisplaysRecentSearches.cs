using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7807_T7810_VerifySearchBoxDisplaysRecentSearchesAndRecentlyViewed
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_iPhone_VerifySearchBoxDisplaysRecent : T7810_MobileBase
    {
        public T7810_iPhone_VerifySearchBoxDisplaysRecent(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_Emulator_VerifySearchBoxDisplaysRecent : T7810_MobileBase
    {
        public T7810_Emulator_VerifySearchBoxDisplaysRecent(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7810_Android_VerifySearchBoxDisplaysRecent : T7810_MobileBase
    {
        public T7810_Android_VerifySearchBoxDisplaysRecent(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SearchBoxDisplaysRecentSearchesAndRecentlyViewed(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that Search Box Displays Recent Searches And Recently Viewed Items
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9407
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7810
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9407"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7810")]
    public abstract class T7810_MobileBase : TestsBaseMobile
    {
        protected T7810_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Act
            1. Perform two (2) different free text searches (different terms)
            2. Navigate to four (4) different PDPs
            */
            InitializeFunctionalTest(config, Urls.HomePageUrl);

            var randomTerm = new List<string> { "bathroom", "chandeliers" };
            Search.SearchRandomTerm(randomTerm);

            //Act: Navigate to four (4) different PDPs
            var randomSkus = ProductActions.GetListableInStockShortSku(4);
            foreach (var sku in randomSkus)
            {
                ProductDetail.NavigateToProductDetailByShortSku(sku);
                Assert.True(ProductDetail.IsCurrentPage, "Current page is not a PDP page");
                Browser.ScrollToBottomOfPageJs();
            }

            /*Act
            1. Click into the Search input box located  in the page header
            2. Once it expands, verify it's content
            */
            var listFreeTextSearch = Search.GetRecentSearchTerms().ToList();
            Log.Message($"The number of search terms is: {listFreeTextSearch.Count}");

            //Assert: FreeText Term
            for (var searchTerm = 0; randomTerm.Count - 1 > searchTerm; searchTerm++) //Verify FreeText Term
            {
                var randomFreeTextSearch = randomTerm[searchTerm];

                var searchDropDownText = listFreeTextSearch[1 - searchTerm];

                Assert.Equals(randomFreeTextSearch, searchDropDownText, "Search Team does not match");
            }

            var randomSkuResult = string.Join(", ", randomSkus.ToArray());

            //Verify Recently Viewed
            for (var sku = 3; sku < randomSkus.Count - 1; sku--)
            {
                var recentlyViewDropDownItem = Search.GetRecentlyViewedItemAttribute(3 - sku, "data-certonasku");

                Assert.StringContains(randomSkuResult, recentlyViewDropDownItem, "Recently Viewed Sku doesn't match");

                if (sku == 1)
                    break;
            }

            //Assert: View all link is present next to Recently Viewed text and underlined.
            Assert.StringContains(Search.GetClearHistoryText(), "underline", "underline is not present");
            Assert.StringContains(Search.GetViewAllText(), "underline", "underline is not present");

            /*Act and Assert
            Click on any Recently Viewed thumbnail image
            User is redirected to a relevant PDP
            */
            //Search.RecentlyViewedItem(0).Click();
            Search.ClickRecentlyViewedItemByIndex(0);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not a PDP page");
        }
    }
}
