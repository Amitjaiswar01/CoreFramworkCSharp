using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ManageAccountWorkflow
{
    public interface IManageAccountWorkflowDesktop
    {
        void ChangeAccountPassword(string userName, string originalPassword, string newPassword);
        void DeleteAllSavedAddresses();
        void DeleteAllSavedPaymentOptions();
        void AddNewDefaultPaymentMethod(CreditCard creditCard);
        void FillOutShippingAddressForm(IAddress address);
    }
}