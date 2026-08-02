using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T335_T527_VerifySearchFieldWithSingleSku
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T527_iPhone_VerifySearchFieldWithSingleSku : T527_MobileBase
    {
        public T527_iPhone_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T527_Emulator_VerifySearchFieldWithSingleSku : T527_MobileBase
    {
        public T527_Emulator_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the search field is cleared after successfully searching for a single SKU.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10056
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T527
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10056"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T527")]
    public abstract class T527_MobileBase : TestsBaseMobile
    {
        protected T527_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Homepage and has identified a SKU.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            /*Act:
            Enter in a SINGLE SKU into the search field.
            Execute the search.
            */
            Search.SearchForSingleSku(sku);
            
            if (Browser.PageUrl.Contains("?s=1"))//TODO Verify if search returns multiple results
            {
                Sort.SelectFirstProductOnSortPage();
            }

            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP.");
            var searchBoxSku = Search.GetSearchTermFromSearchBox();
            
            //Assert: The Search field does NOT contain the SKU that was used to execute the search.
            Assert.Equals(searchBoxSku, string.Empty, "String is containing search term");
        }
    }
}
