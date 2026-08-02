using System.Collections.ObjectModel;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailTiffanyColorPlusBase : Page, IProductDetailTiffanyColorPlus
    {
        protected ProductDetailTiffanyColorPlusBase(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser)
        {
            GlobalLocators = globalLocators;
            ProductDetail = productDetail;
        }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        internal IProductDetail ProductDetail { get; }

        public string BaseOptionsString => "Base Options";
        public string ShadeOptionsString => "Shade Options";
        #endregion

        #region CSS Selector Strings
        public string ColorPlusClass { get; } = "colorPlus";
        public string PdScrollableBaseOptionsId { get; } = "pdScrollableBaseOptions";

        public abstract string AllBaseColorsId { get; }
        public abstract string AllBaseColorsString { get; }
        public abstract string PdImageCarousel { get; }
        public abstract string ProdViewAllColorsId { get; }
        #endregion

        #region Page Elements
        public abstract IElement TiffanyAllBaseColorsSection { get; }
        public abstract IElement TiffanyBaseOptionsLabel { get; }
        public abstract IElement TiffanyColorPlusSlider { get; }
        public abstract IElement TiffanyShadeOptionsLabel { get; }
        public abstract IElement TiffanyViewAllColorsLink { get; }
        
        
        public abstract ReadOnlyCollection<IElement> TiffanyListBaseOptionsWidgetAnchors { get; }
        public abstract ReadOnlyCollection<IElement> TiffanyListAllBaseSectionAnchors { get; }
        #endregion
    }
}
