using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T198_T438_VerifySoldOutCallOutAfterAddingMaxQuantity
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T438_iPhone_VerifySoldOutCallOutAfterAddingMaxQuantity : T438_MobileBase
    {
        public T438_iPhone_VerifySoldOutCallOutAfterAddingMaxQuantity(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SoldOutCallOutAfterAddingMaxQuantity(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T438_Emulator_VerifySoldOutCallOutAfterAddingMaxQuantity : T438_MobileBase
    {
        public T438_Emulator_VerifySoldOutCallOutAfterAddingMaxQuantity(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SoldOutCallOutAfterAddingMaxQuantity(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'Sold Out' callout is displayed on Sort page for user who adds the max QTY to cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5396
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T438
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5396"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T438")]
    public abstract class T438_MobileBase : TestsBaseMobile
    {
        protected T438_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Daily Sale sort page
            Browser.Navigate(Urls.LpDailySalesUrl);

            //Act : Click on any product with the Qty Left callout
            Sort.ClickProductWithQtyLeftCallout();
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not PDP page");

            //Act : On the PDP, make a note of SKU, Product Name and Price
            var pdpProductSku = ProductDetail.GetProductSku();
            var pdpProductName = ProductDetail.GetProductName();
            var pdpProductPrice = ProductDetail.GetProductPrice();
            var pdpProductPriceFormatted = TextActions.FormatToTwoDecimals((decimal)pdpProductPrice);

            //Act : Execute query and notedown SKU, Product Name and Price
            var productDetailsDb = ProductActions.GetShortSkuNameAndPrice(pdpProductSku);
            var priceOnPdpFormatted = TextActions.FormatToTwoDecimals(decimal.Parse(TextActions.GetPriceTextOnly(productDetailsDb.Price)));

            //Act : Select all the available quantity and Add To Cart
            ProductDetail.AddProductMaxQuantity();
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not a Cart page");

            //Act : Navigate back to Daily Sale sort page
            Browser.Navigate(Urls.LpDailySalesUrl);

            //Assert : Verify Sold Out callout displays for product added to Cart
            Assert.True(Sort.HasSoldOutCallOut(pdpProductSku), "Item does not have a sold out call out.");

            //Assert : Verify Product Name and Price match on site and in database
            Assert.Equals(pdpProductName, productDetailsDb.Name.Replace("&quot;", "\""), "Name does not match.");
            Assert.Equals(pdpProductPriceFormatted, priceOnPdpFormatted, "Price does not match.");
        }
    }
}