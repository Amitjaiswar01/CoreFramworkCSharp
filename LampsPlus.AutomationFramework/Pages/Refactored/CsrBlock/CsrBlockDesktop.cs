using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;


namespace LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock
{
    public class CsrBlockDesktop : ICsrBlockDesktop
    {
        //Class Members
        private string _csrPanelId = "csrPanel";
        private string _saleSourceId = "saleSource";
        private string _manualDiscountManagerApprovalClass = "manualDiscountManagerApproval";
        private string _sAndPId  = "sAndP";
        private string _applySAndPId = "applySAndP";
        private string _applyMdPercentId = "applyMdPercent";
        private string _mdPercentId = "mdPercent";
        private string _reasonCodeId = "reasonCode";
        private string _shippingAndProcessingXpath = "//*[@id=\"orderSummary\"]//span[contains(text(), \"10\")]";

        private IElement ManualDiscountPercentTextBox => Browser.Locate.ElementById(_mdPercentId);
        private IElement ReasonCodeDropdown => Browser.Locate.ElementById(_reasonCodeId);
        private IElement ApplyMdPercentButton => Browser.Locate.ElementById(_applyMdPercentId);
        private IElement SAndPField => Browser.Locate.ElementById(_sAndPId);
        private IElement ApplySAndPButton => Browser.Locate.ElementById(_applySAndPId);
        private IElement SaleSourceField => Browser.Locate.ElementById(_saleSourceId);
        private IElement CsrPanel => Browser.Locate.ElementBySelector(_csrPanelId.ToCssIdSelector());

        //Instances
        protected IBrowser Browser;

        public CsrBlockDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
        public bool IsSaleSourceFieldDisplayed => SaleSourceField.IsInitialized && SaleSourceField.Displayed;
        public bool IsCsrPanelVisible => CsrPanel.Displayed;
        public bool IsManualDiscountManagerApprovalFormDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_manualDiscountManagerApprovalClass));

        public void SetSaleSourceValue()
        {
            Browser.Wait.IsVisibleElement(By.Id(_csrPanelId));
            Browser.Locate.ClickDropdownByValue(SaleSourceField, "1");
        }

        public void SetReasonCodeValue()
        {
            Browser.Wait.IsVisibleElement(By.Id(_reasonCodeId));
            Browser.Locate.ClickDropdownByValue(ReasonCodeDropdown, "1");
        }

        public void ApplyShippingAndProcessingCost(string value) // Apply the manual shipping cost for an item or items by employee
        {
            Browser.Wait.IsVisibleElement(By.Id(_sAndPId));
            SAndPField.Clear();
            SAndPField.SendKeys(value);
            Browser.Wait.ForDisplayedElement(ApplySAndPButton).Click();
            Browser.Wait.IsVisibleElement(By.XPath(_shippingAndProcessingXpath));
        }

        public IElement GetMdPercentageButton()
        {
           return  ApplyMdPercentButton;
        }

        public void ApplyCartLevelDiscount(decimal percentDiscount)
        {
            ManualDiscountPercentTextBox.Clear();
            ManualDiscountPercentTextBox.SendKeys(percentDiscount.ToString());

            Browser.Locate.ClickDropdownByValue(ReasonCodeDropdown, "1");
            ApplyMdPercentButton.Click();

            Browser.Wait.ForDomReady();
        }
    }
}
