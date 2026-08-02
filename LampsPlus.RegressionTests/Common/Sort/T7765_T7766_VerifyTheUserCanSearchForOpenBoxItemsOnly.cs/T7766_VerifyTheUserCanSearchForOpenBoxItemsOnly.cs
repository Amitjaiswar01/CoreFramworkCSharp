using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T7765_T7766_VerifyTheUserCanSearchForOpenBoxItemsOnly.cs
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7766_iPhone_VerifyTheUserCanSearchForOpenBoxItemsOnly : T7766_MobileBase
    {
        public T7766_iPhone_VerifyTheUserCanSearchForOpenBoxItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifytheUserCanSearchforOpenBoxItemsOnly(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7766_Emulator_VerifyTheUserCanSearchForOpenBoxItemsOnly : T7766_MobileBase
    {
        public T7766_Emulator_VerifyTheUserCanSearchForOpenBoxItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifytheUserCanSearchforOpenBoxItemsOnly(string config) => Validate(config);
    }

    /// <summary>
    /// Verify search term persists on open box search field
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10204
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7766
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10204"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7766")]
    public abstract class T7766_MobileBase : TestsBaseMobile
    {
        protected T7766_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrangement : User is on the following page: https://www.lampsplus.com/products/openbox_view-open-box-items/
            InitializeFunctionalTest(config, Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on the Sort page.");

            //Assert : Verify all the sort page results have the callout "Open Box"
            Assert.True(Sort.DoesSortPageResultHaveOpenBoxCallout(), "All the sort page results don't have the callout 'Open Box'.");

            //Act : Search Ramdomly categories
            var searchText = Sort.SearchForCategory();

            //Assert : Verify the search term persists in the search field after searching
            Assert.True(Sort.DoesSortPageResultHaveOpenBoxCallout(), "All the sort page results don't have the callout 'Open Box'.");
            Assert.Equals(searchText, Sort.SearchOpenBox(), "Searched term does not persists in the search field after searching");
        }
    }
}