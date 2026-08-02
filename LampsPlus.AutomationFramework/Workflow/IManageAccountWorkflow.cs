using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior for managing account.
    /// </summary>
    public interface IManageAccountWorkflow
    {
        /// <summary>
        /// Add a new default payment method to the given user account.
        /// </summary>
        void AddNewDefaultPaymentMethod();

        /// <summary>
        /// Add a new payment method to the given user account.
        /// </summary>
        /// <param name="creditCard"></param>
        /// <param name="address"></param>
        void AddNewPaymentMethod(CreditCard creditCard, Address address);

        /// <summary>
        /// Update the shipping modal with the given address information.
        /// </summary>
        /// <param name="shippingAddress"></param>
        /// <param name="isIntAddress"></param>
        void AddShippingAddressFromModal(Address shippingAddress, bool isIntAddress = false);

        /// <summary>
        /// Add new address to the add shipping form.
        /// </summary>
        /// <param name="shippingAddress"></param>
        void AddNewShippingAddressToModal(Address shippingAddress);

        /// <summary>
        /// Delete all saved address for the given user.
        /// </summary>
        void DeleteAllSavedAddresses();

        /// <summary>
        /// Delete all saved payment options from the given user account.
        /// </summary>
        void DeleteAllSavedPaymentOptions();

        /// <summary>
        /// Wait until modal is fully closed after animation is complete.
        /// </summary>
        void WaitForModalToFullyClose();
    }
}