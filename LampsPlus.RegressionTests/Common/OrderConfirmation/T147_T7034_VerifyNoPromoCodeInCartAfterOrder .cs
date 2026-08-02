using Automation.Framework.Utilities;
using Castle.Core.Internal;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]

    public class T147_Windows_VerifyNoPromoCodeInCartAfterOrder : T147_DesktopBase
    {
        public T147_Windows_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);        
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T147_Mac_VerifyNoPromoCodeInCartAfterOrder : T147_DesktopBase
    {
        public T147_Mac_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T147_iPad_VerifyNoPromoCodeInCartAfterOrder : T147_DesktopBase
    {
        public T147_iPad_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T147_TabletEmulator_VerifyNoPromoCodeInCartAfterOrder : T147_DesktopBase
    {
        public T147_TabletEmulator_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7034_iPhone_VerifyNoPromoCodeInCartAfterOrder : T7034_MobileBase
    {
        public T7034_iPhone_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7034_Emulator_VerifyNoPromoCodeInCartAfterOrder : T7034_MobileBase
    {
        public T7034_Emulator_VerifyNoPromoCodeInCartAfterOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void NoPromoCodeInCartAfterOrder(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can submit an order with a combo kit sku and valid promo code and that the promo code is removed after an order submission.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6526
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T147
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6526"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T147")]
    public abstract class T147_DesktopBase : T147_T7034_Base
    {
        protected T147_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify a user can submit an order with a combo kit sku and valid promo code and that the promo code is removed after an order submission.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5359
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7034
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5359"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7034")]
    public abstract class T7034_MobileBase : T147_T7034_Base
    {
        protected T7034_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            var promoCode = PromoCodeList.AutoPromoCodeTest;
            var comboKitSkuModel = new ProductModel() { Sku = ProductActions.GetRandomComboKitSku, Quantity = 1 };

            //Step 1: Navigate to the Coupon URL and add an item to the cart.
            Browser.Navigate($"{Urls.SortPagePromoCodeUrl}{promoCode.Name}");

            Browser.Wait.IsVisibleElement(By.CssSelector(WishList.HideMobileOverlayClass.ToCssClassSelector()));

            ShoppingCartWorkflow.AddItemToCartBySku(comboKitSkuModel);

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            Assert.True(CartOverview.ProductSkuLabelCart.Text.ToLower().Contains(comboKitSkuModel.Sku.ToLower()), $"ComboKitSku {comboKitSkuModel.Sku} was not added to the cart");
            Assert.True(CartOverview.PromoCodeLabel.Text.ToLower().Contains(promoCode.Name.ToLower()), $"Coupon code \"{promoCode.Name}\" was not applied to the cart");

            var promoText = CartOverview.PromoCode.Text.ToLower();

            Assert.True(promoText.Contains(PromoCodeList.AutoPromoCodeTest.Name.ToLower()), $"Discount: \"{PromoCodeList.AutoPromoCodeTest.Name}\" was not applied to the cart");

            //Step 2: Proceed to Shipping and Payment pages.
            CartOverview.ClickCheckOutNowButton();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            CustomerAddressInformation.EnterShippingAddress(new IntAddress(), isIntAddress:true);

            OrderSummaryBlock.ProceedToPaymentButton.Click();
            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourIntlOrderButtonId));

            Browser.Wait.WaitForAjaxComplete();
            Browser.Wait.ForDomReady();
            Payment.PlaceInternationalOrder();

            Browser.Wait.IsVisibleElement(By.XPath(OrderConfirmation.OrderIdHeadingXpath));
            
            Assert.False(OrderConfirmation.GetOrderId.IsNullOrEmpty(), "Order id was not found");
            Assert.Equals(OrderConfirmation.GetPromoCodeLabel(), "Promotions and Discounts:", "Promotions and Discounts: was not found on the Order confirmation page");

            Browser.Navigate(Urls.HomePageUrl);

            //Step 3: Add another item to the cart after placing an order and ensure the promo code is no longer present.
            ShoppingCartWorkflow.AddItemToCartBySku(comboKitSkuModel);

            Assert.True(CartOverview.ProductSkuLabelCart.Text.ToLower().Contains(comboKitSkuModel.Sku.ToLower()), $"ComboKitSku {comboKitSkuModel} was not added to the cart");
            Assert.True(CartOverview.OrderSummaryBlockLabel(CartOverview.PromoCodeDiscountTotalPrefix) == null, $"{CartOverview.CouponAndMemberSpecialPriceSavingsLabel} was found in the cart");
        }
    }


    public abstract class T147_T7034_Base : OrderConfirmationTestsBase
    {
        protected T147_T7034_Base(ITestOutputHelper output) : base(output) { }
        
        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var promoCode = PromoCodeList.AutoPromoCodeTest;
            var comboKitSkuModel = new ProductModel() { Sku = ProductActions.GetRandomComboKitSku, Quantity = 1 };

            //Step 1: Navigate to the Coupon URL and add an item to the cart.
            Browser.Navigate($"{Urls.SortPagePromoCodeUrl}{promoCode.Name}");

            Browser.Wait.ForDomReady();

            ShoppingCartWorkflow.AddItemToCartBySku(comboKitSkuModel);

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            Assert.True(CartOverview.ProductSkuLabelCart.Text.ToLower().Contains(comboKitSkuModel.Sku.ToLower()), $"ComboKitSku {comboKitSkuModel.Sku} was not added to the cart");
            Assert.True(CartOverview.PromoCodeLabel.Text.ToLower().Contains(promoCode.Name.ToLower()), $"Coupon code \"{promoCode.Name}\" was not applied to the cart");
            Assert.Equals(CartOverview.GetPromoCodeLabel(), CartOverview.CouponAndMemberSpecialPriceSavingsLabel, $"{CartOverview.CouponAndMemberSpecialPriceSavingsLabel} was not found in the cart");

            //Step 2: Proceed to Shipping and Payment pages.
            CartOverview.ClickCheckOutNowButton();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            CustomerAddressInformation.EnterShippingAddress(new IntAddress(), isIntAddress:true);

            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.ForDomReady();
            Payment.PlaceInternationalOrder();

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            Assert.False(OrderConfirmation.GetOrderIdNumber.IsNullOrEmpty(), "Order id was not found");
            Assert.Equals(OrderConfirmation.GetPromoCodeLabel(), OrderConfirmation.CouponAndMemberSpecialPriceSavingsLabel, $"{CartOverview.CouponAndMemberSpecialPriceSavingsLabel} was not found on the Order confirmation page");

            //Step 3: Add another item to the cart after placing an order and ensure the promo code is no longer present.
            ShoppingCartWorkflow.AddItemToCartBySku(comboKitSkuModel);

            Assert.True(CartOverview.ProductSkuLabelCart.Text.ToLower().Contains(comboKitSkuModel.Sku.ToLower()), $"ComboKitSku {comboKitSkuModel} was not added to the cart");
            Assert.True(CartOverview.OrderSummaryBlockLabel(CartOverview.PromoCodeDiscountTotalPrefix) == null, $"{CartOverview.CouponAndMemberSpecialPriceSavingsLabel} was found in the cart");
        }
    }
}
