namespace LampsPlus.AutomationFramework.Databases.Entities
{    /// <summary>
    /// Product model for the AR.
    /// </summary>
    public class ArProductModel
    {
        public string ShortSku { get; set; }
        public decimal RetailPriceInternet { get; set; }
        public decimal SalePrice1Internet { get; set; }
        public string ProductName { get; set; }
    }
}