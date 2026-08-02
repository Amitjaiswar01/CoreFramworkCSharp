using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface ISortPla
	{
        #region Class Setup
	    string AnswersText { get; }
	    string AttributeWrapper { get; }
        string LargeImageCloseButtonClass { get; }
        string MoreDetailsId { get; }
        string NotifyMeSubmitBtnId { get; }
        string NotifyMyThankYouText { get; }
        string PhoneNumberClass { get; }
        string PlaCertonaImageLoadedXpath { get; }
        string QlViewDetailsId { get; }
        string PlaDetailsId { get; }
        string PlaMainImageLoadedXpath { get; }
        string PlaMainImageClass { get; }
        string PlaSortStickyFilterHeaderId { get; }
	    string ProductImageClass { get; }
	    string ProductLargeImageContainerId { get; }
        string ProductTitleClass { get; }
	    string QuestionsText { get; }
	    string QlTitleWrapperClass { get; }
        string ReviewsText { get; }
        string SfpQuickLookId { get; }
        string TurnToRatingBoxClass { get; }
        string TurnToRatingZeroZeroClass { get; }
        string TurnToReadReviewsClass { get; }
        string TurnToTeaserBlockClass { get; }
        #endregion

        #region Page Elements
        IElement PdpBodyElement { get; }
        IElement PlaFilters { get; }
        IElement PlaFrameElement { get; }
		IElement PlaMoreDetailsLinkElement { get; }
		IElement PlaAddToCartElement { get; }
        IElement PlaProductSkuElement { get; }
        IElement PlaFullCertonaElement { get; }
        IElement PlaRatingBoxElement { get; }
        IElement PlaReadReviewsElement { get; }
        IElement PlaQuestionsElement { get; }
        IElement PlaProductTitleElement { get; }
	    IElement PlaSortStickyFilterHeader { get; }
        IElement PlaViewLargerLinkElement { get; }
        IElement PlaLargeImageElement { get; }
        IElement PlaCloseButtonElement {get; }
        IElement PlaEmail { get; }
        IElement PlaEmailNew { get; }
        IElement ProductNotAvailableCallout { get; }
        IElement ProductNotAvailableCalloutNew { get; }
        IElement PlaNotifyMeButton { get; }
        IElement PhoneNumber { get; }
        IElement EmailField { get; }
        IElement SfpTurnToTeaserBlock { get; }
        IElement ShipsTodayQLElement { get; }
        IElement ShipsTodayPdpElement { get; }
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
        /// Get a PLA sku that contains Reviews as well as Questions and Answers
        /// </summary>
        /// <returns></returns>
        string GetPlaSkuWithReviews();

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        IElement NavigateToPlaWithReviews(string url, string sku);
        IElement VerifyPlaStickyHeader();
        void ClickOnPlaProduct();
    }
}
