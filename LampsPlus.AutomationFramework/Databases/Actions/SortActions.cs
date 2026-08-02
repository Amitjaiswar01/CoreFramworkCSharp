using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Databases.Queries.Sort;
using LampsPlus.AutomationFramework.Databases.Queries.SortCallout;

namespace LampsPlus.AutomationFramework.Databases.Actions
{
    /// <summary>
    /// Helper to provide access to shopping cart related database queries.
    /// </summary>
    public class SortActions
    {
        private const string AtFilterIdString = "@filterId";
        private const string AtMmIdString = "@MmId";
        private const string ShortSkuString = "ShortSku";
        private string ShortSku(string sqlQueryString) => GetSku(ShortSkuString, sqlQueryString);

        /// <inheritdoc />
        public SortActions(string assetsConnectionString, string productsConnectionString, string cartEasyConnectionString)
        {
            AssetsConnectionString = assetsConnectionString;
            ProductsConnectionString = productsConnectionString;
            CartEasyConnectionString = cartEasyConnectionString;
        }

        /// <summary>
        /// Assets database connection string.
        /// </summary>
        public string AssetsConnectionString { get; set; }

        public string ProductsConnectionString { get; set; }
        public string CartEasyConnectionString { get; }

        private string GetSku(string typeOfSku, string sqlQueryString)
        {
            var sku = string.Empty;

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlQueryString, conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        sku = (string)reader[typeOfSku];
                    }
                }
            }

            return sku;
        }
        
        public string GetDailySaleQuantityLeftShortSku => ShortSku(DailySaleQuantityLeftCalloutsShortSku.Query);

        /// <summary>
        /// Find cart total values from Assets database.
        /// </summary>
        /// <param name="orderId">Cart Id.</param>
        /// <returns>Cart Id in the database.</returns>
        public List<SortPathPositionModel> GetSortPathPositionForAllUserRoles(string orderId)
        {
            var result = new List<SortPathPositionModel>();

            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(SortPathPositionAllUsersRoles.Query(orderId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var model = new SortPathPositionModel
                        {
                            LineNumber = Convert.ToInt32(reader["linenumber"]),
                            SortPathId = Convert.ToInt32(reader["sortpathid"]),
                            SortPosition = Convert.ToInt32(reader["sortposition"]),
                            SortPath = Convert.ToString(reader["sortpath"])
                        };

                        result.Add(model);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find database positions for selected products in SharedItems table.
        /// </summary>
        /// <param name="orderId">Cart Id.</param>
        /// <returns>Cart Id in the database.</returns>
        public List<SortPathPositionModel> GetSortPathPositionSharedItems(string orderId)
        {
            var result = new List<SortPathPositionModel>();

            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(SortPathPositionSharedItems.Query(orderId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var model = new SortPathPositionModel
                        {
                            LineNumber = Convert.ToInt32(reader["linenumber"]),
                            SortPathId = Convert.ToInt32(reader["sortpathid"]),
                            SortPosition = Convert.ToInt32(reader["sortposition"]),
                            SortPath = Convert.ToString(reader["sortpath"])
                        };
                        result.Add(model);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find database positions for products in the cart in SharedItems table.
        /// </summary>
        /// <param name="cartId">Cart Id.</param>
        public List<SortPathPositionModel> GetSortPathPositionCartItems(string cartId)
        {
            var result = new List<SortPathPositionModel>();

            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(GetCartItemsPathPosition.Query(cartId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var model = new SortPathPositionModel
                        {
                            ShortSku = (string)reader["shortsku"],
                            SortPath = (string)reader["sortpath"],
                            SortPosition = Convert.ToInt32(reader["sortposition"])
                        };
                        result.Add(model);
                    }
                }
            }

            return result;
        }

        public List<string> GetSortPageWithActiveAbTest()
        {
            var sortPageUrls = new List<string>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(SortPageWithActiveAbTest.Query(), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sortPageUrls.Add((string)reader["url"]);
                    }
                }
            }

            return sortPageUrls;
        }
        public List<Dictionary<string, string>> GetSortWithActiveAbTest()
        {
             var sortAbTestInfo = new List<Dictionary<string, string>>();
             using (var conn = new SqlConnection(AssetsConnectionString))
             {
                 using (var cmd = new SqlCommand(GetSortWithActiveAbTestData.Query(), conn))
                 {
                     conn.Open();
                     var reader = cmd.ExecuteReader();
                     while (reader.Read())
                     {
                        var model = new Dictionary<string,string>
                        {
                            {"ProductCategory", (string)reader["NAME"] },
                            {"Url", (string)reader["url"]},
                            {"FilterId", Convert.ToString(reader["filterid"])},
                            {"TestId", (string)reader["TestId"]},
                            {"MmId", (string)reader["MMId"]},
                            {"FormulaId", (string)reader["FormulaId"]},
                            {"PinId", (string)reader["PinId"]},
                            {"TestStartDate", Convert.ToString(reader["startdate"])},
                            {"TestCompositionId", Convert.ToString(reader["TestCompositionId"])}
                        };
                        sortAbTestInfo.Add(model);
                    }
                 }
             }
             return sortAbTestInfo;
        }

        public List<Dictionary<string, string>> GetSortWithNoActiveAbTest()
        {
            var sortAbTestInfo = new List<Dictionary<string, string>>();
            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(GetSortWithNoActiveAbTestData.Query(), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var model = new Dictionary<string, string>
                        {
                            {"ProductCategory", (string)reader["Name"] },
                            {"Url", (string)reader["url"]},
                            {"FilterId", Convert.ToString(reader["filterid"])},
                            {"TestStartDate", Convert.ToString(reader["startdate"])},
                            { "TestCompositionId",  Convert.ToString(reader["TestCompositionId"])}
                        };
                        sortAbTestInfo.Add(model);
                    }
                }
            }
            return sortAbTestInfo;

        }

        public string GetTestCompositionIdForFilter(int filterId)
        {
            var testCompId = string.Empty;

            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                var cmd = new SqlCommand(GetTestCompositionId.Query(), conn);
                cmd.Parameters.Add(new SqlParameter(AtFilterIdString, filterId));
                using (cmd)
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        testCompId = Convert.ToString(reader["TestCompositionId"]);
                    }
                }
            }

            return testCompId;
        }
    }
}
