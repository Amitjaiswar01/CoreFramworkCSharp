using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual
{
    public class ManageAccountMobileVisual: ManageAccountMobile, IManageAccountMobileVisual
    {
        public ManageAccountMobileVisual(IBrowser browser, AccountActions accountActions, IAssert assert, IModalDesktop modal, IAddress address) : base(browser, accountActions, assert, modal, address) { }

        public IElement IgnoreFirstNameElement()
        {
            return FirstNameField;
        }

        public IElement IgnoreLastNameElement()
        {
            return LastNameField;
        }

        public IElement IgnoreAddress2Element()
        {
            return Address2Field;
        }

        public IElement IgnoreSaveEmailPrefButton()
        {
            return SaveEmailPreferencesButton;
        }
    }
}
