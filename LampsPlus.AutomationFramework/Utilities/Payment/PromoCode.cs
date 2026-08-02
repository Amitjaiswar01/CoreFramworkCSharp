namespace LampsPlus.AutomationFramework.Utilities.Payment
{
    /// <summary>
    /// Representation of a Lamps Plus Promo Code.
    /// </summary>
    public class PromoCode
    {
        /// <summary>
        /// Promo code name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Promo code discount percentage.
        /// </summary>
        public int DiscountPercentage { get; set; }
    }
}
