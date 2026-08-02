using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common PDP  Color Plus behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailColorPlusBase : Page, IProductDetailColorPlus
    {
        protected ProductDetailColorPlusBase(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser)
        {
            GlobalLocators = globalLocators;
            ProductDetail = productDetail;
        }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        internal IProductDetail ProductDetail { get; }
        
        public string BaseColorOptionsString { get; } = "Base Color Options";
        public string ShadeOptionsString { get; } = "Shade Options";
        #endregion

        #region CSS Selectors
        private string PdManufacturerLinkClass { get; } = "pdManufacturerLink";
        private string PdMoreYouMayLikeId { get; } = "pdMoreYouMayLike";

        public abstract string PdScrollableBaseOptionsId { get; }
        public abstract string ColorPlusClass { get; }
        public abstract string AllBaseColorsId { get; }
        public abstract string PdScrollableContainerClass { get; }
        public abstract string ProdViewAllColorsId { get; }
        public abstract string AllBaseColorsString { get; }
        public abstract string PdImageCarousel { get; }
        #endregion

        #region Page Elements
        public IElement ManufacturerLink => Browser.Locate.ElementByClassName(PdManufacturerLinkClass);
        public IElement ManufacturerLinkAnchor => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, ManufacturerLink);
        public IElement PdpMoreYouMayLikeElement => Browser.Locate.ElementById(PdMoreYouMayLikeId);

        public abstract IElement ColorPlusAllBaseColorsSection { get; }
        public abstract IElement ColorPlusBaseColorOptionsLabel { get; }
        public abstract IElement ColorPlusSlider { get; }
        public abstract IElement ColorPlusShadeOptionsLabel { get; }
        public abstract IElement ViewAllColorsLink { get; }

        public abstract ReadOnlyCollection<IElement> ColorPlusListAllBaseSectionAnchors { get; }
        public abstract ReadOnlyCollection<IElement> ProductSliders { get; }
        public abstract ReadOnlyCollection<IElement> ColorPlusListBaseOptionsWidgetAnchors { get; }
        #endregion
    }
}
