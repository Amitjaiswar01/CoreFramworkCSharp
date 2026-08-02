using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Verifies;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual
{
    public class EmailDesktopVisual : EmailDesktop, IEmailDesktopVisual
    {
        public EmailDesktopVisual(IBrowser browser, IAssert assert) : base(browser, assert)
        {
        }

        public IElement IgnoreEmailAddressField()
        {
            return EmailAddressField;
        }

        public List<IElement> IgnoreSubscribeAndUnsubscribeElements()
        {
            return new List<IElement> {AccountTitle, UnsubscribeLpRatioBtn, SubscribeLpRadioBtn, SubscribeObRadioBtn, UnsubscribeObRadioBtn};
        }

        public IElement IgnoreEmailUtagElement()
        {
            return EmailUtagElement;
        }
    }
}
