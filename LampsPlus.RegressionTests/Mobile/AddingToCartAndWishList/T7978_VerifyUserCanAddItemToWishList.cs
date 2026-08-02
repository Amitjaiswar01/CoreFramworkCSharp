using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.AddingToCartAndWishList
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7978_IPhone_VerifyUserCanAddItemToWishList : T7978_MobileBase
    {
        public T7978_IPhone_VerifyUserCanAddItemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanAddItemToWishList(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7978_Emulator_VerifyUserCanAddItemToWishList : T7978_MobileBase
    {
        public T7978_Emulator_VerifyUserCanAddItemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanAddItemToWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can add an item to the wishlist page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10792
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7978
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10792"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7978")]
    public abstract class T7978_MobileBase : TestsBaseMobile
    {
        protected T7978_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Get the eligible product 
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetFreeShippingAndReturnShortSkus;

            //Act: Navigate to the Pdp page and note down product name and Qty
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not Pdp page");

            var pdpProductName = ProductDetail.GetProductName();
            var pdpProductQty = ProductDetail.GetProductQuantity();

            //Act: Click on Save button
            ProductDetail.AddToWishList();
            Assert.True(WishList.IsCurrentPage, "Current page is not wishlist page");

            //Assert: Verify correct Product Name and Qty is added to wishlist page
            Assert.Equals(pdpProductName, WishList.GetProductNameAndQtyFromWishlist()[0], "Incorrect product name is display in the wish list");
            Assert.Equals(pdpProductQty, WishList.GetProductNameAndQtyFromWishlist()[1], "Incorrect product qty is display in the wish list");
        }
    }
}