using System;
using System.Collections.ObjectModel;
using System.Linq;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/samba-china-red-silk-shade-apothecary-table-lamp__29j32.html
    /// </summary>
    public class MobileProductDetailColorPlus : ProductDetailColorPlusBase
    {
        public MobileProductDetailColorPlus(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }

        #region CSS Selectors
        public override string AllBaseColorsString { get; } = "All Base Colors";
        public override string PdImageCarousel { get; } = "pd-images-carousel";

        public override string PdScrollableBaseOptionsId => throw new NotImplementedException();
        public override string ColorPlusClass => throw new NotImplementedException();
        public override string AllBaseColorsId => throw new NotImplementedException();
        public override string PdScrollableContainerClass => throw new NotImplementedException();
        public override string ProdViewAllColorsId => throw new NotImplementedException();
        #endregion

        #region Page Elements        
        public override IElement ColorPlusAllBaseColorsSection => GlobalLocators.PdpDrawerElements.Single(elem => elem.Text.Contains(AllBaseColorsString));
        public override IElement ColorPlusBaseColorOptionsLabel => ProductDetail.GetCourseTitleByName(BaseColorOptionsString);
        public override IElement ColorPlusSlider => Browser.Locate.ElementByClassName(PdImageCarousel);
        public override IElement ColorPlusShadeOptionsLabel => ProductDetail.GetCourseTitleByName(ShadeOptionsString);

        public override IElement ViewAllColorsLink => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> ColorPlusListAllBaseSectionAnchors => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ProductSliders => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ColorPlusListBaseOptionsWidgetAnchors => throw new NotImplementedException();
        #endregion
    }
}
