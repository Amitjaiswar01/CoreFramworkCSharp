using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/samba-china-red-silk-shade-apothecary-table-lamp__29j32.html
    /// </summary>
    public class ProductDetailColorPlus : ProductDetailColorPlusBase
    {
        public ProductDetailColorPlus(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }

        #region CSS Selectors
        public override string AllBaseColorsId { get; } = "all-base-colors";
        public override string ColorPlusClass { get; } = "colorPlus";
        public override string PdScrollableBaseOptionsId { get; } = "pdScrollableBaseOptions";
        public override string PdScrollableContainerClass { get; } = "pdScrollableContainer";
        public override string ProdViewAllColorsId { get; } = "prodViewAllColors";

        public override string AllBaseColorsString => throw new System.NotImplementedException();
        public override string PdImageCarousel => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement ColorPlusSlider => Browser.Locate.ElementByClassName(ColorPlusClass, ProductDetail.ProductSlider);
        public override IElement ColorPlusAllBaseColorsSection => Browser.Locate.ElementById(AllBaseColorsId);
        public override IElement ColorPlusBaseColorOptionsLabel => ProductDetail.GetScrollableHeaderByName(BaseColorOptionsString);
        public override IElement ColorPlusShadeOptionsLabel => ProductDetail.GetScrollableHeaderByName(ShadeOptionsString);
        public override IElement ViewAllColorsLink => Browser.Locate.ElementById(ProdViewAllColorsId);

        public override ReadOnlyCollection<IElement> ColorPlusListAllBaseSectionAnchors => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.A, ColorPlusAllBaseColorsSection);
        public override ReadOnlyCollection<IElement> ProductSliders => Browser.Locate.ElementsByClassName(PdScrollableContainerClass);
        public override ReadOnlyCollection<IElement> ColorPlusListBaseOptionsWidgetAnchors =>
            Browser.Locate.ElementsBySelector($"{PdScrollableBaseOptionsId.ToCssIdSelector()} {ColorPlusClass.ToCssClassSelector()} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.A}");
        #endregion
    }
}
