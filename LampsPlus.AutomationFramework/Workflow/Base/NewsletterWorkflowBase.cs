using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior for Email Subscriptions workflow.
    /// </summary>
    public abstract class NewsletterWorkflowBase : WorkflowBase, INewsletterWorkflow
    {
        /// <inheritdoc />
        protected NewsletterWorkflowBase(TestsBase testsBase) : base(testsBase){ }

        /// <inheritdoc />
        public void AddNewAccountInformation(Account account)
        {
            TestsBase.Newsletter.EmailAddressField.SendKeys(account.EmailAddress);
            TestsBase.Newsletter.ConfirmEmailAddressField.SendKeys(account.EmailAddress);
            TestsBase.Newsletter.FirstNameField.SendKeys(account.FirstName);
            TestsBase.Newsletter.LastNameField.SendKeys(account.LastName);
            TestsBase.Newsletter.ZipCodeField.SendKeys(account.ZipCode);
            TestsBase.Newsletter.SubscribeBtn.Click();
        }
    }
}
