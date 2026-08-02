using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T7867_T7868_VerifyFunctionalityOfTheAddAllToCartOnTheWishList
{
    public class T7868_iPhone_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7868_MobileBase
    {
        public T7868_iPhone_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }

    
    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7868_AndroidPhone_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7868_MobileBase
    {
        public T7868_AndroidPhone_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7868_Emulator_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7868_MobileBase
    {
        public T7868_Emulator_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the Add All to Cart on the wishList.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10173
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7868
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10173"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7868")]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public abstract class T7868_MobileBase : TestsBaseMobile
    {
        protected T7868_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrangement
            User is signed in as a ESI-SIS
            User has added multiple items to the Wish List
            User is on the Wish List page
            */
            InitializeFunctionalTest(config, Urls.WishListPageUrl);
            WishList.WaitForEmptyWishListToLoad();
            const int numberOfProductsToWishList = 3;
            var wishListSkus = WishListWorkflow.AddMultipleAvailableItemsToWishList(Urls.TableLampsSortPageUrl, numberOfProductsToWishList);
            Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

            /*Act
            On the Wish List page, click the 'AddAllToCart' button.
            */
            WishList.AddAllWishlistSkusToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            var cartItemsQty = Cart.GetCountOfAllProductsInCart();
            var cartSkus = Cart.GetListOfCartSkus(Browser.PageUrl, cartItemsQty);

            //Assert. The items from the wishlist with the correct quantities is added to cart.
            Assert.Equals(numberOfProductsToWishList, cartItemsQty, "Wrong WishList QTY added to cart.");
            Assert.Equals(wishListSkus[0], cartSkus[0], "Wrong WishList Product added to cart.");
            Assert.Equals(wishListSkus[1], cartSkus[1], "Wrong WishList Product added to cart.");
            Assert.Equals(wishListSkus[2], cartSkus[2], "Wrong WishList Product added to cart.");
        }
    }
}
