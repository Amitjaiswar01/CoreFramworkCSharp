using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer
{
    public class MobileDrawer :  IMobileDrawer
    {
        //Class members
        private string _removeItemClass = "removeItem";
        private string _calloutBtnListClass = "calloutBtnList";
        private string _confirmDrawerActionClass = "confirmDrawerAction";
        private string _lpmmMenuContainerClass  = "lpmmMenuContainer";
        private string _lpmmMenuClass = "lpmmMenu";
        private IElement DisplayedMobileDrawerMenu => Browser.Locate.ElementByClassName(_lpmmMenuClass);
        private IElement MobileDrawerMenuInnerContainer => Browser.Locate.ElementByClassName(_lpmmMenuContainerClass, DisplayedMobileDrawerMenu);

        //Instances
        protected IBrowser Browser;

        public MobileDrawer(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public void ConfirmDrawer()
        {
            var confirmButton = Browser.Locate.ElementBySelector($"{_calloutBtnListClass.ToCssClassSelector()} {_removeItemClass.ToCssClassSelector()}, {_confirmDrawerActionClass.ToCssClassSelector()}");
            Browser.Wait.ForDisplayedElement(confirmButton);
            Browser.Wait.ForElementToStopAnimating(confirmButton);
            confirmButton.Click();
            Browser.Wait.UntilElementUnloads(confirmButton);
        }

        public void WaitForMobileDrawerToLoad()
        {
            Browser.Wait.ForElementToStopAnimating(MobileDrawerMenuInnerContainer);
        } 
    }
}