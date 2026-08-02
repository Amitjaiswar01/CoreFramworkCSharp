using System;
using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow
{
    public interface IWishListWorkflowDesktop
    {
        void AddSingleItemToWishList();
        void AddToWishlistAndVerifyCount();
        List<String> AddMultipleItemsToWishList(string url, int numberOfProducts);
    }
}