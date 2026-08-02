using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T351_T538_VerifyQtyAddedFromPdpToCart
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T351_Windows_VerifyQtyAddedFromPdpToCart : T351_DesktopBase
    {
        public T351_Windows_VerifyQtyAddedFromPdpToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyQtyAddedFromPdpToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T351_Mac_VerifyQtyAddedFromPdpToCart : T351_DesktopBase
    {
        public T351_Mac_VerifyQtyAddedFromPdpToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyQtyAddedFromPdpToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T351_iPad_VerifyQtyAddedFromPdpToCart : T351_DesktopBase
    {
        public T351_iPad_VerifyQtyAddedFromPdpToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyQtyAddedFromPdpToCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T351_TabletEmulator_VerifyQtyAddedFromPdpToCart : T351_DesktopBase
    {
        public T351_TabletEmulator_VerifyQtyAddedFromPdpToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyQtyAddedFromPdpToCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the entered quantity amount for an item gets added to the cart properly. 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10103
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T351
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10103"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T351")]
    public abstract class T351_DesktopBase : TestsBaseDesktop
    {
        protected T351_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to a PDP which has Inventory > 1 
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetSkuThatHasQuantityGreaterThanTwenty;
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP");

            //Act : On PDP, add Quantity > 1 and add the item to Cart 
            var quantity = MathHelper.GetRandomNumber(2, 20);
            ProductDetail.ChangeProductQuantity(quantity.ToString());
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on the cart page");

            var cartItem = Cart.GetListOfAllProductsOnCartPage();
            var cartSku = cartItem[0].Sku;
            var cartQty = cartItem[0].Quantity;

            //Assert : Verify the correct item and the correct quantity of the item is added to cart
            Assert.Equals(sku, cartSku, "Sku added in the Cart is Incorrect");
            Assert.Equals(quantity, cartQty, "Quantity added in the Cart is Incorrect");
        }
    }
}