namespace LampsPlus.AutomationFramework.Utilities.Payment
{
    /// <summary>
    /// Create new Promo code objects here.
    /// </summary>
    public class PromoCodeList
    {
        /// <summary>
        /// SilicusTest promo code.
        /// </summary>
        public static PromoCode SilicusTest => new PromoCode { Name = "SilicusTest", DiscountPercentage = 5 };

        /// <summary>
        /// AutoPromoCodeTest promo code.
        /// </summary>
        public static PromoCode AutoPromoCodeTest => new PromoCode { Name = "AutoPromoCodeTest", DiscountPercentage = 1 };
    }
}
