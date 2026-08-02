using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	public interface IProductDetailTrackLighting
	{
        #region CSS Selectors
	    string BuildFullSystemId { get; }
	    string BuildFullSystemOptionsId { get; }
	    string DyoBannerClass { get; }
	    string PdAddToPortfolioSystemOptionsId { get; }
	    string PdAddToCartSystemOptionsId { get; }
        string LeftId { get; }
        #endregion

        #region Page Elements
        IElement BuildFullSystemAddToCartButton { get; }
        IElement BuildFullSystemAddToWishListButton { get; }
        IElement BuildFullSystemContainer { get; }
		IElement BuildFullSystemOptions { get; }
		IElement DesignYourOwnTrackLightingSystemBanner { get; }
        IElement DyotsSelectRoom { get; }

        ReadOnlyCollection<IElement> ListOfFullSystemProductNames { get; }
		ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }
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
