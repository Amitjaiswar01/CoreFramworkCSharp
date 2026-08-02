using System.Collections.ObjectModel;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    public abstract class ProductDetailMcpBase : Page, IProductDetailMcp
    {
        protected ProductDetailMcpBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public string LoadingWrapperId => "loadingWrapper";
        public string PdMoreYouMayLikeId => "pdMoreYouMayLike";

        public string GicleeShadeOptionsThumbClass { get; } = "pdGicleeShadeOptionsThumb";
        public string PopularColorsId { get; } = "popularColors";
        public string TopClass { get; } = "top";

        public abstract string ArtShadeCategoryClass { get; }
        public abstract string ArtShadeLinksId { get; }
        public abstract string CaretClass { get; }
        public abstract string ColorClass { get; }
        public abstract string ColorCustomizerHiddenClass { get; }
        public abstract string ColorCustomizerToggleVisibilityClass { get; }
        public abstract string ContentClass { get; }
        public abstract string CustomerReviewId { get; }
        public abstract string CustomerReviewXpath { get; }
        public abstract string CustomizeColorsId { get; }
        public abstract string MoreOptionsId { get; }
        public abstract string MorePatternsString { get; }
        public abstract string ViewAllShadePatternsClass { get; }
        public abstract string PdpInterestId { get; }
        public abstract string SelectColorsClass { get; }
        public abstract string TrimColorsClass { get; }
        #endregion

        #region Page Elements
        public IElement PdpMoreYouMayLikeElement => Browser.Locate.ElementById(PdMoreYouMayLikeId);

        public abstract IElement AllArtShadesLink { get; }
        public abstract IElement ArtShadeLink { get; }
        public abstract IElement CaretIcon { get; }
        public abstract IElement ColorCustomizerToggleVisibilityLink { get; }
        public abstract IElement CustomerReviewsElement { get; }
        public abstract IElement CustomerReviews { get; }
        public abstract IElement CustomizeColors { get; }
        public abstract IElement CustomizeColorsContent { get; }
        public abstract IElement CustomizeColorsTop { get; }
        public abstract IElement MorePatterns { get; }
        public abstract IElement ViewAllShadePatternsBtn { get; }
        public abstract IElement MorePatternsLink { get; }
        public abstract IElement OtherPatterns { get; }
        public abstract IElement OtherPatternsContent { get; }
        public abstract IElement OtherPatternsTop { get; }
        public abstract IElement PdpInterestElement { get; }
        public abstract IElement PopularColors { get; }
        public abstract IElement PopularColorsContent { get; }
        public abstract IElement PopularColorsTop { get; }
        public abstract IElement SelectColors { get; }
        public abstract IElement TrimColors { get; }

        public abstract ReadOnlyCollection<IElement> ArtShadeLinks { get; }
        public abstract ReadOnlyCollection<IElement> ListOfCustomizePatternColors { get; }
        public abstract ReadOnlyCollection<IElement> ListOfCustomizeSelectColors { get; }
        public abstract ReadOnlyCollection<IElement> ListOfOtherPatterns { get; }
        public abstract ReadOnlyCollection<IElement> ListOfPopularColors { get; }
        public abstract ReadOnlyCollection<IElement> ListOfTrimColors { get; }
        #endregion
    }
}
