using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual
{
    public interface IEmailMobileVisual: IEmailMobile
    {
        IElement IgnoreEmailUtagElement();
        List<IElement> IgnoreSubscribeAndUnsubscribeElements();
    }
}
