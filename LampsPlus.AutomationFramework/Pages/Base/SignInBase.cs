using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class SignInBase : Page, ISignIn
    {
        /// <inheritdoc />
        protected SignInBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string AccountSignInId { get; } = "accountSignIn";
        private string AccountSignInSmallId { get; } = "accountSignInSmall";
        private string SignInBtnClass { get; } = "signInBtn";

        public string AccountSignInXpath { get; } = "//*[@id='submitFormBtn']";
        public string ContinueShoppingId { get; } = "continueShopping";
        public string MessageClass { get; } = "message";
        public string ConnectUsingFbId { get; } = "accountFacebookConnectBtn";
        public string CreateAccountLinkText { get; } = "CREATE ACCOUNT";
        public string SubmitFormBtn { get; } = "submitFormBtn";

        public abstract string EmailId { get; }
        public abstract string PasswordId { get; }
        public abstract string ProfessionalPageSignUpClass { get; }
        public abstract string IconFacebookWhiteClass { get; }
        public abstract string SubtextClass { get; }
        #endregion

        #region Page Elements
        public IElement EmailField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "UserName", SignInContainer);
        public IElement SignInButton => Browser.Locate.ElementByXpath(AccountSignInXpath);
        public IElement SignInContainer => Browser.Locate.ElementById(AccountSignInId);
        public IElement HeaderMenuSignInButton => Browser.Locate.ElementByClassName(SignInBtnClass);
        public IElement CreateAccountButton => Browser.Locate.ElementBySelector($"{AccountSignInSmallId.ToCssIdSelector()} {HtmlTextWriterTag.Ul.ToDirectChildSelector()} {HtmlTextWriterTag.Li.ToDirectChildSelector().ToNthChildSelector(1)}");

        public abstract IElement ConnectUsingFb { get; }
        public abstract IElement CreateAccountLink { get; }
        public abstract IElement ConnectUsingFbButton { get; }
        public abstract IElement ContinueShoppingButton { get; }
        public abstract IElement MessageElement { get; }
        public abstract IElement PasswordField { get; }
        public abstract IElement SignInEmailField { get; }
        public abstract IElement SignInPasswordField { get; }
        #endregion
    }
}