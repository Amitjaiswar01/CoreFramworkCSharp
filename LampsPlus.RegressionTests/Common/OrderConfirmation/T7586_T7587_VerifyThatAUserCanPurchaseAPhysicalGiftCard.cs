using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using xRetry;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7586_Windows_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7586_DesktopBase
    {
        public T7586_Windows_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7586_Mac_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7586_DesktopBase
    {
        public T7586_Mac_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7586_iPad_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7586_DesktopBase
    {
        public T7586_iPad_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7586_TabletEmulator_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7586_DesktopBase
    {
        public T7586_TabletEmulator_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }

    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7587_iPhone_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7587_MobileBase
    {
        public T7587_iPhone_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7587_Emulator_VerifyThatAUserCanPurchaseAPhysicalGiftCard : T7587_MobileBase
    {
        public T7587_Emulator_VerifyThatAUserCanPurchaseAPhysicalGiftCard(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanPurchasePhysicalGiftCard(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can purchase a physical Gift Card.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8796
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7586
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8796"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7586")]
    public abstract class T7586_DesktopBase : T7586_T7586_Base
    {
        protected T7586_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify that a user can purchase a physical Gift Card.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8796
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7587
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8796"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7587")]
    public abstract class T7587_MobileBase : T7586_T7586_Base
    {
        protected T7587_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config, Urls.ManageAccountPageUrl);

            //Precondition: Set up consumer information.
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ShippingAddressesLinkXpath));
            ManageAccount.ManageShippingAddressesLinkForElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ManageAccount.BtnAddShippingAddressId.ToCssIdSelector()));
            Browser.ClickOnButtonMultipleTimes(ManageAccount.BtnAddShippingAddress, 5, ManageAccount.IsManageAccountShippingFormVisible);

            ManageAccountWorkflow.AddNewShippingAddressToModal(new Address());
            ManageAccount.BtnSaveShippingAddress.Click();

            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.DefaultAddressXpath));

            //Step 1: Navigate to the Gift Card page.
            Browser.Navigate(Urls.GiftCardLandingPageUrl);

            Browser.Wait.IsVisibleElement(By.XPath(Sort.GiftCardShopNowBtnXpath));

            Assert.Displayed(Sort.GiftCardShopNowBtn, "A purchase option for a Gift Card not displayed.");
            Assert.Displayed(Sort.GiftCardBalanceSection, "A section to check the balance of a Gift Card not displayed.");

            //Step 2: Tap on the 'Shop Now' button.
            Sort.GiftCardShopNowBtn.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Displayed(GlobalLocators.AddToCartButton, "The user is not brought to a PDP page for the purchase of a physical Gift Card.");

            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.GiftCardDenominationXpath));

            //Step 3: Select a Gift Card denomination and add to the cart.
            Browser.ClickOnButtonMultipleTimes(ProductDetail.MobileGiftCardDenomination, 5, ProductDetail.IsCorrectGiftCardAmountSelected);
            
            var prodSku = ProductDetail.SkuOnPdp;
            var prodName = ProductDetail.ProductName;
            var giftCardPrice = ProductDetail.GiftCardDenomination(0).Text;
            var firstName = "LPQA TEST";
            var lastName = "LPQA TEST";
            var giftCardMessage = "LPQA TEST";

            ProductDetail.GiftCardFirstName.SendKeys(firstName);
            ProductDetail.GiftCardLastName.SendKeys(lastName);
            ProductDetail.GiftCardMessage.SendKeys(giftCardMessage);

            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            CartOverview.RemovePromoCode();

            var toNameOfGc = $"{firstName} {lastName}";
            var gcPrice = CartOverview.ProductTotalCostLabel(0).Text.Replace(".00", string.Empty);
            var productNameOnCart = CartOverview.ProductName(0);
            var productSkuOnCart = CartOverview.ProductSku(0);
            
            GiftCardName = CartOverview.GiftCardDetails(0).Text.Split(':')[1].Replace("\r\n", "").TrimStart(' ');
            GiftCardMessageOnCart = CartOverview.GiftCardDetails(1).Text.Split(':')[1].Replace("\r\n", "");

            Assert.Equals(giftCardPrice, gcPrice, "Gift Card price not same on the Gift Card PDP and Cart Page.");
            Assert.Equals(toNameOfGc, GiftCardName, "Name on Gift Card PDP and Cart Page not same.");
            Assert.Equals(giftCardMessage, GiftCardMessageOnCart, "Gift Card message not same on Gift Card PDP and Cart Page.");
            Assert.Equals(prodSku, productSkuOnCart, "Product Sku not same on PDP and Cart Page.");
            Assert.Equals(prodName, productNameOnCart, "Product Name not same on PDP and Cart Page.");

            //Step 4: Proceed through the order flow until reaching the Payment page.
            Browser.ClickOnButtonMultipleTimes(CartOverview.CheckOutNowButton, 5, Shipping.IsShippingPageVisible);

            OrderSummaryBlock.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            Assert.False(Browser.Locate.DoesElementExistImmediately(Payment.PayPalPaymentRadioId.ToCssIdSelector()), "PayPal button displayed on Payment Page.");
        }
    }

    
    public abstract class T7586_T7586_Base : OrderConfirmationTestsBase
    {
        protected T7586_T7586_Base(ITestOutputHelper output) : base(output) { }

        protected string GiftCardName, GiftCardMessageOnCart;

        protected virtual void Validate(string config)
        {
            InitializeFramework(config, Urls.ManageAccountPageUrl);

            //Precondition: Set up consumer information.
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ShippingAddressesLinkXpath));
            ManageAccount.ManageShippingAddressesLinkForElement.Click();
            Browser.Wait.ForDomReady();
            ManageAccount.BtnAddShippingAddress.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));
            ManageAccountWorkflow.AddNewShippingAddressToModal(new Address());
            ManageAccount.BtnSaveShippingAddress.Click();

            Browser.Wait.ForDomReady();
            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId);

            //Step 1: Navigate to the Gift Card page.
            Browser.Navigate(Urls.GiftCardLandingPageUrl);

            Browser.Wait.IsVisibleElement(By.XPath(Sort.GiftCardShopNowBtnXpath));

            Assert.Displayed(Sort.GiftCardShopNowBtn, "A purchase option for a Gift Card not displayed.");
            Assert.Displayed(Sort.GiftCardBalanceSection, "A section to check the balance of a Gift Card not displayed.");

            //Step 2: Tap on the 'Shop Now' button.
            Sort.GiftCardShopNowBtn.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Displayed(GlobalLocators.AddToCartButton, "The user is not brought to a PDP page for the purchase of a physical Gift Card.");

            //Step 3: Select a Gift Card denomination and add to the cart.
            ProductDetail.GiftCardDenomination(0).Click();
            
            var prodSku = ProductDetail.SkuOnPdp;
            var prodName = ProductDetail.ProductName;
            var giftCardPrice = ProductDetail.GiftCardDenomination(0).Text.Trim();
            var firstName = "LPQA TEST";
            var lastName = "LPQA TEST";
            var giftCardMessage = "LPQA TEST";

            ProductDetail.GiftCardFirstName.SendKeys(firstName);
            ProductDetail.GiftCardLastName.SendKeys(lastName);
            ProductDetail.GiftCardMessage.SendKeys(giftCardMessage);

            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            CartOverview.RemovePromoCode();

            var productQuantityOnCart = CartOverview.GiftCardQtyField.GetAttribute("value");
            var toNameOfGc = $"{firstName} {lastName}";
            var gcPrice = CartOverview.ProductTotalCostLabel(0).Text.Replace(".00", string.Empty);
            var productNameOnCart = CartOverview.ProductName(0);
            var productSkuOnCart = CartOverview.ProductSku(0);

            GiftCardName = CartOverview.GiftCardDetails(0).Text.Split(':')[1].Replace("\r\n", "").Trim();
            GiftCardMessageOnCart = CartOverview.GiftCardDetails(1).Text.Split(':')[1].Replace("\r\n", "").Trim();

            Assert.Equals(giftCardPrice, gcPrice, "Gift Card price not same on the Gift Card PDP and Cart Page.");            
            Assert.Equals(toNameOfGc, GiftCardName, "Name on Gift Card PDP and Cart Page not same.");
            Assert.Equals(giftCardMessage, GiftCardMessageOnCart, "Gift Card message not same on Gift Card PDP and Cart Page.");
            Assert.Equals(prodSku, productSkuOnCart, "Product Sku not same on PDP and Cart Page.");
            Assert.Equals(prodName, productNameOnCart, "Product Name not same on PDP and Cart Page.");

            //Step 4: Proceed through the order flow until reaching the Payment page.
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            OrderSummaryBlock.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));
          
            Assert.False(Browser.Locate.DoesElementExistImmediately(Payment.PayPalPaymentRadioId.ToCssIdSelector()), "PayPal button displayed on Payment Page.");
            Assert.Equals(prodName, Payment.OrderSummarySku.Text, "Product Name does not match between PDP and Payment page.");
            Assert.Equals(productQuantityOnCart, Payment.OrderSummaryQuantity.Text.Split(' ')[1], "Quantity does not match between Cart Overview and Payment page.");
        }
    }
}
