using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class ProductDetailTiffanyColorPlus : ProductDetailTiffanyColorPlusBase
    {
        public ProductDetailTiffanyColorPlus(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }

        #region CSS Selectors
        public override string AllBaseColorsId { get; } = "all-base-colors";
        public override string ProdViewAllColorsId { get; } = "prodViewAllColors";

        public override string AllBaseColorsString => throw new NotImplementedException();
        public override string PdImageCarousel => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement TiffanyAllBaseColorsSection => Browser.Locate.ElementById(AllBaseColorsId);
        public override IElement TiffanyBaseOptionsLabel => ProductDetail.GetScrollableHeaderByName(BaseOptionsString);
        public override IElement TiffanyColorPlusSlider => Browser.Locate.ElementByClassName(ColorPlusClass, ProductDetail.ProductSlider);
        public override IElement TiffanyShadeOptionsLabel => ProductDetail.GetScrollableHeaderByName(ShadeOptionsString);
        public override IElement TiffanyViewAllColorsLink => Browser.Locate.ElementById(ProdViewAllColorsId);

        public override ReadOnlyCollection<IElement> TiffanyListBaseOptionsWidgetAnchors =>
            Browser.Locate.ElementsBySelector($"{PdScrollableBaseOptionsId.ToCssIdSelector()} {ColorPlusClass.ToCssClassSelector()} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.A}");
        public override ReadOnlyCollection<IElement> TiffanyListAllBaseSectionAnchors => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.A, TiffanyAllBaseColorsSection);
        #endregion
    }
}
