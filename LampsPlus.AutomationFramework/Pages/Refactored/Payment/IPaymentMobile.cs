using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Payment
{
    public interface IPaymentMobile : IPaymentDesktop
    {
        IElement GetEditPaymentDetails();
        void PlaceInternationalOrder();
        void OpenOrderSummaryDropdown();
        void CloseOrderSummaryDropdown();
    }
}
