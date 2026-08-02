using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn.Visual
{
    public interface ISignInDesktopVisual : ISignInDesktop
    {
        IElement GetUserNameFieldElement();
    }
}
