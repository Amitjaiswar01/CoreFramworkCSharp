namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Order discounts information: Manual and Shipping
    /// </summary>
    public class OrderDiscounts
    {
        public string OrderId;

        public decimal ManualDiscount;

        public decimal ShippingDiscount;
    }
}
