using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T132_T398_VerifyValidPromoCodeCanBeAddedToCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_Windows_VerifyValidPromoCodeCanBeAddedToCart : T132_DesktopBase
    {
        public T132_Windows_VerifyValidPromoCodeCanBeAddedToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T132. Rework - CI-3358")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_Mac_VerifyValidPromoCodeCanBeAddedToCart : T132_DesktopBase
    {
        public T132_Mac_VerifyValidPromoCodeCanBeAddedToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T132. Rework - CI-3358")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_iPad_VerifyValidPromoCodeCanBeAddedToCart : T132_DesktopBase
    {
        public T132_iPad_VerifyValidPromoCodeCanBeAddedToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_TabletEmulator_VerifyValidPromoCodeCanBeAddedToCart : T132_DesktopBase
    {
        public T132_TabletEmulator_VerifyValidPromoCodeCanBeAddedToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can add a valid promo code to the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9912
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T132
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9912"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T132")]
    public abstract class T132_DesktopBase : TestsBaseDesktop
    {
        protected T132_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
           User has valid promo code and knows discount percentage 
           User has identified SKU and added it to the cart
            */
            InitializeFunctionalTest(config);

            var discountRate = PromoCodeList.AutoPromoCodeTest.DiscountPercentage;
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(shortSku));
            Assert.True(Cart.IsCurrentPage, "User is not on cart page.");

            /*Act:
            In the cart, click on the 'Standard Shipping' link.
            Enter zip code '91311' in the 'Enter ZIP/Postal Code' and click the SEARCH button
            Select Standard Shipping option and click the UPDATE button
             */
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.Chatsworth, 0);

            //Act: Calculate values for promo code discount and order subtotal
            var calculatedPromoDiscount = Cart.GetCalculatedPromoDiscount(discountRate,true);
            var calculatedSubtotal = Cart.GetCalculateSubTotal(discountRate, true);

            //Act: Click on Add Promo Code link and apply promo code
            Cart.OpenPromoCodeEntryField();
            Cart.UpdatePromoCode(PromoCodeList.AutoPromoCodeTest.Name);

            //Act: Calculate Order Total
            var calculatedOrderTotal = calculatedSubtotal + Cart.GetShippingCost() + Cart.GetSaleTaxAmount();

            //Act: Get Text for Applied Promo Code on Cart page
            var promoCodeText = Cart.GetPromoCodeStatusMessage();

            //Assert: Verify Promo Code Applied Text is displayed after promo code is applied
            Assert.True(promoCodeText.Contains("APPLIED CODE: AutoPromoCodeTest"), "APPLIED CODE: AutoPromoCodeTest is not present on cart page");

            //Assert: Verify "Promotions and Discounts:" text is present in order summary block
            Assert.True(Cart.IsPromoCodePrefixVisible, "Promotions and Discounts text is not present in order summary block.");

            //Assert: Verify calculated Discount matches with the applied discount displayed in order summary block
            Assert.Equals(calculatedPromoDiscount, Cart.GetDiscountTotalCost(), "Discount Price do not match.");

            //Assert: Verify calculated subtotal cost matches with subtotal displayed in order summary block
            Assert.Equals(calculatedSubtotal, Cart.GetSubTotal(), "Subtotal value do not match.");

            //Assert: Verify calculated order total matches with order total displayed in order summary block
            Assert.Equals(calculatedOrderTotal, Cart.GetOrderTotalCost(), "Order Total do not match.");

            //Data Cleanup
            Cart.RemovePromoCode();
        }
    }
}