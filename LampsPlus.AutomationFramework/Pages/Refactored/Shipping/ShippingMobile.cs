using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Shipping
{
    public class ShippingMobile : ShippingDesktop, IShippingMobile
    {
        //Class Members
        private string _fieldCheckboxClass = "fieldCheckbox";
        private string _fedExAddressValidationClass = "fedExAddressValidation";
        private string _cartMenuClass = "cartMenu";
        private string _requiredNoteClass = "requiredNote";
        private string _addNewAddrClass = "addNewAddr";
        private string _shippingTypeShippingCostClass = "shippingType__shippingCost";

        private IElement OrderSummaryClass => Browser.Locate.ElementByClassName(_cartMenuClass);
        private IElement RequiredNoteText => Browser.Locate.ElementByClassName(_requiredNoteClass);
        private IElement AddNewAddressButton => Browser.Locate.ElementBySelector(_addNewAddrClass.ToCssClassSelector());
        protected override IElement SaveAddressCheckboxInput => Browser.Locate.ElementByClassName(_fieldCheckboxClass).FindElement(By.TagName(HtmlTextWriterTag.Input.ToString()));
        protected IElement GetMobileShippingOptionsModal => Browser.Locate.ElementByXpath("//*[@id='changeShippingOptionsOverlay']//div[contains(@class, 'available-shipping-options__days')]");

        //Instances
        public ShippingMobile(IBrowser browser, IModalDesktop modal) : base(browser, modal) {}

        //Interface implementation
        public override void WaitForModalToFullyClose()
        {
            Browser.Wait.IsInvisibleElement(By.ClassName(_fedExAddressValidationClass));
        }

        public void OpenOrderSummaryBlock()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_cartMenuClass));
            OrderSummaryClass.Click();
        }

        public void SelectRequiredNoteText()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_requiredNoteClass));
            RequiredNoteText.Click();
        }

        public void OpenAddNewAddressModal()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_addNewAddrClass.ToCssClassSelector()));
            AddNewAddressButton.Click();
        }

        public string GetShippingValue()
        {
            return Browser.Locate.ElementByClassName(_shippingTypeShippingCostClass).Text;
        }
    }
}
