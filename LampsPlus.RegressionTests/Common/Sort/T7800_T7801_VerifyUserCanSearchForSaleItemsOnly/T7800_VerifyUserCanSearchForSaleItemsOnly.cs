using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T7800_T7801_VerifyUserCanSearchForSaleItemsOnly
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7800_Windows_VerifyUserCanSearchForSaleItemsOnly : T7800_DesktopBase
    {
        public T7800_Windows_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7800_Mac_VerifyUserCanSearchForSaleItemsOnly : T7800_DesktopBase
    {
        public T7800_Mac_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7800_iPad_VerifyUserCanSearchForSaleItemsOnly : T7800_DesktopBase
    {
        public T7800_iPad_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7800_TabletEmulator_VerifyUserCanSearchForSaleItemsOnly : T7800_DesktopBase
    {
        public T7800_TabletEmulator_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the User Can Search for Sale Items Only
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10070
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7800
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10070"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7800")]
    public abstract class T7800_DesktopBase : TestsBaseDesktop
    {
        protected T7800_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on  Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Sale Sort page
            Browser.Navigate(Urls.LpOnSaleUrl);

            //Assert : Verify All the sort page results have the callout 'Sale'
            Assert.True(Sort.DoesSalePageResultHaveSaleCallOut(), "All Sale page results doesn't have 'Sale' callout.");

            //Assert : Verify the breadcrumbs include 'View On Sale Items'
            Assert.Equals("View On Sale Items", Sort.GetBreadCrumbElement().Text, "The breadcrumbs not include 'View On Sale Items'.");

            //Act : Perform contextual search from sale sort page for any random category
            Sort.SearchText(Sort.GetSaleSearchField(), Sort.GetCategory());

           //Assert: Verify that all search results have either the callout 'Sale' or 'Clearance'
           Assert.True(Sort.DoesSaleSortPageResultHaveSaleOrClearanceCallOut(), "Sale sort page results doesn't have 'Sale' or 'Clearance' callout.");
        }
    }
}