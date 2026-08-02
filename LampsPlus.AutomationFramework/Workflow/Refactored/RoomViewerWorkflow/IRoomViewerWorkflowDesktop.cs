using System.Collections.Generic;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.RoomViewerWorkflow
{
    public interface IRoomViewerWorkflowDesktop
    {
        void AddMultipleItemsToRoom(List<ArProductModel> ArSku);
        void AddSingleProductToRoom(string ShortSku);
    }
}
