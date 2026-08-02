using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class SortPlaBase : Page, ISortPla
    {
        /// <inheritdoc />
        protected SortPlaBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string BdProdDetailId { get; } = "bdProdDetail";

        public string AnswersText { get; } = "Answers";
        public string NotifyMyThankYouText { get; } = "Thank you! We'll notify you as soon as this product becomes available.";
        public string QuestionsText { get; } = "Questions";
        public string ReviewsText { get; } = "Reviews";
        public string EmailFieldId { get; } = "notifyMeEmail";
        public string PlaEmailClass { get; } = "notifyme__col1";
        public string PlaEmailNewClass { get; } = "notifyme__emailWrapper";  
        public string PdAddToCartId { get; } = "pdAddToCart";
        public string ProductNotAvailableCalloutId { get; } = "pdNotAvailable";
        public string ProductNotAvailableCalloutClass { get; } = "plaMsg";
        public string SfpQuickLookId { get; } = "sfpQuicklook";
        public string QlTitleSkuClass { get; } = "qlTitleSku";
        public string QlBkgId { get; } = "qlBkg";
        public string MoreDetailsId { get; } = "moreDetails";
        public string NotifyMeSubmitBtnId { get; } = "notifyMeSubmitBtn";
        public string TurnToRatingBoxClass { get; } = "goldReviewStars";
        public string TurnToRatingZeroZeroClass { get; } = "goldReviewStars--0-0";
        public string TurnToReadReviewsClass { get; } = "pdSummaryTeaser__reviewCount";
        public string ReadQuestionsId { get; } = "readQuestions";
        public string ShipsTodayQLClass { get; } = "shipsTodayQL";
        public string ShipsTodayPdpClass { get; } = "shipsInMessage";
        public string TurnToTeaserBlockClass { get; } = "pdSummaryTeaser--link";

        public abstract string PlaCertonaImageLoadedXpath { get; }
        public abstract string QlViewDetailsId { get; }
        public abstract string PlaDetailsId { get; }
        public abstract string PlaMainImageLoadedXpath { get; }
        public abstract string PlaMainImageClass { get; }
        public abstract string PlaSortStickyFilterHeaderId { get; }
        public abstract string ProductTitleClass { get; }
        public abstract string ProductImageClass { get; }
        public abstract string ProductLargeImageContainerId { get; }
        public abstract string PhoneNumberClass { get; }
        public abstract string LargeImageCloseButtonClass { get; }
        public abstract string AttributeWrapper { get; }
        public abstract string QlTitleWrapperClass { get; }
        #endregion

        #region Page Elements
        //Elements that are the same in both Desktop and Mobile views.
        public IElement PdpBodyElement => Browser.Locate.ElementById(BdProdDetailId);
        public IElement PlaFrameElement => Browser.Locate.ElementById(SfpQuickLookId);
        public IElement PlaNotifyMeButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, NotifyMeSubmitBtnId);
        public IElement PlaAddToCartElement => Browser.Locate.ElementById(PdAddToCartId);
        public IElement PlaProductSkuElement => Browser.Locate.ElementByClassName(QlTitleSkuClass);
        public IElement PlaFullCertonaElement => Browser.Locate.ElementById(QlBkgId);
        public IElement SfpTurnToTeaserBlock => Browser.Locate.ElementBySelector(TurnToTeaserBlockClass.ToCssClassSelector());

        public abstract IElement PlaEmail { get; }
        public abstract IElement PlaEmailNew { get; }
        public abstract IElement ProductNotAvailableCallout { get; }
        public abstract IElement ProductNotAvailableCalloutNew { get; }
        public abstract IElement PhoneNumber { get; }
        public abstract IElement EmailField { get; }

        //Element that exists in Desktop view and not Mobile view.
        public abstract IElement PlaFilters { get; }
        public abstract IElement PlaMoreDetailsLinkElement { get; }
        public abstract IElement PlaRatingBoxElement { get; }
        public abstract IElement PlaReadReviewsElement { get; }
        public abstract IElement PlaProductTitleElement { get; }
        public abstract IElement PlaViewLargerLinkElement { get; }
        public abstract IElement PlaLargeImageElement { get; }
        public abstract IElement PlaCloseButtonElement { get; }
        public abstract IElement PlaQuestionsElement { get; }

        //Element that exists in Mobile view and not Desktop view.
        public abstract IElement PlaSortStickyFilterHeader { get; }
        public abstract IElement ShipsTodayQLElement { get; }
        public abstract IElement ShipsTodayPdpElement { get; }
        #endregion

		/// <summary>
		/// Get a PLA sku that contains Reviews as well as Questions and Answers
		/// </summary>
		/// <returns></returns>
		public string GetPlaSkuWithReviews()
        {
            var plaSkus = new List<string>
            {
                "22087",
                "69794",
                "8c397"
            };

            var randomSkuList = plaSkus.OrderBy(i => Guid.NewGuid()).ToList();

            return randomSkuList[0];
        }

        public abstract IElement NavigateToPlaWithReviews(string url, string sku);
        public abstract IElement VerifyPlaStickyHeader();
        public abstract void ClickOnPlaProduct();
    }
}
