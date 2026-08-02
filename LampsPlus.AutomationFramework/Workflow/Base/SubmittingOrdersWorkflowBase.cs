using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Pages.Desktop;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Workflow to provide common actions for submitting orders.
    /// </summary>
    public abstract class SubmittingOrdersWorkflowBase : WorkflowBase, ISubmittingOrdersWorkflow
    {
        protected SubmittingOrdersWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public void EmployeePlacesOrderForSearchedSkuWithPoPayment(string poNumber = "123", string searchedSku = "")
        {
            if (string.IsNullOrWhiteSpace(searchedSku)) { searchedSku = TestsBase.ProductActions.GetLessThanTenDollarItem; }

            TestsBase.ShoppingCartWorkflow.AddItemToCartBySearchedSkuAndCheckOut(searchedSku);

            TestsBase.CustomerAddressInformation.EnterShippingAddress(new Address { State = StateCodeListUnitedStates.CA });

            TestsBase.CustomerAddressInformation.ProceedToPaymentButton.Click();

            Browser.Wait.ForDisplayedElement(TestsBase.Payment.PurchaseOrderRadioButton);

            TestsBase.Payment.PurchaseOrderRadioButton.Click();
            TestsBase.Payment.PurchaseOrderNumberField.SendKeys(poNumber);
            TestsBase.Payment.PlaceOrderButton.Click();
        }

        internal TestsBase Framework;

        /// <inheritdoc />
        public string EmployeePlacesOrderForCurrentCartWithPoPayment(string poNumber = "123")
        {
            // prepare inputs to Shipping Address form
            var shippingAddress = new Address()
            {
                ZipCode = ZipCodeList.Chatsworth,
                State = StateCodeListUnitedStates.CA
            };

            // fill out shipping address form
            TestsBase.CustomerAddressInformation.EnterShippingAddress(shippingAddress);

            TestsBase.CustomerAddressInformation.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            TestsBase.Payment.PurchaseOrderRadioButton.Click();
            TestsBase.Payment.PurchaseOrderNumberField.Clear();
            TestsBase.Payment.PurchaseOrderNumberField.SendKeys(poNumber);

            TestsBase.Payment.PlaceOrderButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            var orderId = TestsBase.OrderConfirmation.GetOrderId;

            return orderId;
        }

        /// <inheritdoc />
        public string EmployeePlacesOrderForCurrentCartWithPurchaseOrderPaymentMethod(string poNumber = "123")
        {
            // prepare inputs to Shipping Address form
            var shippingAddress = new Address()
            {
                ZipCode = ZipCodeList.Chatsworth,
                State = StateCodeListUnitedStates.CA
            };

            // fill out shipping address form
            TestsBase.CustomerAddressInformation.EnterShippingAddress(shippingAddress);

            TestsBase.CustomerAddressInformation.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            TestsBase.Payment.PurchaseOrderRadioButton.Click();
            TestsBase.Payment.PurchaseOrderNumberField.Clear();
            TestsBase.Payment.PurchaseOrderNumberField.SendKeys(poNumber);

            TestsBase.Payment.PlaceOrderButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            var orderId = TestsBase.OrderConfirmation.GetOrderIdNumber;

            return orderId;
        }

        /// <inheritdoc />
        public abstract void FillCcInfo();
    }
}
