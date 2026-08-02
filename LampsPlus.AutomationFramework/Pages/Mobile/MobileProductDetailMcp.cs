using System;
using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    public class MobileProductDetailMcp : ProductDetailMcpBase
    {
        public MobileProductDetailMcp(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string ArtShadeCategoryClass => throw new NotImplementedException();
        public override string ArtShadeLinksId => throw new NotImplementedException();
        public override string CaretClass => throw new NotImplementedException();
        public override string ColorClass => throw new NotImplementedException();
        public override string ColorCustomizerHiddenClass => throw new NotImplementedException();
        public override string ColorCustomizerToggleVisibilityClass => throw new NotImplementedException();
        public override string ContentClass => throw new NotImplementedException();
        public override string CustomerReviewId => throw new NotImplementedException();
        public override string CustomerReviewXpath => throw new NotImplementedException();
        public override string CustomizeColorsId => throw new NotImplementedException();
        public override string MoreOptionsId => throw new NotImplementedException();
        public override string MorePatternsString => throw new NotImplementedException();
        public override string ViewAllShadePatternsClass => throw new NotImplementedException();
        public override string PdpInterestId => throw new NotImplementedException();
        public override string SelectColorsClass => throw new NotImplementedException();
        public override string TrimColorsClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement PopularColors => Browser.Locate.ElementById(PopularColorsId);
        public override IElement PopularColorsTop => Browser.Locate.ElementByClassName(TopClass, PopularColors);

        public override IElement AllArtShadesLink => throw new NotImplementedException();
        public override IElement ArtShadeLink => throw new NotImplementedException();
        public override IElement CaretIcon => throw new NotImplementedException();
        public override IElement ColorCustomizerToggleVisibilityLink => throw new NotImplementedException();
        public override IElement CustomerReviewsElement => throw new NotImplementedException();
        public override IElement CustomerReviews => throw new NotImplementedException();
        public override IElement CustomizeColors => throw new NotImplementedException();
        public override IElement CustomizeColorsContent => throw new NotImplementedException();
        public override IElement CustomizeColorsTop => throw new NotImplementedException();
        public override IElement MorePatterns => throw new NotImplementedException();
        public override IElement ViewAllShadePatternsBtn => throw new NotImplementedException();
        public override IElement MorePatternsLink => throw new NotImplementedException();
        public override IElement OtherPatterns => throw new NotImplementedException();
        public override IElement OtherPatternsContent => throw new NotImplementedException();
        public override IElement OtherPatternsTop => throw new NotImplementedException();
        public override IElement PdpInterestElement => throw new NotImplementedException();
        public override IElement PopularColorsContent => throw new NotImplementedException();
        public override IElement TrimColors => throw new NotImplementedException();
        public override IElement SelectColors => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> ListOfPopularColors => Browser.Locate.ElementsByClassName(GicleeShadeOptionsThumbClass, PopularColors);

        public override ReadOnlyCollection<IElement> ArtShadeLinks => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfCustomizePatternColors => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfCustomizeSelectColors => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfOtherPatterns => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfTrimColors => throw new NotImplementedException();
        #endregion
    }
}
