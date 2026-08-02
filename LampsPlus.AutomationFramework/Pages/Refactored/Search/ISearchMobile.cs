namespace LampsPlus.AutomationFramework.Pages.Refactored.Search
{
    public interface ISearchMobile : ISearchDesktop
    {
        bool IsSearchBoxVisible { get; }
        bool IsSearchVisibleOnLandingPage();
        void SearchForRandomTerm(string searchValue);
        void SearchForRandomProduct(string searchValue);
    }
}
