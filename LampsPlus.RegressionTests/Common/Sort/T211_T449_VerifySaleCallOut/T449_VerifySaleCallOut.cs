using System;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T211_T449_VerifySaleCallOut
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T449_iPhone_VerifySaleCallOut : T449_MobileBase
    {
        public T449_iPhone_VerifySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SaleCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T449_Android_VerifySaleCallOut : T449_MobileBase
    {
        public T449_Android_VerifySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SaleCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T449_Emulator_VerifySaleCallOut : T449_MobileBase
    {
        public T449_Emulator_VerifySaleCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SaleCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Sale callout is displayed for products where sale price > 0 and lesser than retail price
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10088
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T449
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10088"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T449")]
    public abstract class T449_MobileBase : TestsBaseMobile
    {
        protected T449_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange : Navigate to Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to the Sale page
            Browser.Navigate(Urls.OnSaleUrl);

            //Assert : Verify All the sort page results have the callout 'Sale'
            Assert.True(Sort.DoesSalePageResultHaveSaleCallOut(), "All Sale page results doesn't have 'Sale' callout.");

            //Act : Pick a Sku from Sort page and notedown its data from database
            var sku = Sort.GetSkuWithCallout(Sort.GetSaleCallout());
            var productDetails = Sort.GetContentsOf(sku);
            var resultFromDatabase = ProductActions.GetShortSkuNameAndPrice(sku);

            var priceInternet = ProductActions.GetSalePriceByShortSku(resultFromDatabase.Sku);
            var salePrice = priceInternet.SalePrice1Internet;
            var retailPrice = priceInternet.RetailPriceInternet;

            var currentSalePrice = TextActions.GetPriceTextOnly(productDetails.Price);

            //Assert : Verify Sku has SalePrice1Internet > 0.00, SalePrice1Internet < RetailPriceInternet, sale price on sort page = saleprice1internet
            Assert.True(Convert.ToDecimal(salePrice) > 0, $"{RecurringDataIssue}Expected salePrice1Internet > 0 but was {salePrice}");
            Assert.True(salePrice < retailPrice, $"Expected salePrice1Internet < retailPriceInternet but salePrice1Internet was {salePrice} and the retailPriceInternet was {retailPrice}");
            Assert.Equals(decimal.Round(salePrice, 2), decimal.Parse(currentSalePrice), "Sale price did not match.");

            //Assert : Verify sale price is displayed in red
            Assert.StringContains("rgb(51, 51, 51)", Sort.GetTextColor(), "Sale Price color is not red");
        }
    }
}