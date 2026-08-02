using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderSummaryBlock   
{
	//[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderSummary)]
    public class T160_Windows_VerifyOrderSummaryBlockDiscounts : T160_DesktopBase
	{
        public T160_Windows_VerifyOrderSummaryBlockDiscounts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_CIC)]
        public void OrderSummaryBlockDiscounts(string config) => Validate(config);
	}


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderSummary)]
    public class T160_Mac_VerifyOrderSummaryBlockDiscounts : T160_DesktopBase
    {
        public T160_Mac_VerifyOrderSummaryBlockDiscounts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_CIC)]
        public void OrderSummaryBlockDiscounts(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderSummary)]
    public class T160_iPad_VerifyOrderSummaryBlockDiscounts : T160_DesktopBase
    {
        public T160_iPad_VerifyOrderSummaryBlockDiscounts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
		[InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_CIC)]
        public void OrderSummaryBlockDiscounts(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderSummary)]
    public class T160_TabletEmulator_VerifyOrderSummaryBlockDiscounts : T160_DesktopBase
    {
        public T160_TabletEmulator_VerifyOrderSummaryBlockDiscounts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_CIC)]
        public void OrderSummaryBlockDiscounts(string config) => Validate(config);
    }


	/// <summary>
	/// Verify Order Summary Block when multiple discounts and promo codes are applied.
	/// 
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10913
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T160
	///		
	///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5413
	///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T161
	///
	///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5238
	///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T162
	///
	///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5041
	///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T163
	/// 
	/// </summary>
	//[Collection(LpTraits.UserRole.Employee)]
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10913"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T160"), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T160_DesktopBase : OrderSummaryBlockTestsBase
    {
        protected T160_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
		{
			var setup = new TestSetup(config) { AccountConfig = { ClearStoreInSessionOnSetup = false } };
            InitializeFramework(config, setup: setup);

            AddItemsToCart();

			CartOverview.RemovePromoCode();
			CartOverview.RemoveProfessionalAccount();

			var initialItemPrices = GetInitialProductPrices();

			CartOverview.AddProfessionalAccount(ShoppingCartTypes.CompanyName);

			// get updated prices with discount from UI
			var itemsWithProDiscount = GetProductPricesWithProDiscount();

			VerifyPricesWithProfessionalAccountApplied(initialItemPrices, itemsWithProDiscount);
			VerifyManualDiscount(initialItemPrices.First());

			ApplyPromoCodeAndVerify();

			// get updated prices after promo code applied
			var itemsWithPromoCode = GetProductPricesWithProDiscount();
			CartOverview.ChangeShippingZipCode();

			// Verify Order Summary Block on the Cart page
			VerifyOrderSummaryBlockAfterPromoCodeApplied(initialItemPrices, itemsWithPromoCode);

			VerifyOrderSummaryBlockOnShippingPage(initialItemPrices, itemsWithPromoCode);
			VerifyOrderSummaryBlockOnPaymentPage(initialItemPrices, itemsWithPromoCode);
			VerifyOrderSummaryBlockOnOrderConfirmation(initialItemPrices, itemsWithPromoCode);
		}

		private void AddItemsToCart()
		{
            var anyTwoSkus = ProductActions.GetTwoSkusWithNullUmrp();
			Assert.DatabaseObject(anyTwoSkus, "ProductActions.GetTwoSkusWithNullUmrp()");
			Assert.Equals(anyTwoSkus.Count, 2, "Two products were not returned from ProductActions.GetTwoSkusWithNullUmrp()");

			ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(anyTwoSkus.First()));
			ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(anyTwoSkus.Last()));
			Browser.Wait.ForDomReady();
		}

		private IReadOnlyCollection<decimal> GetInitialProductPrices()
		{
			// get initial prices for both items before discounts applied
			// CartOverview.GetListOfAllProductsOnPage() does not return a new list when called, but
			// passes the list by reference.

			var productPrices = CartOverview.GetListOfAllProductsOnPage();
			var initialItemPrices = new List<decimal>
			{
				TextActions.FormatPrice(productPrices.First().Price),
				TextActions.FormatPrice(productPrices.Last().Price)
			};

			return initialItemPrices;
		}

		private IReadOnlyCollection<decimal> GetProductPricesWithProDiscount()
		{
			var prices = CartOverview.GetListOfAllProductsOnPage();
			var itemsWithProDiscount = new List<decimal>
			{
			    TextActions.FormatPrice(prices.First().Price),
			    TextActions.FormatPrice(prices.Last().Price)
			};

			return itemsWithProDiscount;
		}

		private void VerifyPricesWithProfessionalAccountApplied(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemsWithProDiscount)
		{
            var discountRate = ProductActions.GetCurrentDiscountRateSelectedCompany(ShoppingCartTypes.CompanyName);
            Assert.DatabaseObject(discountRate, "ProductActions.GetCurrentDiscountRateSelectedCompany()");

			// calculate discount for each item
			var firstItemComputedDiscount = CartOverview.GetDiscountedPrice(initialItemPrices.First(), discountRate);
			var secondItemComputedDiscount = CartOverview.GetDiscountedPrice(initialItemPrices.Last(), discountRate);

			// get computed order total and order total from UI
			var shippingAndProcessingValue = CartOverview.GetShippingCostValue();
			var shippingAndProcessingUpdatedValue = (shippingAndProcessingValue == "- -" || shippingAndProcessingValue == "FREE*" || shippingAndProcessingValue == "FREE") ? 0 : TextActions.FormatPrice(shippingAndProcessingValue);

			var orderTotalComputedDiscount = firstItemComputedDiscount + secondItemComputedDiscount + shippingAndProcessingUpdatedValue;
			var orderTotalWithProDiscount = TextActions.FormatPrice(OrderSummaryBlock.OrderTotalValue.Text);

			Assert.Equals(firstItemComputedDiscount, itemsWithProDiscount.First(), "Discount not applied correctly for first item.");
			Assert.Equals(secondItemComputedDiscount, itemsWithProDiscount.Last(), "Discount not applied correctly for second item.");

			Assert.Displayed(CartOverview.ProfessionalAccountLabel, "Company is not added");
			Assert.Equals(ShoppingCartTypes.CompanyName, CartOverview.ProfessionalAccountLabel.Text, "Company name is not displayed.");

			Assert.Equals(orderTotalComputedDiscount, orderTotalWithProDiscount, "Order total price not adjusted from pro discount.");
		}

		private void VerifyManualDiscount(decimal firstItemInitialPrice)
		{
			// apply 1% discount on the first cart item
			ShoppingCartWorkflow.ApplyCartItemDiscount(0, 1);
		    Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.AdditionalDiscountsClass));

            var firstItemManualDiscount = CartOverview.GetDiscountedPrice(firstItemInitialPrice, 1);

			//verify manual discount applied line level
			var cartProductsManualDiscount = CartOverview.GetListOfAllProductsOnPage();
			var updatedFirstPriceWithManual = TextActions.FormatPrice(cartProductsManualDiscount.First().Price);

			Assert.Equals(firstItemManualDiscount, updatedFirstPriceWithManual, "Manual discount not applied.");
		}

		private void ApplyPromoCodeAndVerify()
		{
			CartOverview.CartPromotionalButton.Click();
			
			Browser.Wait.ForDisplayedElement(CartOverview.PromoInputField);
			CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
			CartOverview.ApplyPromoCode();

            var expectedAppliedCode = $"{CartOverview.AppliedCodeLabel} {PromoCodeList.AutoPromoCodeTest.Name}";

			Assert.True(expectedAppliedCode.CaseInsensitiveContains(CartOverview.PromoCodeLabel.Text), "Applied Promo code label not displayed");
        }

        ///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5413
        ///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T161
        private void VerifyOrderSummaryBlockOnShippingPage(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemsWithProDiscount)
		{
			Browser.ScrollToTopOfWindow();
			CsrBlock.SelectSaleSource(Sources.CartSources.SalesPhone);
			Browser.Wait.ForDomReady();
			CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

			Assert.Equals(Browser.PageUrl, Urls.ShippingPageUrl, "Not on the shipping page");
			VerifyOrderSummaryBlockAfterPromoCodeApplied(initialItemPrices, itemsWithProDiscount);
		}

        ///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5238
        ///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T162
        private void VerifyOrderSummaryBlockOnPaymentPage(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemsWithProDiscount)
		{
            CustomerAddressInformation.EnterShippingAddress(new Address("_LP-T160") { State = "CA" });
			ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

			Assert.Equals(Browser.PageUrl, Urls.PaymentPageUrl, "Not on the billing page");
			VerifyOrderSummaryBlockAfterPromoCodeApplied(initialItemPrices, itemsWithProDiscount);
		}

        ///	JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5041
        ///	Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T163
        private void VerifyOrderSummaryBlockOnOrderConfirmation(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemsWithProDiscount)
		{
			ShoppingCartWorkflow.EmployeePlaceOrderViaCheck();
			Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
			Assert.Equals(Browser.PageUrl, Urls.OrderConfirmationPageUrl, "Not on the order confirmation page");
			VerifyOrderSummaryBlockAfterPromoCodeAppliedOrderConfirmation(initialItemPrices, itemsWithProDiscount);
		}
	}
}