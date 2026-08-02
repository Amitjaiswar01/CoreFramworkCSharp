using System;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Common utility methods for Mobile tests.
    /// </summary>
    public class MobileCommonWorkflow : CommonWorkflowBase
    {
        public MobileCommonWorkflow(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public override void ConfirmDrawer()
        {
            var confirmButton = Browser.Locate.ElementBySelector(GlobalLocators.ConfirmDrawerActionClass.ToCssClassSelector());
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.ConfirmDrawerActionClass.ToCssClassSelector()));
            Browser.Wait.ForElementToStopAnimating(confirmButton);
            confirmButton.Click();
            Browser.Wait.UntilElementUnloads(confirmButton);
        }

        /// <inheritdoc />
        public override void ConfirmRemoveItemDrawer()
        {
            var confirmButton = GlobalLocators.RemoveCartItemButton(1);
            Browser.Wait.ForDisplayedElement(GlobalLocators.RemoveCartItemButton(1));
            Browser.Wait.ForElementToStopAnimating(confirmButton);
            confirmButton.Click();
            Browser.Wait.UntilElementUnloads(confirmButton);
        }

        /// <inheritdoc />
        public override void CancelDrawer()
        {
            var cancelButton = Browser.Locate.ElementByClassName(GlobalLocators.HideMobileDrawerClass);
            Browser.Wait.ForDisplayedElement(cancelButton).Click();
            Browser.Wait.UntilElementUnloads(cancelButton);
        }

        /// <inheritdoc />
        public override void WaitForDrawerToStopAnimating()
        {
            // Inner container is the animating element
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.MobileDrawerMenuInnerContainer);
        }

        /// <inheritdoc />
        public override void CloseLpModal() => throw new NotImplementedException();
    }
}
