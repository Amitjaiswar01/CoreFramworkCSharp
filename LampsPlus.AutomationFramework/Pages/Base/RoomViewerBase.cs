using Automation.Framework;
using Automation.Framework.Core;

namespace LampsPlus.AutomationFramework.Pages.Base
{
	/// <summary>
	/// Base class for common behavior between desktop and mobile views.
	/// </summary>
	public abstract class RoomViewerBase : Page, IRoomViewer
	{
		/// <inheritdoc />
		protected RoomViewerBase(IBrowser browser) : base(browser) { }

		#region CSSSelectors
		private string AddToListClass { get; } = "addToList";
		private string AddSelectedCartClass { get; } = "addSelectedCart";
		private string ScenesContainerClass { get; } = "scenesContainer";
		#endregion

		#region Page Elements
		public IElement ActiveRoom => Browser.Locate.ElementByClassName(ActiveClass, Browser.Locate.ElementByClassName(ScenesContainerClass));
		public IElement RoomViewerAddToWishlistButton => Browser.Locate.ElementByClassName(AddToListClass);
		public IElement RoomViewerAddToCartButton => Browser.Locate.ElementByClassName(AddSelectedCartClass);
		#endregion
	}
}
