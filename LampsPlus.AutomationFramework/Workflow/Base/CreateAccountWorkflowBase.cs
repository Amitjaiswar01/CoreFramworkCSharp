using System;
using System.Threading;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.External.Nada;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior for account creation.
    /// </summary>
    public abstract class CreateAccountWorkflowBase : WorkflowBase, ICreateAccountWorkflow
    {
        protected CreateAccountWorkflowBase(TestsBase testsBase) : base(testsBase){}

        /// <inheritdoc />
        public void ClearCreateAccountFormControls()
        {
            TestsBase.CreateAccount.EmailField.Clear();
            TestsBase.CreateAccount.PasswordField.Clear();
        }

        /// <inheritdoc />
        public void AddCreateAccountInformationFromModal(Account account)
        {
            TestsBase.CreateAccount.EmailField.SendKeys(account.EmailAddress);
            TestsBase.CreateAccount.PasswordField.SendKeys(account.Password);

            TestsBase.CreateAccount.CreateAccountBtn.Click();
            Thread.Sleep(3000);//TODO Temporary fix for real mobile (iPhoneX), to replace with wait commands for subsequent steps
        }

        /// <inheritdoc />
        public bool IsAccountVerificationEmailReceived(EmailMessageModel email, string toEmailAddress)
        {
            if (!email.From.Equals(TestsBase.CreateAccount.CustomerServiceEmail, StringComparison.InvariantCultureIgnoreCase))
                return false;
            if (!email.Subject.Equals(TestsBase.CreateAccount.LampsPlusAccountVerificationSubject, StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }
    }
}
