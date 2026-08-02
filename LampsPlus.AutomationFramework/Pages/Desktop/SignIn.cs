using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    public class SignIn : SignInBase
    {
        /// <inheritdoc />
        public SignIn(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string EmailId { get; } = "UserNameModal";
        public override string PasswordId { get; } = "PasswordModal";

        public override string ProfessionalPageSignUpClass { get; } = "existingMember";

        public override string IconFacebookWhiteClass => throw new System.NotImplementedException();
        public override string SubtextClass => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        //Elements that exist in Desktop view only.
        public override IElement ContinueShoppingButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, ContinueShoppingId);

        public override IElement SignInEmailField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, EmailId);
        public override IElement SignInPasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, PasswordId);

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement ConnectUsingFbButton => Browser.Locate.ElementById(ConnectUsingFbId);
        public override IElement MessageElement => Browser.Locate.ElementByClassName(MessageClass);
        public override IElement PasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "txtPassword", SignInContainer);

        //Elements that exist in Mobile view and NOT Desktop view.
        public override IElement CreateAccountLink => Browser.Locate.ElementByLinkText(CreateAccountLinkText);

        public override IElement ConnectUsingFb => throw new System.NotImplementedException();
        #endregion
    }
}