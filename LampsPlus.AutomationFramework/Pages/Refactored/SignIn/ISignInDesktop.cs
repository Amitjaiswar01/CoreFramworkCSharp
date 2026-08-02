using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn
{
    public interface ISignInDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        void SignInWithPrefilledEmail(string password);
        bool SignIn(string userName, string password);
        bool IsRememberMeCheckboxVisible { get; }
        IElement GetSignInMessage();
        string GetSignInMessageText();
        string GetEmailFieldValue();
        void SignInFromHeader(LampsPlusAccount loginAccount);
        bool IsMyAccountLinkVisible();
        void SignInFromShippingHeader(LampsPlusAccount loginAccount);
        void OpenSignInModal();
        void NavigateToProSignInPage();
    }
}
