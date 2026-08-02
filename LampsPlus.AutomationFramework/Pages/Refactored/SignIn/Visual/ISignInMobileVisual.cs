using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn.Visual
{
    public interface ISignInMobileVisual : ISignInMobile
    {
        IElement GetUserNameFieldElement();
    }
}