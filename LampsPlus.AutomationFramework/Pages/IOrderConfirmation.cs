using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/order-confirmation/
    /// </summary>
    public interface IOrderConfirmation
    {
        #region Class Setup
        string SaveAccountConfirmationClass { get; }
        string CalloutBtnClass { get; }
        string ConfirmationReviewModalClass { get; }
        string CloseSaveYourAccountSuccessModalSelector { get; }
        string CreateAccountBtnXpath { get; }
        string CreateAccountSuccessEmailElementSelector { get; }
        string LincOptinWidgetClass { get; }
        string LpContainerId { get; }
        string LpModalContentId { get; }
        string LpMobileDrawerContainerClass { get; }
        string OcPageHeadingClass { get; }
        string OkButtonClass { get; }
        string CreateAccountButtonClass { get; }
        string OrderConfirmationHeaderContainerClass { get; }
        string OrderConfirmationIconsPrintId { get; }
        string OrderConfirmationPrintClass { get; }
        string OrderConfirmationHeadingClass { get; }
        string OrderIdHeadingXpath { get; }
        string ShipmentInfoClass { get; }
        string SaveAccountClass { get; }
        string ShipmentContainerClass { get; }
        string SaveYourAccountForm { get; }
        string SaveAccountFormClass { get; }
        string SaveYourAccountSuccessModal { get; }   
        string SecurityQuestionDrawerId { get; }
        string EmailUTagClass { get; }
        string OrderConfirmationOrderIdClass { get; }
        string OrderConfirmationCreateAccountEmailXpath { get; }
        string OrderConfirmationEnterPwdClass { get; }
        string OrderConfirmationReturnButtonClass { get; }
        #endregion

        #region Page Elements
        IElement CloseWinDialogElement { get; }
        IElement ContinueShoppingButton { get; }
        IElement CreateAccountConfirmationElement { get; }
        IElement CreateAccountModalButtonElement { get; }
        IElement CreateAccountOrderConfirmationBtnElement { get; }
        IElement ShipmentContainer { get; }
        IElement CreateAccountModalElement { get; }
        IElement CreateAccountPasswordElement { get; }
        IElement CreateAccountSecurityAnswerElement { get; }
        IElement CreateAccountSecurityQuestionElement { get; }
        IElement CreateAccountSuccessElement { get; }
        IElement CreateAccountSuccessEmailElement { get; }
        IElement CreateAccountButton { get; }
        IElement EmailUTagElement { get; }
        IElement GoogleSurveyModalIframe { get; }
        IElement GoogleSurveyModalNoButton { get; }
        IElement HoldReasonsElement { get; }
        IElement LincOptInWidget { get; }
        IElement LpModalContent { get; }
        IElement MobileDrawerContainer { get; }
        IElement MobileSecurityQuestion { get; }
        IElement MobileSecurityQuestionDrawer { get; }
        IElement OcPageHeadingElement { get; }
        IElement OCPromotionValue { get; }
        IElement OrderConfirmationContainer { get; }
        IElement OrderConfirmationPrintElement { get; }
        IElement OrderConfirmationReviewModal { get; }        
        IElement OrderIdHeading { get; }
        IElement OrderIdLabel { get; }
        IElement OrderIdNumberElement { get; }
        IElement OrderItemShipmentLabel(int index);
        IElement OrderSummaryBlockLabel(string heading);
        IElement OrderSummaryContainer { get; }
        IElement ProductNameElement { get; }
        IElement ProductSkuLabelOrder { get; }
        IElement SpecificErrorElement { get; }
        IElement BillingAddressElement { get; }
        IElement ShippingAddressElement { get; }
        IElement OrderSummaryElement { get; }
        IElement ProductTotalValue { get; }
        IElement ShippingAndProcessingValue { get; }
        IElement TaxValue { get; }
        IElement OrderTotalValue { get; }
        IElement ShippingAndProcessingTotal { get; }
        IElement PromotionAndDiscountsTotal { get; }
        IElement OrderTotal { get; }
        IElement TaxTotal { get; }
        IElement OrderIdElement { get; }
        IElement OrderIdHeadingElement (int index);
        IElement OrderConfirmationCreateAccount { get; }
        IElement OrderConfirmationCreateAccountEmail { get; }
        IElement OrderConfirmationEnterPwd(int index);
        IElement OrderConfirmationReturnButton { get; }
        ReadOnlyCollection<IElement> OrderDetailsItemShipmentElements { get; }

        string AdditionalDiscountsLabel { get; }
        string CouponAndMemberSpecialPriceSavingsLabel { get; }
        string ShippingAndProcessingLabel { get; }
        string StorePickupLabel { get; }
        string StorePickupAvailableNowLabel { get; }
        string SpecificErrorClass { get; }
        #endregion

        /// <summary>
        /// Is the Trust Pilot modal immediately visible?
        /// </summary>
        bool IsTrustPilotModelElementVisible { get; }

        /// <summary>
        /// Get the Order Id text.
        /// </summary>
        string GetOrderId { get; }

        /// <summary>
        /// Get the Order Id.
        /// </summary>
        string GetOrderIdNumber { get; }

        /// <summary>
        /// Get the Hold Reason for the given order.
        /// </summary>
        string HoldReasonsMessages { get; }

        /// <summary>
        /// Get the Product name Text.
        /// </summary>
        string ProductNameText { get; }

        /// <summary>
        /// Get the shortsku text.
        /// </summary>
        string ProductSkuOrder { get; }

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Fill details in Create Account modal on OC page.
        /// </summary>
        void FillInCreateAccountFormOc();

        /// <summary>
        /// Get the promo code label.
        /// </summary>
        /// <returns></returns>
        string GetPromoCodeLabel();

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Select a given question. Note the questions should match those on the website.
        /// </summary>
        /// <param name="question">Question to select from a dropdown.</param>
        void SelectQuestion(string question);

        IElement OrderSummaryRow(int index);
    }
}
