using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public interface IOrderSummaryBlock
    {
        #region Class Setup
        string CloseButtonXpath { get; }
        string OrderSummaryContainer { get; }
        string OrderSummaryId { get; }
        string ProductNameClass { get; }
        string ProductPriceClass { get; }
        string ProductQtyClass { get; }
        string ProceedPaymentId { get; }
        #endregion

        #region Page Elements
        IElement CloseButton { get; }
        IElement EditOrderButton { get; }
        IElement OrderSummaryBlockContainer { get; }
        IElement OrderSummaryElement { get; }
        IElement OrderTotal { get; }
        IElement OrderTotalValue { get; }
        IElement PosProductTotal { get; }
        IElement ProductName { get; }
        IElement ProductPrice { get; }
        IElement ProductQty { get; }
        IElement ProceedToPaymentButton { get; }
        IElement ProductTotalLabel { get; }
        IElement ProductTotalValue { get; }

        IElement ShippingAndProcessingLabel { get; }
        IElement ShippingAndProcessingValue { get; }
        IElement TaxLabel { get; }
        IElement TaxValue { get; }
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Gets Order Summary row element based on passed index.
        /// </summary>
        /// <param name="index">Index of element to return</param>
        /// <returns>Group Element</returns>
        IElement OrderSummaryRow(int index);

        /// <summary>
        /// Promo Code line label
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IElement PromoCodeLineLabel(int index);

		IElement PromoCodeLineValue(int index);

		/// <summary>
		/// Order Summary Block line values based on index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		IElement OrderSummaryBlockElement(int index);

		/// <summary>
		/// Get the Promo Code line (like Coupon and Member Special Price Savings)
		/// </summary>
		/// <returns></returns>
		string GetPromoCodeLabel();

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);

        /// <summary>
        /// Wait for the kiosk price to update. When the page loads, the initial price is $0.00.
        /// </summary>
        void WaitForKioskPriceToUpdate();
        
        /// <summary>
        /// Get the AdditionalDiscounts line value
        /// </summary>
        /// <returns></returns>
        string GetAdditionalDiscounts();

        /// <summary>
        /// Get the Shipping & Processing line value
        /// </summary>
        /// <returns></returns>
        string GetSandP();
    }
}
