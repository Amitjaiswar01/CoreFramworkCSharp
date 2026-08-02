using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T354_T539_VerifyCorrectQtyAddedToWishListAddingItemPdp
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T539_iPhone_VerifyCorrectQtyAddedToWishListAddingItemPdp : T539_MobileBase
    {
        public T539_iPhone_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T539_Emulator_VerifyCorrectQtyAddedToWishListAddingItemPdp : T539_MobileBase
    {
        public T539_Emulator_VerifyCorrectQtyAddedToWishListAddingItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyCorrectQtyAddedToWishListAddingItemPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the correct QTY is added the Wish List when adding an item from the PDP page
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10104
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T539
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10104"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T539")]
    public abstract class T539_MobileBase : TestsBaseMobile
    {
        protected T539_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on the Product Detail Page for the SKU with Inventory > 20
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetSkuThatHasQuantityGreaterThanTwenty;
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "user is not on the PDP");

            //Act : Type in any digit > 1 in the Quantity Field and add the Product to WishList 
            var quantity = MathHelper.GetRandomNumber(2, 20).ToString();
            ProductDetail.ChangeProductQuantity(quantity);
            ProductDetail.AddToWishList();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "User is not on the Wish List Page");

            var wishListSku = WishList.GetWishListItemSku();
            var wishListQty = WishList.GetWishListProductQty(0);

            //Assert : Verify the quantity entered on the PDP for the SKU is the quantity on the Wish List
            Assert.Equals(sku, wishListSku, "Item Added in the Wish List is Incorrect");
            Assert.Equals(quantity, wishListQty, "Quantity Added in the Wish List is Incorrect");
        }
    }
}