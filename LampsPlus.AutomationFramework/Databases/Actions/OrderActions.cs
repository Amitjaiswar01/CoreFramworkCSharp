using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Databases.Actions
{
    /// <summary>
    /// Helper to provide access to account related database queries.
    /// </summary>
    public class OrderActions
    {
        private const string OrderIdString = "@orderId";

        /// <summary>
        /// Connection strings for the various databases needed.
        /// </summary>
        /// <param name="cartEasyConnectionString">Database connection string to access CartEasy data.</param>
        /// <param name="assetsConnectionString">Database connection string to access Asset data.</param>
        /// <param name="domExportOrderConnectionString">Database connection string to access Dom Export Order data.</param>
        /// <param name="userProfileConnectionString">Database connection string to access User Profile data.</param>
        public OrderActions(string cartEasyConnectionString, string assetsConnectionString, string domExportOrderConnectionString, string userProfileConnectionString)
        {
            CartEasyConnectionString = cartEasyConnectionString;
            AssetsConnectionString = assetsConnectionString;
            DomExportOrderConnectionString = domExportOrderConnectionString;
            UserProfileConnectionString = userProfileConnectionString;
        }

        /// <summary>
        /// Assets database connection string.
        /// </summary>
        public string AssetsConnectionString { get; set; }

        /// <summary>
        /// DomExportOrder database connection string.
        /// </summary>
        public string DomExportOrderConnectionString { get; }

        /// <summary>
        /// CartEasy database connection string.
        /// </summary>
        public string CartEasyConnectionString { get; }

        /// <summary>
        /// UserProfile database connection string.
        /// </summary>
        public string UserProfileConnectionString { get; }

        /// <summary>
        /// Get hold reasons based on the given order id.
        /// </summary>
        /// <param name="orderId">Order id to get hold reason(s) for.</param>
        /// <returns>Hold reasons for a given order id.</returns>
        public List<string> GetHoldReasonsByOrderId(string orderId)
        {
            var holdReasons = new List<string>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.SubmittingOrders.FindOrderHoldReasons.Query(orderId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        holdReasons.Add((string)reader["Description"]);
                    }
                }
            }

            return holdReasons;
        }

		/// <summary>
		/// Check if Order ID exists in the database - TblGlobalOrderHeader
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns>true if Order ID exists</returns>
		public OrderIdModel CheckOrderIdExists(string orderId)
		{
			var orderIdModel = new OrderIdModel{ OrderId = string.Empty};
			using (var conn = new SqlConnection(AssetsConnectionString))
		    {
			    using (var cmd = new SqlCommand(Queries.Orders.OrderIdExists.QueryForOrderHeader(orderId), conn))
			    {
				    conn.Open();
				    var reader = cmd.ExecuteReader();
				    if (reader.Read())
				    {
					    orderIdModel = new OrderIdModel()
						{
							// If more properties are needed for verify, they can be populated here. 
							OrderId = (string)reader["orderid"],
							CommissionEmployee = (int)reader["CommissionEmployee"],
							CashierEmployee = (int)reader["CashierEmployee"]
						};
					}
			    }
		    }

		    return orderIdModel;
	    }

        /// <summary>
        /// Get Order information from Assets
        /// </summary>
        /// <returns>Returns order model</returns>
        public OrderModel GetOrderIdRecordsInAssets(string orderId)
        {
            return GetOrderIdRecords(AssetsConnectionString, Queries.SubmittingOrders.GetOrderRecordsInTblGlobalOrderHeader.Query(orderId));
        }

        private OrderModel GetOrderIdRecords(string connectionString, string query)
        {
            OrderModel model = null;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        model = new OrderModel()
                        {
                            OrderId = (string)reader["orderid"],
                            ShortSku = (string)reader["ShortSku"],
                            ProductName = (string)reader["productname"],
                            ItemTotal = (decimal)reader["ItemTotal"],
                            SAndP = (decimal)reader["SAndP"],
                            TaxTotal = (decimal)reader["TaxTotal"],
                            OrderTotal = (decimal)reader["OrderTotal"]
                        };
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// Get a user account based on the given email address.
        /// </summary>
        /// <returns></returns>
        public OrderIdModel GetAnOrderIdPlacedWithin60Days()
        {
            OrderIdModel orderId = null;
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderPlacedInLast60Days.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderId = new OrderIdModel()
                        {
                            OrderId = (string)reader["orderid"],
                            UserName = (string)reader["emailaddress"]
                        };
                    }
                }
            }
            return orderId;
        }

        public OrderIdModel GetOpenBoxOrder()        {            OrderIdModel orderId = null;            using (var conn = new SqlConnection(AssetsConnectionString))            {                using (var cmd = new SqlCommand(Queries.Orders.OpenBoxOrder.Query, conn))                {                    conn.Open();                    var reader = cmd.ExecuteReader();                    while (reader.Read())                    {                        orderId = new OrderIdModel                        {                            OrderId = (string)reader["orderid"],                            UserName = (string)reader["emailaddress"],                        };                    }                }            }            return orderId;        }

        /// <summary>
        /// Get an order which placed using the PayPal payment option.
        /// </summary>
        /// <returns></returns>
        public OrderIdModel GetAnOrderIdPlacedWithPayPal()
        {
            OrderIdModel orderId = null;
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderWithPayPal.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderId = new OrderIdModel()
                        {
                            OrderId = (string)reader["orderid"],
                            UserName = (string)reader["emailaddress"]
                        };
                    }
                }
            }
            return orderId;
        }

        /// <summary>
        /// Get detailed order items information that can be displayed in the order history page
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderHistoryItems> GetOrderHistoryItems(string orderId)
        {
            var historyItems = new List<OrderHistoryItems>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderDetails.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(OrderIdString, orderId));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        historyItems.Add(new OrderHistoryItems
                        {
                            OrderId = Convert.ToString(reader["orderid"]),
                            CreatedDate = Convert.ToDateTime(reader["OrderDate"]),
                            ShipDate = reader["ShipDate"] == DBNull.Value ? (DateTime?) null : (DateTime) reader["ShipDate"],
                            EmailAddress = Convert.ToString(reader["emailaddress"]),
                            SalesAssociate = Convert.ToInt32(reader["SalesAssociate"]),
                            BillToFirstName = Convert.ToString(reader["billtofirstname"]),
                            BillToLastName = Convert.ToString(reader["billtolastname"]),
                            BillToAddressLine1 = Convert.ToString(reader["BillToAddressLine1"]),
                            BillToAddressLine2 = Convert.ToString(reader["BillToAddressLine2"]),
                            BillToCity = Convert.ToString(reader["BillToCity"]),
                            BillToState = Convert.ToString(reader["BillToState"]),
                            BillToZipCode = Convert.ToString(reader["BilltoZipcode"]),
                            BillToCountry = Convert.ToString(reader["BillToCountry"]),
                            CreditCardType = Convert.ToString(reader["CreditCardType"]),
                            CreditCardLastFour = Convert.ToString(reader["CreditCardLastFour"]),
                            BillToPhoneNumber = Convert.ToString(reader["BillToPhoneNumber"]),
                            PaymentMethod = Convert.ToString(reader["PaymentMethod"]),
                            RewardNumber = Convert.ToInt64(reader["RewardNumber"]),
                            ShipToFirstName = Convert.ToString(reader["ShiptoFirstname"]),
                            ShipToLastName = Convert.ToString(reader["ShiptoLastname"]),
                            ShipToAddressLine2 = Convert.ToString(reader["ShiptoAddressLine2"]),
                            ShipToAddressLine1 = Convert.ToString(reader["ShiptoAddressLine1"]),
                            ShipToCity = Convert.ToString(reader["ShiptoCity"]),
                            ShipToState = Convert.ToString(reader["ShiptoState"]),
                            ShipToZipCode = Convert.ToString(reader["ShiptoZipcode"]),
                            ShipToCountry = Convert.ToString(reader["ShiptoCountry"]),
                            ShipToPhoneNumber = Convert.ToString(reader["ShipToPhoneNumber"]),
                            ProductName = Convert.ToString(reader["ProductName"]),
                            ShortSku = Convert.ToString(reader["ShortSku"]),
                            OrderStatus = Convert.ToString(reader["ItemStatus"]),
                            ExpectedShipDate = reader["ExpectedShipDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ExpectedShipDate"],
                            FirstShipDate = reader["FirstShipDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FirstShipDate"],
                            LastShipDate = reader["LastShipDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["LastShipDate"],
                            FirstDeliveryDate = reader["FirstDeliveryDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["FirstDeliveryDate"],
                            LastDeliveryDate = reader["LastDeliveryDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["LastDeliveryDate"],
                            TrackingType = Convert.ToString(reader["TrackingType"]),
                            TrackingNumber = Convert.ToString(reader["TrackingNumber"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                            ItemTotal = Convert.ToDecimal(reader["ItemTotal"]),
                            ExtPrice = Convert.ToDecimal(reader["ExtPrice"]),
                            PriceAdjustment = Convert.ToDecimal(reader["PriceAdjustment"]),
                            FreightTotal = Convert.ToDecimal(reader["FreighTtotal"]),
                            TaxTotal = Convert.ToDecimal(reader["Taxtotal"]),
                            OrderTotal = Convert.ToDecimal(reader["Ordertotal"])
                        });
                    }
                }
            }
            return historyItems;
        }

        /// <summary>
        /// Get the Linc widget information for an order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderLincModel> GetLincInfo(string orderId)
        {
            var model = new List<OrderLincModel>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderLincCompatible.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter(OrderIdString, orderId));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        model.Add(new OrderLincModel()
                        {
                            OrderId = (string)reader["orderid"],
                            ShortSku = (string)reader["shortsku"],
                            PickUpFromStore = (int?)reader["pickupfromstore"],
                            LincCompatible = (bool?)reader["linccompatible"],
                            ShipToCountry = (string)reader["shiptocountry"],
	                        ItemStatus = (string)reader["itemstatus"]
						});
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// Find orders that have each available status.
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderForEachStatus()
        {
            var model = new List<OrderModel>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderForEachStatus.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        model.Add(new OrderModel()
                        {
                            OrderId = (string)reader["orderid"],
                            OrderStatus = (string)reader["orderstatus"],
                            EmailAddress = (string)reader["emailaddress"]
                        });
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// Find orders that have each available status.
        /// </summary>
        /// <returns></returns>
        public List<OrderIdModel> GetOrderForTheEachStatus()
        {
            var model = new List<OrderIdModel>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderForEachStatus.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        model.Add(new OrderIdModel()
                        {
                            OrderId = (string)reader["orderid"],
                            UserName = (string)reader["emailaddress"],
                            OrderStatus = (string)reader["orderstatus"]
                        });
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// Get the order details for an order on the Order History page.
        /// </summary>
        /// <returns></returns>
        public List<OrderModel> GetOrderDetailsForOrderHistory()
        {
            var model = new List<OrderModel>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderHistoryOrderDetails.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        model.Add(new OrderModel
                        {
                            OrderId = TextActions.NormalizeWhitespace((string)reader["orderid"]),
                            RewardNumber = TextActions.NormalizeWhitespace(reader["rewardnumber"].ToString()),
                            CreatedDate = (DateTime)reader["createddate"],
                            SalesAssociate = (int)reader["SalesAssociate"],
                            BillToFirstname = TextActions.NormalizeWhitespace((string)reader["billtofirstname"]),
                            BillToLastname = TextActions.NormalizeWhitespace((string)reader["billtolastname"]),
                            BillToAddressLine1 = TextActions.NormalizeWhitespace((string)reader["billtoaddressline1"]),
                            BillToAddressLine2 = TextActions.NormalizeWhitespace((string)reader["billtoaddressline2"]),
                            BillToCity = TextActions.NormalizeWhitespace((string)reader["billtocity"]),
                            BillToState = TextActions.NormalizeWhitespace((string)reader["billtostate"]),
                            BillToZipCode = TextActions.NormalizeWhitespace((string)reader["billtozipcode"]),
                            BillToCountry = TextActions.NormalizeWhitespace((string)reader["billtocountry"]),
                            BillToPhoneNumber = TextActions.NormalizeWhitespace((string)reader["billtophonenumber"]),
                            ShipToFirstName = TextActions.NormalizeWhitespace((string)reader["shiptofirstname"]),
                            ShipToLastName = TextActions.NormalizeWhitespace((string)reader["shiptolastname"]),
                            ShipToAddressLine1 = TextActions.NormalizeWhitespace((string)reader["shiptoaddressline1"]),
                            ShipToAddressLine2 = TextActions.NormalizeWhitespace((string)reader["shiptoaddressline2"]),
                            ShipToCity = TextActions.NormalizeWhitespace((string)reader["shiptocity"]),
                            ShipToState = TextActions.NormalizeWhitespace((string)reader["shiptostate"]),
                            ShipToZipCode = TextActions.NormalizeWhitespace((string)reader["shiptozipcode"]),
                            ShipToCountry = TextActions.NormalizeWhitespace((string)reader["shiptocountry"]),
                            ProductName = TextActions.NormalizeWhitespace((string)reader["productname"]),
                            OrderStatus = TextActions.NormalizeWhitespace((string)reader["orderstatus"]),
                            TrackingNumber = TextActions.NormalizeWhitespace((string)reader["trackingnumber"]),
                            Quantity = (int)reader["quantity"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            ItemTotal = (decimal)reader["itemtotal"],
                            SAndP = (decimal)reader["S&P"],
                            TaxTotal = (decimal)reader["taxtotal"],
                            OrderTotal = (decimal)reader["ordertotal"],
                            EmailAddress = TextActions.NormalizeWhitespace((string)reader["emailaddress"]),
                            ShortSku = TextActions.NormalizeWhitespace((string)reader["shortsku"])
                        });
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// Get Linc compatible order.
        /// </summary>
        /// <returns>OrderModel with orderid and email address of Linc order.</returns>
        public OrderIdModel GetLincQualifyingOrders()
        {
            OrderIdModel orderId = null;
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderLincQualifying.Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orderId = new OrderIdModel
                        {
                            OrderId = (string)reader["orderid"],
                            UserName = (string)reader["emailaddress"],
                        };
                    }
                }
            }

            return orderId;
        }
       
        /// <summary>
        /// Get CommissionEmployee by customer email from Order with CSR Secondary Employee Number.
        /// </summary>
        /// <returns>OrderModel with CommissionEmployee CSR order.</returns>
        public OrderIdModel GetCommissionEmployeeWithCsrOrder(string email)
        {
            OrderIdModel orderIdModel = null;
            using (var conn = new SqlConnection(UserProfileConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.SubmittingOrders.FindUserCommissionNumber.Query(email), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderIdModel = new OrderIdModel
                        {
                            CommissionEmployee = (Int32)reader["commissionemployeenumber"]
                        };
                    }
                }
            }

            return orderIdModel;
        }

        /// <summary>//TODO New method
        /// Get Employee Number By UserName
        /// </summary>
        /// <returns>userInfo object.</returns>
        public UserInfo GetEmployeeNumberByUserName(string userName)
        {
            UserInfo userInfo = null;
            using (var conn = new SqlConnection(UserProfileConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.UserProfile.FindUserEmployeeNumber.Query(userName), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userInfo = new UserInfo
                        {
                            EmployeeNumber = (Int32)reader["employeenumber"]
                        };
                    }
                }
            }
            return userInfo;
        }

        /// <summary>
        /// Get Global order details by shipping email used
        /// </summary>
        /// <returns>OrderModel with customers payment details.</returns>
        public OrderModel GetGlobalOrderDetails(string shippingEmail)
        {
            OrderModel orderModel = null;
            using (var conn = new SqlConnection(UserProfileConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.GlobalOrderDetails.Query(shippingEmail), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderModel = new OrderModel
                        {
                            EmailAddress = (string)reader["emailaddress"],
                            BillToFirstname = (string)reader["firstname"],
                            BillToLastname = (string)reader["lastname"],
                            BillToZipCode = (string)reader["billtozipcode"]
                        };
                    }
                }
            }

            return orderModel;
        }

        /// Get Discount and Freight Totals for order.
        /// </summary>
        /// <returns>OrderHistoryItems model with discount and freight totals.</returns>
        public OrderHistoryItems GetOrderDiscountAndFreightTotals(string orderId)
        {
            OrderHistoryItems order = null;
            using (var conn = new SqlConnection(UserProfileConnectionString))
            {
                using (var cmd = new SqlCommand(Queries.Orders.OrderDiscountAndFreightTotals.Query(orderId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order = new OrderHistoryItems
                        {
                            ManualDiscount = (decimal)reader["ManualDiscount"],
                            FreightTotal = (decimal)reader["FreightTotal"]
                        };
                    }
                }
            }

            return order;
        }
    }
}
