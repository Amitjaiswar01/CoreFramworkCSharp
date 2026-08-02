using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using System;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/account/create/
    /// </summary>
    public class MobileCreateAccount : CreateAccountBase
    {
        /// <inheritdoc />
        public MobileCreateAccount(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings

        private string TogglePasswordVisibilityClass { get; } = "togglePasswordVisibility";
         #endregion

        #region Page Elements
        public override IElement CreateAccountBtn => Browser.Locate.ElementById(CreateAccountBtnId);

        public override IElement TogglePasswordVisibility => Browser.Locate.ElementBySelector(TogglePasswordVisibilityClass.ToCssClassSelector());

        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement ProCreateAccountTitle => throw new NotImplementedException();
        #endregion
    }
}
