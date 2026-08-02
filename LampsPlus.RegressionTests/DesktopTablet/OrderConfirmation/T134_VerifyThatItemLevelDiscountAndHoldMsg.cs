using System;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using ProductModel = LampsPlus.AutomationFramework.Utilities.ProductModel;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T134_Windows_VerifyItemLevelDiscountAndHoldMsg : T134_DesktopBase
    {
        public T134_Windows_VerifyItemLevelDiscountAndHoldMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void ItemLevelDiscountAndHoldMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    public class T134_Mac_VerifyItemLevelDiscountAndHoldMsg : T134_DesktopBase
    {
        public T134_Mac_VerifyItemLevelDiscountAndHoldMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void ItemLevelDiscountAndHoldMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T134_iPad_VerifyItemLevelDiscountAndHoldMsg : T134_DesktopBase
    {
        public T134_iPad_VerifyItemLevelDiscountAndHoldMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void ItemLevelDiscountAndHoldMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T134_TabletEmulator_VerifyItemLevelDiscountAndHoldMsg : T134_DesktopBase
    {
        public T134_TabletEmulator_VerifyItemLevelDiscountAndHoldMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void ItemLevelDiscountAndHoldMsg(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an Item Level Discount is applied, Secondary Employee Number is utilized correctly and hold messages are displayed for an employee on the OC page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6570
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T134
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6570"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T134")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public abstract class T134_DesktopBase : OrderConfirmationTestsBase
    {
        protected T134_DesktopBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify that an Item Level Discount is applied, Secondary Employee Number is utilized correctly and hold messages are displayed for an employee on the OC page.
        /// </summary>
        /// <param name="config"></param>
        protected void Validate(string config)
        {
            var setup = new TestSetup(config, useEmployeeManagerAccount:true) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFramework(config, setup: setup);

            const string qaOrderHold = "QA Order Hold";
            const string stateZipMismatch = "Ship state entered does not match ship zip code";

            const int secondaryEmployee = 9981;
            var cashierEmployee = OrderActions.GetEmployeeNumberByUserName(TestSetup.AccountConfig.AccountUnderTest.UserName);//TODO get User Info object to get Employee number

            ShoppingCartWorkflow.EmptyCart();

            var productBetweenTenAndTwenty = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            var discountPct = "10";
           
            Assert.DatabaseObject(productBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");

            //adding item to cart
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = productBetweenTenAndTwenty });

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
          
            //selecting shipping option
            CartOverview.ChangeShippingOptionsLink.Click();
            Browser.Wait.ForElement(CartOverview.ShippingOptionModal);
            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys("91311");
            CartOverview.ClickShippingOptionShipTabSearchButton();
            CartOverview.ClickShippingOptionShipTabUpdateButton();

            Browser.Wait.IsInvisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));

            var itemPrice = decimal.Parse(TextActions.RemoveDollarSign(CartOverview.ProductTotalPrice(0)));  //verifies Product Total before discount

            //applying item level discount
            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDomReady();
            
            CartOverview.TextPercentDiscountField.SendKeys(discountPct);
            CartOverview.SelDiscountReasonField.Click();
            new SelectElement(CartOverview.SelDiscountReasonField.InternalElement).SelectByIndex(1);
            CartOverview.ApplyDiscountButton.Click();

            Browser.Wait.IsVisibleElement(By.XPath(CartOverview.AdditionalDiscountsXpath));

            var subTotalCart = decimal.Parse(CartOverview.SubTotalOnCart.Text.Replace("$", ""));

            //verifying discounted subtotal
            var subTotalPriceTotalBlock = decimal.Parse(TextActions.RemoveDollarSign(CartOverview.ProductTotalPrice(0)));  
            var itemDiscount = Math.Truncate(itemPrice * decimal.Parse(discountPct)) / 100;  //Manually verifies applied 10% discount returns correct value
            var subTotalPriceCalculation = itemPrice - itemDiscount; //manually verifies product total - discount = correct subTotal value
            var discountedPriceOnItemLevel = decimal.Parse(TextActions.RemoveDollarSign(CartOverview.ProductTotalCostLabel(0).Text.Trim()));  // gets discounted item price on Item Level

            Assert.Equals(subTotalPriceCalculation, discountedPriceOnItemLevel, "Discounted price on item level and manual calculation of subtotal are the same");
            Assert.Equals(subTotalPriceTotalBlock, discountedPriceOnItemLevel, "Discounted price on item level and subtotal are the sames");

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            //selecting sale source and entering secondary employee id
            CsrBlock.SelectSaleSource(Sources.CartSources.Phone);
            CsrBlock.SecondaryEmployeeField.Clear();
            Browser.Wait.ForDomReady();
            CsrBlock.SecondaryEmployeeField.SendKeys(secondaryEmployee.ToString());

            //navigating back to PDP and back to Cart Overview page to avoid stale element and to give time for the Secondary Employee number populates in the database.
            CartOverview.ProductImageAnchorWebElement(0).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdAddToPortfolioNormalId.ToCssIdSelector()));

            Browser.Navigate(Urls.CartOverviewPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            Browser.ScrollToBottomOfPageJs();
            Browser.MouseOverOnElement(CartOverview.CheckOutNowButton);
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            //entering Shipping info with Chatsworth ZipCode and New York State
            var shippingAddress = new Address // TODO: Objects should be built with helpers in page objects.
            {
                ZipCode = ZipCodeList.Chatsworth,
                State = StateCodeListUnitedStates.NY,
                Email = "testautomation" + DateTime.Now.Ticks + "@mailinator.com"
            };

            CustomerAddressInformation.EnterShippingAddress(shippingAddress);

            //proceed to payment
            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.XPath(Payment.PurchaseOrderRadioButtonXpath));
            // User selects the P.O. option on the Payment page and enters the Purchase Order Number - FOR KIOSK MODE and places order.
            ShoppingCartWorkflow.EmployeePlaceOrderViaPo();

            // The order confirmation page is displayed with the confirmation ID.
            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.OrderConfirmationOrderIdClass));

            //Capture OrderId and Email Address used to place the order
            var emailAddress = OrderConfirmation.EmailUTagElement.Text;
            var orderId = OrderConfirmation.GetOrderIdNumber;

            //verify hold messages and discounted subtotal amount
            Assert.Displayed(OrderConfirmation.HoldReasonsElement, "Hold Reasons section not displayed on the Order Confirmation page.");
            Assert.StringContains(OrderConfirmation.HoldReasonsMessages, qaOrderHold, "QA Order Hold message is not displayed on the Order Confirmation page.");
            Assert.StringContains(OrderConfirmation.HoldReasonsMessages, stateZipMismatch, "Ship state entered does not match ship zip code message is not displayed on the Order Confirmation page.");
            Browser.Wait.ForDomReady();
            Assert.Equals(subTotalPriceCalculation, subTotalCart, "The Subtotal value on the Order Confirmation Page is different from Subtotal value on the Shopping Cart page");
            Browser.Wait.ForDomReady();

            //verify CashierEmployee and CommissionEmployee in TblGlobalOrderHeader
            var orderIdModel = (OrderIdModel)Browser.Wait.ForOrder(orderId, OrderActions.CheckOrderIdExists);
            Assert.Equals(secondaryEmployee, orderIdModel.CommissionEmployee, "Incorrect Commission Employee Number in TblGlobalOrderHeader.");
            Assert.Equals(cashierEmployee.EmployeeNumber, orderIdModel.CashierEmployee, "Incorrect Cashier Employee Number in TblGlobalOrderHeader.");

			//verify CommissionEmployee in TblUserProfile 
	        var userProfileModel = OrderActions.GetCommissionEmployeeWithCsrOrder(emailAddress);
            Assert.Equals(secondaryEmployee, userProfileModel.CommissionEmployee, "Incorrect Commission Employee Number in TblUserProfile.");

			//verify hold reason
	        var holdReasons = OrderActions.GetHoldReasonsByOrderId(orderId);
            Assert.True(holdReasons.Contains(qaOrderHold), "Order does not have 'QA Order Hold' hold reason.");
            Assert.True(holdReasons.Contains(stateZipMismatch), "Order does not have 'Ship state entered does not match ship zip code' hold reason.");
        }
	}
}