using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual
{
    public class ManageAccountDesktopVisual : ManageAccountDesktop, IManageAccountDesktopVisual
    {
        public ManageAccountDesktopVisual(IBrowser browser, AccountActions accountActions, IAssert assert, IModalDesktop modal, IAddress address) : base(browser, accountActions, assert, modal, address) { }

        public IElement IgnoreRecentlyViewedWidgetContainer()
        {
            return RecentlyViewedWidgetContainer;
        }

        public IElement IgnoreRadioButtons()
        {
            return RadioButtonSection;
        }
    }
}
