using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Sort
{
    public interface ISortMobile : ISortDesktop
    {
        bool AreSortPageContainersVisible();
        bool DoesStickyHeaderDisplayOnPdp();
        bool IsSfpPageLoaded();
        bool IsFilterButtonPresent { get; }
        bool DoesNumberOfResultsDisplay { get; }
        void SelectFilter();
        void CloseMobileSortMenu();
        void AccessPdpThroughPlaProductName();
        void AccessPdpThroughMoreDetails();
        void CloseFilterMenu();
        IElement GetProductName();
        IElement GetProductContainerBySku(string sku);
        IElement GetContextualSearchBarForSort();
        string GetNumberOfResults();
        string GetSortTitleText();
    }
}
