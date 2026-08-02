using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T335_T527_VerifySearchFieldWithSingleSku
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_Windows_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_Windows_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_Mac_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_Mac_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_iPad_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_iPad_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_TabletEmulator_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_TabletEmulator_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the search field is cleared after successfully searching for a single SKU.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10056
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T335
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10056"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T335")]
    public abstract class T335_DesktopBase : TestsBaseDesktop
    {
        protected T335_DesktopBase(ITestOutputHelper output) : base(output) { }

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
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP.");
            var searchBoxSku = Search.GetSearchTermFromSearchBox();

            //Assert: The Search field does NOT contain the SKU that was used to execute the search.
            Assert.Equals(searchBoxSku, string.Empty, "String is containing search term");
        }
    }
}
