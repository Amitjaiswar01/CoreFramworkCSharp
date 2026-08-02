using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email
{
    public interface IEmailMobile : IEmailDesktop
    {
        void SelectPreferenceTab();
        void UpdatePreference();
    }
}