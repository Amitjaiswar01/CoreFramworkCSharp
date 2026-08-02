using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.External.Nada;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CreateAccount
{
    public interface ICreateAccountDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        void ClearEmailAndPasswordFields();
        void AddEmailAndPasswordToForm(Account account);
        void OpenFacebookLoginPage();
        bool IsProfessionalCreateAccountPageLoaded();
        bool IsAccountVerificationEmailReceived(EmailMessageModel email, string toEmailAddress);
        string FacebookLoginUrl { get; }
    }
}