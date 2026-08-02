using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    public class ProductDetailMcp : ProductDetailMcpBase
    {
        public ProductDetailMcp(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string ArtShadeCategoryClass { get; } = "artShadeCategory";
        public override string ArtShadeLinksId { get; } = "artShadeLinks";
        public override string CaretClass { get; } = "caret";
        public override string ColorClass { get; } = "color";
        public override string ColorCustomizerHiddenClass { get; } = "colorCustomizerHidden";
        public override string ColorCustomizerToggleVisibilityClass { get; } = "colorCustomizerToggleVisibility";
        public override string ContentClass { get; } = "content";
        public override string CustomerReviewId { get; } = "turntoReviewsSection";
        public override string CustomerReviewXpath { get; } = "//*[@id='turntoReviewsSection']/div[1]/div[1]";
        public override string CustomizeColorsId { get; } = "customizeColors";
        public override string MoreOptionsId { get; } = "moreOptions";
        public override string MorePatternsString { get; } = "More Patterns";
        public override string ViewAllShadePatternsClass { get; } = "pdpMorePatterns";
        public override string PdpInterestId { get; } = "pdPinterest";
        public override string SelectColorsClass { get; } = "selectColors";
        public override string TrimColorsClass { get; } = "trimColors";
        #endregion

        #region Page Elements
        public override IElement AllArtShadesLink => Browser.Locate.ElementBySelector("#artShadeLinks > a.artShadeCategory");
        public override IElement ArtShadeLink => Browser.Locate.ElementById(ArtShadeLinksId);
        public override IElement CaretIcon => Browser.Locate.ElementByClassName(CaretClass);
        public override IElement ColorCustomizerToggleVisibilityLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.A, ColorCustomizerToggleVisibilityClass);
        public override IElement CustomerReviewsElement => Browser.Locate.ElementById(CustomerReviewId);
        public override IElement CustomerReviews => Browser.Locate.ElementByXpath(CustomerReviewXpath);
        public override IElement CustomizeColors => Browser.Locate.ElementById(CustomizeColorsId);
        public override IElement CustomizeColorsContent => Browser.Locate.ElementByClassName(ContentClass, CustomizeColors);
        public override IElement CustomizeColorsTop => Browser.Locate.ElementBySelector("#customizeColors> div > div.top");
        public override IElement MorePatterns => Browser.Locate.ElementWithText(ArtShadeLinks, AttributeSelectorType.Equals, MorePatternsString);
        public override IElement ViewAllShadePatternsBtn => Browser.Locate.ElementByClassName(ViewAllShadePatternsClass);
        public override IElement MorePatternsLink => Browser.Locate.ElementBySelector($"{ColorCustomizerToggleVisibilityClass.ToCssClassSelector()}, {ColorCustomizerHiddenClass.ToCssClassSelector()}");
        public override IElement OtherPatterns => Browser.Locate.ElementById(MoreOptionsId);
        public override IElement OtherPatternsContent => Browser.Locate.ElementByClassName(ContentClass, OtherPatterns);
        public override IElement OtherPatternsTop => Browser.Locate.ElementBySelector("#moreOptions> div > div.top");
        public override IElement PdpInterestElement => Browser.Locate.ElementById(PdpInterestId);
        public override IElement PopularColors => Browser.Locate.ElementById(PopularColorsId);
        public override IElement PopularColorsContent => Browser.Locate.ElementBySelector("#popularColors > div > div.content");
        public override IElement PopularColorsTop => Browser.Locate.ElementBySelector("#popularColors > div > div.top");
        public override IElement TrimColors => Browser.Locate.ElementBySelector(TrimColorsClass, CustomizeColors);
        public override IElement SelectColors => Browser.Locate.ElementBySelector(SelectColorsClass, CustomizeColors);

        public override ReadOnlyCollection<IElement> ArtShadeLinks => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.A, ArtShadeLink);
        public override ReadOnlyCollection<IElement> ListOfCustomizePatternColors => Browser.Locate.ElementsByClassName(ColorClass, CustomizeColorsContent);
        public override ReadOnlyCollection<IElement> ListOfCustomizeSelectColors => Browser.Locate.ElementsByClassName(ColorClass, SelectColors);
        public override ReadOnlyCollection<IElement> ListOfOtherPatterns => OtherPatterns.FindElements(By.ClassName(GicleeShadeOptionsThumbClass));
        public override ReadOnlyCollection<IElement> ListOfPopularColors => Browser.Locate.ElementsBySelector("#popularColors .pdGicleeShadeOptionsThumb");
        public override ReadOnlyCollection<IElement> ListOfTrimColors => Browser.Locate.ElementsBySelector("div.trimColorsWrapper > .trimColors> .color");
        #endregion
    }
}
