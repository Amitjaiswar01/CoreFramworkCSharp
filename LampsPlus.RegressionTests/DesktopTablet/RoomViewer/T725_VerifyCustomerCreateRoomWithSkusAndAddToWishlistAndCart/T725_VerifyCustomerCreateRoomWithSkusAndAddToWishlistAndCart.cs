using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T725_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_Windows_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T725_DesktopBase
    {
        public T725_Windows_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CustomerCreateRoomWithSkusAndAddToWishlistAndCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_Mac_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T725_DesktopBase
    {
        public T725_Mac_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CustomerCreateRoomWithSkusAndAddToWishlistAndCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_iPad_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T725_DesktopBase
    {
        public T725_iPad_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CustomerCreateRoomWithSkusAndAddToWishlistAndCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T725_TabletEmulator_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart : T725_DesktopBase
    {
        public T725_TabletEmulator_VerifyCustomerCreateRoomWithSkusAndAddToWishlistAndCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CustomerCreateRoomWithSkusAndAddToWishlistAndCart(string config) => Validate(config);
    }


    /// <summary>
    /// Windows - Verify A Customer can Create a Same Sample Room with Different Skus, and Able to Add to Wishlist, a Selected Item to Cart, and Add All to Cart
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10242
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T725
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10242"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T725")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T725_DesktopBase : TestsBaseDesktop
    {
        protected T725_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - User has added 2 different products to the room
            InitializeFunctionalTest(config);
            var shortSkus = ProductActions.GetSkusThatHaveArOption();
            Assert.DatabaseObject(shortSkus, "ProductActions.GetSkusThatHaveArOption");
            var productsInDbList = RoomViewer.dataBaseList(shortSkus);

            RoomViewerWorkflow.AddMultipleItemsToRoom(shortSkus.ArProducts);
            
            var firstProductNameInDb = productsInDbList[0].ProductName;
            var secondProductNameInDb = productsInDbList[1].ProductName;
            var correctedProductName1 = RoomViewer.GetProductNameByShortSkuFromDb(firstProductNameInDb);
            var correctedProductName2 = RoomViewer.GetProductNameByShortSkuFromDb(secondProductNameInDb);
            var productsCount = shortSkus.ArProducts.Count;
            var productsInRoomViewer = RoomViewer.GetListOfAllProductsOnRoomViewer();
            
            // Assert - The correct products have been added to the room
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");
            Assert.Equals(($"{productsCount}"), RoomViewer.GetProductListCount(), "The product count does not match");
            Assert.Equals(correctedProductName1, productsInRoomViewer[0].Name, "The product name does not match");
            Assert.Equals(correctedProductName2, productsInRoomViewer[1].Name, "The product name does not match");
            Assert.Equals(decimal.Round(productsInDbList[0].RetailPriceInternet,2),decimal.Parse(productsInRoomViewer[0].Price),"Price does not match");
            Assert.Equals(decimal.Round(productsInDbList[1].RetailPriceInternet, 2), decimal.Parse(productsInRoomViewer[1].Price), "Price does not match");

            // Act - User adds second product to the wish list
            var cachedWishlistCount = WishList.WishListItemsCount;
            RoomViewer.AddingSecondProductToWishList();

            // Assert - The correct product has been added to the wishlist and wish list count has increased
            Assert.True(WishList.IsCurrentPage, "Current page is not wish list page");
            Assert.True(WishList.WishListItemsCount.Equals(cachedWishlistCount + 1),"Wish list count not increased");
            var wishListSku1 = WishList.GetWishListItemSku();
            Assert.Equals(productsInDbList[1].ShortSku, wishListSku1, "The SKU Matches");

            // Act - User adds first product to the wish list
            RoomViewer.OpenActiveRoom();
            var cachedWishlistCount1 = WishList.WishListItemsCount;
            RoomViewer.AddingFirstProductToWishList();
            Assert.True(WishList.IsCurrentPage, "Current page is not wish list page");

            // Assert - The correct product has been added to the wishlist and wish list count has increased
            Assert.True(WishList.WishListItemsCount.Equals(cachedWishlistCount1 + 1), "Wish list count not increased");
            Assert.Equals(productsInDbList[0].ShortSku, WishList.GetWishListItemSkuList(0), "The SKU does not Matches");
            Assert.Equals(productsInDbList[1].ShortSku, WishList.GetWishListItemSkuList(1), "The SKU does not Matches");

            // Act - User adds the first product to the cart
            RoomViewer.OpenActiveRoom();
            RoomViewer.AddToCart();

            // Assert - The correct product has been added to the cart
            Assert.True(Cart.IsCurrentPage, "Current Page is not Cart");
            var cartSku1 = Cart.GetListOfAllProductsOnCartPage();
            Assert.Equals(productsInDbList[1].ShortSku, cartSku1[0].Sku, "The SKU matches");

            // Act - User clicks on Add all to cart Link
            Browser.Navigate(Urls.AugmentedRealityUrl);
            Assert.True(RoomViewer.IsCurrentPage, "Current page is not Room Viewer Page");
            RoomViewer.AddAllToCart();

            // Assert - The correct products have been added to the cart
            Assert.True(Cart.IsCurrentPage,"Current Page is not Cart");
            var cartSku2 = Cart.GetListOfAllProductsOnCartPage();
            Assert.Equals(productsInDbList[0].ShortSku, cartSku2[0].Sku, "The SKU does not matches");
            Assert.Equals(productsInDbList[1].ShortSku, cartSku1[1].Sku, "The SKU does not matches");
        }
    }
}