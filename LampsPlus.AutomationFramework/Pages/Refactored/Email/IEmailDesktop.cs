using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Email
{
    public interface IEmailDesktop : IPageObjectModel
    {
        void GoToEmailPreferencesByEmail(string email);
        void ChangeEmailPreferencesSubscribe();
        void ChangeEmailPreferencesUnsubscribe();
        void SaveSettings();
        void FillOutSubscribeNow(Account account);
        void UpdateEmailPreferences();
        string GetSaveSettingsThankYouMessage();
        string GetContinueShoppingMessage();
        string GetEmailFromHeader();
        string GetEmailFromEmailAddressField();
        string GetThankYouMessageAfterSubscribing();
        bool VerifyUserEmailNotDisplayedInUrl(string pageUrl, string userEmailInPageUrl);
        bool IsEmailPreferencesPage { get; }
        IBrowser Navigate();
    }
}