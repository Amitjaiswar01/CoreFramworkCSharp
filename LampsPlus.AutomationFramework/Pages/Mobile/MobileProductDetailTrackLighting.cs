using System;
using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
   public class MobileProductDetailTrackLighting : ProductDetailTrackLightingBase
    {
        public MobileProductDetailTrackLighting(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors
        public override string BuildFullSystemId { get; } = "buildFullSystemContainer";
        public override string BuildFullSystemOptionsId => throw new NotImplementedException();
        public override string DyoBannerClass => throw new NotImplementedException();
        public override string PdAddToPortfolioSystemOptionsId => throw new NotImplementedException();
        public override string PdAddToCartSystemOptionsId => throw new NotImplementedException();
        public override string LeftId => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);

        public override IElement BuildFullSystemAddToCartButton => throw new NotImplementedException();
        public override IElement BuildFullSystemAddToWishListButton => throw new NotImplementedException();
        public override IElement BuildFullSystemOptions => throw new NotImplementedException();
        public override IElement DesignYourOwnTrackLightingSystemBanner => throw new NotImplementedException();
        public override IElement DyotsSelectRoom => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> ListOfFullSystemProductNames => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => throw new NotImplementedException();
        #endregion
    }
}
