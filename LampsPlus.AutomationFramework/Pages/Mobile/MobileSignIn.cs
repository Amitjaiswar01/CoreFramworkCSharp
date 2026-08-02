using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Class for the Mobile elements on SignIn page and inherited the SignInBase class.
    /// </summary>
    public class MobileSignIn : SignInBase
    {
        /// <inheritdoc />
        public MobileSignIn(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string IconFacebookWhiteClass { get; } = "iconFacebookWhite";
        public override string SubtextClass { get; } = "subtext";
        public override string ProfessionalPageSignUpClass { get; } = "professionalPage__signUp";

        public override string EmailId => throw new System.NotImplementedException();
        public override string PasswordId => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement CreateAccountLink => Browser.Locate.ElementByLinkText(CreateAccountLinkText);
        public override IElement ConnectUsingFb => Browser.Locate.ElementByClassName(IconFacebookWhiteClass);
        public override IElement MessageElement => Browser.Locate.ElementByClassName(SubtextClass);
        public override IElement PasswordField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, "Password", SignInContainer);
        
        public override IElement ContinueShoppingButton => throw new NotImplementedException();
        public override IElement ConnectUsingFbButton => throw new NotImplementedException();
        public override IElement SignInEmailField => throw new NotImplementedException();
        public override IElement SignInPasswordField => throw new NotImplementedException();
        #endregion
    }
}