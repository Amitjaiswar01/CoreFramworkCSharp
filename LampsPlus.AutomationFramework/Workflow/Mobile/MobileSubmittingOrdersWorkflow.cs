using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Workflow to provide common actions for submitting orders.
    /// </summary>
    public class MobileSubmittingOrdersWorkflow : SubmittingOrdersWorkflowBase
    {
        /// <inheritdoc />
        public MobileSubmittingOrdersWorkflow(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public override void FillCcInfo()
        {
            TestsBase.WaitForGlobalSpinnerToClose();

            Browser.Wait.ForClickableElement(TestsBase.Payment.CreditCardField).Click();

            TestsBase.Payment.CreditCardField.Clear();
            TestsBase.Payment.CreditCardField.SendKeys("4111111111111111");

            TestsBase.Payment.CardCodeField.Clear();
            TestsBase.Payment.CardCodeField.SendKeys("123");

            TestsBase.Payment.SelectMonth("May (05)");

            TestsBase.Payment.SelectYear("2023");

            TestsBase.Payment.NameField.Clear();
            TestsBase.Payment.NameField.SendKeys("LP Automation User");
        }
    }
}
