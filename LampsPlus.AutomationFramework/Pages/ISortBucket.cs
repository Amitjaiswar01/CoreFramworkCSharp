using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface ISortBucket
	{
		#region Page Elements
		/// <summary>
		/// Hybrid element in the upper right corner.
		/// </summary>
		IElement SplashMessageElement { get; }

		/// <summary>
		/// Group of buckets in the area between h1 and filters.
		/// </summary>
		IElement BucketContainerElement { get; }

		/// <summary>
		/// Splash Bucket Container elements.
		/// </summary>
		ReadOnlyCollection<IElement> SplashBucketContainerElements { get; }
		#endregion

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
	}
}
