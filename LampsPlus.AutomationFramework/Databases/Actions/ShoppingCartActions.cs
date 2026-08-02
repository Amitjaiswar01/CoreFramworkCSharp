using System.Data.SqlClient;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart;

namespace LampsPlus.AutomationFramework.Databases.Actions
{
    /// <summary>
    /// Helper to provide access to shopping cart related database queries.
    /// </summary>
    public class ShoppingCartActions
    {
        /// <inheritdoc />
        public ShoppingCartActions(string cartEasyConnectionString, string assetsConnectionString)
        {
            CartEasyConnectionString = cartEasyConnectionString;
            AssetsConnectionString = assetsConnectionString;
        }

        /// <summary>
        /// Assets database connection string.
        /// </summary>
        public string AssetsConnectionString { get; set; }

        /// <summary>
        /// CartEasy database connection string.
        /// </summary>
        public string CartEasyConnectionString { get; }

        /// <summary>
        /// Find cart total values from Assets database.
        /// </summary>
        /// <param name="cartId">Cart Id.</param>
        /// <returns>Cart Id in the database.</returns>
        public ShoppingCartSummaryModel GetCartTotalSection(string cartId)
        {
            var model = new ShoppingCartSummaryModel();

            using (var conn = new SqlConnection(AssetsConnectionString))
            {
                using (var cmd = new SqlCommand(GetCartTotalValues.Query(cartId), conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        model.CartId = cartId;
                        model.ItemTotal = (decimal)reader["ItemTotal"];
                        model.ShippingTotal = (decimal)reader["FreightTotal"];
                        model.TaxTotal = (decimal)reader["TaxTotal"];
                        model.OrderTotal = (decimal)reader["OrderTotal"]; 
                    }
                }
            }

            return model;
        }
    }
}
