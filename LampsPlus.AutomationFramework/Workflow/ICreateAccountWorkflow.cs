using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.External.Nada;

namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior for account creation.
    /// </summary>
    public interface ICreateAccountWorkflow
    {
        /// <summary>
        /// Fill in the account creation form with the given account details.
        /// </summary>
        /// <param name="account"></param>
        void AddCreateAccountInformationFromModal(Account account);

        /// <summary>
        /// Clear Create account form controls.
        /// </summary>
        void ClearCreateAccountFormControls();

        /// <summary>
        /// check certain values in an email message to determine if it is an Account Verification Email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="toEmailAddress"></param>
        /// <returns></returns>
        bool IsAccountVerificationEmailReceived(EmailMessageModel email, string toEmailAddress);
    }
}
