using System;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Various components of Shipping Information for an order including time of arrival, shipping level, and cost.
    /// </summary>
    public class ShippingOptionItem
    {
        public DateTime ArrivesDate { get; set; }
        public DateTime LastArrivalDate { get; set; }
        public string ShippingType { get; set; }
        public decimal Cost { get; set; }
    }
}
