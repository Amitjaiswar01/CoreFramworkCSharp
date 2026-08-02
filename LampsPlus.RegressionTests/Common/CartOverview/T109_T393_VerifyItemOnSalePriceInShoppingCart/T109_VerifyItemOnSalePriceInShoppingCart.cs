using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T109_T393_VerifyItemOnSalePriceInShoppingCart
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T109_Windows_VerifyItemOnSalePriceInShoppingCart : T109_DesktopBase
    {
        public T109_Windows_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T109_Mac_VerifyItemOnSalePriceInShoppingCart : T109_DesktopBase
    {
        public T109_Mac_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T109_iPad_VerifyItemOnSalePriceInShoppingCart : T109_DesktopBase
    {
        public T109_iPad_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T109_TabletEmulator_VerifyItemOnSalePriceInShoppingCart : T109_DesktopBase
    {
        public T109_TabletEmulator_VerifyItemOnSalePriceInShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ItemOnSalePriceInShoppingCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a sale item shows the sale price in the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9909
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T109
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9909"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T109")]
    public abstract class T109_DesktopBase : TestsBaseDesktop
    {
        protected T109_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Add Non-Sale item in Cart
            InitializeFramework(config);
            var shortSku = ProductActions.GetItemNotOnSale;
            var saleShortSku = ProductActions.GetSkuForSaleCallout;

            Assert.DatabaseObject(shortSku, "ProductActions.GetProductNotOnSale()");
            Assert.DatabaseObject(saleShortSku, "ProductActions.GetSkuForSaleCallout()");
            
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel{Sku = shortSku });

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
            Assert.Equals(saleItemInCart.First().Price.TrimStart('$'), priceOnSale, "Sale price of the item in the cart does not match the price on the PDP.");
            Assert.Equals(nonSaleItemInCart.Count(), 1, "Incorrect number of non-Sale items in the cart.");
        }
    }
}
