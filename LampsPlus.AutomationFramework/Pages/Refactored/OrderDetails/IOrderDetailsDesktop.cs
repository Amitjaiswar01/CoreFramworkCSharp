using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails
{
    public interface IOrderDetailsDesktop : IPageObjectModel
    {
        string GetOrderTotal { get; }
        string GetDbProductName(OrderHistoryItems orderDetailItem);
        string FormatPrice(decimal price);
        string GetProductName(string shortSku);
        string GetUnitPrice(string shortSku);
        string GetProductQuantity(string shortSku);
        string GetProductStatus(string shortSku);
        string GetStatusString(OrderHistoryItems orderDetailItem);
        void NavigateToRequestReturnModal();
    }
}