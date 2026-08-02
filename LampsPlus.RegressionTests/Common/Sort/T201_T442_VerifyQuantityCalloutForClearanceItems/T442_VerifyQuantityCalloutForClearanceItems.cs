using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T201_T442_VerifyQuantityCalloutForClearanceItems
{
    public class T442_iPhone_VerifyQuantityCalloutForClearanceItems : T442_MobileBase
    {
        public T442_iPhone_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T442_Android_VerifyQuantityCalloutForClearanceItems : T442_MobileBase
    {
        public T442_Android_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T442_Emulator_VerifyQuantityCalloutForClearanceItems : T442_MobileBase
    {
        public T442_Emulator_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the QTY callout for clearance items is displayed on the Sort page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10083
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T442
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10083"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T442")]
    public abstract class T442_MobileBase : TestsBaseMobile
    {
        protected T442_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Clearance Sort page
            Browser.Navigate(Urls.ClearanceViewPageUrl);

            //Assert : Verify 'Qty Left' callout displays on the Sort page
            Assert.True( Sort.IsQtyLeftCalloutPresent, "Qty Left callout is not displayed on the Sort page");

            var qtyLeftSite = Sort.GetQtyLeftValue();

            //Act : Locate a product with the 'Qty Left' callout and navigate to its Pdp
            var shortSku = Sort.GetSkuWithCallout(Sort.GetQtyLeftCallout());
            Browser.NavigateToPdp(shortSku);

            var shortSkuOnPdp = ProductDetail.GetProductSku();
            var qtyLeftDb = ProductActions.GetQuantityLeft(shortSkuOnPdp);

            //Assert : Verify Qty Left callout value does not match with value in Database
            Assert.Equals(qtyLeftDb, qtyLeftSite, "Qty Left callout value does not match with value in Database");
        }
    }
}