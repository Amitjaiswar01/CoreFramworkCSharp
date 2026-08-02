using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface IOrderDetails
	{
		#region Class Setup
		string BreadCrumbClass { get; }
        string PriceString { get; }
        string ProductColClass { get; }
        string QuantityString { get; }
        string OpenCartResultsClass { get; }
        string ContentWrapClass { get; }
        string H4Class { get; }
        string ItemClass { get; }
        string ItemsClass { get; }
        string LincCareWidgetAnchor2014Class { get; }
        string OrderSummaryClass { get; }
		string OrderDetailPayPal { get; }
		string DetailsClass { get; }
        string ProductOverlayClass { get; }
        string OrderIdRmaHeadingXPath { get; }
		#endregion

		#region Page Elements
        IElement OrderIdRmaHeading { get; }
		IElement BreadCrumbElement { get; }
        IElement Details { get; }
        IElement OrderIdHeader { get; }
		IElement OrderDateHeader { get; }
		IElement RewardNumber { get; }
	    IElement OrderDetailsContainer { get; }
        IElement OrderSummaryContainer { get; }
        IElement OrderIdContainer { get; }
        IElement OrderItemTotal { get; }
		IElement OrderTax { get; }
		IElement OrderTotal { get; }
		IElement OrderSummarySection { get; }
        IElement ShippingUpdatesModalDoneButton { get; }
        IElement ShippingUpdatesModalOptInCheckbox { get; }
        IElement ShippingProcessing { get; }
        IElement TableContainer { get; }
        IElement TrackItemLink { get; }
		

		ReadOnlyCollection<IElement> ListOfOrderDetailBreadCrumbLinks { get; }
        ReadOnlyCollection<IElement> OrderDetailsSections { get; }
		ReadOnlyCollection<IElement> OrderDetailTableFirstRow { get; }
		ReadOnlyCollection<IElement> OrderDetailTableRows { get; }
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
		/// Gets the IElement referencing the SPAN tag that contains the value of a given Order Summary rowLabel text.
		/// </summary>
		/// <param name="rowLabel">The Order Summary rowLabel text to locate. Comparison is not case-sensitive.</param>
		/// <returns></returns>
		IElement GetOrderSummaryRowItem(string rowLabel);

		IElement GetProductName(IElement row);
		IElement GetProductStatus(IElement row);
		IElement GetProductTracking(IElement row);
        IElement GetProductUnitPrice(IElement row);
        IElement GetProductQty(IElement row);
        IElement GetProductExtPrice(IElement row);
        IElement GetProductQuantity(IElement row);
        IElement GetUnitPrice(IElement row);

		/// <summary>
		/// Get the order ID.
		/// </summary>
		/// <returns></returns>
		string GetOrderId();

		/// <summary>
		/// Get the order date.
		/// </summary>
		/// <returns></returns>
		string GetOrderDate();

        /// <summary>
        /// Check for Shipping Update modal on the Order Details page.
        /// </summary>
        /// <returns></returns>
        bool DoesShippingUpdatesModalShow();

        /// <summary>
        /// Get the billing information.
        /// </summary>
        /// <returns></returns>
        string GetOrderBillingInfo();

		/// <summary>
		/// Get the ship to address.
		/// </summary>
		/// <returns></returns>
		string GetOrderShippingInfo();

		/// <summary>
		/// Get the product name for an OrderHistoryItems.
		/// </summary>
		/// <param name="orderDetailItem">Object to get the product name from.</param>
		/// <returns></returns>
		string GetProductNameString(OrderHistoryItems orderDetailItem);

		/// <summary>
		/// Get the sales associate information.
		/// </summary>
		/// <returns></returns>
		string GetSalesAssociateInfo();

		/// <summary>
		/// Replace the following in the given string with an empty string.
		/// "\r\n", " ", "(", ")", "-".
		/// </summary>
		/// <param name="originalString">String to remove special characters from.</param>
		/// <returns>Formatted string.</returns>
		string RemoveFormatting(string originalString);

		/// <summary>
		/// Get formatted string with order status.
		/// </summary>
		/// <param name="orderDetailItem">OrderHistoryItems object to build the status string from.</param>
		/// <returns>Formatted status string.</returns>
		string GetStatusString(OrderHistoryItems orderDetailItem);

		/// <summary>
		/// Format the price "{price:C}".
		/// </summary>
		/// <param name="price">Price to format "{price:C}".</param>
		/// <returns>Formatted price string.</returns>
		string FormatPrice(decimal price);

		/// <summary>
		/// Get formatted shipping and processing price.
		/// </summary>
		/// <param name="orderDetailItem">OrderHistoryItems object to build the shipping and processing price from.</param>
		/// <returns>Formatted shipping and processing price.</returns>
		string ShippingProcessingPrice(OrderHistoryItems orderDetailItem);

		/// <summary>
		/// Convert OrderHistoryItems object to a shipping info string.
		/// </summary>
		/// <param name="orderDetailItem">Populated object to format.</param>
		/// <returns>Formatted shipping info string based on provided order detail item.</returns>
		string ShippingInfo(OrderHistoryItems orderDetailItem);

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);

        /// <summary>
        /// Method to close Shipping Updates modal if it appears.
        /// </summary>
        void HandleShippingUpdatesModal();
    }
}
