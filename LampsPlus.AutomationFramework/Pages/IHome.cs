using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// https://www.lampsplus.com/
	/// </summary>
	public interface IHome
	{
        #region CSS Selectors
        string CartCountId { get; }
        string InvisibleClass { get; }
        string PlaDetailsId { get; }
        string HomeHeaderClass { get; }
        string PlaAddToCartId { get; }
        string BdHomePageId { get;}
        string HomepageSplashBannerClass { get; }
        string HpSplashImgClass { get; }
        string IsHospitalityClass { get; }
        string PlaViewDetailsLinkXpath { get; }
        #endregion

        #region Page Elements
        IElement BodyElement { get; }
        IElement CareersLinks { get; }
        IElement CartCountElement { get; }
        IElement InstagramModal { get; }
        IElement PlaReviews { get; }
        IElement PlaReviewStars { get; }
        IElement PlaViewDetailsLinkElement { get; }
        IElement StoreNumberField { get; }
		#endregion

        /// <summary>
        /// If logged in account is set to store in session or not.
        /// </summary>
        /// <returns></returns>
        bool IsStoreInSession();

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        /// <summary>
        /// Clear the store in session.
        /// </summary>
        void ClearStoreInSession();

        /// <summary>
        /// Enter store number to put the site in Store in Session mode.
        /// </summary>
        /// <param name="storeNumber">Store number to enter for the Store in Session.</param>
        void EnterStoreInSession(string storeNumber);

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
    }
}
