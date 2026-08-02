using System.Collections.ObjectModel;
using Automation.Framework;
using OpenQA.Selenium;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class SortBucketBase : Page, ISortBucket
	{
        /// <summary>
        /// Create a Sort Bucket page object.
        /// </summary>
        /// <param name="browser">The browser to test against.</param>
        /// <param name="globalLocators"></param>
        protected SortBucketBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        #endregion

        #region CSS Selectors
        private string SortSplashRightMssgLongClass { get; } = "sortSplashRightMssgLong";
        private string SortSplashBucketsContainerClass { get; } = "sortSplashBucketsContainer";
        private string SortSplashBucketsClass { get; } = "sortSplashBuckets";
        #endregion

        #region Page Elements
        /// <summary>
        /// Hybrid element in the upper right corner.
        /// </summary>
        public IElement SplashMessageElement => Browser.Locate.ElementByClassName(SortSplashRightMssgLongClass);

		/// <summary>
		/// Group of buckets in the area between h1 and filters.
		/// </summary>
		public IElement BucketContainerElement => Browser.Locate.ElementByClassName(SortSplashBucketsContainerClass);

		/// <summary>
		/// Splash Bucket Container elements.
		/// </summary>
		public ReadOnlyCollection<IElement> SplashBucketContainerElements => Browser.Locate.ElementByClassName(SortSplashBucketsContainerClass).FindElements(By.ClassName(SortSplashBucketsClass));
        #endregion
    }
}
