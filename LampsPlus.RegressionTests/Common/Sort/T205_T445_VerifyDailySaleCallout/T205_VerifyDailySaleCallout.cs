using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T205_T445_VerifyDailySaleCallout
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T205_Windows_VerifyDailySaleCallOutDisplaysOnSortPage : T205_DesktopBase
    {
        public T205_Windows_VerifyDailySaleCallOutDisplaysOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void DailySaleCallOutDisplaysOnSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T205_Mac_VerifyDailySaleCallOutDisplaysOnSortPage : T205_DesktopBase
    {
        public T205_Mac_VerifyDailySaleCallOutDisplaysOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void DailySaleCallOutDisplaysOnSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T205_iPad_VerifyDailySaleCallOutDisplaysOnSortPage : T205_DesktopBase
    {
        public T205_iPad_VerifyDailySaleCallOutDisplaysOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void DailySaleCallOutDisplaysOnSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T205_TabletEmulator_VerifyDailySaleCallOutDisplaysOnSortPage : T205_DesktopBase
    {
        public T205_TabletEmulator_VerifyDailySaleCallOutDisplaysOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void DailySaleCallOutDisplaysOnSortPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Daily Sale callout displays on Sort Page for decrementable products.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10086
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T205
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10086"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T205")]
    public abstract class T205_DesktopBase : TestsBaseDesktop
    {
        protected T205_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Ceiling Fans page
            Browser.Navigate(Urls.LpDailySalesUrl);

            //Act : Locate a product with the 'Daily Sale' callout
            var shortSku = Sort.GetSkuWithCallout(Sort.GetDailySaleCallout());

            //Assert : Verify 'Daily Sale' callout displays on the Sort page for selected item
            Assert.Equals("DAILY SALE", Sort.GetDailySaleCalloutLabel().Trim(), "Daily Sale callout is not displayed on the Sort page for selected Sku");

            var doesDecrementableFlagExist = ProductActions.GetDecremantableFlagForShortSku(shortSku);

            //Assert: Verify 'isdecrementable' column from the database query has a value of 1 for the given SKU
            Assert.True(doesDecrementableFlagExist, "isdecrementable column in database does not have value of 1 for SKU");
        }
    }
}