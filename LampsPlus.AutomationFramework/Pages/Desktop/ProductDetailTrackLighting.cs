using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    public class ProductDetailTrackLighting : ProductDetailTrackLightingBase
    {
        public ProductDetailTrackLighting(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors
        public override string BuildFullSystemId { get; } = "build-full-system";
        public override string BuildFullSystemOptionsId { get; } = "pdFullSystemOptions";
        public override string DyoBannerClass { get; } = "dyoBanner";
        public override string PdAddToPortfolioSystemOptionsId { get; } = "pdAddToPortfolioSystemOptions";
        public override string PdAddToCartSystemOptionsId { get; } = "pdAddToCartSystemOptions";
        public override string LeftId { get; } = "left";
        #endregion

        #region Page Elements
        public override IElement BuildFullSystemAddToCartButton => Browser.Locate.ElementById(PdAddToCartSystemOptionsId);
        public override IElement BuildFullSystemAddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioSystemOptionsId);
        public override IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        public override IElement BuildFullSystemOptions => Browser.Locate.ElementById(BuildFullSystemOptionsId);
        public override IElement DesignYourOwnTrackLightingSystemBanner => Browser.Locate.ElementByClassName(DyoBannerClass);
        public override IElement DyotsSelectRoom => Browser.Locate.ElementById(LeftId);
        
        public override ReadOnlyCollection<IElement> ListOfFullSystemProductNames => ListOfFullSystemData(2);
        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => ListOfFullSystemData(3);
        #endregion
    }
}
