using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium.Support.UI;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/order-confirmation/
    /// </summary>
    public class OrderConfirmation : OrderConfirmationBase
    {
        /// <inheritdoc />
        public OrderConfirmation(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }

        #region CSS Selector Strings
        public override string ConfirmationReviewModalClass { get; } = "confirmationReviewModal";
        public override string LpContainerId { get; } = "lpContainer";
        public override string OkButtonClass { get; } = "okButton";
        public override string OrderConfirmationHeaderContainerClass { get; } = "orderConfirmationHeaderContainer";
        public override string SaveAccountFormClass { get; } = "saveAccountForm";
        public override string ShippingAndProcessingXpath { get; } = "//*[@id='!orderSummary']/div[4]/div[2]";
        public override string TaxXpath { get; } = "//*[@id='!orderSummary']/div[5]/div[2]";
        public override string CalloutBtnClass => throw new NotImplementedException();
        public override string ContinueShoppingButtonId => throw new NotImplementedException();
        public override string CloseSaveYourAccountSuccessModalSelector => throw new NotImplementedException();
        public override string CreateAccountSuccessEmailElementSelector => throw new NotImplementedException();
        public override string LpMobileDrawerContainerClass => throw new NotImplementedException();
        public override string OcPageHeadingClass => throw new NotImplementedException();
        public override string OCPromotionXpath => throw new NotImplementedException();
        public override string OrderConfirmationIconsPrintId => throw new NotImplementedException();
        public override string SaveAccountClass => throw new NotImplementedException();
        public override string ShipmentContainerClass => throw new NotImplementedException();
        public override string SaveYourAccountForm => throw new NotImplementedException();
        public override string SaveYourAccountSuccessModal => throw new NotImplementedException();
        public override string SecurityQuestionDrawerId => throw new NotImplementedException();
        public override string OrderIdHeadingXpath => throw new NotImplementedException();
        public override string OrderConfirmationCreateAccountEmailXpath { get; } = "//*[@id=\"lpModalContent\"]//span";
        public override string OrderConfirmationEnterPwdClass { get; } = "restrictedCharacters";
        public override string OrderConfirmationReturnButtonClass { get; } = "okButton";
        public override string CreateAccountButtonClass { get; } = "saveNewAccount";
        public override string OrderConfirmationHeadingClass { get; } = "orderConfirmation__orderId";
        #endregion

        #region Page Elements
        public override IElement CloseWinDialogElement => Browser.Locate.ElementByClassName(BrdialogCloseClass);
        public override IElement CreateAccountConfirmationElement => Browser.Locate.ElementByClassName(OkButtonClass, LpModalContent);
        public override IElement CreateAccountModalButtonElement => Browser.Locate.ElementByClassName(CreateAccountButtonClass);
        public override IElement CreateAccountModalElement => Browser.Locate.ElementByClassName(SaveAccountFormClass);
        public override IElement CreateAccountSuccessEmailElement => Browser.Locate.ElementByClassName(SaveAccountConfirmationClass);
        public override IElement GoogleSurveyModalIframe => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Iframe, HtmlTextWriterAttribute.Src, Urls.GoogleUrl);
        public override IElement GoogleSurveyModalNoButton => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Div, HtmlTextWriterAttribute.Class, "VfPpkd");
        public override IElement HoldReasonsElement => Browser.Locate.ElementByClassName(HoldReasonsClass);
        public override IElement LpModalContent => Browser.Locate.ElementBySelector(LpModalContentId.ToCssIdSelector());
        public override IElement OrderConfirmationContainer => Browser.Locate.ElementByClassName(OrderConfirmationHeaderContainerClass);
        public override IElement OrderConfirmationReviewModal => Browser.Locate.ElementByClassName(ConfirmationReviewModalClass);
        public override IElement OrderConfirmationPrintElement => Browser.Locate.ElementByClassName(OrderConfirmationPrintClass);
        public override IElement OrderSummaryContainer => Browser.Locate.ElementById(LpContainerId);
        public override IElement ShippingAndProcessingTotal => Browser.Locate.ElementByXpath(ShippingAndProcessingXpath);
        public override IElement TaxTotal => Browser.Locate.ElementByXpath(TaxXpath);

        public override IElement ContinueShoppingButton => throw new NotImplementedException();
        public override IElement MobileDrawerContainer => throw new NotImplementedException();
        public override IElement MobileSecurityQuestionDrawer => throw new NotImplementedException();
        public override IElement MobileSecurityQuestion => throw new NotImplementedException();
        public override IElement OcPageHeadingElement => throw new NotImplementedException();
        public override IElement OCPromotionValue => throw new NotImplementedException();
        public override IElement CreateAccountOrderConfirmationBtnElement => throw new NotImplementedException();
        public override IElement ShipmentContainer => throw new NotImplementedException();
        public override IElement OrderConfirmationCreateAccount => Browser.Locate.ElementByXpath("//button[contains(@class, 'saveNewAccount')]");
        public override IElement OrderConfirmationCreateAccountEmail => Browser.Locate.ElementByXpath(OrderConfirmationCreateAccountEmailXpath);
        public override IElement OrderConfirmationEnterPwd(int index) => Browser.Locate.ElementsByClassName(OrderConfirmationEnterPwdClass)[index];
        public override IElement OrderConfirmationReturnButton => Browser.Locate.ElementByClassName(OrderConfirmationReturnButtonClass);
        public override IElement OrderIdElement => Browser.Locate.ElementByClassName(OrderConfirmationOrderIdClass);
        #endregion

        public override ReadOnlyCollection<IElement> OrderDetailsItemShipmentElements => Browser.Locate.ElementsByClassName(ShipmentInfoClass);

        /// <summary>
        /// Select a given question. Note the questions should match those on the website.
        /// </summary>
        /// <param name="question">Question to select from a dropdown.</param>
        public override void SelectQuestion(string question) { new SelectElement(CreateAccountSecurityQuestionElement.InternalElement).SelectByText(question); }

        /// <inheritdoc />
        public override void FillInCreateAccountFormOc()
        {
            CreateAccountPasswordElement.SendKeys("Password123");
            CreateAccountModalButtonElement.Click();
        }
    }
}
