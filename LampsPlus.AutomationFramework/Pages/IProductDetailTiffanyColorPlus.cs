using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public interface IProductDetailTiffanyColorPlus
	{
        #region Page Elements
        IElement TiffanyAllBaseColorsSection { get; }
        IElement TiffanyBaseOptionsLabel { get; }
        IElement TiffanyColorPlusSlider { get; }
        IElement TiffanyShadeOptionsLabel { get; }
        IElement TiffanyViewAllColorsLink { get; }
        string AllBaseColorsString { get; }
        string PdImageCarousel { get; }
        string AllBaseColorsId { get; }
        string ProdViewAllColorsId { get; }

        ReadOnlyCollection<IElement> TiffanyListBaseOptionsWidgetAnchors { get; }
        ReadOnlyCollection<IElement> TiffanyListAllBaseSectionAnchors { get; }
        #endregion
	

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }
    }
}
