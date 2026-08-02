using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow
{
    public interface IWishListWorkflowMobile
    {
        void AddSingleItemToWishList();
        List<string> AddMultipleAvailableItemsToWishList(string url, int numberOfProducts);
    }
}