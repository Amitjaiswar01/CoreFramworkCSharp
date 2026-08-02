using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SignIn.Visual
{
    public class SignInMobileVisual : SignInMobile, ISignInMobileVisual
    {
        public SignInMobileVisual(IBrowser browser, SessionSettings settings, IAssert assert, IModalDesktop modal) : base(browser, settings, assert, modal)
        {
        }

        public IElement GetUserNameFieldElement()
        {
            return UserNameField;
        }
    }
}
