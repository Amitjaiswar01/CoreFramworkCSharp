using System;
using System.Threading;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Workflow to provide common actions for submitting orders.
    /// </summary>
    public class SubmittingOrdersWorkflow : SubmittingOrdersWorkflowBase
    {
        public SubmittingOrdersWorkflow(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public override void FillCcInfo()
        {
            TestsBase.WaitForGlobalSpinnerToClose();
            TestsBase.Payment.CreditCartRadio.Click();

            Browser.Wait.ForClickableElement(TestsBase.Payment.CreditCardField).Click();

            TestsBase.Payment.CreditCardField.Clear();
            TestsBase.Payment.CreditCardField.SendKeys("4111111111111111");

            TestsBase.Payment.CardCodeField.Clear();
            TestsBase.Payment.CardCodeField.SendKeys("123");

            TestsBase.Payment.SelectMonth("05 - May");

            TestsBase.Payment.SelectYear("2023");

            TestsBase.Payment.NameField.Clear();
            TestsBase.Payment.NameField.SendKeys("LP Automation User");
        }
    }
}
