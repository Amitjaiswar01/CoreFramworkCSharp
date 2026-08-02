using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T345_T536_VerifyUserCanAddFromWishListToCart
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T345_Windows_VerifyUserCanAddFromWishListToCart : T345_DesktopBase
    {
        public T345_Windows_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanAddFromWishListToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T345_Mac_VerifyUserCanAddFromWishListToCart : T345_DesktopBase
    {
        public T345_Mac_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanAddFromWishListToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T345_iPad_VerifyUserCanAddFromWishListToCart : T345_DesktopBase
    {
        public T345_iPad_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanAddFromWishListToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T345_TabletEmulator_VerifyUserCanAddFromWishListToCart : T345_DesktopBase
    {
        public T345_TabletEmulator_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCanAddFromWishListToCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can add an item from a Wish List to the Cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5351
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T345
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5351"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T345")]
    public abstract class T345_DesktopBase : TestsBaseDesktop
    {
        protected T345_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrangement
             User has added an item to a Wish List
             User is on the Wish List page
             */
            InitializeFunctionalTest(config, Urls.WishListPageUrl);
            WishList.EmptyWishList();
            WishListWorkflow.AddSingleItemToWishList();
            Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

            // Act : On the Wish List page, click the 'Add to Cart' button.
            var wishListItemSku = WishList.GetWishListItemSku();
            var wishListItemQty = WishList.GetWishListItemQty();
            WishList.AddToCartByItemIndex(wishlistItemIndex:0);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Assert :  The correct item and the correct quantity of the item is added to cart.
            var cartProductsAfter = Cart.GetListOfAllProductsOnCartPage();
            Assert.Equals(cartProductsAfter[0].Sku, wishListItemSku, "Wrong WishList SKU added to cart.");
            Assert.Equals(cartProductsAfter[0].Quantity, wishListItemQty, "Wrong WishList QTY added to cart.");
        }
    }
}