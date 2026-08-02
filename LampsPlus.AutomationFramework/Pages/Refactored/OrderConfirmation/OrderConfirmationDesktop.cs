using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation
{
    public class OrderConfirmationDesktop : IOrderConfirmationDesktop
    {
        //Class members
        private string _saveNewAccountClass  = "saveNewAccount";
        private string _createAccountBtnXpath  = "//a[contains(@class,'createAccountBtn')]";
        private string _saveAccountConfirmationClass = "saveAccountConfirmation";
        private string _orderConfirmationCreateAccountEmailXpath  = "//*[@id=\"lpModalContent\"]//p[2]";
        private string _orderConfirmationReturnButtonClass = "okButton";
        private string _orderConfirmationHeaderContainerClass  = "orderConfirmationHeaderContainer";
        private string _orderIdHeadingClass = "orderIdHeading";
        private string _lpContainerId  = "lpContainer";
        private string _shipmentInfoClass = "shipmentInfo";
        private string _paymentAddressClass = "paymentAddress";
        private string _shipmentItemNameClass = "shipmentItem__name";

        protected string OrderConfirmationOrderIdClass => "orderConfirmation__orderId";
        protected string EmailUTagClass => "emailUtag";
        protected string PasswordId => "password";
        protected string OrderConfirmationId  => "orderConfirmation";
        protected string LincOptinWidgetClass => "linc-optin-widget";

        private IElement OrderConfirmationReturnButton => Browser.Locate.ElementByClassName(_orderConfirmationReturnButtonClass);
        private IElement ShippingAddressElement => Browser.Locate.ElementByClassName(_shipmentInfoClass);
        private IElement BillingAddressElement => Browser.Locate.ElementByClassName(_paymentAddressClass);
        private IElement ProductName => Browser.Locate.ElementByClassName(_shipmentItemNameClass);
        protected IElement OrderIdHeading => Browser.Locate.ElementByClassName(_orderIdHeadingClass);
        protected IElement EmailId => Browser.Locate.ElementByClassName(EmailUTagClass);
        protected IElement OrderId => Browser.Locate.ElementByClassName(OrderConfirmationOrderIdClass);
        protected IElement EmailUTagElement => Browser.Locate.ElementBySelector(EmailUTagClass.ToCssClassSelector());
        protected IElement CreateAccountPasswordElement => Browser.Locate.ElementBySelector(PasswordId.ToCssIdSelector());
        protected IElement CreateAccountModalButtonElement => Browser.Locate.ElementByClassName(_saveNewAccountClass);
        protected IElement CreateAccountEmail => Browser.Locate.ElementByXpath(_orderConfirmationCreateAccountEmailXpath);
        protected IElement OrderConfirmationContainer => Browser.Locate.ElementByClassName(_orderConfirmationHeaderContainerClass);
        protected IElement LincOptionwdget => Browser.Locate.ElementByClassName(LincOptinWidgetClass);

        protected virtual IElement CreateAccountSuccessEmailElement => Browser.Locate.ElementByClassName(_saveAccountConfirmationClass);
        protected virtual IElement OrderSummaryContainer => Browser.Locate.ElementById(_lpContainerId);
        protected virtual IElement OrderIdElement => Browser.Locate.ElementByClassName(OrderConfirmationOrderIdClass);

        //Instances
        protected IBrowser Browser;
        protected OperatingSystem OperatingSystem;

        public OrderConfirmationDesktop(IBrowser browser, OperatingSystem operatingSystem)
        {
            Browser = browser;
            OperatingSystem = operatingSystem;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public string GetOrderIdNumber => OrderIdElement.Text.ToLower().Replace("order id:", string.Empty).ToUpper().Trim().Split('|')[0].Trim();
        public string GetEmail => EmailUTagElement.Text;

        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(EmailUTagClass.ToCssClassSelector()));

        public virtual void FillInCreateAccountForm(string password)
        {
            if (OperatingSystem == OperatingSystem.iPad)
            {
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(Browser.Locate.ElementByXpath(_createAccountBtnXpath), ref xElementCoordinate, ref yElementCoordinate, 100);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(PasswordId.ToCssIdSelector()));
            CreateAccountPasswordElement.SendKeys(password);
            CreateAccountModalButtonElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_saveAccountConfirmationClass.ToCssClassSelector()));
        }

        public virtual void CloseCreateAccountModal()
        {
            Browser.Wait.ForDisplayedElement(OrderConfirmationReturnButton);
            OrderConfirmationReturnButton.Click();
        }

        public virtual void IsLincOptionWidgetVisible()
        {
            Browser.Wait.ForDisplayedElement(LincOptionwdget);
        }

        public IElement GetOcPageShippingAddress()
        {
            return ShippingAddressElement;
        }

        public IElement GetOcPageBillingAddress()
        {
            return BillingAddressElement;
        }

        public string GetOcPageProductName()
        {
            return ProductName.Text;
        }
    }
}