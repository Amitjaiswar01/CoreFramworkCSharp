using Automation.Framework;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class EmailBase : Page, IEmail
    {
        /// <inheritdoc />
        protected EmailBase(IBrowser browser) : base(browser) { }
            
        #region CSS Selector Strings

        public string NotifyMeMessageId { get; } = "notifyMeSuccess";

        #endregion

        #region Page Elements
        public abstract IElement NotifyMeMessageElement { get; }

        #endregion
    }
}
