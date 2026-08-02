namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Model for an orderID which includes the orderID identifier and the username or email address associated with it.
    /// </summary>
    public class OrderIdModel
    {
        public int CashierEmployee { get; set; }
        public int CommissionEmployee { get; set; }

        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; }
    }
}
