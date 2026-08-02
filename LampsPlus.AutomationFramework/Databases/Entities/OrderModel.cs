using System;

namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Model for an order that outlines all the fields.
    /// </summary>
    public class OrderModel
    {
        public DateTime CreatedDate { get; set; }

        public decimal ItemTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal SAndP { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int SalesAssociate { get; set; }

        public string BillToAddressLine1 { get; set; }
        public string BillToAddressLine2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToCountry { get; set; }
        public string BillToFirstname { get; set; }
        public string BillToLastname { get; set; }
        public string BillToPhoneNumber { get; set; }
        public string BillToState { get; set; }
        public string BillToZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string ProductName { get; set; }
        public string RewardNumber { get; set; }
        public string ShipToAddressLine1 { get; set; }
        public string ShipToAddressLine2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZipCode { get; set; }
        public string ShortSku { get; set; }
        public string TrackingNumber { get; set; }
    }
}
