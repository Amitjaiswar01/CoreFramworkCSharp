using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T7867_T7868_VerifyFunctionalityOfTheAddAllToCartOnTheWishList
{
    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7867_Windows_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7867_DesktopBase
    {
        public T7867_Windows_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7867_Mac_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7867_DesktopBase
    {
        public T7867_Mac_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7867_iPad_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7867_DesktopBase
    {
        public T7867_iPad_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7867_TabletEmulator_VerifyFunctionalityOfTheAddAllToCartOnTheWishList : T7867_DesktopBase
    {
        public T7867_TabletEmulator_VerifyFunctionalityOfTheAddAllToCartOnTheWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void VerifyFunctionalityOfTheAddAllToCartOnTheWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the Add All to Cart on the wishList.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10173
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7867
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10173"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7867")]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    
    public abstract class T7867_DesktopBase : TestsBaseDesktop
    {
        protected T7867_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange
            User is signed in as a ESI-SIS
            User has added multiple items to the Wish List
            User is on the Wish List page
            */
            InitializeFunctionalTest(config, Urls.WishListPageUrl);

            var numberOfProductsToWishList = 3;
            var wishListskus = WishListWorkflow.AddMultipleItemsToWishList(Urls.TableLampsSortPageUrl, numberOfProductsToWishList);
            Browser.Navigate(Urls.WishListPageUrl);
            Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

            /*Act
            On the Wish List page, adding all the SKUs to the cart.
            */
            WishList.AddAllWishlistSkusToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            var cartItemsQty = Cart.GetCountOfAllProductsInCart();
            var cartskus = Cart.GetListOfCartSkus(Browser.PageUrl, cartItemsQty);

            //Assert. The items from the wishlist with the correct quantities is added to cart.
            Assert.Equals(cartItemsQty, numberOfProductsToWishList, "Wrong WishList QTY added to cart.");
            Assert.Equals(wishListskus[0], cartskus[0], "Wrong WishList Product added to cart.");
            Assert.Equals(wishListskus[1], cartskus[1], "Wrong WishList Product added to cart.");
            Assert.Equals(wishListskus[2], cartskus[2], "Wrong WishList Product added to cart.");
        }
    }
}
