using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/chandeliers/style_crystal/?sfp=00044.
    /// </summary>
    public class SortPla : SortPlaBase
    {
        public SortPla(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string AttributeWrapper { get; } = "attributeWrapper";
        public override string PhoneNumberClass { get; } = "pdInformationNumber";
        public override string PlaMainImageLoadedXpath { get; } = "//*[@id='pdImgContainer']//div[contains(@aria-hidden, 'false')]";
        public override string QlTitleWrapperClass { get; } = "qlTitleWrapper";
        public override string PlaMainImageClass { get; } = "slick-active";
        public override string QlViewDetailsId { get; } = "qlViewDetails";

        public override string PlaCertonaImageLoadedXpath => throw new NotImplementedException();
        public override string PlaDetailsId => throw new NotImplementedException();
        public override string PlaSortStickyFilterHeaderId => throw new NotImplementedException();
        public override string ProductTitleClass => throw new NotImplementedException();
        public override string ProductImageClass => throw new NotImplementedException();
        public override string ProductLargeImageContainerId => throw new NotImplementedException();
        public override string LargeImageCloseButtonClass => throw new NotImplementedException();
        #endregion
        public override IElement PlaFilters => Browser.Locate.ElementById(AttributeWrapper);
        public override IElement PlaMoreDetailsLinkElement => Browser.Locate.ElementByXpath("//*[@id='qlViewDetails']");
        public override IElement PlaRatingBoxElement => Browser.Locate.ElementByClassName(TurnToRatingBoxClass);
        public override IElement PlaReadReviewsElement => Browser.Locate.ElementBySelector("divReview".ToCssIdSelector());
        public override IElement PlaQuestionsElement => Browser.Locate.ElementById(ReadQuestionsId);
        public override IElement PlaProductTitleElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(QlTitleWrapperClass));

        public override IElement PlaViewLargerLinkElement => throw new NotImplementedException();
        public override IElement PlaLargeImageElement => throw new NotImplementedException();
        public override IElement PlaCloseButtonElement => throw new NotImplementedException();
        public override IElement PlaSortStickyFilterHeader => throw new NotImplementedException();
        public override IElement ShipsTodayQLElement => throw new NotImplementedException();
        public override IElement ShipsTodayPdpElement => throw new NotImplementedException();
        public override IElement ProductNotAvailableCallout => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, Browser.Locate.ElementById(ProductNotAvailableCalloutId));
        public override IElement PlaEmail => Browser.Locate.ElementByClassName(PlaEmailClass);
        public override IElement PhoneNumber => Browser.Locate.ElementByClassName(PhoneNumberClass);
        public override IElement EmailField => Browser.Locate.ElementById(EmailFieldId);
        public override IElement ProductNotAvailableCalloutNew => throw new NotImplementedException();
        public override IElement PlaEmailNew => throw new NotImplementedException();

        public override IElement NavigateToPlaWithReviews(string url, string sku)
        {
            Browser.Wait.ForDomReady();
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.SwitchFocusToIframe(PlaFrameElement);
            return PlaFrameElement;
        }

        public override IElement VerifyPlaStickyHeader() => throw new NotImplementedException();
        public override void ClickOnPlaProduct()
        {
            PlaRatingBoxElement.Click();
        }
    }
}
