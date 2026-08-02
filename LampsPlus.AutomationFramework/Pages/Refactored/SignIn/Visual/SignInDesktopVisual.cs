using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn.Visual
{
    public class SignInDesktopVisual : SignInDesktop, ISignInDesktopVisual
    {
        public SignInDesktopVisual(IBrowser browser, SessionSettings settings, IAssert assert, IModalDesktop modal) : base(browser, settings, assert, modal)
        {
        }

        public virtual IElement GetUserNameFieldElement()
        {
            return UserNameField;
        }
    }
}
