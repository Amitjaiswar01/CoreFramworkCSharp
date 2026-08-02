using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortPla
{
    public class SortPlaDesktop : ISortPlaDesktop
    {
        //Class members
        private string _sfpQuicklookId = "sfpQuicklook";
        private string _goldReviewStarsClass = "goldReviewStars";
        private string _goldReviewStarsZeroClass = "goldReviewStars--0-0";
        private string _sfpclassClass = "sfpclass";
        private string _qlViewDetailsId = "qlViewDetails";
        private string _turntoReviewsSectionId = "turntoReviewsSection";

        protected string _pdAddToCartId => "pdAddToCart";

        private IElement PlaFrameElement => Browser.Locate.ElementById(_sfpQuicklookId);
        private IElement PlaReadReviewsElement => Browser.Locate.ElementBySelector("divReview".ToCssIdSelector());
        private IElement PlaRatingBoxElement => Browser.Locate.ElementByClassName(_goldReviewStarsClass);

        protected IElement AddToCartButton => Browser.Locate.ElementById(_pdAddToCartId);
        protected virtual IElement MoreDetailsLink => Browser.Locate.ElementById(_qlViewDetailsId);

        //Instances
        protected IBrowser Browser;

        public SortPlaDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_sfpclassClass.ToCssClassSelector()));
        public bool IsReviewsSectionDisplayed => Browser.Wait.IsVisibleElement(By.Id(_turntoReviewsSectionId));

        public string GetPlaSkuWithReviews()
        {
            var plaSkus = new List<string>
            {
                "22087",
                "69794",
                "8C397"
            };

            var randomSkuList = plaSkus.OrderBy(i => Guid.NewGuid()).ToList();

            return randomSkuList[0];
        }

        public virtual void NavigateToPlaWithReviews(string url, string sku)
        {
            Browser.Wait.ForDomReady();
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.SwitchFocusToIframe(PlaFrameElement);
        }

        public bool DoesReviewSummaryContainReviewsText()
        {
            var plaReviewText = TextActions.RegexNoTabsAndNewLines(PlaReadReviewsElement.Text).Trim();
            return plaReviewText.EndsWith("Reviews");
        }

        public bool DoesPlaRatingStarsDisplay()
        {
            return PlaRatingBoxElement.GetAttribute(HtmlTextWriterAttribute.Class.ToString()).Split(Page.SingleSpaceChart).Any(a => a.Equals(_goldReviewStarsZeroClass));
        }

        public void RedirectToCustomerReviewsSection()
        {
            PlaRatingBoxElement.Click();
        }

        public virtual void NavigateToPdpThroughMoreDetails()
        {
            Browser.ClickByJs(MoreDetailsLink);
        }

        public virtual void PlaAddToCart()
        {
            Browser.ScrollIntoView(AddToCartButton);
            Browser.Wait.ForDomReady();
            Browser.ScrollToBottomOfWindow();
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForClickableElement(AddToCartButton, 10);
            AddToCartButton.Click();
            Browser.Wait.IsInvisibleElement(By.CssSelector(_pdAddToCartId.ToCssIdSelector()));
        }
    }
}