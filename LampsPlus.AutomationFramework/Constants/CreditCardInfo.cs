using System;

namespace LampsPlus.AutomationFramework.Constants
{
    /// <summary>
    /// Credit card information.
    /// </summary>
    [Serializable]
    public class CreditCardInfo
    {
        public string CreditCardNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardCvv { get; set; }
    }
}
