using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual
{
    public interface IEmailDesktopVisual : IEmailDesktop
    {
        IElement IgnoreEmailAddressField();
        IElement IgnoreEmailUtagElement();
        List<IElement> IgnoreSubscribeAndUnsubscribeElements();
    }
}
