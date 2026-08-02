using System.Collections.Generic;
using System.Linq;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderSummaryBlock
{

    /// <summary>
    /// Base class for Order Summary Block specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.OrderSummaryBlock)]
    public class OrderSummaryBlockTestsBase : TestsBase
	{
        public OrderSummaryBlockTestsBase(ITestOutputHelper output) : base(output) { }

		/// <summary>
		/// Used to verify Order Summary Block details on the Cart page, Shipping page, Payment page, and Order Confirmation page.
		/// </summary>
		/// <param name="initialItemPrices"></param>
		/// <param name="itemPricesWithPromoCode"></param>
		protected void VerifyOrderSummaryBlockAfterPromoCodeApplied(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemPricesWithPromoCode)
		{
			var productTotal = initialItemPrices.First() + initialItemPrices.Last();
			Assert.Equals(CartOverview.GetProductTotal(), productTotal, "Product Total does not display correct sum.");

			var couponMemberSpecialPriceSavingsValue = CartOverview.GetActualPromoCodeDiscountPrice();
			Assert.Equals(CartOverview.CouponAndMemberSpecialPriceSavingsLabel, CartOverview.GetPromoCodeLabel(), "Coupon & Member Special Price Savings label not displayed");
			Assert.True(0 > couponMemberSpecialPriceSavingsValue, "Coupon & Member Special Savings amount is not negative.");

            var professionalSavingsValue = CartOverview.GetProfessionalSavingsPrice();
            Assert.Equals(CartOverview.ProfessionalSavingsLabel, CartOverview.GetProfessionalSavingsLabel(), "Professional Savings label not displayed");
			Assert.True(0 >= professionalSavingsValue, "Professional Savings amount is not negative.");

            var additionalDiscountsValue = CartOverview.GetAdditionalDiscountsPrice();
			Assert.Equals(CartOverview.AdditionalDiscountsLabel, CartOverview.GetAdditionalDiscountsLabel().Trim(), "Additional Discounts label not displayed");
			Assert.True(0 > additionalDiscountsValue, "Additional Discounts amount is not negative.");

			// verify coupon and member special savings and professional savings only applied to none manual line item discount
			var correctSecondItemPrice = initialItemPrices.Last() + couponMemberSpecialPriceSavingsValue + professionalSavingsValue;
			Assert.Equals(itemPricesWithPromoCode.Last(), correctSecondItemPrice, "Coupon and Pro savings discount not applied correctly to second item.");

			// verify manual discount is applied correctly to first item
			var correctFirstItemPrice = initialItemPrices.First() + additionalDiscountsValue;
			Assert.Equals(itemPricesWithPromoCode.First(), correctFirstItemPrice, "Manual discount not applied correctly to first item");

			Assert.Displayed(CartOverview.SubTotalLabel, "Subtotal line not displayed");
			var correctSubTotalAmount = productTotal + couponMemberSpecialPriceSavingsValue + professionalSavingsValue + additionalDiscountsValue;
			var subTotalAmount = CartOverview.GetSubTotal();
			Assert.Equals(subTotalAmount, correctSubTotalAmount, "Wrong amount for subtotal.");

			Assert.Displayed(CartOverview.ShippingAndProcessingLabel, "Shipping & Processing line is not displayed.");
			Assert.Displayed(CartOverview.TaxLabel, "Tax line is not displayed.");

			var shippingProcessingValue = CartOverview.GetShippingCostValue();
			var shippingProcessingUpdatedValue = (shippingProcessingValue == "FREE*" || shippingProcessingValue == "FREE") ? 0 : TextActions.FormatPrice(shippingProcessingValue);

			var taxValue = CartOverview.GetSaleTax();

			var correctOrderTotal = subTotalAmount + shippingProcessingUpdatedValue + taxValue;
			var updatedOrderTotal = TextActions.FormatPrice(OrderSummaryBlock.OrderTotalValue.Text);
			Assert.Equals(correctOrderTotal, updatedOrderTotal, "Order total amount is incorrect.");
		}

		protected void VerifyOrderSummaryBlockAfterPromoCodeAppliedOrderConfirmation(IReadOnlyCollection<decimal> initialItemPrices, IReadOnlyCollection<decimal> itemPricesWithPromoCode)
		{
			var productTotal = initialItemPrices.First() + initialItemPrices.Last();
			Assert.Equals(CartOverview.GetProductTotal(), productTotal, "Product Total does not display correct sum.");

			var couponMemberSpecialPriceSavingsValue = CartOverview.GetActualPromoCodeDiscount();
			Assert.Equals(CartOverview.CouponAndMemberSpecialPriceSavingsLabel, CartOverview.GetPromoCodeLabel().Trim(), "Coupon & Member Special Price Savings label not displayed");
			Assert.True(0 > couponMemberSpecialPriceSavingsValue, "Coupon & Member Special Savings amount is not negative.");

            var professionalSavingsValue = CartOverview.GetProfessionalSavingsPrice();
			Assert.Equals(CartOverview.ProfessionalSavingsLabel, CartOverview.GetProfessionalSavingsLabel().Trim(), "Professional Savings label not displayed");
			Assert.True(0 >= professionalSavingsValue, "Professional Savings amount is not negative.");

			var additionalDiscountsValue = CartOverview.GetAdditionalDiscountsWithPrefix();
			Assert.Equals(CartOverview.AdditionalDiscountsLabel, CartOverview.GetAdditionalDiscountsLabel().Trim(), "Additional Discounts label not displayed");
			Assert.True(0 > additionalDiscountsValue, "Additional Discounts amount is not negative.");

			// verify coupon and member special savings and professional savings only applied to none manual line item discount
			var correctSecondItemPrice = initialItemPrices.Last() + couponMemberSpecialPriceSavingsValue + professionalSavingsValue;
			Assert.Equals(itemPricesWithPromoCode.Last(), correctSecondItemPrice, "Coupon and Pro savings discount not applied correctly to second item.");

			// verify manual discount is applied correctly to first item
			var correctFirstItemPrice = initialItemPrices.First() + additionalDiscountsValue;
			Assert.Equals(itemPricesWithPromoCode.First(), correctFirstItemPrice, "Manual discount not applied correctly to first item");

			Assert.Displayed(CartOverview.SubTotalLabel, "Subtotal line not displayed");
			var correctSubTotalAmount = productTotal + couponMemberSpecialPriceSavingsValue + professionalSavingsValue + additionalDiscountsValue;
			var subTotalAmount = CartOverview.GetSubTotal();
			Assert.Equals(subTotalAmount, correctSubTotalAmount, "Wrong amount for subtotal.");

			Assert.Displayed(CartOverview.ShippingAndProcessingLabel, "Shipping & Processing line is not displayed.");
			Assert.Displayed(CartOverview.TaxLabelOnOrderConfirmationPage, "Tax line is not displayed.");

			var shippingProcessingValue = CartOverview.GetShippingCostValue();
			var shippingProcessingUpdatedValue = (shippingProcessingValue == "FREE*" || shippingProcessingValue == "FREE") ? 0 : TextActions.FormatPrice(shippingProcessingValue);

			var taxValue = CartOverview.GetSaleTax();

			var correctOrderTotal = subTotalAmount + shippingProcessingUpdatedValue + taxValue;
			var updatedOrderTotal = TextActions.FormatPrice(OrderSummaryBlock.OrderTotalValue.Text);
			Assert.Equals(correctOrderTotal, updatedOrderTotal, "Order total amount is incorrect.");
		}
	}
}
