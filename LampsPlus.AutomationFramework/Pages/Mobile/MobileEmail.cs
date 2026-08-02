using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Email subscription page.
    /// </summary>
    public class MobileEmail : EmailBase
    {
        /// <inheritdoc />
        public MobileEmail(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        #endregion

        #region Page Elements
        public override IElement NotifyMeMessageElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.P, Browser.Locate.ElementById(NotifyMeMessageId));

        #endregion
    }
}
