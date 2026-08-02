using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface ILightingCollection
    {
        string ProductPrice { get; }
        string ProductSku { get; }

        #region Page Elements
        IElement CandleHolderSetImage { get; }
        IElement CheckBoxElement { get; }
        IElement LightingCollectionElement { get; }
        IElement RelatedVideosSlider { get; }
        IElement ViewDetailsElement { get; }
        IElement TopTrendingSlider { get; }
        #endregion
    }
}
