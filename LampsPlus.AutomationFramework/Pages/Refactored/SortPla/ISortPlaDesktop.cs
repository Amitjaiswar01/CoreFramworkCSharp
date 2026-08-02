namespace LampsPlus.AutomationFramework.Pages.Refactored.SortPla
{
    public interface ISortPlaDesktop : IPageObjectModel
    {
        void RedirectToCustomerReviewsSection();
        void NavigateToPdpThroughMoreDetails();
        void PlaAddToCart();
        void NavigateToPlaWithReviews(string url, string sku);
        bool DoesReviewSummaryContainReviewsText();
        bool DoesPlaRatingStarsDisplay();
        bool IsReviewsSectionDisplayed { get; }
        string GetPlaSkuWithReviews();
    }
}