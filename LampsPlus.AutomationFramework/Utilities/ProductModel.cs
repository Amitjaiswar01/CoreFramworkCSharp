namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Organizes information about a product on sort, product, or cart pages.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sku for the product.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Amount of the given product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price for the product per unit.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Total price for the product.
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Product inventory.
        /// </summary>
        public int Inventory { get; set; }

        /// <summary>
        /// Current product inventory.
        /// </summary>
        public int CurrentInventory { get; set; }

        /// <inheritdoc />
        public ProductModel(string sku = "")
        {
            Sku = sku;
            Quantity = 1;
        }

        public ProductModel(string name, string price)
        {
            Name = name;
            Price = price;
        }

        /// <inheritdoc />
        public ProductModel(string name, string sku, string quantity, string price)
        {
            Name = name;
            Sku = sku;
            int.TryParse(quantity, out var amount);
            Quantity = amount;
            Price = price;
        }
    }


    /// <summary>
    /// Organizes information about product badges.
    /// </summary>
    public static class Badge
    {
        /// <summary>
        /// More Options
        /// </summary>
        public static string MoreOptions = "More Options";

        /// <summary>
        /// 16+
        /// Colors
        /// </summary>
        public static string SixteenColors = "16+\r\nColors";
    }
}
