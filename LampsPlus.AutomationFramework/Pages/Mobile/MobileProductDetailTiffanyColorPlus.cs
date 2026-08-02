using System;
using System.Collections.ObjectModel;
using System.Linq;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class MobileProductDetailTiffanyColorPlus : ProductDetailTiffanyColorPlusBase
    {
        public MobileProductDetailTiffanyColorPlus(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }
        
        #region Class Setup
        public override string AllBaseColorsString { get; } = "All Bases";
        public override string PdImageCarousel { get; } = "pd-images-carousel";

        public override string AllBaseColorsId => throw new NotImplementedException();
        public override string ProdViewAllColorsId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement TiffanyAllBaseColorsSection => GlobalLocators.PdpDrawerElements.Single(elem => elem.Text.Contains(AllBaseColorsString));
        public override IElement TiffanyBaseOptionsLabel => ProductDetail.GetCourseTitleByName(BaseOptionsString);
        public override IElement TiffanyColorPlusSlider => Browser.Locate.ElementByClassName(PdImageCarousel);
        public override IElement TiffanyShadeOptionsLabel => ProductDetail.GetCourseTitleByName(ShadeOptionsString);


        public override IElement TiffanyViewAllColorsLink => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> TiffanyListBaseOptionsWidgetAnchors => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> TiffanyListAllBaseSectionAnchors => throw new NotImplementedException();
        #endregion
    }
}
