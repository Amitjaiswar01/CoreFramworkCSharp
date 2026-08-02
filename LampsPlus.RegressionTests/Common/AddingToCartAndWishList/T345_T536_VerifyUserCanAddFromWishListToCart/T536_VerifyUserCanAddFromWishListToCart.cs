using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T345_T536_VerifyUserCanAddFromWishListToCart
{
    public class T536_VerifyUserCanAddFromWishListToCart
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
        //[Collection(LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
        public class T536_IPhone_VerifyUserCanAddFromWishListToCart : T536_MobileBase
        {
            public T536_IPhone_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void UserCanAddFromWishListToCart(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
        //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
        public class T536_Emulator_VerifyUserCanAddFromWishListToCart : T536_MobileBase
        {
            public T536_Emulator_VerifyUserCanAddFromWishListToCart(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void UserCanAddFromWishListToCart(string config) => Validate(config);
        }

        /// <summary>
        /// Verify that a user can add an item from a Wish List to the Cart.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5126
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T536
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5126"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T536")]
        public abstract class T536_MobileBase : TestsBaseMobile
        {
            protected T536_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                 User has added an item to a Wish List
                 User is on the Wish List page
                */
                InitializeFunctionalTest(config);
                Browser.Wait.ForDomReady(30);
                WishListWorkflow.AddSingleItemToWishList();
                Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

                /*Act
                  On the Wish List page, click the 'Add to Cart' button.
                */
                var wishListItemSku = WishList.GetWishListItemSku();
                var wishListItemQty = WishList.GetWishListItemQty();
                WishList.AddToCartByItemIndex(wishlistItemIndex:0);
                Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

                //Assert. The correct item and the correct quantity of the item is added to cart.
                var cartProductsAfter = Cart.GetListOfAllProductsOnCartPage();
                Assert.Equals(cartProductsAfter[0].Sku, wishListItemSku, "Wrong WishList SKU added to cart.");
                Assert.Equals(cartProductsAfter[0].Quantity, wishListItemQty, "Wrong WishList QTY added to cart.");
            }
        }
    }
}