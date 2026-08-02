using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Automation.Framework.Utilities;

namespace LampsPlus.RegressionTests.Common.Sort.T200_T440_VerifyClickProductImageDisplayPage
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T440_iPhone_VerifyClickProductImageDisplayPage : T440_MobileBase
    {
        public T440_iPhone_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T440_Android_VerifyClickProductImageDisplayPage : T440_MobileBase
    {
        public T440_Android_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T440_Emulator_VerifyClickProductImageDisplayPage : T440_MobileBase
    {
        public T440_Emulator_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking a product image on the sort page displays the PDP for the product.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10075
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T440
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10075"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T440")]
    public abstract class T440_MobileBase : TestsBaseMobile
    {
        protected T440_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to a Sort page
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.OutdoorLightingSortUrl);

            //Act : Make a note of Price and ProductName of any product on Sort page
            var sku = Sort.GetNonSaleProductFromSort();
            var productDetails = Sort.GetContentsOf(sku);

            var productName = productDetails.Name;
            var productPrice = productDetails.Price;

            //Act : Navigate to Pdp of the product noted above
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Assert : Verify PDP shows the same product's name, and price, as the sort page
            Assert.Equals(productName, ProductDetail.GetProductName(), "Product Sku does not match.");
            Assert.Equals(TextActions.GetPriceTextOnly(productPrice),  TextActions.RemoveDollarSign(ProductDetail.GetProductPriceOnPdp()), "Product Price does not match.");
        }
    }
}