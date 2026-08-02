namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Contains details about a shopping cart summary.
    /// </summary>
    public class ShoppingCartSummaryModel
    {
        public decimal ItemTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal TaxTotal { get; set; }

        public string CartId { get; set; }
    }
}
