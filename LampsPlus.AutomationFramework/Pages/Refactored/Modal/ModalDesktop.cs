using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Modal
{
    public class ModalDesktop : IModalDesktop
    {
        //Class members
        private string _lpModalContentId = "lpModalContent";
        private string _discountToolTipClass = "discountTooltip";
        private string _lpModalId = "lpModal";

        protected IElement LpModalContent => Browser.Locate.ElementById(_lpModalContentId);
        private IElement DiscountToolTipModal => Browser.Locate.ElementByClassName(_discountToolTipClass);
        private IElement LpModal => Browser.Locate.ElementById(LpModalId);      
        private IElement IframeModal => Browser.Locate.ElementById(LpModalIframeId);
        private IElement LpModalCloseElement => Browser.Locate.ElementById(LpModalCloseId);
        protected IElement Iframe => Browser.Locate.ElementBySelector(_lpModalId.ToCssIdSelector());

        //Instances
        protected IBrowser Browser;

        public ModalDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string LpModalCloseId => "lpModalClose";
        public string LpModalId => "lpModal";
        public string LpModalIframeId => "modalIframe";
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public bool IsModalVisible() => Browser.Wait.IsVisibleElement(By.CssSelector(LpModalId.ToCssIdSelector()));
        public bool IsModalNotVisible() => Browser.Wait.IsInvisibleElement(By.CssSelector(LpModalId.ToCssIdSelector()));
     
        public IElement GetLpModal()
        {
            return LpModal;
        }

        public IElement GetIframeModal()
        {
            return IframeModal;
        }

        public IElement GetLpModalContent()
        {
            return LpModalContent;
        }

        public void CloseLpModal()
        {
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementBySelector(LpModalCloseId.ToCssIdSelector()));
            Browser.Locate.ElementBySelector(LpModalCloseId.ToCssIdSelector()).Click();
            Browser.Wait.UntilElementDoesntExist(LpModalId.ToCssIdSelector());
        }

        public void SwitchFocusToModal()
        {
            Browser.Wait.IsVisibleElement(By.Id(LpModalIframeId));

            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(LpModalIframeId.ToCssIdSelector()));
            Browser.Wait.ForDomReady();
        }

        public IElement GetDiscountToolTipModal()
        {
            return DiscountToolTipModal;
        }

        public void PrintModal()
        {
            Browser.Wait.ForElement(LpModalContent, 3);
            Browser.SwitchFocusToIframe(LpModalContent);
        }

        public IElement GetIframe()
        {
            return Iframe;
        }

        public IElement GetLpModalClose()
        {
            return LpModalCloseElement;
        }

        public void WaitForModalContentToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_lpModalContentId.ToCssIdSelector()));
        }

        public bool IsModalWindowInitialized()
        {
            return Browser.Locate.ElementImmediately(LpModalId.ToCssIdSelector()).IsInitialized;
        }
    }
}