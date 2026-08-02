using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T110_T394_VerifyClickOnItemInCartBringsUserToItemPdp
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T394_iPhone_VerifyClickOnItemInCartBringsUserToItemPdp : T394_MobileBase
    {
        public T394_iPhone_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T394_Emulator_VerifyClickOnItemInCartBringsUserToItemPdp : T394_MobileBase
    {
        public T394_Emulator_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking on an item in the cart brings the user to that PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9910
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T394
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9910"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T394")]
    public abstract class T394_MobileBase : TestsBaseMobile
    {
        protected T394_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Add Non-Sale item in Cart and get the total count and shortsku
            InitializeFramework(config);

            var shortSku = ProductActions.GetItemNotOnSale;
            Assert.DatabaseObject(shortSku, "ProductActions.GetProductNotOnSale()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            var cartItemsQty = Cart.GetCountOfAllProductsInCart();
            var cartSkus = Cart.GetListOfCartSkus(Browser.PageUrl, cartItemsQty);

            // Act : Tap on product image to navigate to PDP
            Cart.NavigateToPdpViaProductImageInCart();
            Assert.True(ProductDetail.IsCurrentPage, "Page is not PDP page");

            // Assert : The user is re-directed to the product page of the item that was tapped.
            Assert.Equals(cartSkus[0], ProductDetail.GetProductSku(), "The Product SKU is not matching.");

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Assert.True(Cart.IsCurrentPage, "current page is not cart page");

            // Act : Tap on product Name to navigate to PDP
            Cart.NavigateToPdpViaProductNameInCart();
            Assert.True(ProductDetail.IsCurrentPage, "Page is not PDP page");

            // Assert : The user is re-directed to the product page of the item that was tapped.
            Assert.Equals(cartSkus[0], ProductDetail.GetProductSku(), "The Product SKU is not matching.");
        }
    }
}
