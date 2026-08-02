using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T208_T448_VerifyQuantityLeftAndDailySaleCallOut
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T448_iPhone_VerifyQuantityLeftAndDailySaleCallOut : T448_MobileBase
    {
        public T448_iPhone_VerifyQuantityLeftAndDailySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void QuantityLeftAndDailySaleCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T448_Android_VerifyQuantityLeftAndDailySaleCallOut : T448_MobileBase
    {
        public T448_Android_VerifyQuantityLeftAndDailySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void QuantityLeftAndDailySaleCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T448_Emulator_VerifyQuantityLeftAndDailySaleCallOut : T448_MobileBase
    {
        public T448_Emulator_VerifyQuantityLeftAndDailySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void QuantityLeftAndDailySaleCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that if there are two callouts (QTY Left and Daily Sale) the display order is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10087
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T448
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10087"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T448")]
    public abstract class T448_MobileBase : TestsBaseMobile
    {
        protected T448_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Daily Sales Sort page
            Browser.Navigate(Urls.LpDailySalesUrl);

            //Assert : Verify 'Qty Left' and 'DAILY SALE' callouts are displayed on the Sort page
            Assert.True(Sort.DoesSortHaveQuantityAndDailySaleCallOut, "Quantity Left and Daily Sale Call Out Is Missing");
        }
    }
}