namespace LampsPlus.AutomationFramework.Constants
{
    /// <summary>
    /// Warning messages.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Promo code related messages.
        /// </summary>
        public static class PromoRelatedMessages
        {
            public const string TooltipMsg = "Tooltip doesn't appear on checkout button hover";
            public const string InvalidPromoCodeMessage = "Not a valid code.";
        }

        public static class CartMessages
        {
            public const string EmailSentMessage = "Thank you!" + "\r\n" + "Your shopping cart has been sent to the following addresses:";
            public const string ShippingErrorMessageForMarshallIsland = "Your destination is in a special shipping zone. You will be contacted with a shipping quote after you submit your order.";
            public const string TenPerOrderMsg =
                "Due to minimum processing and handling charges, we have a $10 per order requirement. Please call us if you need help in placing your order or continue shopping!";
            public const string IncorrectSkuMessage = "Please specify a SKU other than 99999.";
        }

        public static class EmailPageMessages
        {
            public const string ThankYouMessageAfterSubscribingDesktop = "Thank you for requesting email updates from LAMPS PLUS!";
            public const string ThankYouMessageAfterSubscribingMobile = "Thank You!";
        }

        public static class ArMessages
        {
            public const string ArPageTitle = "2 Products In This Room";
        }

        public static class PayPalPriceMessage
        {
            public const string PayPalLessThan30Message = "Pay in 4 interest-free payments on purchases of $30-$1,500 with PayPal. Learn more";
            public const string PayPalBetween1500And9000Message = "Pay monthly for purchases of $199-$10,000 with PayPal. Learn more";
        }

        public static class ShippingMessage
        {
            public const string ShippingMessageChangedMessage = "Shipping Methods Have Changed";
        }
    }
}