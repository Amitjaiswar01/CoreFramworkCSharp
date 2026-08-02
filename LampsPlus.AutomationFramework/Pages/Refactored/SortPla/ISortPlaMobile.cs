namespace LampsPlus.AutomationFramework.Pages.Refactored.SortPla
{
    public interface ISortPlaMobile : ISortPlaDesktop
    {
        bool IsNotifyButtonVisible();
        bool IsStickyHeaderVisible { get; }
        void NavigateToPdpThroughPlaProductName();
    }
}