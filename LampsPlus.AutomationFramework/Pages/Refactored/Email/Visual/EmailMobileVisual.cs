using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual
{
    public class EmailMobileVisual : EmailMobile, IEmailMobileVisual
    {
        public EmailMobileVisual(IBrowser browser, IAssert assert) : base(browser, assert)
        {
        }

        public IElement IgnoreEmailUtagElement()
        {
            return EmailUtagElement;
        }

        public List<IElement> IgnoreSubscribeAndUnsubscribeElements()
        {
            return new List<IElement> {AccountTitle, UnsubscribeLpRatioBtn, SubscribeLpRadioBtn, SubscribeObRadioBtn, UnsubscribeObRadioBtn};
        }
    }
}
