using System;
using System.Threading;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping
{
    public class ShippingDesktop : IShippingDesktop
    {
        //Class members
        private string _proceedPaymentId = "proceedPayment";
        private string _noChangeAddressRadioId = "noChangeAddressRadio";
        private string _submitChangesClass = "submitChanges";
        private string _editShippingAddressSaveBtn = "//*[@id='modalEditShipping']/form/button";
        private string _fedExAddressValidationClass = "fedExAddressValidation";
        private string _shipToDifferentAddressClass = "shipToDifferentAddr";
        private string _selectShippingAddressClass = "fieldRadio";
        private string _applyClass = "updateAddr";
        private string _orderSummaryProductsSkuClass = "orderSummaryProducts__sku";
        private string _promoCodeLineClass= "promoCodeLine";
        private string _jsShipMultipleLinkClass = "jsShipMultipleLink";
        private string _isMultipleShippingClass  = "isMultipleShipping";
        private string _jsNewAddress  = "jsNewAddress";
        private string _shippingOptionsChangedContainerClass = "shippingOptionsChangedContainer";
        private string _shippingCellShippingCostClass = "shippingCell__shippingCost";
        private string _singleShippingCityId = "singleShippingCity";

        private IElement ShipToMultipleAddressesButton => Browser.Locate.ElementByClassName(_jsShipMultipleLinkClass);
        private IElement PromotionDiscount => Browser.Locate.ElementByClassName(_promoCodeLineClass);
        private IElement ShippingOptionsChangedContainer => Browser.Locate.ElementByClassName(_shippingOptionsChangedContainerClass);
        private IElement ShippingCostFromModifyShippingBlock => Browser.Locate.ElementByClassName(_shippingCellShippingCostClass);
        protected IElement ProceedToPaymentButton => Browser.Locate.ElementById(_proceedPaymentId);
        protected IElement NoChangeAddressRadioElement => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.For, _noChangeAddressRadioId);
        protected IElement SubmitChangesElement => Browser.Locate.ElementByClassName(_submitChangesClass);
        protected virtual IElement SaveAddressCheckboxInput => Browser.Locate.ElementByXpath(_editShippingAddressSaveBtn);
        protected IElement ShipToDifferentAddressButton => Browser.Locate.ElementByClassName(_shipToDifferentAddressClass);
        protected IElement SelectShippingAddressRadioButton(int index) => Browser.Locate.ElementsByClassName(_selectShippingAddressClass)[index];
        protected IElement ApplyButton => Browser.Locate.ElementByClassName(_applyClass);
        protected virtual IElement ProductSku => Browser.Locate.ElementBySelector(_orderSummaryProductsSkuClass.ToCssClassSelector());

        private void SelectShippingAddress(int index)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_selectShippingAddressClass.ToCssClassSelector()));
            SelectShippingAddressRadioButton(index).Click();
        }

        //Instances
        protected IBrowser Browser;
        protected IModalDesktop Modal;

        public ShippingDesktop(IBrowser browser, IModalDesktop modal)
        {
            Browser = browser;
            Modal = modal;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/cart/shipping/";
        public string TaxLabel => "Tax ¹:";
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_proceedPaymentId.ToCssIdSelector()));

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public void ShipToMultipleAddresses()
        {
            Browser.Wait.ForClickableElement(ShipToMultipleAddressesButton).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_isMultipleShippingClass.ToCssClassSelector()));
        }

        public void OpenNewAddressByIndex(int index)
        {
            var addressButton = Browser.Locate.ElementsByClassName(_jsNewAddress)[index];
            Browser.Wait.ForClickableElement(addressButton).Click();
        }

        public void WaitForShippingPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_proceedPaymentId.ToCssIdSelector()));
        }

        public void ProceedToPayment()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_proceedPaymentId.ToCssIdSelector()));
            Browser.ScrollIntoView(ProceedToPaymentButton,true);
            Browser.ClickByJs(ProceedToPaymentButton);
        }

        public void HandleFedExModalIfPresent()
        {
            if (DoesFedExModalShow())
            {
                NoChangeAddressRadioElement.Click();
                SubmitChangesElement.Click();
                SaveAddressCheckboxInput.Click();
                WaitForModalToFullyClose();
            }
        }

        public bool DoesFedExModalShow()
        {
            return SpinWait.SpinUntil(() => Browser.Locate.DoesElementExistImmediately(_fedExAddressValidationClass.ToCssClassSelector()), TimeSpan.FromSeconds(15));
        }

        public virtual void WaitForModalToFullyClose()
        {
            Browser.Wait.UntilElementDoesntExist(Modal.LpModalId);
        }
        public void ShipToDifferentAddress()
        {
            ShipToDifferentAddressButton.Click();
        }

        public void SelectNotDefaultShippingAddress(int index)
        {
            SelectShippingAddress(index);
        }

        public string GetShortSkuOnShipping()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_orderSummaryProductsSkuClass.ToCssClassSelector()));
            return ProductSku.Text.Replace("Style # ", String.Empty).TrimEnd();
        }

        public IElement GetPromoCodeElementOnOrderSummary()
        {
            return PromotionDiscount;
        }

        public IElement GetShippingOptionsChangedContainer()
        {
            return ShippingOptionsChangedContainer;
        }

        public string GetShippingCostFromModifyShippingBlock()
        {
            return ShippingCostFromModifyShippingBlock.Text;
        }

        public void WaitForShippingMethodsChangedContainer()
        {
            Browser.Locate.ElementById(_singleShippingCityId).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_shippingOptionsChangedContainerClass));
            Browser.ScrollToElement(Browser.Locate.ElementByClassName(_shippingOptionsChangedContainerClass));
        }
    }
}