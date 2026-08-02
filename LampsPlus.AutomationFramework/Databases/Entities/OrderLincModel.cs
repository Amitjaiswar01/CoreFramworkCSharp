namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Model for the Linc widget on the Order Details page.
    /// </summary>
    public class OrderLincModel
    {
        public bool? LincCompatible { get; set; }

        public int? PickUpFromStore { get; set; }

        public string ItemStatus { get; set; }
        public string OrderId { get; set; }
        public string ShipToCountry { get; set; }
        public string ShortSku { get; set; }
    }
}
