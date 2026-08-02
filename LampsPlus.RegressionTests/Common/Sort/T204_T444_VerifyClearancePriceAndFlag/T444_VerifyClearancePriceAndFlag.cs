using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T204_T444_VerifyClearancePriceAndFlag
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T444_iPhone_VerifyClearancePriceAndFlag : T444_MobileBase
    {
        public T444_iPhone_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T444_Android_VerifyClearancePriceAndFlag : T444_MobileBase
    {
        public T444_Android_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyClearancePriceAndFlag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T444_Emulator_VerifyClearancePriceAndFlag : T444_MobileBase
    {
        public T444_Emulator_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a product marked as a 'Clearance' item has correct flag and price in database.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10077
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T204
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10077"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T444")]
    public abstract class T444_MobileBase : TestsBaseMobile
    {
        protected T444_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Clearance Page
            Browser.Navigate(Urls.ClearancePageUrl);

            //Assert : Verify the Price and the word 'Clearance' is in red
            Assert.True(Sort.DoesTextColorMatches("rgba(51, 51, 51, 1)"), "The price and the word 'Clearance' text is not in red.");

            //Act : Navigate to Pdp of any Clearance product and notedown the shortsku
            var sku = Sort.GetSkuWithCallout(Sort.GetSaleCallout());
            Browser.NavigateToPdp(sku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on Product Detail Page");

            var priceOnSite = ProductDetail.GetProductPriceText();

            //Act : Notedown the product details from Database
            var clearancePriceEntity = ProductActions.GetClearancePriceByShortsku(sku);

            //Assert : Verify Price on site matches with RetailPriceInternet in DB, and ClearanceFlag is 1
            Assert.Equals(priceOnSite, string.Format("{0:0.00}", clearancePriceEntity.RetailPriceInternet).Trim(), "Product price is not identical on Site and in database");
            Assert.True(clearancePriceEntity.IsClearance, "Clearance Flag value is not 1");
        }
    }
}