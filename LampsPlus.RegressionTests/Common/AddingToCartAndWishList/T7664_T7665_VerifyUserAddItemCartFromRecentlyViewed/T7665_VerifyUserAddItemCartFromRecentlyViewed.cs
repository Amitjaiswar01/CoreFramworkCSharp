using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T7664_T7665_VerifyUserAddItemCartFromRecentlyViewed
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7665_iPhone_VerifyUserAddItemCartFromRecentlyViewed : T7665_MobileBase
    {
        public T7665_iPhone_VerifyUserAddItemCartFromRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyUserAddItemCartFromRecentlyViewed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7665_Emulator_VerifyUserAddItemCartFromRecentlyViewed : T7665_MobileBase
    {
        public T7665_Emulator_VerifyUserAddItemCartFromRecentlyViewed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyUserAddItemCartFromRecentlyViewed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can add an item to the cart from the Recently Viewed page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10101
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7665
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10101"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7665")]
    public abstract class T7665_MobileBase : TestsBaseMobile
    {
        protected T7665_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on the Homepage.
            InitializeFunctionalTest(config);

            //Act : User has navigated to 4 PDPs 
            var shortSkus = ProductActions.GetListableInStockShortSku(4);
            ProductDetail.NavigateToEachProductDetailPage(shortSkus);

            //Act : On the 4th PDP, Click on "View All" Button to navigate to Recently Viewed page.
            ProductDetail.NavigateToRecentlyViewedPage();
            Assert.True(SortFullPageCertona.IsCurrentPage, "User is not on the Certona Page");

            //Assert : Verify all four products are on the Recently Viewed page.
            Assert.Equals(SortFullPageCertona.GetNumberOfProductsOnPage(), 4, "There more or less than 4 products on the page");
            Assert.True(shortSkus.Contains(SortFullPageCertona.GetProductContentsOnPage(0).Sku), "Correct Product not Displayed on Recently Viewed Page.");
            Assert.True(shortSkus.Contains(SortFullPageCertona.GetProductContentsOnPage(1).Sku), "Correct Product not Displayed on Recently Viewed Page.");
            Assert.True(shortSkus.Contains(SortFullPageCertona.GetProductContentsOnPage(2).Sku), "Correct Product not Displayed on Recently Viewed Page.");
            Assert.True(shortSkus.Contains(SortFullPageCertona.GetProductContentsOnPage(3).Sku), "Correct Product not Displayed on Recently Viewed Page.");

            var certonaSku = SortFullPageCertona.GetProductContentsOnPage(0).Sku;

            //Act : User has clicked on the Add to Cart button for one of the items.
            SortFullPageCertona.AddToCartOnCertonaSortPage();
            Assert.True(Cart.IsCurrentPage, "User is not on the cart page");

            var cartProductDetails = Cart.GetListOfAllProductsOnCartPage();
            var cartProductSku = cartProductDetails[0].Sku;

            //Assert : Verify that the item is added to the cart.
            Assert.Equals(certonaSku, cartProductSku, "Incorrect SKU is added to the cart");
        }
    }
}