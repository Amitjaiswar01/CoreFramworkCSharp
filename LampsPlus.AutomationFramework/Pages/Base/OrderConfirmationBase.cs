using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class OrderConfirmationBase : Page, IOrderConfirmation
    {
        /// <inheritdoc />
        protected OrderConfirmationBase(IBrowser browser, TestsBase testsBase) : base(browser)
        {
            Framework = testsBase;
        }

        internal TestsBase Framework;

        public string AdditionalDiscountsLabel { get; } = "Additional Discounts:";
        public string CouponAndMemberSpecialPriceSavingsLabel { get; } = "Promotions and Discounts:";
        public string ShippingAndProcessingLabel { get; } = "Shipping & Processing:";
        public string StorePickupAvailableNowLabel { get; } = "Store Pickup - Available Now";
        public string StorePickupLabel { get; } = "Store Pickup:";
        public string EmailUTagClass { get; } = "emailUtag";
        public string SpecificErrorClass { get; } = "specificError";
        #region CSS Selector Strings
        private string BillingAddressClass { get; } = "paymentAddress";
        private string GroupClass { get; } = "group";
        private string OrderSummaryId { get; } = "!orderSummary";
        private string OrderTotalClass { get; } = "orderTotal";
        public string PromotionAndDiscountsClass { get; } = "promoCodeLine";
        private string ShipmentItemNameClass { get; } = "shipmentItem__name";
        private string SecurityAnswerId { get; } = "securityAnswer";
        private string SessionCamHideTextClass { get; } = "sessioncamhidetext";
        private string SecurityQuestionId { get; } = "securityQuestion";
        private string ShippingAddressClass { get; } = "shipmentInfo";
        private string ShipmentItemStyleClass { get; } = "shipmentItem__style";
        private string TrustPilotSurveyId { get; } = "trustPilotSurvey";
        public string BrdialogCloseClass { get; } = "brdialog-close";
        public string CreateAccountBtnXpath { get; } = "//a[contains(@class,'createAccountBtn')]";
        public string HoldReasonsClass { get; } = "holdReasons";
        public string LincOptinWidgetClass { get; } = "linc-optin-widget";
        public string LpModalContentId { get; } = "lpModalContent";
        public string OrderConfirmationId { get; } = "orderConfirmation";
        public string OrderConfirmationPrintClass { get; } = "orderConfirmationPrint";
        public string PasswordId { get; } = "password";
        public string SaveAccountConfirmationClass { get; } = "saveAccountConfirmation";
        public string ShipmentInfoClass { get; } = "shipmentInfo"; 
        public string OrderConfirmationOrderIdClass { get; } = "orderConfirmation__orderId";

        public abstract string ConfirmationReviewModalClass { get; }
        public abstract string LpContainerId { get; }
        public abstract string OkButtonClass { get; }
        public abstract string OrderConfirmationHeaderContainerClass { get; }
        public abstract string SaveAccountFormClass { get; }
        public abstract string CalloutBtnClass { get; }
        public abstract string ContinueShoppingButtonId { get; }
        public abstract string LpMobileDrawerContainerClass { get; }
        public abstract string OcPageHeadingClass { get; }
        public abstract string OCPromotionXpath { get; }
        public abstract string OrderConfirmationIconsPrintId { get; }
        public abstract string SaveAccountClass { get; }
        public abstract string ShipmentContainerClass { get; }
        public abstract string SecurityQuestionDrawerId { get; }
        public abstract string SaveYourAccountForm { get; }
        public abstract string SaveYourAccountSuccessModal { get; }
        public abstract string ShippingAndProcessingXpath { get; }
        public abstract string CloseSaveYourAccountSuccessModalSelector { get; }
        public abstract string CreateAccountSuccessEmailElementSelector { get; }
        public abstract string TaxXpath { get; }
        public abstract string OrderConfirmationCreateAccountEmailXpath { get; }
        public abstract string OrderConfirmationEnterPwdClass { get; }
        public abstract string OrderConfirmationReturnButtonClass { get; }
        public abstract string CreateAccountButtonClass { get; }
        public abstract string OrderConfirmationHeadingClass { get; }
        public abstract string OrderIdHeadingXpath { get; }
        #endregion

        #region Page Elements
        public IElement CreateAccountButton => Browser.Locate.ElementByXpath(CreateAccountBtnXpath);
        public IElement CreateAccountPasswordElement => Browser.Locate.ElementById(PasswordId);
        public IElement CreateAccountSecurityAnswerElement => Browser.Locate.ElementById(SecurityAnswerId);
        public IElement CreateAccountSecurityQuestionElement => Browser.Locate.ElementById(SecurityQuestionId);
        public IElement CreateAccountSuccessElement => Browser.Locate.ElementByClassName(SaveAccountConfirmationClass);
        public IElement EmailUTagElement => Browser.Locate.ElementBySelector(EmailUTagClass.ToCssClassSelector());
        public IElement LincOptInWidget => Browser.Locate.ElementBySelector(LincOptinWidgetClass.ToCssClassSelector());
        public IElement OrderIdHeading => Browser.Locate.ElementByClassName(OrderConfirmationHeadingClass);
        public IElement OrderIdLabel => Browser.Locate.ElementById(OrderConfirmationId);
        public IElement OrderIdHeadingElement(int index) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Strong)[index];
        public IElement OrderIdNumberElement => Browser.Locate.ElementByClassName(SessionCamHideTextClass, OrderIdHeading);
        public IElement OrderItemShipmentLabel(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, OrderDetailsItemShipmentElements[index]);
        public IElement OrderSummaryBlockLabel(string heading) => OrderSummaryTotalLabels().FirstOrDefault(e => e.Text.StartsWith(heading, StringComparison.OrdinalIgnoreCase));
        public IElement ProductNameElement => Browser.Locate.ElementByClassName(ShipmentItemNameClass);
        public IElement ProductSkuLabelOrder => Browser.Locate.ElementByClassName(ShipmentItemStyleClass);
        public IElement SpecificErrorElement => Browser.Locate.ElementByClassName(SpecificErrorClass);
        public IElement BillingAddressElement => Browser.Locate.ElementByClassName(BillingAddressClass);
        public IElement ShippingAddressElement => Browser.Locate.ElementByClassName(ShippingAddressClass);
        public IElement OrderSummaryElement => Browser.Locate.ElementById(OrderSummaryId);
        public IElement OrderSummaryRow(int index) => Browser.Locate.ElementsByClassName(GroupClass, OrderSummaryElement)[index];
        public IElement ProductTotalValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(0));
        public IElement ShippingAndProcessingValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(1));
        public IElement TaxValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderSummaryRow(2));
        public IElement OrderTotalValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, OrderTotal);
        public IElement PromotionAndDiscountValue => Browser.Locate.ElementByClassName(Framework.GlobalLocators.OsValueClass, PromotionAndDiscountsTotal);
        public IElement OrderTotal => Browser.Locate.ElementByClassName(OrderTotalClass);
        public IElement PromotionAndDiscountsTotal => Browser.Locate.ElementByClassName(PromotionAndDiscountsClass);
        public ReadOnlyCollection<IElement> OrderSummaryTotalLabels() => Browser.Locate.ElementsByClassName(Framework.GlobalLocators.OsLabelClass);

        public abstract IElement CloseWinDialogElement { get; }
        public abstract IElement CreateAccountOrderConfirmationBtnElement { get; }
        public abstract IElement ContinueShoppingButton { get; }
        public abstract IElement CreateAccountConfirmationElement { get; }
        public abstract IElement CreateAccountModalButtonElement { get; }
        public abstract IElement ShipmentContainer { get; }
        public abstract IElement CreateAccountModalElement { get; }
        public abstract IElement CreateAccountSuccessEmailElement { get; }
        public abstract IElement GoogleSurveyModalIframe { get; }
        public abstract IElement GoogleSurveyModalNoButton { get; }
        public abstract IElement HoldReasonsElement { get; }
        public abstract IElement LpModalContent { get; }
        public abstract IElement MobileDrawerContainer { get; }
        public abstract IElement MobileSecurityQuestion { get; }
        public abstract IElement MobileSecurityQuestionDrawer { get; }
        public abstract IElement OcPageHeadingElement { get; }
        public abstract IElement OCPromotionValue { get; }
        public abstract IElement OrderConfirmationContainer { get; }
        public abstract IElement OrderConfirmationPrintElement { get; }
        public abstract IElement OrderConfirmationReviewModal { get; }
        public abstract IElement OrderSummaryContainer { get; }
        public abstract IElement ShippingAndProcessingTotal { get; }
        public abstract IElement TaxTotal { get; }
        public abstract IElement OrderConfirmationCreateAccount { get; }
        public abstract IElement OrderConfirmationCreateAccountEmail { get; }
        public abstract IElement OrderConfirmationEnterPwd(int index);
        public abstract IElement OrderConfirmationReturnButton { get; }
        public abstract IElement OrderIdElement { get; }
        public abstract ReadOnlyCollection<IElement> OrderDetailsItemShipmentElements { get; }
        #endregion  
        
        /// <summary>
        /// Fill details in Create Account modal on OC page.
        /// </summary>
        public abstract void FillInCreateAccountFormOc();

        /// <summary>
        /// Get the Order Id text.
        /// </summary>
        public string GetOrderId => OrderIdElement.Text.ToLower().Replace("order id:", string.Empty).ToUpper().Trim();

        /// <summary>
        /// Get the Order Id.
        /// </summary>
        public string GetOrderIdNumber => OrderIdElement.Text.ToLower().Replace("order", "").Replace("#", "").ToUpper().Trim();

        /// <summary>
        /// Get the promo code label.
        /// </summary>
        /// <returns></returns>
        public virtual string GetPromoCodeLabel() => OrderSummaryBlockLabel(CouponAndMemberSpecialPriceSavingsLabel).Text.Trim();

        /// <summary>
        /// Get the Hold Reason for the given order.
        /// </summary>
        public string HoldReasonsMessages => HoldReasonsElement.Text;

        /// <summary>
        /// Is the Trust Pilot modal immediately visible?
        /// </summary>
        public bool IsTrustPilotModelElementVisible => Browser.Locate.ElementImmediately($"{LpModalContentId.ToCssIdSelector()} {TrustPilotSurveyId.ToCssIdSelector()}").IsInitialized;

        /// <summary>
        /// Get the Product name Text.
        /// </summary>
        public string ProductNameText => ProductNameElement.Text;

        /// <summary>
        /// Get the shortsku text.
        /// </summary>
        public string ProductSkuOrder => ProductSkuLabelOrder.Text.Split(SingleSpaceChart)[2];

        /// <summary>
        /// Select a given question. Note the questions should match those on the website.
        /// </summary>
        /// <param name="question">Question to select from a dropdown.</param>
        public abstract void SelectQuestion(string question);
    }
}
