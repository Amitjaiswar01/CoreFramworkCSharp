using OpenQA.Selenium.Support.UI;
using Automation.Framework;
using Automation.Framework.Core;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class CsrBlockBase : Page, ICsrBlock
    {
        /// <inheritdoc />
        protected CsrBlockBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string AddProfessionalAccountClass { get;  } = "addProfessionalAccount";
        private string ApplyMdPercentId { get; } = "applyMdPercent";
        private string ApplySAndPId { get; } = "applySAndP";
        private string CsrPanelId { get; } = "csrPanel";
        private string MdPercentId { get; } = "mdPercent";
        private string ReasonCodeId { get; } = "reasonCode";
        private string RemoveProfessionalAccountId { get; } = "removeProfessionalAccount";
        private string SaleSourceId { get; } = "saleSource";
        private string SAndPId { get; } = "sAndP";
        private string SecondaryEmployeeNumberId { get; } = "secondaryEmployeeNumber";
        public string SaleSourceXpath { get; } = "//*[@id='saleSource']";

        #endregion

        #region Page Elements
        public IElement AddProfessionalAccountLink => Browser.Locate.ElementByClassName(AddProfessionalAccountClass);
        public IElement ApplyMdPercentButton => Browser.Locate.ElementById(ApplyMdPercentId);
        public IElement ApplySAndPButton => Browser.Locate.ElementById(ApplySAndPId);
        public IElement CsrPanelElement => Browser.Locate.ElementById(CsrPanelId);
	    public IElement ManualDiscountPercentTextBox => Browser.Locate.ElementById(MdPercentId);
		public IElement ReasonCodeDropdown => Browser.Locate.ElementById(ReasonCodeId);
        public IElement RemoveProfessionalAccountElement => Browser.Locate.ElementById(RemoveProfessionalAccountId);
		public IElement SaleSourceField => Browser.Locate.ElementByXpath("//*[@id='saleSource']");
		public IElement SAndPField => Browser.Locate.ElementById(SAndPId);
		public IElement SecondaryEmployeeField => Browser.Locate.ElementById(SecondaryEmployeeNumberId);
		#endregion
        
	    /// <summary>
	    /// Select an option in the Sale Source dropdown.
	    /// Use the available options from the SaleSource class.
	    /// </summary>
	    /// <param name="saleSource">Available Sale Source options.</param>
	    public void SelectSaleSource(string saleSource) { new SelectElement(Browser.Locate.ElementById(SaleSourceId).InternalElement).SelectByText(saleSource); }

	    /// <summary>
	    /// Select an option in the Reason dropdown.
	    /// Use the available options from the ReasonCode class.
	    /// </summary>
	    /// <param name="reasonCode">Available Reason Code options.</param>
	    public void SelectReasonCode(string reasonCode) { new SelectElement(Browser.Locate.ElementById(ReasonCodeId).InternalElement).SelectByText(reasonCode); }

        /// <summary>
        /// Enters value in SP $ field and clicks Apply
        /// </summary>
        /// <param name="value"></param>
        public void EnterAndApplySAndP(string value)
        {
            SAndPField.Clear();
            SAndPField.SendKeys(value);
            Browser.Wait.ForDisplayedElement(ApplySAndPButton).Click();
            Browser.Wait.ForDomReady(1000);
        }

        /// <summary>
        /// Enters value in MD % field and clicks Apply
        /// </summary>
        /// <param name="value"></param>
        public void EnterAndApplyManualDiscount(string value)
        {
            ManualDiscountPercentTextBox.Clear();
            ManualDiscountPercentTextBox.SendKeys(value);
            ManualDiscountPercentTextBox.Click();
            Browser.Wait.ForClickableElement(ApplyMdPercentButton).Click();
        }
    }
}
