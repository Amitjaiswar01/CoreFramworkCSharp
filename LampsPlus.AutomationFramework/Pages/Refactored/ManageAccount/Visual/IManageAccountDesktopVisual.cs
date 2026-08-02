using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual
{
    public interface IManageAccountDesktopVisual : IManageAccountDesktop
    {
        IElement IgnoreRecentlyViewedWidgetContainer();
        IElement IgnoreRadioButtons();
    }
}
