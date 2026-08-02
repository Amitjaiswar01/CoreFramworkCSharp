using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T7800_T7801_VerifyUserCanSearchForSaleItemsOnly
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7801_iPhone_VerifyUserCanSearchForSaleItemsOnly : T7801_MobileBase
    {
        public T7801_iPhone_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7801_Emulator_VerifyUserCanSearchForSaleItemsOnly : T7801_MobileBase
    {
        public T7801_Emulator_VerifyUserCanSearchForSaleItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyUserCanSearchForSaleItemsOnly(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the User Can Search for Sale Items Only
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10070
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7801
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10070"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7801")]
    public abstract class T7801_MobileBase : TestsBaseMobile
    {
        protected T7801_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on  Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Sale Sort page
            Browser.Navigate(Urls.LpOnSaleUrl);

            //Assert : Verify All the sort page results have the callout 'Sale'
            Assert.True(Sort.DoesSalePageResultHaveSaleCallOut(), "All Sale page results doesn't have 'Sale' callout.");

            //Act : Perform contextual search from sale sort page for any random category
            Sort.SearchText(Sort.GetSaleSearchField(), Sort.GetCategory());

            //Assert: Verify that all search results have either the callout 'Sale' or 'Clearance'
            Assert.True(Sort.DoesSaleSortPageResultHaveSaleOrClearanceCallOut(), "Sale sort page results doesn't have 'Sale' or 'Clearance' callout.");
        }
    }
}
