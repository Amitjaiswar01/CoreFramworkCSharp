using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Net;
using System.Threading;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using xRetry;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T155_Windows_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T155_DesktopBase
    {
        public T155_Windows_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T155. Rework - ACD-10910")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T155_Mac_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T155_DesktopBase
    {
        public T155_Mac_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T155_iPad_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T155_DesktopBase
    {
        public T155_iPad_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T155_TabletEmulator_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T155_DesktopBase
    {
        public T155_TabletEmulator_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T7041_iPhone_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T7041_MobileBase
    {
        public T7041_iPhone_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T7041_Emulator_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage : T7041_MobileBase
    {
        public T7041_Emulator_VerifyOrderWithCreditCardNavigateToOrderConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void OrderWithCreditCardNavigateToOrderConfirmationPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can submit an order with a Credit Card and does not see CC info when navigating back to OC page after logout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6517
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T155
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6517"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T155")]
    public abstract class T155_DesktopBase : T155_T7041_Base
    {
        protected T155_DesktopBase(ITestOutputHelper output) : base(output) { }

        public override void SaveDefaultAddressAndPayment()
        {
            //Adding default Address
            Browser.Navigate(Urls.ManageAccountPageUrl);
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ShippingAddressesLinkXpath));
            ManageAccount.ManageShippingAddressesLinkForElement.Click();
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ManageShippingAddressContentXpath));
            ManageAccount.BtnAddShippingAddress.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));
            ManageAccountWorkflow.AddNewShippingAddressToModal(new Address());
            ManageAccount.BtnSaveShippingAddress.Click();

            //Adding default Payment Details
            Browser.Wait.IsInvisibleElement(By.Id(GlobalLocators.LpModalBackdropId));
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ShippingAddressesLinkXpath));
            ManageAccount.ManagePaymentOptionsLinkForElement.Click();
            Browser.Wait.ForDomReady();
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();
        }

        public override void VerifyOrderSummaryValues()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            CartOverview.RemovePromoCode();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.AddPromoCodeLinkClass.ToCssClassSelector()));
            CartOverview.CartPromotionalButton.Click();

            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.JsChangeShippingOptionsClass));
            CartOverview.ChangeShippingOptionsLink.Click();

            Browser.Wait.IsVisibleElement(By.Id(CartOverview.SelectDeliveryOptionsModalId));

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys("91311");
            CartOverview.ClickShippingOptionShipTabSearchButton();

            Browser.Wait.ForDomReady();

            CartOverview.ClickShippingOptionShipTabUpdateButton();

            //storing cart values
            var cartProductTotal = Convert.ToString(CartOverview.GeProductTotal(0)).Trim();
            var promotionsAndDiscounts = Convert.ToString(CartOverview.GetActualPromoCodeDiscountPrice()).TrimStart();
            var cartShippingAndProcessing = Convert.ToString(CartOverview.GetShippingTotals(3)).Trim();
            var cartTax = Convert.ToString(CartOverview.GetTaxTotal(4)).Trim();
            var cartOrderTotal = Convert.ToString(CartOverview.GetOrderTotalCost()).Trim();

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.XPath(Shipping.ProceedPaymentXpath));
            Shipping.ProceedToBilling.Click();

            const string securityCode = "111";

            Browser.Wait.IsVisibleElement(By.ClassName(Payment.CardVerificationClass));

            Payment.CardVerificationElement.SendKeys(securityCode);

            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourOrderButtonId));
            Payment.PlaceOrderButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.LincOptinWidgetClass.ToCssClassSelector()));
            Assert.PageUrl(Urls.OrderConfirmationPageUrl, Browser.PageUrl, "User is not taken to the Order Confirmation page.");

            //Storing Order Confirmation values
            var orderId = OrderConfirmation.GetOrderIdNumber;
            var shortSku = OrderConfirmation.ProductSkuOrder;
            var productName = OrderConfirmation.ProductNameText;
            var productTotal = OrderConfirmation.ProductTotalValue.Text;
            var ocPromotionDiscount = Convert.ToString(OrderConfirmation.PromotionAndDiscountsTotal.Text).Substring(27);
            var ocShipping = OrderConfirmation.ShippingAndProcessingTotal.Text;
            var ocTax = OrderConfirmation.TaxTotal.Text;
            var orderTotal = OrderConfirmation.OrderTotalValue.Text;

            Assert.NotNull(orderId, "Order ID is not displayed");
            Assert.NotNull(shortSku, "ShortSKU is not displayed");
            Assert.NotNull(productName, "Product Name is not displayed");
            Assert.NotNull(productTotal, "Product Total is not displayed");
            Assert.NotNull(ocShipping, "Shipping and Processing is not displayed");
            Assert.NotNull(ocTax, "Tax is not displayed");
            Assert.NotNull(orderTotal, "Order ID is not displayed");

            //Cartoverview and OC values confirmation
            Assert.Equals(TextActions.RemoveDollarSign(productTotal), cartProductTotal, "Product Total on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocPromotionDiscount), promotionsAndDiscounts, "Promotion and Discount on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocShipping), cartShippingAndProcessing, "Shipping & Processing on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocTax), cartTax, "Tax on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(orderTotal), cartOrderTotal, "Order Total on Cart and order Confirmation do not match");


            // Database verification
            var dbOrder = OrderActions.GetOrderIdRecordsInAssets(orderId);

            Assert.DatabaseObject(dbOrder, "OrderActions.GetOrderIdRecordsInAssets()");

            Assert.Equals(shortSku, dbOrder.ShortSku, "Short Sku entry in database doesn't match value on order confirmation page.");
            Assert.Equals(productName, WebUtility.HtmlDecode(dbOrder.ProductName), "Product name entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(productTotal), TextActions.FormatToTwoDecimals(dbOrder.ItemTotal), "Product total entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(ocShipping), TextActions.FormatToTwoDecimals(dbOrder.SAndP), "Shipping and processing entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(ocTax), TextActions.FormatToTwoDecimals(dbOrder.TaxTotal), "Tax Price entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(orderTotal), TextActions.FormatToTwoDecimals(dbOrder.OrderTotal), "Order total entry in database doesn't match value on order confirmation page.");
        }

        public override void VerifyOrderConfirmationExpiredPage()
        {
            Browser.MouseOverOnElement(HeaderFooter.UserNameLink);
            Browser.Wait.ForDomReady(5);

            Browser.Wait.IsVisibleElement(By.Id(HeaderFooter.HrdSignOutId));

            HeaderFooter.SignOutLink.Click();
            Browser.GoBack();

            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.SpecificErrorClass));

            Assert.StringContains(OrderConfirmation.SpecificErrorElement.Text, "your order confirmation page is no longer available", "Order Confirmation Not Available Message did not display");
        }
    }


    /// <summary>
    /// Verify the user can submit an order with a Credit Card and does not see CC info when navigating back to OC page after logout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5394
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7041
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5394"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7041")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T7041_MobileBase : T155_T7041_Base
    {
        protected T7041_MobileBase(ITestOutputHelper output) : base(output) { }

        public override void SaveDefaultAddressAndPayment()
        {
            //Adding default Address
            Browser.Navigate(Urls.ManageAccountPageUrl);
            Browser.Wait.ForDisplayedElement(ManageAccount.ManageShippingAddressesLinkForElement);
            ManageAccount.ManageShippingAddressesLinkForElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ManageAccount.BtnAddShippingAddressId.ToCssIdSelector()));
            ManageAccount.BtnAddShippingAddress.Click();
            ManageAccountWorkflow.AddNewShippingAddressToModal(new Address());
            ManageAccount.BtnSaveShippingAddress.Click();

            Browser.Wait.ForDomReady();

            Browser.Wait.ForClickableElement(ManageAccount.ManageAccountBackButton).Click();

            //Adding default Payment Details
            Browser.Wait.ForDisplayedElement(ManageAccount.ManagePaymentOptionsLinkForElement);
            ManageAccount.ManagePaymentOptionsLinkForElement.Click();
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();

            Browser.Wait.ForDomReady();
        }

        public override void VerifyOrderSummaryValues()

        {
            //Applying Promocode
            CartOverview.RemovePromoCode();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CartPromotionalCodeId.ToCssIdSelector()));
            CartOverview.CartPromotionalButton.Click();
            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            Browser.ScrollToTopOfWindow();

            //Applying Zip
            CartOverview.ChangeShippingOptionsLink.Click();
            Browser.Wait.ForElement(CartOverview.ShippingOptionModal);
            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys("91311");
            CartOverview.ClickShippingOptionShipTabSearchButton();
            Browser.Wait.ForDomReady();
            CartOverview.ClickShippingOptionShipTabUpdateButton();
            CartOverview.CloseShippingOptionsOverlay.Click();

            //Storing cart values
            var cartProductTotal = Convert.ToString(CartOverview.GeProductTotal(0));
            var promotionsAndDiscounts = Convert.ToString(CartOverview.GetActualPromoCodeDiscountPrice());
            var cartShippingAndProcessing = Convert.ToString(CartOverview.GetShippingTotals(2));
            var cartTax = Convert.ToString(CartOverview.GetTaxTotal(3));
            var cartOrderTotal = Convert.ToString(CartOverview.GetOrderTotalCost());

            Browser.Wait.ForDomReady();

            Browser.ScrollToTopOfWindow();

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.ForPage(Urls.ShippingPageUrl);

            WaitForGlobalSpinnerToClose();
            ShoppingCartWorkflow.ProceedToPayment();
            const string securityCode = "111";

            Browser.Wait.ForDisplayedElement(Payment.CardVerificationElement);

            Payment.CardVerificationElement.SendKeys(securityCode);

            Browser.Wait.ForDomReady();
            Browser.Wait.ForClickableElement(Payment.PlaceOrderButton).Click();
            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
            Assert.PageUrl(Urls.OrderConfirmationPageUrl, Browser.PageUrl, "User is not taken to the Order Confirmation page.");

            Thread.Sleep(15000);

            //Stroing Order Confirmation Values
            var orderId = Convert.ToString(OrderConfirmation.GetOrderId);
            var shortSku = Convert.ToString(OrderConfirmation.ProductSkuOrder);
            var productName = Convert.ToString(OrderConfirmation.ProductNameText);
            var productTotal = Convert.ToString(OrderConfirmation.ProductTotalValue.Text);
            var ocPromotionDiscount = Convert.ToString(OrderConfirmation.OCPromotionValue.Text);
            var ocShipping = Convert.ToString(OrderConfirmation.ShippingAndProcessingTotal.Text);
            var ocTax = Convert.ToString(OrderConfirmation.TaxTotal.Text);
            var orderTotal = Convert.ToString(OrderConfirmation.OrderTotalValue.Text);

            //Cartoverview and OC values confirmation
            Assert.Equals(TextActions.RemoveDollarSign(productTotal), cartProductTotal, "Product Total on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocPromotionDiscount), promotionsAndDiscounts, "Promotion and Discount on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocShipping), cartShippingAndProcessing, "Shipping & Processing on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(ocTax), cartTax, "Tax on Cart and order Confirmation do not match");
            Assert.Equals(TextActions.RemoveDollarSign(orderTotal), cartOrderTotal, "Order Total on Cart and order Confirmation do not match");

            //Database Verification
            var dbOrder = OrderActions.GetOrderIdRecordsInAssets(orderId);

            Assert.DatabaseObject(dbOrder, "OrderActions.GetOrderIdRecordsInAssets()");

            Assert.Equals(shortSku, dbOrder.ShortSku, "Short Sku entry in database doesn't match value on order confirmation page.");
            Assert.Equals(productName, WebUtility.HtmlDecode(dbOrder.ProductName), "Product name entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(productTotal), TextActions.FormatToTwoDecimals(dbOrder.ItemTotal), "Product total entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(ocShipping), TextActions.FormatToTwoDecimals(dbOrder.SAndP), "Shipping and processing entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(ocTax), TextActions.FormatToTwoDecimals(dbOrder.TaxTotal), "Tax Price entry in database doesn't match value on order confirmation page.");
            Assert.Equals(TextActions.RemoveDollarSign(orderTotal), TextActions.FormatToTwoDecimals(dbOrder.OrderTotal), "Order total entry in database doesn't match value on order confirmation page.");
        }

        public override void VerifyOrderConfirmationExpiredPage()
        {
            Browser.Wait.ForDomReady();
            Browser.Wait.ForClickableElement(OrderConfirmation.ContinueShoppingButton).Click();

            Browser.Wait.ForDomReady();

            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementByClassName(HeaderFooter.LpIconMenuClass)).Click();

            Browser.Wait.ForClickableElement(HeaderFooter.HeaderAccountButton).Click();
            Browser.Wait.ForClickableElement(HeaderFooter.SignOutLink).Click();

            Browser.GoBack();
            Browser.GoBack();

            Browser.Wait.ForDisplayedElement(OrderConfirmation.OcPageHeadingElement);

            Assert.StringContains(OrderConfirmation.OcPageHeadingElement.Text, "Order Confirmation Not Available", "Order Confirmation Not Available Message did not display");
        }
    }


    public abstract class T155_T7041_Base : OrderConfirmationTestsBase
    {
        protected T155_T7041_Base(ITestOutputHelper output) : base(output) { }

        public abstract void VerifyOrderSummaryValues();

        public abstract void VerifyOrderConfirmationExpiredPage();

        public abstract void SaveDefaultAddressAndPayment();

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");

            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();

            SaveDefaultAddressAndPayment();

            ShoppingCartWorkflow.EmptyCart();

            var productBetweenTenAndTwenty = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(productBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = productBetweenTenAndTwenty });

            VerifyOrderSummaryValues();

            VerifyOrderConfirmationExpiredPage();
        }
    }
}