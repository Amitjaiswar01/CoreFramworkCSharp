using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation
{
    public class OrderConfirmationMobile : OrderConfirmationDesktop, IOrderConfirmationMobile
    {
        //Class members
        private string _createAccountButtonClass  = "createAccountBtnPopup";
        private string _closeSaveYourAccountSuccessModalSelector  = ".saveAccountConfirmation .lpMobileDrawerContainer > button";
        private string _createAccountSuccessEmailElementSelector = ".saveAccountConfirmation .lpMobileDrawerContainer";
        private string _OrderIdXpath = "//*[@id='orderConfirmation']/div[1]/div[1]";

        private IElement CreateAccountOrderConfirmationBtnElement => Browser.Locate.ElementByClassName(_createAccountButtonClass);
        protected override IElement CreateAccountSuccessEmailElement => Browser.Locate.ElementBySelector(_createAccountSuccessEmailElementSelector);
        protected override IElement OrderSummaryContainer => Browser.Locate.ElementBySelector(OrderConfirmationId.ToCssIdSelector());
        protected override IElement OrderIdElement => Browser.Locate.ElementByXpath(_OrderIdXpath);

        //Instances
        public OrderConfirmationMobile(IBrowser browser, OperatingSystem operatingSystem) : base(browser, operatingSystem) { }

        //Interface implementation
        public void WaitForOrderConfirmationPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmationOrderIdClass));
        }

        public override void FillInCreateAccountForm(string password)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_createAccountButtonClass));
            Browser.Wait.IsVisibleElement(By.CssSelector(PasswordId.ToCssIdSelector()));
            CreateAccountPasswordElement.SendKeys("Password123");
            CreateAccountOrderConfirmationBtnElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_closeSaveYourAccountSuccessModalSelector));
        }

        public override void CloseCreateAccountModal()
        {
            Browser.Locate.ElementBySelector(_closeSaveYourAccountSuccessModalSelector).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(LincOptinWidgetClass));
        }
    }
}
