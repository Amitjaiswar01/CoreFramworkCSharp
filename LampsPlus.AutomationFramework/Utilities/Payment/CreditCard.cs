namespace LampsPlus.AutomationFramework.Utilities.Payment
{
    /// <summary>
    /// Object that represents information about a Credit Card.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Credit Card unique identifier.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Type of Credit Card.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Credit Card expiration year.
        /// </summary>
        public int ExpirationYear { get; set; }

        /// <summary>
        /// Credit Card expiration month.
        /// </summary>
        public int ExpirationMonth { get; set; }

        /// <summary>
        /// Name on the Credit Card.
        /// </summary>
        public string NameOnCard { get; set; }

        /// <summary>
        /// CVV security code
        /// </summary>
        public string SecurityCode { get; set; }
    }
}
