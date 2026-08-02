namespace LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount
{
    public interface IManageAccountMobile : IManageAccountDesktop
    {
        void ClearSelectedState();
        void CloseEmailPreferencesModal();
        void SelectAddShippingAddress();
        bool IsRewardNumberVisible();
    }
}
