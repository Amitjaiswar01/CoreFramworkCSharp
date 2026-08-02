using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/account/create/
    /// </summary>
    public class CreateAccount : CreateAccountBase
    {
        /// <inheritdoc />
        public CreateAccount(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement CreateAccountBtn => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Button, Browser.Locate.ElementByClassName(CreateAccountButtonWrapperClass));
        public override IElement ProCreateAccountTitle => Browser.Locate.ElementByXpath("//*[@id='accountCreateAccount']/header/h1");

        public override IElement TogglePasswordVisibility => throw new NotImplementedException();
        #endregion
    }
}
