using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T218_T452_VerifyOneHundredBadge
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T452_iPhone_VerifyOneHundredBadge : T452_MobileBase
    {
        public T452_iPhone_VerifyOneHundredBadge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void OneHundredBadge(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T452_Android_VerifyOneHundredBadge : T452_MobileBase
    {
        public T452_Android_VerifyOneHundredBadge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void OneHundredBadge(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T452_Emulator_VerifyOneHundredBadge : T452_MobileBase
    {
        public T452_Emulator_VerifyOneHundredBadge(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void OneHundredBadge(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the '100+ Colors' badge appears on the Sort Page for qualifying items.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10085
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T452
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10085"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T452")]
    public abstract class T452_MobileBase : TestsBaseMobile
    {
        protected T452_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Color Plus page
            Browser.Navigate(Urls.ColorPlusCallOutUrl);

            //Act : Locate a product on Sort page and identify its Shortsku
            var sku = Sort.GetSkuWithCallout(Sort.GetHundredPlusMoreColorsCallout());

            //Act : Execute query using Shortsku from the Sort page
            var dbOneHundredPlusItem = ProductActions.GetOneHundredPlusItem(sku);

            //Assert :  Verify Shortsku and callout match the values from the database
            Assert.Equals(sku, dbOneHundredPlusItem.ShortSku, "'100 + Colors' product sku does not match database.");
            Assert.Equals("100+ Colors", dbOneHundredPlusItem.Callout, "Call out text does not match on site and in database.");
        }
    }
}