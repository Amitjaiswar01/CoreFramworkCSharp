using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.CartOverview;


namespace LampsPlus.RegressionTests.Common.CartOverview
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_Windows_VerifyValidPromoCodeCanBeAddedToTheCart : T132_DesktopBase
    {
        public T132_Windows_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_Mac_VerifyValidPromoCodeCanBeAddedToTheCart : T132_DesktopBase
    {
        public T132_Mac_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_iPad_VerifyValidPromoCodeCanBeAddedToTheCart : T132_DesktopBase
    {
        public T132_iPad_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T132_TabletEmulator_VerifyValidPromoCodeCanBeAddedToTheCart : T132_DesktopBase
    {
        public T132_TabletEmulator_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T398_iPhone_VerifyValidPromoCodeCanBeAddedToTheCart : T398_MobileBase
    {
        public T398_iPhone_VerifyValidPromoCodeCanBeAddedToTheCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ValidPromoCodeCanBeAddedToTheCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
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
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5265
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T132
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5265"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T132")]
    public abstract class T132_DesktopBase : T132_T398_Base
    {
        protected T132_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyOrderTotals(int discountRate)
        {
            CartOverview.ChangeShippingOptionsLink.Click();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);

            CartOverview.ClickShippingOptionShipTabSearchButton();
            CartOverview.ClickShippingOptionShipTabUpdateButton();

            var discountTotal = CartOverview.CalculatePromoDiscount(discountRate, true);
            var subtotal = CartOverview.CalculateSubTotal(discountRate, true);
            
            CartOverview.ClickPromotionalCodeLink();
            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            var orderTotal = subtotal + CartOverview.GetShippingCost() + CartOverview.GetSaleTax();
            var discountText = CartOverview.PromoCodeLabel.Text;

            Assert.True(discountText.Contains("APPLIED CODE: AutoPromoCodeTest"), "APPLIED CODE: AutoPromoCodeTest is not present on cart page");
            Assert.Equals($"{CartOverview.CouponAndMemberSpecialPriceSavingsLabel}", CartOverview.GetPromoCodeLabel(), "Coupon & Member Special Price Savings amount not displayed");
            Assert.Equals(discountTotal, CartOverview.GetPromoCodeDiscountPrice(), "Discount price do not match.");
            Assert.Equals(subtotal, CartOverview.GetSubTotal(), "Value of subtotal do not match.");
            Assert.Equals(orderTotal, CartOverview.GetOrderTotalCost(), "Order total do not match.");

            CartOverview.RemovePromoCode();
        }
    }


    /// <summary>
    /// Verify that the user can add a valid promo code to the cart.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5111
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T398
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5111"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T398")]
    public abstract class T398_MobileBase : T132_T398_Base
    {
        protected T398_MobileBase(ITestOutputHelper output) : base(output) { }
        
        protected override void VerifyOrderTotals(int discountRate)
        {
            var discountTotal = CartOverview.CalculatePromoDiscount(discountRate, true);
            var subtotal = CartOverview.CalculateSubTotal(discountRate, true);

            CartOverview.ClickPromotionalCodeLink();
            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            Browser.ScrollToTopOfWindow();

            CartOverview.ChangeShippingOptionsLink.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.JsShippingCountryClass.ToCssClassSelector()));

            CartOverview.ShippingZipField.SendKeys("99501");
            CartOverview.ClickShippingOptionShipTabUpdateButton();
            CartOverview.CloseShippingOptionsOverlay.Click();

            var orderTotal = subtotal + CartOverview.GetShippingCost() + CartOverview.GetSaleTaxCost();
            var discountText = CartOverview.PromoCodeLabel.Text.Replace("\r\n", " ").ToLower();
            var discountValueUi = decimal.Parse(CartOverview.PromoDiscount.Text.Split('$')[1]);

            Assert.True(discountText.Contains("promotions and discounts:" + " " + PromoCodeList.AutoPromoCodeTest.Name.ToLower()), "Promotions and Discounts: AutoPromoCodeTest is not present on cart page");
            Assert.Equals(discountTotal, discountValueUi, "Discount price do not match.");
            Assert.Equals(subtotal, CartOverview.GetSubTotalCost(), "Value of subtotal do not match.");
            Assert.Equals(orderTotal, CartOverview.GetOrderTotalCost(), "Order total do not match.");

            CartOverview.RemovePromoCode();
        }
    }


    public abstract class T132_T398_Base : ShoppingCartTestsBase
    {
        protected T132_T398_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var discountRate = PromoCodeList.AutoPromoCodeTest.DiscountPercentage;

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Browser.ClickByJs(GlobalLocators.AddToCartButton);
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            VerifyOrderTotals(discountRate);
        }

        /// <summary>
        /// Verify Order Totals for given discount rate.
        /// </summary>
        /// <param name="discountRate"></param>
        protected abstract void VerifyOrderTotals(int discountRate);
    }
}
