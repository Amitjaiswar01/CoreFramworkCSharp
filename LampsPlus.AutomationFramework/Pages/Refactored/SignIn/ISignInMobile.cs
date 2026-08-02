using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn
{
    public interface ISignInMobile : ISignInDesktop
    {
        bool CheckSignOutIcon();
    }
}
