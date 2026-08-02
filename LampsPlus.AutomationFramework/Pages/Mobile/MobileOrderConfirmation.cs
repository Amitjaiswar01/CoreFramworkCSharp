using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/order-confirmation/
    /// </summary>
    public class MobileOrderConfirmation : OrderConfirmationBase
    {

        public override string GetPromoCodeLabel() => Browser.Locate.ElementByXpath("//*[contains(text(),'Promotions and Discounts: ')]").Text;

        /// <inheritdoc />
        public MobileOrderConfirmation(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }

        #region CSS Selector Strings
        public override string CalloutBtnClass { get; } = "calloutBtn";
        public override string CloseSaveYourAccountSuccessModalSelector { get; } = ".saveAccountConfirmation .lpMobileDrawerContainer > button";
        public override string CreateAccountButtonClass { get; } = "createAccountBtnPopup";
        public override string ContinueShoppingButtonId { get; } = "cartHeader";
        public override string CreateAccountSuccessEmailElementSelector { get; } = ".saveAccountConfirmation .lpMobileDrawerContainer";
        public override string LpMobileDrawerContainerClass { get; } = "lpMobileDrawerContainer";
        public override string OcPageHeadingClass { get; } = "ocPageHeading";
        public override string OCPromotionXpath { get; } = "//*[@id='!orderSummary']/strong[1]/div[2]";
        public override string OrderConfirmationIconsPrintId { get; } = "orderConfirmationIcons__print";
        public override string SaveAccountClass { get; } = "saveAccount";
        public override string ShipmentContainerClass { get; } = "shipmentItemSecondaryBlock";
        public override string SaveYourAccountForm { get; } = "saveYourAccountForm";
        public override string SaveYourAccountSuccessModal { get; } = ".saveAccountConfirmation .lpMobileDrawerContainer";
        public override string SecurityQuestionDrawerId { get; } = "securityQuestionDrawer";
        public override string ShippingAndProcessingXpath { get; } = "//*[@id='!orderSummary']/div[3]/div[2]";
        public override string TaxXpath { get; } = "//*[@id='!orderSummary']/div[4]/div[2]";
        public override string OrderIdHeadingXpath { get; } = "//*[@id='orderConfirmation']/div[1]/div[1]";
        public override string OrderConfirmationHeadingClass { get; } = "orderConfirmation__heading";
        public override string ConfirmationReviewModalClass => throw new NotImplementedException();
        public override string LpContainerId => throw new NotImplementedException();
        public override string OkButtonClass => throw new NotImplementedException();
        public override string OrderConfirmationHeaderContainerClass => throw new NotImplementedException();
        public override string SaveAccountFormClass => throw new NotImplementedException();
        public override string OrderConfirmationCreateAccountEmailXpath => throw new NotImplementedException();
        public override string OrderConfirmationEnterPwdClass => throw new NotImplementedException();
        public override string OrderConfirmationReturnButtonClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement ContinueShoppingButton => Browser.Locate.ElementById(ContinueShoppingButtonId);
        public override IElement CreateAccountOrderConfirmationBtnElement => Browser.Locate.ElementByClassName(CreateAccountButtonClass);
        public override IElement CreateAccountConfirmationElement => Browser.Locate.ElementBySelector(SaveYourAccountSuccessModal);
        public override IElement CreateAccountModalButtonElement => Browser.Locate.ElementByClassName(SaveAccountClass);
        public override IElement ShipmentContainer => Browser.Locate.ElementByClassName(ShipmentContainerClass);
        public override IElement CreateAccountSuccessEmailElement => Browser.Locate.ElementBySelector(CreateAccountSuccessEmailElementSelector);
        public override IElement MobileDrawerContainer => Browser.Locate.ElementByClassName(LpMobileDrawerContainerClass);
        public override IElement MobileSecurityQuestion => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "securityQuestion6");
        public override IElement MobileSecurityQuestionDrawer => Browser.Locate.ElementById(SecurityQuestionDrawerId);
        public override IElement OcPageHeadingElement => Browser.Locate.ElementByClassName(OcPageHeadingClass);
        public override IElement OCPromotionValue => Browser.Locate.ElementByXpath(OCPromotionXpath);
        public override IElement OrderSummaryContainer => Browser.Locate.ElementBySelector(OrderConfirmationId.ToCssIdSelector());
        public override IElement ShippingAndProcessingTotal => Browser.Locate.ElementByXpath(ShippingAndProcessingXpath);
        public override IElement OrderIdElement => Browser.Locate.ElementByXpath(OrderIdHeadingXpath);
        public override IElement TaxTotal => Browser.Locate.ElementByXpath(TaxXpath);
        public override IElement CloseWinDialogElement => throw new NotImplementedException();
        public override IElement CreateAccountModalElement => throw new NotImplementedException();
        public override IElement LpModalContent => Browser.Locate.ElementByClassName(SaveYourAccountForm);
        public override IElement GoogleSurveyModalIframe => throw new NotImplementedException();
        public override IElement GoogleSurveyModalNoButton => throw new NotImplementedException();
        public override IElement HoldReasonsElement => throw new NotImplementedException();
        public override IElement OrderConfirmationContainer => throw new NotImplementedException();
        public override IElement OrderConfirmationPrintElement => Browser.Locate.ElementById(OrderConfirmationIconsPrintId);
        public override IElement OrderConfirmationReviewModal => throw new NotImplementedException();
        public override IElement OrderConfirmationCreateAccount => throw new NotImplementedException();
        public override IElement OrderConfirmationCreateAccountEmail => throw new NotImplementedException();
        public override IElement OrderConfirmationEnterPwd(int index) => throw new NotImplementedException();
        public override IElement OrderConfirmationReturnButton => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> OrderDetailsItemShipmentElements => throw new NotImplementedException();
        #endregion
        
        /// <inheritdoc />
        public override void SelectQuestion(string question) { }

        /// <inheritdoc />
        public override void FillInCreateAccountFormOc()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(PasswordId.ToCssIdSelector()));
            CreateAccountPasswordElement.SendKeys("Password123");
            CreateAccountOrderConfirmationBtnElement.Click();
        }
    }
}
