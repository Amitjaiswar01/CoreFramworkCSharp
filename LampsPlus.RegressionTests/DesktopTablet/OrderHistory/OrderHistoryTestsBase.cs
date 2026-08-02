using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Web;
using Xunit;
using Xunit.Abstractions;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderHistory
{
    /// <summary>
    /// Base class for Order History / Order Detail specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.OrderHistory)]
    public class OrderHistoryTestsBase : TestsBase
    {
        /// <summary>
        /// Test base for Order History / Order Detail.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public OrderHistoryTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verifies order detail pages' product status is correct in relation with the status of the order in the database.
        /// Verifies the string mapping between database and view.
        /// </summary>
        /// <param name="dbOrderStatus">String of product status in database.</param>
        /// <param name="pageOrderStatus">String of product status on the order details page.</param>.
        /// <param name="orderId">String of order id</param>
        public void VerifyLineItemStatusIsCorrect(string dbOrderStatus, string pageOrderStatus, string orderId = "")
        {
            Browser.Wait.ForDomReady();
            pageOrderStatus = pageOrderStatus.ToLower().Trim();
            bool isValid;
            var arrivalRangeRegex = @"arrives( between)? [a-z]{3}\. [0-9]{1,2}( - [a-z]{3}\. [0-9]{1,2})?";
            var shippedDateRegex = @"shipped? [a-z]{3}\. [0-9]{1,2}( - [a-z]{3}\. [0-9]{1,2})?";

            switch (dbOrderStatus.ToLower())
            {
                case "shipped":
                    isValid = Regex.Match(pageOrderStatus, shippedDateRegex).Success;

                    Assert.True(isValid,
                        $"Shipped status \"{pageOrderStatus}\" is not a shipped date.");

                    Browser.Wait.ForDisplayedElement(Browser.Locate.ElementByClassName(OrderDetails.ProductColClass));

                    var trackItemButton = OrderHistory.GetTrackItemButton(OrderDetails.OrderDetailTableFirstRow[0]);
                    
                    var homeWindow = Browser.Driver.CurrentWindowHandle;

                    if (OperatingSystem == OperatingSystem.iPhone)
                    {
                        var pixelsScroll = 250;
                        Browser.Wait.ForDomReady();
                        Browser.ScrollToByPixelsVertical(pixelsScroll.ToString());
                        var trackItem  = OrderHistory.TrackItem.GetAttribute("href");
                        Browser.Navigate(trackItem);
                    }
                    else
                    {
                        trackItemButton.Click();
                        Browser.WaitForNewTab(10);
                        Browser.SwitchToCurrentWindow();
                    }


                    Browser.Wait.ForPageWait(Urls.LetsLincUrl,30);

                    Assert.StringContains(Browser.PageUrl, Urls.LetsLincUrl, "Didn't redirect to letslinc.com after clicking track order.");
                    Assert.StringContains(Browser.PageUrl, orderId, "Tracking number not present in url after clicking track order.");

                    //close current browser tab
                    Browser.CloseAllWindowsButOriginal(homeWindow);
                    Browser.Driver.SwitchTo().Window(homeWindow);

                    break;

                case "canceled":
                    // Database and order status page differ in spelling of canceled vs cancelled
                    Assert.Equals("cancelled", pageOrderStatus, $"Customer's order status \"{pageOrderStatus}\" is not equal to database status \"cancelled\".");
                    break;

                case "pickedup":
                    Assert.Equals("picked up", pageOrderStatus, $"Customer's order status \"{pageOrderStatus}\" is not equal to database status \"picked up\".");
                    break;

                case "pending":
                    isValid = pageOrderStatus == "status pending" ||
                              Regex.IsMatch(pageOrderStatus, arrivalRangeRegex) ||
                              Regex.IsMatch(pageOrderStatus, @"backorder( [0-9]{2}/[0-9]{2}/[0-9]{4})?");

                    Assert.True(isValid, $"Customer's order status \"{pageOrderStatus}\" is not valid for a pending order.");
                    break;
            }
        }

        /// <summary>
        /// Verifies if one string is contained in other. Lowercases all letters before compare.
        /// </summary>
        /// <param name="baseString">The string which the other string is supposed to be contained in.</param>
        /// <param name="subString">The string that should be contained in the other.</param>
        public void VerifyIfStringIsContained(string baseString, string subString)
        {
            baseString = baseString.ToLower();
            subString = subString.ToLower();

            Assert.StringContains(baseString, subString, $"{subString} is not contained in {baseString}.");
        }

        /// <summary>
        /// Verify the total price on the page matches the total for the product in the database.
        /// </summary>
        /// <param name="dbTotal">Expected total in the database.</param>
        /// <param name="pageTotalName">Total price shown for the product on the website.</param>
        public void VerifyIfPageTotalsMatchDb(decimal dbTotal, string pageTotalName)
        {
            Assert.Equals(TextActions.FormatToTwoDecimals(dbTotal), OrderHistory.GetTotalFromSummaryByName(pageTotalName), $"Total for {pageTotalName} doesn't match between database and page.");
        }

        /// <summary>
        /// Search for an order and get to the order detail page.
        /// </summary>
        /// <param name="order">OrderModel containing at least the order ID and email address of the order.</param>
        public void SearchForOrder(OrderModel order)
        {
            OrderHistory.OrderIdField.SendKeys(order.OrderId);
            OrderHistory.EmailField.SendKeys(order.EmailAddress);
            OrderHistory.CheckStatusBtn.Click();

            Browser.Wait.ForClickableElement(OrderHistory.ProductLink);
        }

        /// <summary>
        /// Verify the OrderID is correct on the page.
        /// </summary>
        /// <param name="order"></param>
        public void VerifyOrderIdOnPageIsCorrect(OrderModel order)
        {
            Browser.RefreshPage();
            VerifyIfStringIsContained(OrderDetails.OrderIdHeader.Text, order.OrderId);
        }

        /// <summary>
        /// Verify the date of the order is correct on the page.
        /// </summary>
        /// <param name="order"></param>
        public void VerifyOrderDateOnPageIsCorrect(OrderModel order)
        {
            var formattedOrderDate = order.CreatedDate.ToString("dddd, MMMM dd, yyyy");

            VerifyIfStringIsContained(OrderDetails.OrderDateHeader.Text, formattedOrderDate);
        }

        /// <summary>
        /// Verify the Sales Associate number is correct for an order.
        /// </summary>
        /// <param name="order"></param>
        public void VerifySalesAssociateNumberOnPageIsCorrect(OrderModel order)
        {
            VerifyIfStringIsContained(OrderDetails.GetSalesAssociateInfo(), order.SalesAssociate.ToString());
        }

        /// <summary>
        /// Verify the billing information is correct on the page.
        /// </summary>
        /// <param name="order"></param>
        public void VerifyBillingInfoOnPageIsCorrect(OrderModel order)
        {
            var billingInfoOnPage = OrderDetails.GetOrderBillingInfo();
            var billToFullName = $"{order.BillToFirstname} {order.BillToLastname}";
            var address = $"{order.BillToCity},{order.BillToState} {order.BillToZipCode} {order.BillToCountry}";

            VerifyIfStringIsContained(billingInfoOnPage, billToFullName);
            VerifyIfStringIsContained(billingInfoOnPage, order.BillToAddressLine1);
            VerifyIfStringIsContained(billingInfoOnPage, order.BillToAddressLine2);
            VerifyIfStringIsContained(billingInfoOnPage, address);
            VerifyIfStringIsContained(billingInfoOnPage, order.EmailAddress);

            var formattedPhoneNumber = TextActions.NormalizePhoneNumber(order.BillToPhoneNumber);

            VerifyIfStringIsContained(billingInfoOnPage, formattedPhoneNumber);
        }

        /// <summary>
        /// Verify the Shipping information is correct on the page.
        /// </summary>
        /// <param name="order"></param>
        public void VerifyShippingInfoOnPageIsCorrect(OrderModel order)
        {
            var shippingInfoOnPage = OrderDetails.GetOrderShippingInfo();
            var shipToFullName = $"{order.ShipToFirstName}{(order.ShipToLastName != string.Empty ? $" {order.ShipToLastName}" : string.Empty)}";

            VerifyIfStringIsContained(shippingInfoOnPage, shipToFullName);
            VerifyIfStringIsContained(shippingInfoOnPage, order.ShipToAddressLine1);
            VerifyIfStringIsContained(shippingInfoOnPage, order.ShipToAddressLine2);
            VerifyIfStringIsContained(shippingInfoOnPage, $"{order.ShipToCity},{order.ShipToState} {order.ShipToZipCode} {order.ShipToCountry}");
        }

        /// <summary>
        /// Verify the correct products are displayed on the page.
        /// </summary>
        /// <param name="orders"></param>
        public void VerifyLineItemsOnPageAreCorrect(List<OrderModel> orders)
        {
            var lineItems = OrderDetails.OrderDetailTableFirstRow;

            foreach (var item in lineItems)
            {
                var pageName = OrderDetails.GetProductName(item).Text;
                var dbOrder = orders.First(x => pageName.Contains(HttpUtility.HtmlDecode(x.ProductName) ?? throw new InvalidOperationException()));
                VerifyLineItemProductNameIsCorrect(pageName, dbOrder);
                VerifyLineItemStatusIsCorrect(dbOrder.OrderStatus, OrderDetails.GetProductStatus(item).Text, dbOrder.OrderId);
                VerifyLineItemTrackingIsCorrect(OrderDetails.GetProductTracking(item), dbOrder.TrackingNumber);
                VerifyLineItemUnitPriceIsCorrect(OrderDetails.GetProductUnitPrice(item).Text.Trim(), dbOrder.UnitPrice);
                VerifyLineItemQuantityIsCorrect(OrderDetails.GetProductQty(item).Text.Trim(), dbOrder.Quantity);
            }
        }

        /// <summary>
        /// Verify the name of the product is correct.
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="order"></param>
        public void VerifyLineItemProductNameIsCorrect(string pageName, OrderModel order)
        {
            var dbName = $"{HttpUtility.HtmlDecode(order.ProductName)} ({order.ShortSku})";

            Assert.True(pageName.Contains(HttpUtility.HtmlDecode(dbName)), $"Line item product name {pageName} doesn't match database name {dbName}.");
        }

        /// <summary>
        /// Verify the tracking status of an item is correct.
        /// </summary>
        /// <param name="trackingCell"></param>
        /// <param name="trackingNumber"></param>
        public void VerifyLineItemTrackingIsCorrect(IElement trackingCell, string trackingNumber)
        {
            // LampsPlus.AutomationFramework.Pages.OrderHistory.OrderHistory.LincCareWidgetAnchor2014Class
            if (trackingNumber == string.Empty) return;

            // Check for a tracking number match on page or that a tracking button that is present
            var isValid = trackingCell.Text.Trim().Replace(" ", string.Empty).Contains(trackingNumber) ||
                          OrderHistory.GetTrackItemButton(trackingCell).Displayed;

            Assert.True(isValid, "Tracking information for line item is incorrect.");
        }

        /// <summary>
        /// Verify the unit price is correct for an item.
        /// </summary>
        /// <param name="pagePrice"></param>
        /// <param name="dbPrice"></param>
        public void VerifyLineItemUnitPriceIsCorrect(string pagePrice, decimal dbPrice)
        {
            pagePrice = TextActions.RemoveDollarSign(pagePrice);
            var convertedDbPrice = TextActions.FormatToTwoDecimals(dbPrice);

            Assert.Equals(convertedDbPrice, pagePrice,
                $"Unit price in database({convertedDbPrice}) doesn't match price on page({pagePrice}).");
        }

        /// <summary>
        /// Verify the quantity is correct for an item on the page.
        /// </summary>
        /// <param name="pageQty"></param>
        /// <param name="dbQty"></param>
        public void VerifyLineItemQuantityIsCorrect(string pageQty, int dbQty)
        {
            Assert.Equals(dbQty.ToString(), pageQty, $"Line item quantity {pageQty} doesn't match database quantity {dbQty}.");
        }

        /// <summary>
        /// Verify the order total summary is correct on the page.
        /// </summary>
        /// <param name="order"></param>
        public void VerifySummaryTotalsOnPageAreCorrect(OrderModel order)
        {
            VerifyIfPageTotalsMatchDb(order.ItemTotal, "product total");
            VerifyIfPageTotalsMatchDb(order.SAndP, "shipping & processing");
            VerifyIfPageTotalsMatchDb(order.TaxTotal, "tax");
            VerifyIfPageTotalsMatchDb(order.OrderTotal, "order total");
        }

        public void VerifyOrderDetails(OrderHistoryItems orderDetailItem)
        {
            var matchedRow = OrderDetails.OrderDetailTableRows.FirstOrDefault(r => r.Text.Contains(orderDetailItem.ShortSku));

            Assert.Equals(OrderDetails.GetOrderId(), orderDetailItem.OrderId, "Order Ids do not match");
            Assert.Equals(DateTime.Parse(OrderDetails.GetOrderDate(), new CultureInfo("en-US")).Date, orderDetailItem.CreatedDate.Date, "Order dates do not match");
            Assert.Equals(OrderDetails.GetProductName(matchedRow).Text.Replace("OPEN BOX ITEM\r\n", ""), OrderDetails.GetProductNameString(orderDetailItem), "Product Names do not match");
            if(OrderDetails.TrackItemLink.IsInitialized)
                Assert.Displayed(OrderDetails.TrackItemLink, "Track Item link not displayed");
            Assert.Equals(OrderDetails.GetProductQty(matchedRow).Text.Trim(), orderDetailItem.Quantity.ToString(), "Quantities do not match");
            Assert.Equals(OrderDetails.GetProductUnitPrice(matchedRow).Text.Trim(), OrderDetails.FormatPrice(orderDetailItem.UnitPrice), "Unit Prices do not match");
            Assert.Equals(OrderDetails.OrderItemTotal.Text, OrderDetails.FormatPrice(orderDetailItem.ItemTotal), "Item total prices do not match");
            Assert.Equals(OrderDetails.OrderTax.Text, OrderDetails.FormatPrice(orderDetailItem.TaxTotal), "Tax prices do not match");
            Assert.Equals(OrderDetails.OrderTotal.Text, OrderDetails.FormatPrice(orderDetailItem.OrderTotal), "Order Total prices do not match");

            var itemStatus = OrderDetails.GetProductStatus(matchedRow).Text.Replace(" between", "");
            if (itemStatus.ToLower() != "picked up")
            {
                Assert.Equals(itemStatus, OrderDetails.GetStatusString(orderDetailItem), "Item statuses do not match");
            }
        }
    }
}
