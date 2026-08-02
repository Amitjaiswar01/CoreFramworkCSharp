using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IOrderHistory
	{
        #region CSS Selectors
        string CheckStatusButtonId { get; }
        string H4Class { get; }
        string LincOptInWidgetClass { get; }
        string LpContainerId { get; }
        string OrderProductLinkXpath { get; }
        string OrderHistoryRowsTable { get; }
        string ContentWrapClass { get; }
        string OrderNextPageButtonId { get; }
        string OrderClass { get; }
        string ProductColClass { get; }
        string TrackItemClass { get; }
        #endregion

        #region Page Elements
        IElement TrackItem { get; }
        IElement CheckStatusBtn { get; }
        IElement EmailField { get; }
        IElement GetOrderIdLink(string row);
        IElement GetTrackingOrderNumber(string orderNumber);
        IElement GetTrackItemButton(IElement cell);
        IElement LpContainer { get; }
        IElement OptInWidgetElement { get; }
        IElement OpenBoxLabel { get; }
        IElement OrderDetailContainer { get; }
        IElement OrderDetails { get; }        
        IElement OrderIdField { get; }
	    IElement OrderNextPageButton { get; }
        IElement OrderSearchErrorMessageImmediately { get; }
        IElement OrderSummaryContainer { get; }        
        IElement OrderTotalElement { get; }
        IElement PageHeadingElement { get; }
        IElement ProductLink { get; }
        IElement Summary { get; }

        ReadOnlyCollection<IElement> OrderHeaderTitles { get; }		
        ReadOnlyCollection<IElement> OrderPreviewElements { get; }
        ReadOnlyCollection<IElement> OrderDetailElements { get; }
        ReadOnlyCollection<IElement> H5LpContainerElements { get; }
        ReadOnlyCollection<IElement> OrderSummaryRows { get; }

        IElement FirstOrderPreviewElement { get; }

        #endregion

        /// <summary>
        /// Is the BillingInformationSelector element visible?
        /// </summary>
        bool IsBillingInformationSelectorVisible { get; }

		/// <summary>
		/// Is the OrderDateHeading element visible?
		/// </summary>
		bool IsOrderDateHeadingVisible { get; }

		/// <summary>
		/// Is the RewardNumberSelector element visible?
		/// </summary>
		bool IsRewardNumberSelectorVisible { get; }

		/// <summary>
		/// Is the ShippingInformationSelector element visible?
		/// </summary>
		bool IsShippingInformationSelectorVisible { get; }

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        /// <summary>
        /// Formats an order status according to database value.
        /// <param name="orderHistoryItem">Line item information within total order list.</param>
        /// </summary>
        string FormatOrderStatusFromDatabase(OrderHistoryItems orderHistoryItem);


        /// <summary>
        /// Get the product name for the given element.
        /// </summary>
        /// <param name="row">Element to get the product name of.</param>
        /// <returns></returns>
        string GetProductName(IElement row);

		/// <summary>
		/// Get the item status.
		/// </summary>
		/// <param name="row">Element to get the status of.</param>
		/// <returns></returns>
		string GetItemStatus(IElement row);

		/// <summary>
		/// Get the unit price for the given element.
		/// </summary>
		/// <param name="row">Element to get the unit price of.</param>
		/// <returns></returns>
		string GetUnitPrice(IElement row);

		/// <summary>
		/// Get the unit quantity for the given element.
		/// </summary>
		/// <param name="row">Element to get the quantity of.</param>
		/// <returns></returns>
		string GetQuantity(IElement row);

		/// <summary>
		/// Get the order total from the summary block element.
		/// </summary>
		/// <param name="totalName">Name to return the total price for.</param>
		/// <returns></returns>
		string GetTotalFromSummaryByName(string totalName);

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);

        /// <summary>
        /// GetOrderPreview for a given IElement
        /// </summary>
        /// <param name="element">Populates the OrderPreview for a given element</param>
        OrderPreview GetOrderPreview(IElement element);
    }
}
