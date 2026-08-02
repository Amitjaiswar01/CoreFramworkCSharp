using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    public interface IProductDetailMcp
    {
        #region Class Setup
        string ArtShadeCategoryClass { get; }
        string ArtShadeLinksId { get; }
        string CaretClass { get; }
        string ColorClass { get; }
        string ColorCustomizerHiddenClass { get; }
        string ColorCustomizerToggleVisibilityClass { get; }
        string ContentClass { get; }
        string CustomerReviewId { get; }
        string CustomerReviewXpath { get; }
        string CustomizeColorsId { get; }
        string LoadingWrapperId { get; }
        string MoreOptionsId { get; }
        string MorePatternsString { get; }
        string ViewAllShadePatternsClass { get; }
        string PdpInterestId { get; }
        string PdMoreYouMayLikeId { get; }
        string SelectColorsClass { get; }
        string TrimColorsClass { get; }
        #endregion

        #region Page Elements
        IElement AllArtShadesLink { get; }
        IElement ArtShadeLink { get; }
        IElement CaretIcon { get; }
        IElement ColorCustomizerToggleVisibilityLink { get; }
        IElement CustomerReviewsElement { get; }
        IElement CustomerReviews { get; }
        IElement CustomizeColors { get; }
        IElement CustomizeColorsContent { get; }
        IElement CustomizeColorsTop { get; }
        IElement MorePatterns { get; }
        IElement ViewAllShadePatternsBtn { get; }
        IElement MorePatternsLink { get; }
        IElement OtherPatterns { get; }
        IElement OtherPatternsContent { get; }
        IElement OtherPatternsTop { get; }
        IElement PdpInterestElement { get; }
        IElement PdpMoreYouMayLikeElement { get; }
        IElement PopularColors { get; }
        IElement PopularColorsContent { get; }
        IElement PopularColorsTop { get; }
        IElement TrimColors { get; }
        IElement SelectColors { get; }

        ReadOnlyCollection<IElement> ArtShadeLinks { get; }
        ReadOnlyCollection<IElement> ListOfCustomizePatternColors { get; }
        ReadOnlyCollection<IElement> ListOfCustomizeSelectColors { get; }
        ReadOnlyCollection<IElement> ListOfOtherPatterns { get; }
        ReadOnlyCollection<IElement> ListOfPopularColors { get; }
        ReadOnlyCollection<IElement> ListOfTrimColors { get; }
        #endregion
    }
}
