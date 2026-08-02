namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Contains details about payment information.
    /// </summary>
    public class PaymentInfoModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string CardholderName { get; set; }
        public string CardType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExpirationDate { get; set; }
        public string LastFourDigit { get; set; }
        public string PaymentToken { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
