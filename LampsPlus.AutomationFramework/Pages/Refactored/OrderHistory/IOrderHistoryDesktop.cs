using Automation.Framework;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory
{
    public interface IOrderHistoryDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        void CheckOrderStatus(OrderIdModel order);
        void NavigateToOrderDetailsPage();
        void ClickOnTrackOrder();
        void WaitForMoreYouMayLikeWidget();
        void HandleShippingUpdatesModal();
        string GetOrderTotal { get; }
        bool IsOrderIdVisible(OrderIdModel order);
        bool IsOrderDateVisible { get; }
        bool IsBillingInformationVisible { get; }
        bool IsShippingInformationVisible { get; }
        bool IsRewardNumberVisible { get; }
    }
}