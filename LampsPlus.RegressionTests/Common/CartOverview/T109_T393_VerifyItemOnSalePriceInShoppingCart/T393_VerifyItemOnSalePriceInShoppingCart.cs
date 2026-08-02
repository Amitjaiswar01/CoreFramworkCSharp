using System.Linq;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T109_T393_VerifyItemOnSalePriceInShoppingCart
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T393_iPhone_VerifyItemOnSalePriceInShoppingCart : T393_MobileBase
    {
        public T393_iPhone_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T393_Emulator_VerifyItemOnSalePriceInShoppingCart : T393_MobileBase
    {
        public T393_Emulator_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a sale item shows the sale price in the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9909
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T393
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9909"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T393")]
    public abstract class T393_MobileBase : TestsBaseMobile
    {
        protected T393_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Add Non-Sale item in Cart
            InitializeFramework(config);
            var shortSku = ProductActions.GetItemNotOnSale;
            var saleShortSku = ProductActions.GetSkuForSaleCallout;

            Assert.DatabaseObject(shortSku, "ProductActions.GetProductNotOnSale()");
            Assert.DatabaseObject(saleShortSku, "ProductActions.GetSkuForSaleCallout()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            // Act - Add Sale item in Cart
            ProductDetail.NavigateToProductDetailByShortSku(saleShortSku);

            var priceOnSale = ProductDetail.GetShortSkuPrice();
            priceOnSale = TextActions.RemoveTextBeforeAndIncludingCharacter(priceOnSale, '$');

            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            var itemsInCart = Cart.GetListOfAllProductsOnCartPage();
            var saleItemInCart = Cart.GetProductDetailsInCart(saleShortSku);
            var nonSaleItemInCart = Cart.GetProductDetailsInCart(shortSku); ;

            // Assert - Verify added items appear in Cart and Sale item shows Sale price
            Assert.Equals(itemsInCart.Count, 2, "Incorrect number of items in the cart.");
            Assert.Equals(saleItemInCart.Count, 1, "Incorrect number of Sale items in the cart.");
            Assert.Equals(saleItemInCart.First().Price.TrimStart('$'), priceOnSale.Replace("Price:\r\n", "").Replace("$", ""), "Sale price of the item in the cart does not match the price on the PDP.");
            Assert.Equals(nonSaleItemInCart.Count(), 1, "Incorrect number of non-Sale items in the cart.");
        }
    }
}
