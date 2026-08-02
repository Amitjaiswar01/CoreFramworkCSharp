using System;

namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Encompasses the various elements for an order ID located on the Order History page.
    /// </summary>
    public class OrderHistoryItems
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? FirstDeliveryDate { get; set; }
        public DateTime? LastDeliveryDate { get; set; }
        public DateTime? FirstShipDate { get; set; }
        public DateTime? LastShipDate { get; set; }
        public DateTime? ExpectedShipDate { get; set; }
        public DateTime? ShipDate { get; set; }

        public decimal ExtPrice { get; set; }
        public decimal FreightTotal { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal ManualDiscount { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal PriceAdjustment { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal UnitPrice { get; set; }
       
        public int Quantity { get; set; }
        public int SalesAssociate { get; set; }

        public long RewardNumber { get; set; }

        public string BillToAddressLine1 { get; set; }
        public string BillToAddressLine2 { get; set; }
        public string BillToCity { get; set; }
        public string BillToCountry { get; set; }
        public string BillToFirstName { get; set; }
        public string BillToLastName { get; set; }
        public string BillToPhoneNumber { get; set; }
        public string BillToState { get; set; }
        public string BillToZipCode { get; set; }
        public string CreditCardLastFour { get; set; }
        public string CreditCardType { get; set; }
        public string EmailAddress { get; set; }
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string ProductName { get; set; }
        public string ShipToAddressLine1 { get; set; }
        public string ShipToAddressLine2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToPhoneNumber { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZipCode { get; set; }
        public string ShortSku { get; set; }
        public string TrackingType { get; set; }
        public string TrackingNumber { get; set; }
    }
}
