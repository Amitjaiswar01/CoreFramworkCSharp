using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T132_T398_VerifyValidPromoCodeCanBeAddedToCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T398_iPhone_VerifyValidPromoCodeCanBeAddedToTheCart : T398_MobileBase
    {
        public T398_iPhone_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T398_Emulator_VerifyValidPromoCodeCanBeAddedToTheCart : T398_MobileBase
    {
        public T398_Emulator_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can add a valid promo code to the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9912
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T398
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9912"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T398")]
    public abstract class T398_MobileBase : TestsBaseMobile
    {
        protected T398_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            User has valid promo code and knows discount percentage 
            User has identified SKU and added it to the cart
             */
            InitializeFramework(config);

            var discountRate = PromoCodeList.AutoPromoCodeTest.DiscountPercentage;
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(shortSku));
            Assert.True(Cart.IsCurrentPage, "User is not on cart page.");

            //Act: Calculate values for promo code discount and order subtotal
            var calculatedPromoDiscount = Cart.GetCalculatedPromoDiscount(discountRate, true);
            var calculatedSubtotal = Cart.GetCalculateSubTotal(discountRate, true);

            //Act: Click on Add Promo Code link and apply promo code
            Cart.OpenPromoCodeEntryField();
            Cart.UpdatePromoCode(PromoCodeList.AutoPromoCodeTest.Name);

            /*Act:
            In the cart, click on the 'Standard Shipping' link.
            Enter zip code '99501' in the 'Enter ZIP/Postal Code' and click the SEARCH button
            Select Standard Shipping option and click the UPDATE button
             */
            Browser.ScrollToTopOfWindow();
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, zipCode:ZipCodeList.Anchorage, 0);

            //Act: Calculate Order Total
            var calculatedOrderTotal = calculatedSubtotal + Cart.GetShippingCost() + Cart.GetSaleTaxAmount();

            //Act: Get Text for Applied Promo Code and PromoCode Discount displayed on Cart page
            var promoCodeText = Cart.GetPromoCodeStatusMessage();
            var promoCodeDiscountDisplayed= Cart.GetPromoCodeDiscountDisplayed();

            //Assert: Verify Promotions and Discounts text is present on cart page
            Assert.True(promoCodeText.Contains("promotions and discounts:" + " " + PromoCodeList.AutoPromoCodeTest.Name.ToLower()), "Promotions and Discounts: AutoPromoCodeTest is not present on cart page");

            //Assert: Verify calculated Discount matches with the applied discount displayed on cart page
            Assert.Equals(calculatedPromoDiscount, promoCodeDiscountDisplayed, "Discount price do not match.");

            //Assert: Verify calculated subtotal cost matches with subtotal displayed in order summary block
            Assert.Equals(calculatedSubtotal, Cart.GetSubTotal(), "Subtotal value do not match.");

            //Assert: Verify calculated order total matches with order total displayed in order summary block
            Assert.Equals(calculatedOrderTotal, Cart.GetOrderTotalCost(), "Order total do not match.");

            //Data Cleanup
            Cart.RemovePromoCode();
        }
    }
}