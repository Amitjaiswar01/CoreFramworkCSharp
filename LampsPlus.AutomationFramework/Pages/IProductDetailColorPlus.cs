using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Interface for common PDP Color Plus behavior between desktop and mobile views.
    /// </summary>
    public interface IProductDetailColorPlus
    {
        #region Page Elements
        string AllBaseColorsId { get; }
        string AllBaseColorsString { get; }
        string ColorPlusClass { get; }
        string PdScrollableBaseOptionsId { get; }
        string PdScrollableContainerClass { get; }
        string PdImageCarousel { get; }
        string ProdViewAllColorsId { get; }

        IElement ColorPlusAllBaseColorsSection { get; }
        IElement ColorPlusBaseColorOptionsLabel { get; }
        IElement ColorPlusShadeOptionsLabel { get; }
        IElement ColorPlusSlider { get; }
        IElement ManufacturerLink { get; }
        IElement ManufacturerLinkAnchor { get; }
        IElement PdpMoreYouMayLikeElement { get; }
        IElement ViewAllColorsLink { get; }

        ReadOnlyCollection<IElement> ColorPlusListAllBaseSectionAnchors { get; }
        ReadOnlyCollection<IElement> ColorPlusListBaseOptionsWidgetAnchors { get; }
        ReadOnlyCollection<IElement> ProductSliders { get; }
        #endregion
    }
}
