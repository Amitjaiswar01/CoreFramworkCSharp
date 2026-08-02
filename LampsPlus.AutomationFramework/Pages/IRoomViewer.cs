using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IRoomViewer
	{
		#region Page Elements
		/// <summary>
		/// Room Viewer Add To Wishlist Button
		/// </summary>
		IElement RoomViewerAddToWishlistButton { get; }

		/// <summary>
		/// Room Viewer Add To Cart Button
		/// </summary>
		IElement RoomViewerAddToCartButton { get; }

		/// <summary>
		/// Room Viewer Active Room
		/// </summary>
		IElement ActiveRoom { get; }
		#endregion

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
	}
}
